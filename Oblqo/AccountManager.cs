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
using Amazon.Route53Domains.Model;
using Google.Apis.Auth.OAuth2;
using Oblqo.Amazon;
using Oblqo.Google;
using Oblqo.Properties;

namespace Oblqo
{
    [XmlRoot("root")]
    public class AccountManager
    {
        private List<AccountInfo> accounts = new List<AccountInfo>();

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
            if (account != null)
            {
                accounts.Remove(account);
                using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
                {
                    DeleteAllFilesInDirectory(store, "accounts/" + account.AccountName);
                    DeleteAllFilesInDirectory(store, "accounts/" + account.AccountName + "/tasks");
                    store.DeleteDirectory("accounts/" + account.AccountName);
                }
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

        public IEnumerable<AccountInfo> Accounts
        {
            get { return accounts; }
        }

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
                    catch (Exception)
                    {
                    }
                }
            }
            return ret;
        }

        public void Save()
        {
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                foreach (var account in accounts)
                {
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

        public async Task<Account> CreateAccountAsync(AccountInfo info)
        {
            var drives = new DriveCollection();
            var storage = new Glacier(info.StorageVault, info.StorageRootPath, info.StorageAccessKeyId, info.StorageSecretAccessKey, info.StorageRegionEndpoint);
            var token = new CancellationToken();
            var account = new Account();
            switch (info.DriveType)
            {
                case DriveType.GoogleDrive:
                    var drive =
                        await
                            GoogleDrive.CreateInstance(storage, account,
                                GoogleClientSecrets.Load(new MemoryStream(Resources.client_secret)).Secrets,
                                info.DriveRootPath, token);
                    drive.ImageMaxSize = info.DriveImageMaxSize;
                    await ((GoogleDrive)drive).GetServiceAsync(token);
                    drives.Add(drive);
                    break;
                case DriveType.LocalDrive:
                    var localDrive = new Local.LocalDrive(storage, account, info.DriveRootPath)
                    {
                        ImageMaxSize = info.DriveImageMaxSize
                    };
                    drives.Add(localDrive);
                    break;
                default:
                    throw new NotSupportedException("Drive with this type is not supported");
            }
            account.Init(storage, drives);
            return account;
        }

        public async Task<Account> CreateAccountAsync(string name)
        {
            return await CreateAccountAsync(Get(name));
        }
    }
}
