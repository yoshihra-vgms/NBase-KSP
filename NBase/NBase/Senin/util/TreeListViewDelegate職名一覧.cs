using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;
using System.Drawing;


namespace Senin.util
{
    public class TreeListViewDelegate職名一覧 : TreeListViewDelegate
    {
        private NodeController NodeControllers = null;
        private bool InitialChecked = false;
        
        /// <summary>
        /// フォント
        /// </summary>
        //protected Font defaultFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
        //protected Font defaultFont = new Font("MS UI Gothic", 9, FontStyle.Regular);

        public TreeListViewDelegate職名一覧(TreeListView treeListView, bool CheckBoxes)
            : base(treeListView)
        {
            Size s = treeListView.ExpandBoxStyle.ImageSize;
            treeListView.ExpandBoxStyle.ImageSize = new Size(s.Width - 2, s.Height - 2);
            SubItemFont = new Font("MS UI Gothic", 9, FontStyle.Regular);

            treeListView.FocusedNodeStyle.BackColor = Color.White;
            treeListView.FocusedNodeStyle.BorderColor = Color.White;
            treeListView.HoverNodeStyle.BackColor = Color.White;
            treeListView.HoverNodeStyle.BorderColor = Color.White;
            treeListView.SelectedNodeStyle.BackColor = Color.White;
            treeListView.SelectedNodeStyle.BorderColor = Color.White;

            treeListView.CheckBoxes = CheckBoxes;
            treeListView.AfterCheck += new LidorSystems.IntegralUI.ObjectEventHandler(OnAfterCheck);

            NodeControllers = new NodeController();

            treeListView.SuspendUpdate();

            SetColumns(CreateColumns());

            treeListView.ResumeUpdate();
        }


        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "選択";
            h.width = 40;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "職名";
            h.width = 100;
            columns.Add(h);

            return columns;
        }

        private void SetColumns(List<Column> columns)
        {
            base.SetColumns(columns);

            foreach (TreeListViewColumn h in treeListView.Columns)
            {
                h.FormatStyle.HeaderBorderCornerShape.SetCornerShape(LidorSystems.IntegralUI.Style.CornerShape.Squared);
            }
        }

        public override void Clear()
        {
            base.Clear();
            NodeControllers.Clear();
        }
        public override void NodesClear()
        {
            base.NodesClear();
            NodeControllers.Clear();
        }

        public void AddNodes(List<MsSiShokumei> shokumeiListt)
        {
            treeListView.SuspendUpdate();

            foreach (MsSiShokumei shokumei in shokumeiListt)
            {
                TreeListViewNode node = MakeNode(shokumei.Name);
                treeListView.Nodes.Add(node);

                NodeControllers.SetNode(shokumei.MsSiShokumeiID.ToString(), shokumei, node);
            }

            // 全部、開く
            treeListView.ExpandAll();
            treeListView.ResumeUpdate();
        }
        private TreeListViewNode MakeNode(string name)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 
            if (treeListView.CheckBoxes)
            {
                node.Checked = InitialChecked;
                AddSubItem(node, "", true);
            }
            // 名称
            AddSubItem2(node, name);

            return node;
        }

        public List<MsSiShokumei> GetCheckedNodes()
        {
            List<MsSiShokumei> ret = new List<MsSiShokumei>();
            foreach (TreeListViewNode itemNode in treeListView.Nodes)
            {
                if (itemNode.CheckState == CheckState.Unchecked)
                {
                    continue;
                }

                string topKey = NodeControllers.GetTopKey(itemNode);
                MsSiShokumei topInfo = NodeControllers.GetTopInfo(topKey) as MsSiShokumei;
                MsSiShokumei shokumei = new MsSiShokumei();
                shokumei.MsSiShokumeiID = topInfo.MsSiShokumeiID;
                ret.Add(shokumei);
            }
            return ret;
        }
    }
}
