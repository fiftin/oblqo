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

        public override bool IsIgnored => true;
        public override bool IsLantinOnlySupport => true;

        public GlacierPseudoDrive(Account account, string id, XDocument document)
            : base(account, id)
        {
            this.document = document;
            rootFolder = new GlacierPseudoFile(this, document.Root);
        }


        public async Task SaveAsync(Stream output)
        {
            await SaveAsync(document, output);
        }

        public static async Task SaveAsync(XDocument document, Stream output)
        {
            using (var memStrem = new MemoryStream())
            {
                using (var writer = new System.Xml.XmlTextWriter(memStrem, Encoding.UTF8))
                {
                    document.WriteTo(writer);
                    writer.Flush();
                    memStrem.Seek(0, SeekOrigin.Begin);
                    await memStrem.CopyToAsync(output);
                }
            }
        }

        public void ReadFromJson(TextReader render)
        {
            ReadFromJson(document, render);
        }

        public static void ReadFromJson(XDocument document, TextReader render)
        {
            document.Root.RemoveAll();
            var jsonReader = new Newtonsoft.Json.JsonTextReader(render);
            var json = Newtonsoft.Json.Linq.JObject.Load(jsonReader);
            var archives = json["ArchiveList"].ToArray();
            foreach (var item in archives)
            {
                var elem = AddFileElement(document, item.Value<string>("ArchiveDescription"));
                elem.SetAttributeValue("id", item.Value<string>("ArchiveId"));
                elem.SetAttributeValue("size", item.Value<string>("Size"));
                elem.SetAttributeValue("creationDate", item.Value<string>("CreationDate"));
            }
        }

        public static XElement AddFileElement(XDocument document, string path)
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
                return "Glacier";
            }
        }

        public override Task<DriveFile> CreateFolderAsync(string folderName, DriveFile destFolder, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteFileAsync(DriveFile driveFile, CancellationToken token)
        {
            return Task.FromResult(0);
        }

        public override Task DeleteFolderAsync(DriveFile driveFolder, CancellationToken token)
        {
            return Task.FromResult(0);
        }

        public override Task DownloadFileAsync(DriveFile driveFile, Stream output, CancellationToken token)
        {
            return Task.FromResult(0);
        }

        public override Task DownloadFileAsync(DriveFile driveFile, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token)
        {
            return Task.FromResult(0);
        }

        public override Task EnumerateFilesRecursive(DriveFile driveFolder, Action<DriveFile> action, CancellationToken token)
        {
            var folder = (GlacierPseudoFile)driveFolder;
            var files = folder.Element.Elements("file").Select(x => new GlacierPseudoFile(this, x));
            foreach (var f in files)
            {
                action(f);
            }
            return Task.FromResult(0);
        }

        public override Task<DriveFile> GetFileAsync(XElement xml, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override Task<ICollection<DriveFile>> GetFilesAsync(DriveFile driveFolder, CancellationToken token)
        {
            var folder = (GlacierPseudoFile)driveFolder;
            var files = folder.Element.Elements("file").Select(x => new GlacierPseudoFile(this, x)).Cast<DriveFile>().ToList();
            return Task.FromResult<ICollection<DriveFile>>(files);
        }

        public override Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile driveFolder, CancellationToken token)
        {
            var folder = (GlacierPseudoFile)driveFolder;
            var files = folder.Element.Elements("folder").Select(x => new GlacierPseudoFile(this, x)).Cast<DriveFile>().ToList();
            return Task.FromResult<ICollection<DriveFile>>(files);
        }

        public override Task<Image> GetThumbnailAsync(DriveFile file, CancellationToken token)
        {
            throw new BadImageFormatException();
        }

        public override Task<Stream> ReadFileAsync(DriveFile file, CancellationToken token)
        {
            return Task.FromResult<Stream>(new MemoryStream(new byte[0]));
        }

        public override Task<DriveFile> UploadFileAsync(Stream fileStream, string fileName, DriveFile destFolder, string storageFileId, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
