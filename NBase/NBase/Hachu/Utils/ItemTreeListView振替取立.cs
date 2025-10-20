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
    public class ItemTreeListView振替取立 : ItemTreeListView
    {
        private NodeController NodeControllers = null;

        public ItemTreeListView振替取立(TreeListView treeListView)
            : base(treeListView)
        {
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
        
        public TreeListViewNode MakeNode(OdFurikaeToritate OdFkTt)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 発注日
            AddSubItem(node, OdFkTt.HachuDate.ToShortDateString(), true);
            // 項目
            AddSubItem(node, OdFkTt.Koumoku, true);
            // 区分
            if (OdFkTt.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                if (OdFkTt.MsThiIraiShousaiID == NBaseCommon.Common.MsThiIraiShousai_小修理ID)
                {
                    AddSubItem(node, OdFkTt.MsItemSbtName, true);
                }
                else
                {
                    AddSubItem(node, OdFkTt.MsNyukyoKamokuName, true);
                }
            }
            else
            {
                AddSubItem(node, "", true);
            }
            // 業者
            AddSubItem(node, OdFkTt.MsCustomerName, true);
            // 数量
            AddSubItem(node, OdFkTt.Count.ToString(), true);
            // 単価
            AddSubItem(node, NBaseCommon.Common.金額出力2(OdFkTt.Tanka), true);
            // 金額
            AddSubItem(node, NBaseCommon.Common.金額出力2(OdFkTt.Amount), true);
            // 完了日
            AddSubItem(node, OdFkTt.Kanryobi.ToShortDateString(), true);
            // 請求書日
            AddSubItem(node, OdFkTt.Seikyushobi.ToShortDateString(), true);
            // 起票日
            AddSubItem(node, OdFkTt.Kihyobi.ToShortDateString(), true);
            // 備考
            AddSubItem(node, OdFkTt.Bikou, true);

            return node;
        }
    }
}
