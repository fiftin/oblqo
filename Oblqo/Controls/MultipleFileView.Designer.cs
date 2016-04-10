namespace Oblqo.Controls
{
    partial class MultipleFileView
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
            this.lblNumberOfFiles = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblNumberOfFiles
            // 
            this.lblNumberOfFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumberOfFiles.Location = new System.Drawing.Point(0, 0);
            this.lblNumberOfFiles.Name = "lblNumberOfFiles";
            this.lblNumberOfFiles.Size = new System.Drawing.Size(225, 159);
            this.lblNumberOfFiles.TabIndex = 0;
            this.lblNumberOfFiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 72);
            this.panel1.TabIndex = 1;
            // 
            // MultipleFileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNumberOfFiles);
            this.Controls.Add(this.panel1);
            this.Name = "MultipleFileView";
            this.Size = new System.Drawing.Size(225, 231);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNumberOfFiles;
        private System.Windows.Forms.Panel panel1;
    }
}
