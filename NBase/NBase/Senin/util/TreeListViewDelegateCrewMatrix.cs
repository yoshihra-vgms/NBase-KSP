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

namespace Senin.util
{
    internal class TreeListViewDelegateCrewMatrix : TreeListViewDelegate
    {
        internal TreeListViewDelegateCrewMatrix(TreeListView treeListView)
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

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "会社";
            h.width = 150;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "Years with" + System.Environment.NewLine + "Operator";
            h.width = 85;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "Years In" + System.Environment.NewLine + "Rank";
            h.width = 85;
            columns.Add(h);


            List<string> crewMatrixName = new List<string>();

            foreach (MsCrewMatrixType type in SeninTableCache.instance().GetMsCrewMatrixTypeList(NBaseCommon.Common.LoginUser))
            {
                h = new TreeListViewDelegate.Column();
                h.headerContent = "Years on" + System.Environment.NewLine + "Type Of Tankers" + System.Environment.NewLine + "(" + type.TypeName + ")";
                h.width = 105;
                columns.Add(h);
            }

            return columns;
        }


        internal void SetRows(List<SiSimulationDetail> details)
        {
            treeListView.SuspendUpdate();
            treeListView.Nodes.Clear();

            TreeListViewNode node = null;

            for (int i = 0; i < details.Count; i++)
            {
                SiSimulationDetail detail = details[i];

                node = CreateNode();

                AddSubItem(node, detail.CompanyName, true);

                AddSubItem(node, CalcYears(detail.YearsWithOperator).ToString("0.0").PadLeft(4), true);

                AddSubItem(node, CalcYears(detail.YearsInRank).ToString("0.0").PadLeft(4), true);

                for (int j = 0; j < detail.Items.Count; j++)
                {
                    AddSubItem(node, CalcYears(detail.Items[j].YearsOnThisType).ToString("0.0").PadLeft(4), true);
                }

                node.Tag = detail;

                treeListView.Nodes.Add(node);
            }

            #region TOTAL行

            node = CreateNode(Color.Silver);
            decimal YearsWithOperator = 0;
            decimal YearsInRank = 0;
            List<decimal> YearsOnThisTypes = new List<decimal>();

            for (int i = 0; i < details.Count; i++)
            {
                SiSimulationDetail detail = details[i];
                YearsWithOperator += detail.YearsWithOperator;

                YearsInRank += detail.YearsInRank;

                for (int j = 0; j < detail.Items.Count; j++)
                {
                    if (YearsOnThisTypes.Count == j)
                    {
                        decimal d = 0;
                        YearsOnThisTypes.Add(d);
                    }
                    YearsOnThisTypes[j] += detail.Items[j].YearsOnThisType;
                }

            }
            AddSubItem(node, "ＴＯＴＡＬ", true);
            AddSubItem(node, CalcYears(YearsWithOperator).ToString("0.0").PadLeft(4), true);
            AddSubItem(node, CalcYears(YearsInRank).ToString("0.0").PadLeft(4), true);
            foreach (decimal yearsOnThisType in YearsOnThisTypes)
            {
                AddSubItem(node, CalcYears(yearsOnThisType).ToString("0.0").PadLeft(4), true);
            }
            node.Tag = null;
            treeListView.Nodes.Add(node);

            #endregion

            treeListView.ResumeUpdate();
        }

        public static decimal CalcYears(decimal days)
        {
            return days / 365;
        }
    }
}
