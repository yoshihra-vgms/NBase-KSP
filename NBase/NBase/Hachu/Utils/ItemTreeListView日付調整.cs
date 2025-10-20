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
    public class ItemTreeListView日付調整 : ItemTreeListView
    {
        private NodeController NodeControllers = null;

        public ItemTreeListView日付調整(TreeListView treeListView)
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

        public TreeListViewNode MakeNode(ListInfo日付調整 info)
        {
            TreeListViewNode node = new TreeListViewNode();

            // No
            AddSubItem(node, info.no.ToString(), true);

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
                if (info.mk.MkDate != DateTime.MinValue)
                {
                    AddSubItem(node, info.mk.MkDate.ToShortDateString(), true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }

                // 発注日
                AddSubItem(node, info.mk.HachuDate.ToShortDateString(), true);

                // 納期
                AddSubItem(node, info.mk.Nouki.ToShortDateString(), true);
            }
            else
            {
                AddSubItem(node, "", true);
                AddSubItem(node, "", true);
                AddSubItem(node, "", true);
            }

            if (info.jry != null && info.jry.JryDate != DateTime.MinValue)
            {
                // 受領日
                AddSubItem(node, info.jry.JryDate.ToShortDateString(), true);
            }
            else
            {
                AddSubItem(node, "", true);
            }

            if (info.shr != null)
            {
                // 請求書日
                if (info.shr.ShrIraiDate != DateTime.MinValue)
                {
                    AddSubItem(node, info.shr.ShrIraiDate.ToShortDateString(), true);

                }
                else
                {
                    AddSubItem(node, "", true);
                } 
                
                // 支払日
                if (info.shr.ShrDate != DateTime.MinValue)
                {
                    AddSubItem(node, info.shr.ShrDate.ToShortDateString(), true);

                }
                else
                {
                    AddSubItem(node, "", true);
                }
            }
            else
            {
                AddSubItem(node, "", true);
                AddSubItem(node, "", true);
            }

            return node;
        }
    }
}
