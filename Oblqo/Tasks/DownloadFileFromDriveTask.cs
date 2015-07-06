using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DownloadFileFromDriveTask : DownloadFileTask
    {
        public DownloadFileFromDriveTask(Account account, string accountName, int priority, AsyncTask[] parent, AccountFile file, string destFolder) 
            : base(account, accountName, priority, parent, destFolder, file)
        {
        }

        protected override async Task OnStartAsync()
        {
            await Account.Drive.DownloadFileAsync(File.DriveFile, DestFolder,
                ActionIfFileExists.Rewrite, CancellationTokenSource.Token);
        }
    }
}
