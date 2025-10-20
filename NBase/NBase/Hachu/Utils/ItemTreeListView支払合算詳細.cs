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
    public class ItemTreeListView支払合算詳細 : ItemTreeListView
    {
        private NodeController NodeControllers = null;

        public ItemTreeListView支払合算詳細(TreeListView treeListView)
            : base(treeListView)
        {
            treeListView.CheckBoxes = false;

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

        public void AddNodes(List<OdShrGassanItem> gassanItems)
        {
            treeListView.SuspendUpdate();
            foreach (OdShrGassanItem gassanItem in gassanItems)
            {
                TreeListViewNode node = MakeNode(gassanItem);
                treeListView.Nodes.Add(node);
                NodeControllers.SetNode(gassanItem.OdShrGassanItemID, gassanItem, node);

                //開く m.yoshihara
                treeListView.EnsureVisible(node);
            }
            //使えなくなった模様 m.yoshihara
            //treeListView.ExpandAll();
            treeListView.ResumeUpdate();
        }

        private TreeListViewNode MakeNode(OdShrGassanItem gassanItem)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 手配内容
            AddSubItem(node, gassanItem.ThiNaiyou, true);
            // 科目
            string kamokuName = "";
            if (gassanItem.UtiwakeKamokuName != null && gassanItem.UtiwakeKamokuName.Length > 0)
            {
                kamokuName = gassanItem.UtiwakeKamokuName;
            }
            else
            {
                kamokuName = gassanItem.KamokuName;
            }
            AddSubItem(node, kamokuName, true);
            // 発注日
            AddSubItem(node, gassanItem.HachuDate.ToShortDateString(), true);
            // 発注番号
            AddSubItem(node, gassanItem.HachuNo, true);
            // 業者
            AddSubItem(node, gassanItem.CustomerName, true);
            // 完了日
            AddSubItem(node, gassanItem.JryDate.ToShortDateString(), true);
            // 金額
            //AddSubItem(node, NBaseCommon.Common.金額出力2(gassanItem.Amount), true);
            AddSubItem(node, NBaseCommon.Common.金額出力2(gassanItem.Amount + gassanItem.Carriage), true);

            return node;
        }
    }
}
