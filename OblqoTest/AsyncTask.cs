using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oblqo.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OblqoTest
{
    public class AsyncTask
    {
        [TestMethod]
        public async Task MyTestMethod()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var folder = await env.GetFolderByFullPathAsync("photos2015");
            var task = new DeleteFolderTask(env.Account, "", 0, new Oblqo.AsyncTask[0], folder);
            task.StartAsync();

        }
    }
}
