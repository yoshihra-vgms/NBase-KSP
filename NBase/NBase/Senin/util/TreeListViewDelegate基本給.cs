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
    internal class TreeListViewDelegate基本給 : TreeListViewDelegate
    {
        internal TreeListViewDelegate基本給(TreeListView treeListView)
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
            h.headerContent = "種別";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "期間";
            h.width = 160;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "標令";
            h.width = 55;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            //h = new TreeListViewDelegate.Column();
            //h.headerContent = "職務";
            //h.width = 100;
            //columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "基本給";
            h.width = 100;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "組合費";
            h.width = 100;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            return columns;
        }

        
        internal void SetRows(List<SiSalary> salaries)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            foreach (var obj in salaries)
            {
                if (obj.DeleteFlag == 1)
                {
                    continue;
                }

                TreeListViewNode node = CreateNode();

                AddSubItem(node, MsSiSalary.KindStr(obj.Kind), true);
                string priod = "";
                if (obj.StartDate != DateTime.MinValue && obj.EndDate != DateTime.MinValue)
                {
                    priod = obj.StartDate.ToShortDateString() + " - " + obj.EndDate.ToShortDateString();
                }
                else if (obj.StartDate != DateTime.MinValue)
                {
                    priod = obj.StartDate.ToShortDateString() + " - ";
                }
                else if (obj.EndDate != DateTime.MinValue)
                {
                    priod = " - " + obj.EndDate.ToShortDateString();
                }
                AddSubItem(node, priod, true);

                AddSubItem(node, obj.Hyorei.ToString(), true); // 標令
                //AddSubItem(node, SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, obj.MsSiShokumeiId), true); // 職務


                AddSubItem(node, obj.BasicSalary >= 0 ? NBaseCommon.Common.金額出力(obj.BasicSalary) : "", true);
                AddSubItem(node, obj.UnionDues >= 0 ? NBaseCommon.Common.金額出力(obj.UnionDues) : "", true);
                node.Tag = obj;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
