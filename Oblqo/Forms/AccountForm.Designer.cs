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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.storageTabControl = new System.Windows.Forms.TabControl();
            this.glacierTabPage = new System.Windows.Forms.TabPage();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.storageAccessKeyIdTextBox = new System.Windows.Forms.TextBox();
            this.glacierVaultTextBox = new System.Windows.Forms.TextBox();
            this.secretAccessKeyTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.driveTabControl = new Oblqo.DraggableTabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDriveTabPage = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.accountNameTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.storageTabControl.SuspendLayout();
            this.glacierTabPage.SuspendLayout();
            this.driveTabControl.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel4);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.storageTabControl);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.driveTabControl);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            // 
            // linkLabel4
            // 
            resources.ApplyResources(this.linkLabel4, "linkLabel4");
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.TabStop = true;
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // storageTabControl
            // 
            resources.ApplyResources(this.storageTabControl, "storageTabControl");
            this.storageTabControl.Controls.Add(this.glacierTabPage);
            this.storageTabControl.Name = "storageTabControl";
            this.storageTabControl.SelectedIndex = 0;
            // 
            // glacierTabPage
            // 
            resources.ApplyResources(this.glacierTabPage, "glacierTabPage");
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
            this.glacierTabPage.Name = "glacierTabPage";
            this.glacierTabPage.UseVisualStyleBackColor = true;
            // 
            // linkLabel5
            // 
            resources.ApplyResources(this.linkLabel5, "linkLabel5");
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.TabStop = true;
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // linkLabel3
            // 
            resources.ApplyResources(this.linkLabel3, "linkLabel3");
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.TabStop = true;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel2
            // 
            resources.ApplyResources(this.linkLabel2, "linkLabel2");
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.TabStop = true;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Name = "label10";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // regionComboBox
            // 
            resources.ApplyResources(this.regionComboBox, "regionComboBox");
            this.regionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Name = "regionComboBox";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // storageAccessKeyIdTextBox
            // 
            resources.ApplyResources(this.storageAccessKeyIdTextBox, "storageAccessKeyIdTextBox");
            this.storageAccessKeyIdTextBox.Name = "storageAccessKeyIdTextBox";
            // 
            // glacierVaultTextBox
            // 
            resources.ApplyResources(this.glacierVaultTextBox, "glacierVaultTextBox");
            this.glacierVaultTextBox.Name = "glacierVaultTextBox";
            // 
            // secretAccessKeyTextBox
            // 
            resources.ApplyResources(this.secretAccessKeyTextBox, "secretAccessKeyTextBox");
            this.secretAccessKeyTextBox.Name = "secretAccessKeyTextBox";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Name = "label8";
            // 
            // driveTabControl
            // 
            resources.ApplyResources(this.driveTabControl, "driveTabControl");
            this.driveTabControl.ContextMenuStrip = this.contextMenuStrip1;
            this.driveTabControl.Controls.Add(this.addDriveTabPage);
            this.driveTabControl.ImageList = this.imageList1;
            this.driveTabControl.Name = "driveTabControl";
            this.driveTabControl.SelectedIndex = 0;
            this.driveTabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.driveTabControl_Selecting);
            this.driveTabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.driveTabControl_MouseClick);
            this.driveTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.driveTabControl_MouseDown);
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteAccountToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // deleteAccountToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteAccountToolStripMenuItem, "deleteAccountToolStripMenuItem");
            this.deleteAccountToolStripMenuItem.Name = "deleteAccountToolStripMenuItem";
            this.deleteAccountToolStripMenuItem.Click += new System.EventHandler(this.deleteAccountToolStripMenuItem_Click);
            // 
            // addDriveTabPage
            // 
            resources.ApplyResources(this.addDriveTabPage, "addDriveTabPage");
            this.addDriveTabPage.Name = "addDriveTabPage";
            this.addDriveTabPage.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "021.png");
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Name = "label9";
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // accountNameTextBox
            // 
            resources.ApplyResources(this.accountNameTextBox, "accountNameTextBox");
            this.accountNameTextBox.Name = "accountNameTextBox";
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // AccountForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.accountNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountForm_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.storageTabControl.ResumeLayout(false);
            this.glacierTabPage.ResumeLayout(false);
            this.glacierTabPage.PerformLayout();
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