using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oblakoo
{
    public class MockStorage : Storage
    {
        public override Task DeleteFile(StorageFile id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override StorageFile GetFile(DriveFile driveFile)
        {
            throw new NotImplementedException();
        }

        public override Task<StorageFile> UploadFileAsync(string pathName, StorageFile destFolder, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            throw new NotImplementedException();
        }


        public override Task DownloadFileAsync(StorageFile file, string destFolder, ActionIfFileExists actionIfFileExists,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<StorageFile> CreateFolderAsync(string folderName, StorageFile destFolder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override StorageFile RootFolder
        {
            get { throw new NotImplementedException(); }
        }
    }
}
