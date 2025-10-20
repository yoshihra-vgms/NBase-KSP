using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using System.Drawing;

namespace Yojitsu
{
    class HimokuTreeNode
    {
        public MsHimoku MsHimoku { get; set; }
        public string Name { get; set; }
        public Color BgColor { get; set; }
        public bool Dollar { get; set; }
        
        private List<HimokuTreeNode> children = new List<HimokuTreeNode>();
        public List<HimokuTreeNode> Children
        {
            get
            {
                return children;
            }
        }
        
        
        public void Add(HimokuTreeNode child)
        {
            children.Add(child);
        }

        #region IEnumerable<HimokuTreeNode> メンバ

        public IEnumerator<HimokuTreeNode> GetEnumerator()
        {
            return HimokuTreeNode.Traverse(this).GetEnumerator();
        }

        #endregion

        public static IEnumerable<HimokuTreeNode> Traverse(HimokuTreeNode n)
        {
            if (n.MsHimoku != null)
            {
                yield return n;
            }

            foreach (HimokuTreeNode c in n.Children)
            {
                foreach (HimokuTreeNode m in Traverse(c))
                {
                    yield return m;
                }
            }
        }
    }
}
