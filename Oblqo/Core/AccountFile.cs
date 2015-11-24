using Oblqo.Core;
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

        public DriveFileCollection DriveFile => DriveFiles;


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

        public bool HasChildren => DriveFiles.Count > 0 && DriveFiles[0].HasChildren;

        public long Size => DriveFile.Size;
        public DateTime ModifiedDate => DriveFile.ModifiedDate;
        public DateTime CreatedDate => DriveFile.CreatedDate;

        public int OriginalImageWidth => DriveFile.OriginalImageWidth;
        public int OriginalImageHeight => DriveFile.OriginalImageHeight;
        public long OriginalSize => DriveFile.OriginalSize;

        public int ImageWidth => DriveFile.ImageWidth;
        public int ImageHeight => DriveFile.ImageHeight;
        public bool IsRoot => DriveFile.IsRoot;
        public string MimeType => DriveFile.MimeType;

        public AccountFile(StorageFile storageFile, IEnumerable<DriveFile> driveFiles, AccountFile parent)
        {
            DriveFiles.AddRange(driveFiles);
            StorageFile = storageFile;
            Parent = parent;
        }

        public AccountFile(StorageFile storageFile, AccountFile parent)
        {
            StorageFile = storageFile;
            Parent = parent;
        }

        public DriveFile GetFile(Drive drive)
        {
            return DriveFiles.FirstOrDefault(file => file.Drive == drive);
            //return DriveFiles.SingleOrDefault(file => file.Drive == drive);
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

        public XElement ToXml()
        {
            var root = new XElement("file");
            foreach (var file in DriveFiles)
            {
                root.Add(file.ToXml());
            }
            root.Add(StorageFile.ToXml());
            return root;
        }
    }
}
