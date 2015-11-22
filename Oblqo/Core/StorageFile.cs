using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public abstract class StorageFile
    {
        public abstract string Id { get; }
        public abstract string Name { get; }
        public abstract bool IsFolder { get; }
        public abstract bool IsRoot { get; }

        public Storage Storage { get; private set; }

        protected StorageFile(Storage storage)
        {
            Storage = storage;
        }

        public virtual XElement ToXml()
        {
            var ret = new XElement(IsFolder ? "storageFolder" : "storageFile");
            return ret;
        }
    }
}
