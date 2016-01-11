using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    [AccountFileStateChange(AccountFileStates.Deleted)]
    public class DeleteFileTask : DeleteFileTaskBase
    {
        public DeleteFileTask(Account account, string accountName, int priority, AsyncTask[] parents, AccountFile file)
            : base(account, accountName, priority, parents, file, false)
        {
        }
    }
}
