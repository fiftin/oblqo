using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DeleteFolderTask : AsyncTask
    {
        public AccountFile Folder { get; private set; }

        public DeleteFolderTask(Account account, string accountName, int priority, AsyncTask parent, AccountFile folder) 
            : base(account, accountName, priority, parent)
        {
            Folder = folder;
        }

        protected override async Task StartAsync2()
        {
            await Account.DeleteFolderAsync(Folder, CancellationTokenSource.Token);
        }
    }
}
