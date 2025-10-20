using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;

namespace Senin.util
{
    internal class TreeListViewDelegate履歴 : TreeListViewDelegate
    {
        internal TreeListViewDelegate履歴(TreeListView treeListView)
            : base(treeListView)
        {
            treeListView.SuspendUpdate();

            SetColumns(CreateColumns());

            treeListView.ResumeUpdate();
        }


        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "職名";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "年月日";
            h.width = 80;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "本給";
            h.width = 130;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "月額給与";
            h.width = 130;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "等級";
            h.width = 55;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "日額";
            h.width = 75;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "備考";
            h.width = 200;
            columns.Add(h);

            return columns;
        }

        
        internal void SetRows(List<SiRireki> rirekis)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();


            // 2010.09.15 日付の降順で表示するため
            // 　　　　　 以下のコードに置き換え
            #region
            //for (int i = 0; i < rirekis.Count; i++)
            //{
            //    SiRireki r = rirekis[i];

            //    if (r.DeleteFlag == 1)
            //    {
            //        continue;
            //    }

            //    TreeListViewNode node = CreateNode();

            //    AddSubItem(node, SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, r.MsSiShokumeiID), true);
            //    AddSubItem(node, r.RirekiDate.ToShortDateString(), true);
            //    AddSubItem(node, NBaseCommon.Common.金額出力(r.Honkyu), true);
            //    AddSubItem(node, NBaseCommon.Common.金額出力(r.Gekkyu), true);
            //    AddSubItem(node, r.Bikou, true);

            //    node.Tag = r;

            //    treeListView.Nodes.Add(node);
            //}
            #endregion
            var rrks = from p in rirekis
                       orderby p.RirekiDate descending
                       select p;

            foreach (var r in rrks)
            {
                if (r.DeleteFlag == 1)
                {
                    continue;
                }

                TreeListViewNode node = CreateNode();

                AddSubItem(node, SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, r.MsSiShokumeiID), true);
                AddSubItem(node, r.RirekiDate.ToShortDateString(), true);
                AddSubItem(node, NBaseCommon.Common.金額出力(r.Honkyu), true);
                AddSubItem(node, NBaseCommon.Common.金額出力(r.Gekkyu), true);
                AddSubItem(node, r.Tokyu != int.MinValue ? r.Tokyu.ToString() : "", true);
                AddSubItem(node, r.Nitigaku != int.MinValue ? NBaseCommon.Common.金額出力(r.Nitigaku) : "", true);
                AddSubItem(node, r.Bikou, true);

                node.Tag = r;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
