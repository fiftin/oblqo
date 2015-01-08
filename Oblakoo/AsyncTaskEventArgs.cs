using System;

namespace Oblakoo
{
    public class AsyncTaskEventArgs<T> : EventArgs
    {
        public AsyncTask Task { get; private set; }
        public T Args { get; private set; }
        public AsyncTaskEventArgs(AsyncTask task, T args)
        {
            Task = task;
            Args = args;
        }
    }

    public class AsyncTaskEventArgs : AsyncTaskEventArgs<object>
    {
        public AsyncTaskEventArgs(AsyncTask task) : base(task, null)
        {
        }
    }
}
