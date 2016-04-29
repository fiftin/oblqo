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
            resources.ApplyResources(this.tasksToolStrip, "tasksToolStrip");
            this.tasksToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tasksToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeTasksStripButton,
            this.finishedTasksStripButton,
            this.cancelledTasksStripButton,
            this.queuedTasksStripButton});
            this.tasksToolStrip.Name = "tasksToolStrip";
            // 
            // activeTasksStripButton
            // 
            resources.ApplyResources(this.activeTasksStripButton, "activeTasksStripButton");
            this.activeTasksStripButton.Checked = true;
            this.activeTasksStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activeTasksStripButton.Name = "activeTasksStripButton";
            this.activeTasksStripButton.Click += new System.EventHandler(this.activeTasksStripButton_Click);
            // 
            // finishedTasksStripButton
            // 
            resources.ApplyResources(this.finishedTasksStripButton, "finishedTasksStripButton");
            this.finishedTasksStripButton.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.finishedTasksStripButton.Name = "finishedTasksStripButton";
            this.finishedTasksStripButton.Click += new System.EventHandler(this.finishedTasksStripButton_Click);
            // 
            // cancelledTasksStripButton
            // 
            resources.ApplyResources(this.cancelledTasksStripButton, "cancelledTasksStripButton");
            this.cancelledTasksStripButton.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.cancelledTasksStripButton.Name = "cancelledTasksStripButton";
            this.cancelledTasksStripButton.Click += new System.EventHandler(this.cancelledTasksStripButton_Click);
            // 
            // queuedTasksStripButton
            // 
            resources.ApplyResources(this.queuedTasksStripButton, "queuedTasksStripButton");
            this.queuedTasksStripButton.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.queuedTasksStripButton.Name = "queuedTasksStripButton";
            this.queuedTasksStripButton.Click += new System.EventHandler(this.queuedTasksStripButton_Click);
            // 
            // taskListView
            // 
            resources.ApplyResources(this.taskListView, "taskListView");
            this.taskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.taskColumnHeader,
            this.taskTypeColumnHeader,
            this.sizeColumnHeader,
            this.PercentColumnHeader});
            this.taskListView.ContextMenuStrip = this.taskMenu;
            this.taskListView.FullRowSelect = true;
            this.taskListView.HideSelection = false;
            this.taskListView.Name = "taskListView";
            this.taskListView.UseCompatibleStateImageBehavior = false;
            this.taskListView.View = System.Windows.Forms.View.Details;
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
            // taskMenu
            // 
            resources.ApplyResources(this.taskMenu, "taskMenu");
            this.taskMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelTaskToolStripMenuItem,
            this.taskDetailsToolStripMenuItem});
            this.taskMenu.Name = "taskMenu";
            // 
            // cancelTaskToolStripMenuItem
            // 
            resources.ApplyResources(this.cancelTaskToolStripMenuItem, "cancelTaskToolStripMenuItem");
            this.cancelTaskToolStripMenuItem.Name = "cancelTaskToolStripMenuItem";
            this.cancelTaskToolStripMenuItem.Click += new System.EventHandler(this.cancelTaskToolStripMenuItem_Click);
            // 
            // taskDetailsToolStripMenuItem
            // 
            resources.ApplyResources(this.taskDetailsToolStripMenuItem, "taskDetailsToolStripMenuItem");
            this.taskDetailsToolStripMenuItem.Name = "taskDetailsToolStripMenuItem";
            this.taskDetailsToolStripMenuItem.Click += new System.EventHandler(this.taskDetailsToolStripMenuItem_Click_1);
            // 
            // TaskList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.taskListView);
            this.Controls.Add(this.tasksToolStrip);
            this.Name = "TaskList";
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
