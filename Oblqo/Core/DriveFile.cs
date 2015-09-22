using System;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Linq;
using System.Collections.Generic;

namespace Oblqo
{
    public abstract class DriveFile
    {
        public abstract string Id { get; }
        public abstract bool IsImage { get; }
        public abstract bool IsFolder { get; }
        public abstract string Name { get; }
        public abstract bool HasChildren { get; }
        public abstract long Size { get; }
        public abstract DateTime ModifiedDate { get; }
        public abstract DateTime CreatedDate { get; }

       // public abstract string StorageFileId { get; }
        public abstract int OriginalImageWidth { get; set; }
        public abstract int OriginalImageHeight { get; set; }
        public abstract long OriginalSize { get; set; }

        public abstract int ImageWidth { get; }
        public abstract int ImageHeight { get; }
        public abstract bool IsRoot { get; }
        public abstract string MimeType { get; }

        public Drive Drive { get; private set; }

        public DriveFile(Drive drive)
        {
            Drive = drive;
        }

        public virtual XElement ToXml()
        {
            var ret = new XElement(IsFolder ? "driveFolder" : "driveFile");
            ret.SetAttributeValue("id", Id);
            ret.SetAttributeValue("isFolder", IsFolder);
            ret.SetAttributeValue("storageFileId", StorageFileId);
            return ret;
        }

        public async Task ScaleImageAsync()
        {
        }

        public abstract void SetAttribute(string name, string value);
        public abstract string GetAttribute(string name);


        private string GetNewSource(List<string> sources)
        {
            var ret = Drive.Storage.Kind;
            if (sources.Count == 0)
            {
                return ret;
            }
            sources.Sort();
            var last = sources[sources.Count - 1];
            var parts = last.Split('-');
            int i = int.Parse(parts[1]);
            return ret + "-" + (i + 1);
        }

        public string StorageFileId
        {
            get
            {
                var srcStr = GetAttribute("src");
                if (srcStr == null) return null;
                var sources = srcStr.Split(';').Where((x) => x.StartsWith(Drive.Storage.Kind));
                // Source - is kind of storage. For example 'gl-1'
                foreach (var src in sources)
                {
                    // SID - Storage ID.
                    // gl-1.sid - vault name, location and other.
                    var sidPropertyName = string.Format("{0}.sid", src);
                    if (Drive.Storage.Id != GetAttribute(sidPropertyName))
                    {
                        continue;
                    }
                    // gl-1.id-{1}
                    return GetAttribute(src);
                }
                return null;
            }
            set
            {
                var srcStr = GetAttribute("src");
                List<string> sources;
                if (srcStr == null)
                {
                    sources = new List<string>();
                }
                sources = srcStr.Split(';').Where((x) => x.StartsWith(Drive.Storage.Kind)).ToList();
                bool sourceExists = false;
                string source = null;
                foreach (var src in sources)
                {
                    // SID - Storage ID
                    var sidPropertyName = string.Format("{0}.sid", src);
                    if (Drive.Storage.Id != GetAttribute(sidPropertyName))
                    {
                        continue;
                    }
                    sourceExists = true;
                    source = src;
                }

                if (!sourceExists)
                {
                    source = GetNewSource(sources);
                    sources.Add(source);
                }

                SetAttribute("src", source);
                SetAttribute(string.Format("{0}.sid", source), Drive.Storage.Id);
                SetAttribute(string.Format("{0}.id", source), value);

            }
        }
    }
}
