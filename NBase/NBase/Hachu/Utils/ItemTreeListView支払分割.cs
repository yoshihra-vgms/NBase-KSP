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
    public class ItemTreeListView支払分割 : ItemTreeListView
    {
        private NodeController NodeControllers = null;
        private bool InitialChecked = false;

        public ItemTreeListView支払分割(TreeListView treeListView, bool CheckBoxes)
            : base(treeListView)
        {
            treeListView.CheckBoxes = CheckBoxes;
            treeListView.AfterCheck += new LidorSystems.IntegralUI.ObjectEventHandler(OnAfterCheck);

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

        public void AddNodes(List<Item支払品目> 品目s)
        {
            AddNodes(false, 品目s);
        }
        public void AddNodes(bool viewHeader, List<Item支払品目> 品目s)
        {
            this.viewHeader = viewHeader;

            TreeListViewNode headerNode = null;
            string header = "";
            int no = 0;

            treeListView.SuspendUpdate();
            foreach (Item支払品目 品目 in 品目s)
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
        public void AddNodes(ref int no, Item支払品目 品目)
        {
            TreeListViewNode node = MakeNode(品目.品目);
            treeListView.Nodes.Add(node);

            NodeControllers.SetNode(品目.品目.OdShrItemID, 品目, node);

            foreach (OdShrShousaiItem 詳細品目 in 品目.詳細品目s)
            {
                if (詳細品目.CancelFlag == 1)
                    continue;

                no++;
                TreeListViewNode subNode = MakeSubNode(no, 詳細品目);
                node.Nodes.Add(subNode);

                NodeControllers.SetSubNodes(品目.品目.OdShrItemID, 詳細品目.OdShrShousaiItemID, 詳細品目, subNode);

                //開く m.yoshihara
                treeListView.EnsureVisible(subNode);
            }

            // 全部、開く 使えなくなった模様 m.yoshihara
            //treeListView.ExpandAll();

            treeListView.EnsureVisible(node);
        }
        public void AddNodes(ref int no, ref string header, ref TreeListViewNode headerNode, Item支払品目 品目)
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

            NodeControllers.SetNode(品目.品目.OdShrItemID, 品目, node);

            foreach (OdShrShousaiItem 詳細品目 in 品目.詳細品目s)
            {
                if (詳細品目.CancelFlag == 1)
                    continue;

                no++;
                TreeListViewNode subNode = MakeSubNode(no, 詳細品目);
                node.Nodes.Add(subNode);

                NodeControllers.SetSubNodes(品目.品目.OdShrItemID, 詳細品目.OdShrShousaiItemID, 詳細品目, subNode);

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
                node.Checked = InitialChecked;
                AddSubItem(node, "", true);
            }
            // No
            AddSubItem(node, "", true);
            // ヘッダ
            AddSubItemAsHeader(node, headerText, true);

            // 単位
            AddSubItem(node, "", true);
            // 数量
            AddSubItem(node, "", true);
            // 単価
            AddSubItem(node, "", true);
            // 金額
            AddSubItem(node, "", true);
            // 備考
            AddSubItem(node, "", true);

            return node;
        }
        private TreeListViewNode MakeNode(OdShrItem item)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 
            if (treeListView.CheckBoxes)
            {
                node.Checked = InitialChecked;
                AddSubItem(node, "", true);
            }
            // No
            AddSubItem(node, "", true);
            // 品目名
            string itemName = "";
            if (item.MsItemSbtID != null && item.MsItemSbtID.Length > 0)
            {
                itemName += item.MsItemSbtName + "：";
            }
            itemName += item.ItemName;
            AddSubItemAsItem(node, itemName, true);
            // 単位
            AddSubItem(node, "", true);
            // 数量
            AddSubItem(node, "", true);
            // 単価
            AddSubItem(node, "", true);
            // 金額
            AddSubItem(node, "", true);
            // 備考
            AddSubItem(node, "", true);

            return node;
        }
        private TreeListViewNode MakeSubNode(int no, OdShrShousaiItem syousai)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 
            if (treeListView.CheckBoxes)
            {
                node.Checked = InitialChecked;
                AddSubItem(node, "", true);
            }
            // No
            AddSubItem(node, no.ToString(), true);
            // 詳細品目名
            AddSubItemAsShousai(node, syousai.ShousaiItemName, true);
            // 単位
            AddSubItem(node, syousai.MsTaniName, true);
            // 数量
            decimal count = -1;
            string countStr = "";
            if (syousai.Count > -1)
            {
                count = syousai.Count;
                countStr = syousai.Count.ToString();
            }
            AddSubItem(node, countStr, true);
            // 単価
            decimal tanka = -1;
            string tankaStr = "";
            if (syousai.Tanka > -1)
            {
                tanka = syousai.Tanka;
                tankaStr = NBaseCommon.Common.金額出力(tanka);
            }
            AddSubItem(node, tankaStr, true);
            // 金額
            decimal kingaku = -1;
            string kingakuStr = "";
            if (count > -1 && tanka > -1)
            {
                kingaku = count * tanka;
                kingakuStr = NBaseCommon.Common.金額出力(kingaku);
            }
            AddSubItem(node, kingakuStr, true);
            // 備考
            AddSubItem(node, syousai.Bikou, true);

            return node;
        }


        #region 使用していないコード

        //public string GetTopKey()
        //{
        //    string key = null;
        //    TreeListViewNode selectedNode = treeListView.SelectedNode;
        //    if (selectedNode == null)
        //    {
        //        return null;
        //    }
        //    if (selectedNode.Parent == null)
        //    {
        //        key = NodeControllers.GetTopKey(selectedNode);
        //    }
        //    return key;
        //}
        //public string GetSecondKey()
        //{
        //    string key = null;
        //    TreeListViewNode selectedNode = treeListView.SelectedNode;
        //    if (selectedNode == null)
        //    {
        //        return null;
        //    }
        //    if (selectedNode.Parent == null)
        //    {
        //        return null;
        //    }
        //    key = NodeControllers.GetSecondKey(selectedNode);
        //    return key;
        //}
        //public 見積品目 GetTopInfo(string key)
        //{
        //    return NodeControllers.GetTopInfo(key) as 見積品目;
        //}
        //public OdThiShousaiItem GetSecondInfo(string key)
        //{
        //    return NodeControllers.GetSecondInfo(key) as OdThiShousaiItem;
        //}
        //public 見積品目 GetParentInfo(string key)
        //{
        //    return NodeControllers.GetParentInfo(key) as 見積品目;
        //}

        //public void RemoveTopNode(string key)
        //{
        //    // TreeListViewから削除
        //    TreeListViewNode node = NodeControllers.GetTopNode(key);
        //    treeListView.Nodes.Remove(node);

        //    // コントローラから削除
        //    NodeControllers.RemoveTopKey(key);
        //}
        //public void RemoveSecondNode(string key)
        //{
        //    // TreeListViewから削除
        //    TreeListViewNode parentNode = NodeControllers.GetParentNode(key);
        //    TreeListViewNode secondNode = NodeControllers.GetSecondNode(key);
        //    parentNode.Nodes.Remove(secondNode);

        //    // コントローラから削除
        //    NodeControllers.RemoveSecondKey(key);
        //}

        //public void ReplaceTopNode(string key, int index, object info)
        //{
        //    // 変更前のノードを削除する
        //    RemoveTopNode(key);

        //    // 変更後のノードを追加する
        //    見積品目 品目 = info as 見積品目;
        //    TreeListViewNode node = MakeNode(品目.品目);
        //    treeListView.Nodes.Insert(index, node);

        //    NodeControllers.SetNode(品目.品目.OdMmItemID, 品目, node);

        //    int no = 0;
        //    foreach (OdShrShousaiItem 詳細品目 in 品目.詳細品目s)
        //    {
        //        no++;
        //        TreeListViewNode subNode = MakeSubNode(no, 詳細品目);
        //        node.Nodes.Add(subNode);

        //        NodeControllers.SetSubNodes(品目.品目.OdMmItemID, 詳細品目.OdShrShousaiItemID, 詳細品目, subNode);
        //    }

        //    // 全部、開く
        //    treeListView.ExpandAll();
        //}
        //public void ReplaceSecondNode(string key, int index, object info)
        //{
        //    // 親情報を取得しておく
        //    見積品目 品目 = NodeControllers.GetParentInfo(key) as 見積品目;
        //    TreeListViewNode parentNode = NodeControllers.GetParentNode(key);

        //    // 変更前のノードを削除する
        //    RemoveSecondNode(key);

        //    // 変更後のノードを追加する
        //    OdShrShousaiItem 詳細品目 = info as OdShrShousaiItem;
        //    TreeListViewNode subNode = MakeSubNode(index+1, 詳細品目);
        //    parentNode.Nodes.Insert(index, subNode);

        //    NodeControllers.SetSubNodes(品目.品目.OdMmItemID, 詳細品目.OdShrShousaiItemID, 詳細品目, subNode);
        //}

        #endregion

        public List<Item支払品目> GetCheckedNodes()
        {
            List<Item支払品目> ret = new List<Item支払品目>();
            int itemShowOrder = 0;
            if (viewHeader)
            {
                foreach (TreeListViewNode headerNode in treeListView.Nodes)
                {
                    foreach (TreeListViewNode itemNode in headerNode.Nodes)
                    {
                        Item支払品目 item = GetCheckedNodeItem(itemNode);
                        if (item != null)
                        {
                            item.品目.ShowOrder = ++itemShowOrder;
                            ret.Add(item);
                        }
                    }
                }
            }
            else
            {
                foreach (TreeListViewNode itemNode in treeListView.Nodes)
                {
                    Item支払品目 item = GetCheckedNodeItem(itemNode);
                    item.品目.ShowOrder = ++itemShowOrder;
                    if (item != null)
                    {
                        item.品目.ShowOrder = ++itemShowOrder;
                        ret.Add(item);
                    }
                }
            }
            return ret;
        }
        public Item支払品目 GetCheckedNodeItem(TreeListViewNode itemNode)
        {
            if (itemNode.CheckState == CheckState.Unchecked)
            {
                return null;
            }
            string topKey = NodeControllers.GetTopKey(itemNode);
            Item支払品目 topInfo = NodeControllers.GetTopInfo(topKey) as Item支払品目;
            Item支払品目 retItem = new Item支払品目();
            retItem.品目 = new OdShrItem();
            retItem.品目.OdShrItemID = topInfo.品目.OdShrItemID;
            retItem.品目.OdShrID = topInfo.品目.OdShrID;
            retItem.品目.Header = topInfo.品目.Header;
            retItem.品目.MsItemSbtID = topInfo.品目.MsItemSbtID;
            retItem.品目.ItemName = topInfo.品目.ItemName;
            retItem.品目.Bikou = topInfo.品目.Bikou;

            int sousaiShowOrder = 0;
            foreach (TreeListViewNode secondNode in itemNode.Nodes)
            {
                if (secondNode.CheckState == CheckState.Unchecked)
                {
                    continue;
                }
                string secondKey = NodeControllers.GetSecondKey(secondNode);
                OdShrShousaiItem secondInfo = NodeControllers.GetSecondInfo(secondKey) as OdShrShousaiItem;
                OdShrShousaiItem 詳細品目 = new OdShrShousaiItem();
                詳細品目.OdShrShousaiItemID = secondInfo.OdShrShousaiItemID;
                詳細品目.OdShrItemID = secondInfo.OdShrItemID;
                詳細品目.ShousaiItemName = secondInfo.ShousaiItemName;
                詳細品目.MsVesselItemID = secondInfo.MsVesselItemID;
                詳細品目.MsLoID = secondInfo.MsLoID;
                詳細品目.Count = secondInfo.Count;
                詳細品目.Tanka = secondInfo.Tanka;
                詳細品目.MsTaniID = secondInfo.MsTaniID;
                詳細品目.Bikou = secondInfo.Bikou;
                詳細品目.ShowOrder = ++sousaiShowOrder;
                retItem.詳細品目s.Add(詳細品目);
            }
            return retItem;
        }
    }
}
