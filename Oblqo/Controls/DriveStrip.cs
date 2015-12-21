using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oblqo.Controls
{
    public partial class DriveStrip : UserControl
    {
        private AccountFile file;

        public Drive SelectedDrive { get; private set; }

        public event EventHandler SelectedDriveChanged;

        public DriveStrip()
        {
            InitializeComponent();
        }

        [DefaultValue(null), Browsable(false)]
        public AccountFile File
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
                OnFileChanged();
            }
        }

        public DriveFile DriveFile
        {
            get
            {
                return File.GetDriveFile(SelectedDrive);
            }
        }

        protected virtual void OnFileChanged()
        {
            // Adds tabs for new drives
            foreach (var f in file.DriveFiles)
            {
                if (toolStrip1.Items.Find(f.Drive.Id, false).SingleOrDefault() != null)
                {
                    continue; // tab already exists
                }
                ToolStripButton item = new ToolStripButton(f.Drive.ShortName, null, toolStrip1_ButtonClick, name: f.Drive.Id);
                item.DisplayStyle = ToolStripItemDisplayStyle.Text;
                item.Margin = new Padding(1);
                toolStrip1.Items.Add(item);
            }

            // Destroys unused tabs. Selected tab can be deleted. So we need check selected tab. 
            // And if selected tab isn't exists, select first tab.
            bool hasSelectedItem = false;
            for (int i = toolStrip1.Items.Count - 1; i >= 0; i--)
            {
                ToolStripButton item = (ToolStripButton)toolStrip1.Items[i];
                if (file.GetDriveFile(item.Name) == null)
                {
                    toolStrip1.Items.RemoveAt(i);
                }else
                {
                    if (item.Checked)
                    {
                        hasSelectedItem = true;
                    }
                }
            }
            if (!hasSelectedItem)
            {
                ((ToolStripButton)toolStrip1.Items[0]).Checked = true;
                SelectedDrive = File.Account.Drives.FindById(toolStrip1.Items[0].Name);
            }
            OnSelectedDriveChanged();
        }

        /// <summary>
        /// Marks clicked item as checked. Other items marks as unchecked.
        /// </summary>
        private void toolStrip1_ButtonClick(object sender, EventArgs e)
        {
            ToolStripButton item = (ToolStripButton)sender;
            foreach (ToolStripButton x in toolStrip1.Items)
            {
                if (x == item)
                {
                    continue;
                }
                x.Checked = false;
            }
            item.Checked = true;
            SelectedDrive = File.Account.Drives.FindById(item.Name);
            OnSelectedDriveChanged();
        }

        protected virtual void OnSelectedDriveChanged()
        {
            SelectedDriveChanged?.Invoke(this, new EventArgs());
        }
    }
}
