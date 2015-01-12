namespace Oblakoo
{
    partial class AccountForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.accountNameTextBox = new System.Windows.Forms.TextBox();
            this.storageAccessKeyIdTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.secretAccessKeyTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.glacierVaultTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.driveRootPathTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.yandexDriveRadioButton = new System.Windows.Forms.RadioButton();
            this.oneDriveRadioButton = new System.Windows.Forms.RadioButton();
            this.googleDriveRadioButton = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.imageResolutionComboBox = new System.Windows.Forms.ComboBox();
            this.createVaultCheckBox = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(361, 381);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 29);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(280, 381);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 29);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Account Name:";
            // 
            // accountNameTextBox
            // 
            this.accountNameTextBox.Location = new System.Drawing.Point(125, 18);
            this.accountNameTextBox.Name = "accountNameTextBox";
            this.accountNameTextBox.Size = new System.Drawing.Size(288, 20);
            this.accountNameTextBox.TabIndex = 3;
            // 
            // storageAccessKeyIdTextBox
            // 
            this.storageAccessKeyIdTextBox.Location = new System.Drawing.Point(113, 23);
            this.storageAccessKeyIdTextBox.Name = "storageAccessKeyIdTextBox";
            this.storageAccessKeyIdTextBox.Size = new System.Drawing.Size(283, 20);
            this.storageAccessKeyIdTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Access Key ID:";
            // 
            // secretAccessKeyTextBox
            // 
            this.secretAccessKeyTextBox.Location = new System.Drawing.Point(113, 49);
            this.secretAccessKeyTextBox.Name = "secretAccessKeyTextBox";
            this.secretAccessKeyTextBox.Size = new System.Drawing.Size(283, 20);
            this.secretAccessKeyTextBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Secret Access Key:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.createVaultCheckBox);
            this.groupBox1.Controls.Add(this.regionComboBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.glacierVaultTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.secretAccessKeyTextBox);
            this.groupBox1.Controls.Add(this.storageAccessKeyIdTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(17, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 133);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Amazon Glacier Account";
            // 
            // regionComboBox
            // 
            this.regionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(113, 100);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(283, 21);
            this.regionComboBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Region:";
            // 
            // glacierVaultTextBox
            // 
            this.glacierVaultTextBox.Location = new System.Drawing.Point(113, 75);
            this.glacierVaultTextBox.Name = "glacierVaultTextBox";
            this.glacierVaultTextBox.Size = new System.Drawing.Size(220, 20);
            this.glacierVaultTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Vault:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.imageResolutionComboBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.driveRootPathTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.yandexDriveRadioButton);
            this.groupBox2.Controls.Add(this.oneDriveRadioButton);
            this.groupBox2.Controls.Add(this.googleDriveRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(17, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(419, 159);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Drive Account";
            // 
            // driveRootPathTextBox
            // 
            this.driveRootPathTextBox.Location = new System.Drawing.Point(113, 91);
            this.driveRootPathTextBox.Name = "driveRootPathTextBox";
            this.driveRootPathTextBox.Size = new System.Drawing.Size(220, 20);
            this.driveRootPathTextBox.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Root Path:";
            // 
            // yandexDriveRadioButton
            // 
            this.yandexDriveRadioButton.AutoSize = true;
            this.yandexDriveRadioButton.Enabled = false;
            this.yandexDriveRadioButton.Location = new System.Drawing.Point(9, 67);
            this.yandexDriveRadioButton.Name = "yandexDriveRadioButton";
            this.yandexDriveRadioButton.Size = new System.Drawing.Size(89, 17);
            this.yandexDriveRadioButton.TabIndex = 2;
            this.yandexDriveRadioButton.Text = "Yandex Drive";
            this.yandexDriveRadioButton.UseVisualStyleBackColor = true;
            // 
            // oneDriveRadioButton
            // 
            this.oneDriveRadioButton.AutoSize = true;
            this.oneDriveRadioButton.Enabled = false;
            this.oneDriveRadioButton.Location = new System.Drawing.Point(9, 46);
            this.oneDriveRadioButton.Name = "oneDriveRadioButton";
            this.oneDriveRadioButton.Size = new System.Drawing.Size(70, 17);
            this.oneDriveRadioButton.TabIndex = 1;
            this.oneDriveRadioButton.Text = "OneDrive";
            this.oneDriveRadioButton.UseVisualStyleBackColor = true;
            // 
            // googleDriveRadioButton
            // 
            this.googleDriveRadioButton.AutoSize = true;
            this.googleDriveRadioButton.Checked = true;
            this.googleDriveRadioButton.Location = new System.Drawing.Point(9, 23);
            this.googleDriveRadioButton.Name = "googleDriveRadioButton";
            this.googleDriveRadioButton.Size = new System.Drawing.Size(87, 17);
            this.googleDriveRadioButton.TabIndex = 0;
            this.googleDriveRadioButton.TabStop = true;
            this.googleDriveRadioButton.Text = "Google Drive";
            this.googleDriveRadioButton.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Image resolution:";
            // 
            // imageResolutionComboBox
            // 
            this.imageResolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageResolutionComboBox.FormattingEnabled = true;
            this.imageResolutionComboBox.Location = new System.Drawing.Point(113, 121);
            this.imageResolutionComboBox.Name = "imageResolutionComboBox";
            this.imageResolutionComboBox.Size = new System.Drawing.Size(121, 21);
            this.imageResolutionComboBox.TabIndex = 15;
            // 
            // createVaultCheckBox
            // 
            this.createVaultCheckBox.AutoSize = true;
            this.createVaultCheckBox.Location = new System.Drawing.Point(339, 77);
            this.createVaultCheckBox.Name = "createVaultCheckBox";
            this.createVaultCheckBox.Size = new System.Drawing.Size(57, 17);
            this.createVaultCheckBox.TabIndex = 12;
            this.createVaultCheckBox.Text = "Create";
            this.createVaultCheckBox.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(339, 93);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(57, 17);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Create";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // AccountForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(448, 422);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.accountNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Account";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox accountNameTextBox;
        private System.Windows.Forms.TextBox storageAccessKeyIdTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox secretAccessKeyTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton yandexDriveRadioButton;
        private System.Windows.Forms.RadioButton oneDriveRadioButton;
        private System.Windows.Forms.RadioButton googleDriveRadioButton;
        private System.Windows.Forms.TextBox glacierVaultTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox driveRootPathTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.ComboBox imageResolutionComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox createVaultCheckBox;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}