using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public class DriveCollection
    {
        private List<Drive> drives = new List<Drive>();

        public void Add(Drive drive)
        {
            drives.Add(drive);
        }

        public DriveFileCollection RootFolder
        {
            get { throw new NotImplementedException(); }
        }

        public Size ImageMaxSize
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Storage Storage
        {
            get { throw new NotImplementedException(); }
        }

        public Account Account
        {
            get { throw new NotImplementedException(); }
        }

        public Image ScaleImage(Image image)
        {
            throw new NotImplementedException();
        }

        public bool TryGetImageType(string pathName, out ImageType type)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> ScaleImageAsync(ImageType type, Image image, Stream defaultStream)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> ScaleImageAsync(ImageType type, Stream input)
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetImageAsync(DriveFileCollection file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFileAsync(DriveFileCollection driveFile, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task EnumerateFilesRecursive(DriveFileCollection driveFolder, Action<DriveFileCollection> action, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task DownloadFileAsync(DriveFileCollection driveFile, string destFolder, ActionIfFileExists actionIfFileExists,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<DriveFile> UploadFileAsync(string pathName, DriveFileCollection destFolder, string storageFileId, CancellationToken token)
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

        public Task<DriveFileCollection> CreateFolderAsync(string folderName, DriveFileCollection destFolder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<DriveFileCollection>> GetSubfoldersAsync(DriveFileCollection folder, CancellationToken token)
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