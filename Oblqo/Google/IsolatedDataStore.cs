using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo
{
    public class IsolatedDataStore : IDataStore
    {
        private string path;

        public IsolatedDataStore(string path)
        {
            this.path = path;
        }
        
        public async Task ClearAsync()
        {
            await CreateFolderIfNotExists();
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                await DeleteAllFilesInDirectoryAsync(store, path);
            }
        }

        public async Task DeleteAsync<T>(string key)
        {
            await CreateFolderIfNotExists();
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                await Task.Run(() =>
                {
                    store.DeleteFile(GetPath(key));
                });
            }
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var path = GetPath(key);
            await CreateFolderIfNotExists();
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                if (!store.FileExists(path))
                {
                    return default(T);
                }
                return await Task.Run(() =>
                {
                    using (var stream = store.OpenFile(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
                    {
                        System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
                        return (T)ser.Deserialize(stream);
                    }
                });
            }
        }

        public async Task StoreAsync<T>(string key, T value)
        {
            var path = GetPath(key);
            await CreateFolderIfNotExists();
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                await Task.Run(() =>
                {
                    using (var stream = store.OpenFile(path, System.IO.FileMode.Create))
                    {
                        System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
                        ser.Serialize(stream, value);
                    }
                });
            }
        }

        public string GetPath(string file)
        {
            return path + "/" + file;
        }

        private async Task CreateFolderIfNotExists()
        {
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                await Task.Run(() =>
                {
                    if (store.DirectoryExists(path))
                    {
                        return;
                    }
                    store.CreateDirectory(path);
                });
            }
        }

        private async Task DeleteAllFilesInDirectoryAsync(IsolatedStorageFile store, string path)
        {
            await Task.Run(() =>
            {
                if (!store.DirectoryExists(path))
                {
                    return;
                }
                var files = store.GetFileNames(path + "/*");
                foreach (var file in files)
                {
                    store.DeleteFile(path + "/" + file);
                }
            });
        }
        
    }
}
