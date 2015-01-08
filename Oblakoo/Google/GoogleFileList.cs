using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;

namespace Oblakoo.Google
{
    public class GoogleFileList : ICollection<DriveFile>
    {
        private readonly List<GoogleFile> files = new List<GoogleFile>();


        public static async Task<bool> HasSubfoldersAsync(DriveFile folder, DriveService service, CancellationToken token)
        {
            var request = service.Files.List();
            request.MaxResults = 1;
            request.Q = string.Format("'{0}' in parents and trashed = false and mimeType = '{1}'", folder.Id, GoogleMimeTypes.Folder);
            request.Fields = "items(id, mimeType)";
            var files = await request.ExecuteAsync(token);
            return files.Items.Count > 0;
        }

        public static async Task<GoogleFileList> Get(FileList fileList, DriveService service, CancellationToken token)
        {
            var ret = new GoogleFileList();
            foreach (var googleFile in fileList.Items.Select(file => new GoogleFile(file)))
            {
                if (googleFile.IsFolder)
                    googleFile.hasChildren = await HasSubfoldersAsync(googleFile, service, token);
                ret.files.Add(googleFile);
            }
            return ret;
        }

        public static async Task<GoogleFileList> Get(ChildList fileList, DriveService service, CancellationToken token)
        {
            var ret = new GoogleFileList();
            foreach (var child in fileList.Items)
            {
                var file = await service.Files.Get(child.Id).ExecuteAsync(token);
                var googleFile = new GoogleFile(file);
                ret.files.Add(googleFile);
            }
            return ret;
        }

        public IEnumerator<DriveFile> GetEnumerator()
        {
            return files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(DriveFile item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(DriveFile item)
        {
            return files.Contains((GoogleFile)item);
        }

        public void CopyTo(DriveFile[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(DriveFile item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return files.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }
    }
}
