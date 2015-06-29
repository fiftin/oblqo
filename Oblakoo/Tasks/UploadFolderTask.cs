using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class UploadFolderTask : AsyncTask
    {

        public string Path { get; private set; }
        public AccountFile DestFolder { get; private set; }

        public UploadFolderTask(Account account, string accountName, int priority, AsyncTask[] parent, 
            string path, AccountFile destFolder) 
            : base(account, accountName, priority, parent)
        {
            Path = path;
            DestFolder = destFolder;
        }

        private void EnumerateFilesRecursiveAsync(DirectoryInfo folder, AsyncTask task, CancellationToken token)
        {
            foreach (var file in folder.EnumerateFiles())
            {
                if (token.IsCancellationRequested)
                    return;
                var newTask = task == null
                    ? new UploadFileTask(Account, AccountName, 0, null, file.FullName, DestFolder) {Tag = Tag}
                    : new UploadFileTask(Account, AccountName, 0, new[] { task }, file.FullName, null) { Tag = Tag };
                AddTask(newTask);
            }
            foreach (var dir in folder.EnumerateDirectories())
            {
                if (token.IsCancellationRequested)
                    return;
                var newTask = task == null
                    ? new CreateFolderTask(Account, AccountName, 0, null, dir.Name, DestFolder) {Tag = Tag}
                    : new CreateFolderTask(Account, AccountName, 0, new[] { task }, dir.Name, null) { Tag = Tag };
                AddTask(newTask);
                EnumerateFilesRecursiveAsync(dir, newTask, token);
            }
            OnProgress(new AsyncTaskProgressEventArgs(0, null));
        }

        protected override async Task OnStartAsync()
        {
            var folder = new DirectoryInfo(Path);
            OnProgress(new AsyncTaskProgressEventArgs(0, null));
            await Task.Run(() => EnumerateFilesRecursiveAsync(folder, null, CancellationTokenSource.Token));
        }
    }
}
