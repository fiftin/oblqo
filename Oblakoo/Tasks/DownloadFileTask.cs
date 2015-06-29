using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public abstract class DownloadFileTask : AsyncTask
    {
        public string DestFolder { get; private set; }
        public AccountFile File { get; private set; }

        protected DownloadFileTask(Account account, string accountName, int priority, AsyncTask[] parent,
            string destFolder, AccountFile file)
            : base(account, accountName, priority, parent)
        {
            DestFolder = destFolder;
            File = file;
        }
    }
}
