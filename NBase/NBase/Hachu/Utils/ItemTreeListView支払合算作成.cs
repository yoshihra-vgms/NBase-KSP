using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;

using Hachu.Controllers;
using NBaseData.BLC;

namespace Hachu.Utils
{
    public class ItemTreeListView支払合算作成 : ItemTreeListView
    {
        public delegate void CheckedEventHandler();
        public event CheckedEventHandler CheckedEvent = null;

        private NodeController NodeControllers = null;
        private bool InitialChecked = false;

        public ItemTreeListView支払合算作成(TreeListView treeListView)
            : base(treeListView)
        {
            treeListView.CheckBoxes = true;
            treeListView.AfterCheck += new LidorSystems.IntegralUI.ObjectEventHandler(OnChecked);

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

        public void AddNodes(List<合算対象の受領> jrys)
        {
            treeListView.SuspendUpdate();
            foreach (合算対象の受領 jry in jrys)
            {
                TreeListViewNode node = MakeNode(jry);
                treeListView.Nodes.Add(node);
                NodeControllers.SetNode(jry.OdJryID, jry, node);

                //開く m.yoshihara
                treeListView.EnsureVisible(node);
            }

            //使えなくなった模様 m.yoshihara
            //treeListView.ExpandAll();
            treeListView.ResumeUpdate();
        }

        private TreeListViewNode MakeNode(合算対象の受領 jry)
        {
            TreeListViewNode node = new TreeListViewNode();

            // チェックボックス
            node.Checked = InitialChecked;
            AddSubItem(node, "", true);
            // 手配内容
            AddSubItem(node, jry.ThiNaiyou, true);
            // 科目
            string kamokuName = "";
            if (jry.UtiwakeKamokuName != null && jry.UtiwakeKamokuName.Length > 0)
            {
                kamokuName = jry.UtiwakeKamokuName;
            }
            else
            {
                kamokuName = jry.KamokuName;
            }
            AddSubItem(node, kamokuName, true);
            // 発注日
            AddSubItem(node, jry.HachuDate.ToShortDateString(), true);
            // 発注番号
            AddSubItem(node, jry.HachuNo, true);
            // 業者
            AddSubItem(node, jry.CustomerName, true);
            // 完了日
            AddSubItem(node, jry.JryDate.ToShortDateString(), true);
            // 金額
            //AddSubItem(node, NBaseCommon.Common.金額出力2(jry.Amount), true);
            AddSubItem(node, NBaseCommon.Common.金額出力2(jry.Amount + jry.Carriage), true);

            return node;
        }

        public List<合算対象の受領> GetCheckedNodes()
        {
            List<合算対象の受領> ret = new List<合算対象の受領>();
            foreach (TreeListViewNode node in treeListView.Nodes)
            {
                合算対象の受領 item = GetCheckedNodeItem(node);
                if (item != null)
                {
                    ret.Add(item);
                }
            }
            return ret;
        }
        public 合算対象の受領 GetCheckedNodeItem(TreeListViewNode node)
        {
            if (node.CheckState == CheckState.Unchecked)
            {
                return null;
            }
            string topKey = NodeControllers.GetTopKey(node);
            合算対象の受領 topInfo = NodeControllers.GetTopInfo(topKey) as 合算対象の受領;
            return topInfo;
        }
        public void OnChecked(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            if (e.Object is TreeListViewNode)
            {
                if (CheckedEvent != null)
                    CheckedEvent();
            }
        }
    }
}
