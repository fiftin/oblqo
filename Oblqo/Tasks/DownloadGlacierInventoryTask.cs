using Oblqo.Amazon;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo.Tasks
{
    public class DownloadGlacierInventoryTask : AsyncTask
    {
        private string jobId;

        public DownloadGlacierInventoryTask()
        {

        }

        public DownloadGlacierInventoryTask(Account account, string accountName, int priority, AsyncTask[] parent)
            : base(account, accountName, priority, parent)
        {
        }

        protected override async Task OnStartAsync()
        {
            var glacier = (Glacier)Account.Storage;
            if (jobId == null)
            {
                jobId = await glacier.InitiateDownloadingInventoryAsync(CancellationTokenSource.Token);
            }
            var json = await glacier.DownloadInventoryAsync(jobId, CancellationTokenSource.Token);
            var doc = new XDocument(new XElement("vault"));
            GlacierPseudoDrive.ReadFromJson(doc, new StringReader(json));
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                var inventoryPath = "accounts/" + AccountName + "/glacier-inventory.xml";
                var stream = store.CreateFile(inventoryPath);
                await GlacierPseudoDrive.SaveAsync(doc, stream);
            }
        }

        public override async Task LoadAsync(Account account, string id, XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            jobId = xml.Element("jobId").Value;
        }

        public override XElement ToXml()
        {
            var xml = base.ToXml();
            xml.SetElementValue("jobId", jobId);
            return xml;
        }
    }
}
