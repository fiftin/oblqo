using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Oblqo;

namespace OblqoTest
{
    class MockDrive : Drive
    {


        public MockDrive(Account owner) : base(owner)
        {
        }

        public override DriveFile RootFolder
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Task<DriveFile> CreateFolderAsync(string folderName, DriveFile destFolder, CancellationToken token)
        {
            throw new NotImplementedException();
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

        public override Task<DriveFile> GetFileAsync(System.Xml.Linq.XElement xml, CancellationToken token)
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

        public override Task<System.Drawing.Image> GetThumbnailAsync(DriveFile file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<Stream> ReadFileAsync(DriveFile file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<DriveFile> UploadFileAsync(string pathName, DriveFile destFolder, bool scaleRequired, string storageFileId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<DriveFile> UploadFileAsync(Stream fileStream, string fileName, DriveFile destFolder, bool scaleRequired, string storageFileId, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
