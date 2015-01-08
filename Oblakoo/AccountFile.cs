
namespace Oblakoo
{
    public class AccountFile
    {


        public AccountFile(StorageFile storageFile, DriveFile driveFile)
        {
            StorageFile = storageFile;
            DriveFile = driveFile;
        }

        public DriveFile DriveFile { get; set; }
        public StorageFile StorageFile { get; set; }

        public bool IsImage
        {
            get { return DriveFile.IsImage; }
        }

        public bool IsFolder
        {
            get { return DriveFile.IsFolder; }
        }

        public string Name { get { return DriveFile.Name; } }
        public string Id { get { return DriveFile.Id; } }
        public bool HasChildren {
            get { return DriveFile.HasChildren; }
        }
    }
}
