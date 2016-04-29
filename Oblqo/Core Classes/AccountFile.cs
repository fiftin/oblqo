using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public class AccountFile
    {
        public DriveFileCollection DriveFiles { get; } = new DriveFileCollection();

        public StorageFile StorageFile { get; }

        public AccountFile Parent { get; }

        public IEnumerable<Drive> Drives => DriveFiles.Select(x => x.Drive);

        public Storage Storage => StorageFile.Storage;

        public Drive Drive => Drives.First();
        
        public bool HasValidStorageFileId
        {
            get
            {
                return Storage.IsValidStorageFileId(StorageFileId);
            }
        }

        public string StorageFileId
        {
            get
            {
                return StorageFile != null
                    ? StorageFile.Id
                    : DriveFiles.FirstOrDefault(x => x.StorageFileId != null)?.StorageFileId;
            }
        }

        public string Name => DriveFiles.Count > 0 ? DriveFiles[0].Name : StorageFile?.Name;

        public bool IsImage => DriveFiles.Count > 0 && DriveFiles[0].IsImage;

        public bool IsFolder => (DriveFiles.Count > 0 && DriveFiles[0].IsFolder) || (StorageFile != null && StorageFile.IsFolder);

        public bool IsFile => !IsFolder;

        public bool HasParent => Parent != null;

        public bool HasChildren => DriveFiles.Count > 0 && DriveFiles[0].HasChildren;

        public long Size => DriveFiles.Size;
        public DateTime ModifiedDate => DriveFiles.ModifiedDate;
        public DateTime CreatedDate => DriveFiles.CreatedDate;

        public int OriginalImageWidth => DriveFiles.OriginalImageWidth;
        public int OriginalImageHeight => DriveFiles.OriginalImageHeight;
        public long OriginalSize => DriveFiles.OriginalSize;

        public int ImageWidth => DriveFiles.ImageWidth;
        public int ImageHeight => DriveFiles.ImageHeight;
        public bool IsRoot => DriveFiles.IsRoot;
        public string MimeType => DriveFiles.MimeType;
        public Account Account { get; }

        public AccountFile(Account account, StorageFile storageFile, IEnumerable<DriveFile> driveFiles, AccountFile parent)
        {
            DriveFiles.AddRange(driveFiles);
            this.StorageFile = storageFile;
            Parent = parent;
            Account = account;
        }

        public AccountFile(Account account, StorageFile storageFile, AccountFile parent)
        {
            this.StorageFile = storageFile;
            Parent = parent;
            Account = account;
        }

        public DriveFile GetDriveFile(Drive drive)
        {
            return GetDriveFile(drive.Id);
            // return DriveFiles.FirstOrDefault(file => file.Drive == drive);
        }

        public DriveFile GetDriveFile(string driveId)
        {
            foreach (var file in DriveFiles)
            {
                if (file.Drive.Id == driveId)
                {
                    return file;
                }
            }
            return null;
        }

        public async Task<DriveFile> GetFileAndCreateIfFolderIsNotExistsAsync(Drive drive, CancellationToken token)
        {
            var stack = new Stack<AccountFile>();
            var curr = this;
            var driveFile = curr.DriveFiles.SingleOrDefault(file => file.Drive == drive);
            while (driveFile == null)
            {
                stack.Push(curr);
                if (curr.Parent == null)
                {
                    break;
                }
                curr = curr.Parent;
                driveFile = curr.DriveFiles.SingleOrDefault(file => file.Drive == drive);
            }
            while (stack.Count > 0)
            {
                var f = stack.Pop();
                var dirs = await drive.GetSubfoldersAsync(driveFile, token);
                var dir = dirs.FirstOrDefault(x => x.Name == f.Name);
                if (dir == null)
                {
                    driveFile = await drive.CreateFolderAsync(f.Name, driveFile, token);
                }
                else
                {
                    driveFile = dir;
                }
                f.DriveFiles.Add(driveFile);
            }
            return driveFile;
        }

        public XElement ToXml(string tagName)
        {
            var root = new XElement(tagName);
            var driveFiles = new XElement("driveFiles");
            foreach (var file in DriveFiles)
            {
                driveFiles.Add(file.ToXml());
            }
            root.Add(driveFiles);
            if (StorageFile != null)
            {
                root.Add(StorageFile.ToXml());
            }
            if (HasParent)
            {
                var parent = Parent.ToXml("parent");
                root.Add(parent);
            }
            return root;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
