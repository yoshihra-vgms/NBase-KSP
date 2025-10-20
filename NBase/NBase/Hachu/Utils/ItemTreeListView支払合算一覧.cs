using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;

using Hachu.Controllers;
using NBaseData.DAC;

namespace Hachu.Utils
{
    public class ItemTreeListView支払合算一覧 : ItemTreeListView
    {
        private NodeController NodeControllers = null;

        public ItemTreeListView支払合算一覧(TreeListView treeListView)
            : base(treeListView)
        {
            NodeControllers = new NodeController();
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

        public void AddNodes(List<OdShrGassanHead> gassanHeads)
        {
            treeListView.SuspendUpdate();
            foreach (OdShrGassanHead gassanHead in gassanHeads)
            {
                TreeListViewNode node = MakeNode(gassanHead);
                treeListView.Nodes.Add(node);
                NodeControllers.SetNode(gassanHead.OdShrGassanHeadID, gassanHead, node);

                //開く m.yoshihara
                treeListView.EnsureVisible(node);
            }
            //使えなくなった模様 m.yoshihara
            //treeListView.ExpandAll();
                       
            treeListView.ResumeUpdate();
        }

        public TreeListViewNode MakeNode(OdShrGassanHead gassanHead)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 状況
            AddSubItem(node, gassanHead.StatusStr, true);
            // 種別
            AddSubItem(node, gassanHead.ThiIraiSbtName, true);
            // 代表発注番号
            AddSubItem(node, gassanHead.HachuNo, true);
            // 業者
            AddSubItem(node, gassanHead.MsCustomerName, true);
            // 金額
            AddSubItem(node, NBaseCommon.Common.金額出力2(gassanHead.Amount), true);
            // 備考
            AddSubItem(node, gassanHead.Bikou, true);

            return node;
        }

        public OdShrGassanHead 選択データ取得()
        {
            TreeListViewNode selectedNode = treeListView.SelectedNode;
            if (selectedNode == null)
            {
                return null;
            }
            string topKey = NodeControllers.GetTopKey(selectedNode);
            OdShrGassanHead topInfo = NodeControllers.GetTopInfo(topKey) as OdShrGassanHead;
            return topInfo;
        }
    }
}
