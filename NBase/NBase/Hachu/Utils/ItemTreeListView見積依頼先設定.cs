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
    public class ItemTreeListView見積依頼先設定 : ItemTreeListView
    {
        private string ThiIraiSbtID;
        private NodeController NodeControllers = null;

        //public ItemTreeListView見積依頼先設定(TreeListView treeListView, bool CheckBoxes)
        public ItemTreeListView見積依頼先設定(string ThiIraiSbtID, TreeListView treeListView, bool CheckBoxes)
            : base(treeListView)
        {
            this.ThiIraiSbtID = ThiIraiSbtID;
            treeListView.CheckBoxes = CheckBoxes;
            treeListView.AfterCheck += new LidorSystems.IntegralUI.ObjectEventHandler(OnAfterCheck);

            NodeControllers = new NodeController();
        }
        public override void Clear()
        {
            base.Clear();
            NodeControllers.Clear();
        }
        public override void OnTextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                base.OnTextChanged(sender, e);

                try
                {
                    TextBox textBox = (TextBox)sender;
                    TreeListViewSubItem subItem = (TreeListViewSubItem)textBox.Tag;
                    TreeListViewNode node = subItem.Parent;
                    string text = subItem.Text;

                    string key = NodeControllers.GetSecondKey(node);
                    OdMmShousaiItem syousai = NodeControllers.GetSecondInfo(key) as OdMmShousaiItem;
                    if (text == null || text.Length == 0)
                    {
                        syousai.Count = int.MinValue;
                    }
                    else
                    {
                        syousai.Count = int.Parse(text);
                    }
                }
                catch
                {
                }
            }
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

            treeListView.SuspendUpdate();
            foreach (Item見積依頼品目 品目 in 品目s)
            {
                if (品目.品目.CancelFlag == 1)
                    continue;
                if (this.viewHeader)
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
            treeListView.ResumeUpdate();
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
                    if (詳細品目.Count < 1 )//&& Enum表示方式 == 表示方式enum.Zero以外を表示)
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
            treeListView.EnsureVisible(node);
        }
        
        private TreeListViewNode MakeHeaderNode(string headerText)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 
            if (treeListView.CheckBoxes)
            {
                node.Checked = true;
                AddSubItem(node, "", true);
            }
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
            AddSubItem(node, "", true);

            return node;
        }
        private TreeListViewNode MakeNode(OdMmItem item)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 
            if (treeListView.CheckBoxes)
            {
                node.Checked = true;
                AddSubItem(node, "", true);
            }
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
                // 添付するかしないか
                AddCheckBoxItem(node, item.IsAttached);
            }
            else
            {
                AddSubItem(node, "", true);
                AddSubItem(node, "", true);
            }

            return node;
        }
        private TreeListViewNode MakeSubNode(int no, OdMmShousaiItem syousai)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 
            if (treeListView.CheckBoxes)
            {
                node.Checked = true;
                AddSubItem(node, "", true);
            }
            // No
            AddSubItem(node, no.ToString(), true);
            // 詳細品目名
            AddSubItemAsShousai(node, syousai.ShousaiItemName, true);
            // 単位
            AddSubItem(node, syousai.MsTaniName, true);
            // 依頼数
            AddSubItem(node, syousai.Count.ToString(), false);
            // 添付
            if (syousai.OdAttachFileID != null && syousai.OdAttachFileID.Length > 0)
            {
                AddLinkItem(node, syousai.OdAttachFileID);
                // 添付するかしないか
                AddCheckBoxItem(node, syousai.IsAttached);
            }
            else
            {
                AddSubItem(node, "", true);
                AddSubItem(node, "", true);
            }

            return node;
        }
        
        public List<Item見積回答品目> GetCheckedNodes()
        {
            List<Item見積回答品目> ret = new List<Item見積回答品目>();
            if (viewHeader)
            {
                foreach (TreeListViewNode headerNode in treeListView.Nodes)
                {
                    foreach (TreeListViewNode itemNode in headerNode.Nodes)
                    {
                        Item見積回答品目 item = GetCheckedNodeItem(itemNode);
                        if (item != null)
                            ret.Add(item);
                    }
                }
            }
            else
            {
                foreach (TreeListViewNode itemNode in treeListView.Nodes)
                {
                    Item見積回答品目 item = GetCheckedNodeItem(itemNode);
                    if (item != null)
                        ret.Add(item);
                }
            }
            return ret;
        }
        public Item見積回答品目 GetCheckedNodeItem(TreeListViewNode itemNode)
        {
            if (itemNode.CheckState == CheckState.Unchecked)
            {
                return null;
            }
            string topKey = NodeControllers.GetTopKey(itemNode);
            Item見積依頼品目 topInfo = NodeControllers.GetTopInfo(topKey) as Item見積依頼品目;
            Item見積回答品目 retItem = new Item見積回答品目();
            retItem.品目 = new OdMkItem();
            // retItem.品目.OdMkItemID  // DB登録時に割当
            // retItem.品目.OdMkID  // DB登録時に割当
            retItem.品目.Header = topInfo.品目.Header;
            retItem.品目.OdMmItemID = topInfo.品目.OdMmItemID;
            retItem.品目.MsItemSbtID = topInfo.品目.MsItemSbtID;
            retItem.品目.ItemName = topInfo.品目.ItemName;
            retItem.品目.Bikou = topInfo.品目.Bikou;

            if (topInfo.品目.IsAttached)
            {
                retItem.品目.OdAttachFileID = topInfo.品目.OdAttachFileID;
            }
            else
            {
                retItem.品目.OdAttachFileID = null;
            }

            foreach (TreeListViewNode secondNode in itemNode.Nodes)
            {
                if (secondNode.CheckState == CheckState.Unchecked)
                {
                    continue;
                }
                string secondKey = NodeControllers.GetSecondKey(secondNode);
                OdMmShousaiItem secondInfo = NodeControllers.GetSecondInfo(secondKey) as OdMmShousaiItem;
                OdMkShousaiItem 見積回答詳細品目 = new OdMkShousaiItem();
                // 見積回答詳細品目.OdMkShousaiItemID  // DB登録時に割当
                // 見積回答詳細品目.OdMkItemID  // DB登録時に割当
                見積回答詳細品目.OdMmShousaiItemID = secondInfo.OdMmShousaiItemID;
                見積回答詳細品目.ShousaiItemName = secondInfo.ShousaiItemName;
                見積回答詳細品目.MsVesselItemID = secondInfo.MsVesselItemID;
                見積回答詳細品目.MsLoID = secondInfo.MsLoID;
                見積回答詳細品目.Count = secondInfo.Count;
                見積回答詳細品目.MsTaniID = secondInfo.MsTaniID;
                見積回答詳細品目.Bikou = secondInfo.Bikou;

                if (secondInfo.IsAttached)
                {
                    見積回答詳細品目.OdAttachFileID = secondInfo.OdAttachFileID;
                }
                else
                {
                    見積回答詳細品目.OdAttachFileID = null;
                }

                retItem.詳細品目s.Add(見積回答詳細品目);
            }
            return retItem;
        }

        public override void OnCheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                base.OnTextChanged(sender, e);

                try
                {
                    CheckBox checkBox = (CheckBox)sender;
                    TreeListViewSubItem subItem = (TreeListViewSubItem)checkBox.Tag;
                    TreeListViewNode node = subItem.Parent;

                    string key = NodeControllers.GetSecondKey(node);
                    if (key != null)
                    {
                        OdMmShousaiItem syousai = NodeControllers.GetSecondInfo(key) as OdMmShousaiItem;

                        syousai.IsAttached = checkBox.Checked;
                    }
                    else
                    {
                        key = NodeControllers.GetTopKey(node);
                        OdMmItem item = NodeControllers.GetTopInfo(key) as OdMmItem;

                        item.IsAttached = checkBox.Checked;
                    }
                }
                catch
                {
                }
            }
        }

    }
}
