using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;
using System.Drawing;

namespace Senin.util
{
    internal class TreeListViewDelegate健康診断 : TreeListViewDelegate
    {
        internal TreeListViewDelegate健康診断(TreeListView treeListView)
            : base(treeListView)
        {
            treeListView.SuspendUpdate();

            SetColumns(CreateColumns());

            treeListView.ResumeUpdate();

            SubItemFont = new Font("MS UI Gothic", 9, FontStyle.Regular);
        }


        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();
            TreeListViewDelegate.Column h = null;

            h = new TreeListViewDelegate.Column();
            h.headerContent = "No";
            h.width = 40;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "添付";
            h.width = 40;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "種別";
            h.width = 180;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "病院"; // 指定病院
            h.width = 180;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "診断日"; // 身体検査日
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "有効期限"; // 有効日時
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "備考";
            h.width = 150;
            columns.Add(h);

            return columns;
        }


        internal void SetRows(List<SiKenshin> kenshins)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            for (int i = 0; i < kenshins.Count; i++)
            {
                SiKenshin k = kenshins[i];

                if (k.DeleteFlag == 1)
                {
                    continue;
                }

                TreeListViewNode node = CreateNode();

                AddSubItem(node, (i + 1).ToString(), true);
                int attachCount = 0;
                foreach (SiKenshinAttachFile a in k.AttachFiles)
                {
                    if (a.DeleteFlag == 0)
                        attachCount++;
                }
                AddSubItem(node, attachCount.ToString(), true);
                AddSubItem(node, k.KensaName, true);
                AddSubItem(node, k.Hospital, true);
                AddSubItem(node, k.KensaDay.ToShortDateString(), true);
                if (k.ExpiryDate != DateTime.MinValue)
                {
                    AddSubItem(node, k.ExpiryDate.ToShortDateString(), true);
                }
                else
                {
                    AddSubItem(node, "", true);
                }
                AddSubItem(node, k.Remarks, true);

                node.Tag = k;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
