using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oblqo.Controls
{
    public static class ControlUtil
    {
        public static IEnumerable<TreeNode> GetAllTreeViewNodes(TreeView treeView)
        {
            var ret = new List<TreeNode>();
            foreach (TreeNode node in treeView.Nodes)
            {
                ret.Add(node);
                ret.AddRange(GetAllTreeViewNodes(node));
            }
            return ret;
        }

        public static IEnumerable<TreeNode> GetAllTreeViewNodes(TreeNode root)
        {
            var ret = new List<TreeNode>();
            foreach (TreeNode node in root.Nodes)
            {
                ret.Add(node);
                ret.AddRange(GetAllTreeViewNodes(node));
            }
            return ret;
        }

        public static void UpdateTeeViewNode(AccountFileStates newFileState, TreeNode node)
        {
            if ((newFileState & AccountFileStates.Deleted) != 0)
            {
                node.Remove();
                return;
            }
            if ((newFileState & AccountFileStates.New) != 0)
            {
                node.ForeColor = Color.Green;
            }
        }

    }
}
