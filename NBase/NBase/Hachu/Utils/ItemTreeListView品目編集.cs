using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;
using NBaseData.DAC;
using Hachu.HachuManage;
using System.Drawing;

namespace Hachu.Utils
{

    public class ItemTreeListView品目編集 : ItemTreeListView
    {
        public string ThiIraiSbtID = null;

        private static string DefaultShousaiName = "（新規詳細品目）";
        public ItemTreeListView品目編集(TreeListView treeListView)
            : base (treeListView)
        {
            // スタイルは、全体に設定したもの
            StyleFromParent = true;

            // 通常時のバックグラウンド
            treeListView.FocusedNodeStyle.BackColor = Color.FromArgb(250, 250, 250);
            treeListView.FocusedNodeStyle.BorderColor = Color.FromArgb(250, 250, 250);
            treeListView.FocusedNodeStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.BackwardVertical;
            
            // フォーカス時のバックグラウンド
            treeListView.FocusedNodeStyle.BackColor = Color.PaleTurquoise;
            treeListView.FocusedNodeStyle.BorderColor = Color.Black;
            treeListView.FocusedNodeStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.BackwardVertical;

            // 選択時のバックグラウンド
            treeListView.SelectedNodeStyle.BackColor = Color.PaleTurquoise;
            treeListView.SelectedNodeStyle.BorderColor = Color.Black;
            treeListView.SelectedNodeStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.BackwardVertical;
        }

        //===============================================================
        // 手配依頼品目用
        //===============================================================
        #region
        public void AddNodes(List<OdThiShousaiItem> odThiShousaiItems, ref Dictionary<TreeListViewNode, OdThiShousaiItem> odThiShousaiItemNodes)
        {
            treeListView.SuspendUpdate();

            int no = 0;
            foreach (OdThiShousaiItem shousaiItem in odThiShousaiItems)
            {
                if (shousaiItem.CancelFlag == 1)
                {
                    continue;
                }
                no++;
                TreeListViewNode subNode = MakeNode(no, shousaiItem);
                treeListView.Nodes.Add(subNode);

                odThiShousaiItemNodes.Add(subNode, shousaiItem);

                //開く m.yoshihara
                //treeListView.EnsureVisible(subNode);
            }

            //// 全部、開く 使えなくなった模様 m.yoshihara
            treeListView.ExpandAll();
            treeListView.ResumeUpdate();
        }

        public void AddNode(OdThiShousaiItem shousaiItem, ref Dictionary<TreeListViewNode, OdThiShousaiItem> odThiShousaiItemNodes)
        {
            int no = -1;
            TreeListViewNode subNode = MakeNode(no, shousaiItem);
            treeListView.Nodes.Add(subNode);

            odThiShousaiItemNodes.Add(subNode, shousaiItem);

            treeListView.ExpandAll();
            treeListView.ResumeUpdate();
        }

        private TreeListViewNode MakeNode(int no, OdThiShousaiItem shousaiItem)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 詳細品目名
            string shousaiItemName = "";
            if (shousaiItem.OdThiShousaiItemID.Length > 0)
            {
                shousaiItemName = shousaiItem.ShousaiItemName;
            }
            else
            {
                shousaiItemName = DefaultShousaiName;
            }
            AddSubItemAsShousai(node, 0, shousaiItemName, true);

            //if (ThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品))
            //{
            //    // 種別
            //    string strVal = "追加";
            //    if (shousaiItem.SpecificFlag == 1)
            //    {
            //        strVal = "特定";
            //    }
            //    else if (shousaiItem.MsVesselItemID != null && shousaiItem.MsVesselItemID != string.Empty)
            //    {
            //        strVal = "ﾘｽﾄ";
            //    }
            //    else if (shousaiItemName == DefaultShousaiName)
            //    {
            //        strVal = "";
            //    }
            //    AddSubItem(node, strVal, true);
            //}

            // 単位
            AddSubItem(node, shousaiItem.MsTaniName, true);
            // 在庫数
            string zaiko = "";
            if (shousaiItem.ZaikoCount > -1)
            {
                zaiko = shousaiItem.ZaikoCount.ToString();
            }
            AddSubItem(node, zaiko, true);
            // 依頼数
            string iraisu = "";
            if (shousaiItem.Count > -1)
            {
                iraisu = shousaiItem.Count.ToString();
            }
            AddSubItem(node, iraisu, true);
            // 査定数
            string sateisu = "";
            if (shousaiItem.Sateisu > -1)
            {
                sateisu = shousaiItem.Sateisu.ToString();
            }
            AddSubItem(node, sateisu, true);

