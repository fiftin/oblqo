using Oblqo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace OblqoTest
{
    class MockConfigurationStorage : ConfigurationStorage
    {
        public override void DeleteTask(AsyncTask task)
        {
        }

        public override Task<IEnumerable<AsyncTask>> GetTasksAsync(Account account, string accountName, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public override void SaveTask(AsyncTask task)
        {
        }
    }
}
