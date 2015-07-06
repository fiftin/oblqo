using System;
using System.Linq;
using Google.Apis.Drive.v2.Data;

namespace Oblqo.Google
{
    public class GoogleFile : DriveFile
    {
        internal readonly File file;
        internal bool hasChildren;

        public const string OriginalImageWidthPropertyKey = "originalImageWidth";
        public const string OriginalImageHeightPropertyKey = "originalImageHeight";
        public const string OriginalSizePropertyKey = "originalSize";

        public GoogleFile(GoogleDrive drive, File file)
            : base(drive)
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

        public override string MimeType
        {
            get { return file.MimeType; }
        }

        public override System.Xml.Linq.XElement ToXml()
        {
            var ret = base.ToXml();
            ret.SetAttributeValue("fileId", file.Id);
            return ret;
        }

        public static int GetNumericValue(char c)
        {
            return c - '0';
        }

        public override string StorageFileId
        {
            get
            {
                var srcStr = PropertyValue("src");
                if (srcStr == null) return null;
                var sources = srcStr.Split(';').Where((x) => x.StartsWith(Drive.Storage.Kind));
                foreach (var src in sources)
                {
                    var sidPropertyName = string.Format("{0}.sid", src);
                    if (Drive.Storage.Id != PropertyValue(sidPropertyName))
                    {
                        continue;
                    }
                    var fileIdKeyLen = string.Format(GoogleDrive.StorageFileIdFormat, src, 0).Length;
                    var startsWith = string.Format(GoogleDrive.StorageFileIdFormat, src, "");
                    string[] parts = new string[9];
                    foreach (var prop in file.Properties)
                    {
                        if (prop.Key.Length == fileIdKeyLen
                            && prop.Key.StartsWith(startsWith)
                            && char.IsDigit(prop.Key.Last()))
                        {
                            parts[GetNumericValue(prop.Key.Last())] = prop.Value;
                        }
                    }
                    return string.Join("", parts);
                }
                return null;
            }
        }

        private string PropertyValue(string key)
        {
            if (file.Properties == null)
            {
                return null;
            }
            foreach (var prop in file.Properties)
            {
                if (prop.Key == key)
                {
                    return prop.Value;
                }
            }
            return null;
        }

    }
}
