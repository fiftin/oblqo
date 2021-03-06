﻿using System;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Linq;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace Oblqo
{
    public abstract class DriveFile : IDriveFile
    {
        public abstract string Id { get; }
        public abstract bool IsImage { get; }
        public abstract bool IsFolder { get; }
        public abstract string Name { get; }
        public abstract bool HasChildren { get; }
        public abstract long Size { get; }
        public abstract DateTime ModifiedDate { get; }
        public abstract DateTime CreatedDate { get; }
        public abstract int OriginalImageWidth { get; set; }
        public abstract int OriginalImageHeight { get; set; }
        public abstract long OriginalSize { get; set; }
        public abstract int ImageWidth { get; }
        public abstract int ImageHeight { get; }
        public abstract bool IsRoot { get; }
        public abstract string MimeType { get; }

        /// <summary>
        /// Folder contains current file.
        /// </summary>
        public virtual DriveFile Parent => Owner.GetDriveFile(Drive);

        public AccountFile Owner { get; set; }

        public Drive Drive { get; }

        protected DriveFile(Drive drive)
        {
            this.Drive = drive;
        }

        public async Task<Stream> ReadFileAsync(CancellationToken token)
        {
            return await Drive.ReadFileAsync(this, token);
        }

        public virtual XElement ToXml()
        {
            var ret = new XElement("driveFile");
            ret.SetAttributeValue("id", Id);
            ret.SetAttributeValue("driveId", Drive.Id);
            ret.SetAttributeValue("storageFileId", StorageFileId);
            return ret;
        }

        public async Task<Stream> ReadAsync(CancellationToken token)
        {
            return await Drive.ReadFileAsync(this, token);
        }

        public abstract Task WriteAsync(byte[] bytes, CancellationToken token);
        public abstract string GetAttribute(string name);
        public abstract Task SetAttributeAsync(string name, string value, CancellationToken token);

        /// <summary>
        /// Generate unique ID for new source.
        /// </summary>
        /// <param name="sources">Existing source IDs.</param>
        /// <returns></returns>
        public string GetNewSource(List<string> sources)
        {
            var ret = Drive.Storage.Kind;
            if (sources.Count == 0)
            {
                return ret;
            }
            sources.Sort();
            var last = sources[sources.Count - 1];
            var parts = last.Split('-');
            if (parts.Length == 1)
            {
                return ret + "-0";
            }
            var i = int.Parse(parts[1]);
            return ret + "-" + (i + 1);
        }

        public async Task SetStorageFileIdAsync(string value, CancellationToken token)
        {
            var srcStr = GetAttribute("src");
            var sources = srcStr?.Split(';').Where((x) => x.StartsWith(Drive.Storage.Kind)).ToList() ?? new List<string>();
            var sourceExists = false;
            string source = null;
            foreach (var src in sources.Where(src => Drive.Storage.Id == GetAttribute(string.Format("{0}.sid", src))))
            {
                sourceExists = true;
                source = src;
            }

            if (!sourceExists)
            {
                source = GetNewSource(sources);
                sources.Add(source);
            }

            await SetAttributeAsync("src", string.Join(";", sources), token);
            await SetAttributeAsync(string.Format("{0}.sid", source), Drive.Storage.Id, token);
            await SetAttributeAsync(string.Format("{0}.id", source), value, token);

        }

        public virtual string StorageFileId
        {
            get
            {
                var srcStr = GetAttribute("src");
                if (srcStr == null) return null;
                var sources = srcStr.Split(';').Where((x) => x.StartsWith(Drive.Storage.Kind));
                // Source - is kind of storage. For example 'gl-1'
                var ret = (from src in sources
                           where Drive.Storage.Id == GetAttribute(string.Format("{0}.sid", src))
                           select GetAttribute(string.Format("{0}.id", src))).FirstOrDefault();
                return ret;
            }
        }
    }
}
