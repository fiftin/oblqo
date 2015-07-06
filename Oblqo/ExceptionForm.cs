using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oblqo
{
    public partial class ExceptionForm : Form
    {
        private Exception exception;

        public ExceptionForm()
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
                messageTextBox.Text = value.Message;
                typeTextBox.Text = value.GetType().Name;
                callStackTextBox.Text = value.StackTrace;
            }
        }
    }
}
