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
            this.fileListFilterTextBox = new System.Windows.Forms.TextBox();
            this.showSyncFilesOnlyCheckbox = new System.Windows.Forms.CheckBox();
            this.fileListNumberOfFilesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fileListFilterTextBox
            // 
            this.fileListFilterTextBox.AcceptsReturn = true;
            this.fileListFilterTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileListFilterTextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.fileListFilterTextBox.Location = new System.Drawing.Point(330, 1);
            this.fileListFilterTextBox.Name = "fileListFilterTextBox";
            this.fileListFilterTextBox.Size = new System.Drawing.Size(130, 20);
            this.fileListFilterTextBox.TabIndex = 8;
            this.fileListFilterTextBox.Text = "Filter";
            this.fileListFilterTextBox.Enter += new System.EventHandler(this.fileListFilterTextBox_Enter);
            this.fileListFilterTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fileListFilterTextBox_KeyDown);
            this.fileListFilterTextBox.Leave += new System.EventHandler(this.fileListFilterTextBox_Leave);
            // 
            // showSyncFilesOnlyCheckbox
            // 
            this.showSyncFilesOnlyCheckbox.AutoSize = true;
            this.showSyncFilesOnlyCheckbox.Location = new System.Drawing.Point(138, 2);
            this.showSyncFilesOnlyCheckbox.Name = "showSyncFilesOnlyCheckbox";
            this.showSyncFilesOnlyCheckbox.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.showSyncFilesOnlyCheckbox.Size = new System.Drawing.Size(132, 17);
            this.showSyncFilesOnlyCheckbox.TabIndex = 7;
            this.showSyncFilesOnlyCheckbox.Text = "Show unsync only";
            this.showSyncFilesOnlyCheckbox.UseVisualStyleBackColor = true;
            this.showSyncFilesOnlyCheckbox.CheckedChanged += new System.EventHandler(this.showSyncFilesOnlyCheckbox_CheckedChanged);
            // 
            // fileListNumberOfFilesLabel
            // 
            this.fileListNumberOfFilesLabel.AutoSize = true;
            this.fileListNumberOfFilesLabel.Location = new System.Drawing.Point(3, 3);
            this.fileListNumberOfFilesLabel.Name = "fileListNumberOfFilesLabel";
            this.fileListNumberOfFilesLabel.Size = new System.Drawing.Size(83, 13);
            this.fileListNumberOfFilesLabel.TabIndex = 6;
            this.fileListNumberOfFilesLabel.Text = "0 files, 0 unsync";
            // 
            // FileListStatusBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileListFilterTextBox);
            this.Controls.Add(this.showSyncFilesOnlyCheckbox);
            this.Controls.Add(this.fileListNumberOfFilesLabel);
            this.Name = "FileListStatusBar";
            this.Size = new System.Drawing.Size(501, 23);
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
