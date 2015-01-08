using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DownloadFolderFromDriveTask : DownloadFolderTask
    {
        public DownloadFolderFromDriveTask(Account account, string accountName, int priority, AsyncTask parent,
            AccountFile folder, string destFolder)
            : base(account, accountName, priority, parent, folder, destFolder)
        {
        }

        protected override Task StartAsync2()
        {
            throw new System.NotImplementedException();
        }
    }
}
