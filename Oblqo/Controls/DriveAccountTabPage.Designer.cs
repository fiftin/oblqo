namespace Oblqo
{
    partial class DriveAccountTabPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.driveAccountControl1 = new Oblqo.DriveAccountControl();
            this.SuspendLayout();
            // 
            // driveAccountControl1
            // 
            this.driveAccountControl1.DriveImageResolution = new System.Drawing.Size(1600, 1200);
            this.driveAccountControl1.DriveRootPath = "";
            this.driveAccountControl1.DriveType = Oblqo.DriveType.GoogleDrive;
            this.driveAccountControl1.Location = new System.Drawing.Point(0, 0);
            this.driveAccountControl1.Name = "driveAccountControl1";
            this.driveAccountControl1.Size = new System.Drawing.Size(464, 191);
            this.driveAccountControl1.TabIndex = 0;
            this.driveAccountControl1.DriveTypeChanged += new System.EventHandler(this.driveAccountControl1_DriveTypeChanged);
            // 
            // DriveAccountTabPage
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ResumeLayout(false);

        }

        #endregion

        private DriveAccountControl driveAccountControl1;
        private DriveInfo drive;
    }
}
