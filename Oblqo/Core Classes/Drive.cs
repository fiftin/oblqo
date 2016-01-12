using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Drive
    {
        public abstract DriveFile RootFolder { get; }

        public Size ImageMaxSize { get; set; }

        public Account Owner { get; }

        public Storage Storage => Owner.Storage;

        public string Id { get; }
        public abstract string ShortName { get; }

        /// <summary>
        /// Name format of property for storage file ID.
        /// {0} - storage kind.
        /// {1} - storage file id part index *.
        /// * If storage file id length more then PropertyMaxLength, then
        /// storage file id splitting by parts with length PropertyMaxLength.
        /// </summary>
        public static readonly string StorageFileIdFormat = "{0}.id-{1}";

        protected Drive(Account owner, string id)
        {
            Owner = owner;
            Id = id;
        }

        private Image ScaleImage(Image image)
        {
            if (ImageMaxSize.IsEmpty) // scaling not required
            {
                return image;
            }
            var xScale = 1f;
            var yScale = 1f;
            if (ImageMaxSize.Width > 0 && image.Width > ImageMaxSize.Width)
                xScale = ImageMaxSize.Width / (float)image.Width;
            if (ImageMaxSize.Height > 0 && image.Height > ImageMaxSize.Height)
                yScale = ImageMaxSize.Height / (float)image.Height;
            var scale = Math.Min(xScale, yScale);
            if (scale == 1) // scaling not required
            {
                return image;
            }
            var ret = new Bitmap((int)(image.Width * scale), (int)(image.Height * scale));
            using (var g = Graphics.FromImage(ret))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(image, 0, 0, ret.Width, ret.Height);
            }
            return ret;
        }

        public bool TryGetImageType(string pathName, out ImageFormat type)
        {
            var ext = Path.GetExtension(pathName)?.ToLower();
            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                    type = ImageFormat.Jpeg;
                    break;
                case ".png":
                    type = ImageFormat.Png;
                    break;
                case ".bmp":
                    type = ImageFormat.Bmp;
                    break;
                default:
                    type = null;
                    return false;
            }
            return true;
        }
        
        public async Task<Stream> ScaleImageAsync(Image image, ImageFormat type, CancellationToken token)
        {
            var output = new MemoryStream();
            await Task.Run(() =>
            {
                var newImage = ScaleImage(image);
                newImage.Save(output, type);
            });
            output.Position = 0;
            return output;
        }

        public virtual async Task<Image> GetImageAsync(DriveFile file, CancellationToken token)
        {
            using (var stream = await ReadFileAsync(file, token))
            {
                return await Task.Run(() => Image.FromStream(stream));
            }
        }
        
        public abstract Task DeleteFileAsync(DriveFile driveFile, CancellationToken token);
        public abstract Task EnumerateFilesRecursive(DriveFile driveFolder, Action<DriveFile> action, CancellationToken token);
        
        public abstract Task DownloadFileAsync(DriveFile driveFile, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token);
        public abstract Task DownloadFileAsync(DriveFile driveFile, Stream output, CancellationToken token);

        public async Task<DriveFile> UploadFileAsync(string pathName, DriveFile destFolder, string storageFileId, CancellationToken token)
        {
            using (var stream = File.OpenRead(pathName))
            {
                return await UploadFileAsync(stream, Path.GetFileName(pathName), destFolder, storageFileId, token);
            }
        }

        public abstract Task<DriveFile> UploadFileAsync(Stream fileStream, string fileName, DriveFile destFolder, string storageFileId, CancellationToken token);

        public abstract Task<Stream> ReadFileAsync(DriveFile file, CancellationToken token);
        public abstract Task<Image> GetThumbnailAsync(DriveFile file, CancellationToken token);
        public abstract Task<ICollection<DriveFile>> GetFilesAsync(DriveFile folder, CancellationToken token);
        public abstract Task<DriveFile> CreateFolderAsync(string folderName, DriveFile destFolder, CancellationToken token);
        public abstract Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile folder, CancellationToken token);

        public abstract Task DeleteFolderAsync(DriveFile driveFolder, CancellationToken token);

        /// <summary>
        /// Get file by information from <paramref name="xml"/>. Used for resuming tasks.
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="token">CancellationToken class instance for cancellation async operation.</param>
        /// <returns>Drive file instance.</returns>
        public abstract Task<DriveFile> GetFileAsync(System.Xml.Linq.XElement xml, CancellationToken token);

    }
}
