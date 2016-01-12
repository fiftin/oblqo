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
            this.fileListView = new System.Windows.Forms.ListView();
            this.fileNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileDateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileSizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.fileInfoPanel = new Oblqo.Controls.DriveFileControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tasksTabPage = new System.Windows.Forms.TabPage();
            this.taskListView = new System.Windows.Forms.ListView();
            this.taskColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.taskTypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PercentColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.taskMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancelTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tasksToolStrip = new System.Windows.Forms.ToolStrip();
            this.activeTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.finishedTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.cancelledTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.queuedTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.logTabPage = new System.Windows.Forms.TabPage();
            this.logListView = new System.Windows.Forms.ListView();
            this.logDataTimeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logMessageColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.loadingFileListProgressBar = new System.Windows.Forms.ProgressBar();
            this.loadingImageProgressBar = new System.Windows.Forms.ProgressBar();
            this.fileMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadFileFromStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadFileFromDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.synchronizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.synchronizeOnDrivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFromArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openContainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.fileListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.driveStrip1 = new Oblqo.Controls.DriveStrip();
            this.currentDirectoryInfoPanel = new Oblqo.FileListStatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.fileInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tasksTabPage.SuspendLayout();
            this.taskMenu.SuspendLayout();
            this.tasksToolStrip.SuspendLayout();
            this.logTabPage.SuspendLayout();
            this.mainTool.SuspendLayout();
            this.accountMenu.SuspendLayout();
            this.fileMenu.SuspendLayout();
            this.folderMenu.SuspendLayout();
            this.logMenu.SuspendLayout();
            this.fileListMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(2, 0, 0, 1);
            this.splitContainer1.Size = new System.Drawing.Size(997, 566);
            this.splitContainer1.SplitterDistance = 267;
            this.splitContainer1.SplitterWidth = 9;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.splitContainer2.Panel1MinSize = 250;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnNewConnection);
            this.splitContainer2.Panel2.Controls.Add(this.fileListView);
            this.splitContainer2.Panel2.Controls.Add(this.splitter1);
            this.splitContainer2.Panel2.Controls.Add(this.fileInfoPanel);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.splitContainer2.Size = new System.Drawing.Size(997, 267);
            this.splitContainer2.SplitterDistance = 250;
            this.splitContainer2.SplitterWidth = 8;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.smallImageList;
            this.treeView1.ItemHeight = 20;
            this.treeView1.Location = new System.Drawing.Point(2, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(248, 267);
            this.treeView1.TabIndex = 1;
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
            this.btnNewConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewConnection.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNewConnection.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnNewConnection.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnNewConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewConnection.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewConnection.ForeColor = System.Drawing.Color.White;
            this.btnNewConnection.Location = new System.Drawing.Point(39, 68);
            this.btnNewConnection.Name = "btnNewConnection";
            this.btnNewConnection.Size = new System.Drawing.Size(271, 51);
            this.btnNewConnection.TabIndex = 4;
            this.btnNewConnection.Text = "CREATE NEW CONNECTION";
            this.btnNewConnection.UseVisualStyleBackColor = false;
            this.btnNewConnection.Click += new System.EventHandler(this.addNewAccountToolStripMenuItem_Click);
            // 
            // fileListView
            // 
            this.fileListView.AllowColumnReorder = true;
            this.fileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileNameColumnHeader,
            this.fileDateColumnHeader,
            this.fileSizeColumnHeader});
            this.fileListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileListView.FullRowSelect = true;
            this.fileListView.Location = new System.Drawing.Point(0, 0);
            this.fileListView.Name = "fileListView";
            this.fileListView.Size = new System.Drawing.Size(457, 267);
            this.fileListView.SmallImageList = this.smallImageList;
            this.fileListView.TabIndex = 1;
            this.fileListView.UseCompatibleStateImageBehavior = false;
            this.fileListView.View = System.Windows.Forms.View.Details;
            this.fileListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.fileListView_ColumnClick);
            this.fileListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.fileListView_DrawItem);
            this.fileListView.SelectedIndexChanged += new System.EventHandler(this.fileListView_SelectedIndexChanged);
            this.fileListView.SizeChanged += new System.EventHandler(this.fileListView_SizeChanged);
            this.fileListView.DoubleClick += new System.EventHandler(this.fileListView_DoubleClick);
            this.fileListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fileListView_KeyDown);
            this.fileListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fileListView_MouseUp);
            this.fileListView.Move += new System.EventHandler(this.listView1_Move);
            this.fileListView.Resize += new System.EventHandler(this.listView1_Resize);
            // 
            // fileNameColumnHeader
            // 
            this.fileNameColumnHeader.Text = "Name";
            this.fileNameColumnHeader.Width = 250;
            // 
            // fileDateColumnHeader
            // 
            this.fileDateColumnHeader.Text = "Date";
            this.fileDateColumnHeader.Width = 100;
            // 
            // fileSizeColumnHeader
            // 
            this.fileSizeColumnHeader.Text = "Size";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(457, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(8, 267);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            this.splitter1.Visible = false;
            // 
            // fileInfoPanel
            // 
            this.fileInfoPanel.Controls.Add(this.pictureBox1);
            this.fileInfoPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.fileInfoPanel.Location = new System.Drawing.Point(465, 0);
            this.fileInfoPanel.MinimumSize = new System.Drawing.Size(272, 0);
            this.fileInfoPanel.Name = "fileInfoPanel";
            this.fileInfoPanel.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.fileInfoPanel.Size = new System.Drawing.Size(272, 267);
            this.fileInfoPanel.TabIndex = 2;
            this.fileInfoPanel.Visible = false;
            this.fileInfoPanel.Error += new System.EventHandler<Oblqo.ExceptionEventArgs>(this.fileInfoPanel_Error);
            this.fileInfoPanel.ImageLoading += new System.EventHandler<System.EventArgs>(this.fileInfoPanel_ImageLoading);
            this.fileInfoPanel.ImageLoaded += new System.EventHandler<System.EventArgs>(this.fileInfoPanel_ImageLoaded);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(3);
            this.pictureBox1.Size = new System.Drawing.Size(270, 267);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tasksTabPage);
            this.tabControl1.Controls.Add(this.logTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.smallImageList;
            this.tabControl1.Location = new System.Drawing.Point(2, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(995, 289);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Deselecting);
            // 
            // tasksTabPage
            // 
            this.tasksTabPage.Controls.Add(this.taskListView);
            this.tasksTabPage.Controls.Add(this.tasksToolStrip);
            this.tasksTabPage.Location = new System.Drawing.Point(4, 24);
            this.tasksTabPage.Name = "tasksTabPage";
            this.tasksTabPage.Padding = new System.Windows.Forms.Padding(0, 2, 2, 1);
            this.tasksTabPage.Size = new System.Drawing.Size(987, 261);
            this.tasksTabPage.TabIndex = 0;
            this.tasksTabPage.Text = "Tasks";
            this.tasksTabPage.UseVisualStyleBackColor = true;
            // 
            // taskListView
            // 
            this.taskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.taskColumnHeader,
            this.taskTypeColumnHeader,
            this.sizeColumnHeader,
            this.PercentColumnHeader});
            this.taskListView.ContextMenuStrip = this.taskMenu;
            this.taskListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskListView.FullRowSelect = true;
            this.taskListView.HideSelection = false;
            this.taskListView.Location = new System.Drawing.Point(0, 2);
            this.taskListView.Name = "taskListView";
            this.taskListView.Size = new System.Drawing.Size(985, 233);
            this.taskListView.SmallImageList = this.smallImageList;
            this.taskListView.TabIndex = 0;
            this.taskListView.UseCompatibleStateImageBehavior = false;
            this.taskListView.View = System.Windows.Forms.View.Details;
            // 
            // taskColumnHeader
            // 
            this.taskColumnHeader.Text = "File/Folder";
            this.taskColumnHeader.Width = 180;
            // 
            // taskTypeColumnHeader
            // 
            this.taskTypeColumnHeader.Text = "Task";
            this.taskTypeColumnHeader.Width = 120;
            // 
            // sizeColumnHeader
            // 
            this.sizeColumnHeader.Text = "Size";
            // 
            // PercentColumnHeader
            // 
            this.PercentColumnHeader.Text = "%";
            // 
            // taskMenu
            // 
            this.taskMenu.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.taskMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelTaskToolStripMenuItem,
            this.taskDetailsToolStripMenuItem});
            this.taskMenu.Name = "taskMenu";
            this.taskMenu.Size = new System.Drawing.Size(124, 48);
            this.taskMenu.Opened += new System.EventHandler(this.taskMenu_Opened);
            // 
            // cancelTaskToolStripMenuItem
            // 
            this.cancelTaskToolStripMenuItem.Name = "cancelTaskToolStripMenuItem";
            this.cancelTaskToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.cancelTaskToolStripMenuItem.Text = "Cancel";
            this.cancelTaskToolStripMenuItem.Click += new System.EventHandler(this.cancelTaskToolStripMenuItem_Click);
            // 
            // taskDetailsToolStripMenuItem
            // 
            this.taskDetailsToolStripMenuItem.Name = "taskDetailsToolStripMenuItem";
            this.taskDetailsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.taskDetailsToolStripMenuItem.Text = "Details";
            this.taskDetailsToolStripMenuItem.Click += new System.EventHandler(this.taskDetailsToolStripMenuItem_Click);
            // 
            // tasksToolStrip
            // 
            this.tasksToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tasksToolStrip.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tasksToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tasksToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeTasksStripButton,
            this.finishedTasksStripButton,
            this.cancelledTasksStripButton,
            this.queuedTasksStripButton});
            this.tasksToolStrip.Location = new System.Drawing.Point(0, 235);
            this.tasksToolStrip.Name = "tasksToolStrip";
            this.tasksToolStrip.Padding = new System.Windows.Forms.Padding(0, 2, 1, 0);
            this.tasksToolStrip.Size = new System.Drawing.Size(985, 25);
            this.tasksToolStrip.TabIndex = 1;
            this.tasksToolStrip.Text = "toolStrip1";
            // 
            // activeTasksStripButton
            // 
            this.activeTasksStripButton.Checked = true;
            this.activeTasksStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activeTasksStripButton.Image = ((System.Drawing.Image)(resources.GetObject("activeTasksStripButton.Image")));
            this.activeTasksStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.activeTasksStripButton.Name = "activeTasksStripButton";
            this.activeTasksStripButton.Size = new System.Drawing.Size(69, 20);
            this.activeTasksStripButton.Text = "Active";
            this.activeTasksStripButton.Click += new System.EventHandler(this.activeTasksStripButton_Click);
            // 
            // finishedTasksStripButton
            // 
            this.finishedTasksStripButton.Image = ((System.Drawing.Image)(resources.GetObject("finishedTasksStripButton.Image")));
            this.finishedTasksStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.finishedTasksStripButton.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.finishedTasksStripButton.Name = "finishedTasksStripButton";
            this.finishedTasksStripButton.Size = new System.Drawing.Size(83, 20);
            this.finishedTasksStripButton.Text = "Finished";
            this.finishedTasksStripButton.Click += new System.EventHandler(this.finishedTasksStripButton_Click);
            // 
            // cancelledTasksStripButton
            // 
            this.cancelledTasksStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cancelledTasksStripButton.Image")));
            this.cancelledTasksStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelledTasksStripButton.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.cancelledTasksStripButton.Name = "cancelledTasksStripButton";
            this.cancelledTasksStripButton.Size = new System.Drawing.Size(90, 20);
            this.cancelledTasksStripButton.Text = "Cancelled";
            this.cancelledTasksStripButton.Click += new System.EventHandler(this.cancelledTasksStripButton_Click);
            // 
            // queuedTasksStripButton
            // 
            this.queuedTasksStripButton.Image = ((System.Drawing.Image)(resources.GetObject("queuedTasksStripButton.Image")));
            this.queuedTasksStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.queuedTasksStripButton.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.queuedTasksStripButton.Name = "queuedTasksStripButton";
            this.queuedTasksStripButton.Size = new System.Drawing.Size(69, 20);
            this.queuedTasksStripButton.Text = "Queued";
            this.queuedTasksStripButton.ToolTipText = "Queued Tasks";
            this.queuedTasksStripButton.Click += new System.EventHandler(this.queuedTasksStripButton_Click);
            // 
            // logTabPage
            // 
            this.logTabPage.Controls.Add(this.logListView);
            this.logTabPage.Location = new System.Drawing.Point(4, 23);
            this.logTabPage.Name = "logTabPage";
            this.logTabPage.Padding = new System.Windows.Forms.Padding(0, 2, 2, 1);
            this.logTabPage.Size = new System.Drawing.Size(987, 262);
            this.logTabPage.TabIndex = 2;
            this.logTabPage.Text = "Log";
            this.logTabPage.UseVisualStyleBackColor = true;
            // 
            // logListView
            // 
            this.logListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.logDataTimeColumnHeader,
            this.logMessageColumnHeader});
            this.logListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logListView.FullRowSelect = true;
            this.logListView.Location = new System.Drawing.Point(0, 2);
            this.logListView.MultiSelect = false;
            this.logListView.Name = "logListView";
            this.logListView.Size = new System.Drawing.Size(985, 259);
            this.logListView.SmallImageList = this.smallImageList;
            this.logListView.TabIndex = 2;
            this.logListView.UseCompatibleStateImageBehavior = false;
            this.logListView.View = System.Windows.Forms.View.Details;
            this.logListView.SelectedIndexChanged += new System.EventHandler(this.logListView_SelectedIndexChanged);
            this.logListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.logListView_MouseDown);
            // 
            // logDataTimeColumnHeader
            // 
            this.logDataTimeColumnHeader.Text = "Time";
            this.logDataTimeColumnHeader.Width = 140;
            // 
            // logMessageColumnHeader
            // 
            this.logMessageColumnHeader.Text = "Message";
            this.logMessageColumnHeader.Width = 600;
            // 
            // mainTool
            // 
            this.mainTool.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAccountStripButton,
            this.uploadToolStripDropDownButton,
            this.newFolderToolStripButton,
            this.refreshFilesToolStripButton,
            this.aboutStripButton,
            this.helpToolStripButton});
            this.mainTool.Location = new System.Drawing.Point(0, 0);
            this.mainTool.Name = "mainTool";
            this.mainTool.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.mainTool.Size = new System.Drawing.Size(997, 25);
            this.mainTool.TabIndex = 2;
            this.mainTool.Text = "toolStrip2";
            // 
            // newAccountStripButton
            // 
            this.newAccountStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newAccountStripButton.Image")));
            this.newAccountStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newAccountStripButton.Name = "newAccountStripButton";
            this.newAccountStripButton.Size = new System.Drawing.Size(125, 22);
            this.newAccountStripButton.Text = "New connection";
            this.newAccountStripButton.Click += new System.EventHandler(this.addNewAccountToolStripMenuItem_Click);
            // 
            // uploadToolStripDropDownButton
            // 
            this.uploadToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadFolderToolStripMenuItem,
            this.uploadFileToolStripMenuItem});
            this.uploadToolStripDropDownButton.Enabled = false;
            this.uploadToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("uploadToolStripDropDownButton.Image")));
            this.uploadToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uploadToolStripDropDownButton.Margin = new System.Windows.Forms.Padding(130, 1, 0, 2);
            this.uploadToolStripDropDownButton.Name = "uploadToolStripDropDownButton";
            this.uploadToolStripDropDownButton.Size = new System.Drawing.Size(78, 22);
            this.uploadToolStripDropDownButton.Text = "Upload";
            // 
            // uploadFolderToolStripMenuItem
            // 
            this.uploadFolderToolStripMenuItem.Name = "uploadFolderToolStripMenuItem";
            this.uploadFolderToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.uploadFolderToolStripMenuItem.Text = "Upload folder";
            this.uploadFolderToolStripMenuItem.Click += new System.EventHandler(this.uploadFolderToolStripMenuItem_Click);
            // 
            // uploadFileToolStripMenuItem
            // 
            this.uploadFileToolStripMenuItem.Name = "uploadFileToolStripMenuItem";
            this.uploadFileToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.uploadFileToolStripMenuItem.Text = "Upload files";
            this.uploadFileToolStripMenuItem.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // newFolderToolStripButton
            // 
            this.newFolderToolStripButton.Enabled = false;
            this.newFolderToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newFolderToolStripButton.Image")));
            this.newFolderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newFolderToolStripButton.Name = "newFolderToolStripButton";
            this.newFolderToolStripButton.Size = new System.Drawing.Size(97, 22);
            this.newFolderToolStripButton.Text = "New folder";
            this.newFolderToolStripButton.Click += new System.EventHandler(this.newFolderToolStripButton_Click);
            // 
            // refreshFilesToolStripButton
            // 
            this.refreshFilesToolStripButton.Enabled = false;
            this.refreshFilesToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshFilesToolStripButton.Image")));
            this.refreshFilesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshFilesToolStripButton.Name = "refreshFilesToolStripButton";
            this.refreshFilesToolStripButton.Size = new System.Drawing.Size(118, 22);
            this.refreshFilesToolStripButton.Text = "Refresh files";
            this.refreshFilesToolStripButton.Click += new System.EventHandler(this.refreshFilesToolStripButton_Click);
            // 
            // aboutStripButton
            // 
            this.aboutStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.aboutStripButton.Image = ((System.Drawing.Image)(resources.GetObject("aboutStripButton.Image")));
            this.aboutStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutStripButton.Name = "aboutStripButton";
            this.aboutStripButton.Size = new System.Drawing.Size(46, 22);
            this.aboutStripButton.Text = "About";
            this.aboutStripButton.Click += new System.EventHandler(this.aboutStripButton_Click);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(88, 22);
            this.helpToolStripButton.Text = "Online Help";
            this.helpToolStripButton.ToolTipText = "Help";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // accountMenu
            // 
            this.accountMenu.Font = new System.Drawing.Font("Courier New", 8.25F);
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
            this.clearAuthToolStripMenuItem});
            this.accountMenu.Name = "accountContextMenuStrip";
            this.accountMenu.Size = new System.Drawing.Size(222, 270);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(218, 6);
            // 
            // changeAccountToolStripMenuItem
            // 
            this.changeAccountToolStripMenuItem.Name = "changeAccountToolStripMenuItem";
            this.changeAccountToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.changeAccountToolStripMenuItem.Text = "Change Connection";
            this.changeAccountToolStripMenuItem.Click += new System.EventHandler(this.changeAccountToolStripMenuItem_Click);
            // 
            // deleteAccountToolStripMenuItem
            // 
            this.deleteAccountToolStripMenuItem.Name = "deleteAccountToolStripMenuItem";
            this.deleteAccountToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.deleteAccountToolStripMenuItem.Text = "Delete Connection";
            this.deleteAccountToolStripMenuItem.Click += new System.EventHandler(this.deleteAccountToolStripMenuItem_Click);
            // 
            // cloneAccountToolStripMenuItem
            // 
            this.cloneAccountToolStripMenuItem.Name = "cloneAccountToolStripMenuItem";
            this.cloneAccountToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.cloneAccountToolStripMenuItem.Text = "Clone Connection";
            this.cloneAccountToolStripMenuItem.Click += new System.EventHandler(this.cloneAccountToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(218, 6);
            // 
            // newFolderToolStripMenuItem2
            // 
            this.newFolderToolStripMenuItem2.Name = "newFolderToolStripMenuItem2";
            this.newFolderToolStripMenuItem2.Size = new System.Drawing.Size(221, 22);
            this.newFolderToolStripMenuItem2.Text = "New folder";
            this.newFolderToolStripMenuItem2.Click += new System.EventHandler(this.newFolderToolStripButton_Click);
            // 
            // uploadFolderToolStripMenuItem3
            // 
            this.uploadFolderToolStripMenuItem3.Name = "uploadFolderToolStripMenuItem3";
            this.uploadFolderToolStripMenuItem3.Size = new System.Drawing.Size(221, 22);
            this.uploadFolderToolStripMenuItem3.Text = "Upload folder";
            this.uploadFolderToolStripMenuItem3.Click += new System.EventHandler(this.uploadFolderToolStripMenuItem_Click);
            // 
            // uploadFilesToolStripMenuItem2
            // 
            this.uploadFilesToolStripMenuItem2.Name = "uploadFilesToolStripMenuItem2";
            this.uploadFilesToolStripMenuItem2.Size = new System.Drawing.Size(221, 22);
            this.uploadFilesToolStripMenuItem2.Text = "Upload files";
            this.uploadFilesToolStripMenuItem2.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(218, 6);
            // 
            // downloadFromDriveToolStripMenuItem
            // 
            this.downloadFromDriveToolStripMenuItem.Name = "downloadFromDriveToolStripMenuItem";
            this.downloadFromDriveToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.downloadFromDriveToolStripMenuItem.Text = "Download from Drive";
            this.downloadFromDriveToolStripMenuItem.Click += new System.EventHandler(this.downloadFromDriveToolStripMenuItem_Click);
            // 
            // downloadFromArchiveToolStripMenuItem
            // 
            this.downloadFromArchiveToolStripMenuItem.Name = "downloadFromArchiveToolStripMenuItem";
            this.downloadFromArchiveToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.downloadFromArchiveToolStripMenuItem.Text = "Download from Archive";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(218, 6);
            // 
            // clearAuthToolStripMenuItem
            // 
            this.clearAuthToolStripMenuItem.Name = "clearAuthToolStripMenuItem";
            this.clearAuthToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.clearAuthToolStripMenuItem.Text = "Clear Auth";
            this.clearAuthToolStripMenuItem.Click += new System.EventHandler(this.clearAuthToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // loadingFileListProgressBar
            // 
            this.loadingFileListProgressBar.Location = new System.Drawing.Point(297, 373);
            this.loadingFileListProgressBar.Name = "loadingFileListProgressBar";
            this.loadingFileListProgressBar.Size = new System.Drawing.Size(399, 21);
            this.loadingFileListProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.loadingFileListProgressBar.TabIndex = 1;
            this.loadingFileListProgressBar.Value = 10;
            this.loadingFileListProgressBar.Visible = false;
            // 
            // loadingImageProgressBar
            // 
            this.loadingImageProgressBar.Location = new System.Drawing.Point(702, 373);
            this.loadingImageProgressBar.Name = "loadingImageProgressBar";
            this.loadingImageProgressBar.Size = new System.Drawing.Size(232, 21);
            this.loadingImageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.loadingImageProgressBar.TabIndex = 3;
            this.loadingImageProgressBar.Value = 10;
            this.loadingImageProgressBar.Visible = false;
            // 
            // fileMenu
            // 
            this.fileMenu.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.fileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadFileFromStorageToolStripMenuItem,
            this.downloadFileFromDriveToolStripMenuItem,
            this.toolStripSeparator7,
            this.synchronizeToolStripMenuItem,
            this.synchronizeOnDrivesToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteFileToolStripMenuItem,
            this.deleteFromArchiveToolStripMenuItem,
            this.toolStripSeparator8,
            this.openFileToolStripMenuItem,
            this.openContainingFolderToolStripMenuItem,
            this.toolStripSeparator9,
            this.selectAllToolStripMenuItem});
            this.fileMenu.Name = "fileContextMenuStrip";
            this.fileMenu.Size = new System.Drawing.Size(229, 226);
            // 
            // downloadFileFromStorageToolStripMenuItem
            // 
            this.downloadFileFromStorageToolStripMenuItem.Name = "downloadFileFromStorageToolStripMenuItem";
            this.downloadFileFromStorageToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.downloadFileFromStorageToolStripMenuItem.Text = "Download from Ar&chive";
            this.downloadFileFromStorageToolStripMenuItem.Click += new System.EventHandler(this.downloadFileFromStorageToolStripMenuItem_Click);
            // 
            // downloadFileFromDriveToolStripMenuItem
            // 
            this.downloadFileFromDriveToolStripMenuItem.Name = "downloadFileFromDriveToolStripMenuItem";
            this.downloadFileFromDriveToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.downloadFileFromDriveToolStripMenuItem.Text = "Download from D&rive";
            this.downloadFileFromDriveToolStripMenuItem.Click += new System.EventHandler(this.downloadFileFromDriveToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(225, 6);
            // 
            // synchronizeToolStripMenuItem
            // 
            this.synchronizeToolStripMenuItem.Name = "synchronizeToolStripMenuItem";
            this.synchronizeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.synchronizeToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.synchronizeToolStripMenuItem.Text = "&Sync Archive";
            this.synchronizeToolStripMenuItem.Click += new System.EventHandler(this.synchronizeToolStripMenuItem_Click);
            // 
            // synchronizeOnDrivesToolStripMenuItem
            // 
            this.synchronizeOnDrivesToolStripMenuItem.Name = "synchronizeOnDrivesToolStripMenuItem";
            this.synchronizeOnDrivesToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.synchronizeOnDrivesToolStripMenuItem.Text = "Sync Drives";
            this.synchronizeOnDrivesToolStripMenuItem.Click += new System.EventHandler(this.synchronizeOnDrivesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(225, 6);
            // 
            // deleteFileToolStripMenuItem
            // 
            this.deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            this.deleteFileToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.deleteFileToolStripMenuItem.Text = "&Delete";
            this.deleteFileToolStripMenuItem.Click += new System.EventHandler(this.deleteFileToolStripMenuItem_Click);
            // 
            // deleteFromArchiveToolStripMenuItem
            // 
            this.deleteFromArchiveToolStripMenuItem.Name = "deleteFromArchiveToolStripMenuItem";
            this.deleteFromArchiveToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.deleteFromArchiveToolStripMenuItem.Text = "Delete from Archive";
            this.deleteFromArchiveToolStripMenuItem.Click += new System.EventHandler(this.deleteFromArchiveToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(225, 6);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // openContainingFolderToolStripMenuItem
            // 
            this.openContainingFolderToolStripMenuItem.Name = "openContainingFolderToolStripMenuItem";
            this.openContainingFolderToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.openContainingFolderToolStripMenuItem.Text = "Open Containing Folder";
            this.openContainingFolderToolStripMenuItem.Click += new System.EventHandler(this.openContainingFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(225, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.selectAllToolStripMenuItem.Text = "Select &all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // folderMenu
            // 
            this.folderMenu.Font = new System.Drawing.Font("Courier New", 8.25F);
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
            this.folderMenu.Size = new System.Drawing.Size(222, 148);
            // 
            // downloadFolderFromDriveToolStripMenuItem
            // 
            this.downloadFolderFromDriveToolStripMenuItem.Name = "downloadFolderFromDriveToolStripMenuItem";
            this.downloadFolderFromDriveToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.downloadFolderFromDriveToolStripMenuItem.Text = "Download from Drive";
            this.downloadFolderFromDriveToolStripMenuItem.Click += new System.EventHandler(this.downloadFolderFromDriveToolStripMenuItem_Click);
            // 
            // downloadFolderFromStorageToolStripMenuItem
            // 
            this.downloadFolderFromStorageToolStripMenuItem.Name = "downloadFolderFromStorageToolStripMenuItem";
            this.downloadFolderFromStorageToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.downloadFolderFromStorageToolStripMenuItem.Text = "Download from Archive";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(218, 6);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.newFolderToolStripMenuItem.Text = "New folder";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripButton_Click);
            // 
            // uploadFolderToolStripMenuItem2
            // 
            this.uploadFolderToolStripMenuItem2.Name = "uploadFolderToolStripMenuItem2";
            this.uploadFolderToolStripMenuItem2.Size = new System.Drawing.Size(221, 22);
            this.uploadFolderToolStripMenuItem2.Text = "Upload folder";
            this.uploadFolderToolStripMenuItem2.Click += new System.EventHandler(this.uploadFolderToolStripMenuItem_Click);
            // 
            // uploadFilesToolStripMenuItem1
            // 
            this.uploadFilesToolStripMenuItem1.Name = "uploadFilesToolStripMenuItem1";
            this.uploadFilesToolStripMenuItem1.Size = new System.Drawing.Size(221, 22);
            this.uploadFilesToolStripMenuItem1.Text = "Upload files";
            this.uploadFilesToolStripMenuItem1.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(218, 6);
            // 
            // deleteFolderToolStripMenuItem
            // 
            this.deleteFolderToolStripMenuItem.Name = "deleteFolderToolStripMenuItem";
            this.deleteFolderToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.deleteFolderToolStripMenuItem.Text = "Delete";
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
            this.logMenu.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.logMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDescriptionToolStripMenuItem});
            this.logMenu.Name = "logMenu";
            this.logMenu.Size = new System.Drawing.Size(124, 26);
            // 
            // showDescriptionToolStripMenuItem
            // 
            this.showDescriptionToolStripMenuItem.Name = "showDescriptionToolStripMenuItem";
            this.showDescriptionToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.showDescriptionToolStripMenuItem.Text = "Details";
            this.showDescriptionToolStripMenuItem.Click += new System.EventHandler(this.showDescriptionToolStripMenuItem_Click);
            // 
            // indicateErrorTimer
            // 
            this.indicateErrorTimer.Interval = 150;
            this.indicateErrorTimer.Tick += new System.EventHandler(this.indicateErrorTimer_Tick);
            // 
            // fileListMenu
            // 
            this.fileListMenu.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderToolStripMenuItem1,
            this.uploadFolderToolStripMenuItem1,
            this.uploadFilesToolStripMenuItem});
            this.fileListMenu.Name = "fileListMenu";
            this.fileListMenu.Size = new System.Drawing.Size(166, 70);
            // 
            // newFolderToolStripMenuItem1
            // 
            this.newFolderToolStripMenuItem1.Name = "newFolderToolStripMenuItem1";
            this.newFolderToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.newFolderToolStripMenuItem1.Text = "New folder";
            this.newFolderToolStripMenuItem1.Click += new System.EventHandler(this.newFolderToolStripButton_Click);
            // 
            // uploadFolderToolStripMenuItem1
            // 
            this.uploadFolderToolStripMenuItem1.Name = "uploadFolderToolStripMenuItem1";
            this.uploadFolderToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.uploadFolderToolStripMenuItem1.Text = "Upload folder";
            this.uploadFolderToolStripMenuItem1.Click += new System.EventHandler(this.uploadFolderToolStripMenuItem_Click);
            // 
            // uploadFilesToolStripMenuItem
            // 
            this.uploadFilesToolStripMenuItem.Name = "uploadFilesToolStripMenuItem";
            this.uploadFilesToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.uploadFilesToolStripMenuItem.Text = "Upload files";
            this.uploadFilesToolStripMenuItem.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // driveStrip1
            // 
            this.driveStrip1.Location = new System.Drawing.Point(702, 360);
            this.driveStrip1.Name = "driveStrip1";
            this.driveStrip1.Size = new System.Drawing.Size(209, 21);
            this.driveStrip1.TabIndex = 8;
            this.driveStrip1.Visible = false;
            this.driveStrip1.SelectedDriveChanged += new System.EventHandler(this.driveStrip1_SelectedDriveChanged);
            // 
            // currentDirectoryInfoPanel
            // 
            this.currentDirectoryInfoPanel.Location = new System.Drawing.Point(297, 21);
            this.currentDirectoryInfoPanel.Name = "currentDirectoryInfoPanel";
            this.currentDirectoryInfoPanel.NumberOfFiles = 0;
            this.currentDirectoryInfoPanel.NumberOfUnsyncronizedFiles = 0;
            this.currentDirectoryInfoPanel.Size = new System.Drawing.Size(548, 22);
            this.currentDirectoryInfoPanel.TabIndex = 7;
            this.currentDirectoryInfoPanel.FilterChanged += new System.EventHandler<System.EventArgs>(this.currentDirectoryInfoPanel_FilterChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 591);
            this.Controls.Add(this.loadingImageProgressBar);
            this.Controls.Add(this.driveStrip1);
            this.Controls.Add(this.loadingFileListProgressBar);
            this.Controls.Add(this.currentDirectoryInfoPanel);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainTool);
            this.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Oblqo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.fileInfoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tasksTabPage.ResumeLayout(false);
            this.tasksTabPage.PerformLayout();
            this.taskMenu.ResumeLayout(false);
            this.tasksToolStrip.ResumeLayout(false);
            this.tasksToolStrip.PerformLayout();
            this.logTabPage.ResumeLayout(false);
            this.mainTool.ResumeLayout(false);
            this.mainTool.PerformLayout();
            this.accountMenu.ResumeLayout(false);
            this.fileMenu.ResumeLayout(false);
            this.folderMenu.ResumeLayout(false);
            this.logMenu.ResumeLayout(false);
            this.fileListMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tasksTabPage;
        private System.Windows.Forms.ListView taskListView;
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
        private System.Windows.Forms.ContextMenuStrip fileMenu;
        private System.Windows.Forms.ContextMenuStrip folderMenu;
        private System.Windows.Forms.ToolStripMenuItem downloadFileFromDriveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadFolderFromDriveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFolderToolStripMenuItem;
        private System.Windows.Forms.TabPage logTabPage;
        private System.Windows.Forms.ListView logListView;
        private System.Windows.Forms.ToolStripMenuItem downloadFileFromStorageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadFolderFromStorageToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader logDataTimeColumnHeader;
        private System.Windows.Forms.ColumnHeader logMessageColumnHeader;
        private System.Windows.Forms.Timer loadingFoldersTimer;
        private System.Windows.Forms.ToolStripMenuItem downloadFromDriveToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip taskMenu;
        private System.Windows.Forms.ToolStripMenuItem cancelTaskToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip logMenu;
        private System.Windows.Forms.ToolStripMenuItem showDescriptionToolStripMenuItem;
        private System.Windows.Forms.Timer indicateErrorTimer;
        private System.Windows.Forms.ColumnHeader taskTypeColumnHeader;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem uploadFolderToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem uploadFilesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip fileListMenu;
        private System.Windows.Forms.ToolStripMenuItem uploadFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem uploadFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem uploadFolderToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem uploadFilesToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem synchronizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadFromArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private FileListStatusBar currentDirectoryInfoPanel;
        private System.Windows.Forms.ToolStripButton newAccountStripButton;
        private System.Windows.Forms.ToolStrip tasksToolStrip;
        private System.Windows.Forms.ToolStripButton activeTasksStripButton;
        private System.Windows.Forms.ToolStripButton queuedTasksStripButton;
        private System.Windows.Forms.ToolStripButton finishedTasksStripButton;
        private System.Windows.Forms.ToolStripButton cancelledTasksStripButton;
        private System.Windows.Forms.ToolStripMenuItem synchronizeOnDrivesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem taskDetailsToolStripMenuItem;
        private Controls.DriveFileControl fileInfoPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Controls.DriveStrip driveStrip1;
        private System.Windows.Forms.ToolStripButton aboutStripButton;
        private System.Windows.Forms.Button btnNewConnection;
        private System.Windows.Forms.ToolStripMenuItem cloneAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem deleteFromArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openContainingFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem clearAuthToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    }
}

