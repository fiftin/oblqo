using System;
using System.Linq;
using Google.Apis.Drive.v2.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo.Google
{
    public class GoogleFile : DriveFile
    {
        internal readonly File file;
        internal bool hasChildren;

        public const int PropertyMaxLength = 124 / 2;

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
                var property = file.Properties.FirstOrDefault(x => x.Key == nameof(OriginalImageWidth));
                return property == null ? ImageWidth : int.Parse(property.Value);
            }
            set
            {
                file.Properties.Add(new Property() { Key = nameof(OriginalImageWidth), Value = value.ToString() });
            }
        }

        public override int OriginalImageHeight
        {
            get
            {
                if (file.Properties == null)
                    return ImageHeight;
                var property = file.Properties.FirstOrDefault(x => x.Key == nameof(OriginalImageHeight));
                return property == null ? ImageHeight : int.Parse(property.Value);
            }
            set
            {
                if (file.Properties == null)
                {
                    file.Properties = new List<Property>();
                }
                file.Properties.Add(new Property() { Key = nameof(OriginalImageHeight), Value = value.ToString() });
            }
        }

        public override long OriginalSize
        {
            get
            {
                if (file.Properties == null)
                    return Size;
                var property = file.Properties.FirstOrDefault(x => x.Key == nameof(OriginalSize));
                return property == null ? Size : long.Parse(property.Value);
            }
            set
            {
                file.Properties.Add(new Property() { Key = nameof(OriginalSize), Value = value.ToString() });
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

        public override string GetAttribute(string name)
        {
            if (file.Properties == null)
            {
                return null;
            }

            var ret = PropertyValue(name);
            if (ret != null)
            {
                return ret;
            }

            // gl-1.id-{1}
            var fileIdKeyLen = string.Format("{0}-{1}", name, 0).Length;
            var startsWith = string.Format("{0}-{1}", name, "");
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
        /*
        public override async void SetAttribute(string name, string value)
        {

            // Initializing properties.
            List<Property> props = new List<Property>();
            props.Add(new Property { Key = string.Format("{0}.sid", Drive.Storage.Kind), Value = Drive.Storage.Id, Visibility = "PRIVATE" });
            props.Add(new Property { Key = "src", Value = Drive.Storage.Kind, Visibility = "PRIVATE" });

            // Field
            int storageFileIdPropertyKeyLen = string.Format(Drive.StorageFileIdFormat, Drive.Storage.Kind, 0).Length;
            int storageFileIdPropertyValueLen = PropertyMaxLength - storageFileIdPropertyKeyLen;
            string[] storageFileIdParts = Common.SplitBy(StorageFileId, storageFileIdPropertyValueLen);
            if (storageFileIdParts.Length > 9) throw new Exception("Storage file ID is too long");
            for (int i = 0; i < storageFileIdParts.Length; i++)
            {
                props.Add(new Property { Key = string.Format(Drive.StorageFileIdFormat, Drive.Storage.Kind, i), Value = storageFileIdParts[i], Visibility = "PRIVATE" });
            }
            var file = new File
            {
                Properties = props,
            };

            var service = await ((GoogleDrive)Drive).GetServiceAsync(CancellationToken.None);
            var newFile = await service.Files.Update(file, this.file.Id).ExecuteAsync();   
        }
        */

        public override async Task SetAttributeAsync(string name, string value, CancellationToken token)
        {

            // Initializing properties.
            List<Property> props = new List<Property>();

            // Field
            int keyLen = string.Format(Drive.StorageFileIdFormat, Drive.Storage.Kind, 0).Length;
            int valueLen = PropertyMaxLength - keyLen;
            if (value.Length <= valueLen)
            {
                props.Add(new Property { Key = name, Value = value, Visibility = "PRIVATE" });
            }
            else
            {
                string[] storageFileIdParts = Common.SplitBy(value, valueLen);
                if (storageFileIdParts.Length > 9) throw new Exception("Storage file ID is too long");
                for (int i = 0; i < storageFileIdParts.Length; i++)
                {
                    props.Add(new Property { Key = name + "-" + i, Value = storageFileIdParts[i], Visibility = "PRIVATE" });
                }
            }

            var file = new File
            {
                Properties = props,
            };

            var service = await((GoogleDrive)Drive).GetServiceAsync(token);
            var newFile = await service.Files.Update(file, this.file.Id).ExecuteAsync(token);
        }
    }
}
