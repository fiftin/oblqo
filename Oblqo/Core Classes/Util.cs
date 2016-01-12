using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo
{
    public class Util
    {
        public static AccountFileStates GetFileState(AccountFile file)
        {
            AccountFileStates ret = 0;
            var isDrivesSyncronized = file.Account.Drives.Select(x => file.GetDriveFile(x)).All(x => x != null);
            if (!isDrivesSyncronized)
            {
                ret |= AccountFileStates.UnsyncronizedWithDrive;
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
