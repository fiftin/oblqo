using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo.Tasks
{
    public class CreateFolderTask : AsyncTask
    {
        public string FolderName { get; set; }
        public AccountFile DestFolder { get; private set; }
        public AccountFile CreatedFolder { get; set; }

        public CreateFolderTask(Account account, string accountName, int priority, AsyncTask[] parent, string folderName, AccountFile destFolder) 
            : base(account, accountName, priority, parent)
        {
            FolderName = folderName;
            DestFolder = destFolder;
        }

        protected override async Task OnStartAsync()
        {
            var destFolder = DestFolder;
            if (destFolder == null && Common.IsSingle(Parents) && Parents[0] is CreateFolderTask)
                destFolder = ((CreateFolderTask)Parents[0]).CreatedFolder;
            CreatedFolder = await Account.CreateFolderAsync(FolderName, destFolder, CancellationTokenSource.Token);
        }


        public override async Task LoadAsync(Account account, string id, XElement xml, CancellationToken token)
        {
            await base.LoadAsync(account, id, xml, token);
            FolderName = xml.Attribute("folderName").Value;
            DestFolder = await account.GetFileAsync(xml.Element("destFolder"), token);
        }

        public override XElement ToXml()
        {
            var xml = base.ToXml();
            xml.SetAttributeValue("folderName", FolderName);
            xml.Add(DestFolder.ToXml("destFolder"));
            return xml;
        }
    }
}
