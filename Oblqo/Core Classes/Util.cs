using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo
{
    public class Util
    {
        private static ResourceManager resources;
        public static string GetString(string name)
        {
            if (resources == null)
            {
                resources = new ResourceManager("Oblqo.Resources", typeof(MainForm).Assembly);
            }
            return resources.GetString(name);
        }

        public static AccountFileStates GetFileState(AccountFile file)
        {
            AccountFileStates ret = 0;

            var nDrives = file.Account.Drives.Count;
            var nDrivesSyncronized =
                file.Account.Drives.Select(x => file.GetDriveFile(x)).Count(x => x != null);

            DriveFile inventoryFile;
            if (file.Account.Drives.InventoryDrive == null)
            {
                inventoryFile = null;
            }
            else
            {
                nDrives--;
                inventoryFile = file.GetDriveFile(file.Account.Drives.InventoryDrive);
                if (inventoryFile != null)
                {
                    nDrivesSyncronized--;
                }
            }
            
            if (nDrivesSyncronized != nDrives)
            {
                ret |= AccountFileStates.UnsyncronizedWithAllDrives;
            }

            if (file.Account.Drives.InventoryDrive != null)
            {
                if (inventoryFile == null)
                {
                    ret |= AccountFileStates.PlacedOnlyDrive;
                }
                else if (nDrivesSyncronized == 0)
                {
                    ret |= AccountFileStates.PlacedOnlyStorage;
                }
            }

            if (file.StorageFileId == null)
            {
                ret |= AccountFileStates.UnsyncronizedWithStorage;
            }
            else if (!file.HasValidStorageFileId)
            {
                ret |= AccountFileStates.Error;
            }
            return ret;
        }
    }
}
