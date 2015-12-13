using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo
{
    public abstract class ConfigurationStorage
    {
        public abstract void DeleteTask(AsyncTask task);
        public abstract void SaveTask(AsyncTask task);
    }
}
