using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Oblqo;

namespace OblqoTest
{
    public class MockDriveFile : DriveFile
    {
        internal List<MockDriveFile> files = new List<MockDriveFile>();

        private Dictionary<string, string> attrs = new Dictionary<string, string>();

        internal byte[] content = new byte[0];

        internal MockDriveFile Add(MockDriveFile file)
        {
            files.Add(file);
            return file;
        }
        
        public MockDriveFile(Drive drive,
            string name,
            bool isFolder = false,
            bool isImage = false,
            bool isRoot = false,
            string mimeType = "",
            int imageHeight = 0,
            int imageWidth = 0,
            int originalImageHeight = 0,
            int originalImageWidth = 0,
            long originalSize = 0,
            DateTime createdDate = new DateTime(),
            DateTime modifiedDate = new DateTime()
            ) : base(drive)
        {
            Id = name;
            Name = name;
            IsFolder = isFolder;
            IsImage = isImage;
            IsRoot = isRoot;
            MimeType = mimeType;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            ImageHeight = imageHeight;
            ImageWidth = imageWidth;
            OriginalImageHeight = originalImageHeight;
            OriginalImageWidth = originalImageWidth;
            OriginalSize = originalSize;
        }

        internal bool DeleteFileRecursive(DriveFile driveFile)
        {
            var y = files.FirstOrDefault(x => x == driveFile);
            if (y != null)
            {
                return files.Remove(y);
            }
            else
            {
                foreach (var x in files.Where(x=>x.IsFolder && x.files.Count > 0))
                {
                    if (x.DeleteFileRecursive(driveFile))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public override DateTime CreatedDate { get; }

        public override bool HasChildren => files.Count(x => x.IsFolder) > 0;

        public override string Id { get; }

        public override int ImageHeight { get; }

        public override int ImageWidth { get; }

        public override bool IsFolder { get; }

        public override bool IsImage { get; }

        public override bool IsRoot { get; }

        public override string MimeType { get; }

        public override DateTime ModifiedDate { get; }

        public override string Name { get; }

        public override int OriginalImageHeight { get; set; }

        public override int OriginalImageWidth { get; set; }

        public override long OriginalSize { get; set; }

        public override long Size => content.LongLength;

        internal DriveFile CreateFolder(string folderName)
        {
            var ret = new MockDriveFile(Drive, folderName, isFolder: true);
            files.Add(ret);
            return ret;
        }

        public override string GetAttribute(string name)
        {
            string ret;
            attrs.TryGetValue(name, out ret);
            return ret;
        }

        public override async Task SetAttributeAsync(string name, string value, CancellationToken token)
        {
            attrs[name] = value;
        }

        public override Task WriteAsync(byte[] bytes, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
