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
    internal class TreeListViewDelegate船用金送金 : TreeListViewDelegate
    {
        internal TreeListViewDelegate船用金送金(TreeListView treeListView)
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
            h.width = 40;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);
            
            h = new TreeListViewDelegate.Column();
            h.headerContent = "船";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "金額";
            h.width = 90;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "送金日";
            h.width = 80;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "代理店";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "送金者";
            h.width = 87;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "備考";
            h.width = 200;
            columns.Add(h);

            return columns;
        }

        
        internal void SetRows(List<SiSoukin> soukins)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            for (int i = 0; i < soukins.Count; i++)
            {
                SiSoukin s = soukins[i];
                
                TreeListViewNode node = CreateNode();

                AddSubItem(node, (i + 1).ToString(), true);
                AddSubItem(node, SeninTableCache.instance().GetMsVesselName(NBaseCommon.Common.LoginUser, s.MsVesselID), true);
                AddSubItem(node, NBaseCommon.Common.金額出力(s.Kingaku), true);
                AddSubItem(node, s.SoukinDate.ToShortDateString(), true);
                AddSubItem(node, s.CustomerName, true);
                AddSubItem(node, s.SoukinUserName, true);
                AddSubItem(node, s.Bikou, true);

                node.Tag = s;
                
                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
