using System;

namespace Oblakoo
{
    public class TransferProgress
    {
        public int PercentDone { get; private set; }

        public TransferProgress(int percentDone)
        {
            PercentDone = percentDone;
        }
    }
}
