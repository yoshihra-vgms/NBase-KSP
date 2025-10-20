using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseUtil;
using System.Drawing;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DAC;
using System.Windows.Forms;
using Yojitsu.DA;

namespace Yojitsu.util
{
    class GetsujiTreeListViewDelegate : YojitsuTreeListViewDelegate
    {
        private BgKadouVessel kadouVessel;
        // 前年度最終.
        private BgKadouVessel lastKadouVessel;
        
        
        internal GetsujiTreeListViewDelegate(TreeListView treeListView)
            : base(treeListView)
        {
        }


        internal void CreateYojitsuTable(BgYosanHead yosanHead, BgKadouVessel kadouVessel, BgKadouVessel lastKadouVessel,
                                         List<BgYosanItem> yosanItems_前今年度, List<BgYosanItem> yosanItems_今年度_月別,
                                         List<BgJiseki> jisekis_前年度, List<BgJiseki> jisekis_今年度_月別)
        {
            this.yosanHead = yosanHead;
            this.kadouVessel = kadouVessel;
            this.lastKadouVessel = lastKadouVessel;

            Editable = !yosanHead.IsFixed();
            
            treeListView.SuspendUpdate();

            treeListView.Columns.Clear();
            treeListView.Nodes.Clear();
            
            hasYosanItemCellItems.Clear();

            List<BgRate> rates = DbTableCache.instance().GetBgRateList(yosanHead);

            SetColumns(CreateColumns());

            Dictionary<Yojitsu.util.CellItem.CellItemKey, List<CellItem>> itemsDic = BuildCellItemDictionary(yosanHead, yosanItems_前今年度, yosanItems_今年度_月別, jisekis_前年度, jisekis_今年度_月別, rates);
            CreateRows(null, HimokuTreeReader.GetHimokuTree(), itemsDic);
            //NotifyParents(itemsDic);

            treeListView.Columns[0].Fixed = ColumnFixedType.Left;
            treeListView.Columns[1].Fixed = ColumnFixedType.Left;
            treeListView.Columns[2].Fixed = ColumnFixedType.Left;
            treeListView.Columns[3].Fixed = ColumnFixedType.Left;
            treeListView.Columns[4].Fixed = ColumnFixedType.Left;

            Expand();
            treeListView.ResumeUpdate();
        }


