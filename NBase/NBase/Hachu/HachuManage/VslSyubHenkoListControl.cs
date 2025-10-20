using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LidorSystems.IntegralUI.Lists;
using NBaseData.DAC;
using Hachu.Models;


namespace Hachu.HachuManage
{
    public partial class VslSyubHenkoListControl : UserControl
    {
        public delegate void ClickEventHandler(ListInfo手配依頼 selectedNode);
        public event ClickEventHandler ClickEvent;


        public bool IsContains船受領 = false;
        List<ListInfo手配依頼> 手配List;

        public VslSyubHenkoListControl()
        {
            InitializeComponent();
        }

        public void Init(int width)
        {
            Size s = new Size();
            s.Height = Hachu.Common.CommonDefine.ExpandBoxSize;
            s.Width = Hachu.Common.CommonDefine.ExpandBoxSize;
            treeListView船種別変更一覧.ExpandBoxStyle.ImageSize = s;

            // ヘッダ、フッタは見せない
            treeListView船種別変更一覧.Header = false;
            treeListView船種別変更一覧.Footer = false;

            // カラム１（情報を表示）
            TreeListViewColumn column1 = new TreeListViewColumn();
            column1.HeaderText = "情報";
            column1.Width = width;
            treeListView船種別変更一覧.Columns.Add(column1);
            treeListView船種別変更一覧.Columns[0].ContentType = ColumnContentType.Custom;

            // 色がつくのは文字の開始位置から
            treeListView船種別変更一覧.ItemHighlightType = ListItemHighlightType.Partial;

            //発注一覧TreeListView.Nodes.Clear();

            StringBuilder sb = new StringBuilder();
            sb.Append("<div><table><tr><td width=\"100\"><font size=\"10\">あいうえお</font></td></tr></table></div>");
            sb.ToString();

            treeListView船種別変更一覧.SuspendUpdate();
            TreeListViewNode dmyNode1 = MakeTreeListViewNode("dmy", Color.Red, Color.Red, Color.Red, sb.ToString());
            treeListView船種別変更一覧.Nodes.Add(dmyNode1);
            TreeListViewNode dmyNode2 = MakeTreeListViewNode("dmy", Color.Blue, Color.Blue, Color.Blue, sb.ToString());
            treeListView船種別変更一覧.Nodes.Add(dmyNode2);
            treeListView船種別変更一覧.ResumeUpdate();

            treeListView船種別変更一覧.Nodes.Clear();
        }

