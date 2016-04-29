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

        public ExceptionForm()
        {
            InitializeComponent();
        }

        public Exception Exception
        {
            get
            {
                return exceptionVIew1.Exception;
            }
            set
            {
                exceptionVIew1.Exception = value;
            }
        }
    }
}
