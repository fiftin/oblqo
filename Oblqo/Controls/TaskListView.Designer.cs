namespace Oblqo.Controls
{
    partial class TaskListView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskListView));
            this.listView1 = new System.Windows.Forms.ListView();
            this.taskColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.taskTypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PercentColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tasksToolStrip = new System.Windows.Forms.ToolStrip();
            this.activeTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.finishedTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.cancelledTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.queuedTasksStripButton = new System.Windows.Forms.ToolStripButton();
            this.tasksToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.taskColumnHeader,
            this.taskTypeColumnHeader,
            this.sizeColumnHeader,
            this.PercentColumnHeader});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(581, 333);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
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
            // 
            // finishedTasksStripButton
            // 
            this.finishedTasksStripButton.Image = ((System.Drawing.Image)(resources.GetObject("finishedTasksStripButton.Image")));
            this.finishedTasksStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.finishedTasksStripButton.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.finishedTasksStripButton.Name = "finishedTasksStripButton";
            this.finishedTasksStripButton.Size = new System.Drawing.Size(83, 20);
            this.finishedTasksStripButton.Text = "Finished";
            // 
            // cancelledTasksStripButton
            // 
            this.cancelledTasksStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cancelledTasksStripButton.Image")));
            this.cancelledTasksStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelledTasksStripButton.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.cancelledTasksStripButton.Name = "cancelledTasksStripButton";
            this.cancelledTasksStripButton.Size = new System.Drawing.Size(90, 20);
            this.cancelledTasksStripButton.Text = "Cancelled";
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
            // 
            // TaskListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.tasksToolStrip);
            this.Name = "TaskListView";
            this.Size = new System.Drawing.Size(581, 358);
            this.tasksToolStrip.ResumeLayout(false);
            this.tasksToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader taskColumnHeader;
        private System.Windows.Forms.ColumnHeader taskTypeColumnHeader;
        private System.Windows.Forms.ColumnHeader sizeColumnHeader;
        private System.Windows.Forms.ColumnHeader PercentColumnHeader;
        private System.Windows.Forms.ToolStrip tasksToolStrip;
        private System.Windows.Forms.ToolStripButton activeTasksStripButton;
        private System.Windows.Forms.ToolStripButton finishedTasksStripButton;
        private System.Windows.Forms.ToolStripButton cancelledTasksStripButton;
        private System.Windows.Forms.ToolStripButton queuedTasksStripButton;
    }
}
