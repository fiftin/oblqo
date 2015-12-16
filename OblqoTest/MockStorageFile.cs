using Oblqo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OblqoTest
{
    public class MockStorageFile : StorageFile
    {
        public MockStorageFile(Storage storage, string name, bool isFolder = false, bool isRoot = false) : base(storage)
        {
            Id = FileNameToId(name);
            Name = name;
            IsFolder = isFolder;
            IsRoot = isRoot;
        }

        public override string Id { get; }

        public override bool IsFolder { get; }

        public override bool IsRoot { get; }

        public override string Name { get; }

        public static string FileNameToId(string name)
        {
            return name.Replace('.', '-').ToLower();
        }

        public override XElement ToXml()
        {
            var xml = base.ToXml();
            xml.SetAttributeValue("name", Name);
            return xml;
        }

        internal byte[] content = new byte[0];
    }
}
