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
            this.storageTabControl = new System.Windows.Forms.TabControl();
            this.glacierTabPage = new System.Windows.Forms.TabPage();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.glacierVaultTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.driveTabControl = new System.Windows.Forms.TabControl();
            this.addDriveTabPage = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.storageTabControl.SuspendLayout();
            this.glacierTabPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.driveTabControl.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(424, 455);
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
            this.okButton.Location = new System.Drawing.Point(331, 455);
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
            this.accountNameTextBox.Location = new System.Drawing.Point(155, 19);
            this.accountNameTextBox.Name = "accountNameTextBox";
            this.accountNameTextBox.Size = new System.Drawing.Size(330, 20);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 182);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Archive Account";
            // 
            // storageTabControl
            // 
            this.storageTabControl.Controls.Add(this.glacierTabPage);
            this.storageTabControl.Location = new System.Drawing.Point(6, 19);
            this.storageTabControl.Name = "storageTabControl";
            this.storageTabControl.SelectedIndex = 0;
            this.storageTabControl.Size = new System.Drawing.Size(487, 157);
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
            this.glacierTabPage.Size = new System.Drawing.Size(479, 130);
            this.glacierTabPage.TabIndex = 0;
            this.glacierTabPage.Text = "Amazon Glacier";
            this.glacierTabPage.UseVisualStyleBackColor = true;
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
            this.groupBox2.Location = new System.Drawing.Point(12, 251);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 198);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Drive Account";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // driveTabControl
            // 
            this.driveTabControl.ContextMenuStrip = this.contextMenuStrip1;
            this.driveTabControl.Controls.Add(this.addDriveTabPage);
            this.driveTabControl.ImageList = this.imageList1;
            this.driveTabControl.Location = new System.Drawing.Point(6, 19);
            this.driveTabControl.Name = "driveTabControl";
            this.driveTabControl.SelectedIndex = 0;
            this.driveTabControl.Size = new System.Drawing.Size(487, 173);
            this.driveTabControl.TabIndex = 19;
            this.driveTabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.driveTabControl_Selecting);
            this.driveTabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.driveTabControl_MouseClick);
            this.driveTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.driveTabControl_MouseDown);
            // 
            // addDriveTabPage
            // 
            this.addDriveTabPage.ImageIndex = 0;
            this.addDriveTabPage.Location = new System.Drawing.Point(4, 23);
            this.addDriveTabPage.Name = "addDriveTabPage";
            this.addDriveTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.addDriveTabPage.Size = new System.Drawing.Size(479, 146);
            this.addDriveTabPage.TabIndex = 0;
            this.addDriveTabPage.Text = "Add";
            this.addDriveTabPage.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "021.png");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteAccountToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 26);
            // 
            // deleteAccountToolStripMenuItem
            // 
            this.deleteAccountToolStripMenuItem.Name = "deleteAccountToolStripMenuItem";
            this.deleteAccountToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.deleteAccountToolStripMenuItem.Text = "Delete Account";
            this.deleteAccountToolStripMenuItem.Click += new System.EventHandler(this.deleteAccountToolStripMenuItem_Click);
            // 
            // AccountForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(523, 499);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AccountForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.storageTabControl.ResumeLayout(false);
            this.glacierTabPage.ResumeLayout(false);
            this.glacierTabPage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.driveTabControl.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabControl driveTabControl;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl storageTabControl;
        private System.Windows.Forms.TabPage glacierTabPage;
        private System.Windows.Forms.TabPage addDriveTabPage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteAccountToolStripMenuItem;
    }

}