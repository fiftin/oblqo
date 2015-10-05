using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblqo
{
    public class DriveInfo
    {
        public DriveType DriveType { get; set; }

        public string DriveLogin { get; set; }

        public string DriveAppPassword { get; set; }

        public string DriveRootPath { get; set; }

        public Size DriveImageMaxSize { get; set; }
    }
}
