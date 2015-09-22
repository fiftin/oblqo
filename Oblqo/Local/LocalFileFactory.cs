using System;
using System.Collections.Generic;
using System.IO;

namespace Oblqo.Local
{
    class LocalFileFactory
    {
        public static LocalFileFactory instance;
        
        public static LocalFileFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LocalFileFactory();
                    instance.Register(PlatformID.Win32NT, (drive, file, isRoot) => new NtfsLocalFile(drive, file, isRoot));
                }
                return instance;
            }
        }


        private Dictionary<PlatformID, Func<LocalDrive, FileSystemInfo, bool, LocalFile>> creators = new Dictionary<PlatformID, Func<LocalDrive, FileSystemInfo, bool, LocalFile>>();

        public void Register(PlatformID platform, Func<LocalDrive, FileSystemInfo, bool, LocalFile> creator)
        {
            creators.Add(platform, creator);
        }

        public LocalFile Create(LocalDrive drive, FileSystemInfo file, bool isRoot)
        {
            var creator = creators[Environment.OSVersion.Platform];
            if (creator == null)
            {
                throw new PlatformNotSupportedException();
            }
            return creator(drive, file, isRoot);
        }
    }
}
