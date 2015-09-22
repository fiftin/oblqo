using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Auth.OAuth2.Responses;
using Oblqo.Core;

namespace Oblqo.Google
{
    public class GoogleDrive : Drive
    {
        public string AccessKeyId { get; set; }
        public string AccessSecretKey { get; set; }
        public ClientSecrets Secrets { get; set; }
        public const string RootId = "root";


        private GoogleFile rootFolder;
        private readonly ManualResetEvent rootFolderEvent = new ManualResetEvent(false);

        protected GoogleDrive(Storage storage, Account account, ClientSecrets secrets)
            : base(storage, account)
        {
            Secrets = secrets;
        }

        public static async Task<GoogleDrive> CreateInstance(Storage storage, Account account, ClientSecrets secrets, string rootPath, CancellationToken token)
        {
            var ret = new GoogleDrive(storage, account, secrets);
            var rootFolder = await ret.GetFolderByPathAsync(rootPath, token, true);
            ret.rootFolder = rootFolder;
            return ret;
        }

        public async Task<File> GetFolderAsync(string parentFolderId, string folderName, CancellationToken token)
        {
            var service = await GetServiceAsync(CancellationToken.None);
            var request = service.Files.List();
            request.Q = string.Format("'{0}' in parents and trashed = false and title = '{1}'", parentFolderId, folderName);
            request.Fields = "items(id,mimeType,createdDate,modifiedDate,fileSize,title,properties)";
            var result = await request.ExecuteAsync(token);
            if (result.Items.Count == 0)
                return null;
            if (result.Items.Count > 1)
                throw new Exception("Requst returns more then one folder");
            return result.Items[0];
        }

        public async Task<GoogleFile> GetFolderByPathAsync(string path, CancellationToken token, bool createIfNotExists = false)
        {
            var folders = path.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            var file = new File {Id = RootId, MimeType = GoogleMimeTypes.Folder};
            var currentPath = "";
            foreach (var f in folders)
            {
                currentPath += "/" + f;
                var parent = file;
                file = await GetFolderAsync(file.Id, f, token);
                if (file == null && !createIfNotExists)
                {
                    throw new Exception("Path is not exists: " + currentPath);
                }
                if (file == null)
                {
                    var newFile = await CreateFolderAsync(f, new GoogleFile(this, parent), token);
                    file = ((GoogleFile)newFile).file;
                }
            }
            return new GoogleFile(this, file);
        }

