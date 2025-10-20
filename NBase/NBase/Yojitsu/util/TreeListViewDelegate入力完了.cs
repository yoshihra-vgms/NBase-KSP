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
    class TreeListViewDelegate入力完了 : TreeListViewDelegate
    {
        internal TreeListViewDelegate入力完了(TreeListView treeListView)
            : base(treeListView)
        {
        }


        internal void CreateTable(BgYosanHead yosanHead)
        {
            treeListView.Columns.Clear();
            treeListView.Nodes.Clear();

            SetColumns(CreateColumns());
            CreateYojitsuRowData(yosanHead);
        }

        
        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "グループ";
            h.width = 80;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "入力完了日";
            h.width = 100;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "入力完了者";
            h.width = 100;
            h.fixedWidth = true;
            columns.Add(h);

            return columns;
        }


        private void CreateYojitsuRowData(BgYosanHead yosanHead)
        {
            List<BgNrkKanryou> kanryous = DbAccessorFactory.FACTORY.BgNrkKanryou_GetRecordsByYosanHeadID(NBaseCommon.Common.LoginUser, 
                                                                                                         yosanHead.YosanHeadID);

            foreach (BgNrkKanryou k in kanryous)
            {
                if (Constants.IncludeHimoku(k.MsBumonID))
                {
                    TreeListViewNode node1 = CreateNode(Constants.GetMsBumonColor(k.MsBumonID));
                    AddSubItem(node1, k.BumonName, true);
                    AddSubItem(node1, To入力完了日(k.NrkKanryo), true);
                    AddSubItem(node1, To入力完了者(k.NrkKanryouUserName), true);
                    treeListView.Nodes.Add(node1);
                }
            }
        }

        private static string To入力完了日(DateTime nrkKanryo)
        {
            if (nrkKanryo == DateTime.MinValue)
            {
                return "-";
            }
            
            return nrkKanryo.ToShortDateString();
        }

        
        private static string To入力完了者(string nrkKanryouUserName)
        {
            if (nrkKanryouUserName == null || nrkKanryouUserName.Trim() == string.Empty)
            {
                return "-";
            }
            
            return nrkKanryouUserName;
        }
    }
}
