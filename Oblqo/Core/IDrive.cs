using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo
{
    public interface IDrive<T> where T : IDriveFile
    {
        Size ImageMaxSize { get; set; }
        Image ScaleImage(Image image);
        Task<Stream> ScaleImageAsync(Image image, ImageFormat type, Stream defaultStream);
        Task<Stream> ScaleImageAsync(Stream input, ImageFormat type);
        Task<Image> GetImageAsync(T file, CancellationToken token);
        Task DeleteFileAsync(T driveFile, CancellationToken token);
        Task EnumerateFilesRecursive(T driveFolder, Action<T> action, CancellationToken token);
        Task DownloadFileAsync(T driveFile, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token);
        Task<T> UploadFileAsync(string pathName, T destFolder, string storageFileId, CancellationToken token);
        Task<Stream> ReadFileAsync(T file, CancellationToken token);
        Task<Image> GetThumbnailAsync(T file, CancellationToken token);
        Task<ICollection<T>> GetFilesAsync(T folder, CancellationToken token);
        Task<T> CreateFolderAsync(string folderName, T destFolder, CancellationToken token);
        Task<ICollection<T>> GetSubfoldersAsync(T folder, CancellationToken token);
        Task ClearAsync(CancellationToken token);
        Task DeleteFolderAsync(T driveFolder, CancellationToken token);
        /// <summary>
        /// Get file by information from <paramref name="xml"/>. Used for resuming tasks.
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="token">CancellationToken class instance for cancellation async operation.</param>
        /// <returns>Drive file instance.</returns>
        Task<T> GetFileAsync(System.Xml.Linq.XElement xml, CancellationToken token);
    }
}