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

namespace NBaseHonsen.Senin.util
{
    internal class TreeListViewDelegate船内収支 : TreeListViewDelegate
    {
        private decimal 受入金額合計;
        private decimal 支払金額合計;


        internal TreeListViewDelegate船内収支(TreeListView treeListView)
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
            h.headerContent = "日付";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "支払額";
            h.width = 100;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "消費税";
            h.width = 60;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "受入額";
            h.width = 100;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "明細";
            h.width = 300;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "備考";
            h.width = 150;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "登録者";
            h.width = 110;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "更新者";
            h.width = 110;
            columns.Add(h);

            return columns;
        }


        internal void SetRows(DateTime month, DateTime 先月締め日, decimal 先月末残高, List<SiJunbikin> junbikins)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            受入金額合計 = 支払金額合計 = 0;
            SetRow_先月末残高(先月締め日, 先月末残高);

            for (int i = 0; i < junbikins.Count; i++)
            {
                SiJunbikin j = junbikins[i];
                
                TreeListViewNode node = CreateNode();

                AddSubItem(node, (i + 2).ToString(), true);
                AddSubItem(node, j.JunbikinDate.ToShortDateString(), true);
                AddSubItem(node, NBaseCommon.Common.金額出力(j.KingakuOut), true);
                AddSubItem(node, NBaseCommon.Common.金額出力(j.TaxOut), true);
                AddSubItem(node, NBaseCommon.Common.金額出力(j.KingakuIn + j.TaxIn), true);
                AddSubItem(node, SeninTableCache.instance().GetMsSiMeisaiName(NBaseCommon.Common.LoginUser, j.MsSiMeisaiID), true);
                AddSubItem(node, j.Bikou, true);
                AddSubItem(node, j.TourokuUserName, true);
                AddSubItem(node, j.KoushinUserName, true);

                node.Tag = j;
                
                treeListView.Nodes.Add(node);

                支払金額合計 += j.KingakuOut + j.TaxOut;
                受入金額合計 += j.KingakuIn + j.TaxIn;
            }

            treeListView.ResumeUpdate();
        }


        private void SetRow_先月末残高(DateTime 先月締め日, decimal 先月末残高)
        {
            TreeListViewNode node = CreateNode();

            AddSubItem(node, "1", true);
            AddSubItem(node, 先月締め日.ToShortDateString(), true);
            AddSubItem(node, string.Empty, true);
            AddSubItem(node, string.Empty, true);
            AddSubItem(node, NBaseCommon.Common.金額出力(先月末残高), true);
            AddSubItem(node, string.Empty, true);
            AddSubItem(node, string.Empty, true);
            AddSubItem(node, "先月末残高", true);
            AddSubItem(node, string.Empty, true);
            AddSubItem(node, string.Empty, true);
            AddSubItem(node, string.Empty, true);
            AddSubItem(node, string.Empty, true);
            AddSubItem(node, string.Empty, true);

            treeListView.Nodes.Add(node);

            受入金額合計 += 先月末残高;
        }


        internal decimal Get_受入金額合計()
        {
            return 受入金額合計;
        }


        internal decimal Get_支払金額合計()
        {
            return 支払金額合計;
        }
    }
}
