using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo.Tasks
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    sealed class AccountFileStateChangeAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string filePropertyName;

        readonly AccountFileStates newState;

        // This is a positional argument
        public AccountFileStateChangeAttribute(AccountFileStates newState, string filePropertyName = "File")
        {
            this.filePropertyName = filePropertyName;
            this.newState = newState;
        }

        public string FilePropertyName
        {
            get { return filePropertyName; }
        }

        // This is a named argument
        public AccountFileStates NewState
        {
            get
            {
                return newState;
            }
        }
    }
}
