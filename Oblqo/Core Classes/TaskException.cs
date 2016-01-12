using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo
{
    public class TaskException : Exception
    {
        public TaskException(string message)
            : base(message)
        {
        }
        public TaskException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
