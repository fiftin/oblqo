﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            var files = await Account.GetFilesAsync(folder, CancellationTokenSource.Token);
            foreach (var f in files)
                AddTask(CreateDownloadFileTask(Account, AccountName, 0, null, f, dest));
            var dirs = await Account.GetSubfoldersAsync(folder, CancellationTokenSource.Token);
            foreach (var d in dirs)
            {
                var path = Common.AppendToPath(dest, d.Name);
                Directory.CreateDirectory(path);
                await EnumerateFilesRecursiveAsync(d, path);
            }
        }

        public override async Task LoadAsync(Account account, string id, XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            DestFolder = xml.Attribute("destFolder").Value;
            Folder = await account.GetFileAsync(xml, token);
        }

        public override XElement ToXml()
        {
            var xml = base.ToXml();
            xml.SetAttributeValue("destFolder", DestFolder);
            xml.Add(Folder.ToXml("folder"));
            return xml;
        }
    }
}
