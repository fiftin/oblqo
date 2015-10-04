
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo
{
    public class AccountFile
    {
        public DriveFileCollection DriveFile { get; set; }
        public StorageFile StorageFile { get; set; }
        public string Name
        {
            get
            {
                if (DriveFile != null)
                {
                    return DriveFile.Name;
                }
                else if (StorageFile != null)
                {
                    return StorageFile.Name;
                }
                return null;
            }
        }
        public string Id { get { return DriveFile.Id; } }
        public bool IsImage
        {
            get { return DriveFile != null && DriveFile.IsImage; }
        }
        public bool IsFolder
        {
            get { return (DriveFile != null && DriveFile.IsFolder) || (StorageFile != null && StorageFile.IsFolder); }
        }
        public bool HasChildren {
            get { return DriveFile != null && DriveFile.HasChildren; }
        }

        public AccountFile(StorageFile storageFile, DriveFileCollection driveFile)
        {
            StorageFile = storageFile;
            DriveFile = driveFile;
        }

    }
}
