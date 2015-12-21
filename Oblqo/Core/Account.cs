using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public class Account
    {
        public Storage Storage { get; }

        public DriveCollection Drives { get; } = new DriveCollection();

        private AccountFile rootFolder;

        public AccountFile RootFolder
            => rootFolder ?? (rootFolder = new AccountFile(this, Storage.RootFolder, Drives.Select(x => x.RootFolder), null));

        public object Tag { get; set; }
 
        public Account(Storage storage)
        {
            this.Storage = storage;
        }

        public async Task<ICollection<AccountFile>> GetSubfoldersAsync(AccountFile folder, CancellationToken token)
        {
            IDictionary<string, DriveFileCollection> driveFiles = new Dictionary<string, DriveFileCollection>();
            foreach (var f in folder.DriveFiles)
            {
                PutFilesTo(await f.Drive.GetSubfoldersAsync(f, token), driveFiles);
            }
            return driveFiles.Values.Select(files => new AccountFile(this, Storage.GetFile(files), files, folder)).ToList();
        }

        public async Task<Stream> ReadFileAsync(AccountFile file, CancellationToken token)
        {
            foreach (var drive in Drives)
            {
                try
                {
                    return await drive.ReadFileAsync(file.GetDriveFile(drive), token);
                }
                catch (Exception ex)
                {
                    OnError(ex);
                }
            }
            throw new Exception("Can't download this file");
        }

        private void OnError(Exception ex)
        {
            throw ex;
        }

        public async Task<Image> GetThumbnailAsync(AccountFile file, CancellationToken token)
        {
            foreach (var drive in Drives)
            {
                try
                {
                    return await drive.GetThumbnailAsync(file.GetDriveFile(drive), token);
                }
                catch (Exception ex)
                {
                    OnError(ex);
                }
            }
            throw new Exception("Can't download this file");
        }


        /// <summary>
        /// Download folder or file from storage (not from drive).
        /// </summary>
        /// <param name="destFolder">Path of folder whare to nest a downloaded file or folder.
        /// For example if <paramref name="file"/> is "/photo2010/egypt" and <paramref name="destFolder"/> 
        /// is "d:/photos", then in "photos" folder will be created subfolder "egypt" with downloaded files.
        /// </param>
        public async Task DownloadFileFromStorageAsync(AccountFile file, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            await Storage.DownloadFileAsync(file.StorageFile, destFolder, actionIfFileExists, token, progressCallback);
        }

        public async Task DownloadFileFromStorageAsync(AccountFile file, Stream output, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            await Storage.DownloadFileAsync(file.StorageFile, output, token, progressCallback);
        }

        public async Task DownloadFileFromDriveAsync(AccountFile file, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token)
        {
            foreach (var drive in Drives)
            {
                try
                {
                    await drive.DownloadFileAsync(file.GetDriveFile(drive), destFolder, actionIfFileExists, token);
                    return;
                }
                catch (Exception ex)
                {
                    OnError(ex);
                }
            }
            throw new Exception("Can't download this file");
        }

        public async Task DownloadFileFromDriveAsync(AccountFile file, Stream output, CancellationToken token)
        {
            foreach (var drive in Drives)
            {
                try
                {
                    await drive.DownloadFileAsync(file.GetDriveFile(drive), output, token);
                    return;
                }
                catch (Exception ex)
                {
                    OnError(ex);
                }
            }
            throw new Exception("Can't download this file");
        }
        /// <summary>
        /// Upload folder of file to storage and drive.
        /// </summary>
        public async Task UploadFileAsync(string pathName, AccountFile destFolder, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            var uploadedFile = await Storage.UploadFileAsync(pathName, destFolder.StorageFile, token, progressCallback);
            var tasks = Drives.Select(drive => drive.UploadFileAsync(pathName, destFolder.GetDriveFile(drive), false, uploadedFile.Id, token));
            await Task.WhenAll(tasks);
        }

        internal void Disconnect()
        {
        }

        public virtual async Task<Image> GetImageAsync(AccountFile file, CancellationToken token)
        {
            foreach (var drive in Drives)
            {
                var f = file.GetDriveFile(drive);
                if (f == null)
                {
                    continue;
                }
                var image = await drive.GetImageAsync(f, token);
                if (image != null)
                {
                    return image;
                }
            }
            return null;
        }

        public async Task<AccountFile> CreateFolderAsync(string folderName, AccountFile destFolder,
            CancellationToken token)
        {
            var storageDir =
                await
                    Storage.CreateFolderAsync(folderName, destFolder.StorageFile,
                        token);
            var tasks = Drives.Select(drive => drive.CreateFolderAsync(folderName, destFolder.GetDriveFile(drive), token));
            return new AccountFile(this, storageDir, await Task.WhenAll(tasks), destFolder);
        }
        
        public async Task DeleteFolderAsync(AccountFile folder, CancellationToken token)
        {
            var tasks = Drives.Select(drive => drive.DeleteFolderAsync(folder.GetDriveFile(drive), token));
            await Task.WhenAll(tasks);
        }

        public async Task<ICollection<AccountFile>> GetFilesAsync(AccountFile folder, CancellationToken token)
        {
            IDictionary<string, DriveFileCollection> driveFiles = new Dictionary<string, DriveFileCollection>();
            foreach (var f in folder.DriveFiles)
            {
                PutFilesTo(await f.Drive.GetFilesAsync(f, token), driveFiles);
            }
            return driveFiles.Values.Select(files => new AccountFile(this, Storage.GetFile(files), files, folder)).ToList();
        }

        public async Task<AccountFile> GetFileAsync(XElement fileXml, CancellationToken token)
        {
            var storageXml = fileXml.Element("storageFile");
            var driveXmls = fileXml.Element("driveFiles").Elements();
            var storageFile = storageXml == null ? null : await Storage.GetFileAsync(storageXml, token);
            List<DriveFile> driveFiles;
            var tasks =
                Drives.Select(
                    drive =>
                    {
                        var driveFileXml = driveXmls.SingleOrDefault(x => x.Attribute("driveId").Value == drive.Id);
                        var ret = driveFileXml == null ? null : drive.GetFileAsync(driveFileXml, token);
                        return ret;
                    }).Where(x => x != null);
            driveFiles = new List<DriveFile>(await Task.WhenAll(tasks));
            var parentXml = fileXml.Element("parent");
            AccountFile parent = parentXml == null ? null : await GetFileAsync(parentXml, token);
            return new AccountFile(this, storageFile, driveFiles, parent);
        }

        private void PutFilesTo(IEnumerable<DriveFile> files,
            IDictionary<string, DriveFileCollection> fileCollections)
        {
            foreach (var file in files)
            {
                PutFileTo(file, fileCollections);
            }
        }

        private void PutFileTo(DriveFile file, IDictionary<string, DriveFileCollection> fileCollections)
        {
            DriveFileCollection collection;
            if (file.StorageFileId == null)
            {
                if (!TryFindFileByName(file.Name, fileCollections.Values, out collection))
                {
                    collection = new DriveFileCollection();
                    fileCollections.Add(file.StorageFileId ?? Guid.NewGuid().ToString(), collection);
                }
            }
            else if (!fileCollections.TryGetValue(file.StorageFileId, out collection))
            {
                collection = new DriveFileCollection();
                fileCollections.Add(file.StorageFileId ?? Guid.NewGuid().ToString(), collection);
            }
            collection.Add(file);
        }

        private static bool TryFindFileByName(string fileName, IEnumerable<DriveFileCollection> fileCollections, 
            out DriveFileCollection collection)
        {
            foreach (var item in fileCollections.Where(item => (item.FirstOrDefault()?.Name) == fileName))
            {
                collection = item;
                return true;
            }
            collection = null;
            return false;
        }
    }
}