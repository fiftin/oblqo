using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo
{
    public class ResourcesWrapper
    {
        public object OriginalObject { get; private set; }
        private string ReadableString { get; set; }
        public ResourcesWrapper(object obj)
        {
            OriginalObject = obj;
            ReadableString = Util.GetString(obj.ToString());
        }

        public override string ToString()
        {
            return ReadableString;
        }
    }
}
