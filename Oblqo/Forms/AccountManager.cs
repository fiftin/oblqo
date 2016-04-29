using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Google.Apis.Auth.OAuth2;
using Oblqo.Amazon;
using Oblqo.Google;
using Oblqo.Properties;
using System.Xml.Linq;

namespace Oblqo
{
    [XmlRoot("root")]
    public class AccountManager
    {
        private List<AccountInfo> accounts = new List<AccountInfo>();
        private List<Exception> errors = new List<Exception>();

        public AccountManager()
        {
        }

        public void Add(AccountInfo account)
        {
            accounts.Add(account);
        }

        public void Remove(string name)
        {
            var account = Get(name);
            if (account == null) return;
            accounts.Remove(account);
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                DeleteAllFilesInDirectory(store, "accounts/" + account.AccountName);
                DeleteAllFilesInDirectory(store, "accounts/" + account.AccountName + "/tasks");
                store.DeleteDirectory("accounts/" + account.AccountName);
            }
        }

        private void DeleteAllFilesInDirectory(IsolatedStorageFile store, string path)
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
        }

        public IEnumerable<AccountInfo> Accounts => accounts;

        public AccountInfo Get(string name)
        {
            return Accounts.FirstOrDefault(a => a.AccountName == name);
        }

        public static AccountManager Load()
        {
            var ret = new AccountManager();
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                var accountDirs = store.GetDirectoryNames("accounts/*");
                foreach (var dir in accountDirs)
                {
                    try
                    {
                        using (var stream = store.OpenFile("accounts/" + dir + "/settings.xml", FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            var serializer = new XmlSerializer(typeof(AccountInfo));
                            var account = (AccountInfo)serializer.Deserialize(stream);
                            ret.Add(account);
                        }
                    }
                    catch (Exception ex)
                    {
                        ret.OnError(ex);
                    }
                }
            }
            return ret;
        }

        private void OnError(Exception ex)
        {
            errors.Add(ex);
        }

        public void Save()
        {
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                foreach (var account in accounts)
                {
                    if (account.OldAccountName != null)
                    {
                        var oldPath = "accounts/" + account.OldAccountName;
                        DeleteAllFilesInDirectory(store, oldPath);
                        try
                        {
                            store.DeleteDirectory(oldPath);
                        }
                        catch (Exception ex)
                        {
                            OnError(ex);
                        }
                    }
                    var path = "accounts/" + account.AccountName;
                    if (!store.DirectoryExists(path))
                    {
                        store.CreateDirectory(path);
                    }
                    using (var stream = store.OpenFile(path + "/settings.xml", FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        var serializer = new XmlSerializer(typeof(AccountInfo));
                        serializer.Serialize(new StreamWriter(stream), account);
                    }
                }
            }
        }

        public async Task ClearAuthAsync(AccountInfo info)
        {
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                await Task.Run(() =>
                {
                    var dirs = store.GetDirectoryNames("accounts/" + info.AccountName + "/drive-credentials/*");
                    foreach (var dir in dirs)
                    {
                        DeleteAllFilesInDirectory(store, "accounts/" + info.AccountName + "/drive-credentials/" + dir);
                    }
                });
            }
        }

        public async Task<Account> CreateAccountAsync(AccountInfo info)
        {
            var token = new CancellationToken();
            var storage = new Glacier(info.StorageVault, info.StorageRootPath, info.StorageAccessKeyId, info.StorageSecretAccessKey, info.StorageRegionEndpoint);
            await storage.InitAsync(token);
            var account = new Account(storage);
            var accountCredPath = "accounts/" + info.AccountName + "/drive-credentials/";
            var accountInventoryPath = "accounts/" + info.AccountName + "/glacier-inventory.xml";
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                if (store.FileExists(accountInventoryPath))
                {
                    using (var input = store.OpenFile(accountInventoryPath, FileMode.Open))
                    {
                        var reader = new StreamReader(input);
                        var xml = await reader.ReadToEndAsync();
                        var doc = XDocument.Parse(xml);
                        var glacierDrive = new GlacierPseudoDrive(account, "inventory", doc);
                    }
                }
            }
            foreach (var d in info.Drives)
            {
                switch (d.DriveType)
                {
                    case DriveType.GoogleDrive:

                        var drive =
                            await
                                GoogleDrive.CreateInstance(account, d.DriveId,
                                    GoogleClientSecrets.Load(new MemoryStream(Resources.client_secret)).Secrets,
                                    d.DriveRootPath,
                                    accountCredPath + d.DriveId,
                                    token);

                        drive.ImageMaxSize = d.DriveImageMaxSize;
                        await drive.GetServiceAsync(token);
                        account.Drives.Add(drive);
                        break;
                    case DriveType.LocalDrive:
                        var localDrive = new Local.LocalDrive(account, d.DriveId, d.DriveRootPath)
                        {
                            ImageMaxSize = d.DriveImageMaxSize
                        };
                        account.Drives.Add(localDrive);
                        break;
                    default:
                        throw new NotSupportedException("Drive with this type is not supported");
                }
            }
            return account;
        }

        public async Task<Account> CreateAccountAsync(string name)
        {
            return await CreateAccountAsync(Get(name));
        }
    }
}
