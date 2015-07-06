using System;
using System.Drawing;
using System.Linq;
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
            imageResolutionComboBox.Items.Add(new Resolution(2048, 1536));
            imageResolutionComboBox.Items.Add(new Resolution(3200, 2400));
            imageResolutionComboBox.Items.Add(new Resolution(6400, 4800));

            imageResolutionComboBox.SelectedIndex = 2;
            
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

        public DriveType DriveType
        {
            get
            {
                return DriveType.GoogleDrive;
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

        private void AccountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

    }
}
