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

            /// <summary>
            /// Locked until task completed.
            /// </summary>
            public readonly EventWaitHandle waiter;

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
                waiter = new ManualResetEvent(false);
                StateChanged += LockTask_StateChanged;
            }

            public LockTask(Account account = null,
                int priority = 10,
                AsyncTask[] parents = null,
                AsyncTaskParentsMode parentsMode = AsyncTaskParentsMode.CancelIfAnyErrorOrCanceled)
                : base(account, "", priority, parents ?? new AsyncTask[0], parentsMode)
            {
                this.delay = 0; // no delay
                locker = new AutoResetEvent(false);
                waiter = new ManualResetEvent(false);
                StateChanged += LockTask_StateChanged;
            }

            private void LockTask_StateChanged(object sender, EventArgs e)
            {
                if (((LockTask)sender).State == AsyncTaskState.Completed)
                {
                    waiter.Set();
                }
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
            var man = new AsyncTaskManager();
            // very long task
            var task = new LockTask(100000);
            man.Add(task, save: false);
            // wait short a time and check state
            Assert.IsFalse(task.waiter.WaitOne(100));
            Assert.AreNotEqual(AsyncTaskState.Completed, task.State);
            Assert.IsFalse(task.ok);
        }

        [TestMethod]
        public void ShouldBeCompletedAtTime()
        {
            var man = new AsyncTaskManager();
            var task = new LockTask();
            man.Add(task, save: false);
            Assert.IsFalse(task.ok);
            task.locker.Set();
            Assert.IsTrue(task.waiter.WaitOne(3000));
            Assert.IsTrue(task.ok);
            Assert.AreEqual(AsyncTaskState.Completed, task.State);
        }

        //[TestMethod]
        //public async Task ShouldBeCompletedСonsequentiallyAsync()
        //{
        //}

        [TestMethod]
        public async Task ShouldBeCompletedhHierarchically()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var createFolder = new LockTask(env.Account);
            var createSubfolder = new LockTask(env.Account, 10, new AsyncTask[] { createFolder });
            var uploadFile1 = new LockTask(env.Account, 10, new AsyncTask[] { createSubfolder });
            //var uploadFile2 = new LockTask(env.Account, 10, new AsyncTask[] { createSubfolder });
            //var deleteFile2 = new LockTask(env.Account, 10, new AsyncTask[] { uploadFile2 });

            var man = new AsyncTaskManager();

            man.Add(createFolder, save: false);
            man.Add(createSubfolder, save: false);
            man.Add(uploadFile1, save: false);
            //man.Add(uploadFile2, save: false);
            //man.Add(deleteFile2, save: false);

            createFolder.locker.Set();
            createSubfolder.locker.Set();
            uploadFile1.locker.Set();
            //uploadFile2.locker.Set();
            //deleteFile2.locker.Set();

            Assert.IsTrue(createSubfolder.waiter.WaitOne(3000));
            Assert.IsTrue(createFolder.ok);
        }
    }
}
