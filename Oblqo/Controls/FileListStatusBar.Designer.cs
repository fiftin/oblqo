namespace Oblqo
{
    partial class FileListStatusBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileListStatusBar));
            this.fileListFilterTextBox = new System.Windows.Forms.TextBox();
            this.showSyncFilesOnlyCheckbox = new System.Windows.Forms.CheckBox();
            this.fileListNumberOfFilesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fileListFilterTextBox
            // 
            this.fileListFilterTextBox.AcceptsReturn = true;
            resources.ApplyResources(this.fileListFilterTextBox, "fileListFilterTextBox");
            this.fileListFilterTextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.fileListFilterTextBox.Name = "fileListFilterTextBox";
            this.fileListFilterTextBox.Enter += new System.EventHandler(this.fileListFilterTextBox_Enter);
            this.fileListFilterTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fileListFilterTextBox_KeyDown);
            this.fileListFilterTextBox.Leave += new System.EventHandler(this.fileListFilterTextBox_Leave);
            // 
            // showSyncFilesOnlyCheckbox
            // 
            resources.ApplyResources(this.showSyncFilesOnlyCheckbox, "showSyncFilesOnlyCheckbox");
            this.showSyncFilesOnlyCheckbox.Name = "showSyncFilesOnlyCheckbox";
            this.showSyncFilesOnlyCheckbox.UseVisualStyleBackColor = true;
            this.showSyncFilesOnlyCheckbox.CheckedChanged += new System.EventHandler(this.showSyncFilesOnlyCheckbox_CheckedChanged);
            // 
            // fileListNumberOfFilesLabel
            // 
            resources.ApplyResources(this.fileListNumberOfFilesLabel, "fileListNumberOfFilesLabel");
            this.fileListNumberOfFilesLabel.Name = "fileListNumberOfFilesLabel";
            // 
            // FileListStatusBar
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileListFilterTextBox);
            this.Controls.Add(this.showSyncFilesOnlyCheckbox);
            this.Controls.Add(this.fileListNumberOfFilesLabel);
            this.Name = "FileListStatusBar";
            this.SizeChanged += new System.EventHandler(this.FileListStatusBar_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fileListFilterTextBox;
        private System.Windows.Forms.CheckBox showSyncFilesOnlyCheckbox;
        private System.Windows.Forms.Label fileListNumberOfFilesLabel;
    }
}
