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
    public class ItemTreeListView新規発注 : ItemTreeListView
    {
        private NodeController NodeControllers = null;

        public ItemTreeListView新規発注(TreeListView treeListView)
            : base(treeListView)
        {
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

        public void AddNodes(List<Item見積回答品目> 品目s)
        {
            AddNodes(false, 品目s);
        }
        public void AddNodes(bool viewHeader, List<Item見積回答品目> 品目s)
        {
            this.viewHeader = viewHeader;

            TreeListViewNode headerNode = null;
            string header = "";
            int no = 0;

            treeListView.SuspendUpdate();
            foreach (Item見積回答品目 品目 in 品目s)
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
        public void AddNodes(ref int no, Item見積回答品目 品目)
        {
            TreeListViewNode node = MakeNode(品目.品目);
            treeListView.Nodes.Add(node);

            NodeControllers.SetNode(品目.品目.OdMkItemID, 品目, node);

            foreach (OdMkShousaiItem 詳細品目 in 品目.詳細品目s)
            {
                if (詳細品目.CancelFlag == 1)
                    continue;

                no++;
                TreeListViewNode subNode = MakeSubNode(no, 詳細品目);
                node.Nodes.Add(subNode);

                NodeControllers.SetSubNodes(品目.品目.OdMkItemID, 詳細品目.OdMkShousaiItemID, 詳細品目, subNode);

                //開く m.yoshihara
                treeListView.EnsureVisible(subNode);
            }

            // 全部、開く 使えなくなった模様 m.yoshihara
            //treeListView.ExpandAll();

            treeListView.EnsureVisible(node);
        }
        public void AddNodes(ref int no, ref string header, ref TreeListViewNode headerNode, Item見積回答品目 品目)
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

            NodeControllers.SetNode(品目.品目.OdMkItemID, 品目, node);

            foreach (OdMkShousaiItem 詳細品目 in 品目.詳細品目s)
            {
                if (詳細品目.CancelFlag == 1)
                    continue;

                no++;
                TreeListViewNode subNode = MakeSubNode(no, 詳細品目);
                node.Nodes.Add(subNode);

                NodeControllers.SetSubNodes(品目.品目.OdMkItemID, 詳細品目.OdMkShousaiItemID, 詳細品目, subNode);

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
            // 在庫数
            AddSubItem(node, "", true);
            // 回答数
            AddSubItem(node, "", true);
            // 単価
            AddSubItem(node, "", true);
            // 金額
            AddSubItem(node, "", true);
            // 備考
            AddSubItem(node, "", true);

            return node;
        }
        private TreeListViewNode MakeNode(OdMkItem item)
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
            // 発注数
            AddSubItem(node, "", true);
            // 単価
            AddSubItem(node, "", true);
            // 金額
            AddSubItem(node, "", true);
            // 備考
            AddSubItem(node, "", true);

            return node;
        }
        private TreeListViewNode MakeSubNode(int no, OdMkShousaiItem syousai)
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
            // 発注数
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
                tankaStr = NBaseCommon.Common.金額出力((int)tanka);
            }
            AddSubItem(node, tankaStr, tanka.ToString(), Hachu.Common.CommonDefine.TreeListViewColumnLength単価, true);
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

        public string GetTopKey()
        {
            string key = null;
            TreeListViewNode selectedNode = treeListView.SelectedNode;
            if (selectedNode == null)
            {
                return null;
            }
            if (viewHeader)
            {
                if (selectedNode.Parent != null && selectedNode.Parent.Parent == null)
                {
                    key = NodeControllers.GetTopKey(selectedNode);
                }
            }
            else
            {
                if (selectedNode.Parent == null)
                {
                    key = NodeControllers.GetTopKey(selectedNode);
                }
            }
            return key;
        }
        public string GetSecondKey()
        {
            string key = null;
            TreeListViewNode selectedNode = treeListView.SelectedNode;
            if (selectedNode == null)
            {
                return null;
            }
            if (selectedNode.Parent == null)
            {
                return null;
            }
            if (viewHeader)
            {
                if (selectedNode.Parent.Parent == null)
                {
                    return null;
                }
            }
            key = NodeControllers.GetSecondKey(selectedNode);
            return key;
        }
        public Item見積回答品目 GetTopInfo(string key)
        {
            return NodeControllers.GetTopInfo(key) as Item見積回答品目;
        }
        public OdMkShousaiItem GetSecondInfo(string key)
        {
            return NodeControllers.GetSecondInfo(key) as OdMkShousaiItem;
        }
        public Item見積回答品目 GetParentInfo(string key)
        {
            return NodeControllers.GetParentInfo(key) as Item見積回答品目;
        }

        #region 使用しなくなったコード(2009.09.21:aki)
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
        //    Item見積回答品目 品目 = info as Item見積回答品目;
        //    TreeListViewNode node = MakeNode(品目.品目);
        //    treeListView.Nodes.Insert(index, node);

        //    NodeControllers.SetNode(品目.品目.OdMkItemID, 品目, node);

        //    int no = 0;
        //    foreach (OdMkShousaiItem 詳細品目 in 品目.詳細品目s)
        //    {
        //        no++;
        //        TreeListViewNode subNode = MakeSubNode(no, 詳細品目);
        //        node.Nodes.Add(subNode);

        //        NodeControllers.SetSubNodes(品目.品目.OdMkItemID, 詳細品目.OdMkShousaiItemID, 詳細品目, subNode);
        //    }

        //    // 全部、開く
        //    treeListView.ExpandAll();
        //}
        //public void ReplaceSecondNode(string key, int index, object info)
        //{
        //    // 親情報を取得しておく
        //    Item見積回答品目 品目 = NodeControllers.GetParentInfo(key) as Item見積回答品目;
        //    TreeListViewNode parentNode = NodeControllers.GetParentNode(key);

        //    // 変更前のノードを削除する
        //    RemoveSecondNode(key);

        //    // 変更後のノードを追加する
        //    OdMkShousaiItem 詳細品目 = info as OdMkShousaiItem;
        //    TreeListViewNode subNode = MakeSubNode(index+1, 詳細品目);
        //    parentNode.Nodes.Insert(index, subNode);

        //    NodeControllers.SetSubNodes(品目.品目.OdMkItemID, 詳細品目.OdMkShousaiItemID, 詳細品目, subNode);
        //}
        #endregion

        public decimal 見積合計()
        {
            List<object> objs = NodeControllers.GetAllSecondInfo();
            decimal kingaku = 0;
            foreach (object obj in objs)
            {
                OdMkShousaiItem shousaiItem = obj as OdMkShousaiItem;
                if (shousaiItem.Count > 0 && shousaiItem.Tanka > 0)
                {
                    kingaku += shousaiItem.Count * shousaiItem.Tanka;
                }
            }
            return kingaku;
        }
        public bool すべての回答が入力されたかチェック()
        {
            List<object> objs = NodeControllers.GetAllSecondInfo();
            bool ret = true;
            foreach (object obj in objs)
            {
                OdMkShousaiItem shousaiItem = obj as OdMkShousaiItem;
                if (!(shousaiItem.Count >= 0 && shousaiItem.Tanka >= 0))
                {
                    ret = false;
                }
            }
            return ret;
        }

        public bool DoubleClick(OdMk 対象見積回答, ref List<Item見積回答品目> 手配品目s, ref List<Item見積回答品目> 削除手配品目s)
        {
            if (Enabled == false)
                return false;

            if (CheckBoxEvent)
            {
                CheckBoxEvent = false;
                return false;
            }
            if (ExpandCollapseEvent)
            {
                ExpandCollapseEvent = false;
                return false;
            }

            Item見積回答品目 手配品目 = null;
            string key = GetTopKey();
            if (key != null)
            {
                手配品目 = GetTopInfo(key);
            }
            else
            {
                key = GetSecondKey();
                if (key != null)
                {
                    手配品目 = GetParentInfo(key);
                }
            }
            if (手配品目 == null)
            {
                return false;
            }

            // 対象となる品目の準備
            OdMkItem odMkItem = 手配品目.品目.Clone();
            odMkItem.OdMkShousaiItems.Clear();
            foreach (OdMkShousaiItem shousaiItem in 手配品目.詳細品目s)
            {
                odMkItem.OdMkShousaiItems.Add(shousaiItem.Clone());
            }

            // 品目編集Formを開く
            Form form = new 品目編集Form((int)品目編集Form.EDIT_MODE.変更, 対象見積回答.MsThiIraiSbtID, 対象見積回答.MsVesselID, ref odMkItem, 1);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return false;
            }

            #region
            if (odMkItem.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
            {
                // 削除
                string id = 手配品目.品目.OdMkItemID;
                id = Hachu.Common.CommonDefine.RemovePrefix(id);
                if (Hachu.Common.CommonDefine.Is新規(id) == false)
                {
                    削除手配品目s.Add(手配品目);
                }
                int index = 0;
                foreach (Item見積回答品目 品目 in 手配品目s)
                {
                    if (品目.品目.OdMkItemID == 手配品目.品目.OdMkItemID)
                    {
                        break;
                    }
                    index++;
                }
                手配品目s.RemoveAt(index);
            }
            else
            {
                // リスト内の位置
                string id = 手配品目.品目.OdMkItemID;
                int index = 0;
                foreach (Item見積回答品目 品目 in 手配品目s)
                {
                    if (品目.品目.OdMkItemID == id)
                    {
                        break;
                    }
                    index++;
                }

                Item見積回答品目 編集品目 = new Item見積回答品目();
                編集品目.品目 = odMkItem;
                編集品目.詳細品目s.Clear();
                foreach (OdMkShousaiItem shousaiItem in odMkItem.OdMkShousaiItems)
                {
                    if (shousaiItem.CancelFlag == 1)
                    {
                        編集品目.削除詳細品目s.Add(shousaiItem);
                    }
                    else
                    {
                        if (Hachu.Common.CommonDefine.Is新規(shousaiItem.OdMkShousaiItemID) == false)
                        {
                            id = Hachu.Common.CommonDefine.RemovePrefix(shousaiItem.OdMkShousaiItemID);
                            shousaiItem.OdMkShousaiItemID = Hachu.Common.CommonDefine.Prefix変更 + id;
                        }
                        編集品目.詳細品目s.Add(shousaiItem);
                    }
                }
                // 2009.11.25:aki 画面上で追加→登録→ダブルクリックできたときに
                //                変更ではなく新規のままにしないとＤＢに登録されないことの修正
                //id = Hachu.Common.CommonDefine.RemovePrefix(編集品目.品目.OdMkItemID);
                //編集品目.品目.OdMkItemID = Hachu.Common.CommonDefine.Prefix変更 + id;
                if (Hachu.Common.CommonDefine.Is新規(編集品目.品目.OdMkItemID) == false)
                {
                    id = Hachu.Common.CommonDefine.RemovePrefix(編集品目.品目.OdMkItemID);
                    編集品目.品目.OdMkItemID = Hachu.Common.CommonDefine.Prefix変更 + id;
                }

                if (手配品目s[index].品目.Header == 編集品目.品目.Header)
                {
                    // ヘッダが編集されていなければ、そのままの位置に
                    手配品目s[index] = 編集品目;
                }
                else
                {
                    // ヘッダが編集されている場合、位置を入れ替える
                    手配品目s.RemoveAt(index);
                    int insertPos = 0;
                    bool sameHeader = false;
                    foreach (Item見積回答品目 品目 in 手配品目s)
                    {
                        if (品目.品目.Header == 編集品目.品目.Header)
                        {
                            sameHeader = true;
                        }
                        else if (sameHeader)
                        {
                            break;
                        }
                        insertPos++;
                    }
                    if (insertPos >= 手配品目s.Count)
                    {
                        手配品目s.Add(編集品目);
                    }
                    else
                    {
                        手配品目s.Insert(insertPos, 編集品目);
                    }
                }
            }
            #endregion
            NodesClear();
            AddNodes(true, 手配品目s);

            return true;
        }
    }
}
