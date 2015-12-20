using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oblqo.Controls
{
    public partial class DriveFileTabPage : TabPage
    {
        public DriveFile File { get; set; }
        public DriveFileTabPage(DriveFile file)
        {
            InitializeComponent();
            File = file;
        }
        public void UpdateContent()
        {
            driveFileControl1.File = File;
        }
    }
}
