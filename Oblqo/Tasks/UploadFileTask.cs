﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo.Tasks
{
    [AccountFileStateChange(AccountFileStates.New, "UploadedFile")]
    public class UploadFileTask : AsyncTask
    {
        public string FileName { get; private set; }
        public AccountFile DestFolder { get; private set; }
        public AccountFile UploadedFile { get; private set; }
        public UploadFileTask() { }
        public UploadFileTask(Account account, string accountName, int priority, AsyncTask[] parent, string fileName, AccountFile destFolder)
            : base(account, accountName, priority, parent)
        {
            FileName = fileName;
            DestFolder = destFolder;
        }

        protected override async Task OnStartAsync()
        {
            var destFolder = DestFolder;
            if (destFolder == null && Common.IsSingle(Parents) && Parents[0] is CreateFolderTask)
                destFolder = ((CreateFolderTask)Parents[0]).CreatedFolder;

            // sync dest folder on all drives
            var tasks = (from drive in Account.Drives
                         where DestFolder.GetDriveFile(drive) == null
                         select DestFolder.GetFileAndCreateIfFolderIsNotExistsAsync(drive, CancellationTokenSource.Token)
                         ).Cast<Task>().ToList();
            await Task.WhenAll(tasks);

            UploadedFile = await
                Account.UploadFileAsync(FileName, destFolder, CancellationTokenSource.Token,
                    e => OnProgress(new AsyncTaskProgressEventArgs(e.PercentDone, null)));
        }

        public override async Task LoadAsync(Account account, string id, XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            FileName = xml.Attribute("fileName").Value;
            DestFolder = await account.GetFileAsync(xml.Element("destFolder"), token);
        }

        public override XElement ToXml()
        {
            var xml = base.ToXml();
            xml.SetAttributeValue("fileName", FileName);
            xml.Add(DestFolder.ToXml("destFolder"));
            return xml;
        }

    }
}
