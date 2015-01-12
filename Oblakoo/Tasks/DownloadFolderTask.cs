using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public abstract class DownloadFolderTask : AsyncTask
    {
        public string DestFolder { get; private set; }
        public bool OnlyContent { get; private set; }

        protected DownloadFolderTask(Account account, string accountName, int priority, AsyncTask parent,
            string destFolder, bool onlyContent)
            : base(account, accountName, priority, parent)
        {
            DestFolder = destFolder;
            OnlyContent = onlyContent;
        }

    }
}
