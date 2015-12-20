using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oblqo
{
    public partial class FileListStatusBar : UserControl
    {
        private int numberOfFiles;
        private int numberOfUnsyncronizedFiles;

        public bool IsFiltered { get; private set; }

        public FileListStatusBar()
        {
            InitializeComponent();
        }

        public int NumberOfFiles
        {
            get { return numberOfFiles; }
            set
            {
                numberOfFiles = value;
                fileListNumberOfFilesLabel.Text = string.Format("{0} files, {1} unsync", numberOfFiles, numberOfUnsyncronizedFiles);
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
                fileListNumberOfFilesLabel.Text = string.Format("{0} files, {1} unsync", numberOfFiles, numberOfUnsyncronizedFiles);
            }
        }

        public string Filter
        {
            get
            {
                return fileListFilterTextBox.Text;
            }
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
                fileListFilterTextBox.Text = "Filter";
            }
            else
            {
                IsFiltered = true;
            }
            OnFilterChanged();
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
