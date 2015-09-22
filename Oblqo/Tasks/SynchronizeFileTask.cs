using System;
using System.Collections.Generic;
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
            var storageFile = await DestFolder.Storage.UploadFileAsync(await SourceFile.Drive.ReadFileAsync(SourceFile, CancellationTokenSource.Token),
                SourceFile.Name, DestFolder, CancellationTokenSource.Token, e => OnProgress(new AsyncTaskProgressEventArgs(e.PercentDone, null)));
            SourceFile.StorageFileId = storageFile.Id;
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
