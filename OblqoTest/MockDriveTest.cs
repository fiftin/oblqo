using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oblqo;
using System.Threading;
using System.Threading.Tasks;

namespace OblqoTest
{
    [TestClass]
    public class MockDriveTest
    {
        [TestMethod]
        public async Task TestInit()
        {
            var account = new Account(null);
            var drive = new MockDrive(account);
            var root = new MockDriveFile(drive, "c:", isFolder: true);
            var docs = root.Add(new MockDriveFile(drive, "documents", isFolder: true));
            docs.Add(new MockDriveFile(drive, "resume.txt"));
            docs.Add(new MockDriveFile(drive, "my-photo.jpg"));
            var photos = root.Add(new MockDriveFile(drive, "photos", isFolder: true));
            var photos2015 = photos.Add(new MockDriveFile(drive, "photos2015", isFolder: true));
            var lenovo = photos.Add(new MockDriveFile(drive, "lenovo", isFolder: true));
            var turkey = photos.Add(new MockDriveFile(drive, "turkey", isFolder: true));
            photos.Add(new MockDriveFile(drive, "PHOTO1.jpg", isImage: true));
            photos.Add(new MockDriveFile(drive, "PHOTO2.jpg", isImage: true));
            photos.Add(new MockDriveFile(drive, "PHOTO3.jpg", isImage: true));
            photos.Add(new MockDriveFile(drive, "info.txt"));
            drive.root = root;
            drive.rootFolder = photos;
            Assert.AreEqual(3, (await drive.GetSubfoldersAsync(photos, CancellationToken.None)).Count);
            Assert.AreEqual(4, (await drive.GetFilesAsync(photos, CancellationToken.None)).Count);
        }
    }
}
