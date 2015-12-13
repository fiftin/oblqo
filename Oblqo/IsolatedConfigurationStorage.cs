using System;
using System.Collections.Generic;
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
                store.DeleteFile(tasksPath + task.ID);
            }
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
