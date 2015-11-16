using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public abstract class DownloadFolderTask : AsyncTask
    {
        public string DestFolder { get; private set; }
        public AccountFile Folder { get; set; }

        protected DownloadFolderTask()
        {

        }

        protected DownloadFolderTask(Account account, string accountName, int priority, AsyncTask[] parent,
            string destFolder, AccountFile folder)
            : base(account, accountName, priority, parent)
        {
            DestFolder = destFolder;
            Folder = folder;
        }

        protected abstract DownloadFileTask CreateDownloadFileTask(Account account, string accountName, int priority, AsyncTask[] parent, AccountFile file, string destFolder);

        protected async Task EnumerateFilesRecursiveAsync(AccountFile folder, string dest)
        {
            var files = await Account.Drive.GetFilesAsync(folder.DriveFile, folder, CancellationTokenSource.Token);
            foreach (var f in files)
                AddTask(CreateDownloadFileTask(Account, AccountName, 0, null, new AccountFile(Account.Storage.GetFile(f), f, folder), dest));
            var dirs = await Account.Drive.GetSubfoldersAsync(folder.DriveFile, folder, CancellationTokenSource.Token);
            foreach (var d in dirs)
            {
                var path = Common.AppendToPath(dest, d.Name);
                Directory.CreateDirectory(path);
                await EnumerateFilesRecursiveAsync(new AccountFile(Account.Storage.GetFile(d), d, folder), path);
            }
        }

        public override async Task LoadAsync(Account account, string id, System.Xml.Linq.XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            DestFolder = xml.Element("destFolder").Value;
            Folder = await account.GetFileAsync(xml.Element("storageFolder"), xml.Element("driveFolder"), token);
        }

        public override System.Xml.Linq.XElement ToXml()
        {
            var xml = base.ToXml();
            return xml;
        }
    }
}
