using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public class DriveFileCollection
    {
        public string Id
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsImage
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsFolder
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasChildren
        {
            get { throw new NotImplementedException(); }
        }

        public long Size
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime ModifiedDate
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime CreatedDate
        {
            get { throw new NotImplementedException(); }
        }

        public int OriginalImageWidth
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int OriginalImageHeight
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public long OriginalSize
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int ImageWidth
        {
            get { throw new NotImplementedException(); }
        }

        public int ImageHeight
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsRoot
        {
            get { throw new NotImplementedException(); }
        }

        public string MimeType
        {
            get { throw new NotImplementedException(); }
        }

        public DriveCollection Drive
        {
            get { throw new NotImplementedException(); }
        }

        public string StorageFileId
        {
            get { throw new NotImplementedException(); }
        }

        public XElement ToXml()
        {
            throw new NotImplementedException();
        }

        public Task ScaleImageAsync()
        {
            throw new NotImplementedException();
        }

        public string GetAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public Task SetAttributeAsync(string name, string value, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public string GetNewSource(List<string> sources)
        {
            throw new NotImplementedException();
        }

        public Task SetStorageFileIdAsync(string value, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}