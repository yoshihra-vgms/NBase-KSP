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
    internal class TreeListViewDelegate免状免許 : TreeListViewDelegate
    {
        internal TreeListViewDelegate免状免許(TreeListView treeListView)
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
            h.headerContent = "免許／免状";
            h.width = 175;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "種別";
            h.width = 175;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "番号";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "有効期限";
            h.width = 110;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "取得／受講日";
            h.width = 110;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "帳票出力";
            h.width = 70;
            columns.Add(h);

            return columns;
        }

        
        internal void SetRows(List<SiMenjou> menjous)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            for (int i = 0; i < menjous.Count; i++)
            {
                SiMenjou m = menjous[i];

                TreeListViewNode node = CreateNode();

                AddSubItem(node, SeninTableCache.instance().GetMsSiMenjouName(NBaseCommon.Common.LoginUser, m.MsSiMenjouID), true);

                if (m.MsSiMenjouKindID != int.MinValue)
                {
                    AddSubItem(node, SeninTableCache.instance().GetMsSiMenjouKindName(NBaseCommon.Common.LoginUser, m.MsSiMenjouKindID), true);
                }
                else
                {
                    AddSubItem(node, string.Empty, true);
                }

                AddSubItem(node, m.No, true);

                if (m.Kigen == DateTime.MinValue)
                {
                    AddSubItem(node, "", true);
                }
                else
                {
                    AddSubItem(node, m.Kigen.ToShortDateString(), true);
                }

                if (m.ShutokuDate == DateTime.MinValue)
                {
                    AddSubItem(node, "", true);
                }
                else
                {
                    AddSubItem(node, m.ShutokuDate.ToShortDateString(), true);
                }

                if (m.ChouhyouFlag == 1)
                {
                    AddSubItem(node, "○", true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }

                node.Tag = m;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
