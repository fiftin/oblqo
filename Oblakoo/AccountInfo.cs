using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Amazon;

namespace Oblakoo
{
    /// <summary>
    /// Represents link storage account (for long storing) with drive account (for image views).
    /// </summary>
    public class AccountInfo
    {
        public string AccountName { get; set; }

        public string StorageAccessKeyId { get; set; }

        public string StorageSecretAccessKey { get; set; }

        public string StorageVault { get; set; }

        public string StorageRegionSystemName { get; set; }

        [XmlIgnore]
        public RegionEndpoint StorageRegionEndpoint {
            get { return RegionEndpoint.GetBySystemName(StorageRegionSystemName); } 
        }

        public string StorageRootPath { get; set; }

        public DriveType DriveType { get; set; }

        public string DriveLogin { get; set; }

        public string DriveAppPassword { get; set; }

        public string DriveRootPath { get; set; }
    }
}
