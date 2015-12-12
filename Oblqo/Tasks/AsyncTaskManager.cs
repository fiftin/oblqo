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
// ReSharper disable CanBeReplacedWithTryCastAndCheckForNull

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

        public readonly object SyncRoot = new object();

        public int MaxNumberOfTasksRunning { get; set; } = 5;

        public async Task RestoreAsync(Account account, string accountName, CancellationToken token)
        {
            var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            if (!store.DirectoryExists("accounts/" + accountName + "/tasks"))
            {
                return;
            }
            var fileNames = store.GetFileNames("accounts/" + accountName + "/tasks/*");
            foreach (var filename in fileNames)
            {
                try
                {
                    using (var stream = store.OpenFile("accounts/" + accountName + "/tasks/" + filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        var xml = XDocument.Load(stream).Root;
                        var type = Type.GetType(xml.Attribute("type").Value);
                        var ctor = type.GetConstructor(Type.EmptyTypes);
                        if (ctor == null)
                        {
                            throw new Exception("Task has no empty constructor: " + type.Name);
                        }
                        var task = (AsyncTask)ctor.Invoke(new object[0]);
                        await task.LoadAsync(account, filename, xml, token);
                        Add(task, false);
                    }
                }
                catch (Exception ex)
                {
                    store.DeleteFile("accounts/" + accountName + "/tasks/" + filename);
                    OnError(ex);
                }
            }
        }

        public void Save()
        {
            foreach (var task in tasks)
            {
                Save(task);
            }
        }

        public void Save(AsyncTask task)
        {
            var accountName = task.AccountName;
            var tasksPath = "accounts/" + accountName + "/tasks/";
            switch (task.State)
            {
                case AsyncTaskState.Cancelled:
                case AsyncTaskState.Completed:
                case AsyncTaskState.Error:
                    using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
                    {
                        if (store.FileExists(tasksPath + task.ID))
                        {
                            store.DeleteFile(tasksPath + task.ID);
                        }
                    }
                    return;
            }
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                if (!store.DirectoryExists(tasksPath))
                {
                    store.CreateDirectory(tasksPath);
                }
                var doc = new XDocument();
                doc.Add(task.ToXml());
                using (var stream = store.CreateFile(tasksPath + task.ID))
                {
                    doc.Save(stream);
                }
            }
        }

        public void Add(AsyncTask task, bool save = true)
        {
            lock (SyncRoot)
            {
                tasks.Add(task);
                task.StateChanged += task_StateChanged;
                task.Progress += task_Progress;
                task.Manager = this;
                if (save)
                {
                    Save(task);
                }
            } 
            if (TaskAdded != null)
            {
                TaskAdded(this, new AsyncTaskEventArgs(task));
            }
            // TODO: update queue
            Update();
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
                    //using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
                    //{
                    //    var tasksPath = "accounts/" + task.AccountName + "/tasks/";
                    //    store.DeleteFile(tasksPath + task.ID);
                    //}
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
                        var taskInfo = new TaskInfo(task, task.StartAsync());
                        i--;
                    }
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        public void Start()
        {
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
