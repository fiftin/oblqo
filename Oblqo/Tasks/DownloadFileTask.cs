using System.Threading;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public abstract class DownloadFileTask : AsyncTask
    {
        public string DestFolder { get; private set; }
        public AccountFile File { get; private set; }

        protected DownloadFileTask()
        {
            
        }

        protected DownloadFileTask(Account account, string accountName, int priority, AsyncTask[] parent,
            string destFolder, AccountFile file)
            : base(account, accountName, priority, parent)
        {
            DestFolder = destFolder;
            File = file;
        }


        public override async Task LoadAsync(Account account, string id, System.Xml.Linq.XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            DestFolder = xml.Element("destFolder").Value;
            File = await account.GetFileAsync(xml.Element("storageFile"), xml.Element("driveFile"), token);
        }

        public override System.Xml.Linq.XElement ToXml()
        {
            var xml = base.ToXml();
            xml.SetElementValue("destFolder", DestFolder);
            if (File.DriveFile != null)
            {
                xml.Add(File.DriveFile.ToXml());
            }
            if (File.StorageFile != null)
            {
                xml.Add(File.StorageFile.ToXml());
            }
            return xml;
        }
    }
}
