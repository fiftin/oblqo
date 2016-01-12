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

namespace Oblqo.Controls
{
    public partial class FileList : UserControl
    {
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

        private FileListStatusBar currentDirectoryInfoPanel;
        private ImageList smallImageList;

        private CancellationTokenSource updateListCancellationTokenSource;
        private readonly object updateListCancellationTokenSourceLocker = new object();

        private readonly Font UnsyncronizedFileItemFont;

        public ImageList SmallImageList
        {
            get
            {
                return smallImageList;
            }
            set
            {
                smallImageList = value;
            }
        }

        public FileListStatusBar CurrentDirectoryInfoPanel
        {
            get
            {
                return currentDirectoryInfoPanel;
            }
            set
            {
                currentDirectoryInfoPanel = value;
            }
        }


        public FileList()
        {
            InitializeComponent();
            UnsyncronizedFileItemFont = new Font(Font, FontStyle.Strikeout);
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
            OpenSelectedFileIfItLocal();
        }


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

        private void fileListView_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                OpenSelectedFileIfItLocal();
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

        #endregion


        public ListView.SelectedListViewItemCollection SelectedItems
        {
            get
            {
                return fileListView.SelectedItems;
            }
        }

        public ListView.ListViewItemCollection Items
        {
            get
            {
                return fileListView.Items;
            }
        }
        
        public void UpdateFileList(NodeInfo info, Account account)
        {
            lock (updateListCancellationTokenSourceLocker)
            {
                updateListCancellationTokenSource?.Cancel();
                updateListCancellationTokenSource = new CancellationTokenSource();
            }
            ICollection<AccountFile> files;
            fileListView.Enabled = false;
            Task.Run(async delegate
            {
                try
                {
                    switch (info.Type)
                    {
                        case NodeType.Account:
                            files = await account.GetFilesAsync(account.RootFolder,
                                updateListCancellationTokenSource.Token);
                            break;
                        case NodeType.Folder:
                            files = await account.GetFilesAsync(info.File,
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

        private void fileListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(sender, e);
        }




        public event EventHandler SelectedIndexChanged;
        public event EventHandler FileLoaded;
        public event EventHandler<ExceptionEventArgs> Error;

    }
}
