using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo.Controls
{
    public class SlideEventArgs
    {
        public SlideDirection Direction { get; }
        public SlideEventArgs(SlideDirection direction)
        {
            Direction = direction;
        }
    }
}
