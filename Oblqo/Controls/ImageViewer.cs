using System;
using System.Drawing;
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


        public event EventHandler<SlideEventArgs> Slide;

        private void btnBack_Click(object sender, EventArgs e)
        {
            Slide?.Invoke(this, new SlideEventArgs(SlideDirection.Back));
        }

        private void btnFront_Click(object sender, EventArgs e)
        {
            Slide?.Invoke(this, new SlideEventArgs(SlideDirection.Front));
        }
    }
}
