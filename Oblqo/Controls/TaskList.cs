using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oblqo.Tasks;
using System.IO;

namespace Oblqo.Controls
{
    public partial class TaskList : UserControl
    {
        private AsyncTaskManager taskManager;
        private AsyncTaskState[] displayingTaskListStates = new AsyncTaskState[] { AsyncTaskState.Running };

        public TaskList()
        {
            InitializeComponent();
        }


        public ImageList SmallImageList
        {
            get
            {
                return taskListView.SmallImageList;
            }
            set
            {
                taskListView.SmallImageList = value;
            }
        }

        [Browsable(false)]
        public ListView.SelectedListViewItemCollection SelectedItems
        {
            get
            {
                return taskListView.SelectedItems;
            }
        }

        [Browsable(false), DefaultValue(null)]
        public AsyncTaskManager TaskManager {
            get
            {
                return taskManager;
            }
            set
            {
                if (taskManager != null)
                {
                    taskManager.TaskAdded -= taskManager_TaskAdded;
                    taskManager.TaskProgress -= taskManager_TaskProgress;
                    taskManager.TaskRemoved -= taskManager_TaskRemoved;
                    taskManager.TaskStateChanged -= taskManager_TaskStateChanged;
                }
                taskManager = value;
                taskManager.TaskAdded += taskManager_TaskAdded;
                taskManager.TaskProgress += taskManager_TaskProgress;
                taskManager.TaskRemoved += taskManager_TaskRemoved;
                taskManager.TaskStateChanged += taskManager_TaskStateChanged;
            }
        }

        public void UpdateTaskList()
        {
            taskListView.Items.Clear();
            foreach (var task in TaskManager.ToArray().Where(task => displayingTaskListStates.Contains(task.State)))
            {
                AddTask(task);
            }
        }

