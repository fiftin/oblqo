using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    [AccountFileStateChange(AccountFileStates.UnsyncronizedWithStorage)]
    public class DeleteFileFromArchiveTask : DeleteFileTaskBase
    {
        public DeleteFileFromArchiveTask(Account account, string accountName, int priority, AsyncTask[] parents, AccountFile file)
            : base(account, accountName, priority, parents, file, true)
        {
        }
    }
}
