using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public class DeleteFileTaskBase : AsyncTask
    {
        public AccountFile File { get; private set; }
        public bool ArchiveFileOnly { get; private set; }

        public DeleteFileTaskBase(Account account, string accountName, int priority, AsyncTask[] parents, AccountFile file, bool archiveFileOnly)
            : base(account, accountName, priority, parents)
        {
            File = file;
            ArchiveFileOnly = archiveFileOnly;
        }

        protected override async Task OnStartAsync()
        {
            if (File.StorageFile.Id != null && File.HasValidStorageFileId)
            {
                await Account.Storage.DeleteFileAsync(File.StorageFile, CancellationTokenSource.Token);
            }
            if (ArchiveFileOnly)
            {
                // reset storage file id for drive file
                await File.DriveFiles.SetStorageFileIdAsync(null, CancellationTokenSource.Token);
            }
            else
            {
                await Account.Drives.DeleteFileAsync(File.DriveFiles, CancellationTokenSource.Token);
            }
        }

        public override async Task LoadAsync(Account account, string id, System.Xml.Linq.XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            File = await account.GetFileAsync(xml.Element("file"), token);
            ArchiveFileOnly = bool.Parse(xml.Attribute("archiveFileOnly").Value);
        }

        public override System.Xml.Linq.XElement ToXml()
        {
            var xml = base.ToXml();
            xml.Add(File.ToXml("file"));
            xml.SetAttributeValue("archiveFileOnly", ArchiveFileOnly);
            return xml;
        }
    }
}
