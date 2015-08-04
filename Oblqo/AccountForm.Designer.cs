namespace Oblqo
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
            this.label9 = new System.Windows.Forms.Label();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.glacierVaultTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.imageResolutionComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.driveRootPathTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.driveKindComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(421, 409);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(87, 31);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(327, 409);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(87, 31);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Account Name:";
            // 
            // accountNameTextBox
            // 
            this.accountNameTextBox.Location = new System.Drawing.Point(152, 19);
            this.accountNameTextBox.Name = "accountNameTextBox";
            this.accountNameTextBox.Size = new System.Drawing.Size(329, 20);
            this.accountNameTextBox.TabIndex = 3;
            // 
            // storageAccessKeyIdTextBox
            // 
            this.storageAccessKeyIdTextBox.Location = new System.Drawing.Point(132, 57);
            this.storageAccessKeyIdTextBox.Name = "storageAccessKeyIdTextBox";
            this.storageAccessKeyIdTextBox.Size = new System.Drawing.Size(329, 20);
            this.storageAccessKeyIdTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Access Key ID:";
            // 
            // secretAccessKeyTextBox
            // 
            this.secretAccessKeyTextBox.Location = new System.Drawing.Point(132, 85);
            this.secretAccessKeyTextBox.Name = "secretAccessKeyTextBox";
            this.secretAccessKeyTextBox.Size = new System.Drawing.Size(329, 20);
            this.secretAccessKeyTextBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Secret Access Key:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.regionComboBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.glacierVaultTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.secretAccessKeyTextBox);
            this.groupBox1.Controls.Add(this.storageAccessKeyIdTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(20, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(489, 182);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Archive account";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(7, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(196, 14);
            this.label9.TabIndex = 17;
            this.label9.Text = "Amazon Glacier only support";
            // 
            // regionComboBox
            // 
            this.regionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(132, 140);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(329, 22);
            this.regionComboBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 14);
            this.label5.TabIndex = 10;
            this.label5.Text = "Region:";
            // 
            // glacierVaultTextBox
            // 
            this.glacierVaultTextBox.Location = new System.Drawing.Point(132, 113);
            this.glacierVaultTextBox.Name = "glacierVaultTextBox";
            this.glacierVaultTextBox.Size = new System.Drawing.Size(329, 20);
            this.glacierVaultTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "Vault:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.imageResolutionComboBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.driveRootPathTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.driveKindComboBox);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(20, 251);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(489, 143);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Drive Account";
            // 
            // imageResolutionComboBox
            // 
            this.imageResolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageResolutionComboBox.FormattingEnabled = true;
            this.imageResolutionComboBox.Location = new System.Drawing.Point(132, 98);
            this.imageResolutionComboBox.Name = "imageResolutionComboBox";
            this.imageResolutionComboBox.Size = new System.Drawing.Size(140, 22);
            this.imageResolutionComboBox.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 14);
            this.label7.TabIndex = 14;
            this.label7.Text = "Image Resolution:";
            // 
            // driveRootPathTextBox
            // 
            this.driveRootPathTextBox.Location = new System.Drawing.Point(132, 66);
            this.driveRootPathTextBox.Name = "driveRootPathTextBox";
            this.driveRootPathTextBox.Size = new System.Drawing.Size(329, 20);
            this.driveRootPathTextBox.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "Root Path:";
            // 
            // driveKindComboBox
            // 
            this.driveKindComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.driveKindComboBox.FormattingEnabled = true;
            this.driveKindComboBox.Items.AddRange(new object[] {
            "Google Drive",
            "Local Drive"});
            this.driveKindComboBox.Location = new System.Drawing.Point(132, 27);
            this.driveKindComboBox.Name = "driveKindComboBox";
            this.driveKindComboBox.Size = new System.Drawing.Size(140, 22);
            this.driveKindComboBox.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 14);
            this.label10.TabIndex = 17;
            this.label10.Text = "Drive Type:";
            // 
            // AccountForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(523, 453);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.accountNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Account";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AccountForm_FormClosed);
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
        private System.Windows.Forms.TextBox glacierVaultTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox driveRootPathTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.ComboBox imageResolutionComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox driveKindComboBox;
        private System.Windows.Forms.Label label10;
    }
}