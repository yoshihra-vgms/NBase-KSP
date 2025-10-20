using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DAC;
using Hachu.Reports;

namespace Hachu.HachuManage
{
    public partial class 合見積比較Form : BaseForm
    {
        private string OdMmID;

        public 合見積比較Form(string OdMmID)
        {
            this.OdMmID = OdMmID;
            InitializeComponent();
        }

        //======================================================================================
        //
        // コールバック
        //
        //======================================================================================

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 合見積比較Form_Load(object sender, EventArgs e)
        private void 合見積比較Form_Load(object sender, EventArgs e)
        {
            Formに情報をセットする();
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            Formを閉じる();
        }
        #endregion


        /// <summary>
        /// 「ファイル出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonファイル出力_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            合見積比較表 比較書 = new 合見積比較表();
            比較書.Output(OdMmID);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        //======================================================================================
        //
        // 処理メソッド
        //
        //======================================================================================

        /// <summary>
        /// 
        /// </summary>
        #region private void Formに情報をセットする()
        private void Formに情報をセットする()
        {
            this.Text = NBaseCommon.Common.WindowTitle("JM040402", "合見積比較", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            
            Size s = new Size();
            s.Height = Hachu.Common.CommonDefine.ExpandBoxSize;
            s.Width = Hachu.Common.CommonDefine.ExpandBoxSize;
            treeListView.ExpandBoxStyle.ImageSize = s;

            //=========================================
            // ＤＢから必要な情報を取得する
            //=========================================
            List<OdMmItem> OdMmItems = null;
            List<OdMmShousaiItem> OdMmShousaiItems = null;
            List<OdMk> OdMks = null;
            List<List<OdMkItem>> OdMkItemsList = new List<List<OdMkItem>>();
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                OdMmItems = serviceClient.OdMmItem_GetRecordsByOdMmID(NBaseCommon.Common.LoginUser, OdMmID);
                OdMmShousaiItems = serviceClient.OdMmShousaiItem_GetRecordByOdMmID(NBaseCommon.Common.LoginUser, OdMmID);
                OdMks = serviceClient.OdMk_GetRecordsByOdMmID(NBaseCommon.Common.LoginUser, OdMmID);
                foreach (OdMk odMk in OdMks)
                {
                    List<OdMkItem> OdMkItems = serviceClient.OdMkItem_GetRecordsByOdMkID(NBaseCommon.Common.LoginUser, odMk.OdMkID);
                    List<OdMkShousaiItem> OdMkShousaiItems = serviceClient.OdMkShousaiItem_GetRecordByOdMkID(NBaseCommon.Common.LoginUser, odMk.OdMkID);
                    foreach (OdMkItem mkItem in OdMkItems)
                    {
                        foreach (OdMkShousaiItem shousaiItem in OdMkShousaiItems)
                        {
                            if (shousaiItem.OdMkItemID == mkItem.OdMkItemID)
                            {
                                mkItem.OdMkShousaiItems.Add(shousaiItem);
                            }
                        }
                    }
                    OdMkItemsList.Add(OdMkItems);
                }
            }
            foreach (OdMmItem mmItem in OdMmItems)
            {
                foreach (OdMmShousaiItem shousaiItem in OdMmShousaiItems)
                {
                    if (shousaiItem.OdMmItemID == mmItem.OdMmItemID)
                    {
                        mmItem.OdMmShousaiItems.Add(shousaiItem);
                    }
                }
            }
            List<TreeNode_Header> treeNodeHeaders = MakeTreeNodeHeaders(OdMmItems, OdMkItemsList);

            textBox船.Text = OdMks[0].MsVesselName;
            textBox手配内容.Text = OdMks[0].OdThiNaiyou;

            treeListView.SuspendUpdate();

            // ヘッダ行
            InitializeColumn(OdMks);

            // データ部
            foreach (TreeNode_Header header in treeNodeHeaders)
            {
                // ヘッダ
                TreeListViewNode headerNode = MakeNode();
                treeListView.Nodes.Add(headerNode);
                headerNode.SubItems.Add(MakeMmSubItemHeader(header.Header));
                headerNode.SubItems.Add(MakeMmSubItemHeader("")); // 単位列
                // 横方向に
                for (int i = 0; i < OdMkItemsList.Count; i++)
                {
                    // (―)ヘッダ（見積回答列）
                    headerNode.SubItems.Add(MakeMkSubItem(-1, -1));
                }
                treeListView.EnsureVisible(headerNode);

                foreach (TreeNode_Item item in header.Items)
                {
                    // 品目名
                    TreeListViewNode itemNode = MakeNode();
                    headerNode.Nodes.Add(itemNode);
                    itemNode.SubItems.Add(MakeMmSubItemItem(item.ItemName));
                    itemNode.SubItems.Add(MakeMmSubItemItem("")); // 単位列
                    // 横方向に
                    for (int i = 0; i < OdMkItemsList.Count; i++)
                    {
                        // (―)品目名（見積回答列）
                        itemNode.SubItems.Add(MakeMkSubItem(-1, -1));
                    }
                    treeListView.EnsureVisible(itemNode);

                    foreach (TreeNode_ShousaiItem shousaiItem in item.ShousaiItems)
                    {
                        // 詳細品目名
                        TreeListViewNode shousaiNode = MakeNode();
                        itemNode.Nodes.Add(shousaiNode);
                        shousaiNode.SubItems.Add(MakeMmSubItemShousai(shousaiItem.ShousaiItemName));
                        shousaiNode.SubItems.Add(MakeMmSubItemShousai(shousaiItem.Count, shousaiItem.TaniName));

                        // 横方向に
                        for (int i = 0; i < OdMkItemsList.Count; i++)
                        {
                            // （数量、金額）詳細品目名（見積回答列）
                            OdMkShousaiItem mkShousaiItem = GetOdMkShousaiItem(OdMkItemsList[i], item, shousaiItem);
                            TreeListViewSubItem subItem = null;
                            if (mkShousaiItem != null)
                            {
                                subItem = MakeMkSubItem(mkShousaiItem.Count, (mkShousaiItem.Count * mkShousaiItem.Tanka));
                            }
                            else if (shousaiItem.OdMmShousaiItemID != null)
                            {
                                subItem = MakeMkSubItem(0, 0);
                            }
                            else
                            {
                                subItem = MakeMkSubItem(0, -1);
                            }
                            shousaiNode.SubItems.Add(subItem);
                        }
                        treeListView.EnsureVisible(shousaiNode);
                    }
                }
            }
            
            treeListView.ExpandAll();

            treeListView.ResumeUpdate();
        }
        #endregion

