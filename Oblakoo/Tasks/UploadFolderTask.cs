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

        public UploadFolderTask(Account account, string accountName, int priority, AsyncTask parent, string path, AccountFile destFolder) 
            : base(account, accountName, priority, parent)
        {
            Path = path;
            DestFolder = destFolder;
        }

        protected override async Task StartAsync2()
        {
            var files = new List<FileInfo>();
            await Common.EnumerateFilesRecursiveAsync(Path, x =>
            {
                files.Add(x);
                if (files.Count > 10)
                {
                    OnProgress(new AsyncTaskProgressEventArgs(0, files.ToArray()));
                    files.Clear();
                }
            }, CancellationTokenSource.Token);
        }
    }
}
