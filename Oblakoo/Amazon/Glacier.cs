using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Glacier;
using Amazon.Glacier.Model;
using Amazon.Glacier.Transfer;
using Amazon.Runtime;

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
        private readonly AmazonGlacierClient client;
        /// <summary>
        /// Check job state each 10 mins.
        /// </summary>
        const int Timeout = 1000 * 60 * 10;

        public Glacier(string vault, string rootPath, string accessKeyId, string accessSecretKey, RegionEndpoint region)
        {
            Vault = vault;
            RootPath = rootPath;
            AccessKeyId = accessKeyId;
            AccessSecretKey = accessSecretKey;
            Region = region;
            rootFolder = new GlacierFile("", true, "", true);
            client = new AmazonGlacierClient(accessKeyId, accessSecretKey, region);
        }

        public async Task CreateVaultAsync(CancellationToken token)
        {
            var req = new CreateVaultRequest(Vault);
            await client.CreateVaultAsync(req, token);
        }

        public async Task DeleteVaultAsync(CancellationToken token)
        {
            var req = new DeleteVaultRequest(Vault);
            await client.DeleteVaultAsync(req, token);
        }

        public override async Task DeleteFileAsync(StorageFile file, CancellationToken token)
        {
            var req = new DeleteArchiveRequest(Vault, file.Id);
            await client.DeleteArchiveAsync(req, token);
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
            var fn = Path.GetFileName(pathName);
            var path = ((GlacierFile) destFolder).FolderPath;
            if (!string.IsNullOrEmpty(path) && !path.EndsWith("/"))
                path += "/";
            var filePathName = path + fn;
            using (var fileStream = File.OpenRead(pathName))
            {
                var fileLen = fileStream.Length;
                var checksum = TreeHashGenerator.CalculateTreeHash(fileStream);
                var observed = new ObserverStream(fileStream);
                var percent = 0;
                observed.PositionChanged += (sender, e) =>
                {
                    var currentPercent = (int) (100 * ((Stream) sender).Position/(float) fileLen);
                    if (currentPercent == percent) return;
                    percent = currentPercent;
                    progressCallback(new TransferProgress(percent));
                };
                var req = new UploadArchiveRequest(Vault, filePathName, checksum, observed);
                var result = await client.UploadArchiveAsync(req, token);
                return new GlacierFile(result.ArchiveId, false, fn);
            }
        }

        public override async Task DownloadFileAsync(StorageFile file, string destFolder,
            ActionIfFileExists actionIfFileExists, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            if (file.IsFolder) {
                throw new NotSupportedException("Glacier is not supported directories");
            }
            var initReq = new InitiateJobRequest(Vault, new JobParameters { ArchiveId = file.Id, Type = "archive-retrieval" });
            var initResult = await client.InitiateJobAsync(initReq, token);
            var describeReq = new DescribeJobRequest(Vault, initResult.JobId);
            var describeResult = await client.DescribeJobAsync(describeReq, token);
            var ok = false;
            while (!ok) // while job incompleted
            {
                if (describeResult.Completed)
                {
                    ok = true;
                }
                else
                {
                    await Task.Delay(Timeout, token);
                    describeResult = await client.DescribeJobAsync(describeReq, token);
                }
            }
            var req = new GetJobOutputRequest(Vault, initResult.JobId, null);
            var result = await client.GetJobOutputAsync(req, token);
            using (var output = File.OpenWrite(Common.AppendToPath(destFolder, file.Name)))
            {
                await Common.CopyStreamAsync(result.Body, output, null, result.ContentLength);
            }
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
