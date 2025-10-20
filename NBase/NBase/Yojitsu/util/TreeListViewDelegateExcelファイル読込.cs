using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using System.Drawing;
using NBaseData.DAC;
using Yojitsu.DA;

namespace Yojitsu.util
{
    class TreeListViewDelegateExcelファイル読込 : TreeListViewDelegate
    {
        internal TreeListViewDelegateExcelファイル読込(TreeListView treeListView)
            : base(treeListView)
        {
            SetColumns(CreateColumns());
        }

        
        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "ファイル名";
            h.width = 220;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "最終更新日";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "最終更新者";
            h.width = 100;
            columns.Add(h);

            return columns;
        }


        internal void CreateRow(BgYosanExcel yosanExcel)
        {
            TreeListViewNode node1 = CreateNode();
            AddSubItem(node1, yosanExcel.FileName, true);
            AddSubItem(node1, yosanExcel.RenewDate.ToShortDateString(), true);
            AddSubItem(node1, yosanExcel.RenewUserName, true);
            treeListView.Nodes.Add(node1);
        }
    }
}
