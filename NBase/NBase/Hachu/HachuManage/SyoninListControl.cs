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
    public partial class SyoninListControl : UserControl
    {
        public delegate void ClickEventHandler(ListInfoBase selectedNode);
        public event ClickEventHandler ClickEvent;

        public SyoninListControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="width"></param>
        #region public void Init(int width)
        public void Init(int width)
        {
            // ヘッダ、フッタは見せない
            treeListView承認一覧.Header = false;
            treeListView承認一覧.Footer = false;

            // カラム１（情報を表示）
            TreeListViewColumn column1 = new TreeListViewColumn();
            column1.HeaderText = "情報";
            column1.Width = width;
            treeListView承認一覧.Columns.Add(column1);
            treeListView承認一覧.Columns[0].ContentType = ColumnContentType.Custom;

            // 色がつくのは文字の開始位置から
            treeListView承認一覧.ItemHighlightType = ListItemHighlightType.Partial;

            treeListView承認一覧.Nodes.Clear();
        }
        #endregion

        /// <summary>
        /// 一覧にデータをセットする
        /// </summary>
        /// <param name="OdMks"></param>
        /// <param name="OdJrys"></param>
        /// <param name="OdShrs"></param>
        #region public void DrawList(List<OdMk> OdMks, List<OdJry> OdJrys, List<OdShr> OdShrs)
        public void DrawList(List<OdMk> OdMks, List<OdJry> OdJrys, List<OdShr> OdShrs)
        {

            treeListView承認一覧.Nodes.Clear();
            treeListView承認一覧.SuspendUpdate();

            foreach (OdMk mk in OdMks)
            {
                ListInfo見積回答 mkInfo = new ListInfo見積回答();
                mkInfo.info = mk;

                //ノードを作成
                TreeListViewNode mkNode = MakeTreeListViewNode("発注", mkInfo.NormalColor(), mkInfo.SelectedColor(), mkInfo.BorderColor(), mkInfo.承認一覧用文字列());
                mkNode.Tag = mkInfo;
                treeListView承認一覧.Nodes.Add(mkNode);

            }
            foreach (OdJry jry in OdJrys)
            {
                ListInfo受領 jryInfo = new ListInfo受領();
                jryInfo.info = jry;

                //ノードを作成
                TreeListViewNode jryNode = MakeTreeListViewNode("受領", jryInfo.NormalColor(), jryInfo.SelectedColor(), jryInfo.BorderColor(), jryInfo.承認一覧用文字列());
                jryNode.Tag = jryInfo;
                treeListView承認一覧.Nodes.Add(jryNode);

            }
            foreach (OdShr shr in OdShrs)
            {
                ListInfo支払 shrInfo = new ListInfo支払();
                shrInfo.info = shr;

                //ノードを作成
                TreeListViewNode shrNode = MakeTreeListViewNode("支払", shrInfo.NormalColor(), shrInfo.SelectedColor(), shrInfo.BorderColor(), shrInfo.承認一覧用文字列());
                shrNode.Tag = shrInfo;
                treeListView承認一覧.Nodes.Add(shrNode);

            }

            treeListView承認一覧.ResumeUpdate();

        }
        #endregion


        /// <summary>
        /// 現在選択されているノードを削除する
        /// </summary>
        #region public void RemoveSelectedNode()
        public void RemoveSelectedNode()
        {
            // それぞれの詳細画面で「承認」または「差戻」を実行
            try
            {
                // 一覧(TreeListView)から削除する
                TreeListViewNode node = treeListView承認一覧.SelectedNode;
                treeListView承認一覧.Nodes.Remove(node);
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
        #region private void treeListView承認一覧_Click(object sender, EventArgs e)
        private void treeListView承認一覧_Click(object sender, EventArgs e)
        {
            TreeListViewNode selectedNode = treeListView承認一覧.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }

            if (ClickEvent != null)
            {
                ClickEvent(selectedNode.Tag as ListInfoBase);
            }
        }
        #endregion
    }
}