        public ListViewItem AddTask(AsyncTask newTask)
        {
            if (!newTask.Visible)
            {
                return null;
            }

            var taskItem = new ListViewItem { Tag = newTask };
            if (newTask is UploadFolderTask)
            {
                var task = (UploadFolderTask)newTask;
                taskItem.Text = Common.GetFileOrDirectoryName(task.Path);
                taskItem.SubItems.Add("Upload Folder").Name = "type";
                taskItem.SubItems.Add("").Name = "size";
                taskItem.SubItems.Add("0").Name = "percent";
            }
            else if (newTask is DownloadFileFromStorageTask)
            {
                var task = (DownloadFileFromStorageTask)newTask;
                taskItem.Text = task.File.Name;
                taskItem.SubItems.Add("Download File").Name = "type";
                taskItem.SubItems.Add(Common.NumberOfBytesToString(task.File.OriginalSize)).Name = "size";
                taskItem.SubItems.Add("0").Name = "percent";
            }
            else if (newTask is DownloadFileFromDriveTask)
            {
                var task = (DownloadFileFromDriveTask)newTask;
                taskItem.Text = task.File.Name;
                taskItem.SubItems.Add("Download File").Name = "type";
                taskItem.SubItems.Add(Common.NumberOfBytesToString(task.File.OriginalSize)).Name = "size";
                taskItem.SubItems.Add("0").Name = "percent";
            }
            else if (newTask is UploadFileTask)
            {
                var task = (UploadFileTask)newTask;
                taskItem.Text = Path.GetFileName(task.FileName);
                var fileInfo = new FileInfo(task.FileName);
                taskItem.SubItems.Add("Upload File").Name = "type";
                taskItem.SubItems.Add(Common.NumberOfBytesToString(fileInfo.Length)).Name = "size";
                taskItem.SubItems.Add("0").Name = "percent";
            }
            else if (newTask is CreateFolderTask)
            {
                var task = (CreateFolderTask)newTask;
                taskItem.Text = Path.GetFileName(task.FolderName);
                taskItem.SubItems.Add("Create Folder").Name = "type";
                taskItem.SubItems.Add("").Name = "size";
                taskItem.SubItems.Add("0").Name = "percent";
            }
            else if (newTask is DeleteFolderTaskBase)
            {
                var task = (DeleteFolderTaskBase)newTask;
                taskItem.Text = Path.GetFileName(task.Folder.Name);
                if (task is DeleteEmptyFolderTask)
                    taskItem.SubItems.Add("Delete Empty Folder").Name = "type";
                else
                    taskItem.SubItems.Add("Delete Folder").Name = "type";
                taskItem.SubItems.Add("").Name = "size";
                taskItem.SubItems.Add("0").Name = "percent";
            }
            else if (newTask is DeleteFileTask)
            {
                var task = (DeleteFileTask)newTask;
                taskItem.Text = Path.GetFileName(task.File.Name);
                taskItem.SubItems.Add("Delete File").Name = "type";
                taskItem.SubItems.Add("").Name = "size";
                taskItem.SubItems.Add("0").Name = "percent";
            }
            else if (newTask is DownloadFolderTask)
            {
                var task = (DownloadFolderTask)newTask;
                taskItem.Text = Path.GetFileName(task.Folder.Name);
                taskItem.SubItems.Add("Download Folder").Name = "type";
                taskItem.SubItems.Add("").Name = "size";
                taskItem.SubItems.Add("0").Name = "percent";
            }
            else if (newTask is SynchronizeFileTask)
            {
                var task = (SynchronizeFileTask)newTask;
                taskItem.Text = Path.GetFileName(task.SourceFile.Name);
                taskItem.SubItems.Add("Sync File").Name = "type";
                taskItem.SubItems.Add(Common.NumberOfBytesToString(task.SourceFile.Size)).Name = "size";
                taskItem.SubItems.Add("0").Name = "percent";
            }
            else if (newTask is SynchronizeDriveFileTask)
            {
                var task = (SynchronizeDriveFileTask)newTask;
                taskItem.Text = Path.GetFileName(task.File?.Name);
                taskItem.SubItems.Add("Sync File on Drive").Name = "type";
                taskItem.SubItems.Add(Common.NumberOfBytesToString(0)).Name = "size";
                taskItem.SubItems.Add("0").Name = "percent";
            }

            switch (newTask.State)
            {
                case AsyncTaskState.Cancelled:
                    taskItem.ImageKey = "cancel";
                    break;
                case AsyncTaskState.Completed:
                    taskItem.ImageKey = "ok";
                    break;
                case AsyncTaskState.Error:
                    taskItem.ImageKey = "error_red";
                    break;
                case AsyncTaskState.Running:
                    taskItem.ImageKey = "run";
                    break;
                case AsyncTaskState.Waiting:
                    taskItem.ImageKey = "queued";
                    break;
            }
            return taskListView.Items.Add(taskItem);
        }

        private void taskManager_TaskStateChanged(object sender, AsyncTaskEventArgs e)
        {
            var items = taskListView.Items.Cast<ListViewItem>();

            Invoke(new MethodInvoker(() =>
            {
                var item = items.FirstOrDefault(x => x.Tag == e.Task);
                if (displayingTaskListStates.Contains(e.Task.State))
                {
                    if (item == null)
                    {
                        item = AddTask(e.Task);
                    }
                }
                else
                {
                    if (item != null)
                    {
                        item.Remove();
                        item = null;
                    }
                }

                if (e.Task.State == AsyncTaskState.Completed)
                {
                    var attrs = e.Task.GetType().GetCustomAttributes(typeof(AccountFileStateChangeAttribute), true);
                    if (attrs.Length != 0)
                    {
                        var attr = (AccountFileStateChangeAttribute)attrs[0];
                        var fileProp = e.Task.GetType().GetProperty(attr.FilePropertyName);
                        var file = (AccountFile)fileProp.GetValue(e.Task);
                        var parentFileProp = attr.ParentFilePropertyName == null ? null : e.Task.GetType().GetProperty(attr.ParentFilePropertyName);
                        var parentFile = parentFileProp == null ? null : (AccountFile)parentFileProp.GetValue(e.Task);
                    }
                }

                if (item == null)
                {
                    return;
                }

                switch (e.Task.State)
                {
                    case AsyncTaskState.Cancelled:
                        item.ImageKey = "cancel";
                        break;
                    case AsyncTaskState.Completed:
                        item.ImageKey = "ok";
                        break;
                    case AsyncTaskState.Error:
                        item.ImageKey = "error_red";
                        break;
                    case AsyncTaskState.Running:
                        item.ImageKey = "run";
                        break;
                    case AsyncTaskState.Waiting:
                        item.ImageKey = "queued";
                        break;
                }

                switch (e.Task.State)
                {
                    case AsyncTaskState.Cancelled:
                    case AsyncTaskState.Error:
                        item.SubItems["percent"].Text = "0";
                        break;
                    case AsyncTaskState.Completed:
                        item.SubItems["percent"].Text = "100";
                        break;
                }
            }));
        }