            // 添付
            if (shousaiItem.OdAttachFileID != null && shousaiItem.OdAttachFileID.Length > 0)
            {
                AddSubItem(node, "〇", true);
            }
            else
            {
                AddSubItem(node, "", true);
            }
            // 備考
            AddSubItem(node, shousaiItem.Bikou, true);

            return node;
        }
        #endregion

        //===============================================================
        // 見積回答品目用
        //===============================================================
        #region
        public void AddNodes(List<OdMkShousaiItem> OdMkShousaiItems, ref Dictionary<TreeListViewNode, OdMkShousaiItem> OdMkShousaiItemNodes)
        {
            treeListView.SuspendUpdate();

            int no = 0;
            foreach (OdMkShousaiItem shousaiItem in OdMkShousaiItems)
            {
                if (shousaiItem.CancelFlag == 1)
                {
                    continue;
                }
                no++;
                TreeListViewNode subNode = MakeNode(no, shousaiItem);
                treeListView.Nodes.Add(subNode);

                OdMkShousaiItemNodes.Add(subNode, shousaiItem);

                //開く m.yoshihara
                //treeListView.EnsureVisible(subNode);
            }

            // 全部、開く 使えなくなった模様 m.yoshihara
            treeListView.ExpandAll();
            
            treeListView.ResumeUpdate();
        }

        public void AddNode(OdMkShousaiItem shousaiItem, ref Dictionary<TreeListViewNode, OdMkShousaiItem> OdMkShousaiItemNodes)
        {
            int no = -1;
            TreeListViewNode subNode = MakeNode(no, shousaiItem);
            treeListView.Nodes.Add(subNode);

            OdMkShousaiItemNodes.Add(subNode, shousaiItem);

            treeListView.ExpandAll();
            treeListView.ResumeUpdate();
        }

        private TreeListViewNode MakeNode(int no, OdMkShousaiItem shousaiItem)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 詳細品目名
            string shousaiItemName = "";
            if (shousaiItem.OdMkShousaiItemID.Length > 0)
            {
                shousaiItemName = shousaiItem.ShousaiItemName;
            }
            else
            {
                shousaiItemName = DefaultShousaiName;
            }
            AddSubItemAsShousai(node, 0, shousaiItemName, true);
            // 単位
            AddSubItem(node, shousaiItem.MsTaniName, true);
            // 依頼数
            if (shousaiItem.OdMmShousaiItemID != "新規発注")
            {
                string mmCount = "";
                if (shousaiItem.OdMmShousaiItemCount > -1)
                {
                    mmCount = shousaiItem.OdMmShousaiItemCount.ToString();
                }
                AddSubItem(node, mmCount.ToString(), true);
            }
            // 回答数
            decimal count = -1;
            string countStr = "";
            if (shousaiItem.Count > -1)
            {
                count = shousaiItem.Count;
                countStr = count.ToString();
            }
            AddSubItem(node, countStr, true);
            // 単価
            decimal tanka = -1;
            string tankaStr = "";
            if (shousaiItem.Tanka > -1)
            {
                tanka = shousaiItem.Tanka;
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
            AddSubItem(node, shousaiItem.Bikou, true);

            return node;
        }
        #endregion

        //===============================================================
        // 受領品目用
        //===============================================================
        #region
        public void AddNodes(List<OdJryShousaiItem> OdJryShousaiItems, ref Dictionary<TreeListViewNode, OdJryShousaiItem> OdJryShousaiItemNodes)
        {
            treeListView.SuspendUpdate();

            int no = 0;
            foreach (OdJryShousaiItem shousaiItem in OdJryShousaiItems)
            {
                if (shousaiItem.CancelFlag == 1)
                {
                    continue;
                }
                no++;
                TreeListViewNode subNode = MakeNode(no, shousaiItem);
                treeListView.Nodes.Add(subNode);

                OdJryShousaiItemNodes.Add(subNode, shousaiItem);

                //開く m.yoshihara
                //treeListView.EnsureVisible(subNode);
            }

            // 全部、開く 使えなくなった模様 m.yoshihara
            treeListView.ExpandAll();
            treeListView.ResumeUpdate();
        }

        public void AddNode(OdJryShousaiItem shousaiItem, ref Dictionary<TreeListViewNode, OdJryShousaiItem> OdJryShousaiItemNodes)
        {
            int no = -1;
            TreeListViewNode subNode = MakeNode(no, shousaiItem);
            treeListView.Nodes.Add(subNode);

            OdJryShousaiItemNodes.Add(subNode, shousaiItem);

            treeListView.ExpandAll();
            treeListView.ResumeUpdate();
        }

