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
    public partial class DriveAccountControl : UserControl
    {
        public DriveAccountControl()
        {
            InitializeComponent();
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

        public DriveType DriveType
        {
            get
            {
                return (DriveType)((CamelcaseWrapper)driveKindComboBox.SelectedItem).OriginalObject;
            }
            set
            {
                foreach (var item in driveKindComboBox.Items.Cast<CamelcaseWrapper>().Where(item => (DriveType)item.OriginalObject == value))
                {
                    driveKindComboBox.SelectedItem = item;
                }
            }
        }

        public Size DriveImageResolution
        {
            get { return ((Resolution)imageResolutionComboBox.SelectedItem).Size; }
            set
            {
                for (var i = 0; i < imageResolutionComboBox.Items.Count; i++)
                {
                    var x = (Resolution)imageResolutionComboBox.Items[i];
                    if (!x.Size.Equals(value)) continue;
                    imageResolutionComboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        public string DriveRootPath
        {
            get { return driveRootPathTextBox.Text; }
            set { driveRootPathTextBox.Text = value; }
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
            DriveTypeChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler DriveTypeChanged;
    }
}
