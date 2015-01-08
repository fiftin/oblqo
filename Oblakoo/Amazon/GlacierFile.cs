using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblakoo.Amazon
{
    public class GlacierFile : StorageFile
    {
        private readonly string id;
        private readonly bool isFolder;
        private readonly string name;

        public GlacierFile(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public GlacierFile(string id, bool isFolder, string folderPath)
        {
            this.id = id;
            this.isFolder = isFolder;
            this.FolderPath = folderPath;
        }

        public override string Id
        {
            get { return id; }
        }

        public override string Name
        {
            get { return name; }
        }

        public override bool IsFolder
        {
            get { return isFolder; }
        }

        public string FolderPath { get; private set; }
    }
}
