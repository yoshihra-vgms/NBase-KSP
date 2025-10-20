using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;
using NBaseData.DAC;
using Hachu.Models;
using Hachu.Controllers;
using Hachu.HachuManage;

namespace Hachu.Utils
{
    public class ItemTreeListView見積依頼 : ItemTreeListView
    {
        public enum 表示方式enum { Zeroを表示, Zero以外を表示 };
        public 表示方式enum Enum表示方式 = 表示方式enum.Zeroを表示;
        private string ThiIraiSbtID;

        private NodeController NodeControllers = null;

        public ItemTreeListView見積依頼(string ThiIraiSbtID, TreeListView treeListView)
            : base(treeListView)
        {
            this.ThiIraiSbtID = ThiIraiSbtID;
            NodeControllers = new NodeController();
        }
        public override void Clear()
        {
            base.Clear();
            NodeControllers.Clear();
        }
        public void AddNodes(List<Item見積依頼品目> 品目s)
        {
            AddNodes(false, 品目s);
        }
        public void AddNodes(bool viewHeader, List<Item見積依頼品目> 品目s)
        {
            this.viewHeader = viewHeader;

            TreeListViewNode headerNode = null;
            string header = "";
            int no = 0;

            //treeListView.SuspendUpdate(); // 呼び出し側でやる
            foreach (Item見積依頼品目 品目 in 品目s)
            {
                if (品目.品目.CancelFlag == 1)
                    continue;
                if (viewHeader)
                {
                    AddNodes(ref no, ref header, ref headerNode, 品目);
                }
                else
                {
                    AddNodes(ref no, 品目);
                }
            }

            // 全部、開く 使えなくなった模様 m.yoshihara
            //treeListView.ExpandAll();

            //treeListView.ResumeUpdate(); // 呼び出し側でやる
        }

        public void AddNodes(ref int no, Item見積依頼品目 品目)
        {
            // ノードの追加
            TreeListViewNode node = MakeNode(品目.品目);
            //treeListView.Nodes.Add(node);

            //NodeControllers.SetNode(品目.品目.OdMmItemID, 品目, node);

            foreach (OdMmShousaiItem 詳細品目 in 品目.詳細品目s)
            {
                if (詳細品目.CancelFlag == 1)
                    continue;

                //===========================
                // 2014.1 [2013年度改造]
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                {
                    if (Enum表示方式 == 表示方式enum.Zero以外を表示 && 詳細品目.Count < 1)
                        continue;
                    if (Enum表示方式 == 表示方式enum.Zeroを表示 && 詳細品目.Count > 0)
                        continue;
                }

                no++;
                TreeListViewNode subNode = MakeSubNode(no, 詳細品目);
                node.Nodes.Add(subNode);

                NodeControllers.SetSubNodes(品目.品目.OdMmItemID, 詳細品目.OdMmShousaiItemID, 詳細品目, subNode);

                //開く m.yoshihara
                treeListView.EnsureVisible(subNode);

            }

            //===========================
            // 2014.1 [2013年度改造]
            if (ThiIraiSbtID != NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                treeListView.Nodes.Add(node);

                NodeControllers.SetNode(品目.品目.OdMmItemID, 品目, node);
            }
            else
            {
                if (node.Nodes.Count > 0)
                {
                    treeListView.Nodes.Add(node);

                    NodeControllers.SetNode(品目.品目.OdMmItemID, 品目, node);
                }
            }


            // 全部、開く 使えなくなった模様 m.yoshihara
            //treeListView.ExpandAll();
            treeListView.EnsureVisible(node);
        }
        public void AddNodes(ref int no, ref string header, ref TreeListViewNode headerNode, Item見積依頼品目 品目)
        {
            if (headerNode == null)
            {
                header = 品目.品目.Header;
                headerNode = MakeHeaderNode(品目.品目.Header);
                treeListView.Nodes.Add(headerNode);
            }
            else if (header != 品目.品目.Header)
            {
                header = 品目.品目.Header;
                headerNode = MakeHeaderNode(品目.品目.Header);
                treeListView.Nodes.Add(headerNode);
            }

            TreeListViewNode node = MakeNode(品目.品目);
            headerNode.Nodes.Add(node);

            NodeControllers.SetNode(品目.品目.OdMmItemID, 品目, node);

            foreach (OdMmShousaiItem 詳細品目 in 品目.詳細品目s)
            {
                if (詳細品目.CancelFlag == 1)
                    continue;

                no++;
                TreeListViewNode subNode = MakeSubNode(no, 詳細品目);
                node.Nodes.Add(subNode);

                NodeControllers.SetSubNodes(品目.品目.OdMmItemID, 詳細品目.OdMmShousaiItemID, 詳細品目, subNode);

                //開く m.yoshihara
                treeListView.EnsureVisible(subNode);
            }

            // 全部、開く 使えなくなった模様 m.yoshihara
            //treeListView.ExpandAll();
        }
        
