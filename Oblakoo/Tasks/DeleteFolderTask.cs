using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DeleteFolderTask : DeleteFolderTaskBase
    {

        public DeleteFolderTask(Account account, string accountName, int priority, AsyncTask[] parent, AccountFile folder) 
            : base(account, accountName, priority, parent, folder)
        {
        }

        protected override async Task StartAsync2()
        {
            var parents = new List<AsyncTask>();
            var files = await Account.GetFilesAsync(Folder, CancellationTokenSource.Token);
            foreach (var task in files.Select(f => new DeleteFileTask(Account, AccountName, 0, null, f)))
            {
                parents.Add(task);
                AddTask(task);
            }
            var dirs = await Account.GetSubfoldersAsync(Folder, CancellationTokenSource.Token);
            foreach (var task in dirs.Select(d => new DeleteFolderTask(Account, AccountName, Priority, null, d)))
            {
                parents.Add(task);
                AddTask(task);
            }
            var lastTask = new DeleteEmptyFolderTask(Account, AccountName, Priority, parents.ToArray(), Folder);
            AddTask(lastTask);

            await Task.Run(async () =>
            {
                while (lastTask.State == AsyncTaskState.Running || lastTask.State == AsyncTaskState.Waiting)
                    await Task.Delay(500);
                State = lastTask.State;
            });
        }

    }
}
