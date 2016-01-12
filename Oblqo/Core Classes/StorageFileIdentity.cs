using Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo
{
    public class StorageFileIdentity
    {
        public string FileId { get; private set; }
        public string StorageId { get; private set; }
        public string StorageKind { get; private set; }

        public StorageFileIdentity(string storageKind, string storageId, string fileId)
        {
            StorageKind = storageKind;
            StorageId = storageId;
            FileId = fileId;
        }

    }
}
