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


        public DriveCollection()
        {
            RootFolder = new DriveFileCollection();
        }

        public DriveFileCollection RootFolder { get; }

        public Size ImageMaxSize
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Storage Storage { get; private set; }
        public Account Account { get; private set; }

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

        public async Task EnumerateFilesRecursive(DriveFileCollection driveFolder, Action<DriveFileCollection> action,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task DownloadFileAsync(DriveFileCollection driveFile, string destFolder,
            ActionIfFileExists actionIfFileExists,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<DriveFile> UploadFileAsync(string pathName, DriveFileCollection destFolder, string storageFileId,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> ReadFileAsync(DriveFileCollection file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetThumbnailAsync(DriveFileCollection file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<DriveFileCollection>> GetFilesAsync(DriveFileCollection folder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<DriveFileCollection> CreateFolderAsync(string folderName, DriveFileCollection destFolder,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<DriveFileCollection>> GetSubfoldersAsync(DriveFileCollection folder,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFolderAsync(DriveFileCollection driveFolder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<DriveFileCollection> GetFileAsync(XElement xml, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}