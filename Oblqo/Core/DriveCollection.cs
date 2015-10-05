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
    public class DriveCollection
    {
        private readonly List<Drive> drives = new List<Drive>();
        public Storage Storage { get; private set; }
        public Account Account { get; private set; }


        public DriveCollection()
        {
            RootFolder = new DriveFileCollection(this);
        }

        public DriveFileCollection RootFolder { get; }

        public Size ImageMaxSize
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

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
                var image = await drive.GetImageAsync(file.GetFile(drive), token);
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

        public async Task<DriveFileCollection> UploadFileAsync(string pathName, DriveFileCollection destFolder, string storageFileId,
            CancellationToken token)
        {
            var tasks = drives.Select(drive => drive.UploadFileAsync(pathName, destFolder.GetFile(drive), storageFileId, token));
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

        public Task<ICollection<DriveFileCollection>> GetSubfoldersAsync(DriveFileCollection folder,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<DriveFileCollection>> GetFilesAsync(DriveFileCollection folder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<DriveFileCollection> GetFileAsync(XElement xml, CancellationToken token)
        {
            var elements = xml.Elements();
            var tasks = drives.Select(drive => drive.GetFileAsync(elements.Single(x => x.Attribute("").Value == drive.Id), token));
            return new DriveFileCollection(this, await Task.WhenAll(tasks));
        }
    }
}