using System.IO;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public class DownloadFolderFromDriveTask : DownloadFolderTask
    {
        public DownloadFolderFromDriveTask(Account account, string accountName, int priority, AsyncTask[] parent,
            AccountFile folder, string destFolder)
            : base(account, accountName, priority, parent, destFolder, folder)
        {
        }

        protected override async Task OnStartAsync()
        {
            var folder = Common.AppendToPath(DestFolder, Folder.Name);
            Directory.CreateDirectory(folder);
            await EnumerateFilesRecursiveAsync(Folder, folder);
        }

        protected override DownloadFileTask CreateDownloadFileTask(Account account, string accountName, int priority, AsyncTask[] parent,
            AccountFile file, string destFolder)
        {
            return new DownloadFileFromDriveTask(account, accountName, priority, parent, file, destFolder);
        }
    }
}
