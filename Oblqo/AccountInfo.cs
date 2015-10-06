using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using Amazon;

namespace Oblqo
{
    /// <summary>
    /// Represents link storage account (for long storing) with drive account (for image views).
    /// </summary>
    public class AccountInfo
    {
        public string oldAccountName;

        [XmlIgnore]
        public string OldAccountName { get; set; }

        public string AccountName { get; set; }

        public string StorageAccessKeyId { get; set; }

        public string StorageSecretAccessKey { get; set; }

        public string StorageVault { get; set; }

        public string StorageRegionSystemName { get; set; }

        [XmlIgnore]
        public RegionEndpoint StorageRegionEndpoint => RegionEndpoint.GetBySystemName(StorageRegionSystemName);

        public string StorageRootPath { get; set; }

        [XmlArray]
        public List<DriveInfo> Drives { get; set; } = new List<DriveInfo>();
    }
}
