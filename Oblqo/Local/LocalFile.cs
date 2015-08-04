using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo.Local
{
    public class LocalFile : DriveFile
    {
        internal FileSystemInfo file;
        private bool isRoot;


        public LocalFile(LocalDrive drive, FileSystemInfo file, bool isRoot)
            : base(drive)
        {
            this.isRoot = isRoot;
            this.file = file;
        }

        public LocalFile(LocalDrive drive, string path, bool isRoot)
            : base(drive)
        {
            this.isRoot = isRoot;
            if (File.Exists(path))
            {
                this.file = new FileInfo(path);
            }
            else
            {
                this.file = new DirectoryInfo(path);
            }
        }

        public override DateTime CreatedDate
        {
            get
            {
                return file.CreationTime;
            }
        }

        public override bool HasChildren
        {
            get
            {
                if (!IsFolder)
                {
                    return false;
                }
                var dir = (DirectoryInfo)file;
                return dir.EnumerateDirectories().Any();
            }
        }

        public override string Id
        {
            get
            {
                return file.FullName;
            }
        }

        public override int ImageHeight
        {
            get
            {
                if (!IsImage)
                {
                    return 0;
                }
                return 0;
            }
        }

        public override int ImageWidth
        {
            get
            {
                if (!IsImage)
                {
                    return 0;
                }
                return 0;
            }
        }

        public override bool IsFolder
        {
            get
            {
                return file is DirectoryInfo;
            }
        }

        public override bool IsImage
        {
            get
            {
                return MimeType.StartsWith("image/");
            }
        }

        public override bool IsRoot
        {
            get
            {
                return isRoot;
            }
        }

        public override string MimeType
        {
            get
            {
                return MimeTypes.GetMimeTypeByExtension(file.Name);
            }
        }

        public override DateTime ModifiedDate
        {
            get
            {
                return file.LastWriteTime;
            }
        }

        public override string Name
        {
            get
            {
                return file.Name;
            }
        }

        public override int OriginalImageHeight
        {
            get
            {
                return 0;
            }
        }

        public override int OriginalImageWidth
        {
            get
            {
                return 0;
            }
        }

        public override long OriginalSize
        {
            get
            {
                return 0;
            }
        }

        public override long Size
        {
            get
            {
                return ((FileInfo)file).Length;
            }
        }

        public override string StorageFileId
        {
            get
            {
                return null;
            }
        }
    }
}
