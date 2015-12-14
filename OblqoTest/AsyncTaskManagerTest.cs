using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oblqo;
using Oblqo.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OblqoTest
{
    [TestClass]
    public class AsyncTaskManagerTest
    {
        /// <summary>
        /// Fake task.
        /// </summary>
        class LockTask : AsyncTask
        {
            /// <summary>
            /// Task completed.
            /// </summary>
            public bool ok = false;

            /// <summary>
            /// Duration of the task.
            /// </summary>
            public readonly int delay;

            /// <summary>
            /// Lock OnStartAsync.
            /// </summary>
            public readonly EventWaitHandle locker;


            public LockTask(
                int delay,
                Account account = null,
                int priority = 10,
                AsyncTask[] parents = null,
                AsyncTaskParentsMode parentsMode = AsyncTaskParentsMode.CancelIfAnyErrorOrCanceled)
                : base(account, "", priority, parents ?? new AsyncTask[0], parentsMode)
            {
                this.delay = delay;
                locker = new ManualResetEvent(true); // always unlocked
            }

            public LockTask(Account account = null,
                int priority = 10,
                AsyncTask[] parents = null,
                AsyncTaskParentsMode parentsMode = AsyncTaskParentsMode.CancelIfAnyErrorOrCanceled)
                : base(account, "", priority, parents ?? new AsyncTask[0], parentsMode)
            {
                this.delay = 0; // no delay
                locker = new AutoResetEvent(false);
            }

            protected override async Task OnStartAsync()
            {
                await Task.Delay(delay);
                var tcs = new TaskCompletionSource<bool>();
                var rwh = ThreadPool.RegisterWaitForSingleObject(locker,
                    delegate {
                        tcs.TrySetResult(true);
                    }, null, -1, true);
                var task = tcs.Task;
                await task.ContinueWith((antecedent) => rwh.Unregister(null));
                ok = true;
            }
        }
        
        [TestMethod]
        public void ShouldBeAsync()
        {
            var man = new AsyncTaskManager(new MockConfigurationStorage());
            // very long task
            var task = new LockTask(100000);
            man.Add(task);
            // wait short a time and check state
            Assert.IsFalse(task.CompleteWaitHandle.WaitOne(100));
            Assert.AreNotEqual(AsyncTaskState.Completed, task.State);
            Assert.IsFalse(task.ok);
        }

        [TestMethod]
        public void ShouldBeCompletedAtTime()
        {
            var man = new AsyncTaskManager(new MockConfigurationStorage());
            var task = new LockTask();
            man.Add(task);
            Assert.IsFalse(task.ok);
            task.locker.Set();
            Assert.IsTrue(task.CompleteWaitHandle.WaitOne());
            Assert.IsTrue(task.ok);
            Assert.AreEqual(AsyncTaskState.Completed, task.State);
        }

        [TestMethod]
        public async Task ShouldBeCompletedСonsequentiallyAsync()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var man = new AsyncTaskManager(new MockConfigurationStorage());
            man.MaxNumberOfTasksRunning = 1;
            List<LockTask> tasks = new List<LockTask>();
            for (int i = 0; i < 10; i++)
            {
                var newTask = new LockTask(env.Account);
                tasks.Add(newTask);
                man.Add(newTask);
            }
            for (var i = 0; i < 10; i++)
            {
                var currentTask = tasks[i];
                currentTask.locker.Set();
                Assert.IsTrue(currentTask.CompleteWaitHandle.WaitOne());
                Assert.AreEqual(AsyncTaskState.Completed, currentTask.State);
                for (var k = i + 2; k < 10; k++)
                {
                    var task = tasks[k];
                    Assert.AreEqual(AsyncTaskState.Waiting, task.State);
                }
            }
        }

        [TestMethod]
        public async Task ShouldBeCompletedhHierarchically()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var createFolder = new LockTask(env.Account);
            var createSubfolder = new LockTask(env.Account, 10, new AsyncTask[] { createFolder });
            var uploadFile1 = new LockTask(env.Account, 10, new AsyncTask[] { createSubfolder });
            var uploadFile2 = new LockTask(env.Account, 10, new AsyncTask[] { createSubfolder });
            var deleteFile2 = new LockTask(env.Account, 10, new AsyncTask[] { uploadFile2 });

            var man = new AsyncTaskManager(new MockConfigurationStorage());

            man.Add(createFolder);
            man.Add(createSubfolder);
            man.Add(uploadFile1);
            man.Add(uploadFile2);
            man.Add(deleteFile2);

            createFolder.locker.Set();

            Assert.IsFalse(createSubfolder.ok);
            createSubfolder.locker.Set();

            Assert.IsFalse(uploadFile1.ok);
            uploadFile1.locker.Set();

            Assert.IsFalse(uploadFile2.ok);
            uploadFile2.locker.Set();

            Assert.IsFalse(deleteFile2.ok);
            deleteFile2.locker.Set();

            Assert.IsTrue(deleteFile2.CompleteWaitHandle.WaitOne());
            Assert.IsTrue(createFolder.ok);
            Assert.IsTrue(createSubfolder.ok);
            Assert.IsTrue(uploadFile1.ok);
            Assert.IsTrue(uploadFile2.ok);
            Assert.IsTrue(deleteFile2.ok);
        }
    }
}
