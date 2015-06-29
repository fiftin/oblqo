using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oblakoo
{
    public abstract class Storage
    {
        public abstract Task DeleteFileAsync(StorageFile id, CancellationToken token);
        public abstract StorageFile GetFile(DriveFile driveFile);
        public abstract Task<StorageFile> UploadFileAsync(string pathName, StorageFile destFolder, CancellationToken token, Action<TransferProgress> progressCallback);
        public abstract Task DownloadFileAsync(StorageFile file, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token, Action<TransferProgress> progressCallback);
        public abstract Task<StorageFile> CreateFolderAsync(string folderName, StorageFile destFolder, CancellationToken token);
        public abstract StorageFile RootFolder { get; }
        public abstract bool IsSupportFolders { get; }
        public abstract Task ClearAsync(CancellationToken token);
    }
}
