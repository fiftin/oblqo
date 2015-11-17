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
    /// <summary>
    /// Complex drive.
    /// </summary>
    public class DriveCollection : IEnumerable<Drive>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly List<Drive> drives = new List<Drive>();

        public Account Owner { get; internal set; }

        public Storage Storage => Owner.Storage;

        public int Count => drives.Count;

        public DriveFileCollection RootFolder { get; }

        public DriveCollection()
        {
            RootFolder = new DriveFileCollection(this);
        }

        public void Add(Drive drive)
        {
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
            AccountFile owner, CancellationToken token)
        {
            var tasks = drives.Select(drive => drive.UploadFileAsync(pathName, destFolder.GetFile(drive), scaleRequired, storageFileId, token));
            return new DriveFileCollection(this, await Task.WhenAll(tasks), owner);
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
            AccountFile owner, CancellationToken token)
        {
            var tasks = drives.Select(drive => drive.CreateFolderAsync(folderName, destFolder.GetFile(drive), token));
            return new DriveFileCollection(this, await Task.WhenAll(tasks), owner);
        }

        public async Task DeleteFolderAsync(DriveFileCollection driveFolder, CancellationToken token)
        {
            var tasks = drives.Select(drive => drive.DeleteFolderAsync(driveFolder.GetFile(drive), token));
            await Task.WhenAll(tasks);
        }

        public async Task<ICollection<DriveFileCollection>> GetSubfoldersAsync(DriveFileCollection folder,
            AccountFile owner, CancellationToken token)
        {
            var dic = new SortedDictionary<string, DriveFileCollection>();
            foreach (var f in folder.Files)
            {
                AddFiles(await f.Drive.GetSubfoldersAsync(f, token), dic, owner);
            }
            return dic.Values;
        }

        private bool TryFindFileByName(string fileName, IEnumerable<DriveFileCollection> fileCollections, out DriveFileCollection collection)
        {
            foreach (var item in fileCollections.Where(item => item.Name == fileName))
            {
                collection = item;
                return true;
            }
            collection = null;
            return false;
        }

        private void AddFile(DriveFile file, IDictionary<string, DriveFileCollection> fileCollections, AccountFile owner)
        {
            DriveFileCollection collection;
            if (file.StorageFileId == null)
            {
                if (!TryFindFileByName(file.Name, fileCollections.Values, out collection))
                {
                    collection = new DriveFileCollection(this, owner);
                    fileCollections.Add(file.StorageFileId ?? Guid.NewGuid().ToString(), collection);
                }
            }
            else if (!fileCollections.TryGetValue(file.StorageFileId, out collection))
            {
                collection = new DriveFileCollection(this, owner);
                fileCollections.Add(file.StorageFileId ?? Guid.NewGuid().ToString(), collection);
            }
            collection.Add(file);
        }

        private void AddFiles(IEnumerable<DriveFile> files, 
            SortedDictionary<string, DriveFileCollection> fileCollections,
            AccountFile owner)
        {
            foreach (var file in files)
            {
                AddFile(file, fileCollections, owner);
            }
        }

        public async Task<ICollection<DriveFileCollection>> GetFilesAsync(DriveFileCollection folder,
            AccountFile owner, CancellationToken token)
        {
            var dic = new SortedDictionary<string, DriveFileCollection>();
            foreach (var f in folder.Files)
            {
                AddFiles(await f.Drive.GetFilesAsync(f, token), dic, owner);
            }
            return dic.Values;
        }

        public async Task<DriveFileCollection> GetFileAsync(XElement xml, CancellationToken token)
        {
            var elements = xml.Elements();
            var tasks = drives.Select(drive => drive.GetFileAsync(elements.Single(x => x.Attribute("").Value == drive.Id), token));
            return new DriveFileCollection(this, await Task.WhenAll(tasks), null);
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