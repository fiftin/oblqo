using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public class DeleteEmptyFolderTask : DeleteFolderTaskBase
    {
        public DeleteEmptyFolderTask() { }

        public override bool Visible => false;

        public DeleteEmptyFolderTask(Account account, string accountName, int priority, AsyncTask[] parents, AccountFile folder) 
            : base(account, accountName, priority, parents, folder)
        {
        }

        protected override async Task OnStartAsync()
        {
            await Account.DeleteFolderAsync(Folder, CancellationTokenSource.Token);
        }
    }
}
