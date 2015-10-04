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
        public Storage Storage { get; private set; }
        public Account Account { get; private set; }

        /// <summary>
        /// Name format of property for storage file ID.
        /// {0} - storage kind.
        /// {1} - storage file id part index *.
        /// * If storage file id length more then PropertyMaxLength, then
        /// storage file id splitting by parts with length PropertyMaxLength.
        /// </summary>
        public static readonly string StorageFileIdFormat = "{0}.id-{1}";

        protected Drive(Storage storage, Account account)
        {
            Storage = storage;
            Account = account;
        }

        public Image ScaleImage(Image image)
        {
            var xScale = 1f;
            var yScale = 1f;
            if (image.Width > ImageMaxSize.Width)
                xScale = ImageMaxSize.Width / (float)image.Width;
            if (image.Height > ImageMaxSize.Height)
                yScale = ImageMaxSize.Height / (float)image.Height;
            var scale = Math.Min(xScale, yScale);
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

        public bool TryGetImageType(string pathName, out ImageType type)
        {
            var ext = Path.GetExtension(pathName)?.ToLower();
            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                    type = ImageType.Jpeg;
                    break;
                case ".png":
                    type = ImageType.Png;
                    break;
                case ".bmp":
                    type = ImageType.Bmp;
                    break;
                default:
                    type = ImageType.Bmp;
                    return false;
            }
            return true;
        }

#pragma warning disable 1998
        public async Task<Stream> ScaleImageAsync(ImageType type, Image image, Stream defaultStream)
#pragma warning restore 1998
        {
            var newImage = ScaleImage(image);
            ImageFormat format;
            switch (type)
            {
                case ImageType.Bmp:
                    format = ImageFormat.Bmp;
                    break;
                case ImageType.Jpeg:
                    format = ImageFormat.Jpeg;
                    break;
                case ImageType.Png:
                    format = ImageFormat.Png;
                    break;
                default:
                    return defaultStream;
            }
            var output = new MemoryStream();
            newImage.Save(output, format);
            output.Position = 0;
            return output;
        }

        public async Task<Stream> ScaleImageAsync(ImageType type, Stream input)
        {
            var image = Image.FromStream(input);
            return await ScaleImageAsync(type, image, input);
        }
        

        public virtual async Task<Image> GetImageAsync(DriveFile file, CancellationToken token)
        {
            return Image.FromStream(await ReadFileAsync(file, token));
        }

        
        public abstract Task DeleteFileAsync(DriveFile driveFile, CancellationToken token);
        public abstract Task EnumerateFilesRecursive(DriveFile driveFolder, Action<DriveFile> action, CancellationToken token);
        
        public abstract Task DownloadFileAsync(DriveFile driveFile, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token);
        
        public abstract Task<DriveFile> UploadFileAsync(string pathName, DriveFile destFolder, string storageFileId, CancellationToken token);

        public abstract Task<Stream> ReadFileAsync(DriveFile file, CancellationToken token);
        public abstract Task<Image> GetThumbnailAsync(DriveFile file, CancellationToken token);
        public abstract Task<ICollection<DriveFile>> GetFilesAsync(DriveFile folder, CancellationToken token);
        public abstract Task<DriveFile> CreateFolderAsync(string folderName, DriveFile destFolder, CancellationToken token);
        public abstract Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile folder, CancellationToken token);

        public abstract Task ClearAsync(CancellationToken token);

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
