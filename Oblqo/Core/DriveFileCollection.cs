using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public class DriveFileCollection : IEnumerable<DriveFile>
    {
        private readonly List<DriveFile> files = new List<DriveFile>();

        public DriveCollection Drive { get; private set; }

        public DriveFileCollection Parent => Owner.DriveFile;

        public AccountFile Owner { get; internal set; }

        public IList<DriveFile> Files => files;

        internal DriveFileCollection(DriveCollection drive)
        {
            Drive = drive;
        }

        public DriveFileCollection(DriveCollection drive, AccountFile owner)
        {
            Drive = drive;
            Owner = owner;
        }

        public DriveFileCollection(DriveCollection drive, IEnumerable<DriveFile> f, AccountFile owner)
        {
            Drive = drive;
            foreach (var x in f)
            {
                x.Owner = owner.DriveFile;
                files.Add(x);
            }
            Owner = owner;
        }

        private DriveFile First => files[0];

        public async Task<DriveFile> GetFileAndCreateIfFolderIsNotExistsAsync(Drive drive, CancellationToken token)
        {
            var stack = new Stack<DriveFileCollection>();
            var curr = this;
            var driveFile = curr.files.SingleOrDefault(file => file.Drive == drive);
            while (driveFile == null)
            {
                stack.Push(curr);
                if (curr.Parent == null)
                {
                    break;
                }
                curr = curr.Parent;
                driveFile = curr.files.SingleOrDefault(file => file.Drive == drive);
            }
            while (stack.Count > 0)
            {
                var f = stack.Pop();
                driveFile = await drive.CreateFolderAsync(f.Name, driveFile, token);
            }
            return driveFile;
        }

        public DriveFile GetFile(Drive drive)
        {
            return files.SingleOrDefault(file => file.Drive == drive);
        }

        public void Add(DriveFile file)
        {
            files.Add(file);
        }


        public bool IsImage => First.IsImage;

        public bool IsFolder => First.IsFolder;

        public string Name => First.Name;

        public bool HasChildren => First.HasChildren;

        public long Size => First.Size;

        public DateTime ModifiedDate => First.ModifiedDate;

        public DateTime CreatedDate => First.CreatedDate;

        public int OriginalImageWidth
        {
            get { return First.OriginalImageWidth; }
            set
            {
                //TODO: OriginalImageWidth setter
            }
        }

        public int OriginalImageHeight
        {
            get { return First.OriginalImageHeight; }
            set
            {
                //TODO: OriginalImageHeight setter
            }
        }

        public long OriginalSize
        {
            get { return First.OriginalSize; }
            set
            {
                //TODO: OriginalSize setter
            }
        }

        public int ImageWidth => First.ImageWidth;
        public int ImageHeight => First.ImageHeight;

        public bool IsRoot => First.IsRoot;

        public string MimeType => First.MimeType;

        public string StorageFileId => First.StorageFileId;

        public XElement ToXml()
        {
            var root = new XElement("file");
            foreach (var file in Files)
            {
                root.Add(file.ToXml());
            }
            return root;
        }

        public async Task ScaleImageAsync(CancellationToken token)
        {
            await Task.WhenAll(files.Select(file => file.ScaleImageAsync(token)));
        }

        public async Task SetStorageFileIdAsync(string value, CancellationToken token)
        {
            await Task.WhenAll(files.Select(drive => drive.SetStorageFileIdAsync(value, token)));
        }

        public IEnumerator<DriveFile> GetEnumerator() => files.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}