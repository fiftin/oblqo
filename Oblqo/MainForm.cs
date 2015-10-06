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
// ReSharper disable CanBeReplacedWithTryCastAndCheckForNull

namespace Oblqo
{
    public partial class MainForm : Form
    {
        public const string AccountImageKey = "account";
        public const string DisconnectedAccountImageKey = "account_disconnected";
        public const string FolderImageKey = "folder";
        public const string FileImageKey = "file";
        public const string ProgressImageKey = "process";

        /// <summary>
        /// Type of node in left side tree view.
        /// </summary>
        private enum NodeType
        {
            Account,
            Folder
        }

        class FileListSorder : IComparer
        {

            public FileListSorder(ListView listView)
            {
            }

            public int Compare(object x, object y)
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private class NodeInfo
        {
            public NodeInfo(AccountInfo accountInfo)
            {
                Type = NodeType.Account;
                AccountInfo = accountInfo;
                AccountName = AccountInfo.AccountName;
            }

            public NodeInfo(AccountFile file, string accountName)
            {
                Type = NodeType.Folder;
                File = file;
                AccountName = accountName;
            }

            public NodeType Type { get; private set; }
            public AccountFile File { get; set; }
            public AccountInfo AccountInfo { get; private set; }
            public string AccountName { get; private set; }
        }


        private readonly AccountCollection accounts = new AccountCollection();
        private readonly AccountManager accountManager;
        private readonly AsyncTaskManager taskManager = new AsyncTaskManager();
        private CancellationTokenSource updateListCancellationTokenSource;
        private readonly object updateListCancellationTokenSourceLocker = new object();
        private CancellationTokenSource pictureCancellationTokenSource;
        private readonly object pictureCancellationTokenSourceLoker = new object();
        private readonly List<TreeNode> loadingNodes = new List<TreeNode>();
        private ExceptionForm exceptionForm;
        private int loadingFolderImageAngle;
        private AsyncTaskState[] displayingTaskListStates = new AsyncTaskState[] { AsyncTaskState.Running };
        private int controlNumber = 0;
        private int indicateErrorNo;


        public MainForm()
        {
            InitializeComponent();
            try
            {
                accountManager = AccountManager.Load();
            }
            catch
            {
                accountManager = new AccountManager();
            }
            taskManager.TaskStateChanged += taskManager_TaskStateChanged;
            taskManager.TaskAdded += taskManager_TaskAdded;
            taskManager.TaskRemoved += taskManager_TaskRemoved;
            taskManager.Exception += taskManager_Exception;
            taskManager.TaskProgress += taskManager_TaskProgress;
            taskManager.Start();
            InitUI();
            splitContainer2.SplitterWidth = 7;
            
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
            if (!pictureBox1.Visible) return;
            splitter1.Hide();
            pictureBox1.BackgroundImage = null;
            fileInfoPanel.Hide();
        }

        public void ShowFileInfoPanel()
        {
            if (pictureBox1.Visible) return;
            pictureBox1.BackgroundImage = Resources.loading;
            splitter1.Show();
            fileInfoPanel.Show();
        }

