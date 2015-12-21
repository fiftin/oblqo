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
    public class MockDrive : Drive
    {
        internal MockDriveFile root;
        internal MockDriveFile rootFolder;

        public MockDrive(Account owner) : base(owner, "mock")
        {
        }

        public override DriveFile RootFolder => rootFolder;

        public override string ShortName => "Mock";

        public override async Task<DriveFile> CreateFolderAsync(string folderName, DriveFile destFolder, 
            CancellationToken token)
        {
            return ((MockDriveFile)destFolder).CreateFolder(folderName);
        }

        public override async Task DeleteFileAsync(DriveFile driveFile, CancellationToken token)
        {
            rootFolder.DeleteFileRecursive(driveFile);
        }

        public override async Task DeleteFolderAsync(DriveFile driveFolder, CancellationToken token)
        {
            rootFolder.DeleteFileRecursive(driveFolder);
        }

        public override async Task DownloadFileAsync(DriveFile driveFile, Stream output, CancellationToken token)
        {
            await output.WriteAsync(((MockDriveFile)driveFile).content, 0, ((MockDriveFile)driveFile).content.Length);
        }

        public override async Task DownloadFileAsync(DriveFile driveFile, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override async Task EnumerateFilesRecursive(DriveFile driveFolder, Action<DriveFile> action, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override async Task<DriveFile> GetFileAsync(System.Xml.Linq.XElement xml, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override async Task<ICollection<DriveFile>> GetFilesAsync(DriveFile folder, CancellationToken token)
        {
            return ((MockDriveFile)folder).files.Where(x => !x.IsFolder).Cast<DriveFile>().ToList();
        }

        public override async Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile folder, CancellationToken token)
        {
            return ((MockDriveFile)folder).files.Where(x => x.IsFolder).Cast<DriveFile>().ToList();
        }

        public override Task<System.Drawing.Image> GetThumbnailAsync(DriveFile file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override async Task<Stream> ReadFileAsync(DriveFile file, CancellationToken token)
        {
            return new MemoryStream(((MockDriveFile)file).content);
        }

        public override Task<DriveFile> UploadFileAsync(string pathName, DriveFile destFolder, bool scaleRequired, string storageFileId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override async Task<DriveFile> UploadFileAsync(Stream fileStream, string fileName, DriveFile destFolder, bool scaleRequired, string storageFileId, CancellationToken token)
        {
            var file = new MockDriveFile(this, fileName, false);
            file.content = new byte[fileStream.Length];
            fileStream.Read(file.content, 0, (int)fileStream.Length);
            return file;
        }
    }
}
