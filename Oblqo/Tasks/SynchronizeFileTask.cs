using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public class SynchronizeFileTask : AsyncTask
    {
        public DriveFile SourceFile
        {
            get; private set;
        }
        public StorageFile DestFolder { get; private set; }

        public SynchronizeFileTask(Account account, string accountName, int priority, AsyncTask[] parent, DriveFile sourceFile, StorageFile destFolder)
            : base(account, accountName, priority, parent)
        {
            SourceFile = sourceFile;
            DestFolder = destFolder;
        }

        protected override async Task OnStartAsync()
        {
            var inStream = await SourceFile.Drive.ReadFileAsync(SourceFile, CancellationTokenSource.Token);

            var memStream = new MemoryStream();

            await Common.CopyStreamAsync(inStream, memStream);
            memStream.Seek(0, SeekOrigin.Begin);

            var storageFile = await DestFolder.Storage.UploadFileAsync(
                memStream,
                SourceFile.Name,
                DestFolder,
                CancellationTokenSource.Token,
                e => OnProgress(new AsyncTaskProgressEventArgs(e.PercentDone, null)));
            await SourceFile.SetStorageFileIdAsync(storageFile.Id, CancellationTokenSource.Token);
            SourceFile.OriginalImageHeight = SourceFile.ImageHeight;
            SourceFile.OriginalImageWidth = SourceFile.ImageWidth;
            SourceFile.OriginalSize = SourceFile.Size;
            if (SourceFile.IsImage)
            {
                await SourceFile.ScaleImageAsync();
            }
        }
    }
}
