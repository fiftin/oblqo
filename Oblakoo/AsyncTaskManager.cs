using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Oblakoo.Tasks;
// ReSharper disable CanBeReplacedWithTryCastAndCheckForNull

namespace Oblakoo
{
    public class AsyncTaskManager : IEnumerable<AsyncTask>
    {
        private class TaskInfo
        {
            public DateTime StartTime { get; set; }
            public AsyncTask AsyncTask { get; set; }
            public Task Task { get; private set; }

            public TaskInfo(AsyncTask asyncTask, Task task)
            {
                StartTime = DateTime.Now;
                AsyncTask = asyncTask;
                Task = task;
            }
        }

        private readonly List<AsyncTask> tasks = new List<AsyncTask>();
        private int maxNumberOfTasksRunning = 5;
        private readonly AutoResetEvent newTaskEvent = new AutoResetEvent(true);
        public readonly object SyncRoot = new object();
        private bool running;
        private int numberOfTasksRunning;
        private readonly List<TaskInfo> runningTasks = new List<TaskInfo>();
        private Task checkingTaskStatesTask;


        public void Add(AsyncTask task)
        {
            lock (SyncRoot)
            {
                tasks.Add(task);
                task.StateChanged += task_StateChanged;
                task.Progress += task_Progress;
                task.Manager = this;
            }
            newTaskEvent.Set();
            if (TaskAdded != null)
                TaskAdded(this, new AsyncTaskEventArgs(task));
        }

        void task_Progress(object sender, AsyncTaskProgressEventArgs e)
        {
            if (TaskProgress != null)
                TaskProgress(this, new AsyncTaskEventArgs<AsyncTaskProgressEventArgs>((AsyncTask)sender, e));
        }

        private void task_StateChanged(object sender, EventArgs e)
        {
            if (TaskStateChanged != null)
                TaskStateChanged(this, new AsyncTaskEventArgs((AsyncTask) sender));
        }

        public void Remove(AsyncTask task)
        {
            lock (SyncRoot)
            {
                tasks.Remove(task);
                task.StateChanged -= task_StateChanged;
            }
            if (TaskRemoved != null)
                TaskRemoved(this, new AsyncTaskEventArgs(task));
        }

        public IEnumerator<AsyncTask> GetEnumerator()
        {
            lock (SyncRoot)
            {
                return tasks.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void OnTaskStateChanged(AsyncTaskEventArgs e)
        {
            switch (e.Task.State)
            {
                case AsyncTaskState.Running:
                    Interlocked.Increment(ref numberOfTasksRunning);
                    break;
                case AsyncTaskState.Cancelled:
                case AsyncTaskState.Completed:
                    Interlocked.Decrement(ref numberOfTasksRunning);
                    break;
            }
            newTaskEvent.Set();
            if (TaskStateChanged != null)
                TaskStateChanged(this, e);

        }

        public int MaxNumberOfTasksRunning
        {
            get { return maxNumberOfTasksRunning; }
            set
            {
                maxNumberOfTasksRunning = value;
                newTaskEvent.Set();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            running = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            running = true;
            Task.Run(() =>
            {
                while (running)
                {
                    newTaskEvent.WaitOne();
                    AsyncTask[] tasksNow;
                    lock (SyncRoot)
                        tasksNow = tasks.ToArray();
                    while (numberOfTasksRunning < Math.Min(MaxNumberOfTasksRunning, tasksNow.Length))
                    {
                        try
                        {
                            var task = tasksNow.FirstOrDefault(x => x.State == AsyncTaskState.Waiting);
                            if (task == null)
                                break;

                            if (!Common.IsEmptyOrNull(task.Parents) && !task.IsAllParentTasksFinished) continue;
                            var taskInfo = new TaskInfo(task, task.StartAsync());
                            lock (runningTasks)
                            {
                                runningTasks.Add(taskInfo);
                            }
                            OnNewRunningTask();
                        }
                        catch (Exception ex)
                        {
                            OnError(ex);
                        }
                    }
                }
            });
        }

        private void OnNewRunningTask()
        {
            if (checkingTaskStatesTask != null)
                return;

            checkingTaskStatesTask = Task.Run(() =>
            {
                while (running)
                {
                    TaskInfo[] taskInfos;
                    lock (runningTasks)
                    {
                        taskInfos = runningTasks.ToArray();
                    }
                    foreach (var x in taskInfos)
                    {
                        if (x.Task.IsFaulted || x.Task.IsCompleted || x.Task.IsCanceled)
                        {
                            lock (runningTasks)
                            {
                                runningTasks.Remove(x);
                            }
                        }
                        if (x.Task.Exception != null)
                            OnError(x.Task.Exception);
                    }
                }
            });
        }

        private void OnError(Exception exception)
        {
            if (Exception != null)
                Exception(this, new ExceptionEventArgs(exception));
        }

        public event EventHandler<AsyncTaskEventArgs> TaskStateChanged;
        public event EventHandler<AsyncTaskEventArgs> TaskAdded;
        public event EventHandler<AsyncTaskEventArgs> TaskRemoved;
        public event EventHandler<AsyncTaskEventArgs<AsyncTaskProgressEventArgs>> TaskProgress;
        public event EventHandler<ExceptionEventArgs> Exception;
    }
}
