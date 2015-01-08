using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oblakoo;

namespace OblakooTest
{
    class MockDriveFile : DriveFile
    {
        internal string id;
        internal string name;
        internal bool isDirecotry;
        internal bool isImage;
        internal List<DriveFile> children = new List<DriveFile>();
        internal string content;

        public MockDriveFile()
        {
            
        }

        public MockDriveFile(string id, string name)
        {
            this.id = id;
            this.name = name;
            isDirecotry = true;
        }

        public MockDriveFile(string id, string name, string content)
        {
            this.id = id;
            this.name = name;
            this.content = content;
        }


        public override string Id
        {
            get { return id; }
        }

        public override bool IsImage
        {
            get { return isImage; }
        }

        public override bool IsFolder
        {
            get { return isDirecotry; }
        }

        public override string Name
        {
            get { return name; }
        }

        public override bool HasChildren
        {
            get { return children.Count == 0; }
        }

        public override string StorageFileId
        {
            get { return ""; }
            set { }
        }

        public override long Size
        {
            get { throw new NotImplementedException(); }
        }

        public override DateTime ModifiedDate
        {
            get { throw new NotImplementedException(); }
        }

        public override DateTime CreatedDate
        {
            get { throw new NotImplementedException(); }
        }

        public override int OriginalImageWidth
        {
            get { throw new NotImplementedException(); }
        }

        public override int OriginalImageHeight
        {
            get { throw new NotImplementedException(); }
        }

        public override long OriginalSize
        {
            get { throw new NotImplementedException(); }
        }

        public override int ImageWidth
        {
            get { throw new NotImplementedException(); }
        }

        public override int ImageHeight
        {
            get { throw new NotImplementedException(); }
        }

        public Stream GetContentStream()
        {
            return new MemoryStream(Encoding.Unicode.GetBytes(content));
        }
    }
}
