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
using NBaseCommon.Senin;

namespace NBaseHonsen.Senin.util
{
    internal class TreeListViewDelegate下船_乗船休暇 : TreeListViewDelegate
    {
        internal TreeListViewDelegate下船_乗船休暇(TreeListView treeListView)
            : base(treeListView)
        {
            SubItemFont = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            
            treeListView.SelectionMode = SelectionMode.MultiExtended;
            treeListView.FocusedNodeStyle.BackColor = Color.Lime;
            treeListView.SelectedNodeStyle.BackColor = Color.Lime;

            treeListView.SuspendUpdate();

            SetColumns(CreateColumns());

            treeListView.ResumeUpdate();
        }


        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "職名";
            h.width = 150;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "従業員番号";
            h.width = 125;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "氏名";
            h.width = 140;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "氏名（カナ）";
            h.width = 140;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "乗船日";
            h.width = 100;
            columns.Add(h);

            return columns;
        }

        
        internal void SetRows(List<SiCard> cards)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            List<TreeListViewUtils.SiCardRow> rows = TreeListViewUtils.CreateRowData(cards, NBaseCommon.Common.LoginUser, SeninTableCache.instance());

            foreach (TreeListViewUtils.SiCardRow r in rows)
            {
                TreeListViewNode node = CreateNode();

                AddSubItem(node, SeninTableCache.instance().ToShokumeiStr(NBaseCommon.Common.LoginUser, r.card.SiLinkShokumeiCards), true);
                AddSubItem(node, r.card.SeninShimeiCode, true);
                AddSubItem(node, r.card.SeninName, true);
                AddSubItem(node, r.card.SeninNameKana, true);
                AddSubItem(node, StringUtils.ToStr(r.card.StartDate), true);

                node.Tag = r.card;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
