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
using LidorSystems.IntegralUI.Lists.Collections;

namespace NBaseHonsen.Senin.util
{
    internal class TreeListViewDelegate船員2 : TreeListViewDelegate
    {
        internal TreeListViewDelegate船員2(TreeListView treeListView)
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
            h.headerContent = "No";
            h.width = 60;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "職名";
            h.width = 150;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "氏名";
            h.width = 120;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "従業員番号";
            h.width = 125;
            columns.Add(h);

            return columns;
        }


        internal void SetRows(List<SiCard> cards)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            List<TreeListViewUtils.SiCardRow> rows = TreeListViewUtils.CreateRowData(cards, NBaseCommon.Common.LoginUser, SeninTableCache.instance());

            int i = 0;
            foreach (TreeListViewUtils.SiCardRow r in rows)
            {
                TreeListViewNode node = CreateNode();

                AddSubItem(node, (++i).ToString(), true);
                AddSubItem(node, SeninTableCache.instance().ToShokumeiStr(NBaseCommon.Common.LoginUser, r.card.SiLinkShokumeiCards), true);
                AddSubItem(node, r.card.SeninName, true);
                AddSubItem(node, r.card.SeninShimeiCode, true);

                node.Tag = r.card;
                
                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
        public void AllCheck()
        {
            CheckStateChange(this.treeListView.Nodes, CheckState.Checked);
        }
        public void AllUncheck()
        {
            CheckStateChange(this.treeListView.Nodes, CheckState.Unchecked);
        }
        public void CheckStateChange(TreeListViewNodeCollection nodes, CheckState state)
        {
            foreach (TreeListViewNode childNode in nodes)
            {
                if (childNode.Visible)
                {
                    // Use this method to change the CheckState of the childNode,
                    // without triggerring the BeforeCheck and AfterCheck events
                    this.treeListView.ChangeCheckState(childNode, state);

                    // Repeat the whole cycle for other child nodes
                    CheckStateChange(childNode.Nodes, state);
                }
            }
        }

        public List<string> GetCheckedUserIds()
        {
            List<string> ret = new List<string>();
            foreach (TreeListViewNode itemNode in treeListView.Nodes)
            {
                if (itemNode.CheckState == CheckState.Checked)
                {
                    SiCard s = itemNode.Tag as SiCard;
                    ret.Add(s.MsUserID);
                }
            }
            return ret;
        }

        public List<SiCard> GetCheckedUserCards()
        {
            List<SiCard> ret = new List<SiCard>();
            foreach (TreeListViewNode itemNode in treeListView.Nodes)
            {
                if (itemNode.CheckState == CheckState.Checked)
                {
                    SiCard s = itemNode.Tag as SiCard;
                    ret.Add(s);
                }
            }
            return ret;
        }
    }
}
