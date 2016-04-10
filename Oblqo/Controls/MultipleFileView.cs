using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Oblqo.Controls
{
    public partial class MultipleFileView : UserControl
    {
        private ICollection items;

        public MultipleFileView()
        {
            InitializeComponent();
        }

        [DefaultValue(null)]
        public ICollection Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                lblNumberOfFiles.Text = string.Format("{0} files selected", items.Count);
            }
        }
    }
}
