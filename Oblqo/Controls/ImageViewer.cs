using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Oblqo.Controls
{
    public partial class ImageViewer : UserControl
    {
        public ImageViewer()
        {
            InitializeComponent();
            UpdateDriveStripBounds();
        }

        public event EventHandler SelectedDriveChanged;

        [DefaultValue(null), Browsable(false)]
        public Drive SelectedDrive
        {
            get
            {
                return driveStrip1.SelectedDrive;
            }
            set
            {
                driveStrip1.SelectedDrive = value;
            }
        }

        [DefaultValue(null), Browsable(false)]
        public AccountFile File
        {
            get
            {
                return driveStrip1.File;
            }
            set
            {
                driveStrip1.File = value;
            }
        }

        [DefaultValue(null), Browsable(false)]
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

        [DefaultValue(null), Browsable(false)]
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void driveStrip1_SelectedDriveChanged(object sender, EventArgs e)
        {
            SelectedDriveChanged?.Invoke(sender, e);
        }

        private void ImageViewer_Resize(object sender, EventArgs e)
        {
            UpdateDriveStripBounds();
        }

        private void UpdateDriveStripBounds()
        {
            SizeF fileNameSize;
            using (var g = CreateGraphics())
            {
                fileNameSize = g.MeasureString(lblFileName.Text, lblFileName.Font);
            }
            driveStrip1.Top = picImage.Bottom;
            driveStrip1.Left = (int)(Width + fileNameSize.Width) / 2 + 30;
            // driveStrip1.Left = Width - driveStrip1.Width - 7;
        }
    }
}
