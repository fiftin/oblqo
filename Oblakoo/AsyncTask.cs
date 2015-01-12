using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oblakoo
{
    public abstract class AsyncTask
    {
        public const int HighestPriority = int.MaxValue;
        public const int HighPriority = int.MaxValue / 2;
        public const int NormalPriority = 0;
        public const int LowPriority = int.MinValue / 2;
        public const int LowestPriority = int.MinValue;

        public Account Account { get; private set; }
        public string AccountName { get; set; }
        public Object Tag { get; set; }

        internal AsyncTaskManager Manager { get; set; }

        protected void AddATask(AsyncTask task)
        {
            Manager.Add(task);
        }

        protected AsyncTask(Account account, string accountName, int priority, AsyncTask parent)
        {
            Account = account;
            AccountName = accountName;
            State = AsyncTaskState.Waiting;
            Priority = priority;
            Parent = parent;
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
                await StartAsync2();
                if (State == AsyncTaskState.Running)
                    State = AsyncTaskState.Finished;
                OnStateChanged();
            }
            catch (Exception ex)
            {
                State = AsyncTaskState.Error;
                Exception = ex;
                OnStateChanged();
                throw;
            }
        }

        protected abstract Task StartAsync2();

        public void Cancel()
        {
            State = AsyncTaskState.Cancelled;
            cancellationTokenSource.Cancel();
        }

        protected virtual void OnStateChanged()
        {
            if (StateChanged != null) StateChanged(this, EventArgs.Empty);
        }

        protected CancellationTokenSource CancellationTokenSource
        {
            get { return cancellationTokenSource; }
        }

        public int Priority { get; private set; }
        public AsyncTask Parent { get; private set; }
        public Exception Exception { get; protected set; }
        public AsyncTaskState State { get; protected set; }
        public event EventHandler StateChanged;
        public event EventHandler<AsyncTaskProgressEventArgs> Progress;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    }
}
