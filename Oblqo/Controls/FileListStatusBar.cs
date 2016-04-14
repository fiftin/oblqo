using System;
using System.Drawing;
using System.Windows.Forms;

namespace Oblqo
{
    public partial class FileListStatusBar : UserControl
    {
        private int numberOfFiles;
        private int numberOfUnsyncronizedFiles;
        private string filter = "";

        public bool IsFiltered { get; private set; }

        public FileListStatusBar()
        {
            InitializeComponent();
            fileListFilterTextBox.Text = Util.GetString("FileListStatusBar_Filter");
            fileListNumberOfFilesLabel.Text = string.Format(Util.GetString("FileListStatusBar_Status"), 0, 0);
        }

        public int NumberOfFiles
        {
            get { return numberOfFiles; }
            set
            {
                numberOfFiles = value;
                fileListNumberOfFilesLabel.Text = string.Format(Util.GetString("FileListStatusBar_Status"), numberOfFiles, numberOfUnsyncronizedFiles);
            }
        }

        public int NumberOfUnsyncronizedFiles
        {
            get
            {
                return numberOfUnsyncronizedFiles;
            }
            set
            {
                numberOfUnsyncronizedFiles = value;
                fileListNumberOfFilesLabel.Text = string.Format(Util.GetString("FileListStatusBar_Status"), numberOfFiles, numberOfUnsyncronizedFiles);
            }
        }

        public string Filter
        {
            get
            {
                return filter;
            }
        }

        public bool IsValid(string s)
        {
            if (!IsFiltered)
            {
                return true;
            }
            return s.ToLower().Contains(Filter.ToLower());
        }

        public bool ShowOnlyUnsyncronizedFiles
        {
            get
            {
                return showSyncFilesOnlyCheckbox.Checked;
            }
        }

        private void fileListFilterTextBox_Leave(object sender, EventArgs e)
        {
            if (fileListFilterTextBox.Text == "")
            {
                IsFiltered = false;
                fileListFilterTextBox.ForeColor = Color.DarkGray;
                fileListFilterTextBox.Text = Util.GetString("FileListStatusBar_Filter"); ;
                if (filter != "")
                {
                    filter = "";
                    OnFilterChanged();
                }
            }
            else
            {
                IsFiltered = true;
                if (filter != fileListFilterTextBox.Text)
                {
                    filter = fileListFilterTextBox.Text;
                    OnFilterChanged();
                }
            }

        }

        private void fileListFilterTextBox_Enter(object sender, EventArgs e)
        {
            if (!IsFiltered)
            {
                fileListFilterTextBox.Text = "";
                fileListFilterTextBox.ForeColor = SystemColors.ControlText;
            }
        }

        protected virtual void OnFilterChanged()
        {
            FilterChanged?.Invoke(this, new EventArgs());
        }
        
        private void FileListStatusBar_SizeChanged(object sender, EventArgs e)
        {
            showSyncFilesOnlyCheckbox.Left = fileListNumberOfFilesLabel.Right;
            fileListFilterTextBox.Left = showSyncFilesOnlyCheckbox.Right;
            fileListFilterTextBox.Width = Width - showSyncFilesOnlyCheckbox.Right;
        }

        private void fileListFilterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnFilterChanged();
            }
        }

        private void showSyncFilesOnlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            OnFilterChanged();
        }

        public event EventHandler<EventArgs> FilterChanged;
    }
}
