using System;
using System.Collections.Generic;
using System.Drawing;

namespace Oblqo
{
    /// <summary>
    /// Serializable drive info stored in account config file.
    /// </summary>
    public class DriveInfo
    {
        public DriveType DriveType { get; set; }

        public string DriveLogin { get; set; }

        public string DriveAppPassword { get; set; }

        public string DriveRootPath { get; set; }

        public Size DriveImageMaxSize { get; set; }

        public string DriveId { get; set; }
    }
}
