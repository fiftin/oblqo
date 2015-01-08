using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Oblakoo
{
    public class Account
    {
        public Storage Storage { get; private set; }
        public Drive Drive { get; private set; }

        public Account(Storage storage, Drive drive)
        {
            Storage = storage;
            Drive = drive;
            RootFolder = new AccountFile(this.Storage.RootFolder, this.Drive.RootFolder);
        }

        public async Task<ICollection<AccountFile>> GetSubfoldersAsync(CancellationToken token)
        {
            var driveFiles = await Drive.GetSubfoldersAsync(Drive.RootFolder, token);
            return driveFiles.Select(file => new AccountFile(Storage.GetFile(file), file)).ToList();
        }

        public async Task<ICollection<AccountFile>> GetSubfoldersAsync(AccountFile folder, CancellationToken token)
        {
            var driveFiles = await Drive.GetSubfoldersAsync(folder.DriveFile, token);
            return driveFiles.Select(file => new AccountFile(Storage.GetFile(file), file)).ToList();
        }

        public async Task<ICollection<AccountFile>> GetFilesAsync(CancellationToken token)
        {
            var driveFiles = await Drive.GetFilesAsync(Drive.RootFolder, token);
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
        public async Task DownloadFileFromStorageAsync(AccountFile file, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token)
        {
            await Storage.DownloadFileAsync(file.StorageFile, destFolder, actionIfFileExists, token);
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
        public async Task UploadFileAsync(string pathName, AccountFile destFolder, CancellationToken token)
        {
            var uploadedFile = await Storage.UploadFileAsync(pathName, destFolder.StorageFile, token);
            await Drive.UploadFileAsync(pathName, destFolder.DriveFile, uploadedFile.Id, token);
        }


        internal void Disconnect()
        {
            throw new NotImplementedException();
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



        public AccountFile RootFolder { get; private set; }
    }
}