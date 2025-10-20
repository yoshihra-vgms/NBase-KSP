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
    class OdThiTreeListViewHelper
    {
        internal static TreeListViewNode CreateOdThiNode(TreeListView treeListView1,
                                                      TreeListViewDelegate treeListViewDelegate,
                                                      OdThi thi)
        {
            Color backColor = GetBackColor(thi.Status, thi.SendFlag);

            TreeListViewNode node1 = treeListViewDelegate.CreateNode(backColor);
            treeListViewDelegate.AddSubItem(node1, String.Format("{0:yyyy/MM/dd}", thi.ThiIraiDate), true);
            treeListViewDelegate.AddSubItem(node1, thi.ThiUserName, true);
            treeListViewDelegate.AddSubItem(node1, thi.OrderThiIraiStatus, true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, thi.Naiyou, true);
            //treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, "", true);
            treeListViewDelegate.AddSubItem(node1, thi.Bikou, true);
            treeListViewDelegate.AddSubItem(node1, StringUtils.ToStatusStr(thi.SendFlag), true);
            treeListView1.Nodes.Add(node1);

            return node1;
        }

        
        internal static TreeListViewNode CreateOdThiItemNode(TreeListViewNode parentNode,
                                                           TreeListViewDelegate treeListViewDelegate,
                                                           OdThiItem item)
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
            //treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, StringUtils.ToStatusStr(item.SendFlag), true);
            parentNode.Nodes.Add(node1_1);

            return node1_1;
        }

        
        internal static TreeListViewNode CreateOdThiShousaiItemNode(TreeListViewNode parentNode,
                                                                  TreeListViewDelegate treeListViewDelegate,
                                                                  OdThiShousaiItem shousaiItem,
                                                                  string msThiIraiSbtId,
                                                                  int i)
        {
            TreeListViewNode node1_1_1 = treeListViewDelegate.CreateNode();
            treeListViewDelegate.AddSubItem(node1_1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1_1, i.ToString(), true);
            treeListViewDelegate.AddSubItem(node1_1_1, shousaiItem.ShousaiItemName, true);
            //treeListViewDelegate.AddSubItem(node1_1_1, GetShousaiInputKind(shousaiItem, msThiIraiSbtId), true);
            treeListViewDelegate.AddSubItem(node1_1_1, MasterTable.instance().GetMsTani(shousaiItem.MsTaniID).TaniName, true);
            treeListViewDelegate.AddSubItem(node1_1_1, shousaiItem.ZaikoCount.ToString(), true);
            treeListViewDelegate.AddSubItem(node1_1_1, shousaiItem.Count.ToString(), true);
            treeListViewDelegate.AddSubItem(node1_1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1_1, shousaiItem.Bikou, true);
            treeListViewDelegate.AddSubItem(node1_1_1, StringUtils.ToStatusStr(shousaiItem.SendFlag), true);
            parentNode.Nodes.Add(node1_1_1);

            return node1_1_1;
        }

        //public static string GetShousaiInputKind(OdThiShousaiItem shousaiItem, string msThiIraiSbtId)
        //{
        //    string retStr = "";
        //    if (msThiIraiSbtId == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品))
        //    {
        //        retStr = shousaiItem.SpecificFlag == 1 ? "特定" : (shousaiItem.MsVesselItemID != null && shousaiItem.MsVesselItemID != string.Empty) ? "ﾘｽﾄ" : "追加";
        //    }
        //    return retStr;
        //}

        
        internal static Color GetBackColor(int status, int sendFlag)
        {
            Color backColor = Color.White;

            if (status == (int)OdThi.STATUS.船未手配)
            {
                backColor = Color.Yellow;
            }
            else if (status == (int)OdThi.STATUS.手配依頼済)
            {
                backColor = Color.LightGreen;
            }

            return backColor;
        }


        internal static TreeListViewNode CreateThiItemHeaderNode(TreeListViewNode parentNode,
                                                                TreeListViewDelegate treeListViewDelegate,
                                                                ItemHeader<OdThiItem> h)
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
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, "", true);
            treeListViewDelegate.AddSubItem(node1_1, StringUtils.ToStatusStr(h.SendFlag), true);
            parentNode.Nodes.Add(node1_1);

            return node1_1;
        }


        internal static List<ItemHeader<OdThiItem>> GroupByThiItemHeader(List<OdThiItem> odThiItems)
        {
            return GroupByThiItemHeader(odThiItems, true);
        }


        internal static List<ItemHeader<OdThiItem>> GroupByThiItemHeader(List<OdThiItem> odThiItems, bool skipCancel)
        {
            var result = new List<ItemHeader<OdThiItem>>();
            var headerDic = new Dictionary<string, ItemHeader<OdThiItem>>();

            foreach (OdThiItem ti in odThiItems)
            {
                if (skipCancel && ti.CancelFlag == 1)
                {
                    continue;
                }

                if (!headerDic.ContainsKey(ti.Header))
                {
                    ItemHeader<OdThiItem> h = new ItemHeader<OdThiItem>(0);
                    h.Header = ti.Header;
                    h.SendFlag = ti.SendFlag;

                    headerDic[ti.Header] = h;
                    result.Add(h);
                }

                headerDic[ti.Header].Items.Add(ti);
            }

            return result;
        }
    }
}
