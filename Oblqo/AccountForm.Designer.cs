namespace Oblqo
{
    partial class AccountForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountForm));
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.accountNameTextBox = new System.Windows.Forms.TextBox();
            this.storageAccessKeyIdTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.secretAccessKeyTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.glacierVaultTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.driveRootPathBrowseButton = new System.Windows.Forms.Button();
            this.imageResolutionComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.driveRootPathTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.driveKindComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.driveTabControl = new System.Windows.Forms.TabControl();
            this.localDriveTabPage = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.storageTabControl = new System.Windows.Forms.TabControl();
            this.glacierTabPage = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.driveTabControl.SuspendLayout();
            this.localDriveTabPage.SuspendLayout();
            this.storageTabControl.SuspendLayout();
            this.glacierTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(421, 442);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(87, 31);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(327, 442);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(87, 31);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connection Name:";
            // 
            // accountNameTextBox
            // 
            this.accountNameTextBox.Location = new System.Drawing.Point(163, 19);
            this.accountNameTextBox.Name = "accountNameTextBox";
            this.accountNameTextBox.Size = new System.Drawing.Size(318, 20);
            this.accountNameTextBox.TabIndex = 0;
            // 
            // storageAccessKeyIdTextBox
            // 
            this.storageAccessKeyIdTextBox.Location = new System.Drawing.Point(133, 11);
            this.storageAccessKeyIdTextBox.Name = "storageAccessKeyIdTextBox";
            this.storageAccessKeyIdTextBox.Size = new System.Drawing.Size(330, 20);
            this.storageAccessKeyIdTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Access Key ID:";
            // 
            // secretAccessKeyTextBox
            // 
            this.secretAccessKeyTextBox.Location = new System.Drawing.Point(133, 39);
            this.secretAccessKeyTextBox.Name = "secretAccessKeyTextBox";
            this.secretAccessKeyTextBox.Size = new System.Drawing.Size(330, 20);
            this.secretAccessKeyTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Secret Acc. Key:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.storageTabControl);
            this.groupBox1.Location = new System.Drawing.Point(20, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(489, 182);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Archive Account";
            // 
            // regionComboBox
            // 
            this.regionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(133, 94);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(330, 22);
            this.regionComboBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 14);
            this.label5.TabIndex = 10;
            this.label5.Text = "Region:";
            // 
            // glacierVaultTextBox
            // 
            this.glacierVaultTextBox.Location = new System.Drawing.Point(133, 67);
            this.glacierVaultTextBox.Name = "glacierVaultTextBox";
            this.glacierVaultTextBox.Size = new System.Drawing.Size(330, 20);
            this.glacierVaultTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "Vault:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.driveTabControl);
            this.groupBox2.Location = new System.Drawing.Point(20, 251);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(489, 179);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Drive Account";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // driveRootPathBrowseButton
            // 
            this.driveRootPathBrowseButton.Enabled = false;
            this.driveRootPathBrowseButton.Location = new System.Drawing.Point(362, 88);
            this.driveRootPathBrowseButton.Name = "driveRootPathBrowseButton";
            this.driveRootPathBrowseButton.Size = new System.Drawing.Size(101, 31);
            this.driveRootPathBrowseButton.TabIndex = 8;
            this.driveRootPathBrowseButton.Text = "Browse...";
            this.driveRootPathBrowseButton.UseVisualStyleBackColor = true;
            this.driveRootPathBrowseButton.Click += new System.EventHandler(this.driveRootPathBrowseButton_Click);
            // 
            // imageResolutionComboBox
            // 
            this.imageResolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageResolutionComboBox.FormattingEnabled = true;
            this.imageResolutionComboBox.Location = new System.Drawing.Point(133, 34);
            this.imageResolutionComboBox.Name = "imageResolutionComboBox";
            this.imageResolutionComboBox.Size = new System.Drawing.Size(330, 22);
            this.imageResolutionComboBox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 14);
            this.label7.TabIndex = 14;
            this.label7.Text = "Image Size:";
            // 
            // driveRootPathTextBox
            // 
            this.driveRootPathTextBox.Location = new System.Drawing.Point(133, 62);
            this.driveRootPathTextBox.Name = "driveRootPathTextBox";
            this.driveRootPathTextBox.Size = new System.Drawing.Size(330, 20);
            this.driveRootPathTextBox.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "Root Path:";
            // 
            // driveKindComboBox
            // 
            this.driveKindComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.driveKindComboBox.FormattingEnabled = true;
            this.driveKindComboBox.Location = new System.Drawing.Point(133, 6);
            this.driveKindComboBox.Name = "driveKindComboBox";
            this.driveKindComboBox.Size = new System.Drawing.Size(330, 22);
            this.driveKindComboBox.TabIndex = 5;
            this.driveKindComboBox.SelectedIndexChanged += new System.EventHandler(this.driveKindComboBox_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 14);
            this.label10.TabIndex = 17;
            this.label10.Text = "Drive Type:";
            // 
            // driveTabControl
            // 
            this.driveTabControl.Controls.Add(this.localDriveTabPage);
            this.driveTabControl.ImageList = this.imageList1;
            this.driveTabControl.Location = new System.Drawing.Point(6, 19);
            this.driveTabControl.Name = "driveTabControl";
            this.driveTabControl.SelectedIndex = 0;
            this.driveTabControl.Size = new System.Drawing.Size(477, 150);
            this.driveTabControl.TabIndex = 19;
            // 
            // localDriveTabPage
            // 
            this.localDriveTabPage.Controls.Add(this.label10);
            this.localDriveTabPage.Controls.Add(this.driveRootPathBrowseButton);
            this.localDriveTabPage.Controls.Add(this.driveKindComboBox);
            this.localDriveTabPage.Controls.Add(this.imageResolutionComboBox);
            this.localDriveTabPage.Controls.Add(this.label6);
            this.localDriveTabPage.Controls.Add(this.label7);
            this.localDriveTabPage.Controls.Add(this.driveRootPathTextBox);
            this.localDriveTabPage.Location = new System.Drawing.Point(4, 23);
            this.localDriveTabPage.Name = "localDriveTabPage";
            this.localDriveTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.localDriveTabPage.Size = new System.Drawing.Size(469, 123);
            this.localDriveTabPage.TabIndex = 0;
            this.localDriveTabPage.Text = "Local Drive";
            this.localDriveTabPage.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "021.png");
            // 
            // storageTabControl
            // 
            this.storageTabControl.Controls.Add(this.glacierTabPage);
            this.storageTabControl.Location = new System.Drawing.Point(6, 19);
            this.storageTabControl.Name = "storageTabControl";
            this.storageTabControl.SelectedIndex = 0;
            this.storageTabControl.Size = new System.Drawing.Size(477, 157);
            this.storageTabControl.TabIndex = 11;
            // 
            // glacierTabPage
            // 
            this.glacierTabPage.Controls.Add(this.label2);
            this.glacierTabPage.Controls.Add(this.regionComboBox);
            this.glacierTabPage.Controls.Add(this.label3);
            this.glacierTabPage.Controls.Add(this.label5);
            this.glacierTabPage.Controls.Add(this.storageAccessKeyIdTextBox);
            this.glacierTabPage.Controls.Add(this.glacierVaultTextBox);
            this.glacierTabPage.Controls.Add(this.secretAccessKeyTextBox);
            this.glacierTabPage.Controls.Add(this.label4);
            this.glacierTabPage.Location = new System.Drawing.Point(4, 23);
            this.glacierTabPage.Name = "glacierTabPage";
            this.glacierTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.glacierTabPage.Size = new System.Drawing.Size(469, 130);
            this.glacierTabPage.TabIndex = 0;
            this.glacierTabPage.Text = "Amazon Glacier";
            this.glacierTabPage.UseVisualStyleBackColor = true;
            // 
            // AccountForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(523, 486);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.accountNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Connection";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AccountForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.driveTabControl.ResumeLayout(false);
            this.localDriveTabPage.ResumeLayout(false);
            this.localDriveTabPage.PerformLayout();
            this.storageTabControl.ResumeLayout(false);
            this.glacierTabPage.ResumeLayout(false);
            this.glacierTabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox accountNameTextBox;
        private System.Windows.Forms.TextBox storageAccessKeyIdTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox secretAccessKeyTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox glacierVaultTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox driveRootPathTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.ComboBox imageResolutionComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox driveKindComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button driveRootPathBrowseButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabControl driveTabControl;
        private System.Windows.Forms.TabPage localDriveTabPage;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl storageTabControl;
        private System.Windows.Forms.TabPage glacierTabPage;
    }
}