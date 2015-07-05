using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblakoo.Core
{
    public class ConnectionException : Exception
    {
        public ConnectionException(Account account, string message)
            : base(message)
        {
            Account = account;
        }
        public ConnectionException(Account account, string message, Exception innerException)
            : base(message, innerException)
        {
            Account = account;
        }

        public Account Account { get; private set; }
    }
}
