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

namespace Oblqo.Amazon
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
            rootFolder = new GlacierFile(this, "", true, "", true);
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
            var res = await client.DeleteArchiveAsync(req, token);
        }

        public override StorageFile GetFile(DriveFile driveFile)
        {
            return new GlacierFile(this, driveFile.StorageFileId, driveFile.IsFolder, driveFile.Name);
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

                return new GlacierFile(this, result.ArchiveId, false, fn);

            }
        }

        /*s
        public override bool IsValidFileId(string fileId)
        {
            GlacierFileIdentity identity;
            if (!GlacierFileIdentity.TryParse(fileId, out identity))
            {
                return false;
            }
            return identity.Region == Region.SystemName && identity.Vault == Vault;
        }

        internal string GetArchiveId(string fileId)
        {
            return GlacierFileIdentity.Parse(fileId).ArchiveId;
        }

        internal GlacierFileIdentity GetFileIdentity(string archiveId)
        {
            return new GlacierFileIdentity(Region, Vault, archiveId);
        }

        internal string GetFileId(string archiveId)
        {
            return GetFileIdentity(archiveId).ToString();
        }
        */

        public override async Task DownloadFileAsync(StorageFile file, string destFolder,
            ActionIfFileExists actionIfFileExists, CancellationToken token, Action<TransferProgress> progressCallback)
        {
            if (file.IsFolder) {
                throw new NotSupportedException("Glacier is not supported directories");
            }
            string jobId;
            if (((GlacierFile)file).JobId == null)
            {
                var initReq = new InitiateJobRequest(Vault, new JobParameters { ArchiveId = file.Id, Type = "archive-retrieval" });
                var initResult = await client.InitiateJobAsync(initReq, token);
                jobId = initResult.JobId;
                ((GlacierFile)file).JobId = jobId;
            }
            else
            {
                jobId = ((GlacierFile)file).JobId;
            }
            var describeReq = new DescribeJobRequest(Vault, jobId);
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
            var req = new GetJobOutputRequest(Vault, jobId, null);
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
            return new GlacierFile(this, "", true, newFolderPath);
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

        public override async Task<StorageFile> GetFileAsync(System.Xml.Linq.XElement xml, CancellationToken token)
        {
            var ret = new GlacierFile(
                this,
                xml.Attribute("id").Value,
                bool.Parse(xml.Attribute("isFolder").Value),
                xml.Attribute("folderPath").Value,
                bool.Parse(xml.Attribute("isRoot").Value));

            if (xml.Attribute("jobId") != null)
            {
                ret.JobId = xml.Attribute("jobId").Value;
            }
            return ret;
        }

        public override string Kind
        {
            get { return "gl"; }
        }

        public override string Id
        {
            get { return string.Format("{0}:{1}", Region.SystemName, Vault); }
        }

    }
}
