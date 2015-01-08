using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Glacier.Transfer;
using Amazon.Runtime;

namespace Oblakoo.Amazon
{
    /// <summary>
    /// 
    /// </summary>
    public class Glacier : Storage
    {
        private StorageFile rootFolder;
        public string Vault { get; set; }
        public string RootPath { get; set; }
        public string AccessKeyId { get; set; }
        public string AccessSecretKey { get; set; }
        public RegionEndpoint Region { get; set; }

        public Glacier(string vault, string rootPath, string accessKeyId, string accessSecretKey, RegionEndpoint region)
        {
            Vault = vault;
            RootPath = rootPath;
            AccessKeyId = accessKeyId;
            AccessSecretKey = accessSecretKey;
            Region = region;
            rootFolder = new GlacierFile("", true, "/");
        }

        public override StorageFile GetFile(DriveFile driveFile)
        {
            return new GlacierFile(driveFile.StorageFileId, driveFile.Name);
        }

        public override async Task<StorageFile> UploadFileAsync(string pathName, StorageFile destFolder, CancellationToken token)
        {
            Debug.Assert(destFolder.IsFolder);
            if (File.GetAttributes(pathName).HasFlag(FileAttributes.Directory))
                throw new NotSupportedException("Uploading directories now not implemented");
            var manager = CreateTransferManager();
            var options = new UploadOptions();
            options.StreamTransferProgress += upload_StreamTransferProgress;
            UploadResult result = null;
            var fn = Path.GetFileName(pathName);
            var filePathName = ((GlacierFile)destFolder).FolderPath + fn;
            await Task.Run(() =>
            {
                result = manager.Upload(Vault, filePathName, pathName, options);
            });
            if (result == null)
                throw new Exception("Uploading failed");
            return new GlacierFile(result.ArchiveId, fn);
        }

        private ArchiveTransferManager CreateTransferManager()
        {
            return new ArchiveTransferManager(AccessKeyId, AccessSecretKey, Region);
        }

        public override async Task DownloadFileAsync(StorageFile file, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token)
        {
            if (file.IsFolder)
                throw new NotSupportedException("Glacier is not supported directories");
            var manager = CreateTransferManager();
            var options = new DownloadOptions();
            options.StreamTransferProgress += download_StreamTransferProgress;
            var filePathName = destFolder + Path.DirectorySeparatorChar + file.Name;
            await Task.Run(() => manager.Download(Vault, file.Id, filePathName));
        }

        void upload_StreamTransferProgress(object sender, StreamTransferProgressArgs args)
        {
            if (TransferProgress != null)
                TransferProgress(this, new TransferProgressEventArgs(args.PercentDone, TransferDirection.Upload));
        }

        void download_StreamTransferProgress(object sender, StreamTransferProgressArgs args)
        {
            if (TransferProgress != null)
                TransferProgress(this, new TransferProgressEventArgs(args.PercentDone, TransferDirection.Download));
        }

#pragma warning disable 1998
        public override async Task<StorageFile> CreateFolderAsync(string folderName, StorageFile destFolder, CancellationToken token)
#pragma warning restore 1998
        {
            var path = destFolder == null ? "/" : ((GlacierFile) destFolder).FolderPath;
            var newFolderPath = path + folderName + "/";
            return new GlacierFile(newFolderPath, true, newFolderPath);
        }

        public override StorageFile RootFolder
        {
            get { return rootFolder; }
        }

        public event EventHandler<TransferProgressEventArgs> TransferProgress;
    }
}
