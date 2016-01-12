using System;

namespace Oblqo
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
