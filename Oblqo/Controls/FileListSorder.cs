using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oblqo.Controls
{
    public class FileListSorder : IComparer
    {
        public const int ASC_ORDER = 1;
        public const int DESC_ORDER = -1;

        private int order;
        private ColumnHeader column;

        public FileListSorder(ColumnHeader column, int order = ASC_ORDER)
        {
            this.column = column;
            this.order = order;
        }

        public int Compare(object x, object y)
        {
            ListViewItem item1 = (ListViewItem)x;
            ListViewItem item2 = (ListViewItem)y;
            NodeInfo info1 = (NodeInfo)item1.Tag;
            NodeInfo info2 = (NodeInfo)item2.Tag;
            int ret = 0;
            if (info1 != null && info2 != null)
            {
                switch (column.Text)
                {
                    case "Name":
                        ret = info1.File.Name.CompareTo(info2.File.Name);
                        break;
                    case "Date":
                        ret = info1.File.ModifiedDate.CompareTo(info2.File.ModifiedDate);
                        break;
                    case "Size":
                        ret = info1.File.Size.CompareTo(info2.File.Size);
                        break;
                }
            }
            return ret * order;
        }

        public void ToggleOrder(ColumnHeader newColumn)
        {
            if (column == newColumn)
            {
                order = -order;
            }
            else
            {
                order = ASC_ORDER;
                column = newColumn;
            }
        }
    }


}
