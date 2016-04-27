using Amazon;
using Amazon.Glacier;
using Amazon.Glacier.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo.Amazon
{
    public class GlacierPseudoDrive : Drive
    {
        private XDocument document;
        private DriveFile rootFolder;
        private AmazonGlacierClient client;
        private string vault;

        /// <summary>
        /// Check job state each 10 mins.
        /// </summary>
        const int Timeout = 1000 * 60 * 10;

        public GlacierPseudoDrive(Account account, string id, XDocument document,
                                  string accessKeyId, string accessSecretKey,
                                  RegionEndpoint region, string vault)
            : base(account, id)
        {
            this.document = document;
            rootFolder = new GlacierPseudoFile(this, document.Root.Element("folder"));
            client = new AmazonGlacierClient(accessKeyId, accessSecretKey, region);
            this.vault = vault;
        }

        public GlacierPseudoDrive(Account account, string id, XDocument document, string vault)
            : base(account, id)
        {
            this.document = document;
            rootFolder = new GlacierPseudoFile(this, document.Root.Element("folder"));
            this.vault = vault;
        }

        public async Task<string> InitiateLoadingAsync(CancellationToken token)
        {
            var req = new InitiateJobRequest()
            {
                VaultName = vault,
                JobParameters = new JobParameters()
                {
                    Type = "inventory-retrieval"
                }
            };
            var res = await client.InitiateJobAsync(req);
            return res.JobId;
        }

        public async Task SaveAsync(Stream output)
        {
            using (var memStrem = new MemoryStream())
            {
                document.WriteTo(new System.Xml.XmlTextWriter(memStrem, Encoding.UTF8));
                await memStrem.CopyToAsync(output);
            }
        }

        public async Task LoadAsync(string jobId, CancellationToken token)
        {
            var describeReq = new DescribeJobRequest(vault, jobId);
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
            var req = new GetJobOutputRequest(vault, jobId, null);
            var result = await client.GetJobOutputAsync(req, token);
            using (var output = new MemoryStream())
            {
                await Common.CopyStreamAsync(result.Body, output, null, result.ContentLength);
                var reader = new StreamReader(output);
                ReadFromJson(reader);
            }
        }

        public void ReadFromJson(TextReader render)
        {
            document.Root.RemoveAll();
            var jsonReader = new Newtonsoft.Json.JsonTextReader(render);
            var json = Newtonsoft.Json.Linq.JObject.Load(jsonReader);
            var archives = json["ArchiveList"].ToArray();
            foreach (var item in archives)
            {
                var elem = AddFileElement(item.Value<string>("ArchiveDescription"));
                elem.SetAttributeValue("id", item.Value<string>("ArchiveId"));
                elem.SetAttributeValue("size", item.Value<string>("Size"));
                elem.SetAttributeValue("creationDate", item.Value<string>("CreationDate"));
            }
        }

        public XElement AddFileElement(string path)
        {
            var parts = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var elem = document.Root;
            for (int i = 0; i < parts.Length; i++)
            {
                var name = parts[i];
                var tag = i == parts.Length - 1 ? "file" : "folder";
                var nextElem = elem.Elements(tag).FirstOrDefault(x => x.Attribute("name").Value == name);
                if (nextElem == null)
                {
                    nextElem = new XElement(tag);
                    nextElem.SetAttributeValue("name", name);
                    elem.Add(nextElem);
                }
                elem = nextElem;
            }
            return elem;
        }

        public override DriveFile RootFolder
        {
            get
            {
                return rootFolder;
            }
        }

        public override string ShortName
        {
            get
            {
                return document.Root.Attribute("name").Value;
            }
        }

        public override Task<DriveFile> CreateFolderAsync(string folderName, DriveFile destFolder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteFileAsync(DriveFile driveFile, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteFolderAsync(DriveFile driveFolder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task DownloadFileAsync(DriveFile driveFile, Stream output, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task DownloadFileAsync(DriveFile driveFile, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task EnumerateFilesRecursive(DriveFile driveFolder, Action<DriveFile> action, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<DriveFile> GetFileAsync(XElement xml, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<ICollection<DriveFile>> GetFilesAsync(DriveFile folder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile folder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<Image> GetThumbnailAsync(DriveFile file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<Stream> ReadFileAsync(DriveFile file, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<DriveFile> UploadFileAsync(Stream fileStream, string fileName, DriveFile destFolder, string storageFileId, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
