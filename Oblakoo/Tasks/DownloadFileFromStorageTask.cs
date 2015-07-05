using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DownloadFileFromStorageTask : DownloadFileTask
    {
        public DownloadFileFromStorageTask()
        {

        }

        public DownloadFileFromStorageTask(Account account, string accountName, int priority, AsyncTask[] parent, AccountFile file,
            string destFolder)
            : base(account, accountName, priority, parent, destFolder, file)
        {
        }

        protected override async Task OnStartAsync()
        {
            await Account.DownloadFileFromStorageAsync(File, DestFolder,
                ActionIfFileExists.Rewrite, CancellationTokenSource.Token,
                x => OnProgress(new AsyncTaskProgressEventArgs(x.PercentDone, null)));
        }
    }
}
