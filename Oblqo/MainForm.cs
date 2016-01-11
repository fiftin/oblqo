using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Oblqo.Properties;
using Oblqo.Tasks;
using System.Collections;
using Oblqo.Local;

namespace Oblqo
{
    public partial class MainForm : Form
    {
        public const string AccountImageKey = "account";
        public const string DisconnectedAccountImageKey = "account_disconnected";
        public const string FolderImageKey = "folder";
        public const string FileImageKey = "file";
        public const string ProgressImageKey = "process";

        class FileListSorder : IComparer
        {
            public const int ASC_ORDER = 1;
            public const int DESC_ORDER = -1;

            private int order;
            private ColumnHeader column;

            public FileListSorder(ColumnHeader column, int order = ASC_ORDER)
            {
                this.column = column;
                this.order = order;
            }
            
            public int Compare(object x, object y)
            {
                ListViewItem item1 = (ListViewItem)x;
                ListViewItem item2 = (ListViewItem)y;
                NodeInfo info1 = (NodeInfo)item1.Tag;
                NodeInfo info2 = (NodeInfo)item2.Tag;
                int ret = 0;
                if (info1 != null && info2 != null)
                {
                    switch (column.Text)
                    {
                        case "Name":
                            ret = info1.File.Name.CompareTo(info2.File.Name);
                            break;
                        case "Date":
                            ret = info1.File.ModifiedDate.CompareTo(info2.File.ModifiedDate);
                            break;
                        case "Size":
                            ret = info1.File.Size.CompareTo(info2.File.Size);
                            break;
                    }
                }
                return ret * order;
            }

            public void ToggleOrder(ColumnHeader newColumn)
            {
                if (column == newColumn)
                {
                    order = -order;
                }
                else
                {
                    order = ASC_ORDER;
                    column = newColumn;
                }
            }
        }

        private readonly AccountCollection accounts = new AccountCollection();
        private readonly AccountManager accountManager;
        private readonly AsyncTaskManager taskManager = new AsyncTaskManager(new IsolatedConfigurationStorage());
        private CancellationTokenSource updateListCancellationTokenSource;
        private readonly object updateListCancellationTokenSourceLocker = new object();
        private readonly List<TreeNode> loadingNodes = new List<TreeNode>();
        private ExceptionForm exceptionForm;
        private int loadingFolderImageAngle;
        private AsyncTaskState[] displayingTaskListStates = new AsyncTaskState[] { AsyncTaskState.Running };
        private int indicateErrorNo;

        private readonly Font UnsyncronizedFileItemFont;

        public MainForm()
        {
            InitializeComponent();
            try
            {
                accountManager = AccountManager.Load();
            }
            catch (Exception ex)
            {
                accountManager = new AccountManager();
                OnError(ex);
            }
            taskManager.TaskStateChanged += taskManager_TaskStateChanged;
            taskManager.TaskAdded += taskManager_TaskAdded;
            taskManager.TaskRemoved += taskManager_TaskRemoved;
            taskManager.Exception += taskManager_Exception;
            taskManager.TaskProgress += taskManager_TaskProgress;
            InitUI();
            splitContainer2.SplitterWidth = 7;
            UnsyncronizedFileItemFont = new Font(Font, FontStyle.Strikeout);
            btnNewConnection.Visible = accountManager.Accounts.Count() == 0;
            fileListView.ListViewItemSorter = new FileListSorder(fileNameColumnHeader);
        }

        private void taskManager_TaskProgress(object sender, AsyncTaskEventArgs<AsyncTaskProgressEventArgs> e)
        {
            try {
                var items = taskListView.Items.Cast<ListViewItem>();
                Invoke(new MethodInvoker(() =>
                {
                    var item = items.FirstOrDefault(x => x.Tag == e.Task);
                    if (item == null) return;
                    item.SubItems["percent"].Text = e.Args.PercentDone.ToString();
                }));
            } catch (Exception ex)
            {
                OnError(ex);
            }
        }

        void taskManager_Exception(object sender, ExceptionEventArgs e)
        {
            OnError(e.Exception);
        }

        private void InitUI()
        {
            foreach (var x in accountManager. Accounts)
            {
                var node = treeView1.Nodes.Add("", x.AccountName, DisconnectedAccountImageKey);
                node.SelectedImageKey = DisconnectedAccountImageKey;
                node.Tag = new NodeInfo(x);
            }
        }

        private async Task<Account> ConnectAccountAsync(string name, TreeNode node)
        {
            if (loadingNodes.Contains(node))
                return null;
            var nodeImageKey = node.ImageKey;
            loadingNodes.Add(node);
            try
            {
                var ret = await accountManager.CreateAccountAsync(name);
                ret.Tag = node;
                accounts.Add(name, ret);
                node.ImageKey = AccountImageKey;
                node.SelectedImageKey = AccountImageKey;
                var info = (NodeInfo)node.Tag;
                info.File = ret.RootFolder;
                return ret;
            }
            catch(Exception)
            {
                node.ImageKey = nodeImageKey;
                node.SelectedImageKey = nodeImageKey;
                throw;
            }
            finally
            {
                loadingNodes.Remove(node);
                UpdateToolBarAndMenu();
            }
        }


