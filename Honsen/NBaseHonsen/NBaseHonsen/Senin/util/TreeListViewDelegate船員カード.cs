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
    internal class TreeListViewDelegate船員カード : TreeListViewDelegate
    {
        internal TreeListViewDelegate船員カード(TreeListView treeListView)
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
            h.headerContent = "開始日付";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "終了日付";
            h.width = 100;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "種別";
            h.width = 140;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "船";
            h.width = 200;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "職名";
            h.width = 150;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "日数";
            h.width = 60;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            return columns;
        }

        
        internal void SetRows(List<SiCard> cards)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            //
            cards = cards.OrderByDescending(obj => obj.StartDate).ThenByDescending(obj => obj.EndDate).ToList();

            for (int i = 0; i < cards.Count; i++)
            {
                SiCard c = cards[i];

                TreeListViewNode node = CreateNode();

                AddSubItem(node, StringUtils.ToStr(c.StartDate), true);
                AddSubItem(node, StringUtils.ToStr(c.EndDate), true);
                AddSubItem(node, SeninTableCache.instance().GetMsSiShubetsuName(NBaseCommon.Common.LoginUser, c.MsSiShubetsuID), true);
                AddSubItem(node, SeninTableCache.instance().GetMsVesselName(NBaseCommon.Common.LoginUser, c.MsVesselID), true);
                AddSubItem(node, SeninTableCache.instance().ToShokumeiStr(NBaseCommon.Common.LoginUser, c.SiLinkShokumeiCards), true);

                if (Is_休暇管理(c.MsSiShubetsuID))
                {
                    AddSubItem(node, c.Days.ToString(), true);
                }
                else if (c.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.転船))
                {
                    // 2012.03 同日転船対応
                    if (c.Days < 0)
                    {
                        AddSubItem(node, "0", true);
                    }
                    else
                    {
                        AddSubItem(node, StringUtils.ToStr(c.StartDate, c.EndDate), true);
                    }
                }
                else if (c.EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(c.StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(c.EndDate)))
                {
                    AddSubItem(node, StringUtils.ToStr(c.StartDate, DateTime.Now), true);
                }
                else
                {
                    AddSubItem(node, StringUtils.ToStr(c.StartDate, c.EndDate), true);
                }

                node.Tag = c;

                treeListView.Nodes.Add(node);
            }

            treeListView.ResumeUpdate();
        }


        private static bool Is_休暇管理(int msSiShubetsuId)
        {
            return msSiShubetsuId == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.休暇買上) ||
                     msSiShubetsuId == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.本年度休暇日数) ||
                     msSiShubetsuId == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.前年度休暇繰越日数);
        }
    }
}
