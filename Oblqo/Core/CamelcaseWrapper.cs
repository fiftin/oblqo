using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo
{
    public class CamelcaseWrapper
    {
        public object OriginalObject { get; private set; }
        private string ReadableString { get; set; }
        public CamelcaseWrapper(object obj)
        {
            OriginalObject = obj;
            ReadableString = Common.CamelcaseToHumanReadable(obj.ToString());
        }

        public override string ToString()
        {
            return ReadableString;
        }
    }
}
