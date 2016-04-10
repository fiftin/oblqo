using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oblqo
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            lblVersion.Text += fvi.FileVersion;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            Process.Start("https://github.com/fiftin");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            Process.Start("http://fiftin.github.io/oblqo");
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, "https://github.com/fiftin");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, "http://fiftin.github.io/oblqo");
        }
    }
}