        private TreeListViewNode MakeHeaderNode(string headerText)
        {
            TreeListViewNode node = new TreeListViewNode();

            // No
            AddSubItem(node, "", true);
            // ヘッダ
            AddSubItemAsHeader(node, headerText, true);

            // 単位
            AddSubItem(node, "", true);
            // 依頼数
            AddSubItem(node, "", true);
            // 添付
            AddSubItem(node, "", true);

            return node;
        }
        private TreeListViewNode MakeNode(OdMmItem item)
        {
            TreeListViewNode node = new TreeListViewNode();

            // No
            AddSubItem(node, "", true);
            // 品目名
            string itemName = "";
            //if (item.MsItemSbtID != null && item.MsItemSbtID.Length > 0)
            //{
            //    itemName += item.MsItemSbtName + "：";
            //}
            itemName += item.ItemName;
            AddSubItemAsItem(node, itemName, true);
            // 単位
            AddSubItem(node, "", true);
            // 依頼数
            AddSubItem(node, "", true);
            // 添付
            if (item.OdAttachFileID != null && item.OdAttachFileID.Length > 0)
            {
                AddLinkItem(node, item.OdAttachFileID);
            }
            else
            {
                AddSubItem(node, "", true);
            }

            return node;
        }
        private TreeListViewNode MakeSubNode(int no, OdMmShousaiItem syousai)
        {
            TreeListViewNode node = new TreeListViewNode();

            // No
            AddSubItem(node, no.ToString(), true);
            // 詳細品目名
            AddSubItemAsShousai(node, syousai.ShousaiItemName, true);
            // 単位
            AddSubItem(node, syousai.MsTaniName, true);
            // 依頼数
            AddSubItem(node, syousai.Count.ToString(), true);
            // 添付
            if (syousai.OdAttachFileID != null && syousai.OdAttachFileID.Length > 0)
            {
                AddLinkItem(node, syousai.OdAttachFileID);
            }
            else
            {
                AddSubItem(node, "", true);
            }

            return node;
        }

