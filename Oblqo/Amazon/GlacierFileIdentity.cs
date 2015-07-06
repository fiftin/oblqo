using Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo.Amazon
{
    class GlacierFileIdentity
    {
        public string ArchiveId { get; private set; }
        public string Vault { get; private set; }
        public string Region { get; private set; }

        public GlacierFileIdentity(RegionEndpoint region, string vault, string archiveId)
            : this(region.SystemName, vault, archiveId)
        {
        }

        public GlacierFileIdentity(string region, string vault, string archiveId)
        {
            ArchiveId = archiveId;
            Vault = vault;
            Region = region;
        }

        public static GlacierFileIdentity Parse(string fileId)
        {
            var parts = fileId.Split(':');
            return new GlacierFileIdentity(parts[0], parts[1], parts[2]);
        }

        public static bool TryParse(string fileId, out GlacierFileIdentity identity)
        {
            var parts = fileId.Split(':');
            if (parts.Length != 3) {
                identity = null;
                return false;
            }
            identity = new GlacierFileIdentity(parts[0], parts[1], parts[2]);
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", Region, Vault, ArchiveId);
        }

    }
}
