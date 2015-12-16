using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public abstract class AsyncTask
    {
        public const int HighestPriority = int.MaxValue;
        public const int HighPriority = int.MaxValue / 2;
        public const int NormalPriority = 0;
        public const int LowPriority = int.MinValue / 2;
        public const int LowestPriority = int.MinValue;

        private List<AsyncTask> parents = new List<AsyncTask>();
        private AsyncTask[] parentsCache = new AsyncTask[0];
        public readonly EventWaitHandle CompleteWaitHandle = new ManualResetEvent(false);

        public void AddParent(AsyncTask parent)
        {
            lock (parents)
            {
                parents.Add(parent);
                parentsCache = parents.ToArray();
            }
        }

        public AsyncTask[] Parents
        {
            get
            {
                return parentsCache;
            }
            private set
            {
                lock (parents)
                {
                    parents.Clear();
                    if (value != null)
                    {
                        parents.AddRange(value);
                    }
                    parentsCache = value;
                }
            }
        }

        public string ID { get; private set; }
        public Account Account { get; private set; }
        public string AccountName { get; private set; }
        public Object Tag { get; set; }
        public AsyncTaskParentsMode ParentsMode { get; private set; }
        public int Priority { get; private set; }
        public Exception Exception { get; protected set; }
        public AsyncTaskState State { get; protected set; }
        public event EventHandler StateChanged;
        public event EventHandler<AsyncTaskProgressEventArgs> Progress;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        
        internal AsyncTaskManager Manager { get; set; }

        protected void AddAllTasks(IEnumerable<AsyncTask> tasks)
        {
            foreach (var task in tasks)
                AddTask(task);
        }

        protected void AddTask(AsyncTask task)
        {
            Manager.Add(task);
        }

        protected AsyncTask()
        {
        }

        protected AsyncTask(Account account, string accountName, int priority, 
            AsyncTask[] parents, 
            AsyncTaskParentsMode parentsMode = AsyncTaskParentsMode.CancelIfAnyErrorOrCanceled)
        {
            Account = account;
            AccountName = accountName;
            State = AsyncTaskState.Waiting;
            Priority = priority;
            Parents = parents;
            ParentsMode = parentsMode;
            ID = Guid.NewGuid().ToString();
        }

        public virtual async Task LoadAsync(Account account, string id, XElement xml, CancellationToken token)
        {
            Account = account;
            AccountName = xml.Attribute("accountName").Value;
            State = AsyncTaskState.Waiting;
            Priority = int.Parse(xml.Attribute("priority").Value);
            ParentsMode = (AsyncTaskParentsMode)Enum.Parse(typeof(AsyncTaskParentsMode), xml.Attribute("parentsMode").Value);
            ID = id;
        }

        public virtual XElement ToXml()
        {
            var xml = new XElement("task");
            xml.SetAttributeValue("type", GetType().FullName);
            xml.SetAttributeValue("accountName", AccountName);
            xml.SetAttributeValue("priority", Priority);
            xml.SetAttributeValue("parentsMode", ParentsMode);
            return xml;
        }

        public static async Task<AsyncTask> LoadFromXmlAsync(Account account, string id, XElement xml, CancellationToken token)
        {
            var type = Type.GetType(xml.Attribute("type").Value);
            var ctor = type.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
            {
                throw new Exception("Task has no empty constructor: " + type.Name);
            }
            var task = (AsyncTask)ctor.Invoke(new object[0]);
            await task.LoadAsync(account, id, xml, token);
            return task;
        }

        protected virtual void OnProgress(AsyncTaskProgressEventArgs e)
        {
            if (Progress != null)
                Progress(this, e);
        }

        public async Task StartAsync()
        {
            State = AsyncTaskState.Running;
            OnStateChanged();
            try
            {
                await OnStartAsync();
                if (State == AsyncTaskState.Running)
                    State = AsyncTaskState.Completed;
                OnStateChanged();
            }
            catch (Exception ex)
            {
                State = AsyncTaskState.Error;
                Exception = ex;
                try
                {
                    OnStateChanged();
                }
                catch { }
            }
        }

        protected abstract Task OnStartAsync();

        public void Cancel()
        {
            State = AsyncTaskState.Cancelled;
            cancellationTokenSource.Cancel();
            OnStateChanged();
        }

        protected virtual void OnStateChanged()
        {
            switch (State)
            {
                case AsyncTaskState.Cancelled:
                case AsyncTaskState.Completed:
                case AsyncTaskState.Error:
                    CompleteWaitHandle.Set();
                    break;
            }
            if (StateChanged != null) StateChanged(this, EventArgs.Empty);
        }

        protected CancellationTokenSource CancellationTokenSource
        {
            get { return cancellationTokenSource; }
        }

        public bool IsAllParentTasksCompletedSuccessful
        {
            get { return Parents.All(parent => parent.State == AsyncTaskState.Completed); }
        }

        public bool IsAllParentTasksFinished
        {
            get { return Parents.All(parent => parent.State == AsyncTaskState.Completed 
                || parent.State == AsyncTaskState.Cancelled || parent.State == AsyncTaskState.Error); }
        }

    }
}
