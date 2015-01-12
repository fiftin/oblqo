using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Oblakoo;

namespace OblakooTest
{
    public class MockDrive : Drive
    {
        readonly MockDriveFile root = new MockDriveFile();

        public MockDrive(string rootPath)
        {
            root.children.Add(new MockDriveFile("test.txt", "test.txt", "Hello, World!"));
            root.children.Add(new MockDriveFile("file.txt", "file.txt", "File file file"));
            root.children.Add(new MockDriveFile("folder1", "folder1"));
        }


#pragma warning disable 1998
        public override DriveFile RootFolder
        {
            get { throw new NotImplementedException(); }
        }

        public override Task DeleteFileAsync(DriveFile driveFile, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task EnumerateFilesRecursive(DriveFile driveFolder, Action<DriveFile> action, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task DownloadFileAsync(DriveFile driveFile, string destFolder, ActionIfFileExists actionIfFileExists,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<DriveFile> UploadFileAsync(string pathName, DriveFile destFolder, string storageFileId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

#pragma warning disable 1998
        public override async Task<Stream> ReadFileAsync(DriveFile file, CancellationToken token)
#pragma warning restore 1998
        {
            return ((MockDriveFile) file).GetContentStream();
        }

        public override Task<Image> GetThumbnailAsync(DriveFile file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

#pragma warning disable 1998
        public override async Task<ICollection<DriveFile>> GetFilesAsync(DriveFile folder, CancellationToken token)
#pragma warning restore 1998
        {
            return ((MockDriveFile) folder).children;
        }

        public override Task<DriveFile> CreateFolderAsync(string folderName, DriveFile destFolder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile folder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task ClearAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteFolderAsync(DriveFile driveFolder, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
