using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Http;

namespace Oblakoo.Google
{
    public class GoogleDrive : Drive
    {
        public string AccessKeyId { get; set; }
        public string AccessSecretKey { get; set; }
        public ClientSecrets Secrets { get; set; }

        public const string RootId = "root";

        private readonly GoogleFile rootFolder;

        public GoogleDrive(ClientSecrets secrets, string rootPath)
        {
            Secrets = secrets;
            rootFolder = GetFolderByPath(rootPath);
        }

        private File GetFolder(string parentFolderId, string folderName)
        {
            var request = GetServiceAsync(CancellationToken.None).Result.Files.List();
            request.Q = string.Format("'{0}' in parents and trashed = false and title = '{1}'", parentFolderId, folderName);
            request.Fields = "items(id,mimeType,createdDate,modifiedDate,fileSize,title,properties)";
            var result = request.Execute();
            if (result.Items.Count == 0)
                return null;
            if (result.Items.Count > 1)
                throw new Exception("Requst returns more then one folder");
            return result.Items[0];
        }

        private GoogleFile GetFolderByPath(string path)
        {
            var folders = path.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            var file = new File {Id = RootId, MimeType = GoogleMimeTypes.Folder};
            var currentPath = "";
            foreach (var f in folders)
            {
                currentPath += "/" + f;
                file = GetFolder(file.Id, f);
                if (file == null)
                    throw new Exception("Path is not exists: " + currentPath);
            }
            return new GoogleFile(file);
        }

        internal async Task<DriveService> GetServiceAsync(CancellationToken token)
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(Secrets, new[] { DriveService.Scope.Drive }, "user", token);
            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Oblakoo"
            });
        }

        public override async Task<DriveFile> UploadFileAsync(string pathName, DriveFile destFolder, string storageFileId, CancellationToken token)
        {
            Debug.Assert(System.IO.File.Exists(pathName) &&
                         !System.IO.File.GetAttributes(pathName).HasFlag(System.IO.FileAttributes.Directory));
            var service = await GetServiceAsync(token);
            var file = new File
            {
                Properties = new List<Property>
                {
                    new Property { Key=GoogleFile.StorageFileIdPropertyKey, Value=storageFileId }
                },
                Title = System.IO.Path.GetFileName(pathName),
                Parents =
                    new List<ParentReference> { new ParentReference { Id = destFolder == null ? RootId : destFolder.Id } }
            };
            var stream = new System.IO.FileStream(pathName, System.IO.FileMode.Open);
            var request = await service.Files.Insert(file, stream, "").UploadAsync(token);

            if (request.Status == UploadStatus.Failed)
            {
                //TODO: Action if upload is failed
            }
            return new GoogleFile(file);
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
            var service = await GetServiceAsync(token);
            var request = service.Files.List();
            request.MaxResults = 1000;
            if (!string.IsNullOrWhiteSpace(q))
                q = string.Format("and ({0})", q);
            request.Q = string.Format("'{0}' in parents and trashed = false {1}", folderId, q);
            request.Fields = "items(downloadUrl,webContentLink,thumbnailLink,id,mimeType,createdDate,modifiedDate,fileSize,title,properties)";
            var files = await request.ExecuteAsync(token);
            return await GoogleFileList.Get(files, service, token);
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
            return new GoogleFile(file);
        }

        public override async Task<ICollection<DriveFile>> GetSubfoldersAsync(DriveFile folder, CancellationToken token)
        {
            return await GetFilesAsync(folder.Id, string.Format("mimeType = '{0}'", GoogleMimeTypes.Folder), token);
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
                var fileName = destFolder + System.IO.Path.DirectorySeparatorChar + driveFile.Name;
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
    }
}
