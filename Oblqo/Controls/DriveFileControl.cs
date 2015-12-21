using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Oblqo.Properties;

namespace Oblqo.Controls
{
    public partial class DriveFileControl : UserControl
    {
        private DriveFile driveFile;
        private int controlNumber;
        private CancellationTokenSource pictureCancellationTokenSource;
        private readonly object pictureCancellationTokenSourceLoker = new object();

        public DriveFileControl()
        {
            InitializeComponent();
        }

        [Browsable(false), DefaultValue(null)]
        public DriveFile DriveFile
        {
            get
            {
                return driveFile;
            }
            set
            {
                driveFile = value;
                OnFileChanged();
            }
        }
        
        protected virtual void OnFileChanged()
        {

            controlNumber++;

            fileNameLabel.Text = DriveFile.Name;
            fileSizeLabel.Text = Common.NumberOfBytesToString(DriveFile.Size);

            label3.Visible = false;
            widthAndHeightLabel.Visible = false;

            if (!DriveFile.IsImage)
            {
                lock (pictureCancellationTokenSourceLoker)
                {
                    pictureCancellationTokenSource?.Cancel();
                    pictureCancellationTokenSource = new CancellationTokenSource();
                }
                
                ImageLoaded?.Invoke(this, new EventArgs());
                pictureBox1.BackgroundImage = Resources.no_image;
                return;
            }

            // Image preview async loading
            ImageLoading?.Invoke(this, new EventArgs());
            Task.Run(async delegate
            {
                try
                {
                    lock (pictureCancellationTokenSourceLoker)
                    {
                        if (pictureCancellationTokenSource != null)
                            pictureCancellationTokenSource.Cancel();
                        pictureCancellationTokenSource = new CancellationTokenSource();
                    }

                    pictureBox1.BackgroundImage = Resources.loading;
                    int cn = controlNumber;
                    Image image;
                    try
                    {
                        image = await DriveFile.Drive.GetImageAsync(DriveFile, pictureCancellationTokenSource.Token);
                        if (cn != controlNumber)
                        {
                            return;
                        }
                        Invoke(new MethodInvoker(() =>
                        {
                            label3.Visible = true;
                            widthAndHeightLabel.Visible = true;
                            widthAndHeightLabel.Text = string.Format("{0} x {1}", DriveFile.ImageWidth, DriveFile.ImageHeight);
                            pictureBox1.BackgroundImage = image;
                            pictureBox1.Image = null;
                            ImageLoaded?.Invoke(this, new EventArgs());
                            if (widthAndHeightLabel.Text == "0 x 0")
                                widthAndHeightLabel.Text = string.Format("{0} x {1}", image.Width, image.Height);
                        }));
                    }
                    catch (OperationCanceledException) { }
                }
                catch (Exception ex)
                {
                    OnError(ex);
                }
            });
        }

        private void OnError(Exception ex)
        {
            Error(this, new ExceptionEventArgs(ex));
        }

        public event EventHandler<ExceptionEventArgs> Error;
        public event EventHandler<EventArgs> ImageLoading;
        public event EventHandler<EventArgs> ImageLoaded;
    }
}
