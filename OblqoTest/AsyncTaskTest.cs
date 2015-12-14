using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oblqo;
using Oblqo.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OblqoTest
{
    [TestClass]
    public class AsyncTaskTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task TestDeleteFolderTask()
        {
            var man = new AsyncTaskManager(new MockConfigurationStorage());
            var env = await TestEnvironment.CreateSimpleAsync();
            var folder = await env.GetFolderByFullPathAsync("photos2015");
            var task = new DeleteFolderTask(env.Account, "", 0, new AsyncTask[0], folder);
            man.Add(task);
            // throws exception becouse directory isn't exists
            await env.GetFolderByFullPathAsync("photos2015");
        }

        [TestMethod]
        public async Task TestCreateFolderTask()
        {
            var man = new AsyncTaskManager(new MockConfigurationStorage());
            var env = await TestEnvironment.CreateSimpleAsync();
            var folder = await env.GetFolderByFullPathAsync("photos2015");
            var task = new CreateFolderTask(env.Account, "", 0, new AsyncTask[0], "new", folder);
            man.Add(task);
            task.CompleteWaitHandle.WaitOne();
            var newFolder = await env.GetFolderByFullPathAsync("photos2015/new");
            Assert.AreEqual("new", newFolder.Name);
        }

    }
}
