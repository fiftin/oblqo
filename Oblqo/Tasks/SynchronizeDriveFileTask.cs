using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public class SynchronizeDriveFileTask : AsyncTask
    {
        public DriveFileCollection File { get; set; }
        public DriveFileCollection DestFolder { get; set; }

        public SynchronizeDriveFileTask()
        {

        }

        public SynchronizeDriveFileTask(Account account, string accountName, int priority, AsyncTask[] parent, DriveFileCollection file, DriveFileCollection folder)
            : base(account, accountName, priority, parent)
        {
            File = file;
            DestFolder = folder;
        }

        protected override async Task OnStartAsync()
        {
            var bestFile = GetBestFile();
            var dr = File.Drive;
            var stream = await bestFile.Drive.ReadFileAsync(bestFile, CancellationTokenSource.Token);
            var tasks = (from drive in dr
                         where File.GetFile(drive) == null
                         select drive.UploadFileAsync(stream, File.Name, DestFolder.GetFile(drive), bestFile.StorageFileId != null, bestFile.StorageFileId, CancellationTokenSource.Token)
                         ).Cast<Task>().ToList();
            await Task.WhenAll(tasks);
        }
        
        private DriveFile GetBestFile()
        {
            return File.First();
        }
    }
}
