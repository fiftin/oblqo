using System;
using System.Xml.Linq;

namespace Oblqo
{
    public abstract class DriveFile
    {
        public abstract string Id { get; }
        public abstract bool IsImage { get; }
        public abstract bool IsFolder { get; }
        public abstract string Name { get; }
        public abstract bool HasChildren { get; }
        public abstract string StorageFileId { get; }
        public abstract long Size { get; }
        public abstract DateTime ModifiedDate { get; }
        public abstract DateTime CreatedDate { get; }
        public abstract int OriginalImageWidth { get; }
        public abstract int OriginalImageHeight { get; }
        public abstract long OriginalSize { get; }
        public abstract int ImageWidth { get; }
        public abstract int ImageHeight { get; }
        public abstract bool IsRoot { get; }
        public abstract string MimeType { get; }

        public Drive Drive { get; private set; }

        public DriveFile(Drive drive)
        {
            Drive = drive;
        }

        public virtual XElement ToXml()
        {
            var ret = new XElement(IsFolder ? "driveFolder" : "driveFile");
            ret.SetAttributeValue("id", Id);
            ret.SetAttributeValue("isFolder", IsFolder);
            ret.SetAttributeValue("storageFileId", StorageFileId);
            return ret;
        }
    }
}
