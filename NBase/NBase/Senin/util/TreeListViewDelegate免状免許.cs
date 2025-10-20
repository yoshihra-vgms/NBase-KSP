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
    internal class TreeListViewDelegate免状免許 : TreeListViewDelegate
    {
        internal TreeListViewDelegate免状免許(TreeListView treeListView)
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
            h.headerContent = "免許／免状";
            h.width = 125;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "種別";
            h.width = 150;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "番号";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "有効期限";
            h.width = 90;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "取得／受講日";
            h.width = 90;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = " 添付 ";
            h.width = 40;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "帳票出力";
            h.width = 60;
            columns.Add(h);

            return columns;
        }

        
        internal void SetRows(List<SiMenjou> menjous)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            var list = menjous.OrderBy(o => o.MsSiMenjouID).ThenBy(o => o.MsSiMenjouKindID);
            for (int i = 0; i < list.Count(); i++)
            {
                SiMenjou m = menjous[i];

                if (m.DeleteFlag == 1)
                {
                    continue;
                }
                
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

                int attachCount = 0;
                foreach (SiMenjouAttachFile a in m.AttachFiles)
                {
                    if (a.DeleteFlag == 0)
                        attachCount++;
                }
                AddSubItem(node, attachCount.ToString(), true);

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
