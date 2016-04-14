namespace Oblqo
{
    partial class DriveAccountControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriveAccountControl));
            this.label10 = new System.Windows.Forms.Label();
            this.driveRootPathBrowseButton = new System.Windows.Forms.Button();
            this.driveKindComboBox = new System.Windows.Forms.ComboBox();
            this.imageResolutionComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.driveRootPathTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // driveRootPathBrowseButton
            // 
            resources.ApplyResources(this.driveRootPathBrowseButton, "driveRootPathBrowseButton");
            this.driveRootPathBrowseButton.Name = "driveRootPathBrowseButton";
            this.driveRootPathBrowseButton.UseVisualStyleBackColor = true;
            this.driveRootPathBrowseButton.Click += new System.EventHandler(this.driveRootPathBrowseButton_Click);
            // 
            // driveKindComboBox
            // 
            resources.ApplyResources(this.driveKindComboBox, "driveKindComboBox");
            this.driveKindComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.driveKindComboBox.FormattingEnabled = true;
            this.driveKindComboBox.Name = "driveKindComboBox";
            this.driveKindComboBox.SelectedIndexChanged += new System.EventHandler(this.driveKindComboBox_SelectedIndexChanged);
            // 
            // imageResolutionComboBox
            // 
            resources.ApplyResources(this.imageResolutionComboBox, "imageResolutionComboBox");
            this.imageResolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageResolutionComboBox.FormattingEnabled = true;
            this.imageResolutionComboBox.Name = "imageResolutionComboBox";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // driveRootPathTextBox
            // 
            resources.ApplyResources(this.driveRootPathTextBox, "driveRootPathTextBox");
            this.driveRootPathTextBox.Name = "driveRootPathTextBox";
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.driveRootPathTextBox);
            this.panel1.Controls.Add(this.driveRootPathBrowseButton);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.driveKindComboBox);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.imageResolutionComboBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Name = "panel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Name = "label2";
            // 
            // DriveAccountControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "DriveAccountControl";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button driveRootPathBrowseButton;
        private System.Windows.Forms.ComboBox driveKindComboBox;
        private System.Windows.Forms.ComboBox imageResolutionComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox driveRootPathTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