        internal async Task<DriveService> GetServiceAsync(CancellationToken token)
        {
            try
            {
                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(Secrets, new[] { DriveService.Scope.Drive }, "user", token);
                return new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Oblqo"
                });
            }
            catch (TokenResponseException ex)
            {
                throw new ConnectionException(Account, ex.Message, ex);
            }
        }

        public void AssociateFileWithStorageFile(GoogleFile driveFile, StorageFile storageFile)
        {
        }

        public override async Task<DriveFile> UploadFileAsync(string pathName, DriveFile destFolder, string storageFileId, CancellationToken token)
        {
            Debug.Assert(System.IO.File.Exists(pathName) &&
                         !System.IO.File.GetAttributes(pathName).HasFlag(System.IO.FileAttributes.Directory));
            var service = await GetServiceAsync(token);

            // Initializing properties.
            List<Property> props = new List<Property>();
            //
            props.Add(new Property { Key=string.Format("{0}.sid", Storage.Kind), Value=Storage.Id, Visibility="PRIVATE" });
            //
            props.Add(new Property { Key = "src", Value = Storage.Kind, Visibility = "PRIVATE" });
            // Storage file ID.
            int storageFileIdPropertyKeyLen = string.Format(StorageFileIdFormat, Storage.Kind, 0).Length;
            int storageFileIdPropertyValueLen = GoogleFile.PropertyMaxLength - storageFileIdPropertyKeyLen;
            string[] storageFileIdParts = Common.SplitBy(storageFileId, storageFileIdPropertyValueLen);
            if (storageFileIdParts.Length > 9) throw new Exception("Storage file ID is too long");
            for (int i = 0; i < storageFileIdParts.Length; i++)
            {
                props.Add(new Property { Key = string.Format(StorageFileIdFormat, Storage.Kind, i), Value = storageFileIdParts[i], Visibility = "PRIVATE" });
            }

            var file = new File
            {
                Properties = props,
                Title = System.IO.Path.GetFileName(pathName),
                Parents =
                    new List<ParentReference> { new ParentReference { Id = destFolder == null ? RootId : destFolder.Id } }
            };

            using (var stream = new System.IO.FileStream(pathName, System.IO.FileMode.Open))
            {
                System.IO.Stream scaled;
                ImageType imageType;
                if (TryGetImageType(pathName, out imageType))
                    scaled = await ScaleImageAsync(imageType, stream);
                else
                    scaled = stream;
                var observed = new ObserverStream(scaled);
                observed.PositionChanged += (sender, e) =>
                {
                    ;
                };
                var request = await service.Files.Insert(file, observed, "").UploadAsync(token);
                if (request.Status == UploadStatus.Failed)
                {
                    //TODO: Action if upload is failed
                    throw new Exception(request.Exception.Message);
                }
            }
            return new GoogleFile(this, file);
        }

        public override async Task<System.IO.Stream> ReadFileAsync(DriveFile file, CancellationToken token)
        {
            var service = await GetServiceAsync(token);
            var gFile = await service.Files.Get(file.Id).ExecuteAsync(token);
            if (String.IsNullOrEmpty(gFile.DownloadUrl)) return null;
            return await service.HttpClient.GetStreamAsync(gFile.DownloadUrl);
        }


#pragma warning disable 1998
        public override async Task<Image> GetThumbnailAsync(DriveFile file, CancellationToken token)
