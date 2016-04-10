namespace Oblqo.Controls
{
    partial class TaskList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskList));
            this.tasksToolStrip = new System.Windows.Forms.ToolStrip();
            this.activeTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.finishedTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.cancelledTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.queuedTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.taskListView = new System.Windows.Forms.ListView();
            this.taskColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.taskTypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PercentColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.taskMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancelTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tasksToolStrip.SuspendLayout();
            this.taskMenu.SuspendLayout();
            this.SuspendLayout();
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
            this.tasksToolStrip.Location = new System.Drawing.Point(0, 333);
            this.tasksToolStrip.Name = "tasksToolStrip";
            this.tasksToolStrip.Padding = new System.Windows.Forms.Padding(0, 2, 1, 0);
            this.tasksToolStrip.Size = new System.Drawing.Size(581, 25);
            this.tasksToolStrip.TabIndex = 3;
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
            // taskListView
            // 
            this.taskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.taskColumnHeader,
            this.taskTypeColumnHeader,
            this.sizeColumnHeader,
            this.PercentColumnHeader});
            this.taskListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskListView.FullRowSelect = true;
            this.taskListView.HideSelection = false;
            this.taskListView.Location = new System.Drawing.Point(0, 0);
            this.taskListView.Name = "taskListView";
            this.taskListView.Size = new System.Drawing.Size(581, 333);
            this.taskListView.TabIndex = 4;
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
            this.taskMenu.Size = new System.Drawing.Size(153, 70);
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
            this.taskDetailsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.taskDetailsToolStripMenuItem.Text = "Details";
            this.taskDetailsToolStripMenuItem.Click += new System.EventHandler(this.taskDetailsToolStripMenuItem_Click_1);
            // 
            // TaskList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.taskListView);
            this.Controls.Add(this.tasksToolStrip);
            this.Name = "TaskList";
            this.Size = new System.Drawing.Size(581, 358);
            this.tasksToolStrip.ResumeLayout(false);
            this.tasksToolStrip.PerformLayout();
            this.taskMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tasksToolStrip;
        private System.Windows.Forms.ToolStripButton activeTasksStripButton;
        private System.Windows.Forms.ToolStripButton finishedTasksStripButton;
        private System.Windows.Forms.ToolStripButton cancelledTasksStripButton;
        private System.Windows.Forms.ToolStripButton queuedTasksStripButton;
        private System.Windows.Forms.ListView taskListView;
        private System.Windows.Forms.ColumnHeader taskColumnHeader;
        private System.Windows.Forms.ColumnHeader taskTypeColumnHeader;
        private System.Windows.Forms.ColumnHeader sizeColumnHeader;
        private System.Windows.Forms.ColumnHeader PercentColumnHeader;
        private System.Windows.Forms.ContextMenuStrip taskMenu;
        private System.Windows.Forms.ToolStripMenuItem cancelTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taskDetailsToolStripMenuItem;
    }
}
