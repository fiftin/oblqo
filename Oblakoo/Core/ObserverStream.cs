using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblakoo
{
    public class ObserverStream : Stream
    {
        private readonly Stream stream;
        public event EventHandler PositionChanged;

        protected virtual void OnPositionChanged()
        {
            if (PositionChanged != null)
                PositionChanged(this, new EventArgs());
        }

        public ObserverStream(Stream stream)
        {
            this.stream = stream;
        }

        public override void Flush()
        {
            stream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            var ret = stream.Seek(offset, origin);
            OnPositionChanged();
            return ret;
        }

        public override void SetLength(long value)
        {
            stream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var ret = stream.Read(buffer, offset, count);
            OnPositionChanged();
            return ret;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            stream.Write(buffer, offset, count);
            OnPositionChanged();
        }

        public override bool CanRead
        {
            get { return stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return stream.CanWrite; }
        }

        public override long Length
        {
            get { return stream.Length; }
        }

        public override long Position
        {
            get { return stream.Position; }
            set
            {
                stream.Position = value;
                OnPositionChanged();
            }
        }

    }
}
