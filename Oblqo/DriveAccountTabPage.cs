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
    public partial class DriveAccountTabPage : TabPage
    {

        public DriveAccountTabPage(DriveInfo drive)
        {
            this.drive = drive;
            InitializeComponent();
            driveAccountControl1.DriveType = drive.DriveType;
            driveAccountControl1.DriveRootPath = drive.DriveRootPath;
            driveAccountControl1.DriveImageResolution = drive.DriveImageMaxSize;
            Controls.Add(driveAccountControl1);
            this.PerformLayout();
            Text = Common.CamelcaseToHumanReadable(driveAccountControl1.DriveType.ToString());
        }

        public DriveAccountTabPage()
        {
            InitializeComponent();
            Controls.Add(driveAccountControl1);
            this.PerformLayout();
            Text = Common.CamelcaseToHumanReadable(driveAccountControl1.DriveType.ToString());
        }

        public DriveAccountControl DriveControl => driveAccountControl1;

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void driveAccountControl1_DriveTypeChanged(object sender, EventArgs e)
        {
            Text = Common.CamelcaseToHumanReadable(driveAccountControl1.DriveType.ToString());
        }
    }
}