        private List<TreeListViewDelegate.Column> CreateColumns()
        {
            List<TreeListViewDelegate.Column> columns = new List<TreeListViewDelegate.Column>();

            // 費目
            TreeListViewDelegate.Column h = new TreeListViewDelegate.Column();
            h.headerContent = "<div><p>費目</p><p>稼働期間</p><p>検査種別 / 不稼働月 / 前 / 入渠 / 後</p></div>";
            h.width = 250;
            h.fixedWidth = true;
            columns.Add(h);

            // 担当
            h = new TreeListViewDelegate.Column();
            h.headerContent = "担当";
            h.width = 50;
            h.fixedWidth = true;
            columns.Add(h);

            string[] kinds = { "予算", "実績" };
            Color[] colors = { COLOR_予算, COLOR_実績 };

            for (int i = 0; i < kinds.Length; i++)
            {
                h = new TreeListViewDelegate.Column();

                if (i == 0)
                {
                    h.headerContent = "<div><p>" + (yosanHead.Year - 1) + " 年度" + kinds[i] + "</p>" + Create船稼働String(lastKadouVessel) + "</div>";
                }
                else
                {
                    h.headerContent = "<div><p>" + (yosanHead.Year - 1) + " 年度" + kinds[i] + "</p><p>　</p><p>　</p></div>";
                }

                h.width = 100;
                h.fixedWidth = true;
                h.alignment = HorizontalAlignment.Right;
                h.headerColor = colors[i];
                columns.Add(h);
            }

            h = new TreeListViewDelegate.Column();
            h.headerContent = "<div><p>" + yosanHead.Year + " 年度予算</p>" + Create船稼働String(kadouVessel) + "</div>";
            h.width = 100;
            h.fixedWidth = true;
            h.alignment = HorizontalAlignment.Right;
            h.headerColor = COLOR_予算;
            columns.Add(h);

            for (int i = 0; i < NBaseData.BLC.予実.GetMonthRange(yosanHead.YosanSbtID); i++)
            {
                for (int k = 0; k < Constants.MONTH.Length; k++)
                {
                    // 予算
                    h = new TreeListViewDelegate.Column();
                    h.headerContent = "<div><p>" + (yosanHead.Year + i) + " 年度</p><p>" + Constants.MONTH[k] + "予算</p><p>　</p></div>";
                    h.width = 100;
                    h.fixedWidth = true;
                    h.alignment = HorizontalAlignment.Right;
                    h.headerColor = COLOR_予算;
                    h.tag = Column.ColumnKind.予算;
                    columns.Add(h);
                    
                    // 実績
                    h = new TreeListViewDelegate.Column();
                    h.headerContent = "<div><p>" + (yosanHead.Year + i) + " 年度</p><p>" + Constants.MONTH[k] + "実績</p><p>　</p></div>";
                    h.width = 100;
                    h.fixedWidth = true;
                    h.alignment = HorizontalAlignment.Right;
                    h.headerColor = COLOR_実績;
                    h.tag = Column.ColumnKind.実績;
                    columns.Add(h);
                    
                    // 差異
                    h = new TreeListViewDelegate.Column();
                    h.headerContent = "<div><p>" + (yosanHead.Year + i) + " 年度</p><p>" + Constants.MONTH[k] + "差異</p><p>　</p></div>";
                    h.width = 100;
                    h.fixedWidth = true;
                    h.alignment = HorizontalAlignment.Right;
                    h.headerColor = COLOR_差異;
                    h.tag = Column.ColumnKind.差異;
                    columns.Add(h);
                }
            }

            return columns;
        }


        internal void ShowJisseki(bool visible)
        {
            treeListView.SuspendUpdate();
            
            foreach (TreeListViewColumn col in treeListView.Columns)
            {
                if ((Column.ColumnKind)col.Tag == Column.ColumnKind.実績 || (Column.ColumnKind)col.Tag == Column.ColumnKind.差異)
                {
                    col.Visible = visible;
                }
            }

            treeListView.ResumeUpdate();
        }


        private static Dictionary<Yojitsu.util.CellItem.CellItemKey, List<CellItem>>
            BuildCellItemDictionary(BgYosanHead yosanHead, List<BgYosanItem> yosanItems_前今年度, List<BgYosanItem> yosanItems_今年度_月別,
                                        List<BgJiseki> jisekis_前年度, List<BgJiseki> jisekis_今年度_月別, List<BgRate> rates)
        {
            var itemsDic = new Dictionary<Yojitsu.util.CellItem.CellItemKey, List<CellItem>>();
            var yosanItem_前今年度_Dic = YosanItemBuilder.Build_年(yosanItems_前今年度);
            var jiseki_前年度_Dic = JisekiBuilder.Build_年(jisekis_前年度);
            var jiseki_今年度_月別_Dic = JisekiBuilder.Build_月(jisekis_今年度_月別);
            var rateDic = RateBuilder.Build_レート(rates);

            foreach (BgYosanItem item in yosanItems_今年度_月別)
            {
                CreateItems(itemsDic, yosanHead, item, yosanItem_前今年度_Dic, jiseki_前年度_Dic, jiseki_今年度_月別_Dic, rateDic, CurrencyFactory.金額種別.円);
                CreateItems(itemsDic, yosanHead, item, yosanItem_前今年度_Dic, jiseki_前年度_Dic, jiseki_今年度_月別_Dic, rateDic, CurrencyFactory.金額種別.ドル);
                CreateItems(itemsDic, yosanHead, item, yosanItem_前今年度_Dic, jiseki_前年度_Dic, jiseki_今年度_月別_Dic, rateDic, CurrencyFactory.金額種別.合計);
            }

            return itemsDic;
        }


