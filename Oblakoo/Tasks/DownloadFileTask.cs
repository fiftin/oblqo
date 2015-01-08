using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public abstract class DownloadFileTask : AsyncTask
    {
        public AccountFile File { get; private set; }
        public string DestFolder { get; private set; }

        protected DownloadFileTask(Account account, string accountName, int priority, AsyncTask parent, AccountFile file,
            string destFolder)
            : base(account, accountName, priority, parent)
        {
            File = file;
            DestFolder = destFolder;
        }
    }
}
