using System.IO;
using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DownloadFolderFromDriveTask : DownloadFolderTask
    {
        public DriveFile Folder { get; set; }

        public DownloadFolderFromDriveTask(Account account, string accountName, int priority, AsyncTask parent,
            DriveFile folder, string destFolder, bool onlyContent)
            : base(account, accountName, priority, parent, destFolder, onlyContent)
        {
            Folder = folder;
        }

        private async Task EnumerateFilesRecursiveAsync(DriveFile folder, string dest)
        {
            var files = await Account.Drive.GetFilesAsync(folder, CancellationTokenSource.Token);
            foreach (var f in files)
                AddATask(new DownloadFileFromDriveTask(Account, AccountName, 0, null, f, dest));
            var dirs = await Account.Drive.GetSubfoldersAsync(folder, CancellationTokenSource.Token);
            foreach (var d in dirs)
            {
                var path = Common.AppendFolderToPath(dest, d.Name);
                Directory.CreateDirectory(path);
                await EnumerateFilesRecursiveAsync(d, path);
            }
        }

        protected override async Task StartAsync2()
        {
            var folder = OnlyContent ? DestFolder : Common.AppendFolderToPath(DestFolder, Folder.Name);
            Directory.CreateDirectory(folder);
            await EnumerateFilesRecursiveAsync(Folder, folder);
        }
    }
}
