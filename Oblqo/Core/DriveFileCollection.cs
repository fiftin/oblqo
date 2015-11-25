using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public class DriveFileCollection : IDriveFile, IList<DriveFile>
    {
        private List<DriveFile> files = new List<DriveFile>();

        public DriveFileCollection()
        {
        }

        public DriveFileCollection(IEnumerable<DriveFile> files)
        {
            this.files.AddRange(files);
        }

        public int Count => files.Count;

        public DateTime CreatedDate => files.Select(x => x.CreatedDate).Max();

        public bool HasChildren => files.Exists(x => x.HasChildren == true);
        public bool IsFolder => files.Exists(x => x.IsFolder == true);
        public bool IsImage => files.Exists(x => x.IsImage == true);
        public bool IsRoot => files.Exists(x => x.IsRoot == true);

        public bool IsReadOnly => false;

        public string Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int ImageHeight => files.Select(x => x.ImageHeight).Max();

        public int ImageWidth => files.Select(x => x.ImageWidth).Max();

        public string MimeType => files.Select(x => x.MimeType).FirstOrDefault(x => x != null);

        public void AddRange(IEnumerable<DriveFile> driveFiles)
        {
            files.AddRange(driveFiles);
            if (files.Count > 2)
            {
                Console.WriteLine(files.Count);
            }
        }

        public DateTime ModifiedDate => files.Select(x => x.ModifiedDate).Max();

        public string Name => files.Select(x => x.Name).FirstOrDefault(x => x != null);
        public string StorageFileId => files.Select(x => x.StorageFileId).FirstOrDefault(x => x != null);
        public int OriginalImageHeight
        {
            get
            {
                return files.Select(x => x.OriginalImageHeight).Max();
            }
            set
            {

            }
        }
        public int OriginalImageWidth
        {
            get
            {
                return files.Select(x => x.OriginalImageWidth).Max();
            }
            set
            {

            }
        }
        public long OriginalSize
        {
            get
            {
                return files.Select(x => x.OriginalSize).Max();
            }
            set
            {

            }
        }
        public long Size => files.Select(x => x.Size).Max();

        public DriveFile this[int index]
        {
            get { return files[index]; }
            set { files[index] = value; }
        }

        public string GetAttribute(string name)
        {
            return files.Select(x => x.GetAttribute(name)).FirstOrDefault(x => x != null);
        }

        public string GetNewSource(List<string> sources)
        {
            throw new NotImplementedException();
        }

        public async Task SetAttributeAsync(string name, string value, CancellationToken token)
        {
            var tasks = files.Select(x => x.SetAttributeAsync(name, value, token));
            await Task.WhenAll(tasks);
        }

        public async Task SetStorageFileIdAsync(string value, CancellationToken token)
        {
            var tasks = files.Select(x => x.SetStorageFileIdAsync(value, token));
            await Task.WhenAll(tasks);
        }

        public XElement ToXml()
        {
            XElement ret = new XElement("driveFile");
            foreach (var x in files)
            {
                ret.Add(x.ToXml());
            }
            return ret;
        }

        public void Add(DriveFile item)
        {
            files.Add(item);
            if (files.Count > 2)
            {
                Console.WriteLine(files.Count);
            }
        }

        public void Clear()
        {
            files.Clear();
        }

        public bool Contains(DriveFile item)
        {
            return files.Contains(item);
        }

        public void CopyTo(DriveFile[] array, int arrayIndex)
        {
            files.CopyTo(array, arrayIndex);
        }

        public IEnumerator<DriveFile> GetEnumerator()
        {
            return files.GetEnumerator();
        }

        public bool Remove(DriveFile item)
        {
            return files.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return files.GetEnumerator();
        }

        public int IndexOf(DriveFile item)
        {
            return files.IndexOf(item);
        }

        public void Insert(int index, DriveFile item)
        {
            files.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            files.RemoveAt(index);
        }

        public async Task<Stream> ReadFileAsync(CancellationToken token)
        {
            return await files.First().ReadFileAsync(token);
        }
    }
}
