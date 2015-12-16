using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public abstract class ConfigurationStorage
    {
        public abstract void DeleteTask(AsyncTask task);
        public abstract void SaveTask(AsyncTask task);
        public abstract Task<IEnumerable<AsyncTask>> GetTasksAsync(Account account, string accountName, System.Threading.CancellationToken token);
        protected virtual void OnError(Exception ex)
        {
            Error?.Invoke(this, new ExceptionEventArgs(ex));
        }
        public event EventHandler<ExceptionEventArgs> Error;
    }
}