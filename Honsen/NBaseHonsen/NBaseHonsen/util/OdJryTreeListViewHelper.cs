using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using LidorSystems.IntegralUI.Lists;
using NBaseUtil;
using NBaseData.DAC;

namespace NBaseHonsen.util
{
    class OdJryTreeListViewHelper
    {
        internal static TreeListViewNode CreateOdJryNode(TreeListView treeListView1,
                                                       TreeListViewDelegate treeListViewDelegate,
                                                       OdJry jry)
        {
            Color backColor = Color.Thistle;

            TreeListViewNode node1 = treeListViewDelegate.CreateNode(backColor);
            treeListViewDelegate.AddSubItem(node1, String.Format("{0:yyyy/MM/dd}", jry.OdThiThiIraiDate), true);
            treeListViewDelegate.AddSubItem(node1, String.Format("{0:yyyy/MM/dd}", jry.OdMkNouki), true);
            treeListViewDelegate.AddSubItem(node1, jry.OdThiNaiyou, true);
            treeListViewDelegate.AddSubItem(node1, jry.MsCustomerCustomerName, true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, jry.MsUserThiUserName, true);
            treeListViewDelegate.AddSubItem(node1, StringUtils.ToStatusStr(jry.SendFlag), true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListView1.Nodes.Add(node1);

            return node1;
        }
        
        
        internal static TreeListViewNode CreateOdJryItemNode(TreeListViewNode parentNode,
                                                           TreeListViewDelegate treeListViewDelegate,
                                                           OdJryItem item)
        {
            TreeListViewNode node1_1 = treeListViewDelegate.CreateNode();
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            //string itemName = item.MsItemSbtName != null && item.MsItemSbtName.Length > 0 ?
            //    item.MsItemSbtName + " : " + item.ItemName : item.ItemName;
            //treeListViewDelegate.AddSubItem(node1_1, itemName, true);
            treeListViewDelegate.AddSubItem(node1_1, item.ItemName, true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, StringUtils.ToStatusStr(item.SendFlag), true);
            treeListViewDelegate.AddSubItem(node1_1, item.Bikou == null ? "" : item.Bikou.Replace("\n", ""), true);
            parentNode.Nodes.Add(node1_1);

            return node1_1;
        }

        internal static TreeListViewNode CreateOdJryShousaiItemNode(TreeListViewNode parentNode,
                                                                  TreeListViewDelegate treeListViewDelegate, 
                                                                  OdJryShousaiItem shousaiItem,
                                                                  int i)
        {
            TreeListViewNode node1_1_1 = treeListViewDelegate.CreateNode();
            treeListViewDelegate.AddSubItem(node1_1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1_1, i.ToString(), true);
            treeListViewDelegate.AddSubItem(node1_1_1, shousaiItem.ShousaiItemName, true);
            treeListViewDelegate.AddSubItem(node1_1_1, StringUtils.ToStr(shousaiItem.Count), true);
            treeListViewDelegate.AddSubItem(node1_1_1, StringUtils.ToStr(shousaiItem.JryCount), true);
            treeListViewDelegate.AddSubItem(node1_1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1_1, StringUtils.ToStatusStr(shousaiItem.SendFlag), true);
            treeListViewDelegate.AddSubItem(node1_1_1, shousaiItem.Bikou == null ? "" : shousaiItem.Bikou.Replace("\n", ""), true);
            parentNode.Nodes.Add(node1_1_1);

            return node1_1_1;
        }


        internal static TreeListViewNode CreateJryItemHeaderNode(TreeListViewNode parentNode,
                                                                TreeListViewDelegate treeListViewDelegate,
                                                                ItemHeader<OdJryItem> h)
        {
            Color backColor = Color.LightBlue;

            TreeListViewNode node1_1 = treeListViewDelegate.CreateNode();
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, h.Header, true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, StringUtils.ToStatusStr(h.SendFlag), true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            parentNode.Nodes.Add(node1_1);

            return node1_1;
        }


        internal static List<ItemHeader<OdJryItem>> GroupByJryItemHeader(List<OdJryItem> odJryItems)
        {
            var result = new List<ItemHeader<OdJryItem>>();
            var headerDic = new Dictionary<string, ItemHeader<OdJryItem>>();

            int i = 0;
            foreach (OdJryItem ji in odJryItems)
            {
                if (!headerDic.ContainsKey(ji.Header))
                {
                    ItemHeader<OdJryItem> h = new ItemHeader<OdJryItem>(i++);
                    h.Header = ji.Header;
                    h.SendFlag = ji.SendFlag;

                    headerDic[ji.Header] = h;
                    result.Add(h);
                }

                headerDic[ji.Header].Items.Add(ji);
            }

            return result;
        }
    }
}
