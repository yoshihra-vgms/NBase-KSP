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
    internal class TreeListViewDelegate傷病 : TreeListViewDelegate
    {
        internal TreeListViewDelegate傷病(TreeListView treeListView)
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
            h.width = 75;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "ステータス";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "等級";
            h.width = 55;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "日額";
            h.width = 75;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "対象期間";
            h.width = 170;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "船名";
            h.width = 150;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "職名";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "傷病名";
            h.width = 200;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "口座";
            h.width = 130;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "金額";
            h.width = 75;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "書類送付";
            h.width = 80;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "書類返送";
            h.width = 80;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "提出日";
            h.width = 80;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "通知";
            h.width = 80;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "立替金伝票";
            h.width = 80;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "入金伝票";
            h.width = 80;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "送金";
            h.width = 80;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "本人郵送";
            h.width = 80;
            columns.Add(h);

            return columns;
        }

        
        internal void SetRows(List<SiShobyo> shobyos)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            var sortedList = shobyos.OrderByDescending(obj => obj.FromDate);

            foreach (var s in sortedList)
            {
                if (s.DeleteFlag == 1)
                {
                    continue;
                }

                TreeListViewNode node = CreateNode();

                AddSubItem(node, SiShobyo.KIND[s.Kind], true);
                AddSubItem(node, SiShobyo.STATUS[s.Status], true);
                AddSubItem(node, s.Tokyu.ToString(), true);
                AddSubItem(node, NBaseCommon.Common.金額出力(s.Nitigaku), true);

                string dateFromTo = s.FromDate.ToShortDateString() + "～";
                if (s.ToDate != DateTime.MinValue)
                {
                    dateFromTo += s.ToDate.ToShortDateString();
                }
                AddSubItem(node, dateFromTo, true);
                AddSubItem(node, SeninTableCache.instance().GetMsVesselName(NBaseCommon.Common.LoginUser, s.MsVesselID), true);
                AddSubItem(node, SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, s.MsSiShokumeiID), true);
                AddSubItem(node, s.ShobyoName, true);
                AddSubItem(node, s.Kouza, true);
                AddSubItem(node, NBaseCommon.Common.金額出力(s.Kingaku), true);

                AddSubItem(node, s.SendDocument != DateTime.MinValue ? s.SendDocument.ToShortDateString() : "", true);
                AddSubItem(node, s.DocumentReturn != DateTime.MinValue ? s.DocumentReturn.ToShortDateString() : "", true);
                AddSubItem(node, s.FilingDate != DateTime.MinValue ? s.FilingDate.ToShortDateString() : "", true);
                AddSubItem(node, s.Notification != DateTime.MinValue ? s.Notification.ToShortDateString() : "", true);
                AddSubItem(node, s.AdvanceVoucher != DateTime.MinValue ? s.AdvanceVoucher.ToShortDateString() : "", true);
                AddSubItem(node, s.DepositSlip != DateTime.MinValue ? s.DepositSlip.ToShortDateString() : "", true);
                AddSubItem(node, s.MoneyTransfer != DateTime.MinValue ? s.MoneyTransfer.ToShortDateString() : "", true);
                AddSubItem(node, s.MailToPrincipal != DateTime.MinValue ? s.MailToPrincipal.ToShortDateString() : "", true);

                node.Tag = s;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }
    }
}
