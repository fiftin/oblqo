using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    [AccountFileStateChange(AccountFileStates.SyncronizedWithStorage, "SourceFile")]
    public class SynchronizeStorageFileTask : AsyncTask
    {
        public AccountFile SourceFile {  get; private set; }

        public SynchronizeStorageFileTask()
        {

        }
        public SynchronizeStorageFileTask(Account account, string accountName, int priority, AsyncTask[] parent, AccountFile sourceFile)
            : base(account, accountName, priority, parent)
        {
            SourceFile = sourceFile;
        }

        protected override async Task OnStartAsync()
        {
            if (SourceFile.StorageFileId != null)
            {
                return;
            }
            StorageFile storageFile;
            using (var inStream = await SourceFile.DriveFiles.ReadFileAsync(CancellationTokenSource.Token))
            {
                Stream stream;
                if (!inStream.CanSeek) // Amazon Glicer required SetPosition. Read file to memory, if inStream is not support it.
                {
                    var memStream = new MemoryStream();
                    await Common.CopyStreamAsync(inStream, memStream);
                    memStream.Seek(0, SeekOrigin.Begin);
                    stream = memStream;
                }
                else
                {
                    stream = inStream;
                }
                storageFile = await SourceFile.Storage.UploadFileAsync(
                    stream,
                    SourceFile.Name,
                    SourceFile.Parent.StorageFile,
                    CancellationTokenSource.Token,
                    e => OnProgress(new AsyncTaskProgressEventArgs(e.PercentDone, null)));
            }
            await SourceFile.DriveFiles.SetStorageFileIdAsync(storageFile.Id, CancellationTokenSource.Token);
            SourceFile.DriveFiles.OriginalImageHeight = SourceFile.ImageHeight;
            SourceFile.DriveFiles.OriginalImageWidth = SourceFile.ImageWidth;
            SourceFile.DriveFiles.OriginalSize = SourceFile.Size;
            if (SourceFile.IsImage)
            {
                //TODO: Uncomment
                //await SourceFile.DriveFile.ScaleImageAsync(CancellationTokenSource.Token);
            }
        }



        public override async Task LoadAsync(Account account, string id, System.Xml.Linq.XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            SourceFile = await account.GetFileAsync(xml.Element("sourceFile"), token);
        }

        public override System.Xml.Linq.XElement ToXml()
        {
            var xml = base.ToXml();
            var sourceFileXml = SourceFile.ToXml("sourceFile");
            xml.Add();
            return xml;
        }
    }
}
