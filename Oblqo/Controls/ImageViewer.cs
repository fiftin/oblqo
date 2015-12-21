using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo.Controls
{
    public partial class ImageViewer : Component
    {
        public ImageViewer()
        {
            InitializeComponent();
        }

        public ImageViewer(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        public Image Picture
        {
            get
            {
                return null;
            }
            set
            {

            }
        }

        public event EventHandler Back;
        public event EventHandler Front;

    }
}
