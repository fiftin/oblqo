using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public abstract class DeleteFolderTaskBase : AsyncTask
    {
        public AccountFile Folder { get; private set; }

        protected DeleteFolderTaskBase(Account account, string accountName, int priority, AsyncTask[] parents, AccountFile folder)
            : base(account, accountName, priority, parents, AsyncTaskParentsMode.CancelIfAnyErrorOrCanceled)
        {
            Folder = folder;
        }

    }
}
