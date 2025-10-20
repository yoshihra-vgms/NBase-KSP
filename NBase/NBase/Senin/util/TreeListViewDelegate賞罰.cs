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
    internal class TreeListViewDelegate賞罰 : TreeListViewDelegate
    {
        internal TreeListViewDelegate賞罰(TreeListView treeListView)
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
            h.headerContent = "賞罰名";
            h.width = 200;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "備考";
            h.width = 200;
            columns.Add(h);


            return columns;
        }

        
        internal void SetRows(List<SiShobatsu> shobatsus)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            var sortedList = shobatsus.OrderByDescending(obj => obj.ShobatsuDate);

            int no = 0;
            foreach (var s in sortedList)
            {
                if (s.DeleteFlag == 1)
                {
                    continue;
                }

                TreeListViewNode node = CreateNode();

                AddSubItem(node, (++no).ToString(), true);
                AddSubItem(node, s.ShobatsuDate.ToShortDateString(), true);
                AddSubItem(node, s.ShobatsuName.ToString(), true);
                AddSubItem(node, s.Remarks.ToString(), true);

                node.Tag = s;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
