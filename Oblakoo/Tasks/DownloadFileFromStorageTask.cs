using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DownloadFileFromStorageTask : DownloadFileTask
    {
        public AccountFile File { get; private set; }

        public DownloadFileFromStorageTask(Account account, string accountName, int priority, AsyncTask[] parent, AccountFile file,
            string destFolder)
            : base(account, accountName, priority, parent, destFolder)
        {
            File = file;
        }

        protected override async Task StartAsync2()
        {
            await Account.DownloadFileFromStorageAsync(File, DestFolder,
                ActionIfFileExists.Rewrite, CancellationTokenSource.Token,
                x => OnProgress(new AsyncTaskProgressEventArgs(x.PercentDone, null)));
        }
    }
}