        private static void CreateItems(Dictionary<Yojitsu.util.CellItem.CellItemKey, List<CellItem>> itemsDic,
                                          BgYosanHead yosanHead,
                                          BgYosanItem item,
                                          Dictionary<int, Dictionary<string, BgYosanItem>> yosanItem_前今年度_Dic,
                                          Dictionary<int, BgJiseki> jiseki_前年度_Dic,
                                          Dictionary<int, Dictionary<string, BgJiseki>> jiseki_今年度_月別_Dic,
                                          Dictionary<int, BgRate> rateDic,
                                          CurrencyFactory.金額種別 amountKind)
        {
            Yojitsu.util.CellItem.CellItemKey key = new Yojitsu.util.CellItem.CellItemKey(item.MsHimokuID, amountKind);
            CurrencyFactory currency = CurrencyFactory.Create(amountKind);
            
            if (!itemsDic.ContainsKey(key))
            {
                var items = new List<CellItem>();
                itemsDic.Add(key, items);
            }

            if (itemsDic[key].Count == 0)
            {
                CellItem cellItem;

                // 前年度予算
                if (yosanItem_前今年度_Dic.ContainsKey(item.MsHimokuID) &&
                        yosanItem_前今年度_Dic[item.MsHimokuID].ContainsKey((yosanHead.Year - 1).ToString()))
                {
                    BgYosanItem yosanItem_前年度 = yosanItem_前今年度_Dic[item.MsHimokuID][(yosanHead.Year - 1).ToString()];
                    cellItem = new CellItem(yosanItem_前年度, currency);
                }
                else
                {
                    cellItem = new CellItem(currency);
                }

                cellItem.ReadOnly = true;
                itemsDic[key].Add(cellItem);

                // 前年度実績
                if (jiseki_前年度_Dic.ContainsKey(item.MsHimokuID))
                {
                    BgJiseki jiseki = jiseki_前年度_Dic[item.MsHimokuID];
                    cellItem = new CellItem(jiseki, currency);
                }
                else
                {
                    cellItem = new CellItem(currency);
                }

                cellItem.ReadOnly = true;
                itemsDic[key].Add(cellItem);

                // 今年度予算
                BgYosanItem yosanItem_今年度 = yosanItem_前今年度_Dic[item.MsHimokuID][yosanHead.Year.ToString()];
                cellItem = new CellItem(yosanItem_今年度, currency, RateBuilder.DetectRate(rateDic, yosanItem_今年度.Nengetsu));
                cellItem.ReadOnly = true;
                itemsDic[key].Add(cellItem);
            }

            // 月予算
            var cellItem月予算 = new CellItem(item, currency, RateBuilder.DetectRate(rateDic, item.Nengetsu));
            var cellItem年予算 = itemsDic[key][2];
            cellItem年予算.HChildren.Add(cellItem月予算);
            cellItem月予算.Parents.Add(cellItem年予算);

            if (見直し予算の初年度上期のとき(yosanHead, item))
            {
                cellItem月予算.ReadOnly = true;
            }

            itemsDic[key].Add(cellItem月予算);

            // 月実績
            CellItem cellItem月実績;

            if (jiseki_今年度_月別_Dic.ContainsKey(item.MsHimokuID) && jiseki_今年度_月別_Dic[item.MsHimokuID].ContainsKey(item.Nengetsu))
            {
                BgJiseki jiseki = jiseki_今年度_月別_Dic[item.MsHimokuID][item.Nengetsu];
                cellItem月実績 = new CellItem(jiseki, currency);
            }
            else
            {
                EmptyYojitsu jiseki = new EmptyYojitsu();
                cellItem月実績 = new CellItem(jiseki, currency);
            }

            cellItem月実績.ReadOnly = true;
            itemsDic[key].Add(cellItem月実績);

            // 月差異
            Yojitsu.util.CellItem.SaiCellItem saiCellItem = new Yojitsu.util.CellItem.SaiCellItem(cellItem月予算, cellItem月実績, currency);
            cellItem月予算.saiCellItem = saiCellItem;
            saiCellItem.ReadOnly = true;
            itemsDic[key].Add(saiCellItem);
        }

        
        private static bool 見直し予算の初年度上期のとき(BgYosanHead yosanHead, BgYosanItem item)
        {
            return (yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.見直し) && Constants.IsKamiki(item.Nengetsu);
        }

        
        internal void Furiwake()
        {
            int index今年度予算 = 4;
            
            foreach (TreeListViewNode node in treeListView.SelectedNodes)
            {
                TreeListViewSubItem subItem = node.SubItems[index今年度予算];

                CurrencyFactory currency = CurrencyFactory.Create(subItem);

                CellItem cellItem年 = hasYosanItemCellItems[subItem];
                decimal yearAmount = currency.Amount(cellItem年);

                decimal monthAmount_04_02 = Math.Round(yearAmount / 12, 0, MidpointRounding.AwayFromZero);
                decimal monthAmount_03 = yearAmount - (monthAmount_04_02 * 11);

                for (int i = 0; i < cellItem年.HChildren.Count; i++)
                {
                    CellItem ci = cellItem年.HChildren[i];
                    TreeListViewSubItem si = ci.SubItem;

                    if (si.Control != null)
                    {
                        // 4月 - 2月
                        if (i < cellItem年.HChildren.Count - 1)
                        {
                            ((TextBox)si.Control).Text = monthAmount_04_02.ToString();
                            si.Text = currency.金額出力(monthAmount_04_02);
                            hasYosanItemCellItems[si].SetNewAmount(monthAmount_04_02);
                        }
                        // 3月
                        else
                        {
                            ((TextBox)si.Control).Text = monthAmount_03.ToString();
                            si.Text = currency.金額出力(monthAmount_03);
                            hasYosanItemCellItems[si].SetNewAmount(monthAmount_03);
                        }

                        hasYosanItemCellItems[si].NotifyParents();
                    }
                }
            }
        }


