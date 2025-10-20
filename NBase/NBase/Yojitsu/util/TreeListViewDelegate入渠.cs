using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DAC;
using Yojitsu.DA;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace Yojitsu.util
{
    class TreeListViewDelegate入渠 : TreeListViewDelegate
    {
        private BgYosanHead yosanHead;
        private int msVesselId;

        private CellItem_入渠.Collection cellItemCol;

        private bool Editable;

        private bool updated;


        internal TreeListViewDelegate入渠(TreeListView treeListView)
            : base(treeListView)
        {
        }


        internal void CreateTable(BgYosanHead yosanHead, int msVesselId, List<BgKadouVessel> kadouVessels, List<BgUchiwakeYosanItem> items)
        {
            this.yosanHead = yosanHead;
            this.msVesselId = msVesselId;

            Editable = !yosanHead.IsFixed();

            treeListView.SuspendUpdate();

            treeListView.Columns.Clear();

            SetColumns(CreateColumns(kadouVessels));

            cellItemCol = BuildCellItemDictionary(yosanHead, items);
            CreateRows();

            treeListView.Columns[0].Fixed = ColumnFixedType.Left;

            treeListView.ResumeUpdate();
        }


        private CellItem_入渠.Collection BuildCellItemDictionary(BgYosanHead yosanHead, List<BgUchiwakeYosanItem> yosanItems)
        {
            CellItem_入渠.Collection col = new CellItem_入渠.Collection();

            foreach (BgUchiwakeYosanItem item in yosanItems)
            {
                col.Add(item);
            }

            return col;
        }


        private List<Column> CreateColumns(List<BgKadouVessel> kadouVessels)
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            // 費目
            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "<div><p>費目</p><p>稼働期間</p><p>検査種別 / 不稼働月 / 前 / 入渠 / 後</p></div>";
            h.width = 200;
            h.fixedWidth = true;
            columns.Add(h);

            for (int i = 0; i < NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID); i++)
            {
                // 金額
                h = new TreeListViewDelegate.Column();

                h.headerContent = "<div><p>" + (yosanHead.Year + i) + " 年度予算</p>" + YojitsuTreeListViewDelegate.Create船稼働String(kadouVessels[i]) + "</div>";
                h.width = 90;
                h.alignment = HorizontalAlignment.Right;
                h.headerColor = YojitsuTreeListViewDelegate.COLOR_予算;
                h.rightShape = LidorSystems.IntegralUI.Style.CornerShape.Squared;
                columns.Add(h);

                // 内訳項目
                h = new TreeListViewDelegate.Column();

                h.width = 150;
                h.alignment = HorizontalAlignment.Left;
                h.headerColor = YojitsuTreeListViewDelegate.COLOR_予算;
                h.leftShape = LidorSystems.IntegralUI.Style.CornerShape.Squared;
                columns.Add(h);
            }

            return columns;
        }


        private void CreateRows()
        {
            treeListView.Nodes.Clear();

            CreateRows(HimokuTreeReader_入渠.GetHimokuTree());
            cellItemCol.NotifyParents();

            updated = false;
        }


        private void CreateRows(HimokuTreeNode htNode)
        {
            Dictionary<string, List<CellItem_入渠>> parentCellItems = cellItemCol.GetParentCellItems(htNode);

            foreach (HimokuTreeNode childHtNode in htNode.Children)
            {
                // 1費目に対して2個以上の内訳費目があるときの追加行生成.
                int rowCount = cellItemCol.MaxRow(childHtNode);

                for (int k = 0; k < rowCount; k++)
                {
                    TreeListViewNode tlvNode = BuildNode(cellItemCol, childHtNode, k, parentCellItems);

                    // 子費目について再帰呼び出し
                    if (k == 0 && childHtNode.Children != null)
                    {
                        CreateRows(childHtNode);
                    }

                    treeListView.Nodes.Add(tlvNode);
                }
            }
        }


        private TreeListViewNode BuildNode(CellItem_入渠.Collection itemsDic, HimokuTreeNode childHtNode, int rowNo,
                                             Dictionary<string, List<CellItem_入渠>> parentCellItems)
        {
            TreeListViewNode childTlvNode = CreateNode(childHtNode.BgColor, childHtNode);

            // 費目名
            AddSubItem(childTlvNode, rowNo == 0 ? childHtNode.Name : string.Empty, true);

            for (int i = 0; i < NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID); i++)
            {
                string year = (yosanHead.Year + i).ToString();

                CellItem_入渠 cellItem = itemsDic.Get(childHtNode, year, rowNo);

                if (cellItem != null)
                {
                    if (cellItem.YosanItem.MsHimokuID == CellItem_入渠.Collection.MS_HIMOKU_ID_備考)
                    {
                        AddSubItem(childTlvNode, string.Empty, true);
                        AddBikouSubItem(cellItem, childHtNode, childTlvNode, !Editable);
                    }
                    else
                    {
                        bool readOnly = false;

                        if (childHtNode.MsHimoku == null || !Editable)
                        {
                            readOnly = true;
                        }

                        AddAmountSubItem(cellItem, childHtNode, childTlvNode, readOnly);
                        AddBikouSubItem(cellItem, childHtNode, childTlvNode, readOnly);

                        if (parentCellItems != null)
                        {
                            if (!cellItem.Parents.Contains(parentCellItems[year][0]))
                            {
                                cellItem.Parents.Add(parentCellItems[year][0]);
                                parentCellItems[year][0].VChildrenPlus.Add(cellItem);
                            }
                        }
                    }
                }
                else
                {
                    AddSubItem(childTlvNode, string.Empty, true);
                    AddSubItem(childTlvNode, string.Empty, true);
                }
            }

            return childTlvNode;
        }


        private void AddAmountSubItem(CellItem_入渠 item, HimokuTreeNode childHtNode, TreeListViewNode childTlvNode, bool readOnly)
        {
            decimal amount = item.YosanItem.Amount / 1000;

            string subItemValue = NBaseCommon.Common.金額出力(amount);
            string textBoxValue = amount.ToString();

            if (cellItemCol.IsLast(item) && !readOnly)
            {
                subItemValue = "(追加)";
            }

            TreeListViewSubItem subItem =
              AddSubItem(childTlvNode,
                         subItemValue,
                         textBoxValue,
                         readOnly,
                         false,
                         7,//5, // 2014.08.06：201408月度改造
                         ImeMode.Disable,
                     delegate(object sender, EventArgs e)
                     {
                         TextBox textBox = (TextBox)sender;
                         TreeListViewSubItem si = textBox.Tag as TreeListViewSubItem;

                         CellItem_入渠 cellItem = si.Tag as CellItem_入渠;

                         decimal am;
                         if (!Decimal.TryParse(textBox.Text, out am))
                         {
                             am = cellItem.YosanItem.Amount / 1000;
                             textBox.Text = am.ToString();
                             si.Text = NBaseCommon.Common.金額出力(am);
                         }
                         else
                         {
                             if ((!cellItemCol.IsLast(cellItem) || am != 0) && am != cellItem.YosanItem.Amount / 1000)
                             {
                                 si.Text = NBaseCommon.Common.金額出力(am);
                                 textBox.Text = am.ToString();

                                 cellItem.SetAmount(am * 1000);
                                 cellItem.NotifyParents();

                                 if (cellItemCol.IsLast(cellItem))
                                 {
                                     cellItemCol.AddNew(cellItem);

                                     treeListView.SuspendUpdate();

                                     CreateRows();

                                     treeListView.ResumeUpdate();
                                 }

                                 updated = true;
                             }
                         }
                     });

            subItem.Tag = item;
            item.SubItemAmount = subItem;
        }


        internal List<BgUchiwakeYosanItem> GetEditedBgUchiwakeYosanItems()
        {
            List<BgUchiwakeYosanItem> yosanItems = new List<BgUchiwakeYosanItem>();

            foreach (CellItem_入渠 item in cellItemCol)
            {
                if (item.Edited && (!cellItemCol.IsLast(item) || item.YosanItem.MsHimokuID == CellItem_入渠.Collection.MS_HIMOKU_ID_備考))
                {
                    if (item.YosanItem.IsNew())
                    {
                        DetectVesselYosanId(item.YosanItem);
                    }

                    yosanItems.Add(item.YosanItem);
                }
            }

            return yosanItems;
        }


        internal Dictionary<int, Dictionary<string, decimal>> Get修繕費Dic()
        {
            Dictionary<int, Dictionary<string, decimal>> amountDic =
                               new Dictionary<int, Dictionary<string, decimal>>();

            foreach (CellItem_入渠 item in cellItemCol)
            {
                if (!cellItemCol.IsLast(item))
                {
                    BgUchiwakeYosanItem yosanItem = item.YosanItem;

                    if (!amountDic.ContainsKey(yosanItem.MsHimokuID))
                    {
                        amountDic[yosanItem.MsHimokuID] = new Dictionary<string, decimal>();
                    }

                    if (!amountDic[yosanItem.MsHimokuID].ContainsKey(yosanItem.Nengetsu.Trim()))
                    {
                        amountDic[yosanItem.MsHimokuID][yosanItem.Nengetsu.Trim()] = yosanItem.Amount;
                    }
                    else
                    {
                        amountDic[yosanItem.MsHimokuID][yosanItem.Nengetsu.Trim()] += yosanItem.Amount;
                    }
                }
            }

            return amountDic;
        }


        private void DetectVesselYosanId(BgUchiwakeYosanItem bgUchiwakeYosanItem)
        {
            BgVesselYosan vesselYosan = DbAccessorFactory.FACTORY.BgVesselYosan_GetRecordByYearAndYosanHeadIdAndMsVesselId(NBaseCommon.Common.LoginUser,
                                                    int.Parse(bgUchiwakeYosanItem.Nengetsu.Trim()),
                                                    yosanHead.YosanHeadID,
                                                    msVesselId
                                                   );

            bgUchiwakeYosanItem.VesselYosanID = vesselYosan.VesselYosanID;
        }


        private void AddBikouSubItem(CellItem_入渠 item, HimokuTreeNode childHtNode, TreeListViewNode childTlvNode, bool readOnly)
        {
            bool multiLine = false;

            if (item.YosanItem.MsHimokuID == CellItem_入渠.Collection.MS_HIMOKU_ID_備考)
            {
                multiLine = true;
            }

            TreeListViewSubItem subItem =
             AddSubItem(childTlvNode,
                        item.YosanItem.Bikou,
                        item.YosanItem.Bikou,
                        readOnly,
                        multiLine,
                        500,
                        ImeMode.On,
                        new EventHandler(this.BikouLeaved));

            subItem.Tag = item;
            item.SubItemBikou = subItem;
        }


        private void BikouLeaved(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            TreeListViewSubItem si = textBox.Tag as TreeListViewSubItem;

            CellItem_入渠 cellItem = si.Tag as CellItem_入渠;

            if (!cellItemCol.IsLast(cellItem) || cellItem.YosanItem.MsHimokuID == CellItem_入渠.Collection.MS_HIMOKU_ID_備考)
            {
                cellItem.SetBikou(textBox.Text);
                updated = true;
            }
        }


        internal bool IsUpdated()
        {
            return updated;
        }
    }
}
