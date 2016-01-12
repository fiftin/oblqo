namespace Oblqo.Controls
{
    partial class FolderTreeView
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
            this.fileListView = new System.Windows.Forms.ListView();
            this.fileNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileDateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileSizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.fileListView.Size = new System.Drawing.Size(502, 343);
            this.fileListView.TabIndex = 2;
            this.fileListView.UseCompatibleStateImageBehavior = false;
            this.fileListView.View = System.Windows.Forms.View.Details;
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
            // FolderTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileListView);
            this.Name = "FolderTreeView";
            this.Size = new System.Drawing.Size(502, 343);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.ColumnHeader fileNameColumnHeader;
        private System.Windows.Forms.ColumnHeader fileDateColumnHeader;
        private System.Windows.Forms.ColumnHeader fileSizeColumnHeader;
    }
}
