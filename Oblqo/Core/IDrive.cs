using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo
{
    public interface IDrive
    {
        DriveFile RootFolder { get; }
        Size ImageMaxSize { get; set; }
        Storage Storage { get; }
        Account Account { get; }
        Image ScaleImage(Image image);
        //bool TryGetImageType(string pathName, out ImageFormat type);
        Task<Stream> ScaleImageAsync(Image image, ImageFormat type, Stream defaultStream);
        Task<Stream> ScaleImageAsync(Stream input, ImageFormat type);
        Task<Image> GetImageAsync(DriveFile file, CancellationToken token);
        Task DeleteFileAsync(DriveFile driveFile, CancellationToken token);
        Task EnumerateFilesRecursive(DriveFile driveFolder, Action<DriveFile> action, CancellationToken token);
        Task DownloadFileAsync(DriveFile driveFile, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token);
        Task<DriveFile> UploadFileAsync(string pathName, DriveFile destFolder, string storageFileId, CancellationToken token);
        Task<Stream> ReadFileAsync(DriveFile file, CancellationToken token);
        Task<Image> GetThumbnailAsync(DriveFile file, CancellationToken token);
        Task<ICollection<DriveFile>> GetFilesAsync(DriveFile folder, CancellationToken token);
        Task<DriveFile> CreateFolderAsync(string folderName, DriveFile destFolder, CancellationToken token);
        Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile folder, CancellationToken token);
        Task ClearAsync(CancellationToken token);
        Task DeleteFolderAsync(DriveFile driveFolder, CancellationToken token);

        /// <summary>
        /// Get file by information from <paramref name="xml"/>. Used for resuming tasks.
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="token">CancellationToken class instance for cancellation async operation.</param>
        /// <returns>Drive file instance.</returns>
        Task<DriveFile> GetFileAsync(System.Xml.Linq.XElement xml, CancellationToken token);
    }
}