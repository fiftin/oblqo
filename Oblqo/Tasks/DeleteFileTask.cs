using System;
using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class DeleteFileTask : AsyncTask
    {
        public AccountFile File { get; private set; }

        public DeleteFileTask(Account account, string accountName, int priority, AsyncTask[] parents, AccountFile file)
            : base(account, accountName, priority, parents)
        {
            File = file;
        }

        protected override async Task OnStartAsync()
        {
            if (!string.IsNullOrEmpty(File.StorageFile.Id))
                await Account.Storage.DeleteFileAsync(File.StorageFile, CancellationTokenSource.Token);
            await Account.Drive.DeleteFileAsync(File.DriveFile, CancellationTokenSource.Token);
        }
    }
}
