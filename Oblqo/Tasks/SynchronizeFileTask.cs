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
        public DriveFileCollection SourceFile {  get; private set; }
        public StorageFile DestFolder { get; private set; }

        public SynchronizeFileTask()
        {

        }
        public SynchronizeFileTask(Account account, string accountName, int priority, AsyncTask[] parent, DriveFileCollection sourceFile, StorageFile destFolder)
            : base(account, accountName, priority, parent)
        {
            SourceFile = sourceFile;
            DestFolder = destFolder;
        }

        protected override async Task OnStartAsync()
        {
            if (SourceFile.StorageFileId != null)
            {
                return;
            }
            Stream inStream = await SourceFile.Drive.ReadFileAsync(SourceFile, CancellationTokenSource.Token);
            if (!inStream.CanSeek) // Amazon Glicer required SetPosition. Read file to memory, if inStream is not support it.
            {
                var memStream = new MemoryStream();
                await Common.CopyStreamAsync(inStream, memStream);
                memStream.Seek(0, SeekOrigin.Begin);
                inStream = memStream;
            }
            var storageFile = await DestFolder.Storage.UploadFileAsync(
                inStream,
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



        public override async Task LoadAsync(Account account, string id, System.Xml.Linq.XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            DestFolder = await account.Storage.GetFileAsync(xml.Element("storageFolder"), token);
            SourceFile = await account.Drive.GetFileAsync(xml.Element("driveFile"), token);
        }

        public override System.Xml.Linq.XElement ToXml()
        {
            var xml = base.ToXml();
            xml.Add(SourceFile.ToXml());
            xml.Add(DestFolder.ToXml());
            return xml;
        }
    }
}
