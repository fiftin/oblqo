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

        public IList<DriveFile> Files => files;

        public DriveFileCollection(DriveCollection drive)
        {
            Drive = drive;
        }

        public DriveFileCollection(DriveCollection drive, IEnumerable<DriveFile> f)
        {
            Drive = drive;
            files.AddRange(f);
        }

        private DriveFile First => files[0];

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

        //public async Task ScaleImageAsync()
        //{
        //    await Task.WhenAll(files.Select(drive => drive.ScaleImageAsync()));
        //}

        public async Task SetStorageFileIdAsync(string value, CancellationToken token)
        {
            await Task.WhenAll(files.Select(drive => drive.SetStorageFileIdAsync(value, token)));
        }

        public IEnumerator<DriveFile> GetEnumerator() => files.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}