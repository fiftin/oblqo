using Oblqo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OblqoTest
{
    class MockConfigurationStorage : ConfigurationStorage
    {
        public override void DeleteTask(AsyncTask task)
        {
        }

        public override void SaveTask(AsyncTask task)
        {
        }
    }
}
