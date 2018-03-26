using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Amazon;
using Oblqo.Properties;

namespace Oblqo
{
    public partial class AccountForm : Form
    {
        private class RegionInfo
        {
            public string SystemName { get; set; }
            public string DisplayName { get; set; }
            public override string ToString()
            {
                return DisplayName;
            }
        }

        public AccountForm(bool newAccount, bool canCancel = true)
        {
            InitializeComponent();

            cancelButton.Enabled = canCancel;

            Text = newAccount ? Util.GetString("AccountForm_CreateNewAccount") : Util.GetString("AccountForm_ChangeAccount");
            foreach (var region in RegionEndpoint.EnumerableAllRegions)
            {
                regionComboBox.Items.Add(new RegionInfo
                {
                    SystemName = region.SystemName,
                    DisplayName = region.DisplayName
                });
            }
            
            regionComboBox.SelectedIndex = 0;

            if (newAccount)
            {
                driveTabControl.TabPages.Add(new DriveAccountTabPage());
                driveTabControl.TabPages.Remove(addDriveTabPage);
                driveTabControl.TabPages.Add(addDriveTabPage);
            }
            
        }

        public void AddDrives(IEnumerable<DriveInfo> d)
        {
            var driveInfos = d as DriveInfo[] ?? d.ToArray();
            foreach (var page in driveInfos.Select(drive => new DriveAccountTabPage(drive)))
            {
                
                driveTabControl.TabPages.Add(page);
            }
            driveTabControl.TabPages.Remove(addDriveTabPage);
            driveTabControl.TabPages.Add(addDriveTabPage);
        }

        public IEnumerable<DriveInfo> GetDrives()
        {
            return driveTabControl.TabPages.OfType<DriveAccountTabPage>().Select(page => new DriveInfo
            {
                DriveType = page.DriveControl.DriveType,
                DriveRootPath = page.DriveControl.DriveRootPath,
                DriveImageMaxSize = page.DriveControl.DriveImageResolution,
                DriveId = page.DriveId
            }).ToList();
        }

        public string StorageRegionSystemName
        {
            get
            {
                return ((RegionInfo) regionComboBox.SelectedItem).SystemName;
            }
            set
            {
                for (var i = 0; i < regionComboBox.Items.Count; i++)
                {
                    if (((RegionInfo) regionComboBox.Items[i]).SystemName == value)
                        regionComboBox.SelectedIndex = i;
                }
            }
        }
        
        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public string StorageAccessTokenId
        {
            get { return storageAccessKeyIdTextBox.Text; }
            set { storageAccessKeyIdTextBox.Text = value; }
        }

        public string StorageSecretAccessKey
        {
            get { return secretAccessKeyTextBox.Text; }
            set { secretAccessKeyTextBox.Text = value; }
        }

        public string AccountName
        {
            get { return accountNameTextBox.Text; }
            set { accountNameTextBox.Text = value; }
        }

        public string GlacierVault
        {
            get { return glacierVaultTextBox.Text; }
            set { glacierVaultTextBox.Text = value; }
        }
        

        private void AccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.ApplicationExitCall)
            {
                return;
            }
            if (DialogResult == DialogResult.Cancel)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(accountNameTextBox.Text))
            {
                MessageBox.Show(
                    Util.GetString("EmptyConnectionName_Message"),
                    Util.GetString("InvalidConnection_Caption"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                accountNameTextBox.Focus();
            } else if (string.IsNullOrWhiteSpace(storageAccessKeyIdTextBox.Text)
                || string.IsNullOrWhiteSpace(secretAccessKeyTextBox.Text)
                || string.IsNullOrWhiteSpace(glacierVaultTextBox.Text))
            {
                MessageBox.Show(
                    Util.GetString("InvalidArchive_Message"),
                    Util.GetString("InvalidConnection_Caption"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                storageAccessKeyIdTextBox.Focus();
            }
        }

        private void driveTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage != addDriveTabPage)
            {
                return;
            }
            var tab = new DriveAccountTabPage();
            driveTabControl.TabPages.Insert(driveTabControl.TabPages.Count - 1, tab);
            driveTabControl.SelectedTab = tab;
        }

        private void deleteAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            driveTabControl.TabPages.RemoveAt(driveTabControl.SelectedIndex);
        }

        private void driveTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            var p = driveTabControl.PointToClient(Cursor.Position);
            for (var i = 0; i < driveTabControl.TabCount; i++)
            {
                var r = driveTabControl.GetTabRect(i);
                if (r.Contains(p) && driveTabControl.TabPages[i] != addDriveTabPage)
                {
                    driveTabControl.SelectedIndex = i;
                    contextMenuStrip1.Show(Cursor.Position);
                    return;
                }
            }
        }

        private void driveTabControl_MouseClick(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// How to add user for Amazon Glacier
        /// </summary>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/fiftin/oblqo/wiki/How-to-add-user-for-Amazon-Glacier");
        }

        /// <summary>
        /// Amazon Glacier link
        /// </summary>
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://aws.amazon.com/glacier");

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://docs.aws.amazon.com/general/latest/gr/rande.html");
        }

        /// <summary>
        /// Read how to fill Access Key ID / Secret Key
        /// </summary>
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/fiftin/oblqo/wiki/How-configure-connection-with-using-Credentials-file");
        }
    }
}
