using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;
using NBaseData.DAC;
using Hachu.Models;
using Hachu.Controllers;
using Hachu.HachuManage;

namespace Hachu.Utils
{
    public class ItemTreeListView年度変更 : ItemTreeListView
    {
        private NodeController NodeControllers = null;

        public ItemTreeListView年度変更(TreeListView treeListView)
            : base(treeListView)
        {
            treeListView.CheckBoxes = true;

            NodeControllers = new NodeController();
        }
        public override void Clear()
        {
            base.Clear();
            NodeControllers.Clear();
        }
        public void AddNode(TreeListViewNode node)
        {
            treeListView.Nodes.Add(node);
        }

        public TreeListViewNode MakeNode(ListInfo年度変更 info)
        {
            TreeListViewNode node = new TreeListViewNode();

            // No
            //AddSubItem(node, info.no.ToString(), true);
            AddSubItem(node, "", true);

            // 船名
            AddSubItem(node, info.thi.VesselName, true);

            // 種別
            AddSubItem(node, info.thi.ThiIraiSbtName, true);

            // 詳細種別
            AddSubItem(node, info.thi.ThiIraiShousaiName, true);

            // 件名
            AddSubItem(node, info.thi.Naiyou, true);


            // 手配依頼日
            AddSubItem(node, info.thi.ThiIraiDate.ToShortDateString(), true);

            // 見積依頼日
            AddSubItem(node, info.mm.MmDate.ToShortDateString(), true);

            if (info.mk != null)
            {
                // 回答日
                AddSubItem(node, info.mk.MkDate.ToShortDateString(), true);
                // 発注日
                AddSubItem(node, info.mk.HachuDate.ToShortDateString(), true);
                // 発注番号
                AddSubItem(node, info.mk.HachuNo, true);
            }
            else
            {
                AddSubItem(node, "", true);
                AddSubItem(node, "", true);
                AddSubItem(node, "", true);
            }

            if (info.jry != null)
            {
                AddSubItem(node, info.mk.StatusName + ":" + info.jry.StatusName + "(" + info.jry.UserKey + ")", true);
            }
            else
            {
                AddSubItem(node, "受領データなし", true);
            }

            return node;
        }

        public List<TreeListViewNode> GetCheckedNodes()
        {
            List<TreeListViewNode> ret = new List<TreeListViewNode>();

            foreach (TreeListViewNode itemNode in treeListView.Nodes)
            {
                if (itemNode.CheckState != CheckState.Unchecked)
                {
                    ret.Add(itemNode);
                }
            }
            return ret;
        }
    }
}
