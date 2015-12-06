using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public class DriveCollection : IList<Drive>, IDrive<DriveFileCollection>
    {

        private List<Drive> drives = new List<Drive>();
        
        public Drive this[int index]
        {
            get
            {
                return drives[index];
            }

            set
            {
                drives[index] = value;
            }
        }

        public int Count => drives.Count;

        public Size ImageMaxSize
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly => false;

        public void Add(Drive item)
        {
            drives.Add(item);
        }

        public void AddRange(IEnumerable<Drive> items)
        {
            drives.AddRange(items);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Drive item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Drive[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public async Task<DriveFileCollection> CreateFolderAsync(string folderName, 
            DriveFileCollection destFolder, CancellationToken token)
        {
            var tasks = destFolder.Select(x => x.Drive.CreateFolderAsync(folderName, x, token));
            var ret = new DriveFileCollection(await Task.WhenAll(tasks));

            return ret;
        }

        public async Task DeleteFileAsync(DriveFileCollection driveFile, CancellationToken token)
        {
            var tasks = driveFile.Select(x => x.Drive.DeleteFileAsync(x, token));
            await Task.WhenAll(tasks);
        }

        public async Task DeleteFolderAsync(DriveFileCollection driveFolder, CancellationToken token)
        {
            var tasks = driveFolder.Select(x => x.Drive.DeleteFolderAsync(x, token));
            await Task.WhenAll(tasks);
        }

        public async Task DownloadFileAsync(DriveFileCollection driveFile, string destFolder, 
            ActionIfFileExists actionIfFileExists, CancellationToken token)
        {
            var file = driveFile.First();
            await file.Drive.DownloadFileAsync(file, destFolder, actionIfFileExists, token);
        }

        public async Task EnumerateFilesRecursive(DriveFileCollection driveFolder, 
            Action<DriveFileCollection> action, CancellationToken token)
        {
            throw new NotImplementedException();
        }


        public async Task<DriveFileCollection> GetFileAsync(XElement xml, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<DriveFileCollection>> GetFilesAsync(DriveFileCollection folder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<Image> GetImageAsync(DriveFileCollection file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<DriveFileCollection>> GetSubfoldersAsync(DriveFileCollection folder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<Image> GetThumbnailAsync(DriveFileCollection file, CancellationToken token)
        {
            throw new NotImplementedException();
        }


        public async Task<Stream> ReadFileAsync(DriveFileCollection file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Image ScaleImage(Image image)
        {
            throw new NotImplementedException();
        }

        public async Task<Stream> ScaleImageAsync(Stream input, ImageFormat type)
        {
            throw new NotImplementedException();
        }

        public async Task<Stream> ScaleImageAsync(Image image, ImageFormat type, Stream defaultStream)
        {
            throw new NotImplementedException();
        }

        public async Task<DriveFileCollection> UploadFileAsync(string pathName, DriveFileCollection destFolder, string storageFileId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Drive> GetEnumerator()
        {
            return drives.GetEnumerator();
        }

        public int IndexOf(Drive item)
        {
            return drives.IndexOf(item);
        }

        public void Insert(int index, Drive item)
        {
            drives.Insert(index, item);
        }

        public bool Remove(Drive item)
        {
            return drives.Remove(item);
        }

        public void RemoveAt(int index)
        {
            drives.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return drives.GetEnumerator();
        }
    }
}
