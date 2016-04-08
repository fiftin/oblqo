namespace Oblqo.Controls
{
    partial class ImageViewer
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.picImage = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnBack = new System.Windows.Forms.PictureBox();
            this.btnFront = new System.Windows.Forms.PictureBox();
            this.lblFileName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFront)).BeginInit();
            this.SuspendLayout();
            // 
            // picImage
            // 
            this.picImage.BackColor = System.Drawing.Color.Black;
            this.picImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picImage.Location = new System.Drawing.Point(0, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(738, 362);
            this.picImage.TabIndex = 1;
            this.picImage.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnBack, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnFront, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblFileName, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 362);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(738, 76);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::Oblqo.Properties.Resources.back;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBack.Location = new System.Drawing.Point(322, 29);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(44, 44);
            this.btnBack.TabIndex = 0;
            this.btnBack.TabStop = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnFront
            // 
            this.btnFront.BackgroundImage = global::Oblqo.Properties.Resources.front;
            this.btnFront.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFront.Location = new System.Drawing.Point(372, 29);
            this.btnFront.Name = "btnFront";
            this.btnFront.Size = new System.Drawing.Size(44, 44);
            this.btnFront.TabIndex = 1;
            this.btnFront.TabStop = false;
            this.btnFront.Click += new System.EventHandler(this.btnFront_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblFileName, 4);
            this.lblFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFileName.Location = new System.Drawing.Point(3, 0);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(732, 26);
            this.lblFileName.TabIndex = 2;
            this.lblFileName.Text = "label1";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picImage);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ImageViewer";
            this.Size = new System.Drawing.Size(738, 438);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFront)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox btnBack;
        private System.Windows.Forms.PictureBox btnFront;
        private System.Windows.Forms.Label lblFileName;
    }
}
