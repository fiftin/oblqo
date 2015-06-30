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
using Oblakoo.Amazon;
using Oblakoo.Google;
using Oblakoo.Properties;

namespace Oblakoo
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
                Accounts.Remove(account);
        }


        public List<AccountInfo> Accounts
        {
            get { return accounts; }
        }

        public AccountInfo Get(string name)
        {
            return Accounts.FirstOrDefault(a => a.AccountName == name);
        }

        public static AccountManager Load(XmlReader reader)
        {
            var serializer = new XmlSerializer(typeof(AccountManager));
            return (AccountManager)serializer.Deserialize(reader);
        }

        public void Save(XmlWriter writer)
        {
            var serializer = new XmlSerializer(typeof(AccountManager));
            serializer.Serialize(writer, this);
        }

        public async Task<Account> CreateAccountAsync(AccountInfo info)
        {
            Drive drive;
            var token = new CancellationToken();
            switch (info.DriveType)
            {
                case DriveType.GoogleDrive:
                    drive = await GoogleDrive.CreateInstance(GoogleClientSecrets.Load(new MemoryStream(Resources.client_secret)).Secrets, info.DriveRootPath);
                    drive.ImageMaxSize = info.DriveImageMaxSize;
                    ((GoogleDrive) drive).GetServiceAsync(token).Wait(token);
                    break;
                default:
                    throw new NotSupportedException("Drive with this type is not supported");
            }
            return new Account(new Glacier(info.StorageVault, info.StorageRootPath, info.StorageAccessKeyId, info.StorageSecretAccessKey, info.StorageRegionEndpoint), drive);
        }

        public async Task<Account> CreateAccountAsync(string name)
        {
            return await CreateAccountAsync(Get(name));
        }
    }
}
