using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DownloadFileFromDriveTask : DownloadFileTask
    {
        public DownloadFileFromDriveTask(Account account, string accountName, int priority, AsyncTask parent, AccountFile file, string destFolder) 
            : base(account, accountName, priority, parent, file, destFolder)
        {
        }

        protected override async Task StartAsync2()
        {
            await Account.DownloadFileFromDriveAsync(File, DestFolder,
                ActionIfFileExists.Rewrite, CancellationTokenSource.Token);
        }
    }
}
