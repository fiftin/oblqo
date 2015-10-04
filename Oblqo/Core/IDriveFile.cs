using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oblqo
{
    public interface IDriveFile
    {
        string Id { get; }
        bool IsImage { get; }
        bool IsFolder { get; }
        string Name { get; }
        bool HasChildren { get; }
        long Size { get; }
        DateTime ModifiedDate { get; }
        DateTime CreatedDate { get; }
        int OriginalImageWidth { get; set; }
        int OriginalImageHeight { get; set; }
        long OriginalSize { get; set; }
        int ImageWidth { get; }
        int ImageHeight { get; }
        bool IsRoot { get; }
        string MimeType { get; }
        Drive Drive { get; }
        string StorageFileId { get; }
        XElement ToXml();
        Task ScaleImageAsync();
        string GetAttribute(string name);
        Task SetAttributeAsync(string name, string value, CancellationToken token);

        /// <summary>
        /// Generate unique ID for new source.
        /// </summary>
        /// <param name="sources">Existing source IDs.</param>
        /// <returns></returns>
        string GetNewSource(List<string> sources);

        Task SetStorageFileIdAsync(string value, CancellationToken token);
    }
}