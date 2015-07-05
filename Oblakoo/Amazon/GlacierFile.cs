using System.Xml.Linq;
namespace Oblakoo.Amazon
{
    public class GlacierFile : StorageFile
    {
        private readonly string id;
        private readonly bool isFolder;
        private readonly string name;
        private readonly bool isRoot;
        private string jobId;
        internal Glacier storage;

        /// <summary>
        /// Create file (not folder) instance.
        /// Create folder or file instance.
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <param name="isFolder"></param>
        /// <param name="pathName"></param>
        /// <param name="isRoot"></param>
        public GlacierFile(Glacier storage, string id, bool isFolder, string pathName, bool isRoot = false)
            : base(storage)
        {
            this.id = id;
            this.isFolder = isFolder;
            FolderPath = pathName;
            var lastSlashIndex = pathName.LastIndexOf('/');
            name = lastSlashIndex >= 0 ? pathName.Substring(lastSlashIndex + 1) : pathName;
            this.isRoot = isRoot;
        }

        //public override string Id
        //{
        //    get { return id; }
        //}

        public override string Name
        {
            get { return name; }
        }

        public override bool IsFolder
        {
            get { return isFolder; }
        }

        public override bool IsRoot
        {
            get { return isRoot; }
        }

        public string JobId
        {
            get
            {
                return jobId;
            }
            set
            {
                jobId = value;
            }
        }

        public string FolderPath { get; private set; }

        public override XElement ToXml()
        {
            var ret = base.ToXml();
            ret.SetAttributeValue("id", id);
            ret.SetAttributeValue("isFolder", IsFolder);
            ret.SetAttributeValue("folderPath", FolderPath);
            ret.SetAttributeValue("isRoot", IsRoot);
            ret.SetAttributeValue("jobId", JobId);
            return ret;
        }

        public override string Id
        {
            get { return id; }
        }
    }
}
