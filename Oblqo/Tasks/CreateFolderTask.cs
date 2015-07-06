using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public class CreateFolderTask : AsyncTask
    {
        public string FolderName { get; set; }
        public AccountFile DestFolder { get; private set; }
        public AccountFile CreatedFolder { get; set; }

        public CreateFolderTask(Account account, string accountName, int priority, AsyncTask[] parent, string folderName, AccountFile destFolder) 
            : base(account, accountName, priority, parent)
        {
            FolderName = folderName;
            DestFolder = destFolder;
        }

        protected override async Task OnStartAsync()
        {
            var destFolder = DestFolder;
            if (destFolder == null && Common.IsSingle(Parents) && Parents[0] is CreateFolderTask)
                destFolder = ((CreateFolderTask)Parents[0]).CreatedFolder;
            CreatedFolder = await Account.CreateFolderAsync(FolderName, destFolder, CancellationTokenSource.Token);
        }
    }
}
