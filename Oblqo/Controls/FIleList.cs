using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using Oblqo.Local;
using System.IO;
using System.Threading;
using Oblqo.Tasks;

namespace Oblqo.Controls
{
    public partial class FileList : UserControl
    {
        private CancellationTokenSource updateListCancellationTokenSource;
        private readonly object updateListCancellationTokenSourceLocker = new object();
        private readonly Font UnsyncronizedFileItemFont;

        public FileList()
        {
            InitializeComponent();
            UnsyncronizedFileItemFont = new Font("Courier New", Font.Size, FontStyle.Strikeout);
            fileListView.ListViewItemSorter = new FileListSorder(fileNameColumnHeader);
            
        }

        #region Event Handlers

        private void fileListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            FileListSorder sorter = (FileListSorder)fileListView.ListViewItemSorter;
            sorter.ToggleOrder(fileListView.Columns[e.Column]);
            fileListView.Sort();
        }

        private void fileListView_DoubleClick(object sender, EventArgs e)
        {
            FileDoubleClick?.Invoke(this, new EventArgs());
        }


        private void fileListView_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    ShowMenu(true);
                    break;
            }
        }

        public void ShowMenu(bool showSelectAll)
        {
            selectAllToolStripMenuItemSeparator.Visible = showSelectAll;
            selectAllToolStripMenuItem.Visible = showSelectAll;

            if (fileListView.SelectedItems.Count > 0)
            {
                foreach (ToolStripItem menuItem in fileMenu.Items)
                {
                    menuItem.Enabled = true;
                }

                // Calc number of items stored in archive among selected items
                // and set menu items states.

                var nSelectedItemsInArchive = 0;
                foreach (ListViewItem item in fileListView.SelectedItems)
                {
                    var info = (NodeInfo)item.Tag;
                    if (info.File.StorageFile != null && info.File.StorageFile.Id != null)
                    {
                        nSelectedItemsInArchive++;
                    }
                }
                if (nSelectedItemsInArchive == 0)
                {
                    downloadFileFromStorageToolStripMenuItem.Enabled = false;
                    synchronizeOnDrivesToolStripMenuItem.Enabled = false;
                    deleteFromArchiveToolStripMenuItem.Enabled = false;
                }
                else if (nSelectedItemsInArchive == fileListView.SelectedItems.Count)
                {
                    synchronizeToolStripMenuItem.Enabled = false;
                }

                if (fileListView.SelectedItems.Count == 1)
                {
                    var nodeInfo = (NodeInfo)fileListView.SelectedItems[0].Tag;
                    if (nodeInfo.File.DriveFiles.FirstOrDefault(x => x.Drive is LocalDrive) == null)
                    {
                        openFileToolStripMenuItem.Enabled = false;
                        openContainingFolderToolStripMenuItem.Enabled = false;
                    }
                }
                else
                {
                    openFileToolStripMenuItem.Enabled = false;
                    openContainingFolderToolStripMenuItem.Enabled = false;
                }

                fileMenu.Show(Cursor.Position);
            }
            else
            {
                if (Account != null)
                {
                    fileListMenu.Show(Cursor.Position);
                }
            }
        }

        private void fileListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FileDoubleClick?.Invoke(this, new EventArgs());
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

        private void downloadFileFromDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (var info in from ListViewItem item in fileListView.SelectedItems select (NodeInfo)item.Tag)
            {
                TaskManager.Add(new DownloadFileFromDriveTask(Account, FolderInfo.AccountName, AsyncTask.NormalPriority, null, info.File, folderBrowserDialog1.SelectedPath));
            }
        }

        private void downloadFileFromStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (var info in from ListViewItem item in fileListView.SelectedItems select (NodeInfo)item.Tag)
            {
                var task = new DownloadFileFromStorageTask(Account, FolderInfo.AccountName, 0, null, info.File, folderBrowserDialog1.SelectedPath);
                TaskManager.Add(task);
            }
        }

        
        private void uploadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadFile();
        }

        private void uploadFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadFolder();
        }

        private void newFolderToolStripButton_Click(object sender, EventArgs e)
        {
            CreateFolder();
        }


        private void synchronizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in fileListView.SelectedItems)
            {
                var info = (NodeInfo)item.Tag;
                if (info.File.DriveFiles.StorageFileId == null)
                {
                    TaskManager.Add(new SynchronizeFileTask(Account,
                        FolderInfo.AccountName, 0, new AsyncTask[0], info.File));
                }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }


        #endregion

        public void UploadFile()
        {

            if (openFileDialog1.ShowDialog() != DialogResult.OK || FolderNode == null)
            {
                return;
            }
            foreach (var fileName in openFileDialog1.FileNames)
            {
                TaskManager.Add(new UploadFileTask(Account, FolderInfo.AccountName, AsyncTask.NormalPriority, null,
                    fileName, FolderInfo.File)
                { Tag = FolderNode });
            }
        }

        public void UploadFolder()
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK || FolderNode == null)
            {
                return;
            }
            TaskManager.Add(new UploadFolderTask(Account,
                FolderInfo.AccountName,
                0,
                null,
                folderBrowserDialog1.SelectedPath,
                FolderInfo.File)
            { Tag = FolderNode });
        }

        public void CreateFolder()
        {
            if (FolderNode == null)
            {
                return;
            }
            using (var dialog = new CreateFolderForm())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var syncDestFolderTask = new SynchronizeDriveEmptyFolderTask(Account,
                        FolderInfo.AccountName, AsyncTask.NormalPriority, null, FolderInfo.File);
                    TaskManager.Add(syncDestFolderTask);
                    TaskManager.Add(new CreateFolderTask(Account, FolderInfo.AccountName,
                        AsyncTask.NormalPriority, new AsyncTask[] { syncDestFolderTask }, dialog.DirecotryName, FolderInfo.File)
                    { Tag = FolderNode });
                }
            }
        }

        public ImageList SmallImageList
        {
            get
            {
                return fileListView.SmallImageList;
            }
            set
            {
                fileListView.SmallImageList = value;
            }
        }

        public FileListStatusBar CurrentDirectoryInfoPanel { get; set; }

        [Browsable(false)]
        public AsyncTaskManager TaskManager { get; set; }

        [Browsable(false)]
        public ListView.SelectedListViewItemCollection SelectedItems
        {
            get
            {
                return fileListView.SelectedItems;
            }
        }

        public void SelectNextFile(SlideDirection direction)
        {
            if (SelectedItems.Count == 0)
            {
                return;
            }
            var item = SelectedItems[0];
            fileListView.SelectedIndices.Clear();
            var index = item.Index;
            if (direction == SlideDirection.Front)
            {
                index++;
                if (index == fileListView.Items.Count - 1)
                {
                    index = 0;
                }
            }
            else
            {
                index--;
                if (index == 0)
                {
                    index = fileListView.Items.Count - 1;
                }
            }
            fileListView.SelectedIndices.Add(index);
        }

        [Browsable(false)]
        public ListView.ListViewItemCollection Items
        {
            get
            {
                return fileListView.Items;
            }
        }

        private NodeInfo FolderInfo => (NodeInfo)FolderNode.Tag;
        private TreeNode FolderNode { get; set; }
        private Account Account { get; set; }

        public void UpdateFileList(TreeNode node, Account account)
        {
            FolderNode = node;
            Account = account;

            lock (updateListCancellationTokenSourceLocker)
            {
                updateListCancellationTokenSource?.Cancel();
                updateListCancellationTokenSource = new CancellationTokenSource();
            }

            if (account == null)
            {
                fileListView.Items.Clear();
                CurrentDirectoryInfoPanel.NumberOfFiles = 0;
                CurrentDirectoryInfoPanel.NumberOfUnsyncronizedFiles = 0;
                return;
            }

            ICollection<AccountFile> files;
            fileListView.Enabled = false;
            Task.Run(async delegate
            {
                try
                {
                    switch (FolderInfo.Type)
                    {
                        case NodeType.Account:
                            files = await Account.GetFilesAsync(Account.RootFolder,
                                updateListCancellationTokenSource.Token);
                            break;
                        case NodeType.Folder:
                            files = await Account.GetFilesAsync(FolderInfo.File,
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
                        var fileState = Util.GetFileState(file);
                        if (!CurrentDirectoryInfoPanel.IsValid(file.Name))
                        {
                            continue;
                        }
                        if ((fileState & AccountFileStates.UnsyncronizedWithStorage) != 0)
                        {
                            numberOfUnsyncFiles++;
                        }
                        else if (CurrentDirectoryInfoPanel.ShowOnlyUnsyncronizedFiles)
                        {
                            continue;
                        }
                        numberOfFiles++;
                        AddFile(file, FolderInfo.AccountName);
                    }
                    CurrentDirectoryInfoPanel.NumberOfFiles = numberOfFiles;
                    CurrentDirectoryInfoPanel.NumberOfUnsyncronizedFiles = numberOfUnsyncFiles;
                    fileListView.Enabled = true;
                    OnFileLoaded();
                }));
            });
        }

        private void OnFileLoaded()
        {
            FileLoaded?.Invoke(this, new EventArgs());
        }

        private void OnError(Exception ex)
        {
            Error?.Invoke(this, new ExceptionEventArgs(ex));
        }

        public void UpdateFileListItem(AccountFileStates newFileState, ListViewItem fileItem)
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

        public void AddFile(AccountFile file, string accountName)
        {
            var fileState = Util.GetFileState(file);
            var key = "file";
            if (!string.IsNullOrWhiteSpace(file.MimeType))
            {
                var mimeTypeParts = file.MimeType.Split('/');
                if (mimeTypeParts.Length == 2)
                {
                    var tmpKey = string.Format("file_{0}_{1}", mimeTypeParts[0], mimeTypeParts[1]);
                    if (SmallImageList.Images.ContainsKey(tmpKey))
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

        private void fileListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(sender, e);
        }


        public event EventHandler FileDoubleClick;
        public event EventHandler SelectedIndexChanged;
        public event EventHandler FileLoaded;
        public event EventHandler<ExceptionEventArgs> Error;

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedFileIfItLocal();
        }


        public void OpenSelectedFileIfItLocal()
        {
            if (SelectedItems.Count > 0)
            {
                var selectedFile = SelectedItems[0];
                var nodeInfo = (NodeInfo)selectedFile.Tag;
                var localFile = (LocalFile)nodeInfo.File.DriveFiles.FirstOrDefault(x => x.Drive is LocalDrive);
                if (localFile != null)
                {
                    System.Diagnostics.Process.Start(localFile.FullName);
                }
            }
        }
        private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedFileContainingFolderIfItLocal();
        }

        private void synchronizeOnDrivesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var syncFolderTask = new SynchronizeDriveEmptyFolderTask(Account,
                FolderInfo.AccountName, 0, new AsyncTask[0], FolderInfo.File);
            TaskManager.Add(syncFolderTask);
            foreach (ListViewItem item in fileListView.SelectedItems)
            {
                var info = (NodeInfo)item.Tag;
                if (info.File.DriveFiles.Count < Account.Drives.Count)
                {
                    TaskManager.Add(new SynchronizeDriveFileTask(Account, info.AccountName, 0, new AsyncTask[] { syncFolderTask }, info.File));
                }
            }
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Util.GetString("DeleteFileFromArchiveAndDrives_Message"),
                                Util.GetString("DeleteFileFromArchiveAndDrives_Caption"),
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            foreach (ListViewItem item in fileListView.SelectedItems)
            {
                var info = (NodeInfo)item.Tag;
                TaskManager.Add(new DeleteFileTask(Account, info.AccountName, 0, null, info.File) { Tag = item });
            }
        }

        private void deleteFromArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Util.GetString("DeleteFileFromArchive_Message"),
                                Util.GetString("DeleteFileFromArchive_Caption"),
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            foreach (ListViewItem item in fileListView.SelectedItems)
            {
                var info = (NodeInfo)item.Tag;
                TaskManager.Add(new DeleteFileFromArchiveTask(Account, info.AccountName, 0, null, info.File) { Tag = item });
            }

        }
    }
}
