using Oblqo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Xml.Linq;

namespace OblqoTest
{
    public class MockStorage : Storage
    {
        StorageFile rootFolder;

        Dictionary<string, MockStorageFile> files = new Dictionary<string, MockStorageFile>();
        

        public MockStorage(string id)
        {
        }

        public override string Id { get; }

        public override bool IsSupportFolders => false;

        public override string Kind => "mock";

        public override StorageFile RootFolder => rootFolder;

        public override Task ClearAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async override Task<StorageFile> CreateFolderAsync(string folderName, StorageFile destFolder, CancellationToken token)
        {
            var ret = new MockStorageFile(this, folderName);
            files.Add(ret.Id, ret);
            return ret;
        }

        public override Task DeleteFileAsync(StorageFile id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task DownloadFileAsync(StorageFile file, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            throw new NotImplementedException();
        }

        public override StorageFile GetFile(DriveFileCollection driveFile)
        {
            var id = driveFile.Select(x => x.StorageFileId).FirstOrDefault(x => x != null);
            if (id == null)
            {
                return null;
            }
            MockStorageFile ret;
            files.TryGetValue(id, out ret);
            return ret;
        }

        public override async Task<StorageFile> GetFileAsync(XElement xml, CancellationToken token)
        {
            var ret = new MockStorageFile(this, xml.Attribute("name").Value);
            return ret;
        }

        public override Task<StorageFile> UploadFileAsync(string pathName, StorageFile destFolder, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            throw new NotImplementedException();
        }

        public override Task<StorageFile> UploadFileAsync(Stream stream, string fileName, StorageFile destFolder, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            throw new NotImplementedException();
        }

        public void AddFile(MockStorageFile file)
        {
            files.Add(file.Id, file);
        }

        public async override Task DownloadFileAsync(StorageFile file, Stream stream, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            var f = (MockStorageFile)file;
            await stream.WriteAsync(f.content, 0, f.content.Length);
        }
    }
}
