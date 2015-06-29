using System;

namespace Oblakoo
{
    public class AsyncTaskProgressEventArgs : EventArgs
    {
        public object State { get; private set; }
        public int PercentDone { get; private set; }

        public AsyncTaskProgressEventArgs(int percentDone, object state)
        {
            PercentDone = percentDone;
            State = state;
        }
    }
}
