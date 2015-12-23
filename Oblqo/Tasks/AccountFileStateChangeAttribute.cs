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
        readonly string parentFilePropertyName;

        readonly AccountFileStates newState;

        // This is a positional argument
        public AccountFileStateChangeAttribute(AccountFileStates newState, string filePropertyName = "File", string parentFilePropertyName = null)
        {
            this.filePropertyName = filePropertyName;
            this.newState = newState;
            this.parentFilePropertyName = parentFilePropertyName;
        }

        public string FilePropertyName
        {
            get { return filePropertyName; }
        }

        public string ParentFilePropertyName
        {
            get { return parentFilePropertyName; }
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
