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


        protected LocalFile(LocalDrive drive, FileSystemInfo file, bool isRoot)
            : base(drive)
        {
            this.IsRoot = isRoot;
            this.file = file;
        }

        protected LocalFile(LocalDrive drive, string path, bool isRoot)
            : base(drive)
        {
            this.IsRoot = isRoot;
            if (System.IO.File.Exists(path))
            {
                this.file = new FileInfo(path);
            }
            else
            {
                this.file = new DirectoryInfo(path);
            }
        }

        public override DateTime CreatedDate => file.CreationTime;

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

        public override string Id => file.FullName;

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

        public override bool IsFolder => file is DirectoryInfo;

        public override bool IsImage => MimeType.StartsWith("image/");

        public override bool IsRoot { get; }

        public override string MimeType => MimeTypes.GetMimeTypeByExtension(file.Name);

        public override DateTime ModifiedDate => file.LastWriteTime;

        public override string Name => file.Name;

        public override long Size => ((FileInfo)file).Length;

        public override int OriginalImageWidth
        {
            get
            {
                int ret;
                return int.TryParse(GetAttribute("OriginalImageWidth"), out ret) ? ret : 0;
            }
            set
            {
                SetAttribute("OriginalImageWidth", value.ToString());
            }
        }

        public override int OriginalImageHeight
        {
            get
            {
                int ret;
                return int.TryParse(GetAttribute("OriginalImageHeight"), out ret) ? ret : 0;
            }
            set
            {
                SetAttribute("OriginalImageHeight", value.ToString());
            }
        }

        public override long OriginalSize
        {
            get
            {
                long ret;
                return long.TryParse(GetAttribute("OriginalSize"), out ret) ? ret : 0;
            }
            set
            {
                SetAttribute("OriginalSize", value.ToString());
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

        public override async Task WriteAsync(byte[] bytes) { }
    }
}
