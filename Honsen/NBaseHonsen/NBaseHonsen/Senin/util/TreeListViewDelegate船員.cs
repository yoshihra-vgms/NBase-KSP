using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using NBaseData.DAC;
using System.Drawing;
using NBaseData.DS;
using NBaseCommon.Senin;

namespace NBaseHonsen.Senin.util
{
    internal class TreeListViewDelegate船員 : TreeListViewDelegate
    {
        internal TreeListViewDelegate船員(TreeListView treeListView)
            : base(treeListView)
        {
            SubItemFont = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            treeListView.SuspendUpdate();

            SetColumns(CreateColumns());

            treeListView.ResumeUpdate();
        }


        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "No";
            h.width = 40;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);
            h = new TreeListViewDelegate.Column();
            h.headerContent = "職名";
            h.width = 150;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "氏名";
            h.width = 125;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "氏名（カナ）";
            h.width = 150;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "従業員番号";
            h.width = 125;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "乗船日";
            h.width = 100;
            columns.Add(h);

            //h = new TreeListViewDelegate.Column();
            //h.headerContent = "種別";
            //h.width = 100;
            //columns.Add(h);

            //h = new TreeListViewDelegate.Column();
            //h.headerContent = "社員区分";
            //h.width = 90;
            //columns.Add(h);

            //h = new TreeListViewDelegate.Column();
            //h.headerContent = "保険番号";
            //h.width = 60;
            //columns.Add(h);

            //h = new TreeListViewDelegate.Column();
            //h.headerContent = "合計乗船";
            //h.width = 100;
            //h.alignment = HorizontalAlignment.Right;
            //columns.Add(h);

            //h = new TreeListViewDelegate.Column();
            //h.headerContent = "合計休暇";
            //h.width = 100;
            //h.alignment = HorizontalAlignment.Right;
            //columns.Add(h);

            return columns;
        }


        internal void SetRows(List<SiCard> cards)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            List<TreeListViewUtils.SiCardRow> rows = TreeListViewUtils.CreateRowData(cards, NBaseCommon.Common.LoginUser, SeninTableCache.instance());

            int i = 0;
            foreach (TreeListViewUtils.SiCardRow r in rows)
            {
                TreeListViewNode node = CreateNode();

                AddSubItem(node, (++i).ToString(), true);
                AddSubItem(node, SeninTableCache.instance().ToShokumeiStr(NBaseCommon.Common.LoginUser, r.card.SiLinkShokumeiCards), true);
                AddSubItem(node, r.card.SeninName, true);
                AddSubItem(node, r.card.SeninNameKana, true);
                AddSubItem(node, r.card.SeninShimeiCode, true);
                AddSubItem(node, r.乗船日, true);


                //AddSubItem(node, SeninTableCache.instance().GetMsSiShubetsuName(NBaseCommon.Common.LoginUser, r.card.MsSiShubetsuID), true);
                //AddSubItem(node, r.card.SeninKubunStr, true);
                //AddSubItem(node, r.card.SeninHokenNo, true);
                //int 合計乗船 = 0;
                //if (r.card.合計日数 != null && r.card.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船)))
                //{
                //    合計乗船 = r.card.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船)];
                //}
                //AddSubItem(node, 合計乗船.ToString(), true);

                //int 合計休暇 = 0;
                //if (r.card.合計日数 != null && r.card.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇)))
                //{
                //    合計休暇 = r.card.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇)];
                //}
                //AddSubItem(node, 合計休暇.ToString(), true);

                node.Tag = r.card;
                
                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
