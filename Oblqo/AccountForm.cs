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

        private readonly List<DriveInfo> drives = new List<DriveInfo>();

        public AccountForm(bool newAccount)
        {
            InitializeComponent();
            Text = newAccount ? Resources.AccountForm_CreateNewAccount : Resources.AccountForm_ChangeAccount;
            foreach (var region in RegionEndpoint.EnumerableAllRegions)
                regionComboBox.Items.Add(new RegionInfo
                {
                    SystemName = region.SystemName,
                    DisplayName = region.DisplayName
                });

            regionComboBox.SelectedIndex = 0;

            if (newAccount)
            {
                driveTabControl.TabPages.Add(new DriveAccountTabPage());
            }

        }

        public void AddDrives(IEnumerable<DriveInfo> d)
        {
            var driveInfos = d as DriveInfo[] ?? d.ToArray();
            this.drives.AddRange(driveInfos);
            foreach (var page in driveInfos.Select(drive => new DriveAccountTabPage(drive)))
            {
                driveTabControl.TabPages.Add(page);
            }
            driveTabControl.TabPages.Remove(addDriveTabPage);
            driveTabControl.TabPages.Add(addDriveTabPage);
        }

        public IEnumerable<DriveInfo> GetDrives()
        {
            return drives;
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
        
        private void AccountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void driveKindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                MessageBox.Show("Connection Name can't be empty", "Illegal configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                accountNameTextBox.Focus();
            } else if (string.IsNullOrWhiteSpace(storageAccessKeyIdTextBox.Text)
                || string.IsNullOrWhiteSpace(secretAccessKeyTextBox.Text)
                || string.IsNullOrWhiteSpace(glacierVaultTextBox.Text))
            {
                MessageBox.Show("Invalid Archive configuration", "Illegal configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        
    }
}
