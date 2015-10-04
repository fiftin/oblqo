
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

        public string Name => DriveFile != null ? DriveFile.Name : StorageFile?.Name;

        public bool IsImage => DriveFile != null && DriveFile.IsImage;

        public bool IsFolder => (DriveFile != null && DriveFile.IsFolder) || (StorageFile != null && StorageFile.IsFolder);

        public bool HasChildren => DriveFile != null && DriveFile.HasChildren;

        public AccountFile(StorageFile storageFile, DriveFileCollection driveFile)
        {
            StorageFile = storageFile;
            DriveFile = driveFile;
        }

    }
}
