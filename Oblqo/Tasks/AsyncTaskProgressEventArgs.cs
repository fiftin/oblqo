using System;

namespace Oblqo
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