        /// <summary>
        /// Formを閉じる
        /// </summary>
        #region private void Formを閉じる(
        private void Formを閉じる()
        {
            BaseFormClosing();
            Close();
        }
        #endregion

        private OdMkShousaiItem GetOdMkShousaiItem(List<OdMkItem> OdMkItems, TreeNode_Item TnItem, TreeNode_ShousaiItem TnShousaiItem)
        {
            OdMkShousaiItem ret = null;
            foreach (OdMkItem mkItem in OdMkItems)
            {
                string itemName = "";
                //if (mkItem.MsItemSbtID != null && mkItem.MsItemSbtID.Length > 0)
                //{
                //    itemName += mkItem.MsItemSbtName + "：";
                //}
                itemName += mkItem.ItemName;
                if (TnItem.ItemName == itemName && (TnItem.OdMmItemID == mkItem.OdMmItemID || TnItem.OdMkItemID == mkItem.OdMkItemID) )
                {
                    foreach (OdMkShousaiItem mkShousaiItem in mkItem.OdMkShousaiItems)
                    {
                        if (TnShousaiItem.OdMmShousaiItemID != null)
                        {
                            if (mkShousaiItem.OdMmShousaiItemID == TnShousaiItem.OdMmShousaiItemID)
                            {
                                ret = mkShousaiItem;
                                break;
                            }
                        }
                        else
                        {
                            if (mkShousaiItem.OdMkShousaiItemID == TnShousaiItem.OdMkShousaiItemID)
                            {
                                ret = mkShousaiItem;
                                break;
                            }
                        }
                    }
                }
            }
            return ret;
        }
        private TreeListViewNode MakeNode()
        {
            TreeListViewNode node = new TreeListViewNode();
            node.StyleFromParent = false;
            node.NormalStyle.BorderColor = Color.Gainsboro;
            node.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            return node;
        }
        private TreeListViewSubItem MakeMmSubItemHeader(string itemName)
        {
            return MakeMmSubItem(1, itemName, null, null);
        }
        private TreeListViewSubItem MakeMmSubItemItem(string itemName)
        {
            return MakeMmSubItem(2, itemName, null, null);
        }
        private TreeListViewSubItem MakeMmSubItemShousai(string itemName)
        {
            return MakeMmSubItem(3, itemName, null, null);
        }
        private TreeListViewSubItem MakeMmSubItemShousai(string count, string tani)
        {
            return MakeMmSubItem(3, null, count, tani);
        }
        private TreeListViewSubItem MakeMmSubItem(int category, string itemName, string count, string tani)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = false;
            subItem.NormalStyle.BackColor = Color.LightSteelBlue;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            subItem.SelectedStyle = subItem.NormalStyle;

