using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public class IsolatedConfigurationStorage : ConfigurationStorage
    {
        public override void DeleteTask(AsyncTask task)
        {
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                var tasksPath = "accounts/" + task.AccountName + "/tasks/";
                if (store.FileExists(tasksPath + task.ID))
                {
                    store.DeleteFile(tasksPath + task.ID);
                }
            }
        }

        public override async Task<IEnumerable<AsyncTask>> GetTasksAsync(Account account, string accountName, CancellationToken token)
        {
            var ret = new List<AsyncTask>();
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                if (!store.DirectoryExists("accounts/" + accountName + "/tasks"))
                {
                    return ret;
                }
                var fileNames = store.GetFileNames("accounts/" + accountName + "/tasks/*");
                foreach (var filename in fileNames)
                {
                    try
                    {
                        using (var stream = store.OpenFile("accounts/" + accountName + "/tasks/" + filename, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            var xml = XDocument.Load(stream).Root;
                            var task = await AsyncTask.LoadFromXmlAsync(account, filename, xml, token);
                            ret.Add(task);
                        }
                    }
                    catch (Exception ex)
                    {
                        store.DeleteFile("accounts/" + accountName + "/tasks/" + filename);
                        OnError(ex);
                    }
                }
            }
            return ret;
        }

        public override void SaveTask(AsyncTask task)
        {
            var accountName = task.AccountName;
            var tasksPath = "accounts/" + accountName + "/tasks/";
            switch (task.State)
            {
                case AsyncTaskState.Cancelled:
                case AsyncTaskState.Completed:
                case AsyncTaskState.Error:
                    using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
                    {
                        if (store.FileExists(tasksPath + task.ID))
                        {
                            store.DeleteFile(tasksPath + task.ID);
                        }
                    }
                    return;
            }
            using (var store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                if (!store.DirectoryExists(tasksPath))
                {
                    store.CreateDirectory(tasksPath);
                }
                var doc = new XDocument();
                doc.Add(task.ToXml());
                using (var stream = store.CreateFile(tasksPath + task.ID))
                {
                    doc.Save(stream);
                }
            }
        }
    }
}
