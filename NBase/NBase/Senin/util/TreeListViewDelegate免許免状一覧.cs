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
    public class TreeListViewDelegate免状免許一覧 : TreeListViewDelegate
    {
        private NodeController NodeControllers = null;
        private bool InitialChecked = false;
        
        /// <summary>
        /// フォント
        /// </summary>
        //protected Font defaultFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
        //protected Font defaultFont = new Font("MS UI Gothic", 9, FontStyle.Regular);

        public TreeListViewDelegate免状免許一覧(TreeListView treeListView, bool CheckBoxes)
            : base(treeListView)
        {
            Size s = treeListView.ExpandBoxStyle.ImageSize;
            treeListView.ExpandBoxStyle.ImageSize = new Size(s.Width - 3, s.Height - 3);
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

        private void SetColumns(List<Column> columns)
        {
            base.SetColumns(columns);

            foreach (TreeListViewColumn h in treeListView.Columns)
            {
                h.FormatStyle.HeaderBorderCornerShape.SetCornerShape(LidorSystems.IntegralUI.Style.CornerShape.Squared);
            }
        }


        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "選択";
            h.width = 70;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "免許・免状 / 種別";
            h.width = 240;
            columns.Add(h);

            return columns;
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

        public void AddNodes(List<MsSiMenjou> menjouList, List<MsSiMenjouKind> kindList)
        {
            treeListView.SuspendUpdate();

            foreach (MsSiMenjou menjou in menjouList)
            {
                TreeListViewNode node = MakeNode(menjou.Name);
                treeListView.Nodes.Add(node);

                NodeControllers.SetNode(menjou.MsSiMenjouID.ToString(), menjou, node);

                var kindListAtMenjouId = from kind in kindList
                                         where kind.MsSiMenjouID == menjou.MsSiMenjouID
                                         select kind;

                foreach (MsSiMenjouKind kind in kindListAtMenjouId)
                {
                    TreeListViewNode subNode = MakeNode(kind.Name);
                    node.Nodes.Add(subNode);

                    NodeControllers.SetSubNodes(menjou.MsSiMenjouID.ToString(), kind.MsSiMenjouKindID.ToString(), kind, subNode);
                }
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

        public List<MsSiMenjou> GetCheckedNodes()
        {
            List<MsSiMenjou> ret = new List<MsSiMenjou>();
            foreach (TreeListViewNode itemNode in treeListView.Nodes)
            {
                MsSiMenjou menjou = GetCheckedNodeItem(itemNode);
                if (menjou != null)
                    ret.Add(menjou);
            }
            return ret;
        }

        public MsSiMenjou GetCheckedNodeItem(TreeListViewNode itemNode)
        {
            if (itemNode.CheckState == CheckState.Unchecked)
            {
                return null;
            }

            string topKey = NodeControllers.GetTopKey(itemNode);
            MsSiMenjou topInfo = NodeControllers.GetTopInfo(topKey) as MsSiMenjou;
            MsSiMenjou retMenjou = new MsSiMenjou();
            retMenjou.MsSiMenjouID = topInfo.MsSiMenjouID;
            retMenjou.menjouKinds = new List<MsSiMenjouKind>();

            foreach (TreeListViewNode secondNode in itemNode.Nodes)
            {
                if (secondNode.CheckState == CheckState.Unchecked)
                {
                    continue;
                }
                string secondKey = NodeControllers.GetSecondKey(secondNode);
                MsSiMenjouKind secondInfo = NodeControllers.GetSecondInfo(secondKey) as MsSiMenjouKind;
                MsSiMenjouKind retMenjouKind = new MsSiMenjouKind();
                retMenjouKind.MsSiMenjouID = secondInfo.MsSiMenjouID;
                retMenjouKind.MsSiMenjouKindID = secondInfo.MsSiMenjouKindID;
                retMenjou.menjouKinds.Add(retMenjouKind);
            }
            return retMenjou;
        }
    }
}
