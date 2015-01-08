using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oblakoo
{
    public class Common
    {
        public static String NumberOfBytesToString(long bytes)
        {
            if (bytes < 1000)
                return string.Format("{0}B", bytes);
            else if (bytes < 1000000)
                return string.Format("{0}KB", bytes/1000);
            else if (bytes < 1000000000)
                return string.Format("{0}MB", bytes / 1000000);
            else
                return string.Format("{0}GB", bytes / 1000000000);
        }

        public static async Task EnumerateFilesRecursiveAsync(string path, Action<FileInfo> action, CancellationToken token)
        {
            var folder = new DirectoryInfo(path);
            foreach (var file in folder.EnumerateFiles(path))
            {
                if (token.IsCancellationRequested)
                    return;
                action(file);
            }
            foreach (var dir in folder.EnumerateDirectories())
            {
                if (token.IsCancellationRequested)
                    return;
                await EnumerateFilesRecursiveAsync(dir.FullName, action, token);
            }
        }
    }
}
