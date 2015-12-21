using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo.Local
{
    public class LocalDrive : Drive
    {
		private DriveFile rootFolder;

        public LocalDrive(Account account, string id, string rootPath)
            : base(account, id)
        {
			rootFolder = LocalFileFactory.Instance.Create(this, new DirectoryInfo(rootPath), true);
        }

		public override DriveFile RootFolder 
		{ 
			get 
			{ 
				return rootFolder;
			}
		}

        public override string ShortName => "Local";


#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task<DriveFile> CreateFolderAsync(string folderName, DriveFile destFolder, CancellationToken token)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var dir = Directory.CreateDirectory(((LocalFile)destFolder).file.FullName + Path.DirectorySeparatorChar + folderName);
            return LocalFileFactory.Instance.Create(this, dir, false);
        }

        public override async Task DeleteFileAsync(DriveFile driveFile, CancellationToken token)
        {
            ((LocalFile)driveFile).File.Delete();
        }

        public override async Task DeleteFolderAsync(DriveFile driveFolder, CancellationToken token)
        {
            ((LocalFile)driveFolder).Directory.Delete();
        }

        public override async Task DownloadFileAsync(DriveFile driveFile, Stream output, CancellationToken token)
        {
            using (var input = ((LocalFile)driveFile).File.OpenRead())
            {
                await Common.CopyStreamAsync(input, output, token);
            }
        }

        public override async Task DownloadFileAsync(DriveFile driveFile, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token)
        {
            // TODO: use actionIfFileExists
            using (var output = File.OpenWrite(destFolder + Path.DirectorySeparatorChar + driveFile.Name))
            {
                await DownloadFileAsync(driveFile, output, token);
            }
        }

        public override async Task EnumerateFilesRecursive(DriveFile driveFolder, Action<DriveFile> action, CancellationToken token)
        {
            foreach (var file in ((LocalFile)driveFolder).Directory.EnumerateFiles())
            {
                action(LocalFileFactory.Instance.Create(this, file, false));
            }
        }

        public override async Task<DriveFile> GetFileAsync(XElement xml, CancellationToken token)
        {
            return LocalFileFactory.Instance.Create(this, new FileInfo(xml.Attribute("fileId").Value), false);
        }

        public override async Task<ICollection<DriveFile>> GetFilesAsync(DriveFile folder, CancellationToken token)
        {
            return ((LocalFile) folder).Directory.EnumerateFiles()
                .Select(file => LocalFileFactory.Instance.Create(this, file, false))
                .Cast<DriveFile>().ToList();
        }

        public override async Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile folder, CancellationToken token)
        {
            return ((LocalFile) folder).Directory.EnumerateDirectories()
                .Select(file => LocalFileFactory.Instance.Create(this, file, false))
                .Cast<DriveFile>().ToList();
        }

        public override async Task<Image> GetThumbnailAsync(DriveFile file, CancellationToken token)
        {
            return !((LocalFile)file).IsImage ? null : Image.FromStream(((LocalFile)file).File.OpenRead());
        }

        public override async Task<Stream> ReadFileAsync(DriveFile file, CancellationToken token)
        {
            return ((LocalFile)file).File.OpenRead();
        }

        public override async Task<DriveFile> UploadFileAsync(System.IO.Stream stream, string fileName, DriveFile destFolder, bool scaleRequired, string storageFileId, CancellationToken token)
        {
            ImageFormat imageType;
            Stream scaled;
            Image image;
            if (TryGetImageType(fileName, out imageType))
            {
                image = Image.FromStream(stream);
                scaled = await ScaleImageAsync(image, imageType, token);
            }
            else
            {
                image = null;
                scaled = stream;
            }
            var observed = new ObserverStream(scaled);
            observed.PositionChanged += (sender, e) => { };
            var f = new FileInfo(((LocalFile)destFolder).file.FullName + Path.DirectorySeparatorChar + fileName);
            using (var outStream = f.Create())
            {
                await scaled.CopyToAsync(outStream);
            }
            var localFile = LocalFileFactory.Instance.Create(this, f, false);
            //var originFile = new FileInfo(pathName);
            await localFile.SetAttributeAsync("StorageFileId", storageFileId, token);
            //await localFile.SetAttributeAsync(nameof(localFile.OriginalSize), originFile.Length.ToString(), token);
            await localFile.SetAttributeAsync("OriginalImageHeight", image?.Height.ToString(), token);
            await localFile.SetAttributeAsync("OriginalImageWidth", image?.Width.ToString(), token);
            return localFile;

        }

        public override async Task<DriveFile> UploadFileAsync(string pathName, DriveFile destFolder, bool scaleRequired, string storageFileId, CancellationToken token)
        {
            using (var stream = new FileStream(pathName, System.IO.FileMode.Open))
            {
                return await UploadFileAsync(stream, Path.GetFileName(pathName), destFolder, scaleRequired, storageFileId, token);
            }
        }

        
    }
}
