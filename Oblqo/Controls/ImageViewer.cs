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
    public partial class ImageViewer : UserControl
    {
        private DriveFile imageFile;

        public ImageViewer()
        {
            InitializeComponent();
        }

        public string FileName
        {
            get
            {
                return lblFileName.Text;
            }
            set
            {
                lblFileName.Text = value;
            }
        }

        public Image Picture
        {
            get
            {
                return picImage.BackgroundImage;
            }
            set
            {
                picImage.BackgroundImage = value;
            }
        }

        public event EventHandler SlideBack;
        public event EventHandler SlideFront;

        private void btnBack_Click(object sender, EventArgs e)
        {
            SlideBack?.Invoke(this, new EventArgs());
        }

        private void btnFront_Click(object sender, EventArgs e)
        {
            SlideFront?.Invoke(this, new EventArgs());
        }

        private void ImageViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
            }
        }
    }
}