        private async Task ConnectAccountAsync(TreeNode node)
        {
            var nodeInfo = (NodeInfo)node.Tag;
            try
            {
                await ConnectAccountAsync(nodeInfo.AccountInfo.AccountName, node);
                UpdateNode(node);
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        public void HideFileInfoPanel()
        {
            if (!fileInfoPanel.Visible) return;
            splitter1.Hide();
            fileInfoPanel.Hide();
            driveStrip1.Visible = false;
        }

        public void ShowFileInfoPanel()
        {
            if (fileInfoPanel.Visible) return;
            splitter1.Show();
            fileInfoPanel.Show();
            driveStrip1.Visible = true;
        }

        private AccountFileStates GetFileState(AccountFile file)
        {
            AccountFileStates ret = 0;
            var isDrivesSyncronized = file.Account.Drives.Select(x => file.GetDriveFile(x)).All(x => x != null);
            if (!isDrivesSyncronized)
            {
                ret |= AccountFileStates.UnsyncronizedWithDrive;
            }
            if (file.StorageFileId == null)
            {
                ret |= AccountFileStates.UnsyncronizedWithStorage;
            }
            else if (!file.HasValidStorageFileId)
            {
                ret |= AccountFileStates.Error;
            }
            return ret;
        }

        private void UpdateFileListItem(AccountFileStates newFileState, ListViewItem fileItem)
        {
            if ((newFileState & AccountFileStates.Deleted) != 0)
            {
                fileItem.Remove();
                return;
            }
            if ((newFileState & AccountFileStates.New) != 0)
            {
                fileItem.ForeColor = Color.Green;
            }
            if ((newFileState & AccountFileStates.UnsyncronizedWithDrive) != 0)
            {
                fileItem.ForeColor = Color.Red;
            }
            if ((newFileState & AccountFileStates.UnsyncronizedWithStorage) != 0)
            {
                fileItem.Font = UnsyncronizedFileItemFont;
            }
            if ((newFileState & AccountFileStates.SyncronizedWithDrive) != 0)
            {
                fileItem.ForeColor = SystemColors.ControlText;
            }
            if ((newFileState & AccountFileStates.SyncronizedWithStorage) != 0)
            {
                fileItem.Font = Font;
            }
            if ((newFileState & AccountFileStates.Error) != 0)
            {
                fileItem.BackColor = Color.Red;
            }
        }

        private void AddFile(AccountFile file, string accountName)
        {
            var fileState = GetFileState(file);
            var key = "file";
            if (!string.IsNullOrWhiteSpace(file.MimeType))
            {
                var mimeTypeParts = file.MimeType.Split('/');
                if (mimeTypeParts.Length == 2)
                {
                    var tmpKey = string.Format("file_{0}_{1}", mimeTypeParts[0], mimeTypeParts[1]);
                    if (smallImageList.Images.ContainsKey(tmpKey))
                    {
                        key = tmpKey;
                    }
                }
            }

            var item = fileListView.Items.Add("", file.Name, key);
            UpdateFileListItem(fileState, item);
            item.Tag = new NodeInfo(file, accountName);
            item.SubItems.Add(file.ModifiedDate.ToShortDateString());
            item.SubItems.Add(Common.NumberOfBytesToString(file.Size));
        }

        private void UpdateFileList()
        {
            lock (updateListCancellationTokenSourceLocker)
            {
                updateListCancellationTokenSource?.Cancel();
                updateListCancellationTokenSource = new CancellationTokenSource();
            }
            var node = treeView1.SelectedNode;
            if (node == null) return;
            HideFileInfoPanel();
            var info = (NodeInfo) node.Tag;
            ICollection<AccountFile> files;
            Account account;
            if (!accounts.TryGetValue(info.AccountName, out account))
                return;
            fileListView.Enabled = false;
            loadingFileListProgressBar.Visible = true;
            Task.Run(async delegate
            {
                try
                {
                    switch (info.Type)
                    {
                        case NodeType.Account:
                            files = await accounts[info.AccountName].GetFilesAsync(accounts[info.AccountName].RootFolder,
                                updateListCancellationTokenSource.Token);
                            break;
                        case NodeType.Folder:
                            files = await accounts[info.AccountName].GetFilesAsync(info.File,
                                updateListCancellationTokenSource.Token);
                            break;
                        default:
                            throw new Exception("Unsupported node type");
                    }
                }
                catch (OperationCanceledException)
                {
                    // file getting is cancelled
                    return;
                }
                catch (Exception ex)
                {
                    OnError(ex);
                    return;
                }
                Invoke(new MethodInvoker(delegate
                {
                    fileListView.Items.Clear();
                    int numberOfFiles = 0;
                    int numberOfUnsyncFiles = 0;
                    foreach (var file in files.Where(file => !file.IsFolder))
                    {
                        var fileState = GetFileState(file);
                        if (!currentDirectoryInfoPanel.IsValid(file.Name))
                        {
                            continue;
                        }
                        if ((fileState & AccountFileStates.UnsyncronizedWithStorage) != 0)
                        {
                            numberOfUnsyncFiles++;
                        }
                        else if (currentDirectoryInfoPanel.ShowOnlyUnsyncronizedFiles)
                        {
                            continue;
                        }
                        numberOfFiles++;
                        AddFile(file, info.AccountName);
                    }
                    currentDirectoryInfoPanel.NumberOfFiles = numberOfFiles;
                    currentDirectoryInfoPanel.NumberOfUnsyncronizedFiles = numberOfUnsyncFiles;
                    fileListView.Enabled = true;
                    loadingFileListProgressBar.Visible = false;
                }));
            });
        }

        private void UpdateTaskList()
        {
            taskListView.Items.Clear();
            foreach (var task in taskManager.ToArray().Where(task => displayingTaskListStates.Contains(task.State)))
            {
                AddTask(task);
            }
        }

        private void UpdateNode(TreeNode node, bool extendNodeAfterUpdate = false, bool updateList = false)
        {
            var token = new CancellationToken();
            var info = (NodeInfo) node.Tag;
            node.Nodes.Clear();
            var nodeImageKey = node.ImageKey;
            loadingNodes.Add(node);
            Task.Run(async delegate
            {
                ICollection<AccountFile> folders = null;
                try
                {
                    switch (info.Type)
                    {
                        case NodeType.Account:
                            folders = await accounts[info.AccountName].GetSubfoldersAsync(accounts[info.AccountName].RootFolder, token);
                            break;
                        case NodeType.Folder:
                            folders = await accounts[info.AccountName].GetSubfoldersAsync(info.File, token);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    OnError(ex);
                }

                if (folders == null)
                {
                    folders = new List<AccountFile>();
                }

                Invoke(new MethodInvoker(delegate
                {
                    foreach (var file in folders)
                    {
                        var viewNode = node.Nodes.Add("", file.Name, FolderImageKey);
                        viewNode.SelectedImageKey = FolderImageKey;
                        viewNode.Tag = new NodeInfo(file, info.AccountName);
                        if (file.HasChildren)
                            viewNode.Nodes.Add("", "", "");
                    }
                    if (extendNodeAfterUpdate)
                        node.Expand();
                    loadingNodes.Remove(node);
                    node.ImageKey = nodeImageKey;
                    node.SelectedImageKey = nodeImageKey;
                    if (updateList)
                        UpdateFileList();
                }));
            });
        }

        private IEnumerable<TreeNode> GetAllTreeViewNodes(TreeView treeView)
        {
            var ret = new List<TreeNode>();
            foreach (TreeNode node in treeView.Nodes)
            {
                ret.Add(node);
                ret.AddRange(GetAllTreeViewNodes(node));
            }
            return ret;
        }

        private IEnumerable<TreeNode> GetAllTreeViewNodes(TreeNode root)
        {
            var ret = new List<TreeNode>();
            foreach (TreeNode node in root.Nodes)
            {
                ret.Add(node);
                ret.AddRange(GetAllTreeViewNodes(node));
            }
            return ret;
        }

        private void UpdateTeeViewNode(AccountFileStates newFileState, TreeNode node)
        {
            if ((newFileState & AccountFileStates.Deleted) != 0)
            {
                node.Remove();
                return;
            }
            if ((newFileState & AccountFileStates.New) != 0)
            {
                node.ForeColor = Color.Green;
            }
        }


        private void AddNode(AccountFile file, TreeNode parentNode, string accountName)
        {

            var newNode = parentNode.Nodes.Add("", file.Name, FolderImageKey);

            newNode.SelectedImageKey = FolderImageKey;
            newNode.Tag = new NodeInfo(file, accountName);
            if (file.HasChildren)
                newNode.Nodes.Add("", "", "");
        }

        private void addNewAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var accountForm = new AccountForm(true))
            {
                if (accountForm.ShowDialog() != DialogResult.OK) return;
                btnNewConnection.Visible = false;
                var info = new AccountInfo
                {
                    AccountName = accountForm.AccountName,
                    StorageAccessKeyId = accountForm.StorageAccessTokenId,
                    StorageSecretAccessKey = accountForm.StorageSecretAccessKey,
                    StorageVault = accountForm.GlacierVault,
                    StorageRegionSystemName = accountForm.StorageRegionSystemName,
                };
                info.Drives.Clear();
                info.Drives.AddRange(accountForm.GetDrives());
                accountManager.Add(info);
                var node = treeView1.Nodes.Add("", info.AccountName, AccountImageKey);
                node.SelectedImageKey = AccountImageKey;
                node.Tag = new NodeInfo(info);
                accountManager.Save();
                DisconnectAccount(node);
            }
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

        private ListViewItem AddTask(AsyncTask newTask)
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
                } else
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
                        var parentFile = parentFileProp == null ? null :(AccountFile)parentFileProp.GetValue(e.Task);

                        if (file.IsFile)
                        {
                            if (attr.NewState == AccountFileStates.New)
                            {
                                AddFile(file, accounts.GetName(file.Account));
                            }
                            else
                            {
                                foreach (ListViewItem x in fileListView.Items)
                                {
                                    var info = (NodeInfo)x.Tag;
                                    if (info.File == file)
                                    {
                                        UpdateFileListItem(attr.NewState, x);

                                        if (driveStrip1.File == file)
                                        {
                                            driveStrip1.RefreshData();
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        else // IsFolder
                        {
                            if (attr.NewState == AccountFileStates.New)
                            {
                                foreach (var x in GetAllTreeViewNodes(treeView1))
                                {
                                    var info = (NodeInfo)x.Tag;
                                    if (info.File == parentFile)
                                    {
                                        AddNode(file, x, info.AccountName);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                foreach (var x in GetAllTreeViewNodes(treeView1))
                                {
                                    var info = (NodeInfo)x.Tag;
                                    if (info.File == file)
                                    {
                                        UpdateTeeViewNode(attr.NewState, x);
                                    }
                                }
                            }
                        }
                    }
                }

                if (item == null) return;
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
                        break;
                    case AsyncTaskState.Completed:
                        item.SubItems["percent"].Text = "100";
                        if (e.Task is CreateFolderTask)
                        {
                            var task = (CreateFolderTask)e.Task;
                            var parentNode = task.Tag as TreeNode;
                            if (parentNode != null)
                            {
                                var viewNode = parentNode.Nodes.Add("", task.FolderName, FolderImageKey);
                                viewNode.SelectedImageKey = FolderImageKey;
                                viewNode.Tag = new NodeInfo(task.CreatedFolder, task.AccountName);
                            }
                        }
                        else if (e.Task is UploadFolderTask)
                        {
                            var task = (UploadFolderTask)e.Task;
                            var parentNode = task.Tag as TreeNode;
                            if (parentNode != null)
                            {
                                var viewNode = parentNode.Nodes.Add("", task.FolderName, FolderImageKey);
                                viewNode.SelectedImageKey = FolderImageKey;
                                viewNode.Tag = new NodeInfo(task.CreatedFolder, task.AccountName);
                            }
                        }
                        else if (e.Task is UploadFileTask)
                        {
                            var task = (UploadFileTask)e.Task;
                            var parentNode = task.Tag as TreeNode;
                            var selectedNode = treeView1.SelectedNode;
                            if (parentNode != null && selectedNode == parentNode)
                            {
                                UpdateFileList();
                            }
                        }
                        else if (e.Task is DeleteFileTask)
                        {
                            var task = (DeleteFileTask)e.Task;
                            var listItem = task.Tag as ListViewItem;
                            listItem?.Remove();
                        }
                        else if (e.Task is DeleteFolderTask)
                        {
                            var task = (DeleteFolderTask)e.Task;
                            var node = task.Tag as TreeNode;
                            node?.Remove();
                        }
                        else if (e.Task is SynchronizeFileTask)
                        {
                        }
                        else if (e.Task is SynchronizeDriveFileTask)
                        {
                        }
                        break;
                }
            }));
        }

        private async void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                var nodeInfo = ((NodeInfo)e.Node.Tag);
                switch (nodeInfo.Type)
                {
                    case NodeType.Account:
                        // is not connected
                        if (!accounts.ContainsKey(nodeInfo.AccountInfo.AccountName))
                        {
                            var account = await ConnectAccountAsync(nodeInfo.AccountInfo.AccountName, e.Node);
                            UpdateNode(e.Node, true, true);
                            await Task.Run(async delegate
                            {
                                await taskManager.RestoreAsync(account, nodeInfo.AccountInfo.AccountName, CancellationToken.None);
                            });
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            accountManager.Save();
            taskManager.SaveAll();
        }

        /// <summary>
        /// Show content menu for Left Side Tree View.
        /// </summary>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var nodeInfo = (NodeInfo) e.Node.Tag; 
            switch (e.Button)
            {
                case MouseButtons.Right:
                    switch (nodeInfo.Type)
                    {
                        case NodeType.Account:
                            accountMenu.Show(Cursor.Position);
                            break;
                        case NodeType.Folder:
                            folderMenu.Show(Cursor.Position);
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Change Account menu item.
        /// </summary>
        private async void changeAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null)
                return;
            using (var accountForm = new AccountForm(false))
            {
                var nodeInfo = ((NodeInfo)node.Tag);
                var account = accountManager.Get(nodeInfo.AccountInfo.AccountName);
                accountForm.AccountName = account.AccountName;
                accountForm.StorageAccessTokenId = account.StorageAccessKeyId;
                accountForm.StorageSecretAccessKey = account.StorageSecretAccessKey;
                accountForm.StorageRegionSystemName = account.StorageRegionSystemName;
                accountForm.GlacierVault = account.StorageVault;
                accountForm.AddDrives(account.Drives);

                if (accountForm.ShowDialog() != DialogResult.OK) return;
                account.OldAccountName = accountForm.AccountName == account.AccountName ? null : account.AccountName;
                account.AccountName = accountForm.AccountName;
                account.StorageAccessKeyId = accountForm.StorageAccessTokenId;
                account.StorageSecretAccessKey = accountForm.StorageSecretAccessKey;
                account.StorageRegionSystemName = accountForm.StorageRegionSystemName;
                account.StorageVault = accountForm.GlacierVault;
                node.Text = account.AccountName;
                node.Tag = new NodeInfo(account);
               
                account.Drives.Clear();
                account.Drives.AddRange(accountForm.GetDrives());

                accountManager.Save();

                DisconnectAccount(node);
                await ConnectAccountAsync(node);
            }
        }


        private async void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                await ConnectAccountAsync(((NodeInfo)treeView1.SelectedNode.Tag).AccountInfo.AccountName, treeView1.SelectedNode);
                UpdateNode(treeView1.SelectedNode);
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateFileList();
            UpdateToolBarAndMenu();
        }

        private void UpdateToolBarAndMenu()
        {
            var node = treeView1.SelectedNode;
            if (node == null)
                return;
            var nodeInfo = (NodeInfo)node.Tag;
            switch (nodeInfo.Type)
            {
                case NodeType.Account:
                    if (accounts.ContainsKey(nodeInfo.AccountName))
                    {
                        newFolderToolStripButton.Enabled = true;
                        uploadToolStripDropDownButton.Enabled = true;
                        refreshFilesToolStripButton.Enabled = true;
                        connectToolStripMenuItem.Enabled = false;
                        disconnectToolStripMenuItem.Enabled = true;
                        downloadFromDriveToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        newFolderToolStripButton.Enabled = false;
                        uploadToolStripDropDownButton.Enabled = false;
                        refreshFilesToolStripButton.Enabled = false;
                        connectToolStripMenuItem.Enabled = true;
                        disconnectToolStripMenuItem.Enabled = false;
                        downloadFromDriveToolStripMenuItem.Enabled = false;
                    }
                    break;
                case NodeType.Folder:
                    newFolderToolStripButton.Enabled = true;
                    uploadToolStripDropDownButton.Enabled = true;
                    refreshFilesToolStripButton.Enabled = true;
                    break;
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var node = treeView1.GetNodeAt(e.Location);
            if (node == null)
                return;
            treeView1.SelectedNode = node;
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            var info = (NodeInfo) e.Node.Tag;
            if (info.Type != NodeType.Folder) return;
            if (e.Node.Nodes[0].Name == "")
                UpdateNode(e.Node);
        }

        private void fileListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileListView.SelectedItems.Count == 0)
                return;
            if (fileListView.SelectedItems.Count > 1)
                return;

            ShowFileInfoPanel();

            var info = (NodeInfo)fileListView.SelectedItems[0].Tag;

            driveStrip1.File = info.File;

        }