#pragma warning restore 1998
        {
            var gFile = ((GoogleFile) file).file;
            if (gFile.Thumbnail == null)
                throw new Exception("File has no thumbnail");
            var bytes = Convert.FromBase64String(gFile.Thumbnail.Image);
            return new Bitmap(new System.IO.MemoryStream(bytes));
        }

        public override DriveFile RootFolder
        {
            get { return rootFolder; }
        }

        private async Task<ICollection<DriveFile>> GetFilesAsync(string folderId, CancellationToken token)
        {
            return await GetFilesAsync(folderId, string.Format("mimeType != '{0}'", GoogleMimeTypes.Folder), token);
        }

        private async Task<ICollection<DriveFile>> GetFilesAsync(string folderId, string q, CancellationToken token)
        {
            try
            {
                var service = await GetServiceAsync(token);
                var request = service.Files.List();
                request.MaxResults = 1000;
                if (!string.IsNullOrWhiteSpace(q))
                    q = string.Format("and ({0})", q);
                request.Q = string.Format("'{0}' in parents and trashed = false {1}", folderId, q);
                request.Fields = "items(downloadUrl,webContentLink,thumbnailLink,id,mimeType,createdDate,modifiedDate,fileSize,title,properties)";
                var files = await request.ExecuteAsync(token);
                return await GoogleFileList.Get(this, files, service, token);
            }
            catch (TokenResponseException ex)
            {
                throw new ConnectionException(Account, ex.Message, ex);
            }
        }

        public override async Task<ICollection<DriveFile>> GetFilesAsync(DriveFile folder, CancellationToken token)
        {
            return await GetFilesAsync(folder.Id, token);
        }

        public override async Task<DriveFile> CreateFolderAsync(string folderName, DriveFile destFolder,
            CancellationToken token)
        {
            var folder = new File
            {
                Title = folderName,
                MimeType = GoogleMimeTypes.Folder,
                Parents = new[] {new ParentReference {Id = destFolder.Id}}
            };
            var service = await GetServiceAsync(token);
            var file = await service.Files.Insert(folder).ExecuteAsync(token);
            return new GoogleFile(this, file);
        }

        public override async Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile folder, CancellationToken token)
        {
            return await GetFilesAsync(folder.Id, string.Format("mimeType = '{0}'", GoogleMimeTypes.Folder), token);
        }

        public override async Task ClearAsync(CancellationToken token)
        {
            Debug.Assert(RootFolder.IsFolder);
            var service = await GetServiceAsync(token);
            var files = await GetFilesAsync(RootFolder.Id, "", token);
            foreach (var file in files)
                await service.Files.Delete(file.Id).ExecuteAsync(token);
        }

        public override async Task DeleteFolderAsync(DriveFile driveFolder, CancellationToken token)
        {
            Debug.Assert(driveFolder.IsFolder);
            var service = await GetServiceAsync(token);
            if (driveFolder.Id == RootId)
            {
                var files = await GetFilesAsync(RootId, "", token);
                foreach (var file in files)
                    await service.Files.Delete(file.Id).ExecuteAsync(token);
            }
            else
                await service.Files.Delete(driveFolder.Id).ExecuteAsync(token);
        }

        private static string GetAvailableFileName(string wishfulFileName)
        {
            if (!System.IO.File.Exists(wishfulFileName))
                return wishfulFileName;
            var dir = System.IO.Path.GetDirectoryName(wishfulFileName);
            var fn = System.IO.Path.GetFileNameWithoutExtension(wishfulFileName);
            var ext = System.IO.Path.GetExtension(wishfulFileName);
            var number = 1;
            var ret = string.Format("{0}{1} {2}{3}", dir, fn, number, ext);
            while (System.IO.File.Exists(ret))
            {
                number++;
                ret = string.Format("{0}{1} {2}{3}", dir, fn, number, ext);
            }
            return ret;
        }

        public override async Task DeleteFileAsync(DriveFile driveFile, CancellationToken token)
        {
            Debug.Assert(!driveFile.IsFolder);
            var service = await GetServiceAsync(token);
            await service.Files.Delete(driveFile.Id).ExecuteAsync(token);
        }

        public override async Task EnumerateFilesRecursive(DriveFile driveFolder, Action<DriveFile> action, CancellationToken token)
        {
            Debug.Assert(driveFolder.IsFolder);
            var files = await GetFilesAsync(driveFolder, token);
            foreach (var file in files)
            {
                if (file.IsFolder)
                    await EnumerateFilesRecursive(file, action, token);
                else
                {
                    var f = file;
                    await Task.Run(() => action(f), token);
                }
            }
        }

        public override async Task DownloadFileAsync(DriveFile driveFile, string destFolder, ActionIfFileExists actionIfFileExists, CancellationToken token)
        {
            Debug.Assert(!driveFile.IsFolder);
            var url = ((GoogleFile) driveFile).file.DownloadUrl;
            if (url == null)
                throw new TaskException("Can't download this file");
            var service = await GetServiceAsync(token);
            var stream = await service.HttpClient.GetStreamAsync(url);
            if (stream != null)
            {
                var fileName = Common.AppendToPath(destFolder, driveFile.Name);
                if (System.IO.File.Exists(fileName))
                {
                    switch (actionIfFileExists)
                    {
                        case ActionIfFileExists.Skip:
                            return;
                        case ActionIfFileExists.Rewrite:
                            fileName = GetAvailableFileName(fileName);
                            break;
                    }
                }
                using (var fileStream = System.IO.File.Create(fileName))
                {
                    var buffer = new byte[1000];
                    var n = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                    while (n > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, n, token);
                        n = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                    }
                }
            }
        }

        public override async Task<DriveFile> GetFileAsync(System.Xml.Linq.XElement xml, CancellationToken token)
        {
            var fileId = xml.Attribute("fileId").Value;
            var service = await GetServiceAsync(token);
            var request = service.Files.Get(fileId);
            return null;
        }
    }
}
