using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblakoo.Tasks
{
    public class EmptyTask : AsyncTask
    {
        public EmptyTask() { }

        public EmptyTask(Account account, string accountName, int priority, AsyncTask[] parents, AsyncTaskParentsMode parentsMode = AsyncTaskParentsMode.CancelIfAnyErrorOrCanceled)
            : base(account, accountName, priority, parents, parentsMode)
        {

        }

        protected override async Task OnStartAsync() { }
    }
}
