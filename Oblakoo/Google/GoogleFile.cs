using System;
using System.Linq;
using Google.Apis.Drive.v2.Data;

namespace Oblakoo.Google
{
    public class GoogleFile : DriveFile
    {
        internal readonly File file;
        internal bool hasChildren;

        public const string StorageFileIdPropertyKey = "storageFileId";
        public const string OriginalImageWidthPropertyKey = "originalImageWidth";
        public const string OriginalImageHeightPropertyKey = "originalImageHeight";
        public const string OriginalSizePropertyKey = "originalSize";

        public GoogleFile(File file)
        {
            this.file = file;
        }

        public override string Id
        {
            get { return file.Id; }
        }

        public override bool IsImage
        {
            get { return file.MimeType.StartsWith("image/"); }
        }

        public override bool IsFolder
        {
            get { return file.MimeType == GoogleMimeTypes.Folder; }
        }

        public override string Name
        {
            get { return file.Title; }
        }

        public override bool HasChildren
        {
            get { return hasChildren; }
        }

        public override string StorageFileId
        {
            get
            {
                if (file.Properties == null)
                    return "";
                foreach (var prop in file.Properties.Where(prop => prop.Key == StorageFileIdPropertyKey))
                    return prop.Value;
                return "";
            }
            set
            {
                file.Properties.Add(new Property { Key = StorageFileIdPropertyKey, Value = value });
            }
        }

        public override long Size
        {
            get { return file.FileSize.GetValueOrDefault(); }
        }

        public override DateTime ModifiedDate
        {
            get { return file.ModifiedDate.GetValueOrDefault(); }
        }

        public override DateTime CreatedDate
        {
            get { return file.CreatedDate.GetValueOrDefault(); }
        }

        public override int OriginalImageWidth
        {
            get
            {
                if (file.Properties == null)
                    return ImageWidth;
                var property = file.Properties.FirstOrDefault(x => x.Key == OriginalImageWidthPropertyKey);
                return property == null ? ImageWidth : int.Parse(property.Value);
            }
        }

        public override int OriginalImageHeight
        {
            get
            {
                if (file.Properties == null)
                    return ImageHeight;
                var property = file.Properties.FirstOrDefault(x => x.Key == OriginalImageHeightPropertyKey);
                return property == null ? ImageHeight : int.Parse(property.Value);
            }
        }

        public override long OriginalSize
        {
            get
            {
                if (file.Properties == null)
                    return Size;
                var property = file.Properties.FirstOrDefault(x => x.Key == OriginalSizePropertyKey);
                return property == null ? Size : long.Parse(property.Value);
            }
        }

        public override int ImageWidth
        {
            get
            {
                if (file.ImageMediaMetadata != null && file.ImageMediaMetadata.Width != null)
                    return file.ImageMediaMetadata.Width.Value;
                return 0;
            }

        }

        public override int ImageHeight
        {
            get
            {
                if (file.ImageMediaMetadata != null && file.ImageMediaMetadata.Height != null)
                    return file.ImageMediaMetadata.Height.Value;
                return 0;
            }
        }

        public override bool IsRoot
        {
            get { return Id == GoogleDrive.RootId; }
        }
    }
}
