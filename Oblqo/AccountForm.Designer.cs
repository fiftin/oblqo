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
            this.storageTabControl = new System.Windows.Forms.TabControl();
            this.glacierTabPage = new System.Windows.Forms.TabPage();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.glacierVaultTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label9 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.driveTabControl = new Oblqo.DraggableTabControl();
            this.addDriveTabPage = new System.Windows.Forms.TabPage();
            this.storageTabControl.SuspendLayout();
            this.glacierTabPage.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.driveTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(732, 420);
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
            this.okButton.Location = new System.Drawing.Point(639, 420);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(87, 31);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connection Name:";
            // 
            // accountNameTextBox
            // 
            this.accountNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.accountNameTextBox.Location = new System.Drawing.Point(155, 15);
            this.accountNameTextBox.Name = "accountNameTextBox";
            this.accountNameTextBox.Size = new System.Drawing.Size(664, 20);
            this.accountNameTextBox.TabIndex = 0;
            // 
            // storageAccessKeyIdTextBox
            // 
            this.storageAccessKeyIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.storageAccessKeyIdTextBox.Location = new System.Drawing.Point(13, 36);
            this.storageAccessKeyIdTextBox.Name = "storageAccessKeyIdTextBox";
            this.storageAccessKeyIdTextBox.Size = new System.Drawing.Size(361, 20);
            this.storageAccessKeyIdTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Access Key ID:";
            // 
            // secretAccessKeyTextBox
            // 
            this.secretAccessKeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secretAccessKeyTextBox.Location = new System.Drawing.Point(13, 104);
            this.secretAccessKeyTextBox.Name = "secretAccessKeyTextBox";
            this.secretAccessKeyTextBox.Size = new System.Drawing.Size(361, 20);
            this.secretAccessKeyTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Secret Access Key:";
            // 
            // storageTabControl
            // 
            this.storageTabControl.Controls.Add(this.glacierTabPage);
            this.storageTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storageTabControl.Location = new System.Drawing.Point(0, 43);
            this.storageTabControl.Name = "storageTabControl";
            this.storageTabControl.SelectedIndex = 0;
            this.storageTabControl.Size = new System.Drawing.Size(395, 315);
            this.storageTabControl.TabIndex = 11;
            // 
            // glacierTabPage
            // 
            this.glacierTabPage.Controls.Add(this.linkLabel5);
            this.glacierTabPage.Controls.Add(this.linkLabel3);
            this.glacierTabPage.Controls.Add(this.linkLabel2);
            this.glacierTabPage.Controls.Add(this.label10);
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
            this.glacierTabPage.Size = new System.Drawing.Size(387, 288);
            this.glacierTabPage.TabIndex = 0;
            this.glacierTabPage.Text = "Amazon Glacier";
            this.glacierTabPage.UseVisualStyleBackColor = true;
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(85, 261);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(231, 14);
            this.linkLabel5.TabIndex = 26;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "Read documentation about regions";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(10, 127);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(245, 14);
            this.linkLabel3.TabIndex = 25;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Read how to fill Secret Access Key";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(10, 59);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(217, 14);
            this.linkLabel2.TabIndex = 24;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Read how to fill Access Key ID";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Location = new System.Drawing.Point(13, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(361, 31);
            this.label10.TabIndex = 13;
            this.label10.Text = "Name of your Glacier vault. The vault will be created if it does not exist";
            // 
            // regionComboBox
            // 
            this.regionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.regionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(88, 236);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(283, 22);
            this.regionComboBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 14);
            this.label5.TabIndex = 10;
            this.label5.Text = "Region:";
            // 
            // glacierVaultTextBox
            // 
            this.glacierVaultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glacierVaultTextBox.Location = new System.Drawing.Point(13, 171);
            this.glacierVaultTextBox.Name = "glacierVaultTextBox";
            this.glacierVaultTextBox.Size = new System.Drawing.Size(361, 20);
            this.glacierVaultTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "Vault:";
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
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "021.png");
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(404, 43);
            this.label9.TabIndex = 21;
            this.label9.Text = "Your files storages. Storage can be folder on your computer or folder on Google D" +
    "rive.";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 55);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel4);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.storageTabControl);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.driveTabControl);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Size = new System.Drawing.Size(807, 358);
            this.splitContainer1.SplitterDistance = 395;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 22;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(34, 0);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(105, 14);
            this.linkLabel4.TabIndex = 24;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Amazon Glacier";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(122, 47);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(245, 14);
            this.linkLabel1.TabIndex = 23;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "How to add user for Amazon Glacier";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(395, 43);
            this.label8.TabIndex = 21;
            this.label8.Text = "Your Amazon Glacier account settings. You should have amazon account and configur" +
    "ed user.";
            // 
            // driveTabControl
            // 
            this.driveTabControl.ContextMenuStrip = this.contextMenuStrip1;
            this.driveTabControl.Controls.Add(this.addDriveTabPage);
            this.driveTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.driveTabControl.ImageList = this.imageList1;
            this.driveTabControl.Location = new System.Drawing.Point(0, 43);
            this.driveTabControl.Name = "driveTabControl";
            this.driveTabControl.SelectedIndex = 0;
            this.driveTabControl.Size = new System.Drawing.Size(404, 315);
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
            this.addDriveTabPage.Size = new System.Drawing.Size(396, 288);
            this.addDriveTabPage.TabIndex = 0;
            this.addDriveTabPage.Text = "Add";
            this.addDriveTabPage.UseVisualStyleBackColor = true;
            // 
            // AccountForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(831, 463);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.accountNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AccountForm_FormClosed);
            this.storageTabControl.ResumeLayout(false);
            this.glacierTabPage.ResumeLayout(false);
            this.glacierTabPage.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.driveTabControl.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox glacierVaultTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private DraggableTabControl driveTabControl;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl storageTabControl;
        private System.Windows.Forms.TabPage glacierTabPage;
        private System.Windows.Forms.TabPage addDriveTabPage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteAccountToolStripMenuItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel5;
    }

}