using Oblqo.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo
{
    public abstract class Storage
    {
        /// <summary>
        /// Storage kind unique identifier.
        /// For example Glacier has kind "gl".
        /// </summary>
        public abstract string Kind { get; }
        public abstract string Id { get; }
        public abstract Task DeleteFileAsync(StorageFile id, CancellationToken token);
        public abstract StorageFile GetFile(DriveFileCollection driveFile);
        public abstract Task<StorageFile> UploadFileAsync(Stream stream, string fileName, StorageFile destFolder, CancellationToken token, Action<TransferProgress> progressCallback);
        public abstract Task<StorageFile> UploadFileAsync(string pathName, StorageFile destFolder, CancellationToken token, Action<TransferProgress> progressCallback);
        public abstract Task DownloadFileAsync(StorageFile file, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token, Action<TransferProgress> progressCallback);
        public abstract Task DownloadFileAsync(StorageFile file, Stream stream, CancellationToken token, Action<TransferProgress> progressCallback);
        public abstract Task<StorageFile> CreateFolderAsync(string folderName, StorageFile destFolder, CancellationToken token);
        public abstract StorageFile RootFolder { get; }
        public abstract bool IsSupportFolders { get; }
        public abstract Task ClearAsync(CancellationToken token);
        public abstract Task<StorageFile> GetFileAsync(System.Xml.Linq.XElement xml, CancellationToken token);
    }
}
