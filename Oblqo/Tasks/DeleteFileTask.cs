using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    [AccountFileStateChange(AccountFileStates.Deleted)]
    public class DeleteFileTask : AsyncTask
    {
        public AccountFile File { get; private set; }

        public DeleteFileTask(Account account, string accountName, int priority, AsyncTask[] parents, AccountFile file)
            : base(account, accountName, priority, parents)
        {
            File = file;
        }

        protected override async Task OnStartAsync()
        {
            if (!string.IsNullOrEmpty(File.StorageFile.Id))
                await Account.Storage.DeleteFileAsync(File.StorageFile, CancellationTokenSource.Token);
            await Account.Drives.DeleteFileAsync(File.DriveFiles, CancellationTokenSource.Token);
        }

        public override async Task LoadAsync(Account account, string id, System.Xml.Linq.XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            File = await account.GetFileAsync(xml.Element("file"), token);
        }

        public override System.Xml.Linq.XElement ToXml()
        {
            var xml = base.ToXml();
            xml.Add(File.ToXml("file"));
            return xml;
        }
    }
}
