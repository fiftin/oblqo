using System;
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

        private class Resolution
        {
            public Resolution(Size size) : this(size.Width,size.Height) { }
            public Resolution(int w, int h)
            {
                Size = new Size(w, h);
            }
            public Size Size { get; private set; }
            public override string ToString()
            {
                return string.Format("{0} x {1}", Size.Width, Size.Height);
            }

            public override bool Equals(object obj)
            {
                return Size.Equals(obj);
            }

            public override int GetHashCode()
            {
                return Size.GetHashCode();
            }
        }

        private class CamelcaseWrapper
        {
            public object OriginalObject { get; private set; }
            public string ReadableString { get; private set; }
            public CamelcaseWrapper(object obj)
            {
                OriginalObject = obj;
                ReadableString = Common.CamelcaseToHumanReadable(obj.ToString());
            }

            public override string ToString()
            {
                return ReadableString;
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

            regionComboBox.SelectedIndex = 0;

            imageResolutionComboBox.Items.Add(new Resolution(1024, 768));
            imageResolutionComboBox.Items.Add(new Resolution(1280, 1024));
            imageResolutionComboBox.Items.Add(new Resolution(1600, 1200));

            imageResolutionComboBox.SelectedIndex = 2;

            foreach (var driveType in Enum.GetValues(typeof(DriveType)))
            {
                driveKindComboBox.Items.Add(new CamelcaseWrapper(driveType));
            }
            driveKindComboBox.SelectedIndex = 0;
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

        public DriveType DriveType
        {
            get
            {
                return (DriveType)((CamelcaseWrapper)driveKindComboBox.SelectedItem).OriginalObject;
            }
            set
            {
                foreach (CamelcaseWrapper item in driveKindComboBox.Items)
                {
                    if ((DriveType)item.OriginalObject == value)
                    {
                        driveKindComboBox.SelectedItem = item;
                    }
                }
            }
        }

        public Size DriveImageResolution
        {
            get { return ((Resolution) imageResolutionComboBox.SelectedItem).Size; }
            set
            {
                for (var i = 0; i < imageResolutionComboBox.Items.Count; i++)
                {
                    var x = (Resolution)imageResolutionComboBox.Items[i];
                    if (x.Size.Equals(value))
                    {
                        imageResolutionComboBox.SelectedIndex = i;
                        break;
                    }
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

        private void AccountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void driveRootPathBrowseButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = DriveRootPath;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DriveRootPath = folderBrowserDialog1.SelectedPath;
            }
        }

        private void driveKindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            driveRootPathBrowseButton.Enabled = DriveType == DriveType.LocalDrive;
            localDriveTabPage.Text = driveKindComboBox.Text;
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
    }
}