        private void OnError(Exception exception)
        {
            if (exception is Core.ConnectionException)
            {
                Invoke(new MethodInvoker(() =>
                {
                    var acc = ((Core.ConnectionException)exception).Account;
                    if (acc.Tag is TreeNode)
                    {
                        DisconnectAccount((TreeNode)acc.Tag);
                    }
                    else
                    {
                        acc.Disconnect();
                    }
                }));
                OnError(exception.InnerException);
            }
            else if (exception is AggregateException)
            {
                ((AggregateException)exception).Handle(x =>
               {
                   OnError(x);
                   return true;
               });
            }
            else
            {
                try
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        var item = logListView.Items.Insert(0, DateTime.Now.ToString(CultureInfo.CurrentCulture), "error");
                        item.SubItems.Add(exception.Message);
                        item.Tag = exception;
                        logTabPage.ImageKey = "error";
                        IndicateError();
                    }));
                }
                catch (Exception)
                {
                    // Ignore
                }
            }
        }

        private void IndicateError()
        {
            indicateErrorTimer.Start();
            indicateErrorNo++;
        }

        private void deleteAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null)
                return;
            var nodeInfo = (NodeInfo) node.Tag;
            accountManager.Remove(nodeInfo.AccountName);
            if (accounts.ContainsKey(nodeInfo.AccountName))
            {
                accounts[nodeInfo.AccountName].Disconnect();
                accounts.Remove(nodeInfo.AccountName);
            }
            accountManager.Save();
            treeView1.Nodes.Remove(node);
        }

        private void uploadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            var selectedNode = treeView1.SelectedNode;
            if (selectedNode == null) return;
            var nodeInfo = (NodeInfo) selectedNode.Tag;
            foreach (var fileName in openFileDialog1.FileNames)
                taskManager.Add(new UploadFileTask(accounts[nodeInfo.AccountName], nodeInfo.AccountName, AsyncTask.NormalPriority, null,
                    fileName, nodeInfo.File) { Tag = selectedNode });
        }

        private void uploadFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;
            var node = treeView1.SelectedNode;
            if (node == null)
                return;
            var info = (NodeInfo)node.Tag;
            taskManager.Add(new UploadFolderTask(accounts[info.AccountName],
                info.AccountName,
                0,
                null,
                folderBrowserDialog1.SelectedPath,
                info.File) {Tag = node});
        }

        private void newFolderToolStripButton_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null)
                return;
            var nodeInfo = (NodeInfo) node.Tag;
            using (var dialog = new CreateFolderForm())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var syncDestFolderTask = new SynchronizeDriveEmptyFolderTask(accounts[nodeInfo.AccountName],
                        nodeInfo.AccountName, AsyncTask.NormalPriority, null, nodeInfo.File);
                    taskManager.Add(syncDestFolderTask);
                    taskManager.Add(new CreateFolderTask(accounts[nodeInfo.AccountName], nodeInfo.AccountName,
                        AsyncTask.NormalPriority, new AsyncTask[] { syncDestFolderTask }, dialog.DirecotryName, nodeInfo.File) {Tag = node});
                }
            }
        }

        /// <summary>
        /// Resize File List View Progress Bar.
        /// </summary>
        private void listView1_Move(object sender, EventArgs e)
        {
            var bars = new Control[] { loadingFileListProgressBar, currentDirectoryInfoPanel };
            foreach (var bar in bars)
            {
                bar.Left = splitContainer2.SplitterDistance + splitContainer2.SplitterWidth;
                bar.Width = fileListView.Width;
            }
            loadingImageProgressBar.Left = splitContainer2.SplitterDistance + splitContainer2.SplitterWidth + splitter1.Left + splitter1.Width;
            loadingImageProgressBar.Width = fileInfoPanel.Width - 1;

            driveStrip1.Left = splitContainer2.SplitterDistance + splitContainer2.SplitterWidth + splitter1.Left + splitter1.Width;
            driveStrip1.Width = fileInfoPanel.Width - 1;

        }

        /// <summary>
        /// Resize File List View Progress Bar.
        /// </summary>
        private void listView1_Resize(object sender, EventArgs e)
        {
            var bars = new Control[] { loadingFileListProgressBar, currentDirectoryInfoPanel };
            foreach (var bar in bars)
            {
                bar.Left = splitContainer2.SplitterDistance + splitContainer2.SplitterWidth;
                bar.Width = fileListView.Width;
                bar.Top = splitContainer1.Top + splitContainer1.SplitterDistance + 7;
            }

            driveStrip1.Left = loadingImageProgressBar.Left = splitContainer2.SplitterDistance + splitContainer2.SplitterWidth + splitter1.Left + splitter1.Width;
            driveStrip1.Width = loadingImageProgressBar.Width = fileInfoPanel.Width - 1;
            driveStrip1.Top = loadingImageProgressBar.Top = loadingFileListProgressBar.Top;
        }

        /// <summary>
        /// Show context menu for File List View.
        /// </summary>
        private void fileListView_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    if (fileListView.SelectedItems.Count > 0)
                    {
                        downloadFileFromStorageToolStripMenuItem.Enabled = true;
                        if (fileListView.SelectedItems.Count == 1)
                        {
                            var info = (NodeInfo)fileListView.SelectedItems[0].Tag;
                            if (info.File.StorageFile == null || info.File.StorageFile.Id == null)
                            {
                                downloadFileFromStorageToolStripMenuItem.Enabled = false;
                            }
                        }
                        fileMenu.Show(Cursor.Position);
                    }
                    else
                    {
                        fileListMenu.Show(Cursor.Position);
                    }
                    break;
            }
        }

        private void downloadFileFromDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;
            foreach (var info in from ListViewItem item in fileListView.SelectedItems select (NodeInfo) item.Tag)
                taskManager.Add(new DownloadFileFromDriveTask(accounts[info.AccountName], info.AccountName,
                    AsyncTask.NormalPriority, null, info.File, folderBrowserDialog1.SelectedPath));
        }

        private void downloadFileFromStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;
            foreach (var info in from ListViewItem item in fileListView.SelectedItems select (NodeInfo)item.Tag)
            {
                var task = new DownloadFileFromStorageTask(
                    accounts[info.AccountName],
                    info.AccountName,
                    0, null, info.File,
                    folderBrowserDialog1.SelectedPath);
                taskManager.Add(task);
            }
        }

        private void downloadFolderFromDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFolderFromDrive();
        }

        private void refreshFilesToolStripButton_Click(object sender, EventArgs e)
        {
            UpdateFileList();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeView1.SelectedNode;
            if (selectedNode == null)
                return;
            DisconnectAccount(selectedNode);
        }

        private void loadingFoldersTimer_Tick(object sender, EventArgs e)
        {
            var imageKey = ProgressImageKey + (loadingFolderImageAngle == 0 ? "" : loadingFolderImageAngle.ToString());
            foreach (var node in loadingNodes)
            {
                node.ImageKey = imageKey;
                node.SelectedImageKey = imageKey;
            }
            loadingFolderImageAngle += 90;
            while (loadingFolderImageAngle >= 360)
                loadingFolderImageAngle -= 360;
        }

        private void DisconnectAccount(TreeNode node)
        {
            loadingNodes.Remove(node);
            node.Nodes.Clear();
            node.ImageKey = DisconnectedAccountImageKey;
            node.SelectedImageKey = DisconnectedAccountImageKey;
            accounts.Remove(node.Text);
            fileListView.Items.Clear();
            UpdateToolBarAndMenu();
            // TODO: Break unfinished tasks
        }
        
        private void deleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null)
                return;
            var nodeInfo = (NodeInfo)node.Tag;
            var account = accounts[nodeInfo.AccountName];
            if (account == null)
                return;
            taskManager.Add(new DeleteFolderTask(account, nodeInfo.AccountName, 0, null, nodeInfo.File) { Tag = node });
        }

        private void downloadFromDriveOnlyContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFolderFromDrive();
        }

        private void downloadFromDriveFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFolderFromDrive();
        }

        private void downloadFromDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFolderFromDrive();
        }

        private void DownloadFolderFromDrive()
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;
            var node = treeView1.SelectedNode;
            if (node == null)
                return;
            var nodeInfo = (NodeInfo)node.Tag;
            var account = accounts[nodeInfo.AccountName];
            if (account == null)
                return;
            taskManager.Add(new DownloadFolderFromDriveTask(account, nodeInfo.AccountName, 0, null,
                nodeInfo.File, folderBrowserDialog1.SelectedPath));

        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in fileListView.SelectedItems)
            {
                var info = (NodeInfo) item.Tag;
                var account = accounts[info.AccountName];
                if (account == null)
                    continue;
                taskManager.Add(new DeleteFileTask(account, info.AccountName, 0, null, info.File) { Tag = item });
            }
        }

        private void cancelTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var task in from ListViewItem item in taskListView.SelectedItems select (AsyncTask) item.Tag)
                task.Cancel();
        }

        private void showDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = logListView.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (selectedItem == null)
                return;
            if (exceptionForm == null || exceptionForm.IsDisposed)
                exceptionForm = new ExceptionForm();
            exceptionForm.Exception = (Exception)selectedItem.Tag;
            if (!exceptionForm.Visible)
            {
                exceptionForm.Show(this);
            }
            exceptionForm.Focus();
        }

        private void logListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right)
                return;
            if (logListView.SelectedItems.Count > 0)
                logMenu.Show(Cursor.Position);
        }

        private void logListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (exceptionForm == null)
                return;            var selectedItem = logListView.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (selectedItem == null)
                return;
            exceptionForm.Exception = (Exception)selectedItem.Tag;
        }

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == logTabPage)
                logTabPage.ImageKey = null;
        }

        private void indicateErrorTimer_Tick(object sender, EventArgs e)
        {
            if (indicateErrorNo < 12)
            {
                logTabPage.ImageKey = logTabPage.ImageKey == "error_red" ? "error" : "error_red";
                indicateErrorNo++;
            }
            if (indicateErrorNo >= 12)
            {
                logTabPage.ImageKey = "error";
                indicateErrorNo = 0;
                indicateErrorTimer.Stop();
            }
        }

        private void fileListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
            var info = (NodeInfo)e.Item.Tag;
            if (info.File.StorageFileId == null)
            {
                e.Graphics.DrawLine(Pens.Black, e.Bounds.X, e.Bounds.Y + e.Bounds.Height / 2,
                    e.Bounds.Right, e.Bounds.Y + e.Bounds.Height / 2);
            }
        }

        private void synchronizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in fileListView.SelectedItems)
            {
                var info = (NodeInfo)item.Tag;
                var folderInfo = (NodeInfo)treeView1.SelectedNode.Tag;
                if (info.File.DriveFiles.StorageFileId == null)
                {
                    taskManager.Add(new SynchronizeFileTask(accounts[info.AccountName],
                        info.AccountName, 0, new AsyncTask[0], info.File));
                }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in fileListView.Items)
            {
                item.Selected = true;
            }
        }

        private void fileListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            FileListSorder sorter = (FileListSorder)fileListView.ListViewItemSorter;
            sorter.ToggleOrder(fileListView.Columns[e.Column]);
            fileListView.Sort();
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            var newMargin = uploadToolStripDropDownButton.Margin;
            newMargin.Left = e.X + 2 - newAccountStripButton.Width;
            uploadToolStripDropDownButton.Margin = newMargin;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFileList();
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

        private void synchronizeOnDrivesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folderInfo = (NodeInfo)treeView1.SelectedNode.Tag;
            var syncFolderTask = new SynchronizeDriveEmptyFolderTask(accounts[folderInfo.AccountName],
                folderInfo.AccountName, 0, new AsyncTask[0], folderInfo.File);
            taskManager.Add(syncFolderTask);
            foreach (ListViewItem item in fileListView.SelectedItems)
            {
                var info = (NodeInfo)item.Tag;
                var account = accounts[info.AccountName];
                if (info.File.DriveFiles.Count < account.Drives.Count)
                {
                    taskManager.Add(new SynchronizeDriveFileTask(account, info.AccountName, 0, new AsyncTask[] { syncFolderTask }, info.File));
                }
            }
        }

        private void synchronizeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in fileListView.SelectedItems)
            {
                var info = (NodeInfo)item.Tag;
                var folderInfo = (NodeInfo)treeView1.SelectedNode.Tag;
                if (info.File.DriveFiles.StorageFileId == null)
                {
                    taskManager.Add(new SynchronizeFileTask(accounts[info.AccountName],
                        info.AccountName, 0, new AsyncTask[0], info.File));
                }
            }
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

        private void currentDirectoryInfoPanel_FilterChanged(object sender, EventArgs e)
        {
            fileListView.Focus();
            UpdateFileList();
        }

        private void fileInfoPanel_Error(object sender, ExceptionEventArgs e)
        {
            OnError(e.Exception);
        }

        private void fileInfoPanel_ImageLoading(object sender, EventArgs e)
        {
            loadingImageProgressBar.Visible = true;
            driveStrip1.Visible = false;
        }

        private void fileInfoPanel_ImageLoaded(object sender, EventArgs e)
        {
            loadingImageProgressBar.Visible = false;
            driveStrip1.Visible = true;
        }

        private void driveStrip1_SelectedDriveChanged(object sender, EventArgs e)
        {
            fileInfoPanel.DriveFile = driveStrip1.DriveFile;
        }

        private void synchronizeFolder_Click(object sender, EventArgs e)
        {
        }

        private void aboutStripButton_Click(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

        private void fileListView_SizeChanged(object sender, EventArgs e)
        {
            btnNewConnection.Left = fileListView.Width / 4 - btnNewConnection.Width / 4;
            btnNewConnection.Top = fileListView.Height/ 3;
        }

        private void cloneAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeView1.SelectedNode;
            if (selectedNode == null)
                return;
            var nodeInfo = (NodeInfo)selectedNode.Tag;
            var info = nodeInfo.AccountInfo.Clone();
            info.AccountName += " Copy";
            while (accountManager.Get(info.AccountName) != null)
            {
                info.AccountName += " Copy";
            }
            accountManager.Add(info);
            var newNode = treeView1.Nodes.Add("", info.AccountName, AccountImageKey);
            newNode.SelectedImageKey = AccountImageKey;
            newNode.Tag = new NodeInfo(info);
            DisconnectAccount(newNode);
            accountManager.Save();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/fiftin/oblqo/wiki");
        }

        private void deleteFromArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in fileListView.SelectedItems)
            {
                var info = (NodeInfo)item.Tag;
                var account = accounts[info.AccountName];
                if (account == null)
                    continue;
                taskManager.Add(new DeleteFileFromArchiveTask(account, info.AccountName, 0, null, info.File) { Tag = item });
            }
            
        }

        private void fileListView_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedFileIfItLocal();
        }

        private void OpenSelectedFileIfItLocal()
        {
            if (fileListView.SelectedItems.Count > 0)
            {
                var selectedFile = fileListView.SelectedItems[0];
                var nodeInfo = (NodeInfo)selectedFile.Tag;
                var localFile = (LocalFile)nodeInfo.File.DriveFiles.FirstOrDefault(x => x.Drive is LocalDrive);
                if (localFile != null)
                {
                    System.Diagnostics.Process.Start(localFile.FullName);
                }
            }
        }

        private void OpenSelectedFileContainingFolderIfItLocal()
        {
            if (fileListView.SelectedItems.Count > 0)
            {
                var selectedFile = fileListView.SelectedItems[0];
                var nodeInfo = (NodeInfo)selectedFile.Tag;
                var localFile = (LocalFile)nodeInfo.File.DriveFiles.FirstOrDefault(x => x.Drive is LocalDrive);
                if (localFile != null)
                {
                    System.Diagnostics.Process.Start(Path.GetDirectoryName(localFile.FullName));
                }
            }
        }

        private void fileListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenSelectedFileIfItLocal();
            }
        }

        private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedFileContainingFolderIfItLocal();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedFileIfItLocal();
        }

        private async void clearAuthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null)
                return;
            var nodeInfo = (NodeInfo)node.Tag;
            await accountManager.ClearAuthAsync(nodeInfo.AccountInfo);
        }
        
    }

}
