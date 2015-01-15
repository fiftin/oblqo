using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public abstract class DownloadFileTask : AsyncTask
    {
        public string DestFolder { get; private set; }

        protected DownloadFileTask(Account account, string accountName, int priority, AsyncTask[] parent,
            string destFolder)
            : base(account, accountName, priority, parent)
        {
            DestFolder = destFolder;
        }
    }
}
