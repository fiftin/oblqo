using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class UploadFolderTask : AsyncTask
    {

        public class ProgressState
        {
            public ProgressState(DirectoryInfo folder, FileInfo file, CreateFolderTask parentTask)
            {
                Folder = folder;
                File = file;
                ParentTask = parentTask;
            }

            public CreateFolderTask ParentTask { get; private set; }
            public DirectoryInfo Folder { get; private set; }
            public FileInfo File { get; private set; }

            public bool IsFolder
            {
                get { return File == null; }
            }
            public CreateFolderTask CreatedTask { get; set; }
        }

        public string Path { get; private set; }
        public AccountFile DestFolder { get; private set; }

        public UploadFolderTask(Account account, string accountName, int priority, AsyncTask parent, string path, AccountFile destFolder) 
            : base(account, accountName, priority, parent)
        {
            Path = path;
            DestFolder = destFolder;
        }

        private static void EnumerateFilesRecursiveAsync(DirectoryInfo folder, CreateFolderTask task, Action<ProgressState> action, CancellationToken token)
        {
            foreach (var file in folder.EnumerateFiles())
            {
                if (token.IsCancellationRequested)
                    return;
                action(new ProgressState(folder, file, task));
            }
            foreach (var dir in folder.EnumerateDirectories())
            {
                if (token.IsCancellationRequested)
                    return;
                var state = new ProgressState(dir, null, task);
                action(state);
                EnumerateFilesRecursiveAsync(dir, state.CreatedTask, action, token);
            }
        }

        protected override async Task StartAsync2()
        {
            var folder = new DirectoryInfo(Path);
            var rootState = new ProgressState(folder, null, null);
            OnProgress(new AsyncTaskProgressEventArgs(0, rootState));
            await Task.Run(() => EnumerateFilesRecursiveAsync(folder, rootState.CreatedTask, x =>
            {
                OnProgress(new AsyncTaskProgressEventArgs(0, x));
            }, CancellationTokenSource.Token));
        }
    }
}
