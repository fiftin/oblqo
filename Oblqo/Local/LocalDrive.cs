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

        public LocalDrive(Account account, string id, string rootPath) : base(account, id)
        {
            rootFolder = LocalFileFactory.Instance.Create(this, new DirectoryInfo(rootPath), true);
        }

        public override DriveFile RootFolder => rootFolder;

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
            await Task.Run(() => ((LocalFile)driveFile).File.Delete());
        }

        public override async Task DeleteFolderAsync(DriveFile driveFolder, CancellationToken token)
        {
            await Task.Run(() => ((LocalFile)driveFolder).Directory.Delete());
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
            await Task.Run(() =>
            {
                foreach (var file in ((LocalFile)driveFolder).Directory.EnumerateFiles())
                {
                    action(LocalFileFactory.Instance.Create(this, file, false));
                }
            });
        }

        public override async Task<DriveFile> GetFileAsync(XElement xml, CancellationToken token)
        {
            return LocalFileFactory.Instance.Create(this, new FileInfo(xml.Attribute("fileId").Value), false);
        }

        public override async Task<ICollection<DriveFile>> GetFilesAsync(DriveFile folder, CancellationToken token)
        {
            return await Task.Run(() => ((LocalFile)folder).Directory.EnumerateFiles()
                .Select(file => LocalFileFactory.Instance.Create(this, file, false))
                .Cast<DriveFile>().ToList());
        }

        public override async Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile folder, CancellationToken token)
        {
            var dir = ((LocalFile)folder).Directory;
            if (!dir.Exists)
            {
                dir.Create();
            }
            return await Task.Run(() => dir.EnumerateDirectories()
                .Select(file => LocalFileFactory.Instance.Create(this, file, false))
                .Cast<DriveFile>().ToList());
        }

        public override async Task<Image> GetThumbnailAsync(DriveFile file, CancellationToken token)
        {
            if (!((LocalFile)file).IsImage)
            {
                return null;
            }
            return await Task.Run(() =>
            {
                using (var stream = ((LocalFile)file).File.OpenRead())
                {
                    return Image.FromStream(stream);
                }
            });
        }

        public override async Task<Stream> ReadFileAsync(DriveFile file, CancellationToken token)
        {
            return ((LocalFile)file).File.OpenRead();
        }

        public override async Task<DriveFile> UploadFileAsync(Stream stream, string fileName, DriveFile destFolder, string storageFileId, CancellationToken token)
        {
            Stream scaled;
            var f = new FileInfo(((LocalFile)destFolder).file.FullName + Path.DirectorySeparatorChar + fileName);
            var localFile = LocalFileFactory.Instance.Create(this, f, false);
            ImageFormat imageType;
            if (!ImageMaxSize.IsEmpty && TryGetImageType(fileName, out imageType))
            {
                using (var image = Image.FromStream(stream))
                {
                    scaled = await ScaleImageAsync(image, imageType, token);
                    await localFile.SetAttributeAsync("OriginalImageHeight", image.Height.ToString(), token);
                    await localFile.SetAttributeAsync("OriginalImageWidth", image.Width.ToString(), token);
                }
            }
            else
            {
                scaled = stream;
            }
            var observed = new ObserverStream(scaled);
            observed.PositionChanged += (sender, e) => { };
            using (var outStream = f.Create())
            {
                await scaled.CopyToAsync(outStream);
            }
            await localFile.SetStorageFileIdAsync(storageFileId, token);
            return localFile;

        }

       
    }
}
