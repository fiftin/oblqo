using Oblqo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OblqoTest
{
    public class TestEnvironment
    {
        public Account Account { get; }

        public TestEnvironment(MockStorage storage, params MockDrive[] drives)
        {
            Account = new Account(storage);
            Account.Drives.AddRange(drives);
        }

        public TestEnvironment(MockStorage storage, IEnumerable<MockDrive> drives)
        {
            Account = new Account(storage);
            Account.Drives.AddRange(drives);
        }

        public static async Task<TestEnvironment> CreateTwoDrivesSimpleAsync()
        {
            var env = await CreateSimpleAsync();
            var drive2 = new MockDrive(env.Account);
            var root = new MockDriveFile(drive2, "/", isFolder: true);
            var home = new MockDriveFile(drive2, "home", isFolder: true);
            root.Add(home);
            var fiftin = new MockDriveFile(drive2, "fiftin", isFolder: true);
            home.Add(fiftin);
            var photos = new MockDriveFile(drive2, "photos", isFolder: true);
            fiftin.Add(photos);
            drive2.root = root;
            drive2.rootFolder = photos;
            env.Account.Drives.Add(drive2);
            return new TestEnvironment((MockStorage)env.Account.Storage, env.Account.Drives.Cast<MockDrive>());
        }

        /// <summary>
        /// Creates environmant with next drive FS structure:
        //  c:
        //      documents
        //          resume.txt
        //          my-photos.jpg
        //      photos
        //          photos2015
        //              lenovo
        //              turkey
        //              PHOTO1.jpg
        //              PHOTO2.jpg
        //              PHOTO3.jpg
        //              info.txt
        //
        //  Root directory: c:/photos.
        /// </summary>
        public static async Task<TestEnvironment> CreateSimpleAsync()
        {
            byte[] info_txt = Encoding.ASCII.GetBytes("Hello, World!");
            byte[] photo1_jpg = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var storage = new MockStorage("mock-1");
            storage.AddFile(new MockStorageFile(storage, "PHOTO1.jpg") { content = photo1_jpg });
            storage.AddFile(new MockStorageFile(storage, "info.txt") { content = info_txt });

            var account = new Account(storage);
            var drive = new MockDrive(account);
            var root = new MockDriveFile(drive, "c:", isFolder: true);

            var docs = root.Add(new MockDriveFile(drive, "documents", isFolder: true));
            docs.Add(new MockDriveFile(drive, "resume.txt"));
            docs.Add(new MockDriveFile(drive, "my-photo.jpg"));
            var photos = root.Add(new MockDriveFile(drive, "photos", isFolder: true));
            var photos2015 = photos.Add(new MockDriveFile(drive, "photos2015", isFolder: true));
            var lenovo = photos.Add(new MockDriveFile(drive, "lenovo", isFolder: true));
            var turkey = photos.Add(new MockDriveFile(drive, "turkey", isFolder: true));
            await photos2015.Add(new MockDriveFile(drive, "PHOTO1.jpg", isImage: true) { content = photo1_jpg }).SetStorageFileIdAsync("photo1-jpg", CancellationToken.None);
            var image2 = new MockDriveFile(drive, "PHOTO2.jpg", isImage: true);
            image2.content = new byte[] {
                1, 1, 1, 1, 1,
                0, 0, 0, 0, 0,
                1, 1, 1, 1, 1
            };
            photos2015.Add(image2); // unsync
            photos2015.Add(new MockDriveFile(drive, "PHOTO3.jpg", isImage: true)); // unsync
            await photos2015.Add(new MockDriveFile(drive, "info.txt") { content = info_txt }).SetStorageFileIdAsync("info-txt", CancellationToken.None);
            drive.root = root;
            drive.rootFolder = photos;
            return new TestEnvironment(storage, drive);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath"></param>
        public async Task<AccountFile> GetFileByFullPathAsync(string fullPath)
        {
            string[] path = fullPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            AccountFile folder = Account.RootFolder;
            foreach (var folderName in path.Take(path.Length - 1))
            {
                var subfolders = await Account.GetSubfoldersAsync(folder, CancellationToken.None);
                folder = subfolders.First(x => x.Name == folderName && x.IsFolder);
                if (folder == null)
                {
                    throw new ArgumentException("Directory doesn't exist: " + folderName);
                }
            }
            var fileName = path.Last();
            var file = (await Account.GetFilesAsync(folder, CancellationToken.None)).FirstOrDefault(x => x.Name == fileName && x.IsFile);
            if (file == null)
            {
                throw new ArgumentException("File doesn't exist: " + fileName);
            }
            return file;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath"></param>
        public async Task<AccountFile> GetFolderByFullPathAsync(string fullPath)
        {
            string[] path = fullPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            AccountFile folder = Account.RootFolder;
            foreach (var folderName in path)
            {
                var subfolders = await Account.GetSubfoldersAsync(folder, CancellationToken.None);
                folder = subfolders.FirstOrDefault(x => x.Name == folderName && x.IsFolder);
                if (folder == null)
                {
                    throw new ArgumentException("Directory doesn't exist: " + folderName);
                }
            }
            return folder;
        }
    }
}