        private void UpdateFileList()
        {
            lock (updateListCancellationTokenSourceLocker)
            {
                if (updateListCancellationTokenSource != null)
                    updateListCancellationTokenSource.Cancel();
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
                        numberOfFiles++;
                        if (file.StorageFile == null || string.IsNullOrEmpty(file.StorageFile.Id))
                        {
                            numberOfUnsyncFiles++;
                        }
                        else if (showSyncFilesOnlyCheckbox.Checked)
                        {
                            continue;
                        }

                        if (fileListFilterTextBox.Tag != null)
                        {
                            if (!file.Name.Contains(fileListFilterTextBox.Text))
                            {
                                continue;
                            }
                        }

                        string mimeType = file.DriveFile.MimeType;
                        string key = "file";
                        if (!string.IsNullOrWhiteSpace(mimeType))
                        {
                            string[] mimeTypeParts = mimeType.Split('/');
                            if (mimeTypeParts.Length == 2)
                            {
                                string tmpKey = string.Format("file_{0}_{1}", mimeTypeParts[0], mimeTypeParts[1]);
                                if (smallImageList.Images.ContainsKey(tmpKey))
                                {
                                    key = tmpKey;
                                }
                            }
                        }
                        var item = fileListView.Items.Add("", file.Name, key);
                        if (string.IsNullOrEmpty(file.DriveFile.StorageFileId))
                        {
                            item.ForeColor = Color.Red;
                        }
                        item.Tag = new NodeInfo(file, info.AccountName);
                        item.SubItems.Add(file.DriveFile.ModifiedDate.ToShortDateString());
                        item.SubItems.Add(Common.NumberOfBytesToString(file.DriveFile.Size));
                    }
                    fileListNumberOfFilesLabel.Text = string.Format("{0} files, {1} unsync", numberOfFiles, numberOfUnsyncFiles);
                    fileListView.Enabled = true;
                    loadingFileListProgressBar.Visible = false;
                }));
            });
        }

        /// <summary>
        /// Update labels on Properties page.
        /// </summary>
        private void UpdateProperties()
        {
            var node = treeView1.SelectedNode;
            if (node == null)
                return;
            var nodeInfo = (NodeInfo)node.Tag;
            switch (nodeInfo.Type)
            {
                case NodeType.Account:
                    vaultNameLabel.Text = nodeInfo.AccountInfo.StorageVault;
                    vaultRegionLabel.Text = nodeInfo.AccountInfo.StorageRegionSystemName;
                    //driveTypeLabel.Text = Common.CamelcaseToHumanReadable(nodeInfo.AccountInfo.DriveType.ToString());
                    //driveRootLabel.Text = nodeInfo.AccountInfo.DriveRootPath.Length == 0 ? "/" : nodeInfo.AccountInfo.DriveRootPath;
                    //imageMaxSizeLabel.Text = string.Format("{0} x {1}", nodeInfo.AccountInfo.DriveImageMaxSize.Width, nodeInfo.AccountInfo.DriveImageMaxSize.Height);
                    break;
                case NodeType.Folder:
                    break;
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

        private async void addNewAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var accountForm = new AccountForm(true))
            {
                if (accountForm.ShowDialog() != DialogResult.OK) return;
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
                await ConnectAccountAsync(info.AccountName, node);
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

        private void UpdateTaskList()
        {
            taskListView.Items.Clear();
            foreach (var task in taskManager.ToArray().Where(task => displayingTaskListStates.Contains(task.State)))
            {
                AddTask(task);
            }
        }

        private ListViewItem AddTask(AsyncTask newTask)
        {
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
                if (task.File.DriveFile != null)
                {
                    taskItem.SubItems.Add(Common.NumberOfBytesToString(task.File.DriveFile.OriginalSize))
                        .Name = "size";
                }
                else
                {
                    taskItem.SubItems.Add(Common.NumberOfBytesToString(0))
                        .Name = "size";
                }
                taskItem.SubItems.Add("0").Name = "percent";
            }
            else if (newTask is DownloadFileFromDriveTask)
            {
                var task = (DownloadFileFromDriveTask)newTask;
                taskItem.Text = task.File.Name;
                taskItem.SubItems.Add("Download File").Name = "type";
                if (task.File.DriveFile != null)
                {
                    taskItem.SubItems.Add(Common.NumberOfBytesToString(task.File.DriveFile.OriginalSize))
                        .Name = "size";
                }
                else
                {
                    taskItem.SubItems.Add(Common.NumberOfBytesToString(0))
                        .Name = "size";
                }
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
            taskManager.Save();
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
                var account = accountManager.Get(((NodeInfo) node.Tag).AccountInfo.AccountName);
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

                account.Drives.Clear();
                account.Drives.AddRange(accountForm.GetDrives());

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
            UpdateProperties();
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

            controlNumber++;
            var info = (NodeInfo) fileListView.SelectedItems[0].Tag;

            fileNameLabel.Text = info.File.Name;
            fileSizeLabel.Text = Common.NumberOfBytesToString(info.File.DriveFile.Size);
            fileStorageIdLabel.Text = string.IsNullOrEmpty(info.File.StorageFile.Id) ? "none" : info.File.StorageFile.Id;
            storageIdLabel.Text = info.File.StorageFile.Storage.Id;

            label3.Visible = false;
            widthAndHeightLabel.Visible = false;

            if (!info.File.IsImage)
            {
                lock (pictureCancellationTokenSourceLoker)
                {
                    pictureCancellationTokenSource?.Cancel();
                    pictureCancellationTokenSource = new CancellationTokenSource();
                }

                loadingImageProgressBar.Visible = false;
                pictureBox1.BackgroundImage = Resources.no_image;
                return;
            }
            
            // Image preview async loading
            loadingImageProgressBar.Visible = true;
            Task.Run(async delegate
            {
                try
                {
                    lock (pictureCancellationTokenSourceLoker)
                    {
                        if (pictureCancellationTokenSource != null)
                            pictureCancellationTokenSource.Cancel();
                        pictureCancellationTokenSource = new CancellationTokenSource();
                    }

                    pictureBox1.BackgroundImage = Resources.loading;
                    int cn = controlNumber;
                    Image image;
                    try
                    {
                        image = await accounts[info.AccountName].GetImageAsync(info.File, pictureCancellationTokenSource.Token);
                        if (cn != controlNumber)
                        {
                            return;
                        }
                        Invoke(new MethodInvoker(() =>
                        {
                            label3.Visible = true;
                            widthAndHeightLabel.Visible = true;
                            widthAndHeightLabel.Text = string.Format("{0} x {1}", info.File.DriveFile.ImageWidth, info.File.DriveFile.ImageHeight);
                            pictureBox1.BackgroundImage = image;
                            pictureBox1.Image = null;
                            loadingImageProgressBar.Visible = false;
                            if (widthAndHeightLabel.Text == "0 x 0")
                                widthAndHeightLabel.Text = string.Format("{0} x {1}", image.Width, image.Height);
                        }));
                    }
                    catch (System.OperationCanceledException) { }
                }
                catch (Exception ex)
                {
                    OnError(ex);
                }
            });
        }

        private void OnError(Exception exception)
        {
            if (exception is Oblqo.Core.ConnectionException)
            {
                Invoke(new MethodInvoker(() =>
                {
                    var acc = ((Oblqo.Core.ConnectionException)exception).Account;
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
                catch { }
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
                    taskManager.Add(new CreateFolderTask(accounts[nodeInfo.AccountName], nodeInfo.AccountName,
                        AsyncTask.NormalPriority, null, dialog.DirecotryName, nodeInfo.File) {Tag = node});
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
                bar.Top = splitContainer1.Top + splitContainer1.SplitterDistance + 3;
            }

            loadingImageProgressBar.Left = splitContainer2.SplitterDistance + splitContainer2.SplitterWidth + splitter1.Left + splitter1.Width;
            loadingImageProgressBar.Width = fileInfoPanel.Width - 1;
            loadingImageProgressBar.Top = loadingFileListProgressBar.Top;
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
            if (string.IsNullOrWhiteSpace(info.File.DriveFile.StorageFileId))
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
                if (info.File.DriveFile.StorageFileId == null)
                {
                    taskManager.Add(new SynchronizeFileTask(accounts[info.AccountName],
                        info.AccountName, 0, new AsyncTask[0], info.File.DriveFile, folderInfo.File.StorageFile));
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
            fileListView.ListViewItemSorter = new FileListSorder(fileListView);
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

        private void fileListFilterTextBox_Enter(object sender, EventArgs e)
        {
            if (fileListFilterTextBox.Tag == null)
            {
                fileListFilterTextBox.Text = "";
                fileListFilterTextBox.ForeColor = SystemColors.ControlText;
            }
        }

        private void fileListFilterTextBox_Leave(object sender, EventArgs e)
        {
            if (fileListFilterTextBox.Text == "")
            {
                fileListFilterTextBox.Text = "Filter";
                fileListFilterTextBox.ForeColor = Color.DarkGray;
                fileListFilterTextBox.Tag = null;
            } else
            {
                fileListFilterTextBox.Tag = new object();
            }
        }

        private void fileListFilterTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void fileListFilterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fileListView.Focus();
                UpdateFileList();
            }
        }
    }

}
