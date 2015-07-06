using System.IO;

namespace Oblqo.Google
{
    public static class GoogleMimeTypes
    {
        public static readonly string Folder = "application/vnd.google-apps.folder";
        public static readonly string TextFile = "text/plain";
        public static readonly string JpegFile = "image/jpeg";
        public static readonly string PngFile = "image/png";

        public static string GetMimeTypeByExtension(string pathName)
        {
            var ext = Path.GetExtension(pathName);
            switch (ext)
            {
                case ".jpeg":
                    return JpegFile;
                case ".png":
                    return PngFile;
                case ".txt":
                    return TextFile;
            }
            return "";
        }

    }
}
