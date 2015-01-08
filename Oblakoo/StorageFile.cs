using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblakoo
{
    public abstract class StorageFile
    {
        public abstract string Id { get; }
        public abstract string Name { get; }
        public abstract bool IsFolder { get; }
    }
}
