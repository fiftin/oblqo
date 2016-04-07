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
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fileListMenu.SuspendLayout();
            this.fileMenu.SuspendLayout();
            this.SuspendLayout();
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
            this.fileListView.Size = new System.Drawing.Size(572, 419);
            this.fileListView.TabIndex = 2;
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
            // 
            // uploadFolderToolStripMenuItem1
            // 
            this.uploadFolderToolStripMenuItem1.Name = "uploadFolderToolStripMenuItem1";
            this.uploadFolderToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.uploadFolderToolStripMenuItem1.Text = "Upload folder";
            // 
            // uploadFilesToolStripMenuItem
            // 
            this.uploadFilesToolStripMenuItem.Name = "uploadFilesToolStripMenuItem";
            this.uploadFilesToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.uploadFilesToolStripMenuItem.Text = "Upload files";
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
            this.deleteFromArchiveToolStripMenuItem,
            this.deleteFileToolStripMenuItem,
            this.toolStripSeparator8,
            this.openFileToolStripMenuItem,
            this.openContainingFolderToolStripMenuItem,
            this.toolStripSeparator9,
            this.selectAllToolStripMenuItem});
            this.fileMenu.Name = "fileContextMenuStrip";
            this.fileMenu.Size = new System.Drawing.Size(271, 226);
            // 
            // downloadFileFromStorageToolStripMenuItem
            // 
            this.downloadFileFromStorageToolStripMenuItem.Name = "downloadFileFromStorageToolStripMenuItem";
            this.downloadFileFromStorageToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.downloadFileFromStorageToolStripMenuItem.Text = "Download from Ar&chive";
            this.downloadFileFromStorageToolStripMenuItem.Click += new System.EventHandler(this.downloadFileFromStorageToolStripMenuItem_Click);
            // 
            // downloadFileFromDriveToolStripMenuItem
            // 
            this.downloadFileFromDriveToolStripMenuItem.Name = "downloadFileFromDriveToolStripMenuItem";
            this.downloadFileFromDriveToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.downloadFileFromDriveToolStripMenuItem.Text = "Download from D&rive";
            this.downloadFileFromDriveToolStripMenuItem.Click += new System.EventHandler(this.downloadFileFromDriveToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(267, 6);
            // 
            // synchronizeToolStripMenuItem
            // 
            this.synchronizeToolStripMenuItem.Name = "synchronizeToolStripMenuItem";
            this.synchronizeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.synchronizeToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.synchronizeToolStripMenuItem.Text = "&Upload to Archive";
            this.synchronizeToolStripMenuItem.Click += new System.EventHandler(this.synchronizeToolStripMenuItem_Click);
            // 
            // synchronizeOnDrivesToolStripMenuItem
            // 
            this.synchronizeOnDrivesToolStripMenuItem.Name = "synchronizeOnDrivesToolStripMenuItem";
            this.synchronizeOnDrivesToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.synchronizeOnDrivesToolStripMenuItem.Text = "Sync with All Drives";
            this.synchronizeOnDrivesToolStripMenuItem.Click += new System.EventHandler(this.synchronizeOnDrivesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(267, 6);
            // 
            // deleteFromArchiveToolStripMenuItem
            // 
            this.deleteFromArchiveToolStripMenuItem.Name = "deleteFromArchiveToolStripMenuItem";
            this.deleteFromArchiveToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.deleteFromArchiveToolStripMenuItem.Text = "Delete from Archive";
            this.deleteFromArchiveToolStripMenuItem.Click += new System.EventHandler(this.deleteFromArchiveToolStripMenuItem_Click);
            // 
            // deleteFileToolStripMenuItem
            // 
            this.deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            this.deleteFileToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.deleteFileToolStripMenuItem.Text = "&Delete from Archive && Drives";
            this.deleteFileToolStripMenuItem.Click += new System.EventHandler(this.deleteFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(267, 6);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.openFileToolStripMenuItem.Text = "Open Local File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // openContainingFolderToolStripMenuItem
            // 
            this.openContainingFolderToolStripMenuItem.Name = "openContainingFolderToolStripMenuItem";
            this.openContainingFolderToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.openContainingFolderToolStripMenuItem.Text = "Open Containing Folder";
            this.openContainingFolderToolStripMenuItem.Click += new System.EventHandler(this.openContainingFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(267, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.selectAllToolStripMenuItem.Text = "Select &all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            // 
            // FileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileListView);
            this.Name = "FileList";
            this.Size = new System.Drawing.Size(572, 419);
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
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
