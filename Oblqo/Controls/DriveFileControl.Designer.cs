namespace Oblqo.Controls
{
    partial class DriveFileControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.filePropertiesPanel = new System.Windows.Forms.Panel();
            this.filePropertiesTable = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.fileSizeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.widthAndHeightLabel = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.filePropertiesPanel.SuspendLayout();
            this.filePropertiesTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(3);
            this.pictureBox1.Size = new System.Drawing.Size(297, 227);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // filePropertiesPanel
            // 
            this.filePropertiesPanel.Controls.Add(this.filePropertiesTable);
            this.filePropertiesPanel.Controls.Add(this.fileNameLabel);
            this.filePropertiesPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.filePropertiesPanel.Location = new System.Drawing.Point(0, 227);
            this.filePropertiesPanel.Name = "filePropertiesPanel";
            this.filePropertiesPanel.Size = new System.Drawing.Size(297, 87);
            this.filePropertiesPanel.TabIndex = 5;
            // 
            // filePropertiesTable
            // 
            this.filePropertiesTable.ColumnCount = 2;
            this.filePropertiesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.filePropertiesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filePropertiesTable.Controls.Add(this.label1, 0, 0);
            this.filePropertiesTable.Controls.Add(this.fileSizeLabel, 1, 0);
            this.filePropertiesTable.Controls.Add(this.label3, 0, 1);
            this.filePropertiesTable.Controls.Add(this.widthAndHeightLabel, 1, 1);
            this.filePropertiesTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.filePropertiesTable.Location = new System.Drawing.Point(0, 33);
            this.filePropertiesTable.Name = "filePropertiesTable";
            this.filePropertiesTable.RowCount = 2;
            this.filePropertiesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.filePropertiesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.filePropertiesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.filePropertiesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.filePropertiesTable.Size = new System.Drawing.Size(297, 48);
            this.filePropertiesTable.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Size:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fileSizeLabel
            // 
            this.fileSizeLabel.AutoSize = true;
            this.fileSizeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileSizeLabel.Location = new System.Drawing.Point(81, 0);
            this.fileSizeLabel.Name = "fileSizeLabel";
            this.fileSizeLabel.Size = new System.Drawing.Size(213, 23);
            this.fileSizeLabel.TabIndex = 1;
            this.fileSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Dimensions:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // widthAndHeightLabel
            // 
            this.widthAndHeightLabel.AutoSize = true;
            this.widthAndHeightLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.widthAndHeightLabel.Location = new System.Drawing.Point(81, 23);
            this.widthAndHeightLabel.Name = "widthAndHeightLabel";
            this.widthAndHeightLabel.Size = new System.Drawing.Size(213, 25);
            this.widthAndHeightLabel.TabIndex = 3;
            this.widthAndHeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.fileNameLabel.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileNameLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fileNameLabel.Location = new System.Drawing.Point(0, 0);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(297, 33);
            this.fileNameLabel.TabIndex = 1;
            this.fileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DriveFileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.filePropertiesPanel);
            this.Name = "DriveFileControl";
            this.Size = new System.Drawing.Size(297, 314);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.filePropertiesPanel.ResumeLayout(false);
            this.filePropertiesTable.ResumeLayout(false);
            this.filePropertiesTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel filePropertiesPanel;
        private System.Windows.Forms.TableLayoutPanel filePropertiesTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fileSizeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label widthAndHeightLabel;
        private System.Windows.Forms.Label fileNameLabel;
    }
}
