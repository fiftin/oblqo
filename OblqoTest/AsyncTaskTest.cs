using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oblqo;
using Oblqo.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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


        [TestMethod]
        public async Task TestSyncronizeDriveFileTask(TestEnvironment env, SynchronizeDriveFileTask task)
        {
            var man = new AsyncTaskManager(new MockConfigurationStorage());
            man.Add(task);
            task.CompleteWaitHandle.WaitOne();
            var syncedFile = await env.GetFileByFullPathAsync("/photos2015/PHOTO2.jpg");
            Assert.AreEqual(2, syncedFile.DriveFiles.Count);
            Assert.AreEqual(((MockDriveFile)syncedFile.DriveFiles[0]).Name, ((MockDriveFile)syncedFile.DriveFiles[1]).Name);
            Assert.AreEqual(((MockDriveFile)syncedFile.DriveFiles[0]).content.ToString(), ((MockDriveFile)syncedFile.DriveFiles[1]).content.ToString());
        }

        [TestMethod]
        public async Task TestSyncronizeDriveFileTask()
        {
            var env = await TestEnvironment.CreateTwoDrivesSimpleAsync();
            await TestSyncronizeDriveFileTask(env, new SynchronizeDriveFileTask(env.Account, "", 10, new AsyncTask[0], await env.GetFileByFullPathAsync("/photos2015/PHOTO2.jpg")));
        }

        [TestMethod]
        public async Task TestSyncronizeDriveFileTaskFromXml()
        {
            var env = await TestEnvironment.CreateTwoDrivesSimpleAsync();
            var origTask = new SynchronizeDriveFileTask(env.Account, "", 10, new AsyncTask[0], await env.GetFileByFullPathAsync("/photos2015/PHOTO2.jpg"));
            var taskXml = origTask.ToXml();
            var task = new SynchronizeDriveFileTask();
            await task.LoadAsync(env.Account, "", taskXml, CancellationToken.None);
            await TestSyncronizeDriveFileTask(env, new SynchronizeDriveFileTask(env.Account, "", 10, new AsyncTask[0], await env.GetFileByFullPathAsync("/photos2015/PHOTO2.jpg")));
        }

        [TestMethod]
        public async Task TestDeleteEmptyFolderTask()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var folder = await env.GetFolderByFullPathAsync("/lenovo");
            var task = new DeleteEmptyFolderTask(env.Account, "", 10, new AsyncTask[0], folder);
            var man = new AsyncTaskManager(new MockConfigurationStorage());
            man.Add(task);
            task.CompleteWaitHandle.WaitOne();
            try
            {
                await env.GetFolderByFullPathAsync("/lenovo");
                Assert.Fail();
            }
            catch (ArgumentException) { }
        }

        [TestMethod]
        public async Task TestDeleteEmptyFolderTaskFromXml()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var folder = await env.GetFolderByFullPathAsync("/lenovo");
            var origTask = new DeleteEmptyFolderTask(env.Account, "", 10, new AsyncTask[0], folder);
            var taskXml = origTask.ToXml();
            var task = new DeleteEmptyFolderTask();
            await task.LoadAsync(env.Account, "", taskXml, CancellationToken.None);
            var man = new AsyncTaskManager(new MockConfigurationStorage());
            man.Add(task);
            task.CompleteWaitHandle.WaitOne();
            try
            {
                await env.GetFolderByFullPathAsync("/lenovo");
                Assert.Fail();
            }
            catch (ArgumentException) { }
        }
    }
}
