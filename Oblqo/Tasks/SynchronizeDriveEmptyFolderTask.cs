using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo.Tasks
{
    public class SynchronizeDriveEmptyFolderTask : AsyncTask
    {
        public AccountFile Folder { get; private set; }

        public SynchronizeDriveEmptyFolderTask(Account account, string accountName, int priority, 
            AsyncTask[] parents, AccountFile folder)
            : base(account, accountName, priority, parents)
        {
            Folder = folder;
        }

        protected async override Task OnStartAsync()
        {
            var tasks = (from drive in Account.Drives
                         where Folder.GetDriveFile(drive) == null
                         select Folder.GetFileAndCreateIfFolderIsNotExistsAsync(drive, CancellationTokenSource.Token)
                         ).Cast<Task>().ToList();
            await Task.WhenAll(tasks);
        }

        public override bool Visible => false;


        public override async Task LoadAsync(Account account, string id, XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            Folder = await account.GetFileAsync(xml.Element("folder"), token);
        }

        public override XElement ToXml()
        {
            var xml = base.ToXml();
            xml.Add(Folder.ToXml("folder"));
            return xml;
        }
    }
}
