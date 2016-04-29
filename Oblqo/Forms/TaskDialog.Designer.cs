namespace Oblqo.Forms
{
    partial class TaskDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskDialog));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tabXml = new System.Windows.Forms.TabPage();
            this.txtXml = new System.Windows.Forms.TextBox();
            this.tabException = new System.Windows.Forms.TabPage();
            this.exceptionVIew1 = new Oblqo.Controls.ExceptionVIew();
            this.txtTaskName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.tabXml.SuspendLayout();
            this.tabException.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabInfo);
            this.tabControl1.Controls.Add(this.tabXml);
            this.tabControl1.Controls.Add(this.tabException);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabInfo
            // 
            resources.ApplyResources(this.tabInfo, "tabInfo");
            this.tabInfo.Controls.Add(this.propertyGrid1);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            resources.ApplyResources(this.propertyGrid1, "propertyGrid1");
            this.propertyGrid1.DisabledItemForeColor = System.Drawing.SystemColors.ControlText;
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // tabXml
            // 
            resources.ApplyResources(this.tabXml, "tabXml");
            this.tabXml.Controls.Add(this.txtXml);
            this.tabXml.Name = "tabXml";
            this.tabXml.UseVisualStyleBackColor = true;
            // 
            // txtXml
            // 
            resources.ApplyResources(this.txtXml, "txtXml");
            this.txtXml.BackColor = System.Drawing.Color.White;
            this.txtXml.Name = "txtXml";
            this.txtXml.ReadOnly = true;
            // 
            // tabException
            // 
            resources.ApplyResources(this.tabException, "tabException");
            this.tabException.Controls.Add(this.exceptionVIew1);
            this.tabException.Name = "tabException";
            this.tabException.UseVisualStyleBackColor = true;
            // 
            // exceptionVIew1
            // 
            resources.ApplyResources(this.exceptionVIew1, "exceptionVIew1");
            this.exceptionVIew1.Exception = null;
            this.exceptionVIew1.Name = "exceptionVIew1";
            // 
            // txtTaskName
            // 
            resources.ApplyResources(this.txtTaskName, "txtTaskName");
            this.txtTaskName.Name = "txtTaskName";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.button1);
            this.panel1.Name = "panel1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // TaskDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtTaskName);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "TaskDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tabControl1.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.tabXml.ResumeLayout(false);
            this.tabXml.PerformLayout();
            this.tabException.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.TabPage tabXml;
        private System.Windows.Forms.TextBox txtXml;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Label txtTaskName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabException;
        private Controls.ExceptionVIew exceptionVIew1;
    }
}