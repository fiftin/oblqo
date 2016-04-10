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
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Drive Type:";
            //
            // driveRootPathBrowseButton
            //
            this.driveRootPathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.driveRootPathBrowseButton.Enabled = false;
            this.driveRootPathBrowseButton.Location = new System.Drawing.Point(356, 153);
            this.driveRootPathBrowseButton.Name = "driveRootPathBrowseButton";
            this.driveRootPathBrowseButton.Size = new System.Drawing.Size(101, 31);
            this.driveRootPathBrowseButton.TabIndex = 21;
            this.driveRootPathBrowseButton.Text = "Browse...";
            this.driveRootPathBrowseButton.UseVisualStyleBackColor = true;
            this.driveRootPathBrowseButton.Click += new System.EventHandler(this.driveRootPathBrowseButton_Click);
            //
            // driveKindComboBox
            //
            this.driveKindComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.driveKindComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.driveKindComboBox.FormattingEnabled = true;
            this.driveKindComboBox.Location = new System.Drawing.Point(127, 6);
            this.driveKindComboBox.Name = "driveKindComboBox";
            this.driveKindComboBox.Size = new System.Drawing.Size(330, 21);
            this.driveKindComboBox.TabIndex = 18;
            this.driveKindComboBox.SelectedIndexChanged += new System.EventHandler(this.driveKindComboBox_SelectedIndexChanged);
            //
            // imageResolutionComboBox
            //
            this.imageResolutionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageResolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageResolutionComboBox.FormattingEnabled = true;
            this.imageResolutionComboBox.Location = new System.Drawing.Point(127, 34);
            this.imageResolutionComboBox.Name = "imageResolutionComboBox";
            this.imageResolutionComboBox.Size = new System.Drawing.Size(330, 21);
            this.imageResolutionComboBox.TabIndex = 19;
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Root Path:";
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Image Dimensions:";
            //
            // driveRootPathTextBox
            //
            this.driveRootPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.driveRootPathTextBox.Location = new System.Drawing.Point(6, 97);
            this.driveRootPathTextBox.Name = "driveRootPathTextBox";
            this.driveRootPathTextBox.Size = new System.Drawing.Size(451, 20);
            this.driveRootPathTextBox.TabIndex = 20;
            //
            // panel1
            //
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
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 218);
            this.panel1.TabIndex = 25;
            //

            // label1
            //
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(124, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(333, 36);
            this.label1.TabIndex = 25;
            this.label1.Text = "If photo\'s size more than this, the picture automatically scaled";
            //
            // label2
            //
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(3, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(454, 30);
            this.label2.TabIndex = 26;
            this.label2.Text = "Path to folder with files, for example d:\\photos. For Google Drive it can be phot" +
    "os/trevel.";
            //
            // DriveAccountControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "DriveAccountControl";
            this.Size = new System.Drawing.Size(464, 218);
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