        internal void Set運航費(BgUnkouhi unkouhi)
        {
            NBaseData.DS.BlobUnkouhiList unkouhiDataList = NBaseData.DS.BlobUnkouhiList.ToObject(unkouhi.ObjectData);

            foreach (CellItem ci in hasYosanItemCellItems.Values)
            {
                int msHimokuId = ci.GetMsHimokuId();
                string nengetsu = ci.GetNengetsu().Trim();

                if (nengetsu.Length == 6)
                {
                    int monthIndex = NBaseData.DS.Constants.GetPaddingMonthIndex(nengetsu.Substring(4));
                    decimal kadouRatio = Calc_月稼働率(nengetsu);
                    
                    NBaseData.DS.BlobUnkouhi unkouhiData = unkouhiDataList.List[monthIndex];

                    if (msHimokuId == Constants.MS_HIMOKU_ID_貨物運賃)
                    {
                        _SetUnkouhi(ci, unkouhiData.円データ.貨物運賃_計(), unkouhiData.ドルデータ.貨物運賃_計(), kadouRatio);
                    }
                    else if (msHimokuId == Constants.MS_HIMOKU_ID_燃料費)
                    {
                        _SetUnkouhi(ci, unkouhiData.円データ.燃料費_計(), unkouhiData.ドルデータ.燃料費_計(), kadouRatio);
                    }
                    else if (msHimokuId == Constants.MS_HIMOKU_ID_港費)
                    {
                        _SetUnkouhi(ci, unkouhiData.円データ.港費_計(), unkouhiData.ドルデータ.港費_計(), kadouRatio);
                    }
                    else if (msHimokuId == Constants.MS_HIMOKU_ID_貨物費)
                    {
                        _SetUnkouhi(ci, unkouhiData.円データ.貨物費_計(), unkouhiData.ドルデータ.貨物費_計(), kadouRatio);
                    }
                    else if (msHimokuId == Constants.MS_HIMOKU_ID_その他運航費)
                    {
                        _SetUnkouhi(ci, unkouhiData.円データ.その他運航費_計(), unkouhiData.ドルデータ.その他運航費_計(), kadouRatio);
                    }
                }
            }
        }


