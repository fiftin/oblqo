using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oblqo;
using Oblqo.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OblqoTest
{
    [TestClass]
    public class AsyncTaskManagerTest
    {

        class TestAsyncTask : AsyncTask
        {

            public bool ok = false;

            public TestAsyncTask()
            {

            }

            protected override async Task OnStartAsync()
            {
                ok = true;
            }
        }

        [TestMethod]
        public async Task TestOnceRun()
        {
            var man = new AsyncTaskManager();
            var task = new TestAsyncTask();
            man.Start();
            man.Add(task, save: false);
            await Task.Delay(100);
            man.Stop();
            Assert.AreEqual(AsyncTaskState.Completed, task.State);
            Assert.IsTrue(task.ok);
        }

        [TestMethod]
        public async Task Test()
        {

        }

    }
}