            StringBuilder subItemStr = new StringBuilder();
            subItemStr.Append("<div>");
            subItemStr.Append("<table><tr>");
            if (category == 1)
            {
                subItemStr.Append("<td><font size=\"11\"><b>" + itemName + "</b></font></td>");
            }
            else if (category == 2)
            {
                subItemStr.Append("<td>" + itemName + "</td>");
            }
            else if (category == 3)
            {
                if (itemName != null)
                {
                    subItemStr.Append("<td>" + itemName + "</td>");
                    subItem.Text = itemName;
                }
                else
                {
                    try
                    {
                        int c = int.Parse(count);
                        subItemStr.Append("<td width=\"60\" align=\"Right\"><font face=\"ＭＳ Ｐゴシック\" size=\"10\">" + count + " " + tani + "</font></td>");
                    }
                    catch
                    {
                        subItemStr.Append("<td width=\"60\" align=\"Right\"><font face=\"ＭＳ Ｐゴシック\" size=\"10\">" + tani + "</font></td>");
                    }
                }
            }
            else
            {
                subItemStr.Append("<td></td>");
            }
            subItemStr.Append("</tr></table>");
            subItemStr.Append("</div>");
            subItem.Content = subItemStr.ToString();
            return subItem;
        }
        private TreeListViewSubItem MakeMkSubItem(int count, decimal kingaku)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = false;
            subItem.NormalStyle.BackColor = Color.FromArgb(240, 240, 240); //Color.LightSteelBlue;
            subItem.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;
            subItem.HoverStyle = subItem.NormalStyle;
            subItem.SelectedStyle = subItem.NormalStyle;

