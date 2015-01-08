using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class UploadFolderTask : AsyncTask
    {
        public UploadFolderTask(AsyncTaskType type, object options, int priority, AsyncTask parent) : base(type, options, priority, parent)
        {
        }
    }
}
