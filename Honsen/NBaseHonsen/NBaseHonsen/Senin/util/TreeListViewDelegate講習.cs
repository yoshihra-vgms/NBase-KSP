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
    internal class TreeListViewDelegate講習 : TreeListViewDelegate
    {
        internal TreeListViewDelegate講習(TreeListView treeListView)
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
            h.headerContent = "講習名";
            h.width = 175;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "開始予定日";
            h.width = 95;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "終了予定日";
            h.width = 95;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "受講開始日";
            h.width = 95;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "受講終了日";
            h.width = 95;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "有効期限";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "備考";
            h.width = 200;
            columns.Add(h);

            return columns;
        }

        internal void SetRows(List<SiKoushu> koushus)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            foreach (SiKoushu k in koushus)
            {
                if (k.DeleteFlag == 1)
                {
                    continue;
                }

                TreeListViewNode node = CreateNode();

                AddSubItem(node, k.KoushuName, true);
                if (k.YoteiFrom != DateTime.MinValue)
                {
                    AddSubItem(node, k.YoteiFrom.ToShortDateString(), true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }
                if (k.YoteiTo != DateTime.MinValue)
                {
                    AddSubItem(node, k.YoteiTo.ToShortDateString(), true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }
                if (k.JisekiFrom != DateTime.MinValue)
                {
                    AddSubItem(node, k.JisekiFrom.ToShortDateString(), true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }
                if (k.JisekiTo != DateTime.MinValue)
                {
                    AddSubItem(node, k.JisekiTo.ToShortDateString(), true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }
                AddSubItem(node, k.KoushuYukokigenStr, true);
                AddSubItem(node, k.Bikou, true);

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
