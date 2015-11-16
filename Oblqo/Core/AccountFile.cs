namespace Oblqo
{
    public class AccountFile
    {
        public DriveFileCollection DriveFile { get; }
        public StorageFile StorageFile { get; }
        public AccountFile Parent { get; }
        public string Name => DriveFile != null ? DriveFile.Name : StorageFile?.Name;

        public bool IsImage => DriveFile != null && DriveFile.IsImage;

        public bool IsFolder => (DriveFile != null && DriveFile.IsFolder) || (StorageFile != null && StorageFile.IsFolder);

        public bool HasChildren => DriveFile != null && DriveFile.HasChildren;

        public AccountFile(StorageFile storageFile, DriveFileCollection driveFile, AccountFile parent)
        {
            StorageFile = storageFile;
            DriveFile = driveFile;
            Parent = parent;
        }

    }
}
