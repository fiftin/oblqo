using System.IO;
using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public abstract class DownloadFolderTask : AsyncTask
    {
        public string DestFolder { get; private set; }
        public bool OnlyContent { get; private set; }

        protected DownloadFolderTask(Account account, string accountName, int priority, AsyncTask[] parent,
            string destFolder, bool onlyContent)
            : base(account, accountName, priority, parent)
        {
            DestFolder = destFolder;
            OnlyContent = onlyContent;
        }

        protected abstract DownloadFileTask CreateDownloadFileTask(Account account, string accountName, int priority, AsyncTask[] parent, AccountFile file, string destFolder);

        protected async Task EnumerateFilesRecursiveAsync(DriveFile folder, string dest)
        {
            var files = await Account.Drive.GetFilesAsync(folder, CancellationTokenSource.Token);
            foreach (var f in files)
                AddTask(CreateDownloadFileTask(Account, AccountName, 0, null, new AccountFile(Account.Storage.GetFile(f), f), dest));
            var dirs = await Account.Drive.GetSubfoldersAsync(folder, CancellationTokenSource.Token);
            foreach (var d in dirs)
            {
                var path = Common.AppendFolderToPath(dest, d.Name);
                Directory.CreateDirectory(path);
                await EnumerateFilesRecursiveAsync(d, path);
            }
        }


    }
}
