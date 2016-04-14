using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Oblqo.Tasks;
using System.IO.IsolatedStorage;
using System.Xml.Linq;

namespace Oblqo
{
    public class AsyncTaskManager : IEnumerable<AsyncTask>
    {
        private class TaskInfo
        {
            public DateTime StartTime { get; set; }
            public AsyncTask AsyncTask { get; set; }
            public Task Task { get; }
            public TaskInfo(AsyncTask asyncTask, Task task)
            {
                StartTime = DateTime.Now;
                AsyncTask = asyncTask;
                Task = task;
            }
        }

        private readonly List<AsyncTask> tasks = new List<AsyncTask>();
        private readonly ConfigurationStorage config;

        public readonly object SyncRoot = new object();

        public int MaxNumberOfTasksRunning { get; set; } = 10;

        public AsyncTaskManager(ConfigurationStorage config)
        {
            this.config = config;
            this.config.Error += Config_Error; ;
        }

        private void Config_Error(object sender, ExceptionEventArgs e)
        {
            OnError(e.Exception);
        }

        public async Task RestoreAsync(Account account, string accountName, CancellationToken token)
        {
            var tasks = await config.GetTasksAsync(account, accountName, token);
            foreach (var task in tasks.Where((x) => x.Account == account))
            {
                task.Pause();
            }
            AddRange(tasks);
        }

        public int CountTasksOf(Account account)
        {
            return tasks.Count((x) => x.Account == account);
        }

        public void ResumeAllTasksOf(Account account)
        {
            foreach (var task in tasks.Where((x) => x.Account == account))
            {
                task.Resume();
            }
        }

        public void CancelAllTasksOf(Account account)
        {
            foreach (var task in tasks.Where((x) => x.Account == account))
            {
                task.Cancel();
            }
        }

        public void SaveAll()
        {
            foreach (var task in tasks)
            {
                Save(task);
            }
        }

        public void Save(AsyncTask task)
        {
            config.SaveTask(task);
        }

        public void AddRange(IEnumerable<AsyncTask> newTasks)
        {
            foreach (var x in newTasks)
            {
                Add(x);
            }
        }

        public void Add(AsyncTask task)
        {
            lock (SyncRoot)
            {
                tasks.Add(task);
                task.StateChanged += task_StateChanged;
                task.Progress += task_Progress;
                task.Manager = this;
            }
            if (TaskAdded != null)
            {
                TaskAdded(this, new AsyncTaskEventArgs(task));
            }
            Update();
        }

        public void AddAndSave(AsyncTask task)
        {
            Add(task);
            Save(task);
        }

        public int Count
        {
            get
            {
                return tasks.Count;
            }
        }

        void task_Progress(object sender, AsyncTaskProgressEventArgs e)
        {
            if (TaskProgress != null)
                TaskProgress(this, new AsyncTaskEventArgs<AsyncTaskProgressEventArgs>((AsyncTask)sender, e));
        }

        private void task_StateChanged(object sender, EventArgs e)
        {
            var task = ((AsyncTask)sender);
            if (TaskStateChanged != null)
            {
                TaskStateChanged(this, new AsyncTaskEventArgs(task));
            }
            switch (task.State) // delete finished tasks from tracking
            {
                case AsyncTaskState.Cancelled:
                case AsyncTaskState.Completed:
                case AsyncTaskState.Error:
                    config.DeleteTask(task);
                    task.StateChanged -= task_StateChanged;
                    task.Progress -= task_Progress;
                    break;
            }
            Update();
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
                    break;
                case AsyncTaskState.Cancelled:
                case AsyncTaskState.Completed:
                    break;
            }
            if (TaskStateChanged != null)
                TaskStateChanged(this, e);

        }

        protected void Update()
        {
            List<AsyncTask> tasksNow;
            lock (SyncRoot)
            {
                tasksNow = new List<AsyncTask>(tasks);
            }
            try
            {
                var waitingTasks = tasksNow.FindAll(x => x.State == AsyncTaskState.Waiting);
                var runningTasks = tasksNow.FindAll(x => x.State == AsyncTaskState.Running);
                var i = MaxNumberOfTasksRunning - runningTasks.Count;
                if (i > 0 && waitingTasks.Count > 0)
                {
                    foreach (var task in waitingTasks)
                    {
                        if (i == 0)
                        {
                            break;
                        }
                        if (!Common.IsEmptyOrNull(task.Parents) && !task.IsAllParentTasksFinished)
                        {
                            continue;
                        }
                        var tsk = task.StartAsync();
                        var taskInfo = new TaskInfo(task, tsk);
                        i--;
                    }
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }
        
        private void OnError(Exception exception)
        {
            Exception?.Invoke(this, new ExceptionEventArgs(exception));
        }

        public event EventHandler<AsyncTaskEventArgs> TaskStateChanged;
        public event EventHandler<AsyncTaskEventArgs> TaskAdded;
        public event EventHandler<AsyncTaskEventArgs> TaskRemoved;
        public event EventHandler<AsyncTaskEventArgs<AsyncTaskProgressEventArgs>> TaskProgress;
        public event EventHandler<ExceptionEventArgs> Exception;
    }
}
