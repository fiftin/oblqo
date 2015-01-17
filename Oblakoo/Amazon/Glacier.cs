using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Glacier;
using Amazon.Glacier.Model;
using Amazon.Glacier.Transfer;
using Amazon.Runtime.Internal;

namespace Oblakoo.Amazon
{
    /// <summary>
    /// 
    /// </summary>
    public class Glacier : Storage
    {
        private readonly StorageFile rootFolder;
        public string Vault { get; set; }
        public string RootPath { get; set; }
        public string AccessKeyId { get; set; }
        public string AccessSecretKey { get; set; }
        public RegionEndpoint Region { get; set; }
        private AmazonGlacierClient client;


        public Glacier(string vault, string rootPath, string accessKeyId, string accessSecretKey, RegionEndpoint region)
        {
            Vault = vault;
            RootPath = rootPath;
            AccessKeyId = accessKeyId;
            AccessSecretKey = accessSecretKey;
            Region = region;
            rootFolder = new GlacierFile("", true, "", true);
        }

        private async Task TransferAsync(Action<ArchiveTransferManager> action, CancellationToken token)
        {
            await Task.Run(() =>
            {
                var manager = CreateTransferManager();
                token.Register(() => manager.Dispose());
                using (manager)
                {
                    action(manager);
                }
            }, token);
        }

        public async Task CreateVaultAsync(CancellationToken token)
        {
            //var request = new CreateVaultRequest() {VaultName = Vault};

            //await client.CreateVaultAsync(request, token);
            await TransferAsync(manager => manager.CreateVault(Vault), token);
        }

        public async Task DeleteVaultAsync(CancellationToken token)
        {
            await TransferAsync(manager => manager.DeleteVault(Vault), token);
        }

        public override async Task DeleteFileAsync(StorageFile file, CancellationToken token)
        {
            if (file.IsRoot)
                await DeleteVaultAsync(token);
            else
                await TransferAsync(manager => manager.DeleteArchive(Vault, file.Id), token);
        }

        public override StorageFile GetFile(DriveFile driveFile)
        {
            return new GlacierFile(driveFile.StorageFileId, driveFile.IsFolder, driveFile.Name);
        }

        public override async Task<StorageFile> UploadFileAsync(string pathName, StorageFile destFolder,
            CancellationToken token, Action<TransferProgress> progressCallback)
        {
            Debug.Assert(destFolder.IsFolder);
            if (File.GetAttributes(pathName).HasFlag(FileAttributes.Directory))
                throw new NotSupportedException("Uploading directories now not implemented");
            var options = new UploadOptions();
            options.StreamTransferProgress += (sender, e) => progressCallback(new TransferProgress(e.PercentDone));
            UploadResult result = null;
            var fn = Path.GetFileName(pathName);
            var path = ((GlacierFile) destFolder).FolderPath;
            if (!string.IsNullOrEmpty(path) && !path.EndsWith("/"))
                path += "/";
            var filePathName = path + fn;
            await TransferAsync(manager => result = manager.Upload(Vault, filePathName, pathName, options), token);
            if (result == null)
                throw new Exception("Uploading failed");
            return new GlacierFile(result.ArchiveId, false, fn);
        }

        private ArchiveTransferManager CreateTransferManager()
        {
            return new ArchiveTransferManager(AccessKeyId, AccessSecretKey, Region);
        }

        public override async Task DownloadFileAsync(StorageFile file, string destFolder,
            ActionIfFileExists actionIfFileExists, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            if (file.IsFolder)
                throw new NotSupportedException("Glacier is not supported directories");
            var options = new DownloadOptions();
            options.StreamTransferProgress += (sender, e) => progressCallback(new TransferProgress(e.PercentDone));
            await TransferAsync(manager =>
            {
                var filePathName = Common.AppendFolderToPath(destFolder, file.Name);
                var jobId = manager.InitiateArchiveRetrievalJob(Vault, file.Id);
                
                manager.DownloadJob(Vault, jobId, filePathName, options);
            }, token);
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

        public override bool IsSupportFolders
        {
            get { return false; }
        }

        public override async Task ClearAsync(CancellationToken token)
        {
            await DeleteVaultAsync(token);
            await CreateVaultAsync(token);
        }
    }
}
