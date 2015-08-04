using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo.Local
{
    public class LocalDrive : Drive
    {
        private DriveFile rootFolder;

        public LocalDrive(Storage storage, Account account)
            : base(storage, account)
        {
        }

        public override DriveFile RootFolder
        {
            get
            {
                return rootFolder;
            }
        }

        public override Task ClearAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task<DriveFile> CreateFolderAsync(string folderName, DriveFile destFolder, CancellationToken token)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var dir = Directory.CreateDirectory(((LocalFile)destFolder).file.FullName + Path.DirectorySeparatorChar + folderName);
            return new LocalFile(this, dir, false);
        }

        public override Task DeleteFileAsync(DriveFile driveFile, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteFolderAsync(DriveFile driveFolder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task DownloadFileAsync(DriveFile driveFile, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task EnumerateFilesRecursive(DriveFile driveFolder, Action<DriveFile> action, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<DriveFile> GetFileAsync(XElement xml, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<ICollection<DriveFile>> GetFilesAsync(DriveFile folder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile folder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<Image> GetThumbnailAsync(DriveFile file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<Stream> ReadFileAsync(DriveFile file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<DriveFile> UploadFileAsync(string pathName, DriveFile destFolder, string storageFileId, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
