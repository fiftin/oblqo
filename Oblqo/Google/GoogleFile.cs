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

        public override string Id => file.Id;

        public override bool IsImage => file.MimeType.StartsWith("image/");

        public override bool IsFolder => file.MimeType == GoogleMimeTypes.Folder;

        public override string Name => file.Title;

        public override bool HasChildren => hasChildren;

        public override long Size => file.FileSize.GetValueOrDefault();

        public override DateTime ModifiedDate => file.ModifiedDate.GetValueOrDefault();

        public override DateTime CreatedDate => file.CreatedDate.GetValueOrDefault();

        public override int OriginalImageWidth
        {
            get
            {
                if (file.Properties == null)
                    return ImageWidth;
                var property = file.Properties.FirstOrDefault(x => x.Key == "OriginalImageWidth");
                return property == null ? ImageWidth : int.Parse(property.Value);
            }
            set
            {
                file.Properties.Add(new Property() { Key = "OriginalImageWidth", Value = value.ToString() });
            }
        }

        public override int OriginalImageHeight
        {
            get
            {
                if (file.Properties == null)
                    return ImageHeight;
                var property = file.Properties.FirstOrDefault(x => x.Key == "OriginalImageHeight");
                return property == null ? ImageHeight : int.Parse(property.Value);
            }
            set
            {
                if (file.Properties == null)
                {
                    file.Properties = new List<Property>();
                }
                file.Properties.Add(new Property() { Key = "OriginalImageHeight", Value = value.ToString() });
            }
        }

        public override long OriginalSize
        {
            get
            {
                if (file.Properties == null)
                    return Size;
                var property = file.Properties.FirstOrDefault(x => x.Key == "OriginalSize");
                return property == null ? Size : long.Parse(property.Value);
            }
            set
            {
                file.Properties.Add(new Property() { Key = "OriginalSize", Value = value.ToString() });
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

        public override bool IsRoot => Id == GoogleDrive.RootId;

        public override string MimeType => file.MimeType;

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
            return file.Properties == null ? null : (from prop in file.Properties where prop.Key == key select prop.Value).FirstOrDefault();
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
            var parts = new string[9];

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

        public override async Task SetAttributeAsync(string name, string value, CancellationToken token)
        {

            // Initializing properties.
            var props = new List<Property>();

            // Field
            var keyLen = string.Format(Drive.StorageFileIdFormat, Drive.Storage.Kind, 0).Length;
            var valueLen = PropertyMaxLength - keyLen;
            if (value.Length <= valueLen)
            {
                props.Add(new Property { Key = name, Value = value, Visibility = "PRIVATE" });
            }
            else
            {
                var storageFileIdParts = Common.SplitBy(value, valueLen);
                if (storageFileIdParts.Length > 9) throw new Exception("Storage file ID is too long");
                props.AddRange(storageFileIdParts.Select((t, i) => new Property {Key = name + "-" + i, Value = t, Visibility = "PRIVATE"}));
            }

            var service = await((GoogleDrive)Drive).GetServiceAsync(token);
            var newFile = await service.Files.Update(new File { Properties = props }, file.Id).ExecuteAsync(token);
        }

        public override async Task WriteAsync(byte[] bytes, CancellationToken token)
        {
            var stream = new System.IO.MemoryStream(bytes, false);

            var newFile = await ((GoogleDrive)Drive).UploadFileAsync(stream, Name, file.Parents, StorageFileId, token);
            
        }
    }
}
