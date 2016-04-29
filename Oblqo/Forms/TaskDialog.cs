using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oblqo.Forms
{
    public partial class TaskDialog : Form
    {
        private AsyncTask task;

        public TaskDialog()
        {
            InitializeComponent();
        }

        public AsyncTask Task
        {
            get
            {
                return task;
            }
            set
            {
                task = value;
                txtTaskName.Text = Util.GetString(task.GetType().Name);
                txtXml.Text = task.ToXml().ToString();
                propertyGrid1.SelectedObject = task;
            }
        }
    }
}
