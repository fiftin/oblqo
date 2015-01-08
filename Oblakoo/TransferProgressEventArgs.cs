using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblakoo
{
    public class TransferProgressEventArgs : EventArgs
    {
        public int PercentDone { get; private set; }
        public TransferDirection Direction { get; set; }

        public TransferProgressEventArgs(int percentDone, TransferDirection direction)
        {
            PercentDone = percentDone;
            Direction = direction;
        }
    }
}
