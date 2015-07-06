using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public class UploadFileTask : AsyncTask
    {
        public string FileName { get; private set; }
        public AccountFile DestFolder { get; private set; }

        public UploadFileTask(Account account, string accountName, int priority, AsyncTask[] parent, string fileName, AccountFile destFolder)
            : base(account, accountName, priority, parent)
        {
            FileName = fileName;
            DestFolder = destFolder;
        }

        protected override async Task OnStartAsync()
        {
            var destFolder = DestFolder;
            if (destFolder == null && Common.IsSingle(Parents) && Parents[0] is CreateFolderTask)
                destFolder = ((CreateFolderTask)Parents[0]).CreatedFolder;
            await
                Account.UploadFileAsync(FileName, destFolder, CancellationTokenSource.Token,
                    e => OnProgress(new AsyncTaskProgressEventArgs(e.PercentDone, null)));
        }
    }
}
