using System.IO;
using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DownloadFolderFromDriveTask : DownloadFolderTask
    {
        public DriveFile Folder { get; set; }

        public DownloadFolderFromDriveTask(Account account, string accountName, int priority, AsyncTask[] parent,
            DriveFile folder, string destFolder, bool onlyContent)
            : base(account, accountName, priority, parent, destFolder, onlyContent)
        {
            Folder = folder;
        }

        protected override async Task StartAsync2()
        {
            var folder = OnlyContent ? DestFolder : Common.AppendFolderToPath(DestFolder, Folder.Name);
            Directory.CreateDirectory(folder);
            await EnumerateFilesRecursiveAsync(Folder, folder);
        }

        protected override DownloadFileTask CreateDownloadFileTask(Account account, string accountName, int priority, AsyncTask[] parent,
            AccountFile file, string destFolder)
        {
            return new DownloadFileFromDriveTask(account, accountName, priority, parent, file.DriveFile, destFolder);
        }
    }
}
