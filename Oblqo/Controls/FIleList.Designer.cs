using System;
using System.Windows.Forms;

namespace Oblqo.Controls
{
    partial class FileList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileList));
            this.fileListView = new System.Windows.Forms.ListView();
            this.fileNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileDateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileSizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadFileFromStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadFileFromDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.synchronizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.synchronizeOnDrivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteFromArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openContainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItemSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fileListMenu.SuspendLayout();
            this.fileMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileListView
            // 
            resources.ApplyResources(this.fileListView, "fileListView");
            this.fileListView.AllowColumnReorder = true;
            this.fileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileNameColumnHeader,
            this.fileDateColumnHeader,
            this.fileSizeColumnHeader});
            this.fileListView.FullRowSelect = true;
            this.fileListView.Name = "fileListView";
            this.fileListView.UseCompatibleStateImageBehavior = false;
            this.fileListView.View = System.Windows.Forms.View.Details;
            this.fileListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.fileListView_ColumnClick);
            this.fileListView.SelectedIndexChanged += new System.EventHandler(this.fileListView_SelectedIndexChanged);
            this.fileListView.DoubleClick += new System.EventHandler(this.fileListView_DoubleClick);
            this.fileListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fileListView_KeyDown);
            this.fileListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fileListView_MouseUp);
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
            // fileListMenu
            // 
            resources.ApplyResources(this.fileListMenu, "fileListMenu");
            this.fileListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderToolStripMenuItem1,
            this.uploadFolderToolStripMenuItem1,
            this.uploadFilesToolStripMenuItem});
            this.fileListMenu.Name = "fileListMenu";
            // 
            // newFolderToolStripMenuItem1
            // 
            resources.ApplyResources(this.newFolderToolStripMenuItem1, "newFolderToolStripMenuItem1");
            this.newFolderToolStripMenuItem1.Name = "newFolderToolStripMenuItem1";
            this.newFolderToolStripMenuItem1.Click += new System.EventHandler(this.newFolderToolStripButton_Click);
            // 
            // uploadFolderToolStripMenuItem1
            // 
            resources.ApplyResources(this.uploadFolderToolStripMenuItem1, "uploadFolderToolStripMenuItem1");
            this.uploadFolderToolStripMenuItem1.Name = "uploadFolderToolStripMenuItem1";
            this.uploadFolderToolStripMenuItem1.Click += new System.EventHandler(this.uploadFolderToolStripMenuItem_Click);
            // 
            // uploadFilesToolStripMenuItem
            // 
            resources.ApplyResources(this.uploadFilesToolStripMenuItem, "uploadFilesToolStripMenuItem");
            this.uploadFilesToolStripMenuItem.Name = "uploadFilesToolStripMenuItem";
            this.uploadFilesToolStripMenuItem.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // fileMenu
            // 
            resources.ApplyResources(this.fileMenu, "fileMenu");
            this.fileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadFileFromStorageToolStripMenuItem,
            this.downloadFileFromDriveToolStripMenuItem,
            this.toolStripSeparator7,
            this.synchronizeToolStripMenuItem,
            this.synchronizeOnDrivesToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteFromArchiveToolStripMenuItem,
            this.deleteFileToolStripMenuItem,
            this.toolStripSeparator8,
            this.openFileToolStripMenuItem,
            this.openContainingFolderToolStripMenuItem,
            this.selectAllToolStripMenuItemSeparator,
            this.selectAllToolStripMenuItem});
            this.fileMenu.Name = "fileContextMenuStrip";
            // 
            // downloadFileFromStorageToolStripMenuItem
            // 
            resources.ApplyResources(this.downloadFileFromStorageToolStripMenuItem, "downloadFileFromStorageToolStripMenuItem");
            this.downloadFileFromStorageToolStripMenuItem.Name = "downloadFileFromStorageToolStripMenuItem";
            this.downloadFileFromStorageToolStripMenuItem.Click += new System.EventHandler(this.downloadFileFromStorageToolStripMenuItem_Click);
            // 
            // downloadFileFromDriveToolStripMenuItem
            // 
            resources.ApplyResources(this.downloadFileFromDriveToolStripMenuItem, "downloadFileFromDriveToolStripMenuItem");
            this.downloadFileFromDriveToolStripMenuItem.Name = "downloadFileFromDriveToolStripMenuItem";
            this.downloadFileFromDriveToolStripMenuItem.Click += new System.EventHandler(this.downloadFileFromDriveToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            // 
            // synchronizeToolStripMenuItem
            // 
            resources.ApplyResources(this.synchronizeToolStripMenuItem, "synchronizeToolStripMenuItem");
            this.synchronizeToolStripMenuItem.Name = "synchronizeToolStripMenuItem";
            this.synchronizeToolStripMenuItem.Click += new System.EventHandler(this.synchronizeToolStripMenuItem_Click);
            // 
            // synchronizeOnDrivesToolStripMenuItem
            // 
            resources.ApplyResources(this.synchronizeOnDrivesToolStripMenuItem, "synchronizeOnDrivesToolStripMenuItem");
            this.synchronizeOnDrivesToolStripMenuItem.Name = "synchronizeOnDrivesToolStripMenuItem";
            this.synchronizeOnDrivesToolStripMenuItem.Click += new System.EventHandler(this.synchronizeOnDrivesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // deleteFromArchiveToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteFromArchiveToolStripMenuItem, "deleteFromArchiveToolStripMenuItem");
            this.deleteFromArchiveToolStripMenuItem.Name = "deleteFromArchiveToolStripMenuItem";
            this.deleteFromArchiveToolStripMenuItem.Click += new System.EventHandler(this.deleteFromArchiveToolStripMenuItem_Click);
            // 
            // deleteFileToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteFileToolStripMenuItem, "deleteFileToolStripMenuItem");
            this.deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            this.deleteFileToolStripMenuItem.Click += new System.EventHandler(this.deleteFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            // 
            // openFileToolStripMenuItem
            // 
            resources.ApplyResources(this.openFileToolStripMenuItem, "openFileToolStripMenuItem");
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // openContainingFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.openContainingFolderToolStripMenuItem, "openContainingFolderToolStripMenuItem");
            this.openContainingFolderToolStripMenuItem.Name = "openContainingFolderToolStripMenuItem";
            this.openContainingFolderToolStripMenuItem.Click += new System.EventHandler(this.openContainingFolderToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItemSeparator
            // 
            resources.ApplyResources(this.selectAllToolStripMenuItemSeparator, "selectAllToolStripMenuItemSeparator");
            this.selectAllToolStripMenuItemSeparator.Name = "selectAllToolStripMenuItemSeparator";
            // 
            // selectAllToolStripMenuItem
            // 
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // openFileDialog1
            // 
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            this.openFileDialog1.Multiselect = true;
            // 
            // FileList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileListView);
            this.Name = "FileList";
            this.fileListMenu.ResumeLayout(false);
            this.fileMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.ColumnHeader fileNameColumnHeader;
        private System.Windows.Forms.ColumnHeader fileDateColumnHeader;
        private System.Windows.Forms.ColumnHeader fileSizeColumnHeader;
        private System.Windows.Forms.ContextMenuStrip fileListMenu;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem uploadFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem uploadFilesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip fileMenu;
        private System.Windows.Forms.ToolStripMenuItem downloadFileFromStorageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadFileFromDriveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem synchronizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem synchronizeOnDrivesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFromArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openContainingFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator selectAllToolStripMenuItemSeparator;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;

        public void Clear()
        {
            fileListView.Items.Clear();
        }

        public void SelectAll()
        {
            foreach (ListViewItem item in fileListView.Items)
            {
                item.Selected = true;
            }
        }

        private FolderBrowserDialog folderBrowserDialog1;
        private OpenFileDialog openFileDialog1;
    }
}
