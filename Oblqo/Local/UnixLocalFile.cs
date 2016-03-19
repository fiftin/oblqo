using System;
using Oblqo.Local;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace Oblqo
{
	public class UnixLocalFile : LocalFile
	{
		public UnixLocalFile(LocalDrive drive, FileSystemInfo file)
			:base(drive, file)
		{
		}

		public override string GetAttribute(string name)
		{
			byte[] bytes;
			var attr = FormatAttributeName (name);
			Mono.Unix.Native.Syscall.getxattr(FullName, attr, out bytes);
			if (bytes == null) {
				return null;
			}
			var ret = Encoding.UTF8.GetString (bytes);
			return ret;
		}

		protected override void SetAttribute(string name, string value)
		{
			Mono.Unix.Native.Syscall.setxattr(FullName, FormatAttributeName(name), Encoding.UTF8.GetBytes(value));
		}

		public override async Task SetAttributeAsync(string name, string value, CancellationToken token)
		{
			await Task.Run(() => SetAttribute(FormatAttributeName(name), value));
		}

		private static string FormatAttributeName(string name) {
			return string.Format ("user.{0}", name);
		}

	}
}

