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
    public partial class DriveFileControl : UserControl
    {
        private DriveFile file;

        public DriveFileControl()
        {
            InitializeComponent();
        }

        public DriveFile File
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
                OnFileChanged();
            }
        }

        protected virtual void OnFileChanged()
        {
            
        }
    }
}
