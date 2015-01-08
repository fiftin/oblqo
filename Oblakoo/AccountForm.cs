using System;
using System.Windows.Forms;
using Amazon;
using Oblakoo.Properties;

namespace Oblakoo
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

        public DriveType DriveType
        {
            get
            {
                if (googleDriveRadioButton.Checked)
                    return DriveType.GoogleDrive;
                if (oneDriveRadioButton.Checked)
                    return DriveType.OneDrive;
                if (yandexDriveRadioButton.Checked)
                    return DriveType.YandexDrive;
                throw new Exception();
            }
            set
            {
                switch (value)
                {
                    case DriveType.GoogleDrive:
                        googleDriveRadioButton.Checked = true;
                        break;
                    case DriveType.OneDrive:
                        oneDriveRadioButton.Checked = true;
                        break;
                    case DriveType.YandexDrive:
                        yandexDriveRadioButton.Checked = true;
                        break;
                }
            }
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

        public string DriveRootPath
        {
            get { return driveRootPathTextBox.Text; }
            set { driveRootPathTextBox.Text = value; }
        }
        public string GlacierVault
        {
            get { return glacierVaultTextBox.Text; }
            set { glacierVaultTextBox.Text = value; }
        }

    }
}
