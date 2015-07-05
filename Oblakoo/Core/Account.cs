using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblakoo
{
    public class Account
    {
        public Storage Storage { get; private set; }
        public Drive Drive { get; private set; }
        public AccountFile RootFolder { get; private set; }

        public Account(Storage storage, Drive drive)
        {
            Storage = storage;
            Drive = drive;
            RootFolder = new AccountFile(this.Storage.RootFolder, this.Drive.RootFolder);
        }

        public async Task<ICollection<AccountFile>> GetSubfoldersAsync(AccountFile folder, CancellationToken token)
        {
            var driveFiles = await Drive.GetSubfoldersAsync(folder.DriveFile, token);
            return driveFiles.Select(file => new AccountFile(Storage.GetFile(file), file)).ToList();
        }

        public async Task<Stream> ReadFileAsync(AccountFile file, CancellationToken token)
        {
            return await Drive.ReadFileAsync(file.DriveFile, token);
        }

        public async Task<Image> GetThumbnailAsync(AccountFile file, CancellationToken token)
        {
            return await Drive.GetThumbnailAsync(file.DriveFile, token);
        }

        public async Task<ICollection<AccountFile>> GetFilesAsync(AccountFile folder, CancellationToken token)
        {
            var driveFiles = await Drive.GetFilesAsync(folder == null ? null : folder.DriveFile, token);
            return driveFiles.Select(file => new AccountFile(Storage.GetFile(file), file)).ToList();
        }

        /// <summary>
        /// Download folder or file from storage (not from drive).
        /// </summary>
        /// <param name="file"></param>
        /// <param name="destFolder">Path of folder whare to nest a downloaded file or folder.
        /// For example if <paramref name="file"/> is "/photo2010/egypt" and <paramref name="destFolder"/> 
        /// is "d:/photos", then in "photos" folder will be created subfolder "egypt" with downloaded files.
        /// </param>
        /// <param name="actionIfFileExists"></param>
        /// <param name="token"></param>
        /// <param name="progressCallback"></param>
        public async Task DownloadFileFromStorageAsync(AccountFile file, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            await Storage.DownloadFileAsync(file.StorageFile, destFolder, actionIfFileExists, token, progressCallback);
        }

        public async Task DownloadFileFromDriveAsync(AccountFile file, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token)
        {
            await Drive.DownloadFileAsync(file.DriveFile, destFolder, actionIfFileExists, token);
        }

        /// <summary>
        /// Upload folder of file to storage and drive.
        /// </summary>
        /// <param name="pathName"></param>
        /// <param name="destFolder"></param>
        /// <param name="token"></param>
        /// <param name="progressCallback"></param>
        public async Task UploadFileAsync(string pathName, AccountFile destFolder, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            var uploadedFile = await Storage.UploadFileAsync(pathName, destFolder.StorageFile, token, progressCallback);
            await Drive.UploadFileAsync(pathName, destFolder.DriveFile, uploadedFile.Id, token);
        }

        internal void Disconnect()
        {
        }

        public virtual async Task<Image> GetImageAsync(AccountFile file, CancellationToken token)
        {
            return await Drive.GetImageAsync(file.DriveFile, token);
        }

        public async Task<AccountFile> CreateFolderAsync(string folderName, AccountFile destFolder,
            CancellationToken token)
        {
            var storageDir =
                await
                    Storage.CreateFolderAsync(folderName, destFolder.StorageFile,
                        token);
            var driveDir =
                await Drive.CreateFolderAsync(folderName, destFolder.DriveFile, token);
            return new AccountFile(storageDir, driveDir);
        }

        public async Task ClearAsync(CancellationToken token)
        {
            await Storage.ClearAsync(token);
            await Drive.ClearAsync(token);
        }

        public async Task DeleteFolderAsync(AccountFile folder, CancellationToken token)
        {
            await Drive.DeleteFolderAsync(folder.DriveFile, token);
        }

        public async Task<AccountFile> GetFileAsync(XElement storageXml, XElement driveXml, CancellationToken token)
        {
            var storageFile = storageXml == null ? null : await Storage.GetFileAsync(storageXml, token);
            var driveFile = driveXml == null ? null : await Drive.GetFileAsync(driveXml, token);
            return new AccountFile(storageFile, driveFile);
        }
    }
}