        public void AddCustomerData(見積依頼先 customerInfo, List<Item見積依頼品目> 見積依頼品目s)
        {
            AddCustomerData(customerInfo, 見積依頼品目s, false);
        }
        public void AddCustomerData(見積依頼先 customerInfo, List<Item見積依頼品目> 見積依頼品目s, bool updateFlag)
        {
            if (updateFlag == true)
            {
                treeListView.SuspendUpdate();
            }

            TreeListViewColumn column = new TreeListViewColumn();
            StringBuilder columnStr = new StringBuilder();
            columnStr.Append("<div>");
            columnStr.Append("<table>");
            columnStr.Append("<tr><td>" + customerInfo.CustomerName + "</td></tr>");
            columnStr.Append("</table>");
            columnStr.Append("</div>");
            column.HeaderContent = columnStr.ToString();
            column.Width = 100;
            column.StyleFromParent = false;
            column.NormalStyle.HeaderColor = Color.FromArgb(182, 182, 255);
            column.SelectedStyle.HeaderColor = Color.FromArgb(182, 182, 255);
            column.HoverStyle.HeaderColor = Color.FromArgb(182, 182, 255);
            column.FormatStyle.HeaderBorderCornerShape.SetCornerShape(LidorSystems.IntegralUI.Style.CornerShape.Squared);
            column.FormatStyle.ContentAlign = HorizontalAlignment.Right;

            column.FormatStyle.HeaderFont = defaultFont;
            column.FormatStyle.HeaderPadding = new Padding(2, 5, 3, 2);

            treeListView.Columns.Add(column);

            int headerNodeIndex = -1;
            int itemNodeIndex = 0;
            string header = null;
            foreach (Item見積依頼品目 品目 in 見積依頼品目s)
            {
                if (品目.品目.CancelFlag == 1)
                    continue;

                if (viewHeader)
                {
                    AddCustomerItemNodes(ref headerNodeIndex, ref itemNodeIndex, ref header, customerInfo, 品目);
                }
                else
                {
                    AddCustomerItemNodes(ref itemNodeIndex, customerInfo, 品目);
                }
            }
            //treeListView.CollapseAll();
            //treeListView.ExpandAll();
            //treeListView.Invalidate();

            if (updateFlag == true)
            {
                treeListView.ResumeUpdate();
            }
        }
        private void AddCustomerItemNodes(ref int headerNodeIndex, ref int itemNodeIndex, ref string header, 見積依頼先 customerInfo, Item見積依頼品目 依頼品目)
        {
            TreeListViewSubItem subItem = null;
            TreeListViewNode node;

            if (header == null || header != 依頼品目.品目.Header)
            {
                // ヘッダ行
                header = 依頼品目.品目.Header;
                subItem = makeCustomerDataSubItem("");
                ++headerNodeIndex;
                treeListView.Nodes[headerNodeIndex].SubItems.Add(subItem);
                itemNodeIndex = 0;
            }
            node = treeListView.Nodes[headerNodeIndex];

            // 品目行
            subItem = makeCustomerDataSubItem("");
            node.Nodes[itemNodeIndex].SubItems.Add(subItem);

            // 詳細品目行
            List<OdMkShousaiItem> 対象回答詳細品目s = null;
            foreach (Item見積回答品目 回答品目 in customerInfo.見積回答品目s)
            {
                if (回答品目.品目.OdMmItemID == 依頼品目.品目.OdMmItemID)
                {
                    対象回答詳細品目s = 回答品目.詳細品目s;
                    break;
                }
            }

            if (対象回答詳細品目s != null)
            {
                int subNodeIndex = 0;
                foreach (OdMmShousaiItem mmSyousai in 依頼品目.詳細品目s)
                {
                    subItem = null;
                    foreach (OdMkShousaiItem mkShousai in 対象回答詳細品目s)
                    {
                        if (mmSyousai.OdMmShousaiItemID == mkShousai.OdMmShousaiItemID)
                        {
                            subItem = makeCustomerDataSubItem(mkShousai.Count.ToString());
                            break;
                        }
                    }
                    if (subItem == null)
                    {
                        subItem = makeCustomerDataSubItem("-");
                    }

                    node.Nodes[itemNodeIndex].Nodes[subNodeIndex].SubItems.Add(subItem);
                    subNodeIndex++;
                }
            }
            else
            {
                int subNodeIndex = 0;
                foreach (OdMmShousaiItem mmSyousai in 依頼品目.詳細品目s)
                {
                    subItem = makeCustomerDataSubItem("-");
                    node.Nodes[itemNodeIndex].Nodes[subNodeIndex].SubItems.Add(subItem);
                    subNodeIndex++;
                }
            }
            itemNodeIndex++;
        }
        private void AddCustomerItemNodes(ref int nodeIndex, 見積依頼先 customerInfo, Item見積依頼品目 依頼品目)
        {
            TreeListViewSubItem subItem = null;
            subItem = makeCustomerDataSubItem("");
            //treeListView.Nodes[nodeIndex].SubItems.Add(subItem);

            List<OdMkShousaiItem> 対象回答詳細品目s = null;
            foreach (Item見積回答品目 回答品目 in customerInfo.見積回答品目s)
            {
                if (回答品目.品目.OdMmItemID == 依頼品目.品目.OdMmItemID)
                {
                    対象回答詳細品目s = 回答品目.詳細品目s;
                    break;
                }
            }

            //===========================
            // 2014.1 [2013年度改造]
            bool addNodes = false;
            if (対象回答詳細品目s != null)
            {
                foreach (OdMmShousaiItem mmSyousai in 依頼品目.詳細品目s)
                {
                    if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                    {
                        if (Enum表示方式 == 表示方式enum.Zero以外を表示 && mmSyousai.Count < 1)
                            continue;
                        if (Enum表示方式 == 表示方式enum.Zeroを表示 && mmSyousai.Count > 0)
                            continue;
                    }
                    treeListView.Nodes[nodeIndex].SubItems.Add(subItem);
                    addNodes = true;
                    break;
                }
            }
            else
            {
                treeListView.Nodes[nodeIndex].SubItems.Add(subItem);
                addNodes = true;
            }

            if (対象回答詳細品目s != null)
            {
                int subNodeIndex = 0;
                foreach (OdMmShousaiItem mmSyousai in 依頼品目.詳細品目s)
                {
                    //===========================
                    // 2014.1 [2013年度改造]
                    if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                    {
                        if (Enum表示方式 == 表示方式enum.Zero以外を表示 && mmSyousai.Count < 1)
                            continue;
                        if (Enum表示方式 == 表示方式enum.Zeroを表示 && mmSyousai.Count > 0)
                            continue;
                    }

                    subItem = null;
                    foreach (OdMkShousaiItem mkShousai in 対象回答詳細品目s)
                    {
                        if (mmSyousai.OdMmShousaiItemID == mkShousai.OdMmShousaiItemID)
                        {
                            subItem = makeCustomerDataSubItem(mkShousai.Count.ToString());
                            break;
                        }
                    }
                    if (subItem == null)
                    {
                        subItem = makeCustomerDataSubItem("-");
                    }
                    treeListView.Nodes[nodeIndex].Nodes[subNodeIndex].SubItems.Add(subItem);
                    subNodeIndex++;
                }
            }
            else
            {
                foreach (OdMmShousaiItem mmSyousai in 依頼品目.詳細品目s)
                {
                    int subNodeIndex = 0;
                    subItem = makeCustomerDataSubItem("-");
                    treeListView.Nodes[nodeIndex].Nodes[subNodeIndex].SubItems.Add(subItem);
                    subNodeIndex++;
                }
            }

            //nodeIndex++;
            if (addNodes)
            {
                nodeIndex++;
            }
        }
        private TreeListViewSubItem makeCustomerDataSubItem(string text)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem(text);
            subItem.StyleFromParent = false;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            subItem.NormalStyle.BackColor = readOnlyBgColor;
            subItem.HoverStyle = subItem.NormalStyle;
            subItem.FormatStyle.Font = defaultFont;

            return subItem;
        }
    }
}
