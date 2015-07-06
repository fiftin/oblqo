using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    public class UploadFolderTask : AsyncTask
    {

        public string Path { get; private set; }
        public AccountFile DestFolder { get; private set; }
        public AccountFile CreatedFolder { get; set; }
        public string FolderName
        {
            get
            {
                return System.IO.Path.GetFileName(Path);
            }
        }
        private AsyncTask lastTask;

        public UploadFolderTask(Account account, string accountName, int priority, AsyncTask[] parent, string path, AccountFile destFolder) 
            : base(account, accountName, priority, parent)
        {
            Path = path;
            DestFolder = destFolder;
        }

        private void EnumerateFilesRecursiveAsync(DirectoryInfo folder, AccountFile destDirectory, AsyncTask task, CancellationToken token)
        {
            foreach (var file in folder.EnumerateFiles())
            {
                if (token.IsCancellationRequested)
                    return;
                //var newTask = task == null
                //    ? new UploadFileTask(Account, AccountName, 0, null, file.FullName, destDirectory)
                //    : new UploadFileTask(Account, AccountName, 0, new[] { task }, file.FullName, null);
                var newTask = new UploadFileTask(Account, AccountName, 0, new[] { task }, file.FullName, null);
                AddTask(newTask);
                lastTask.AddParent(newTask);
            }
            foreach (var dir in folder.EnumerateDirectories())
            {
                if (token.IsCancellationRequested)
                    return;
                //var newTask = task == null
                //    ? new CreateFolderTask(Account, AccountName, 0, null, dir.Name, destDirectory)
                //    : new CreateFolderTask(Account, AccountName, 0, new[] { task }, dir.Name, null);
                var newTask = new CreateFolderTask(Account, AccountName, 0, new[] { task }, dir.Name, null);
                AddTask(newTask);
                lastTask.AddParent(newTask);
                EnumerateFilesRecursiveAsync(dir, destDirectory, newTask, token);
            }
            OnProgress(new AsyncTaskProgressEventArgs(0, null));
        }

        protected override async Task OnStartAsync()
        {
            lastTask = new EmptyTask(Account, AccountName, Priority, null);
            var folder = new DirectoryInfo(Path);
            OnProgress(new AsyncTaskProgressEventArgs(0, null));
            var createFolderTask = new CreateFolderTask(Account, AccountName, 0, null, FolderName, DestFolder);
            AddTask(createFolderTask);
            lastTask.AddParent(createFolderTask);
            //CreatedFolder = await Account.CreateFolderAsync(System.IO.Path.GetFileName(Path), DestFolder, CancellationTokenSource.Token);
            EnumerateFilesRecursiveAsync(folder, CreatedFolder, createFolderTask, CancellationTokenSource.Token);
            AddTask(lastTask);
            await Task.Run(async () =>
            {
                while (lastTask.State == AsyncTaskState.Running || lastTask.State == AsyncTaskState.Waiting)
                {
                    await Task.Delay(500);
                }
            });
        }
    }
}
