using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DownloadFileFromStorageTask : DownloadFileTask
    {
        public DownloadFileFromStorageTask(Account account, string accountName, int priority, AsyncTask parent, AccountFile file,
            string destFolder)
            : base(account, accountName, priority, parent, file, destFolder)
        {
        }

        protected override async Task StartAsync2()
        {
            await Account.DownloadFileFromStorageAsync(File, DestFolder,
                ActionIfFileExists.Rewrite, CancellationTokenSource.Token);
        }
    }
}
