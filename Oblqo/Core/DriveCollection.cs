using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public class DriveCollection : IEnumerable<Drive>
    {
        private readonly List<Drive> drives = new List<Drive>();
        public Storage Storage { get; private set; }
        public Account Account { get; private set; }

        public int Count => drives.Count;

        public DriveCollection()
        {
            RootFolder = new DriveFileCollection(this);
        }

        public DriveFileCollection RootFolder { get; }

        public void Add(Drive drive)
        {
            Account = drive.Account;
            Storage = drive.Storage;
            RootFolder.Add(drive.RootFolder);
            drives.Add(drive);
        }


        public async Task<Image> GetImageAsync(DriveFileCollection file, CancellationToken token)
        {
            foreach (var drive in drives)
            {
                var f = file.GetFile(drive);
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

        public async Task DeleteFileAsync(DriveFileCollection driveFile, CancellationToken token)
        {
            var tasks = drives.Select(drive => drive.DeleteFileAsync(driveFile.GetFile(drive), token));
            await Task.WhenAll(tasks);
        }

        public async Task DownloadFileAsync(DriveFileCollection driveFile, string destFolder,
            ActionIfFileExists actionIfFileExists,
            CancellationToken token)
        {
            foreach (var drive in drives)
            {
                try
                {
                    await drive.DownloadFileAsync(driveFile.GetFile(drive), destFolder, actionIfFileExists, token);
                }
                catch
                {
                    // ignored
                }
            }
            throw new Exception("Can't download this file");
        }

        public async Task<DriveFileCollection> UploadFileAsync(string pathName, DriveFileCollection destFolder, bool scaleRequired, string storageFileId,
            CancellationToken token)
        {
            var tasks = drives.Select(drive => drive.UploadFileAsync(pathName, destFolder.GetFile(drive), scaleRequired, storageFileId, token));
            return new DriveFileCollection(this, await Task.WhenAll(tasks));
        }

        public async Task<Stream> ReadFileAsync(DriveFileCollection file, CancellationToken token)
        {
            foreach (var drive in drives)
            {
                try
                {
                    return await drive.ReadFileAsync(file.GetFile(drive),  token);
                }
                catch
                {
                    // ignored
                }
            }
            throw new Exception("Can't download this file");
        }

        public async Task<Image> GetThumbnailAsync(DriveFileCollection file, CancellationToken token)
        {
            foreach (var drive in drives)
            {
                try
                {
                    return await drive.GetThumbnailAsync(file.GetFile(drive), token);
                }
                catch
                {
                    // ignored
                }
            }
            throw new Exception("Can't download this file");
        }

        public async Task<DriveFileCollection> CreateFolderAsync(string folderName, DriveFileCollection destFolder,
            CancellationToken token)
        {
            var tasks = drives.Select(drive => drive.CreateFolderAsync(folderName, destFolder.GetFile(drive), token));
            return new DriveFileCollection(this, await Task.WhenAll(tasks));
        }


        public async Task DeleteFolderAsync(DriveFileCollection driveFolder, CancellationToken token)
        {
            var tasks = drives.Select(drive => drive.DeleteFolderAsync(driveFolder.GetFile(drive), token));
            await Task.WhenAll(tasks);
        }

        public async Task<ICollection<DriveFileCollection>> GetSubfoldersAsync(DriveFileCollection folder,
            CancellationToken token)
        {
            var dic = new SortedDictionary<string, DriveFileCollection>();
            foreach (var f in folder.Files)
            {
                AddFiles(await f.Drive.GetSubfoldersAsync(f, token), dic);
            }
            return dic.Values;
        }

        private bool TryFindFileByName(string fileName, IEnumerable<DriveFileCollection> fileCollections, out DriveFileCollection collection)
        {
            foreach (var item in fileCollections)
            {
                if (item.Name == fileName)
                {
                    collection = item;
                    return true;
                }
            }
            collection = null;
            return false;
        }

        private void AddFile(DriveFile file, IDictionary<string, DriveFileCollection> fileCollections)
        {
            DriveFileCollection collection;
            if (file.StorageFileId == null)
            {
                if (!TryFindFileByName(file.Name, fileCollections.Values, out collection))
                {
                    collection = new DriveFileCollection(this);
                    fileCollections.Add(file.StorageFileId ?? Guid.NewGuid().ToString(), collection);
                }
            }
            else if (!fileCollections.TryGetValue(file.StorageFileId, out collection))
            {
                collection = new DriveFileCollection(this);
                fileCollections.Add(file.StorageFileId ?? Guid.NewGuid().ToString(), collection);
            }
            collection.Add(file);
        }

        private void AddFiles(IEnumerable<DriveFile> files, SortedDictionary<string, DriveFileCollection> fileCollections)
        {
            foreach (var file in files)
            {
                AddFile(file, fileCollections);
            }
        }

        public async Task<ICollection<DriveFileCollection>> GetFilesAsync(DriveFileCollection folder, CancellationToken token)
        {
            var dic = new SortedDictionary<string, DriveFileCollection>();
            foreach (var f in folder.Files)
            {
                AddFiles(await f.Drive.GetFilesAsync(f, token), dic);
            }
            return dic.Values;
        }

        public async Task<DriveFileCollection> GetFileAsync(XElement xml, CancellationToken token)
        {
            var elements = xml.Elements();
            var tasks = drives.Select(drive => drive.GetFileAsync(elements.Single(x => x.Attribute("").Value == drive.Id), token));
            return new DriveFileCollection(this, await Task.WhenAll(tasks));
        }

        public IEnumerator<Drive> GetEnumerator()
        {
            return drives.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}