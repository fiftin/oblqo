using Oblqo.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oblqo
{
    public partial class FileTabControl : TabControl
    {
        private NodeInfo node;

        public FileTabControl()
        {
            InitializeComponent();
        }

        public NodeInfo Node
        {
            get
            {
                return node;
            }
            set
            {
                node = value;
                OnNodeChanged();
            }
        }

        protected virtual void OnNodeChanged()
        {
            UpdateTabPages();
        }
        
        public void UpdateTabPages()
        {
            int nTabs = TabPages.Count;
            int nNodes = node.File.DriveFiles.Count;
            for (int i = 0; i < nNodes; i++)
            {
                if (i < nTabs)
                {
                    ((DriveFileTabPage)TabPages[i]).File = node.File.DriveFiles[i];
                    TabPages[i].Show();
                }
                else
                {
                    var newTab = new DriveFileTabPage(node.File.DriveFiles[i]);
                    TabPages.Add(newTab);
                }
            }

            for (int i = nNodes; i < nTabs; i++)
            {
                TabPages[i].Hide();
            }

        }

        private void FileTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            ((DriveFileTabPage)e.TabPage).UpdateContent();
        }
    }
}
