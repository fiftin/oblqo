﻿using System;
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

            imageResolutionComboBox.Items.Add(new Dimensions(1024, 768));
            imageResolutionComboBox.Items.Add(new Dimensions(1280, 1024));
            imageResolutionComboBox.Items.Add(new Dimensions(1600, 1200));
            imageResolutionComboBox.Items.Add(new Dimensions(Size.Empty));
            imageResolutionComboBox.SelectedIndex = 3;

            foreach (var driveType in Enum.GetValues(typeof(DriveType)))
            {
                driveKindComboBox.Items.Add(driveType);
            }
        }

        public DriveType DriveType
        {
            get
            {
                if (driveKindComboBox.SelectedIndex == -1)
                {
                    return DriveType.LocalDrive;
                }
                return (DriveType)driveKindComboBox.SelectedItem;
            }
            set
            {
                foreach (var item in driveKindComboBox.Items.Cast<DriveType>().Where(item => ((DriveType)item) == value))
                {
                    driveKindComboBox.SelectedItem = item;
                }
            }
        }

        public Size DriveImageResolution
        {
            get { return ((Dimensions)imageResolutionComboBox.SelectedItem).Size; }
            set
            {
                for (var i = 0; i < imageResolutionComboBox.Items.Count; i++)
                {
                    var x = (Dimensions)imageResolutionComboBox.Items[i];
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
