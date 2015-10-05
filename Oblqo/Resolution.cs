using System;
using System.Drawing;

namespace Oblqo
{
    internal class Resolution
    {
        public Resolution(Size size) : this(size.Width,size.Height) { }
        public Resolution(int w, int h)
        {
            Size = new Size(w, h);
        }
        public Size Size { get; private set; }
        public override string ToString()
        {
            return String.Format("{0} x {1}", Size.Width, Size.Height);
        }

        public override bool Equals(object obj)
        {
            return Size.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Size.GetHashCode();
        }
    }
}