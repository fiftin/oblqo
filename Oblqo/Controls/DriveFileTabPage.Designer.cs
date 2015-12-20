namespace Oblqo.Controls
{
    partial class DriveFileTabPage
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
            this.driveFileControl1 = new Oblqo.Controls.DriveFileControl();
            this.SuspendLayout();
            // 
            // driveFileControl1
            // 
            this.driveFileControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.driveFileControl1.Location = new System.Drawing.Point(0, 0);
            this.driveFileControl1.Name = "driveFileControl1";
            this.driveFileControl1.Size = new System.Drawing.Size(297, 300);
            this.driveFileControl1.TabIndex = 0;
            this.ResumeLayout(false);

        }

        #endregion

        private DriveFileControl driveFileControl1;
    }
}