        private decimal Calc_月稼働率(string nengetsu)
        {
            if (kadouVessel.NyukyoMonth == 0)
            {
                return 1;
            }
            else
            {
                DateTime date = DateTime.Parse(nengetsu.Substring(0, 4) + "/" + nengetsu.Substring(4));

                if (date.Month != kadouVessel.NyukyoMonth)
                {
                    return 1;
                }
                else
                {
                    int dayOfMonth = (date.AddMonths(1) - date).Days;

                    // 2014.05.21 : やはり、元々の計算で正しいとの指摘に対応
                    //// 2014.04.24 : 「入渠日数のみで計算する」指摘に対応
                    ////return (decimal)(dayOfMonth - (kadouVessel.Fukadoubi1 + kadouVessel.Fukadoubi2 + kadouVessel.Fukadoubi3)) / (decimal)dayOfMonth;
                    //return (decimal)(dayOfMonth - kadouVessel.Fukadoubi2) / (decimal)dayOfMonth;
                    return (decimal)(dayOfMonth - (kadouVessel.Fukadoubi1 + kadouVessel.Fukadoubi2 + kadouVessel.Fukadoubi3)) / (decimal)dayOfMonth;
                }
            }
        }


        private static void _SetUnkouhi(CellItem ci, decimal amount, decimal dollarAmount, decimal kadouRatio)
        {
            if (ci.Currency.Type() == CurrencyFactory.金額種別.円)
            {
                decimal am = (amount * kadouRatio) / 1000;

                ci.SetNewAmount(am);
                ci.SubItem.Text = NBaseCommon.Common.金額出力(am);
                ci.NotifyParents();
            }
            else if (ci.Currency.Type() == CurrencyFactory.金額種別.ドル)
            {
                decimal am = dollarAmount * kadouRatio;

                ci.SetNewAmount(am);
                ci.SubItem.Text = NBaseCommon.Common.金額出力(am);
                ci.NotifyParents();
            }
        }


        internal void Set特別修繕引当金(string year, string month, decimal totalAmount)
        {
            string nengetsu = year + month;

            foreach (CellItem ci in hasYosanItemCellItems.Values)
            {
                int msHimokuId = ci.GetMsHimokuId();
                string nengetsu2 = ci.GetNengetsu().Trim();

                if (msHimokuId == Constants.MS_HIMOKU_ID_特別修繕引当金 && 
                      nengetsu2.Length == 6 &&
                      int.Parse(nengetsu2) < int.Parse(nengetsu))
                {
                    if (ci.Currency.Type() == CurrencyFactory.金額種別.円)
                    {
                        decimal am = totalAmount / 60;

                        ci.SetNewAmount(am);
                        ci.SubItem.Text = NBaseCommon.Common.金額出力(am);
                        ci.NotifyParents();
                    }
                }
                else if (msHimokuId == Constants.MS_HIMOKU_ID_特別修繕引当金取崩 &&
                      nengetsu2.Length == 6 &&
                      int.Parse(nengetsu2) == int.Parse(nengetsu))
                {
                    if (ci.Currency.Type() == CurrencyFactory.金額種別.円)
                    {
                        decimal am = - totalAmount;

                        ci.SetNewAmount(am);
                        ci.SubItem.Text = NBaseCommon.Common.金額出力(am);
                        ci.NotifyParents();
                    }
                }
            }
        }


        internal void Set換算レート(List<BgRate> rates)
        {
            var rateDic = RateBuilder.Build_レート(rates);
            
            foreach (CellItem ci in hasYosanItemCellItems.Values)
            {
                BgRate rate;
                decimal r = RateBuilder.DetectRate(rateDic, ci.GetNengetsu(), out rate);

                if (rate != null && ci.GetDollarAmount() > 0)
                {
                    ci.SetNewRate(r);
                }
            }
        }
    }
}
