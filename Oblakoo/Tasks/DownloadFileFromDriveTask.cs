using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DownloadFileFromDriveTask : DownloadFileTask
    {
        public DriveFile File { get; private set; }

        public DownloadFileFromDriveTask(Account account, string accountName, int priority, AsyncTask[] parent, DriveFile file, string destFolder) 
            : base(account, accountName, priority, parent, destFolder)
        {
            File = file;
        }

        protected override async Task StartAsync2()
        {
            await Account.Drive.DownloadFileAsync(File, DestFolder,
                ActionIfFileExists.Rewrite, CancellationTokenSource.Token);
        }
    }
}
