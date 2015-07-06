using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oblqo
{
    public class Common
    {
        const int BufferLength = 10000;

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


        public static string GetFileOrDirectoryName(string path)
        {
            return Path.GetFileName(path.TrimEnd(Path.DirectorySeparatorChar));
        }

        public static string AppendToPath(string destFolder, string folderName)
        {
            if (destFolder.EndsWith(Path.DirectorySeparatorChar.ToString()))
                return destFolder + folderName;
            else
                return destFolder + Path.DirectorySeparatorChar + folderName;
        }

        public static bool IsSingle<T>(T[] arr)
        {
            return arr != null && arr.Length == 1;
        }

        public static bool IsEmptyOrNull<T>(T[] arr)
        {
            return arr == null || arr.Length == 0;
        }

        public static async Task<int> CopyStreamAsync(Stream input, Stream output, Action<TransferProgress> callback = null, long length = -1)
        {
            long len;
            if (length >= 0)
            {
                len = length;
            }
            else
            {
                try
                {
                    len = input.Length;
                }
                catch (NotSupportedException)
                {
                    len = -1;
                }
            }
            var percent = 0;
            var totalBytesCopied = 0;
            var ok = false;
            while (!ok)
            {
                var buffer = new byte[BufferLength];
                var n = await input.ReadAsync(buffer, 0, buffer.Length);
                if (n <= 0)
                    ok = true;
                else
                {
                    await output.WriteAsync(buffer, 0, n);
                    totalBytesCopied += n;
                    if (len != -1 && callback != null)
                    {
                        var currentPercent = (int) (100 * totalBytesCopied/(float) len);
                        if (currentPercent != percent)
                        {
                            percent = currentPercent;
                            callback(new TransferProgress(percent));
                        }
                    }
                }
            } 
            if (callback != null)
                callback(new TransferProgress(100));
            return totalBytesCopied;
        }

        public static string[] SplitBy(string s, int len)
        {
            int n = s.Length / len;
            if (s.Length % len != 0)
            {
                n++;
            }
            var ret = new string[n];
            for (int i = 0; i < n; i++)
            {
                if (i * len + len > s.Length)
                {
                    ret[i] = s.Substring(i * len);
                }
                else
                {
                    ret[i] = s.Substring(i * len, len);
                }
            }
            return ret;
        }
    }
}
