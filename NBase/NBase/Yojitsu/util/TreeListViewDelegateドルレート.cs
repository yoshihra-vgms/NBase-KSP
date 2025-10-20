using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using NBaseData.DAC;
using Yojitsu.DA;
using LidorSystems.IntegralUI.Lists;
using System.Windows.Forms;
using System.Drawing;

namespace Yojitsu.util
{
    class TreeListViewDelegateドルレート : TreeListViewDelegate
    {
        protected readonly Dictionary<TreeListViewNode, CellItem> cellItems =
            new Dictionary<TreeListViewNode, CellItem>();


        internal TreeListViewDelegateドルレート(TreeListView treeListView)
            : base(treeListView)
        {
        }


        internal void CreateTable(BgYosanHead yosanHead, bool canEdit)
        {
            treeListView.Columns.Clear();
            treeListView.Nodes.Clear();

            SetColumns(CreateColumns());
            CreateRowData(yosanHead, canEdit);
        }

        
        private List<Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "年";
            h.width = 80;
            h.fixedWidth = true;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "上期（円）";
            h.width = 80;
            h.fixedWidth = true;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            h = new TreeListViewDelegate.Column();
            h.headerContent = "下期（円）";
            h.width = 80;
            h.fixedWidth = true;
            h.alignment = HorizontalAlignment.Right;
            columns.Add(h);

            return columns;
        }


        private void CreateRowData(BgYosanHead yosanHead, bool canEdit)
        {
            List<BgRate> rates = DbTableCache.instance().GetBgRateList(yosanHead);

            foreach (BgRate r in rates)
            {
                Color color;
                bool readOnly;

                if (!canEdit || r.YosanHeadID != yosanHead.YosanHeadID)
                {
                    color = Color.Gainsboro;
                    readOnly = true;
                }
                else
                {
                    color = Color.White;
                    readOnly = false;
                }

                CellItem cellItem = new CellItem(r);
                
                TreeListViewNode node1 = CreateNode(color);
                AddSubItem(node1, r.Year.ToString(), true);

                AddSubItem(node1, NBaseCommon.Common.金額出力(r.KamikiRate), r.KamikiRate.ToString(), readOnly,
                                                   delegate(object sender, EventArgs e)
                                                   {
                                                       TextBox textBox = (TextBox)sender;
                                                       TreeListViewSubItem si = textBox.Tag as TreeListViewSubItem;

                                                       decimal amount;
                                                       Decimal.TryParse(textBox.Text, out amount);
                                                       si.Text = NBaseCommon.Common.金額出力(amount);
                                                       textBox.Text = amount.ToString();
                                                       cellItems[si.Parent].SetKamikiRate(amount);
                                                   });

                AddSubItem(node1, NBaseCommon.Common.金額出力(r.ShimokiRate), r.ShimokiRate.ToString(), readOnly,
                                                   delegate(object sender, EventArgs e)
                                                   {
                                                       TextBox textBox = (TextBox)sender;
                                                       TreeListViewSubItem si = textBox.Tag as TreeListViewSubItem;

                                                       decimal amount;
                                                       Decimal.TryParse(textBox.Text, out amount);
                                                       si.Text = NBaseCommon.Common.金額出力(amount);
                                                       textBox.Text = amount.ToString();
                                                       cellItems[si.Parent].SetShimokiRate(amount);
                                                   });

                treeListView.Nodes.Add(node1);
                cellItems.Add(node1, cellItem);
            }
        }


        public class CellItem
        {
            public BgRate Rate { get; private set; }
            public bool Edited { get; private set; }
           
            
            public CellItem(BgRate rate)
            {
                this.Rate = rate;
            }
            
            internal void SetKamikiRate(decimal kamikiRate)
            {
                if (Rate.KamikiRate != kamikiRate)
                {
                    Rate.KamikiRate = kamikiRate;
                    Edited = true;
                }
            }

            internal void SetShimokiRate(decimal shimokiRate)
            {
                if (Rate.ShimokiRate != shimokiRate)
                {
                    Rate.ShimokiRate = shimokiRate;
                    Edited = true;
                }
            }
        }

        internal List<BgRate> GetEditedBgRates()
        {
            List<BgRate> result = new List<BgRate>();

            foreach (CellItem ci in cellItems.Values)
            {
                if (ci.Edited)
                {
                    BgRate r = (BgRate)ci.Rate;
                    r.RenewDate = DateTime.Now;
                    r.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                    
                    result.Add(r);
                }
            }

            return result;
        }
    }
}
