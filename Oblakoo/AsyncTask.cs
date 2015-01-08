using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Oblakoo
{
    public class AsyncTask
    {

        public class OptionsBase
        {
            public OptionsBase(Account account, string accountName)
            {
                Account = account;
                AccountName = accountName;
            }

            public OptionsBase(Account account, Object tag)
            {
                Account = account;
                Tag = tag;
            }

            public Account Account { get; private set; }
            public string AccountName { get; set; }
            public Object Tag { get; set; }
        }

        public class CreateFolderOptions : OptionsBase
        {
            public string FolderName { get; set; }
            public AccountFile DestFolder { get; private set; }
            public AccountFile CreatedFolder { get; set; }
            public CreateFolderOptions(Account account, string accountName, string folderName, AccountFile destFolder)
                : base(account, accountName)
            {
                FolderName = folderName;
                DestFolder = destFolder;
            }
        }
        public class DownloadFolderOptions : OptionsBase
        {
            public AccountFile Folder { get; private set; }
            public string DestFolder { get; private set; }

            public DownloadFolderOptions(Account account, string accountName, AccountFile folder, string destFolder)
                : base(account, accountName)
            {
                Folder = folder;
                DestFolder = destFolder;
            }
        }

        public class UploadFolderOptions : OptionsBase
        {
            public string Path { get; private set; }
            public AccountFile DestFolder { get; private set; }

            public UploadFolderOptions(Account account, string accountName, string path, AccountFile destFolder)
                : base(account, accountName)
            {
                Path = path;
                DestFolder = destFolder;
            }
        }
        public class DownloadFileOptions : OptionsBase
        {
            public AccountFile File { get; private set; }
            public string DestFolder { get; private set; }

            public DownloadFileOptions(Account account, string accountName, AccountFile file, string destFolder)
                : base(account, accountName)
            {
                File = file;
                DestFolder = destFolder;
            }
        }

        public class UploadFileOptions : OptionsBase
        {
            public UploadFileOptions(Account account, string accountName, string fileName, AccountFile destFolder)
                : base(account, accountName)
            {
                FileName = fileName;
                DestFolder = destFolder;
            }

            public string FileName { get; private set; }
            public AccountFile DestFolder { get; private set; }
        }

        public const int HighestPriority = int.MaxValue;
        public const int HighPriority = int.MaxValue / 2;
        public const int NormalPriority = 0;
        public const int LowPriority = int.MinValue / 2;
        public const int LowestPriority = int.MinValue;

        public AsyncTask(AsyncTaskType type, object options, int priority, AsyncTask parent)
        {
            State = AsyncTaskState.Waiting;
            Type = type;
            Options = options;
            Priority = priority;
            Parent = parent;
        }
        public AsyncTask(AsyncTaskType type, object options, int priority)
        {
            State = AsyncTaskState.Waiting;
            Type = type;
            Options = options;
            Priority = priority;
        }

        public AsyncTask(AsyncTaskType type, object options)
        {
            State = AsyncTaskState.Waiting;
            Type = type;
            Options = options;
            Priority = NormalPriority;
        }

        private async Task UploadFolder(CancellationToken token)
        {
            var options = (UploadFolderOptions) Options;
            var files = new List<FileInfo>();
            await Common.EnumerateFilesRecursiveAsync(options.Path, x =>
            {
                files.Add(x);
                if (files.Count > 10)
                {
                    OnProgress(new AsyncTaskProgressEventArgs(0, files.ToArray()));
                    files.Clear();
                }
            }, token);
        }

        protected virtual void OnProgress(AsyncTaskProgressEventArgs e)
        {
            if (Progress != null)
                Progress(this, e);
        }


        public async Task StartAsync()
        {
            State = AsyncTaskState.Running;
            OnStateChanged();
            try
            {
                switch (Type)
                {
                    case AsyncTaskType.DownloadFolderFromDrive:

                        break;
                    case AsyncTaskType.DownloadFolderFromStorage:

                        break;

                    case AsyncTaskType.UploadFolder:
                        await UploadFolder(cancellationTokenSource.Token);
                        break;

                    case AsyncTaskType.UploadFile:
                        await ((UploadFileOptions) Options).Account.UploadFileAsync(
                            ((UploadFileOptions) Options).FileName, ((UploadFileOptions) Options).DestFolder,
                            cancellationTokenSource.Token);
                        if (State == AsyncTaskState.Running)
                            State = AsyncTaskState.Finished;
                        break;
                    case AsyncTaskType.DownloadFileFromDrive:
                        await ((DownloadFileOptions) Options).Account.DownloadFileFromDriveAsync(
                            ((DownloadFileOptions) Options).File, ((DownloadFileOptions) Options).DestFolder,
                            ActionIfFileExists.Rewrite, cancellationTokenSource.Token);
                        if (State == AsyncTaskState.Running)
                            State = AsyncTaskState.Finished;
                        break;
                    case AsyncTaskType.DownloadFileFromStorage:
                        await ((DownloadFileOptions)Options).Account.DownloadFileFromStorageAsync(
                            ((DownloadFileOptions)Options).File, ((DownloadFileOptions)Options).DestFolder,
                            ActionIfFileExists.Rewrite, cancellationTokenSource.Token);
                        if (State == AsyncTaskState.Running)
                            State = AsyncTaskState.Finished;
                        break;
                    case AsyncTaskType.CreateFolder:
                        ((CreateFolderOptions) Options).CreatedFolder =
                            await ((CreateFolderOptions) Options).Account.CreateFolderAsync(
                                ((CreateFolderOptions) Options).FolderName,
                                ((CreateFolderOptions) Options).DestFolder, cancellationTokenSource.Token);
                        if (State == AsyncTaskState.Running)
                            State = AsyncTaskState.Finished;
                        break;
                }
            }
            catch (Exception ex)
            {
                State = AsyncTaskState.Error;
                Exception = ex;
                OnStateChanged();
                throw;
            }
            OnStateChanged();
        }

        public void Cancel()
        {
            State = AsyncTaskState.Cancelled;
            cancellationTokenSource.Cancel();
        }

        protected virtual void OnStateChanged()
        {
            if (StateChanged != null) StateChanged(this, EventArgs.Empty);
        }

        public int Priority { get; private set; }
        public AsyncTask Parent { get; private set; }
        public Exception Exception { get; private set; }
        public object Options { get; private set; }
        public AsyncTaskType Type { get; private set; }
        public AsyncTaskState State { get; private set; }
        public event EventHandler StateChanged;
        public event EventHandler<AsyncTaskProgressEventArgs> Progress;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    }
}
