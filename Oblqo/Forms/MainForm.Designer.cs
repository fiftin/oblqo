namespace Oblqo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.btnNewConnection = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tasksTabPage = new System.Windows.Forms.TabPage();
            this.logTabPage = new System.Windows.Forms.TabPage();
            this.logListView = new System.Windows.Forms.ListView();
            this.logDataTimeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logMessageColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.taskColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.taskTypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PercentColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileDateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileSizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mainTool = new System.Windows.Forms.ToolStrip();
            this.newAccountStripButton = new System.Windows.Forms.ToolStripButton();
            this.uploadToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.uploadFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFolderToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.refreshFilesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.aboutStripButton = new System.Windows.Forms.ToolStripButton();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.accountMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.changeAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.newFolderToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFolderToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFilesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.downloadFromDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadFromArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAuthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.loadingFileListProgressBar = new System.Windows.Forms.ProgressBar();
            this.loadingImageProgressBar = new System.Windows.Forms.ProgressBar();
            this.folderMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadFolderFromDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadFolderFromStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFolderToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFilesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadingFoldersTimer = new System.Windows.Forms.Timer(this.components);
            this.logMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDescriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indicateErrorTimer = new System.Windows.Forms.Timer(this.components);
            this.imageViewer1 = new Oblqo.Controls.ImageViewer();
            this.driveStrip1 = new Oblqo.Controls.DriveStrip();
            this.currentDirectoryInfoPanel = new Oblqo.FileListStatusBar();
            this.fileListView = new Oblqo.Controls.FileList();
            this.fileInfoPanel = new Oblqo.Controls.DriveFileControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.multipleFileView1 = new Oblqo.Controls.MultipleFileView();
            this.taskListView = new Oblqo.Controls.TaskList();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tasksTabPage.SuspendLayout();
            this.logTabPage.SuspendLayout();
            this.mainTool.SuspendLayout();
            this.accountMenu.SuspendLayout();
            this.folderMenu.SuspendLayout();
            this.logMenu.SuspendLayout();
            this.fileInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            resources.ApplyResources(this.splitContainer2.Panel1, "splitContainer2.Panel1");
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer2.Panel2
            // 
            resources.ApplyResources(this.splitContainer2.Panel2, "splitContainer2.Panel2");
            this.splitContainer2.Panel2.Controls.Add(this.btnNewConnection);
            this.splitContainer2.Panel2.Controls.Add(this.fileListView);
            this.splitContainer2.Panel2.Controls.Add(this.splitter1);
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // treeView1
            // 
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.HideSelection = false;
            this.treeView1.ImageList = this.smallImageList;
            this.treeView1.ItemHeight = 20;
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // smallImageList
            // 
            this.smallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImageList.ImageStream")));
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImageList.Images.SetKeyName(0, "folder");
            this.smallImageList.Images.SetKeyName(1, "file_old");
            this.smallImageList.Images.SetKeyName(2, "download");
            this.smallImageList.Images.SetKeyName(3, "upload");
            this.smallImageList.Images.SetKeyName(4, "cloud_download");
            this.smallImageList.Images.SetKeyName(5, "cloud_upload");
            this.smallImageList.Images.SetKeyName(6, "file_text");
            this.smallImageList.Images.SetKeyName(7, "account");
            this.smallImageList.Images.SetKeyName(8, "account_disconnected");
            this.smallImageList.Images.SetKeyName(9, "delete");
            this.smallImageList.Images.SetKeyName(10, "process");
            this.smallImageList.Images.SetKeyName(11, "process90");
            this.smallImageList.Images.SetKeyName(12, "process180");
            this.smallImageList.Images.SetKeyName(13, "process270");
            this.smallImageList.Images.SetKeyName(14, "error");
            this.smallImageList.Images.SetKeyName(15, "info");
            this.smallImageList.Images.SetKeyName(16, "file_image_gif");
            this.smallImageList.Images.SetKeyName(17, "file_image_jpeg");
            this.smallImageList.Images.SetKeyName(18, "file_image_png");
            this.smallImageList.Images.SetKeyName(19, "file");
            this.smallImageList.Images.SetKeyName(20, "file_image");
            this.smallImageList.Images.SetKeyName(21, "file_text");
            this.smallImageList.Images.SetKeyName(22, "error_red");
            this.smallImageList.Images.SetKeyName(23, "ok");
            this.smallImageList.Images.SetKeyName(24, "run");
            this.smallImageList.Images.SetKeyName(25, "pause");
            this.smallImageList.Images.SetKeyName(26, "cancel");
            this.smallImageList.Images.SetKeyName(27, "queued");
            // 
            // btnNewConnection
            // 
            resources.ApplyResources(this.btnNewConnection, "btnNewConnection");
            this.btnNewConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewConnection.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNewConnection.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnNewConnection.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnNewConnection.ForeColor = System.Drawing.Color.White;
            this.btnNewConnection.Name = "btnNewConnection";
            this.btnNewConnection.UseVisualStyleBackColor = false;
            this.btnNewConnection.Click += new System.EventHandler(this.addNewAccountToolStripMenuItem_Click);
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.fileInfoPanel);
            this.panel1.Controls.Add(this.multipleFileView1);
            this.panel1.Name = "panel1";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tasksTabPage);
            this.tabControl1.Controls.Add(this.logTabPage);
            this.tabControl1.ImageList = this.smallImageList;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Deselecting);
            // 
            // tasksTabPage
            // 
            resources.ApplyResources(this.tasksTabPage, "tasksTabPage");
            this.tasksTabPage.Controls.Add(this.taskListView);
            this.tasksTabPage.Name = "tasksTabPage";
            this.tasksTabPage.UseVisualStyleBackColor = true;
            // 
            // logTabPage
            // 
            resources.ApplyResources(this.logTabPage, "logTabPage");
            this.logTabPage.Controls.Add(this.logListView);
            this.logTabPage.Name = "logTabPage";
            this.logTabPage.UseVisualStyleBackColor = true;
            // 
            // logListView
            // 
            resources.ApplyResources(this.logListView, "logListView");
            this.logListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.logDataTimeColumnHeader,
            this.logMessageColumnHeader});
            this.logListView.FullRowSelect = true;
            this.logListView.MultiSelect = false;
            this.logListView.Name = "logListView";
            this.logListView.SmallImageList = this.smallImageList;
            this.logListView.UseCompatibleStateImageBehavior = false;
            this.logListView.View = System.Windows.Forms.View.Details;
            this.logListView.SelectedIndexChanged += new System.EventHandler(this.logListView_SelectedIndexChanged);
            this.logListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.logListView_MouseDown);
            // 
            // logDataTimeColumnHeader
            // 
            resources.ApplyResources(this.logDataTimeColumnHeader, "logDataTimeColumnHeader");
            // 
            // logMessageColumnHeader
            // 
            resources.ApplyResources(this.logMessageColumnHeader, "logMessageColumnHeader");
            // 
            // taskColumnHeader
            // 
            resources.ApplyResources(this.taskColumnHeader, "taskColumnHeader");
            // 
            // taskTypeColumnHeader
            // 
            resources.ApplyResources(this.taskTypeColumnHeader, "taskTypeColumnHeader");
            // 
            // sizeColumnHeader
            // 
            resources.ApplyResources(this.sizeColumnHeader, "sizeColumnHeader");
            // 
            // PercentColumnHeader
            // 
            resources.ApplyResources(this.PercentColumnHeader, "PercentColumnHeader");
            // 
            // fileNameColumnHeader
            // 
            resources.ApplyResources(this.fileNameColumnHeader, "fileNameColumnHeader");
            // 
            // fileDateColumnHeader
            // 
            resources.ApplyResources(this.fileDateColumnHeader, "fileDateColumnHeader");
            // 
            // fileSizeColumnHeader
            // 
            resources.ApplyResources(this.fileSizeColumnHeader, "fileSizeColumnHeader");
            // 
            // mainTool
            // 
            resources.ApplyResources(this.mainTool, "mainTool");
            this.mainTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAccountStripButton,
            this.uploadToolStripDropDownButton,
            this.newFolderToolStripButton,
            this.refreshFilesToolStripButton,
            this.aboutStripButton,
            this.helpToolStripButton});
            this.mainTool.Name = "mainTool";
            // 
            // newAccountStripButton
            // 
            resources.ApplyResources(this.newAccountStripButton, "newAccountStripButton");
            this.newAccountStripButton.Name = "newAccountStripButton";
            this.newAccountStripButton.Click += new System.EventHandler(this.addNewAccountToolStripMenuItem_Click);
            // 
            // uploadToolStripDropDownButton
            // 
            resources.ApplyResources(this.uploadToolStripDropDownButton, "uploadToolStripDropDownButton");
            this.uploadToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadFolderToolStripMenuItem,
            this.uploadFileToolStripMenuItem});
            this.uploadToolStripDropDownButton.Margin = new System.Windows.Forms.Padding(130, 1, 0, 2);
            this.uploadToolStripDropDownButton.Name = "uploadToolStripDropDownButton";
            // 
            // uploadFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.uploadFolderToolStripMenuItem, "uploadFolderToolStripMenuItem");
            this.uploadFolderToolStripMenuItem.Name = "uploadFolderToolStripMenuItem";
            this.uploadFolderToolStripMenuItem.Click += new System.EventHandler(this.uploadFolderToolStripMenuItem_Click);
            // 
            // uploadFileToolStripMenuItem
            // 
            resources.ApplyResources(this.uploadFileToolStripMenuItem, "uploadFileToolStripMenuItem");
            this.uploadFileToolStripMenuItem.Name = "uploadFileToolStripMenuItem";
            this.uploadFileToolStripMenuItem.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // newFolderToolStripButton
            // 
            resources.ApplyResources(this.newFolderToolStripButton, "newFolderToolStripButton");
            this.newFolderToolStripButton.Name = "newFolderToolStripButton";
            this.newFolderToolStripButton.Click += new System.EventHandler(this.newFolderToolStripButton_Click);
            // 
            // refreshFilesToolStripButton
            // 
            resources.ApplyResources(this.refreshFilesToolStripButton, "refreshFilesToolStripButton");
            this.refreshFilesToolStripButton.Name = "refreshFilesToolStripButton";
            this.refreshFilesToolStripButton.Click += new System.EventHandler(this.refreshFilesToolStripButton_Click);
            // 
            // aboutStripButton
            // 
            resources.ApplyResources(this.aboutStripButton, "aboutStripButton");
            this.aboutStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.aboutStripButton.Name = "aboutStripButton";
            this.aboutStripButton.Click += new System.EventHandler(this.aboutStripButton_Click);
            // 
            // helpToolStripButton
            // 
            resources.ApplyResources(this.helpToolStripButton, "helpToolStripButton");
            this.helpToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // accountMenu
            // 
            resources.ApplyResources(this.accountMenu, "accountMenu");
            this.accountMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.toolStripSeparator2,
            this.changeAccountToolStripMenuItem,
            this.deleteAccountToolStripMenuItem,
            this.cloneAccountToolStripMenuItem,
            this.toolStripSeparator5,
            this.newFolderToolStripMenuItem2,
            this.uploadFolderToolStripMenuItem3,
            this.uploadFilesToolStripMenuItem2,
            this.toolStripSeparator6,
            this.downloadFromDriveToolStripMenuItem,
            this.downloadFromArchiveToolStripMenuItem,
            this.toolStripSeparator10,
            this.clearAuthToolStripMenuItem,
            this.downloadInventoryToolStripMenuItem});
            this.accountMenu.Name = "accountContextMenuStrip";
            // 
            // connectToolStripMenuItem
            // 
            resources.ApplyResources(this.connectToolStripMenuItem, "connectToolStripMenuItem");
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            resources.ApplyResources(this.disconnectToolStripMenuItem, "disconnectToolStripMenuItem");
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // changeAccountToolStripMenuItem
            // 
            resources.ApplyResources(this.changeAccountToolStripMenuItem, "changeAccountToolStripMenuItem");
            this.changeAccountToolStripMenuItem.Name = "changeAccountToolStripMenuItem";
            this.changeAccountToolStripMenuItem.Click += new System.EventHandler(this.changeAccountToolStripMenuItem_Click);
            // 
            // deleteAccountToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteAccountToolStripMenuItem, "deleteAccountToolStripMenuItem");
            this.deleteAccountToolStripMenuItem.Name = "deleteAccountToolStripMenuItem";
            this.deleteAccountToolStripMenuItem.Click += new System.EventHandler(this.deleteAccountToolStripMenuItem_Click);
            // 
            // cloneAccountToolStripMenuItem
            // 
            resources.ApplyResources(this.cloneAccountToolStripMenuItem, "cloneAccountToolStripMenuItem");
            this.cloneAccountToolStripMenuItem.Name = "cloneAccountToolStripMenuItem";
            this.cloneAccountToolStripMenuItem.Click += new System.EventHandler(this.cloneAccountToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // newFolderToolStripMenuItem2
            // 
            resources.ApplyResources(this.newFolderToolStripMenuItem2, "newFolderToolStripMenuItem2");
            this.newFolderToolStripMenuItem2.Name = "newFolderToolStripMenuItem2";
            this.newFolderToolStripMenuItem2.Click += new System.EventHandler(this.newFolderToolStripButton_Click);
            // 
            // uploadFolderToolStripMenuItem3
            // 
            resources.ApplyResources(this.uploadFolderToolStripMenuItem3, "uploadFolderToolStripMenuItem3");
            this.uploadFolderToolStripMenuItem3.Name = "uploadFolderToolStripMenuItem3";
            this.uploadFolderToolStripMenuItem3.Click += new System.EventHandler(this.uploadFolderToolStripMenuItem_Click);
            // 
            // uploadFilesToolStripMenuItem2
            // 
            resources.ApplyResources(this.uploadFilesToolStripMenuItem2, "uploadFilesToolStripMenuItem2");
            this.uploadFilesToolStripMenuItem2.Name = "uploadFilesToolStripMenuItem2";
            this.uploadFilesToolStripMenuItem2.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // downloadFromDriveToolStripMenuItem
            // 
            resources.ApplyResources(this.downloadFromDriveToolStripMenuItem, "downloadFromDriveToolStripMenuItem");
            this.downloadFromDriveToolStripMenuItem.Name = "downloadFromDriveToolStripMenuItem";
            this.downloadFromDriveToolStripMenuItem.Click += new System.EventHandler(this.downloadFromDriveToolStripMenuItem_Click);
            // 
            // downloadFromArchiveToolStripMenuItem
            // 
            resources.ApplyResources(this.downloadFromArchiveToolStripMenuItem, "downloadFromArchiveToolStripMenuItem");
            this.downloadFromArchiveToolStripMenuItem.Name = "downloadFromArchiveToolStripMenuItem";
            // 
            // toolStripSeparator10
            // 
            resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            // 
            // clearAuthToolStripMenuItem
            // 
            resources.ApplyResources(this.clearAuthToolStripMenuItem, "clearAuthToolStripMenuItem");
            this.clearAuthToolStripMenuItem.Name = "clearAuthToolStripMenuItem";
            this.clearAuthToolStripMenuItem.Click += new System.EventHandler(this.clearAuthToolStripMenuItem_Click);
            // 
            // downloadInventoryToolStripMenuItem
            // 
            resources.ApplyResources(this.downloadInventoryToolStripMenuItem, "downloadInventoryToolStripMenuItem");
            this.downloadInventoryToolStripMenuItem.Name = "downloadInventoryToolStripMenuItem";
            this.downloadInventoryToolStripMenuItem.Click += new System.EventHandler(this.downloadInventoryToolStripMenuItem_Click);
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            this.openFileDialog1.Multiselect = true;
            // 
            // loadingFileListProgressBar
            // 
            resources.ApplyResources(this.loadingFileListProgressBar, "loadingFileListProgressBar");
            this.loadingFileListProgressBar.Name = "loadingFileListProgressBar";
            this.loadingFileListProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.loadingFileListProgressBar.Value = 10;
            // 
            // loadingImageProgressBar
            // 
            resources.ApplyResources(this.loadingImageProgressBar, "loadingImageProgressBar");
            this.loadingImageProgressBar.Name = "loadingImageProgressBar";
            this.loadingImageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.loadingImageProgressBar.Value = 10;
            // 
            // folderMenu
            // 
            resources.ApplyResources(this.folderMenu, "folderMenu");
            this.folderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadFolderFromDriveToolStripMenuItem,
            this.downloadFolderFromStorageToolStripMenuItem,
            this.toolStripSeparator3,
            this.newFolderToolStripMenuItem,
            this.uploadFolderToolStripMenuItem2,
            this.uploadFilesToolStripMenuItem1,
            this.toolStripSeparator4,
            this.deleteFolderToolStripMenuItem});
            this.folderMenu.Name = "folderContextMenuStrip";
            // 
            // downloadFolderFromDriveToolStripMenuItem
            // 
            resources.ApplyResources(this.downloadFolderFromDriveToolStripMenuItem, "downloadFolderFromDriveToolStripMenuItem");
            this.downloadFolderFromDriveToolStripMenuItem.Name = "downloadFolderFromDriveToolStripMenuItem";
            this.downloadFolderFromDriveToolStripMenuItem.Click += new System.EventHandler(this.downloadFolderFromDriveToolStripMenuItem_Click);
            // 
            // downloadFolderFromStorageToolStripMenuItem
            // 
            resources.ApplyResources(this.downloadFolderFromStorageToolStripMenuItem, "downloadFolderFromStorageToolStripMenuItem");
            this.downloadFolderFromStorageToolStripMenuItem.Name = "downloadFolderFromStorageToolStripMenuItem";
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // newFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.newFolderToolStripMenuItem, "newFolderToolStripMenuItem");
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripButton_Click);
            // 
            // uploadFolderToolStripMenuItem2
            // 
            resources.ApplyResources(this.uploadFolderToolStripMenuItem2, "uploadFolderToolStripMenuItem2");
            this.uploadFolderToolStripMenuItem2.Name = "uploadFolderToolStripMenuItem2";
            this.uploadFolderToolStripMenuItem2.Click += new System.EventHandler(this.uploadFolderToolStripMenuItem_Click);
            // 
            // uploadFilesToolStripMenuItem1
            // 
            resources.ApplyResources(this.uploadFilesToolStripMenuItem1, "uploadFilesToolStripMenuItem1");
            this.uploadFilesToolStripMenuItem1.Name = "uploadFilesToolStripMenuItem1";
            this.uploadFilesToolStripMenuItem1.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // deleteFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteFolderToolStripMenuItem, "deleteFolderToolStripMenuItem");
            this.deleteFolderToolStripMenuItem.Name = "deleteFolderToolStripMenuItem";
            this.deleteFolderToolStripMenuItem.Click += new System.EventHandler(this.deleteFolderToolStripMenuItem_Click);
            // 
            // loadingFoldersTimer
            // 
            this.loadingFoldersTimer.Enabled = true;
            this.loadingFoldersTimer.Interval = 150;
            this.loadingFoldersTimer.Tick += new System.EventHandler(this.loadingFoldersTimer_Tick);
            // 
            // logMenu
            // 
            resources.ApplyResources(this.logMenu, "logMenu");
            this.logMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDescriptionToolStripMenuItem});
            this.logMenu.Name = "logMenu";
            // 
            // showDescriptionToolStripMenuItem
            // 
            resources.ApplyResources(this.showDescriptionToolStripMenuItem, "showDescriptionToolStripMenuItem");
            this.showDescriptionToolStripMenuItem.Name = "showDescriptionToolStripMenuItem";
            this.showDescriptionToolStripMenuItem.Click += new System.EventHandler(this.showDescriptionToolStripMenuItem_Click);
            // 
            // indicateErrorTimer
            // 
            this.indicateErrorTimer.Interval = 150;
            this.indicateErrorTimer.Tick += new System.EventHandler(this.indicateErrorTimer_Tick);
            // 
            // imageViewer1
            // 
            resources.ApplyResources(this.imageViewer1, "imageViewer1");
            this.imageViewer1.FileName = "label1";
            this.imageViewer1.Name = "imageViewer1";
            this.imageViewer1.PictureRightMouseDown += new System.EventHandler(this.imageViewer1_PictureRightMouseDown);
            this.imageViewer1.SelectedDriveChanged += new System.EventHandler(this.imageViewer1_SelectedDriveChanged);
            this.imageViewer1.Slide += new System.EventHandler<Oblqo.Controls.SlideEventArgs>(this.imageViewer1_Slide);
            // 
            // driveStrip1
            // 
            resources.ApplyResources(this.driveStrip1, "driveStrip1");
            this.driveStrip1.Name = "driveStrip1";
            this.driveStrip1.SelectedDrive = null;
            this.driveStrip1.SelectedDriveChanged += new System.EventHandler(this.driveStrip1_SelectedDriveChanged);
            // 
            // currentDirectoryInfoPanel
            // 
            resources.ApplyResources(this.currentDirectoryInfoPanel, "currentDirectoryInfoPanel");
            this.currentDirectoryInfoPanel.Name = "currentDirectoryInfoPanel";
            this.currentDirectoryInfoPanel.NumberOfFiles = 0;
            this.currentDirectoryInfoPanel.NumberOfUnsyncronizedFiles = 0;
            this.currentDirectoryInfoPanel.FilterChanged += new System.EventHandler<System.EventArgs>(this.currentDirectoryInfoPanel_FilterChanged);
            // 
            // fileListView
            // 
            resources.ApplyResources(this.fileListView, "fileListView");
            this.fileListView.CurrentDirectoryInfoPanel = this.currentDirectoryInfoPanel;
            this.fileListView.Name = "fileListView";
            this.fileListView.SmallImageList = this.smallImageList;
            this.fileListView.TaskManager = null;
            this.fileListView.FileDoubleClick += new System.EventHandler(this.fileListView_FileDoubleClick);
            this.fileListView.SelectedIndexChanged += new System.EventHandler(this.fileListView_SelectedIndexChanged);
            this.fileListView.FileLoaded += new System.EventHandler(this.fileListView_FileLoaded);
            this.fileListView.Error += new System.EventHandler<Oblqo.ExceptionEventArgs>(this.xxx_Exception);
            this.fileListView.SizeChanged += new System.EventHandler(this.fileListView_SizeChanged);
            this.fileListView.Move += new System.EventHandler(this.listView1_Move);
            this.fileListView.Resize += new System.EventHandler(this.listView1_Resize);
            // 
            // fileInfoPanel
            // 
            resources.ApplyResources(this.fileInfoPanel, "fileInfoPanel");
            this.fileInfoPanel.Controls.Add(this.pictureBox1);
            this.fileInfoPanel.Name = "fileInfoPanel";
            this.fileInfoPanel.Error += new System.EventHandler<Oblqo.ExceptionEventArgs>(this.xxx_Exception);
            this.fileInfoPanel.ImageLoading += new System.EventHandler<System.EventArgs>(this.fileInfoPanel_ImageLoading);
            this.fileInfoPanel.ImageLoaded += new System.EventHandler<System.EventArgs>(this.fileInfoPanel_ImageLoaded);
            this.fileInfoPanel.ZoomClicked += new System.EventHandler(this.fileInfoPanel_ZoomClicked);
            this.fileInfoPanel.PictureRightMouseDown += new System.EventHandler(this.fileInfoPanel_PictureRightMouseDown);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // multipleFileView1
            // 
            resources.ApplyResources(this.multipleFileView1, "multipleFileView1");
            this.multipleFileView1.Name = "multipleFileView1";
            // 
            // taskListView
            // 
            resources.ApplyResources(this.taskListView, "taskListView");
            this.taskListView.Name = "taskListView";
            this.taskListView.SmallImageList = this.smallImageList;
            this.taskListView.Error += new System.EventHandler<Oblqo.ExceptionEventArgs>(this.xxx_Exception);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageViewer1);
            this.Controls.Add(this.driveStrip1);
            this.Controls.Add(this.loadingImageProgressBar);
            this.Controls.Add(this.loadingFileListProgressBar);
            this.Controls.Add(this.currentDirectoryInfoPanel);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainTool);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tasksTabPage.ResumeLayout(false);
            this.logTabPage.ResumeLayout(false);
            this.mainTool.ResumeLayout(false);
            this.mainTool.PerformLayout();
            this.accountMenu.ResumeLayout(false);
            this.folderMenu.ResumeLayout(false);
            this.logMenu.ResumeLayout(false);
            this.fileInfoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private Controls.FileList fileListView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tasksTabPage;
        private Controls.TaskList taskListView;
        private System.Windows.Forms.ColumnHeader taskColumnHeader;
        private System.Windows.Forms.ColumnHeader sizeColumnHeader;
        private System.Windows.Forms.ColumnHeader PercentColumnHeader;
        private System.Windows.Forms.ToolStrip mainTool;
        private System.Windows.Forms.ToolStripButton newFolderToolStripButton;
        private System.Windows.Forms.ToolStripButton refreshFilesToolStripButton;
        private System.Windows.Forms.ToolStripDropDownButton uploadToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem uploadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFolderToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip accountMenu;
        private System.Windows.Forms.ToolStripMenuItem changeAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ImageList smallImageList;
        private System.Windows.Forms.ColumnHeader fileNameColumnHeader;
        private System.Windows.Forms.ColumnHeader fileDateColumnHeader;
        private System.Windows.Forms.ColumnHeader fileSizeColumnHeader;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar loadingFileListProgressBar;
        private System.Windows.Forms.ProgressBar loadingImageProgressBar;
        private System.Windows.Forms.ContextMenuStrip folderMenu;
        private System.Windows.Forms.ToolStripMenuItem downloadFolderFromDriveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFolderToolStripMenuItem;
        private System.Windows.Forms.TabPage logTabPage;
        private System.Windows.Forms.ListView logListView;
        private System.Windows.Forms.ToolStripMenuItem downloadFolderFromStorageToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader logDataTimeColumnHeader;
        private System.Windows.Forms.ColumnHeader logMessageColumnHeader;
        private System.Windows.Forms.Timer loadingFoldersTimer;
        private System.Windows.Forms.ToolStripMenuItem downloadFromDriveToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip logMenu;
        private System.Windows.Forms.ToolStripMenuItem showDescriptionToolStripMenuItem;
        private System.Windows.Forms.Timer indicateErrorTimer;
        private System.Windows.Forms.ColumnHeader taskTypeColumnHeader;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem uploadFolderToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem uploadFilesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem uploadFolderToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem uploadFilesToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem downloadFromArchiveToolStripMenuItem;
        private FileListStatusBar currentDirectoryInfoPanel;
        private System.Windows.Forms.ToolStripButton newAccountStripButton;
        private Controls.DriveFileControl fileInfoPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Controls.DriveStrip driveStrip1;
        private System.Windows.Forms.ToolStripButton aboutStripButton;
        private System.Windows.Forms.Button btnNewConnection;
        private System.Windows.Forms.ToolStripMenuItem cloneAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem clearAuthToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private Controls.ImageViewer imageViewer1;
        private System.Windows.Forms.Panel panel1;
        private Controls.MultipleFileView multipleFileView1;
        private System.Windows.Forms.ToolStripMenuItem downloadInventoryToolStripMenuItem;
    }
}

