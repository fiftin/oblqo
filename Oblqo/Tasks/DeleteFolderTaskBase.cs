using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo.Tasks
{
    public abstract class DeleteFolderTaskBase : AsyncTask
    {
        public AccountFile Folder { get; private set; }

        protected DeleteFolderTaskBase() { }

        protected DeleteFolderTaskBase(Account account, string accountName, int priority, AsyncTask[] parents, AccountFile folder)
            : base(account, accountName, priority, parents, AsyncTaskParentsMode.CancelIfAnyErrorOrCanceled)
        {
            Folder = folder;
        }

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
