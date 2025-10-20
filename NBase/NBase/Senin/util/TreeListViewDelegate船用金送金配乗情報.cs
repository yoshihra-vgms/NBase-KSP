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
    internal class TreeListViewDelegate船用金送金配乗情報 : TreeListViewDelegate
    {

        internal TreeListViewDelegate船用金送金配乗情報(TreeListView treeListView)
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
            h.width = 120;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "人数";
            h.width = 40;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            return columns;
        }

        
        internal void SetRows(Dictionary<int, int> rowData)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();
            
            foreach(MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                TreeListViewNode node = CreateNode();

                AddSubItem(node, s.Name, true);

                if (rowData.ContainsKey(s.MsSiShokumeiID))
                {
                    AddSubItem(node, rowData[s.MsSiShokumeiID].ToString(), true);
                }
                else
                {
                    AddSubItem(node, "0", true);
                }
                
                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        
            
        }
    }
}
