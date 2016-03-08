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
			Mono.Unix.Native.Syscall.getxattr (FullName, name, out bytes);
			if (bytes == null) {
				return null;
			}
			return Encoding.UTF8.GetString (bytes);
		}

		protected override void SetAttribute(string name, string value)
		{
			Mono.Unix.Native.Syscall.setxattr (FullName, name, Encoding.UTF8.GetBytes(value));
		}

		public override async Task SetAttributeAsync(string name, string value, CancellationToken token)
		{
			await Task.Run(() => SetAttribute(name, value));
		}



	}
}

