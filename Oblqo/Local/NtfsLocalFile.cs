using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo.Local
{
    class NtfsLocalFile : LocalFile
    {
        public override long OriginalSize
        {
            get
            {
                // TODO:
                return 0;
            }

            set
            {
                // TODO: Implement OriginalSize setter
            }
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern SafeFileHandle CreateFileW(
            string lpFileName,
            [MarshalAs(UnmanagedType.U4)] FileAccess dwDesiredAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode,
            IntPtr lpSecurityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        public NtfsLocalFile(LocalDrive drive, FileSystemInfo file)
            :base(drive, file)
        {
        }

        public override string GetAttribute(string name)
        {
            using (var handle = CreateFileW(
                file.FullName + ":" + name,
                FileAccess.Read,
                FileShare.ReadWrite,
                IntPtr.Zero,
                FileMode.Open,
                0,
                IntPtr.Zero))
            {
                if (handle == null || handle.IsInvalid)
                {
                    return null;
                }
                using (var stream = new FileStream(handle, FileAccess.Read))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var ret = reader.ReadToEnd();
                        if (string.IsNullOrEmpty(ret))
                        {
                            return null;
                        }
                        return ret;
                    }
                }
            }
        }

        protected override void SetAttribute(string name, string value)
        {
            using (var handle = CreateFileW(
                file.FullName + ":" + name,
                FileAccess.Write,
                FileShare.ReadWrite,
                IntPtr.Zero,
                FileMode.Create,
                0,
                IntPtr.Zero))
            {
                if (handle == null || handle.IsInvalid)
                {
                    return;
                }
                using (var stream = new FileStream(handle, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(value);
                    }
                }
            }
        }

        public override async Task SetAttributeAsync(string name, string value, CancellationToken token)
        {
            await Task.Run(() => SetAttribute(name, value));
        }
    }
}
