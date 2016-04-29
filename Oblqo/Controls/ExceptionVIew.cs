using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oblqo.Controls
{
    public partial class ExceptionVIew : UserControl
    {
        private Exception exception;

        public ExceptionVIew()
        {
            InitializeComponent();
        }

        public Exception Exception
        {
            get
            {
                return exception;
            }
            set
            {
                exception = value;
                if (value == null)
                {
                    messageTextBox.Text = "";
                    typeTextBox.Text = "";
                    callStackTextBox.Text = "";
                    return;
                }
                messageTextBox.Text = value.Message;
                typeTextBox.Text = value.GetType().Name;
                callStackTextBox.Text = value.StackTrace;
            }
        }
    }
}
