using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public class SynchronizeDriveEmptyFolderTask : AsyncTask
    {
        AccountFile Folder { get; }

        public SynchronizeDriveEmptyFolderTask(Account account, string accountName, int priority, 
            AsyncTask[] parents, AccountFile folder)
            : base(account, accountName, priority, parents)
        {
            Folder = folder;
        }

        protected async override Task OnStartAsync()
        {
            var token = CancellationTokenSource.Token;
            var tasks = (from drive in Account.Drives
                         where Folder.GetFile(drive) == null
                         select Folder.GetFileAndCreateIfFolderIsNotExistsAsync(drive, token)
                         ).Cast<Task>().ToList();
            await Task.WhenAll(tasks);
        }
    }
}
