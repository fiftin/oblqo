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

        private readonly AccountCollection accounts = new AccountCollection();
        private readonly AccountManager accountManager;
        private readonly AsyncTaskManager taskManager = new AsyncTaskManager(new IsolatedConfigurationStorage());
        private readonly List<TreeNode> loadingNodes = new List<TreeNode>();
        private ExceptionForm exceptionForm;
        private int loadingFolderImageAngle;
        private int indicateErrorNo;

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

            fileListView.TaskManager = taskManager;
            taskListView.TaskManager = taskManager;

            taskManager.TaskStateChanged += taskManager_TaskStateChanged;
            taskManager.Exception += xxx_Exception;
            InitUI();
            splitContainer2.SplitterWidth = 7;
            btnNewConnection.Visible = accountManager.Accounts.Count() == 0;
        }

        void xxx_Exception(object sender, ExceptionEventArgs e)
        {
            OnError(e.Exception);
        }

        private void InitUI()
        {
            foreach (var x in accountManager.Accounts)
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
            catch (Exception)
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

        private void UpdateFileList()
        {
            var node = treeView1.SelectedNode;
            if (node == null) return;
            HideFileInfoPanel();
            var info = (NodeInfo)node.Tag;
            Account account;
            if (!accounts.TryGetValue(info.AccountName, out account))
                return;
            loadingFileListProgressBar.Visible = true;
            fileListView.UpdateFileList(node, account);
        }

        private void UpdateNode(TreeNode node, bool extendNodeAfterUpdate = false, bool updateList = false)
        {
            var token = new CancellationToken();
            var info = (NodeInfo)node.Tag;
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

        private void taskManager_TaskStateChanged(object sender, AsyncTaskEventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
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

                        if (file.IsFile)
                        {
                            if (attr.NewState == AccountFileStates.New)
                            {
                                fileListView.AddFile(file, accounts.GetName(file.Account));
                            }
                            else
                            {
                                foreach (ListViewItem x in fileListView.Items)
                                {
                                    var info = (NodeInfo)x.Tag;
                                    if (info.File == file)
                                    {
                                        fileListView.UpdateFileListItem(attr.NewState, x);

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
                                foreach (var x in Oblqo.Controls.ControlUtil.GetAllTreeViewNodes(treeView1))
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
                                foreach (var x in Oblqo.Controls.ControlUtil.GetAllTreeViewNodes(treeView1))
                                {
                                    var info = (NodeInfo)x.Tag;
                                    if (info.File == file)
                                    {
                                        Oblqo.Controls.ControlUtil.UpdateTeeViewNode(attr.NewState, x);
                                    }
                                }
                            }
                        }
                    }
                }

                switch (e.Task.State)
                {
                    case AsyncTaskState.Completed:
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
            var nodeInfo = (NodeInfo)e.Node.Tag;
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
            var info = (NodeInfo)e.Node.Tag;
            if (info.Type != NodeType.Folder) return;
            if (e.Node.Nodes[0].Name == "")
                UpdateNode(e.Node);
        }

        private void fileListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileListView.SelectedItems.Count == 0 || fileListView.SelectedItems.Count > 1)
            {
                return;
            }
            ShowFileInfoPanel();
            var info = (NodeInfo)fileListView.SelectedItems[0].Tag;
            driveStrip1.File = info.File;
        }

        private void OnError(Exception exception)
        {
            if (exception is ConnectionException)
            {
                Invoke(new MethodInvoker(() =>
                {
                    var acc = ((ConnectionException)exception).Account;
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
            var nodeInfo = (NodeInfo)node.Tag;
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
            fileListView.UploadFile();
        }

        private void uploadFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileListView.UploadFolder();
        }

        private void newFolderToolStripButton_Click(object sender, EventArgs e)
        {
            fileListView.CreateFolder();
        }

        #region Recalculate Layout

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

        #endregion

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
            fileListView.Clear();
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
            if (e.Button != MouseButtons.Right)
                return;
            if (logListView.SelectedItems.Count > 0)
                logMenu.Show(Cursor.Position);
        }

        private void logListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (exceptionForm == null)
                return; var selectedItem = logListView.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
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

        private void currentDirectoryInfoPanel_FilterChanged(object sender, EventArgs e)
        {
            fileListView.Focus();
            UpdateFileList();
        }

        private void fileInfoPanel_ImageLoading(object sender, EventArgs e)
        {
            loadingImageProgressBar.Visible = true;
            driveStrip1.Visible = false;
            UpdateImageViewer(loading: true);
        }

        private void fileInfoPanel_ImageLoaded(object sender, EventArgs e)
        {
            loadingImageProgressBar.Visible = false;
            driveStrip1.Visible = true;
            UpdateImageViewer();
        }

        private void driveStrip1_SelectedDriveChanged(object sender, EventArgs e)
        {
            fileInfoPanel.DriveFile = driveStrip1.DriveFile;
        }

        private void aboutStripButton_Click(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
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

        private async void clearAuthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null)
            {
                return;
            }
            var nodeInfo = (NodeInfo)node.Tag;
            await accountManager.ClearAuthAsync(nodeInfo.AccountInfo);
        }

        private void fileListView_SizeChanged(object sender, EventArgs e)
        {
            btnNewConnection.Left = fileListView.Width / 4 - btnNewConnection.Width / 4;
            btnNewConnection.Top = fileListView.Height / 3;
        }

        private void fileListView_FileLoaded(object sender, EventArgs e)
        {
            loadingFileListProgressBar.Visible = false;
        }

        private void fileInfoPanel_ZoomClicked(object sender, EventArgs e)
        {
            UpdateImageViewer();
            imageViewer1.Bounds = ClientRectangle;
            imageViewer1.Show();
            imageViewer1.BringToFront();
        }

        public void UpdateImageViewer(bool loading = false)
        {
            if (loading)
            {
                imageViewer1.Picture = Resources.loading;
            }
            else
            {
                imageViewer1.Picture = fileInfoPanel.Picture;
            }
            imageViewer1.FileName = fileInfoPanel.FileName;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            imageViewer1.Bounds = ClientRectangle;
        }

        private void imageViewer1_SlideBack(object sender, EventArgs e)
        {
            fileListView.SelectPrev();
        }

        private void imageViewer1_SlideFront(object sender, EventArgs e)
        {
            fileListView.SelectNext();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                imageViewer1.Hide();
            }
            else if (e.KeyCode == Keys.Left)
            {
                fileListView.SelectPrev();
            }
            else if (e.KeyCode == Keys.Right)
            {
                fileListView.SelectNext();
            }
        }
    }
}
