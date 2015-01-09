using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class CreateFolderTask : AsyncTask
    {
        public string FolderName { get; set; }
        public AccountFile DestFolder { get; private set; }
        public AccountFile CreatedFolder { get; set; }

        public CreateFolderTask(Account account, string accountName, int priority, AsyncTask parent, string folderName, AccountFile destFolder) 
            : base(account, accountName, priority, parent)
        {
            FolderName = folderName;
            DestFolder = destFolder;
        }

        protected override async Task StartAsync2()
        {
            var destFolder = DestFolder;
            if (destFolder == null && Parent is CreateFolderTask)
                destFolder = ((CreateFolderTask)Parent).CreatedFolder;
            CreatedFolder = await Account.CreateFolderAsync(FolderName, destFolder, CancellationTokenSource.Token);
            if (State == AsyncTaskState.Running)
                State = AsyncTaskState.Finished;
        }
    }
}
