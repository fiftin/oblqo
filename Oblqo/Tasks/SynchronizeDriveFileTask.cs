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


        public async Task<DriveFile> UploadFile(Drive drive)
        {
            var token = CancellationTokenSource.Token;
            var bestFile = GetBestFile();
            var stream = await bestFile.Drive.ReadFileAsync(bestFile, token);
            var folder = await DestFolder.GetFileAndCreateIfFolderIsNotExistsAsync(drive, token);
            return await drive.UploadFileAsync(stream, File.Name, folder, bestFile.StorageFileId != null, bestFile.StorageFileId, token);
        }

        protected override async Task OnStartAsync()
        {
            var dr = File.Drive;
            var tasks = (from drive in dr
                         where File.GetFile(drive) == null
                         select UploadFile(drive)
                         ).Cast<Task>().ToList();
            await Task.WhenAll(tasks);
        }
        
        private DriveFile GetBestFile()
        {
            return File.First();
        }
    }
}
