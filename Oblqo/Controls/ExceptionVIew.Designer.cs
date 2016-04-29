namespace Oblqo.Controls
{
    partial class ExceptionVIew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionVIew));
            this.panel1 = new System.Windows.Forms.Panel();
            this.callStackTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.typeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.callStackTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.messageTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.typeTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Name = "panel1";
            // 
            // callStackTextBox
            // 
            resources.ApplyResources(this.callStackTextBox, "callStackTextBox");
            this.callStackTextBox.Name = "callStackTextBox";
            this.callStackTextBox.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // messageTextBox
            // 
            resources.ApplyResources(this.messageTextBox, "messageTextBox");
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // typeTextBox
            // 
            resources.ApplyResources(this.typeTextBox, "typeTextBox");
            this.typeTextBox.Name = "typeTextBox";
            this.typeTextBox.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ExceptionVIew
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ExceptionVIew";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox callStackTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox typeTextBox;
        private System.Windows.Forms.Label label1;
    }
}
