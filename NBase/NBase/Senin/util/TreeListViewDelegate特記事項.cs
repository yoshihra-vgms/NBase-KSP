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
    internal class TreeListViewDelegate特記事項 : TreeListViewDelegate
    {
        internal TreeListViewDelegate特記事項(TreeListView treeListView)
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
            h.headerContent = "No";
            h.width = 75;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "年月日";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "タイトル";
            h.width = 200;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "特記事項";
            h.width = 200;
            columns.Add(h);


            return columns;
        }

        
        internal void SetRows(List<SiRemarks> remarks)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            var sortedList = remarks.OrderByDescending(obj => obj.RemarksDate);

            int no = 0;
            foreach (var s in sortedList)
            {
                if (s.DeleteFlag == 1)
                {
                    continue;
                }

                TreeListViewNode node = CreateNode();

                AddSubItem(node, (++no).ToString(), true);
                AddSubItem(node, s.RemarksDate.ToShortDateString(), true);
                AddSubItem(node, s.RemarksName.ToString(), true);
                AddSubItem(node, s.Remarks.ToString(), true);

                node.Tag = s;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