        private TreeListViewNode MakeNode(int no, OdJryShousaiItem shousaiItem)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 詳細品目名
            string shousaiItemName = "";
            if (shousaiItem.OdJryShousaiItemID.Length > 0)
            {
                shousaiItemName = shousaiItem.ShousaiItemName;
            }
            else
            {
                shousaiItemName = DefaultShousaiName;
            }
            AddSubItemAsShousai(node, 0, shousaiItemName, true);
            // 単位
            AddSubItem(node, shousaiItem.MsTaniName, true);
            // 発注数
            string countStr = "";
            if (shousaiItem.Count > -1)
            {
                countStr = shousaiItem.Count.ToString();
            }
            AddSubItem(node, countStr, true);
            // 受領数
            decimal jryCount = -1;
            string jryCountStr = "";
            if (shousaiItem.JryCount > -1)
            {
                jryCount = shousaiItem.JryCount;
                jryCountStr = jryCount.ToString();
            }
            AddSubItem(node, jryCountStr, true);
            // 単価
            decimal tanka = -1;
            string tankaStr = "";
            if (shousaiItem.Tanka > -1)
            {
                tanka = shousaiItem.Tanka;
                tankaStr = NBaseCommon.Common.金額出力(tanka);
            }
            AddSubItem(node, tankaStr, true);
            // 金額
            decimal kingaku = -1;
            string kingakuStr = "";
            if (jryCount > -1 && tanka > -1)
            {
                kingaku = jryCount * tanka;
                kingakuStr = NBaseCommon.Common.金額出力(kingaku);
            }
            AddSubItem(node, kingakuStr, true);
            // 備考
            AddSubItem(node, shousaiItem.Bikou, true);

            return node;
        }
        #endregion

        //===============================================================
        // 支払品目用
        //===============================================================
        #region
        public void AddNodes(List<OdShrShousaiItem> OdShrShousaiItems, ref Dictionary<TreeListViewNode, OdShrShousaiItem> OdShrShousaiItemNodes)
        {
            treeListView.SuspendUpdate();

            int no = 0;
            foreach (OdShrShousaiItem shousaiItem in OdShrShousaiItems)
            {
                if (shousaiItem.CancelFlag == 1)
                {
                    continue;
                }
                no++;
                TreeListViewNode subNode = MakeNode(no, shousaiItem);
                treeListView.Nodes.Add(subNode);

                OdShrShousaiItemNodes.Add(subNode, shousaiItem);

                //開く m.yoshihara
                //treeListView.EnsureVisible(subNode);
            }

            // 全部、開く 使えなくなった模様 m.yoshihara
            treeListView.ExpandAll();
            treeListView.ResumeUpdate();
        }

        public void AddNode(OdShrShousaiItem shousaiItem, ref Dictionary<TreeListViewNode, OdShrShousaiItem> OdShrShousaiItemNodes)
        {
            int no = -1;
            TreeListViewNode subNode = MakeNode(no, shousaiItem);
            treeListView.Nodes.Add(subNode);

            OdShrShousaiItemNodes.Add(subNode, shousaiItem);

            treeListView.ExpandAll();
            treeListView.ResumeUpdate();
        }

        private TreeListViewNode MakeNode(int no, OdShrShousaiItem shousaiItem)
        {
            TreeListViewNode node = new TreeListViewNode();

            // 詳細品目名
            string shousaiItemName = "";
            if (shousaiItem.OdShrShousaiItemID.Length > 0)
            {
                shousaiItemName = shousaiItem.ShousaiItemName;
            }
            else
            {
                shousaiItemName = DefaultShousaiName;
            }
            AddSubItemAsShousai(node, 0, shousaiItemName, true);
            // 単位
            AddSubItem(node, shousaiItem.MsTaniName, true);
            // 数量
            int count = -1;
            string countStr = "";
            if (shousaiItem.Count > -1)
            {
                count = shousaiItem.Count;
                countStr = shousaiItem.Count.ToString();
            }
            AddSubItem(node, countStr, true);
            // 単価
            decimal tanka = -1;
            string tankaStr = "";
            if (shousaiItem.Tanka > -1)
            {
                tanka = shousaiItem.Tanka;
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
            AddSubItem(node, shousaiItem.Bikou, true);

            return node;
        }
        #endregion
    }
}