            StringBuilder subItemStr = new StringBuilder();
            subItemStr.Append("<div>");
            subItemStr.Append("<table width=\"100%\"><tr>");
            if (count > 0)
            {
                subItemStr.Append("<td width=\"45\" align=\"Right\"><font face=\"ＭＳ Ｐゴシック\" size=\"10\">" + count.ToString() + "</font></td>");
                subItemStr.Append("<td width=\"125\" align=\"Right\"><font face=\"ＭＳ Ｐゴシック\" size=\"10\">" + NBaseCommon.Common.金額出力(kingaku) + "　</font></td>");
            }
            else if (count == -1 && kingaku == -1)
            {
                // ヘッダ行 および 品目行
                subItemStr.Append("<td>  </td>");
            }
            else if (kingaku == -1)
            {
                // 他の業者が追加した詳細品目の場合
                subItemStr.Append("<td width=\"45\" align=\"Right\" style=\"textcolor:red;\">-----  </td>");
            }
            else
            {
                // 見積依頼されていない詳細品目の場合
                subItemStr.Append("<td width=\"45\" align=\"Right\">-----  </td>");
            }
            subItemStr.Append("</tr></table>");
            subItemStr.Append("</div>");
            subItem.Content = subItemStr.ToString();
            return subItem;
        }
        private void InitializeColumn(List<OdMk> OdMks)
        {
            TreeListViewColumn column1 = new TreeListViewColumn();
            column1.StyleFromParent = false;
            column1.HoverStyle = column1.NormalStyle;
            column1.SelectedStyle = column1.NormalStyle;
            column1.ContentType = ColumnContentType.Custom;
            column1.FormatStyle.HeaderBorderCornerShape.SetCornerShape(LidorSystems.IntegralUI.Style.CornerShape.Squared);
            column1.Width = 225;
            column1.HeaderContent = "<div><table><tr><td width=\"200\"><font size=\"10\">区分 /　仕様・型式 /　詳細品目</font></td></tr></table></div>";
            treeListView.Columns.Add(column1);

            TreeListViewColumn column2 = new TreeListViewColumn();
            column2.StyleFromParent = false;
            column2.HoverStyle = column2.NormalStyle;
            column2.SelectedStyle = column2.NormalStyle;
            column2.ContentType = ColumnContentType.Custom;
            column2.FormatStyle.HeaderBorderCornerShape.SetCornerShape(LidorSystems.IntegralUI.Style.CornerShape.Squared);
            column2.Width = 70;
            column2.HeaderContent = "<div><table><tr><td width=\"70\" height=\"140\" valign=\"bottom\">見積依頼数</td></tr></table></div>";
            treeListView.Columns.Add(column2);

            foreach (OdMk odMk in OdMks)
            {
                treeListView.Columns.Add(MakeMkColumn(odMk));
            }
        }
        private TreeListViewColumn MakeMkColumn(OdMk odMk)
        {
            TreeListViewColumn column = new TreeListViewColumn();
            column.StyleFromParent = false;
            column.HoverStyle = column.NormalStyle;
            column.SelectedStyle = column.NormalStyle;
            column.ContentType = ColumnContentType.Custom;
            column.FormatStyle.HeaderBorderCornerShape.SetCornerShape(LidorSystems.IntegralUI.Style.CornerShape.Squared);
            column.Width = 185;
            StringBuilder columnStr = new StringBuilder();
            columnStr.Append("<div>");
            columnStr.Append("<font size=\"11\">" + odMk.MsCustomerName + "</font>");
            columnStr.Append("<table>");
            columnStr.Append("<tr><td width=\"65\" height=\"25\" valign=\"bottom\">見積合計</td><td width=\"95\" height=\"25\" valign=\"bottom\" align=\"Right\"><font face=\"ＭＳ Ｐゴシック\" size=\"10\">" + NBaseCommon.Common.金額出力(odMk.Amount) + "　</font></td></tr>");
            columnStr.Append("<tr><td>消費税</td><td align=\"Right\"><font face=\"ＭＳ Ｐゴシック\" size=\"10\">" + NBaseCommon.Common.金額出力(odMk.Tax) + "　</font></td></tr>");
            columnStr.Append("<tr><td>送料・運搬料</td><td align=\"Right\"><font face=\"ＭＳ Ｐゴシック\" size=\"10\">" + NBaseCommon.Common.金額出力(odMk.Carriage) + "　</font></td></tr>");
            columnStr.Append("<tr><td>値引額</td><td align=\"Right\"><font face=\"ＭＳ Ｐゴシック\" size=\"10\">" + NBaseCommon.Common.金額出力(odMk.MkAmount) + "　</font></td></tr>");
            columnStr.Append("<tr><td>合計金額(税込)</td><td height=\"25\" valign=\"bottom\" align=\"Right\"><font face=\"ＭＳ Ｐゴシック\" size=\"10\">" + NBaseCommon.Common.金額出力(odMk.Amount + odMk.Tax + odMk.Carriage - odMk.MkAmount) + "　</font></td></tr>");
            columnStr.Append("<tr><td height=\"45\" valign=\"bottom\">見積回答数</td><td  height=\"45\" valign=\"bottom\" align=\"Right\">金額　</td></tr>");
            columnStr.Append("</table>");
            columnStr.Append("</div>");
            column.HeaderContent = columnStr.ToString();
            return column;
        }

        private List<TreeNode_Header> MakeTreeNodeHeaders(List<OdMmItem> odMmItems, List<List<OdMkItem>> odMkItemsList)
        {
            List<TreeNode_Header> ret = new List<TreeNode_Header>();
            if (odMmItems == null || odMmItems.Count == 0)
                return ret; 

            TreeNode_Header th = null;
            TreeNode_Item ti = null;
            Dictionary<string, TreeNode_Header> headerDic = new Dictionary<string, TreeNode_Header>();
            Dictionary<string, OdMmItem> itemIDDic = new Dictionary<string, OdMmItem>();
            Dictionary<string, OdMmShousaiItem> shousaiIDDic = new Dictionary<string, OdMmShousaiItem>();

            Dictionary<string, OdMkItem> checkedItemIDDic = new Dictionary<string, OdMkItem>();

            // 見積依頼時の品目を表示対象リストに追加する
            foreach (OdMmItem odMmItem in odMmItems)
            {
                if (headerDic.ContainsKey(odMmItem.Header))
                {
                    th = headerDic[odMmItem.Header];
                }
                else
                {
                    th = new TreeNode_Header();
                    th.Header = odMmItem.Header;
                    th.Items = new List<TreeNode_Item>();
                    ret.Add(th);

                    headerDic.Add(th.Header, th);
                }
                ti = new TreeNode_Item();
                string itemName = "";
                //if (odMmItem.MsItemSbtID != null && odMmItem.MsItemSbtID.Length > 0)
                //{
                //    itemName += odMmItem.MsItemSbtName + "：";
                //}
                itemName += odMmItem.ItemName;
                ti.ItemName = itemName;
                ti.OdMmItemID = odMmItem.OdMmItemID;
                ti.ShousaiItems = new List<TreeNode_ShousaiItem>();
                th.Items.Add(ti);

                itemIDDic.Add(odMmItem.OdMmItemID, odMmItem);

                // 見積依頼時の詳細品目を表示対象リストに追加する
                foreach (OdMmShousaiItem shousaiItem in odMmItem.OdMmShousaiItems)
                {
                    TreeNode_ShousaiItem ts = new TreeNode_ShousaiItem();
                    ts.OdMmShousaiItemID = shousaiItem.OdMmShousaiItemID;
                    ts.ShousaiItemName = shousaiItem.ShousaiItemName;
                    ts.Count = shousaiItem.Count.ToString();
                    ts.TaniName = shousaiItem.MsTaniName;

                    ti.ShousaiItems.Add(ts);
                    shousaiIDDic.Add(shousaiItem.OdMmShousaiItemID, shousaiItem);
                }
            }

            // 見積依頼時になかった詳細品目を表示対象リストに追加する
            // （見積回答で追加された詳細品目）
            foreach (List<OdMkItem> odMkItems in odMkItemsList)
            {
                foreach (OdMkItem odMkItem in odMkItems)
                {
                    //if (checkedItemIDDic.ContainsKey(odMkItem.OdMkItemID))
                    //{
                    //    continue;
                    //}

                    if (headerDic.ContainsKey(odMkItem.Header))
                    {
                        th = headerDic[odMkItem.Header];
                    }
                    else
                    {
                        th = new TreeNode_Header();
                        th.Header = odMkItem.Header;
                        th.Items = new List<TreeNode_Item>();
                        ret.Add(th);

                        headerDic.Add(th.Header, th);
                    }

                    bool hitItem = false;
                    if (itemIDDic.ContainsKey(odMkItem.OdMmItemID))
                    {
                        hitItem = false;
                        OdMmItem hitMmItem = itemIDDic[odMkItem.OdMmItemID];
                        if (hitMmItem.MsItemSbtID == odMkItem.MsItemSbtID && hitMmItem.ItemName == odMkItem.ItemName)
                        {
                            // 同じ、OD_MM_ITEM_ID でも品目名が違う場合、（事務所の見積回答画面で編集が可能）
                            // 合見積画面で、同じ品目としては、扱わない
                            hitItem = true;

                            string itemName = "";
                            //if (odMkItem.MsItemSbtID != null && odMkItem.MsItemSbtID.Length > 0)
                            //{
                            //    itemName += odMkItem.MsItemSbtName + "：";
                            //}
                            itemName += odMkItem.ItemName;

                            foreach (TreeNode_Item c_ti in th.Items)
                            {
                                if (c_ti.ItemName == itemName && c_ti.OdMmItemID == hitMmItem.OdMmItemID)
                                {
                                    ti = c_ti;
                                    break;
                                }
                            }

                            bool hitShousaiItem = false;
                            foreach (OdMkShousaiItem mkShousaiItem in odMkItem.OdMkShousaiItems)
                            {
                                hitShousaiItem = false;
                                if (shousaiIDDic.ContainsKey(mkShousaiItem.OdMmShousaiItemID))
                                {
                                    // 同じ、OD_MM_SHOUSAI_ITEM_ID でも品目名が違う場合、（事務所の見積回答画面で編集が可能）
                                    // 合見積画面で、同じ品目としては、扱わない
                                    OdMmShousaiItem hitMmShousaiItem = shousaiIDDic[mkShousaiItem.OdMmShousaiItemID];
                                    if (hitMmShousaiItem.ShousaiItemName == mkShousaiItem.ShousaiItemName)
                                    {
                                        hitShousaiItem = true;
                                    }
                                }
                                if (hitShousaiItem == false)
                                {
                                    TreeNode_ShousaiItem ts = new TreeNode_ShousaiItem();
                                    ts.OdMkShousaiItemID = mkShousaiItem.OdMkShousaiItemID;
                                    ts.ShousaiItemName = mkShousaiItem.ShousaiItemName;
                                    ts.Count = "--";
                                    ts.TaniName = mkShousaiItem.MsTaniName;

                                    ti.ShousaiItems.Add(ts);
                                }
                            }

                            checkedItemIDDic.Add(odMkItem.OdMkItemID, odMkItem);
                        }
                    }
                    if (hitItem == false)
                    {
                        // 見積回答時に追加されている品目／詳細品目を表示対象リストに追加する
                        TreeNode_Item c_ti = new TreeNode_Item();
                        string itemName = "";
                        //if (odMkItem.MsItemSbtID != null && odMkItem.MsItemSbtID.Length > 0)
                        //{
                        //    itemName += odMkItem.MsItemSbtName + "：";
                        //}
                        itemName += odMkItem.ItemName;
                        c_ti.ItemName = itemName;
                        c_ti.OdMkItemID = odMkItem.OdMkItemID;
                        c_ti.ShousaiItems = new List<TreeNode_ShousaiItem>();
                        th.Items.Add(c_ti);

                        foreach (OdMkShousaiItem mkShousaiItem in odMkItem.OdMkShousaiItems)
                        {
                            TreeNode_ShousaiItem c_ts = new TreeNode_ShousaiItem();
                            c_ts.OdMkShousaiItemID = mkShousaiItem.OdMkShousaiItemID;
                            c_ts.ShousaiItemName = mkShousaiItem.ShousaiItemName;
                            c_ts.Count = "--";
                            c_ts.TaniName = mkShousaiItem.MsTaniName;

                            c_ti.ShousaiItems.Add(c_ts);
                        }

                        checkedItemIDDic.Add(odMkItem.OdMkItemID, odMkItem);
                    }
                }
            }

            // 見積依頼時にないヘッダを見積回答時に追加している場合、こちら。
            foreach (List<OdMkItem> odMkItems in odMkItemsList)
            {
                th = null;
                foreach (OdMkItem odMkItem in odMkItems)
                {
                    if (checkedItemIDDic.ContainsKey(odMkItem.OdMkItemID))
                    {
                        continue;
                    }
                    if (th == null || th.Header != odMkItem.Header)
                    {
                        th = new TreeNode_Header();
                        th.Header = odMkItem.Header;
                        th.Items = new List<TreeNode_Item>();
                        ret.Add(th);
                    }
                    ti = new TreeNode_Item();
                    string itemName = "";
                    //if (odMkItem.MsItemSbtID != null && odMkItem.MsItemSbtID.Length > 0)
                    //{
                    //    itemName += odMkItem.MsItemSbtName + "：";
                    //}
                    itemName += odMkItem.ItemName;
                    ti.ItemName = itemName;
                    ti.OdMkItemID = odMkItem.OdMkItemID;
                    ti.ShousaiItems = new List<TreeNode_ShousaiItem>();
                    th.Items.Add(ti);

                    foreach (OdMkShousaiItem mkShousaiItem in odMkItem.OdMkShousaiItems)
                    {
                        TreeNode_ShousaiItem ts = new TreeNode_ShousaiItem();
                        ts.OdMkShousaiItemID = mkShousaiItem.OdMkShousaiItemID;
                        ts.ShousaiItemName = mkShousaiItem.ShousaiItemName;
                        ts.Count = "--";
                        ts.TaniName = mkShousaiItem.MsTaniName;

                        ti.ShousaiItems.Add(ts);
                    }
                }
            }
            return ret;
        }
        private class TreeNode_ShousaiItem
        {
            public string OdMmShousaiItemID;
            public string OdMkShousaiItemID;
            public string ShousaiItemName;
            public string Count;
            public string TaniName;
        }
        private class TreeNode_Item
        {
            public string OdMmItemID;
            public string OdMkItemID;
            public string ItemName;

            public List<TreeNode_ShousaiItem> ShousaiItems;
        }
        private class TreeNode_Header
        {
            public string Header;
            public  List<TreeNode_Item> Items;
        }
    }
}
