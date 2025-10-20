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
    internal class TreeListViewDelegate乗船 : TreeListViewDelegate
    {
        internal TreeListViewDelegate乗船(TreeListView treeListView)
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
            h.headerContent = "種別";
            h.width = 140;
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


        internal void SetRows(List<MsSenin> senins)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            List<TreeListViewUtils.MsSeninRow> rows = TreeListViewUtils.CreateRowData(senins, NBaseCommon.Common.LoginUser, SeninTableCache.instance());

            int i = 0;
            foreach (TreeListViewUtils.MsSeninRow r in rows)
            {
                TreeListViewNode node = CreateNode();

                AddSubItem(node, (++i).ToString(), true);
                AddSubItem(node, SeninTableCache.instance().GetMsSiShubetsuName(NBaseCommon.Common.LoginUser, r.senin.MsSiShubetsuID), true);
                AddSubItem(node, SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, r.senin.MsSiShokumeiID), true);
                AddSubItem(node, r.senin.Sei + " " + r.senin.Mei, true);
                AddSubItem(node, r.senin.SeiKana + " " + r.senin.MeiKana, true);
                AddSubItem(node, r.senin.ShimeiCode, true);

                //AddSubItem(node, r.senin.KubunStr, true);
                //AddSubItem(node, r.senin.HokenNo, true);
                //int 合計乗船 = 0;
                //if (r.senin.合計日数 != null && r.senin.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船)))
                //{
                //    合計乗船 = r.senin.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船)];
                //}
                //AddSubItem(node, 合計乗船.ToString(), true);
                //int 合計休暇 = 0;
                //if (r.senin.合計日数 != null && r.senin.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇)))
                //{
                //    合計休暇 = r.senin.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇)];
                //}
                //AddSubItem(node, 合計休暇.ToString(), true);

                node.Tag = r.senin;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
