using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo.Tasks
{
    public class SynchronizeDriveFileTask : AsyncTask
    {
        public AccountFile File { get; set; }
        
        public SynchronizeDriveFileTask()
        {
        }

        public SynchronizeDriveFileTask(Account account, string accountName, int priority, AsyncTask[] parent, AccountFile file)
            : base(account, accountName, priority, parent)
        {
            File = file;
        }

        public async Task<DriveFile> UploadFileAsync(Drive drive)
        {
            var token = CancellationTokenSource.Token;
            var bestFile = GetBestFile();
            var stream = await bestFile.Drive.ReadFileAsync(bestFile, token);
            var folder = await File.Parent.GetFileAndCreateIfFolderIsNotExistsAsync(drive, token);
            return await drive.UploadFileAsync(stream, File.Name, folder, bestFile.StorageFileId != null, bestFile.StorageFileId, token);
        }

        protected override async Task OnStartAsync()
        {
            var tasks = (from drive in Account.Drives
                         where File.GetDriveFile(drive) == null
                         select UploadFileAsync(drive)
                         ).Cast<Task>().ToList();
            await Task.WhenAll(tasks);
        }
        
        private DriveFile GetBestFile()
        {
            return File.DriveFiles.First();
        }

        public override XElement ToXml()
        {
            var xml = base.ToXml();
            var fileXml = File.ToXml("file");
            xml.Add(fileXml);
            return xml;
        }

        public override async Task LoadAsync(Account account, string id, XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            File = await account.GetFileAsync(xml.Element("file"), token);
        }
    }
}
