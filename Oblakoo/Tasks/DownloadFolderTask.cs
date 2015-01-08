using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public abstract class DownloadFolderTask : AsyncTask
    {
        public AccountFile Folder { get; private set; }
        public string DestFolder { get; private set; }

        protected DownloadFolderTask(Account account, string accountName, int priority, AsyncTask parent, AccountFile folder, string destFolder) 
            : base(account, accountName, priority, parent)
        {
            Folder = folder;
            DestFolder = destFolder;
        }

    }
}
