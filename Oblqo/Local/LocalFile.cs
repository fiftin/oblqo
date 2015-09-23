using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo.Local
{
    public abstract class LocalFile : DriveFile
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
            if (System.IO.File.Exists(path))
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
        
        public override long Size
        {
            get
            {
                return ((FileInfo)file).Length;
            }
        }

        public override int OriginalImageWidth
        {
            get
            {
                int ret;
                if (int.TryParse(GetAttribute(nameof(OriginalImageWidth)), out ret))
                {
                    return ret;
                }
                return 0;
            }
            set
            {
                SetAttribute(nameof(OriginalImageWidth), value.ToString());
            }
        }

        public override int OriginalImageHeight
        {
            get
            {
                int ret;
                if (int.TryParse(GetAttribute(nameof(OriginalImageHeight)), out ret))
                {
                    return ret;
                }
                return 0;
            }
            set
            {
                SetAttribute(nameof(OriginalImageHeight), value.ToString());
            }
        }

        public override long OriginalSize
        {
            get
            {
                long ret;
                if (long.TryParse(GetAttribute(nameof(OriginalSize)), out ret))
                {
                    return ret;
                }
                return 0;
            }
            set
            {
                SetAttribute(nameof(OriginalSize), value.ToString());
            }
        }

        public FileInfo File => (FileInfo)file;

        public DirectoryInfo Directory => (DirectoryInfo)file;

        public override System.Xml.Linq.XElement ToXml()
        {
            var ret = base.ToXml();
            ret.SetAttributeValue("fileId", file.FullName);
            return ret;
        }

        protected abstract void SetAttribute(string name, string value);
    }
}
