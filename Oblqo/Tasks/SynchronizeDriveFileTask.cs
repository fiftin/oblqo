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

        public SynchronizeDriveFileTask(DriveFileCollection file, DriveFileCollection folder)
        {
            File = file;
            DestFolder = folder;
        }

        protected override async Task OnStartAsync()
        {
            var bestFile = GetBestFile();
            var dr = File.Drive;
            var tasks = new List<Task>();
            foreach (var drive in dr)
            {
                if (File.GetFile(drive) != null) continue;
                tasks.Add(drive.UploadFileAsync(null, DestFolder.GetFile(drive), bestFile.StorageFileId, CancellationTokenSource.Token));
            }
            await Task.WhenAll(tasks);
        }
        
        private DriveFile GetBestFile()
        {
            return File.First();
        }
    }
}
