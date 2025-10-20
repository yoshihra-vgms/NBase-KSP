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

    public class ItemTreeListView手配依頼 : ItemTreeListView
    {
        public enum 表示方式enum { Zeroを表示, Zero以外を表示 };
        public 表示方式enum Enum表示方式 = 表示方式enum.Zeroを表示;

        private string ThiIraiSbtID;
        private NodeController NodeControllers = null;

        public bool editable = true;//2021/10/01

        public ItemTreeListView手配依頼(string ThiIraiSbtID, TreeListView treeListView, bool CheckBoxes)
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
        public override void NodesClear()
        {
            base.NodesClear();
            NodeControllers.Clear();
        }
        public override void OnTextChanged(object sender, EventArgs e)
        {
            if (Enabled == false)
                return;

            if (sender is TextBox)
            {
                base.OnTextChanged(sender, e);

                try
                {
                    TextBox textBox = (TextBox)sender;
                    TreeListViewSubItem subItem = (TreeListViewSubItem)textBox.Tag;
                    TreeListViewNode node = subItem.Parent;
                    string text = subItem.Text;
                    int subItemNo = int.Parse(subItem.Key);

                    string key = NodeControllers.GetSecondKey(node);
                    OdThiShousaiItem syousai = NodeControllers.GetSecondInfo(key) as OdThiShousaiItem;
                    if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                    {
                        if (subItemNo == 3)
                        {
                            if (text == null || text.Length == 0)
                            {
                                syousai.Sateisu = int.MinValue;
                            }
                            else
                            {
                                syousai.Sateisu = int.Parse(text);
                            }
                        }
                        else
                        {
                            if (text == null || text.Length == 0)
                            {
                                syousai.Tanka = int.MinValue;
                            }
                            else
                            {
                                syousai.Tanka = (int)NBaseCommon.Common.金額表示を数値へ変換(text);
                            }
                            node.SubItems[4].Text = NBaseCommon.Common.金額出力((decimal)syousai.Tanka);
                        }
                    }
                    else
                    {
                        if (text == null || text.Length == 0)
                        {
                            syousai.Sateisu = int.MinValue;
                        }
                        else
                        {
                            syousai.Sateisu = int.Parse(text);
                        }
                    }
                    if (Hachu.Common.CommonDefine.Is新規(syousai.OdThiShousaiItemID) == false)
                    {
                        string id = Hachu.Common.CommonDefine.RemovePrefix(syousai.OdThiShousaiItemID);
                        syousai.OdThiShousaiItemID = Hachu.Common.CommonDefine.Prefix変更 + id;
                    }
                }
                catch
                {
                }
            }
        }

        public void AddNodes(List<Item手配依頼品目> 品目s)
        {
            AddNodes(false, 品目s);
        }
        public void AddNodes(bool viewHeader, List<Item手配依頼品目> 品目s)
        {
            this.viewHeader = viewHeader;

            TreeListViewNode headerNode = null;
            string header = "";
            int no = 0;

            treeListView.SuspendUpdate();
            foreach (Item手配依頼品目 品目 in 品目s)
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
        public void AddNodes(ref int no, Item手配依頼品目 品目)
        {
            TreeListViewNode node = MakeNode(品目.品目);
            //treeListView.Nodes.Add(node);

            //NodeControllers.SetNode(品目.品目.OdThiItemID, 品目, node);

            List<TreeListViewNode> subNodeList = new List<TreeListViewNode>();//miho
            foreach (OdThiShousaiItem 詳細品目 in 品目.詳細品目s)
            {
                if (詳細品目.CancelFlag == 1)
                    continue;

                //===========================
                // 2014.1 [2013年度改造]
                if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                {
                    if (Enum表示方式 == 表示方式enum.Zero以外を表示 && 詳細品目.Sateisu < 1)
                        continue;
                    if (Enum表示方式 == 表示方式enum.Zeroを表示 && 詳細品目.Sateisu > 0)
                        continue;
                }

                no++;
                TreeListViewNode subNode = MakeSubNode(no, 詳細品目);
                node.Nodes.Add(subNode);

                NodeControllers.SetSubNodes(品目.品目.OdThiItemID, 詳細品目.OdThiShousaiItemID, 詳細品目, subNode);

                //開く m.yoshihara
                //treeListView.EnsureVisible(subNode);
                subNodeList.Add(subNode);
            }

            //===========================
            // 2014.1 [2013年度改造]
            if (ThiIraiSbtID != NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                treeListView.Nodes.Add(node);

                NodeControllers.SetNode(品目.品目.OdThiItemID, 品目, node);
            }
            else
            {
                if (node.Nodes.Count > 0)
                {
                    treeListView.Nodes.Add(node);

                    NodeControllers.SetNode(品目.品目.OdThiItemID, 品目, node);
                }
            }

            // 全部、開く 使えなくなった模様 m.yoshihara
            //treeListView.ExpandAll();
            treeListView.EnsureVisible(node);
            foreach (TreeListViewNode nd in subNodeList)
            {
                treeListView.EnsureVisible(nd);
            }
            


        }
        public void AddNodes(ref int no, ref string header, ref TreeListViewNode headerNode, Item手配依頼品目 品目)
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

            NodeControllers.SetNode(品目.品目.OdThiItemID, 品目, node);


            List<TreeListViewNode> subNodeList = new List<TreeListViewNode>();//miho

            foreach (OdThiShousaiItem 詳細品目 in 品目.詳細品目s)
            {
                if (詳細品目.CancelFlag == 1)
                    continue;

                no++;
                TreeListViewNode subNode = MakeSubNode(no, 詳細品目);
                
                node.Nodes.Add(subNode);

                NodeControllers.SetSubNodes(品目.品目.OdThiItemID, 詳細品目.OdThiShousaiItemID, 詳細品目, subNode);

                //開く m.yoshihara
                //treeListView.EnsureVisible(subNode);
                subNodeList.Add(subNode);
            }

            // 全部、開く 使えなくなった模様 m.yoshihara
            //treeListView.ExpandAll(); 
            treeListView.EnsureVisible(node);
            foreach (TreeListViewNode nd in subNodeList)
            {
                treeListView.EnsureVisible(nd);
            }
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

            if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            {
                // 種別
                AddSubItem(node, "", true);
            }

            // 単位
            AddSubItem(node, "", true);
            // 在庫数
            AddSubItem(node, "", true);
            // 依頼数
            AddSubItem(node, "", true);
            // 査定数
            AddSubItem(node, "", true);
            // 添付
            AddSubItem(node, "", true);
            // 備考
            AddSubItem(node, "", true);

            return node;
        }
        private TreeListViewNode MakeNode(OdThiItem item)
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

            if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            {
                // 種別
                AddSubItem(node, "", true);
            }

            // 単位
            AddSubItem(node, "", true);
            if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                // 数量
                AddSubItem(node, "", true);
                // 単価
                AddSubItem(node, "", true);
            }
            else
            {
                // 在庫数
                AddSubItem(node, "", true);
                // 依頼数
                AddSubItem(node, "", true);
                // 査定数
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
                // 備考
                AddSubItem(node, "", true);
            }

            return node;
        }
        private TreeListViewNode MakeSubNode(int no, OdThiShousaiItem syousai)
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
            if (ThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                // 数量(査定数)
                string sateisu = "";
                if (syousai.Sateisu > -1)
                {
                    sateisu = syousai.Sateisu.ToString();
                }
                AddSubItem(node, sateisu, false);
                // 単価
                decimal tanka = -1;
                string tankaStr = "";
                if (syousai.Tanka > -1)
                {
                    tanka = syousai.Tanka;
                    tankaStr = NBaseCommon.Common.金額出力((int)tanka);
                }
                AddSubItem(node, tankaStr, tanka.ToString(), Hachu.Common.CommonDefine.TreeListViewColumnLength単価, false);
            }
            else
            {
                // 在庫数
                string zaiko = "";
                if (syousai.ZaikoCount > -1)
                {
                    zaiko = syousai.ZaikoCount.ToString();
                }
                AddSubItem(node, zaiko, true);
                // 依頼数
                AddSubItem(node, syousai.Count.ToString(), true);
                // 査定数
                string sateisu = "";
                if (syousai.Sateisu > -1)
                {
                    sateisu = syousai.Sateisu.ToString();
                }
                //AddSubItem(node, sateisu, false);
                AddSubItem(node, sateisu, !editable);//2021/10/01
                // 添付
                if (syousai.OdAttachFileID != null && syousai.OdAttachFileID.Length > 0)
                {
                    AddLinkItem(node, syousai.OdAttachFileID);
                }
                else
                {
                    AddSubItem(node, "", true);
                }
                // 備考
                AddSubItem(node, syousai.Bikou, true);
            }

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
        public Item手配依頼品目 GetTopInfo(string key)
        {
            return NodeControllers.GetTopInfo(key) as Item手配依頼品目;
        }
        public OdThiShousaiItem GetSecondInfo(string key)
        {
            return NodeControllers.GetSecondInfo(key) as OdThiShousaiItem;
        }
        public Item手配依頼品目 GetParentInfo(string key)
        {
            return NodeControllers.GetParentInfo(key) as Item手配依頼品目;
        }

        public List<Item手配依頼品目> GetCheckedNodes()
        {
            List<Item手配依頼品目> ret = new List<Item手配依頼品目>();
            if (viewHeader)
            {
                foreach (TreeListViewNode headerNode in treeListView.Nodes)
                {
                    foreach (TreeListViewNode itemNode in headerNode.Nodes)
                    {
                        Item手配依頼品目 item = GetCheckedNodeItem(itemNode);
                        if (item != null)
                            ret.Add(item);
                    }
                }
            }
            else
            {
                foreach (TreeListViewNode itemNode in treeListView.Nodes)
                {
                    Item手配依頼品目 item = GetCheckedNodeItem(itemNode);
                    if (item != null)
                        ret.Add(item);
                }
            }
            return ret;
        }
        public Item手配依頼品目 GetCheckedNodeItem(TreeListViewNode itemNode)
        {
            if (itemNode.CheckState == CheckState.Unchecked)
            {
                return null;
            }
            string topKey = NodeControllers.GetTopKey(itemNode);
            Item手配依頼品目 topInfo = NodeControllers.GetTopInfo(topKey) as Item手配依頼品目;
            Item手配依頼品目 retItem = new Item手配依頼品目();
            retItem.品目 = topInfo.品目;

            foreach (TreeListViewNode sousaiNode in itemNode.Nodes)
            {
                if (sousaiNode.CheckState == CheckState.Unchecked)
                {
                    continue;
                }
                string secondKey = NodeControllers.GetSecondKey(sousaiNode);
                OdThiShousaiItem secondInfo = NodeControllers.GetSecondInfo(secondKey) as OdThiShousaiItem;
                retItem.詳細品目s.Add(secondInfo);
            }

            return retItem;
        }

        public bool DoubleClick(OdThi 対象手配依頼, ref List<Item手配依頼品目> 手配品目s, ref List<Item手配依頼品目> 削除手配品目s)
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

            Item手配依頼品目 手配品目 = null;
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
            OdThiItem odThiItem = 手配品目.品目.Clone();
            odThiItem.OdThiShousaiItems.Clear();
            foreach (OdThiShousaiItem shousaiItem in 手配品目.詳細品目s)
            {
                odThiItem.OdThiShousaiItems.Add(shousaiItem.Clone());
            }

            // 品目編集Formを開く
            List<OdAttachFile> odAttachFiles = 手配品目.添付Files;
            Form form = new 品目編集Form((int)品目編集Form.EDIT_MODE.変更, 対象手配依頼.MsThiIraiSbtID, 対象手配依頼.MsVesselID, ref odThiItem, ref odAttachFiles);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return false;
            }

            #region
            if (odThiItem.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
            {
                // 削除
                string id = 手配品目.品目.OdThiItemID;
                id = Hachu.Common.CommonDefine.RemovePrefix(id);
                if (Hachu.Common.CommonDefine.Is新規(id) == false)
                {
                    削除手配品目s.Add(手配品目);
                }
                int index = 0;
                foreach (Item手配依頼品目 品目 in 手配品目s)
                {
                    if (品目.品目.OdThiItemID == 手配品目.品目.OdThiItemID)
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
                string id = 手配品目.品目.OdThiItemID;
                int index = 0;
                foreach (Item手配依頼品目 品目 in 手配品目s)
                {
                    if (品目.品目.OdThiItemID == id)
                    {
                        break;
                    }
                    index++;
                }
                Item手配依頼品目 編集品目 = new Item手配依頼品目();
                編集品目.品目 = odThiItem;
                編集品目.詳細品目s.Clear();
                編集品目.添付Files = odAttachFiles;
                foreach (OdThiShousaiItem shousaiItem in odThiItem.OdThiShousaiItems)
                {
                    if (shousaiItem.CancelFlag == 1)
                    {
                        編集品目.削除詳細品目s.Add(shousaiItem);
                    }
                    else
                    {
                        if (Hachu.Common.CommonDefine.Is新規(shousaiItem.OdThiShousaiItemID) == false)
                        {
                            id = Hachu.Common.CommonDefine.RemovePrefix(shousaiItem.OdThiShousaiItemID);
                            shousaiItem.OdThiShousaiItemID = Hachu.Common.CommonDefine.Prefix変更 + id;
                        }
                        編集品目.詳細品目s.Add(shousaiItem);
                    }
                }
                // 2009.11.25:aki 画面上で追加→登録→ダブルクリックできたときに
                //                変更ではなく新規のままにしないとＤＢに登録されないことの修正
                //id = Hachu.Common.CommonDefine.RemovePrefix(編集品目.品目.OdThiItemID);
                //編集品目.品目.OdThiItemID = Hachu.Common.CommonDefine.Prefix変更 + id;
                if (Hachu.Common.CommonDefine.Is新規(編集品目.品目.OdThiItemID) == false)
                {
                    id = Hachu.Common.CommonDefine.RemovePrefix(編集品目.品目.OdThiItemID);
                    編集品目.品目.OdThiItemID = Hachu.Common.CommonDefine.Prefix変更 + id;
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
                    foreach (Item手配依頼品目 品目 in 手配品目s)
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
