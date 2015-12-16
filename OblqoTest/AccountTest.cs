using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oblqo;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace OblqoTest
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public async Task TestCreateFolderAsync()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var folder = await env.GetFolderByFullPathAsync("photos2015");
            var newFolder = await env.Account.CreateFolderAsync("november", folder, CancellationToken.None);
            await env.Account.CreateFolderAsync("At Home", newFolder, CancellationToken.None);
            await env.Account.CreateFolderAsync("At Street", newFolder, CancellationToken.None);
            var folders = await env.Account.GetSubfoldersAsync(newFolder, CancellationToken.None);
            Assert.AreEqual(2, folders.Count);
            folders.Single(x => x.Name == "At Home");
            folders.Single(x => x.Name == "At Street");
        }

        [TestMethod]
        public async Task TestDeleteFolderAsync()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var folder = await env.GetFolderByFullPathAsync("photos2015");
            await env.Account.DeleteFolderAsync(folder, CancellationToken.None);
            var folders = await env.Account.GetSubfoldersAsync(env.Account.RootFolder, CancellationToken.None);
            Assert.IsNull(folders.FirstOrDefault(x => x.Name == "photos2015"));
        }

        [TestMethod]
        public async Task TestDownloadFileFromStorageAsync()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var file = await env.GetFileByFullPathAsync("photos2015/info.txt");
            var stream = new MemoryStream();
            await env.Account.DownloadFileFromStorageAsync(file, stream, CancellationToken.None, (x) => { });
            stream.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, 0, buffer.Length);
            var content = Encoding.ASCII.GetString(buffer);
            Assert.AreEqual("Hello, World!", content);
        }


        [TestMethod]
        public async Task TestDownloadFileFromDriveAsync()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var file = await env.GetFileByFullPathAsync("photos2015/info.txt");
            var stream = new MemoryStream();
            await env.Account.DownloadFileFromDriveAsync(file, stream, CancellationToken.None);
            stream.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, 0, buffer.Length);
            var content = Encoding.ASCII.GetString(buffer);
            Assert.AreEqual("Hello, World!", content);
        }

        [TestMethod]
        public async Task TestGetFilesAsync()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var folder = await env.GetFolderByFullPathAsync("photos2015");
            var files = await env.Account.GetFilesAsync(folder, CancellationToken.None);
            Assert.AreEqual(4, files.Count);
        }

        [TestMethod]
        public async Task TestUploadFile()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var folder = await env.GetFolderByFullPathAsync("photos2015");

        }


        [TestMethod]
        public async Task TestGetFileAsync()
        {
            var env = await TestEnvironment.CreateSimpleAsync();
            var storageXml = new XElement("storageFile");
            storageXml.SetAttributeValue("name", "test.txt");
            var driveXmls = new XElement("driveFiles");
            var fileXml = new XElement("file");
            fileXml.Add(storageXml);
            fileXml.Add(driveXmls);
            var file = await env.Account.GetFileAsync(fileXml, CancellationToken.None);

        }

    }
}