        public int DrawList(
            //発注一覧検索条件 検索条件,
            List<OdThi> OdThis,
            List<OdMm> OdMms,
            List<OdMk> OdMks,
            List<OdJry> OdJrys,
            List<OdShr> OdShrs)
        {
            手配List = new List<ListInfo手配依頼>();
            
            treeListView船種別変更一覧.Nodes.Clear();
            treeListView船種別変更一覧.SuspendUpdate();

            #region
            foreach (OdThi thi in OdThis)
            {
                if (thi.Status == thi.OdStatusValue.Values[(int)OdThi.STATUS.船未手配].Value)
                    continue;

                var 船受領Jry = from obj in OdJrys
                             where obj.OdThiID == thi.OdThiID && obj.Status == (int)OdJry.STATUS.船受領
                             select obj;
                bool isExists船受領 = false;
                if (船受領Jry.Count<OdJry>() > 0)
                {
                    isExists船受領 = true;
                }
                if (isExists船受領 && (thi.MsThiIraiStatusID == Hachu.Common.CommonDefine.MsThiIraiStatus_見積中 || thi.MsThiIraiStatusID == Hachu.Common.CommonDefine.MsThiIraiStatus_発注済))
                {
                    var mks = OdMks.Where(obj => obj.OdThiID == thi.OdThiID);
                    int mkCount = mks.Count();

                    var jrys = OdJrys.Where(obj => obj.OdThiID == thi.OdThiID);
                    int jryCount = jrys.Count();

                    //if (検索条件.status船受領 == false && mkCount == 1 && jryCount == 1)
                    if (IsContains船受領 == false && mkCount == 1 && jryCount == 1)
                    {
                        // 条件に船受領のﾁｪｯｸがなく、船受領のﾃﾞｰﾀしかない場合、表示対象からはずす
                        continue;
                    }
                    if (mkCount > 1)
                    {
                        // 手配依頼のステータスが、見積中 or 発注済　で、船受領があるが、発注が複数ある場合、ステータスは船受領としない
                        isExists船受領 = false;
                    }
                }



                ListInfo手配依頼 thiInfo = new ListInfo手配依頼();
                thiInfo.info = thi;
                thiInfo.isExists船受領 = isExists船受領;
                手配List.Add(thiInfo);

                foreach (OdMm mm in OdMms)
                {
                    if (mm.OdThiID == thi.OdThiID)
                    {
                        ListInfo見積依頼 mmInfo = null;
                        TreeListViewNode mmNode = null;
                        if (mm.MmNo.Substring(0, 7) != "Enabled")
                        {
                            mmInfo = new ListInfo見積依頼();
                            mmInfo.parent = thi;
                            mmInfo.info = mm;
                            thiInfo.Children.Add(mmInfo);
                        }
                        foreach (OdMk mk in OdMks)
                        {
                            if (mk.OdMmID == mm.OdMmID)
                            {
                                ListInfo見積回答 mkInfo = null;
                                TreeListViewNode mkNode = null;
                                if (mk.MkNo.Substring(0, 7) != "Enabled")
                                {
                                    mkInfo = new ListInfo見積回答();
                                    mkInfo.parent = mm;
                                    mkInfo.info = mk;
                                    mmInfo.Children.Add(mkInfo);
                                }
                                foreach (OdJry jry in OdJrys)
                                {
                                    if (jry.OdMkID == mk.OdMkID)
                                    {
                                        ListInfo受領 jryInfo = new ListInfo受領();
                                        jryInfo.parent = mk;
                                        jryInfo.info = jry;
                                        if (mkInfo != null)
                                        {
                                            mkInfo.Children.Add(jryInfo);
                                        }
                                        else
                                        {
                                            thiInfo.Children.Add(jryInfo);
                                        }

                                        foreach (OdShr shr in OdShrs)
                                        {
                                            if (shr.OdJryID == jry.OdJryID)
                                            {
                                                ListInfo支払 shrInfo = new ListInfo支払();
                                                shrInfo.parent = jry;
                                                shrInfo.info = shr;
                                                jryInfo.Children.Add(shrInfo);
                                            }
                                            foreach (OdShrGassanItem gassanItem in shr.OdShrGassanItems)
                                            {
                                                if (gassanItem.OdJryID != shr.OdJryID && gassanItem.OdJryID == jry.OdJryID)
                                                {
                                                    ListInfo支払 shrInfo = new ListInfo支払();
                                                    shrInfo.parent = jry;
                                                    shrInfo.info = shr;
                                                    jryInfo.Children.Add(shrInfo);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            foreach (ListInfo手配依頼 thiInfo in 手配List)
            {
                int classNo = 1;
                string text = "手配依頼";
                Color NormalColor = thiInfo.NormalColor();
                Color SelectedColor = thiInfo.SelectedColor();
                Color BorderColor = thiInfo.BorderColor();

                if (thiInfo.Children != null)
                {
                    foreach (ListInfoBase info in thiInfo.Children)
                    {
                        if (info is ListInfo見積依頼)
                        {
                            if (classNo > 2)
                                break;

                            classNo = 2;
                            text = "見積依頼";
                            NormalColor = (info as ListInfo見積依頼).NormalColor();
                            SelectedColor = (info as ListInfo見積依頼).SelectedColor();
                            BorderColor = (info as ListInfo見積依頼).BorderColor();

                            foreach (ListInfoBase mkInfo in info.Children)
                            {
                                if (classNo > 3)
                                    break;

                                classNo = 3;
                                if ((mkInfo as ListInfo見積回答).info.Status == (mkInfo as ListInfo見積回答).info.OdStatusValue.MaxValue)
                                {
                                    text = "　発注　";
                                }
                                else
                                {
                                    text = "見積回答";
                                }
                                NormalColor = (mkInfo as ListInfo見積回答).NormalColor();
                                SelectedColor = (mkInfo as ListInfo見積回答).SelectedColor();
                                BorderColor = (mkInfo as ListInfo見積回答).BorderColor();

                                foreach (ListInfoBase jryInfo in mkInfo.Children)
                                {
                                    if (classNo > 4)
                                        break;

                                    classNo = 4;
                                    text = "受領";
                                    NormalColor = (jryInfo as ListInfo受領).NormalColor();
                                    SelectedColor = (jryInfo as ListInfo受領).SelectedColor();
                                    BorderColor = (jryInfo as ListInfo受領).BorderColor();

                                    foreach (ListInfoBase shrInfo in jryInfo.Children)
                                    {
                                        classNo = 5;
                                        text = "支払";
                                        NormalColor = (shrInfo as ListInfo支払).NormalColor();
                                        SelectedColor = (shrInfo as ListInfo支払).SelectedColor();
                                        BorderColor = (shrInfo as ListInfo支払).BorderColor();

                                        break;
                                    }
                                }
                            }
                        }
                        else if (info is ListInfo見積回答)
                        {
                            if (classNo > 3)
                                break;

                            classNo = 3;
                            if ((info as ListInfo見積回答).info.Status == (info as ListInfo見積回答).info.OdStatusValue.MaxValue)
                            {
                                text = "　発注　";
                            }
                            else
                            {
                                text = "見積回答";
                            }
                            NormalColor = (info as ListInfo見積回答).NormalColor();
                            SelectedColor = (info as ListInfo見積回答).SelectedColor();
                            BorderColor = (info as ListInfo見積回答).BorderColor();

                            foreach (ListInfoBase jryInfo in info.Children)
                            {
                                if (classNo > 4)
                                    break;

                                classNo = 4;
                                text = "受領";
                                NormalColor = (jryInfo as ListInfo受領).NormalColor();
                                SelectedColor = (jryInfo as ListInfo受領).SelectedColor();
                                BorderColor = (jryInfo as ListInfo受領).BorderColor();

                                foreach (ListInfoBase shrInfo in jryInfo.Children)
                                {
                                    classNo = 5;
                                    text = "支払";
                                    NormalColor = (shrInfo as ListInfo支払).NormalColor();
                                    SelectedColor = (shrInfo as ListInfo支払).SelectedColor();
                                    BorderColor = (shrInfo as ListInfo支払).BorderColor();

                                    break;
                                }
                            }
                        }
                        else if (info is ListInfo受領)
                        {
                            if (classNo > 4)
                                break;

                            classNo = 4;
                            text = "受領";
                            NormalColor = (info as ListInfo受領).NormalColor();
                            SelectedColor = (info as ListInfo受領).SelectedColor();
                            BorderColor = (info as ListInfo受領).BorderColor();

                            foreach (ListInfoBase shrInfo in info.Children)
                            {
                                classNo = 5;
                                text = "支払";
                                NormalColor = (shrInfo as ListInfo支払).NormalColor();
                                SelectedColor = (shrInfo as ListInfo支払).SelectedColor();
                                BorderColor = (shrInfo as ListInfo支払).BorderColor();

                                break;
                            }
                        }
                    }
                }


                TreeListViewNode thiNode = MakeTreeListViewNode(text, NormalColor, SelectedColor, BorderColor, thiInfo.発注状況一覧用文字列(text));
                thiNode.Tag = thiInfo;
                treeListView船種別変更一覧.Nodes.Add(thiNode);
            }



            treeListView船種別変更一覧.ResumeUpdate();

            return 手配List.Count();
        }

        /// <summary>
        /// 現在選択されているノードを削除する
        /// </summary>
        #region public void RemoveSelectedNode()
        public void RemoveSelectedNode()
        {
            try
            {
                // 一覧(TreeListView)から削除する
                TreeListViewNode node = treeListView船種別変更一覧.SelectedNode;
                treeListView船種別変更一覧.Nodes.Remove(node);
            }
            catch
            {
            }
        }
        #endregion


        /// <summary>
        /// ノードを作成する
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="bgColor"></param>
        /// <param name="sColor"></param>
        /// <param name="bColor"></param>
        /// <param name="content"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        #region private TreeListViewNode MakeTreeListViewNode(string nodeName, Color bgColor, Color sColor, Color bColor, string content)
        private TreeListViewNode MakeTreeListViewNode(string nodeName, Color bgColor, Color sColor, Color bColor, string content)
        {
            TreeListViewNode node = new TreeListViewNode(nodeName);
            node.StyleFromParent = false;

            // ノードの標準のバックグラウンド
            node.NormalStyle.BackColor = bgColor;
            node.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;

            // マウスオーバー時のバックグラウンド
            node.HoverStyle.BackColor = sColor;
            node.HoverStyle.BorderColor = bColor;
            node.HoverStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.BackwardVertical;

            // フォーカス時のバックグラウンド
            node.FocusedStyle.BackColor = sColor;
            node.FocusedStyle.BorderColor = Color.Black;
            node.FocusedStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.BackwardVertical;

            // 選択されている時のバックグラウンド
            node.SelectedStyle.BackColor = sColor;
            node.SelectedStyle.BorderColor = Color.Black;
            node.SelectedStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.BackwardVertical;

            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.Content = content;
            node.SubItems.Add(subItem);

            return node;
        }
        #endregion



        /// <summary>
        /// ノードクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeListView発注一覧_Click(object sender, EventArgs e)
        {
            TreeListViewNode selectedNode = treeListView船種別変更一覧.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }

            if (ClickEvent != null)
            {
                ClickEvent(selectedNode.Tag as ListInfo手配依頼);
            }
        }



        public void RedrawSelectedNode(ListInfo手配依頼 thiInfo, bool onlyRedraw = false)
        {
            System.Diagnostics.Debug.WriteLine("RedrawSelectedNode ==>");

            int classNo = 1;
            string text = "手配依頼";
            Color NormalColor = thiInfo.NormalColor();
            Color SelectedColor = thiInfo.SelectedColor();
            Color BorderColor = thiInfo.BorderColor();

            if (thiInfo.Children != null)
            {
                foreach (ListInfoBase info in thiInfo.Children)
                {
                    if (info is ListInfo見積依頼)
                    {
                        if (classNo > 2)
                            break;

                        classNo = 2;
                        text = "見積依頼";
                        NormalColor = (info as ListInfo見積依頼).NormalColor();
                        SelectedColor = (info as ListInfo見積依頼).SelectedColor();
                        BorderColor = (info as ListInfo見積依頼).BorderColor();

                        foreach (ListInfoBase mkInfo in info.Children)
                        {
                            if (classNo > 3)
                                break;

                            classNo = 3;

                            if ((mkInfo as ListInfo見積回答).info.Status == (mkInfo as ListInfo見積回答).info.OdStatusValue.MaxValue)
                            {
                                text = "　発注　";
                            }
                            else
                            {
                                text = "見積回答";
                            }
                            NormalColor = (mkInfo as ListInfo見積回答).NormalColor();
                            SelectedColor = (mkInfo as ListInfo見積回答).SelectedColor();
                            BorderColor = (mkInfo as ListInfo見積回答).BorderColor();

                            foreach (ListInfoBase jryInfo in mkInfo.Children)
                            {
                                if (classNo > 4)
                                    break;

                                classNo = 4;
                                text = "受領";
                                NormalColor = (jryInfo as ListInfo受領).NormalColor();
                                SelectedColor = (jryInfo as ListInfo受領).SelectedColor();
                                BorderColor = (jryInfo as ListInfo受領).BorderColor();

                                foreach (ListInfoBase shrInfo in jryInfo.Children)
                                {
                                    classNo = 5;
                                    text = "支払";
                                    NormalColor = (shrInfo as ListInfo支払).NormalColor();
                                    SelectedColor = (shrInfo as ListInfo支払).SelectedColor();
                                    BorderColor = (shrInfo as ListInfo支払).BorderColor();

                                    break;
                                }
                            }
                        }
                    }
                    else if (info is ListInfo見積回答)
                    {
                        if (classNo > 3)
                            break;

                        classNo = 3;
                        if ((info as ListInfo見積回答).info.Status == (info as ListInfo見積回答).info.OdStatusValue.MaxValue)
                        {
                            text = "　発注　";
                        }
                        else
                        {
                            text = "見積回答";
                        }
                        NormalColor = (info as ListInfo見積回答).NormalColor();
                        SelectedColor = (info as ListInfo見積回答).SelectedColor();
                        BorderColor = (info as ListInfo見積回答).BorderColor();

                        foreach (ListInfoBase jryInfo in info.Children)
                        {
                            if (classNo > 4)
                                break;

                            classNo = 4;
                            text = "受領";
                            NormalColor = (jryInfo as ListInfo受領).NormalColor();
                            SelectedColor = (jryInfo as ListInfo受領).SelectedColor();
                            BorderColor = (jryInfo as ListInfo受領).BorderColor();

                            foreach (ListInfoBase shrInfo in jryInfo.Children)
                            {
                                classNo = 5;
                                text = "支払";
                                NormalColor = (shrInfo as ListInfo支払).NormalColor();
                                SelectedColor = (shrInfo as ListInfo支払).SelectedColor();
                                BorderColor = (shrInfo as ListInfo支払).BorderColor();

                                break;
                            }
                        }
                    }
                    else if (info is ListInfo受領)
                    {
                        if (classNo > 4)
                            break;

                        classNo = 4;
                        text = "受領";
                        NormalColor = (info as ListInfo受領).NormalColor();
                        SelectedColor = (info as ListInfo受領).SelectedColor();
                        BorderColor = (info as ListInfo受領).BorderColor();

                        foreach (ListInfoBase shrInfo in info.Children)
                        {
                            classNo = 5;
                            text = "支払";
                            NormalColor = (shrInfo as ListInfo支払).NormalColor();
                            SelectedColor = (shrInfo as ListInfo支払).SelectedColor();
                            BorderColor = (shrInfo as ListInfo支払).BorderColor();

                            break;
                        }
                    }
                }
            }

            TreeListViewNode selectedNode = treeListView船種別変更一覧.SelectedNode;

            // ノードの標準のバックグラウンド
            selectedNode.NormalStyle.BackColor = NormalColor;
            selectedNode.NormalStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.Flat;

            // マウスオーバー時のバックグラウンド
            selectedNode.HoverStyle.BackColor = SelectedColor;
            selectedNode.HoverStyle.BorderColor = BorderColor;
            //node.HoverStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.BackwardVertical;

            // フォーカス時のバックグラウンド
            selectedNode.FocusedStyle.BackColor = SelectedColor;
            //node.FocusedStyle.BorderColor = Color.Black;
            //node.FocusedStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.BackwardVertical;

            // 選択されている時のバックグラウンド
            selectedNode.SelectedStyle.BackColor = SelectedColor;
            //node.SelectedStyle.BorderColor = Color.Black;
            //node.SelectedStyle.FillStyle = LidorSystems.IntegralUI.Style.FillStyle.BackwardVertical;


            selectedNode.SubItems[0].Content = thiInfo.発注状況一覧用文字列(text);



            if (ClickEvent != null && onlyRedraw == false)
            {
                ClickEvent(selectedNode.Tag as ListInfo手配依頼);
            }

            System.Diagnostics.Debug.WriteLine("RedrawSelectedNode <==");
        }



        /// <summary>
        /// スクロールバー一番下にする　2021/08/04 m.yoshihara
        /// </summary>
        public void MoveScrollbarLast()
        {
            int c = treeListView船種別変更一覧.Nodes.Count;
            if (c == 0) return;

            TreeListViewNode node = treeListView船種別変更一覧.Nodes[c-1];

            treeListView船種別変更一覧.EnsureVisible(node);
        }

    }
}
