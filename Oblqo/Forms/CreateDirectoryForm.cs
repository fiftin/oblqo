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
    public partial class CreateFolderForm : Form
    {
        public CreateFolderForm()
        {
            InitializeComponent();
        }


        public string DirecotryName
        {
            get { return folderNameTextBox.Text; }
        }

        private void CreateFolderForm_Load(object sender, EventArgs e)
        {

        }
    }

}