        private void taskManager_TaskProgress(object sender, AsyncTaskEventArgs<AsyncTaskProgressEventArgs> e)
        {
            try
            {
                var items = taskListView.Items.Cast<ListViewItem>();
                Invoke(new MethodInvoker(() =>
                {
                    var item = items.FirstOrDefault(x => x.Tag == e.Task);
                    if (item == null) return;
                    item.SubItems["percent"].Text = e.Args.PercentDone.ToString();
                }));
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        private void OnError(Exception ex)
        {
            Error?.Invoke(this, new ExceptionEventArgs(ex));
        }

        private void taskManager_TaskRemoved(object sender, AsyncTaskEventArgs e)
        {
            for (var i = 0; i < taskListView.Items.Count; i++)
            {
                if (taskListView.Items[i].Tag == e.Task)
                {
                    taskListView.Items.RemoveAt(i);
                    break;
                }
            }
        }

        private void taskManager_TaskAdded(object sender, AsyncTaskEventArgs e)
        {
            if (e.Task is DeleteEmptyFolderTask || e.Task is EmptyTask)
            {
                return;
            }

            if (!displayingTaskListStates.Contains(e.Task.State))
            {
                return;
            }

            Invoke(new MethodInvoker(() => AddTask(e.Task)));
        }


        private void cancelTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var task in from ListViewItem item in taskListView.SelectedItems select (AsyncTask)item.Tag)
                task.Cancel();
        }


        private void taskDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (taskListView.SelectedItems.Count == 0)
            {
                return;
            }
            var item = taskListView.SelectedItems[0];
            var task = (AsyncTask)item.Tag;
            if (task.State == AsyncTaskState.Error)
            {
                using (var exceptionDlg = new ExceptionForm())
                {
                    exceptionDlg.Exception = task.Exception;
                    exceptionDlg.ShowDialog();
                }
            }
        }

        private void taskMenu_Opened(object sender, EventArgs e)
        {
            var cancellable = false;
            foreach (var task in from ListViewItem item in taskListView.SelectedItems select (AsyncTask)item.Tag)
            {
                if (task.State == AsyncTaskState.Running
                    || task.State == AsyncTaskState.Waiting)
                {
                    cancellable = true;
                    break;
                }
            }
            cancelTaskToolStripMenuItem.Enabled = cancellable;
        }

        #region Task State Buttons

        private void CheckTasksToolStripButton(ToolStripButton button)
        {
            foreach (ToolStripButton item in tasksToolStrip.Items)
            {
                if (item != button)
                {
                    item.Checked = false;
                }
            }
            button.Checked = true;
        }

        private void activeTasksStripButton_Click(object sender, EventArgs e)
        {
            displayingTaskListStates = new AsyncTaskState[] { AsyncTaskState.Running };
            UpdateTaskList();
            CheckTasksToolStripButton((ToolStripButton)sender);
        }

        private void finishedTasksStripButton_Click(object sender, EventArgs e)
        {
            displayingTaskListStates = new AsyncTaskState[] { AsyncTaskState.Completed };
            UpdateTaskList();
            CheckTasksToolStripButton((ToolStripButton)sender);
        }

        private void cancelledTasksStripButton_Click(object sender, EventArgs e)
        {
            displayingTaskListStates = new AsyncTaskState[] { AsyncTaskState.Cancelled, AsyncTaskState.Error };
            UpdateTaskList();
            CheckTasksToolStripButton((ToolStripButton)sender);
        }

        private void queuedTasksStripButton_Click(object sender, EventArgs e)
        {
            displayingTaskListStates = new AsyncTaskState[] { AsyncTaskState.Waiting };
            UpdateTaskList();
            CheckTasksToolStripButton((ToolStripButton)sender);
        }

        #endregion

        public event EventHandler<ExceptionEventArgs> Error;
    }
}
