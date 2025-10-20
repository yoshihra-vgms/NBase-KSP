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
    class NenjiTreeListViewDelegate : YojitsuTreeListViewDelegate
    {
        private List<BgKadouVessel> kadouVessels;
        // 前年度最終.
        private BgKadouVessel lastKadouVessel;


        internal NenjiTreeListViewDelegate(TreeListView treeListView)
            : base(treeListView)
        {
        }


        internal void CreateYojitsuTable(BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels, BgKadouVessel lastKadouVessel, List<BgYosanItem> yosanItems, List<BgJiseki> jisekis)
        {
            this.yosanHead = yosanHead;
            this.kadouVessels = kadouVessels;
            this.lastKadouVessel = lastKadouVessel;

            Editable = !yosanHead.IsFixed();

            treeListView.SuspendUpdate();

            treeListView.Columns.Clear();
            treeListView.Nodes.Clear();

            hasYosanItemCellItems.Clear();

            List<BgRate> rates = DbTableCache.instance().GetBgRateList(yosanHead);

            SetColumns(CreateColumns());

            Dictionary<CellItem.CellItemKey, List<CellItem>> itemsDic = BuildCellItemDictionary(yosanHead, yosanItems, jisekis, rates);
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

            string[] kinds = { "予算", "実績", "差異" };
            Color[] colors = { COLOR_予算, COLOR_実績, COLOR_差異 };

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

            for (int i = 0; i < NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID); i++)
            {
                h = new TreeListViewDelegate.Column();

                string 船稼働String = null;
                if (kadouVessels == null|| kadouVessels.Count <= i)
                {
                    船稼働String = Create船稼働String(null);
                }
                else
                {
                    船稼働String = Create船稼働String(kadouVessels[i]);
                }

                h.headerContent = "<div><p>" + (yosanHead.Year + i) + " 年度予算</p>" + 船稼働String + "</div>";
                h.width = 100;
                h.fixedWidth = true;
                h.alignment = HorizontalAlignment.Right;
                h.headerColor = COLOR_予算;
                columns.Add(h);
            }

            return columns;
        }


        private static Dictionary<CellItem.CellItemKey, List<CellItem>>
            BuildCellItemDictionary(BgYosanHead yosanHead, List<BgYosanItem> yosanItems, List<BgJiseki> jisekis, List<BgRate> rates)
        {
            var itemsDic = new Dictionary<CellItem.CellItemKey, List<CellItem>>();
            var jisekiDic = JisekiBuilder.Build_年(jisekis);
            var rateDic = RateBuilder.Build_レート(rates);

            foreach (BgYosanItem item in yosanItems)
            {
                CreateItems(itemsDic, yosanHead, jisekiDic, item, rateDic, CurrencyFactory.金額種別.円);
                CreateItems(itemsDic, yosanHead, jisekiDic, item, rateDic, CurrencyFactory.金額種別.ドル);
                CreateItems(itemsDic, yosanHead, jisekiDic, item, rateDic, CurrencyFactory.金額種別.合計);
            }

            return itemsDic;
        }


        private static void CreateItems(Dictionary<CellItem.CellItemKey, List<CellItem>> itemsDic,
                                          BgYosanHead yosanHead,
                                          Dictionary<int, BgJiseki> jisekiDic,
                                          BgYosanItem item,
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

            CellItem cellItem年予算 = new CellItem(item, currency, RateBuilder.DetectRate(rateDic, item.Nengetsu));

            if (見直し予算の初年度のとき(yosanHead, item))
            {
                cellItem年予算.ReadOnly = true;
            }

            itemsDic[key].Add(cellItem年予算);

            // 前年度予算、実績、差異
            if (itemsDic[key].Count == 1)
            {
                // 予算
                cellItem年予算.ReadOnly = true;

                // 実績
                CellItem jisekiCellItem;

                if (jisekiDic.ContainsKey(item.MsHimokuID))
                {
                    BgJiseki jiseki = jisekiDic[item.MsHimokuID];
                    jisekiCellItem = new CellItem(jiseki, currency);
                }
                else
                {
                    jisekiCellItem = new CellItem(currency);
                }

                jisekiCellItem.ReadOnly = true;
                itemsDic[key].Add(jisekiCellItem);

                // 差異
                CellItem saiCellItem = new Yojitsu.util.CellItem.SaiCellItem(cellItem年予算, jisekiCellItem, currency);
                saiCellItem.ReadOnly = true;
                itemsDic[key].Add(saiCellItem);
            }
        }


        private static bool 見直し予算の初年度のとき(BgYosanHead yosanHead, BgYosanItem item)
        {
            return (yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.見直し) && (Int32.Parse(item.Nengetsu.Trim()) == yosanHead.Year);
        }


        internal void Set運航費(BgUnkouhi unkouhi, bool doCopy)
        {
            NBaseData.DS.BlobUnkouhiList unkouhiDataList = NBaseData.DS.BlobUnkouhiList.ToObject(unkouhi.ObjectData);

            foreach (CellItem ci in hasYosanItemCellItems.Values)
            {
                if (IsCopy(unkouhi, ci.GetNengetsu(), doCopy))
                {
                    BgKadouVessel kv;
                    decimal kadouRatio = Calc_月稼働率(ci.GetNengetsu(), out kv);
                    int msHimokuId = ci.GetMsHimokuId();

                    if (msHimokuId == Constants.MS_HIMOKU_ID_貨物運賃 ||
                        msHimokuId == Constants.MS_HIMOKU_ID_燃料費 ||
                        msHimokuId == Constants.MS_HIMOKU_ID_港費 ||
                        msHimokuId == Constants.MS_HIMOKU_ID_貨物費 ||
                        msHimokuId == Constants.MS_HIMOKU_ID_その他運航費)
                    {
                        _SetUnkouhi(ci, unkouhiDataList, kv, kadouRatio);
                    }
                }
            }
        }


        private static bool IsCopy(BgUnkouhi unkouhi, string nengetsu, bool doCopy)
        {
            if (!doCopy)
            {
                return int.Parse(nengetsu.Trim()) == unkouhi.Year;
            }
            else
            {
                return int.Parse(nengetsu.Trim()) >= unkouhi.Year;
            }
        }


        private decimal Calc_月稼働率(string nengetsu, out BgKadouVessel kadouVessel)
        {
            kadouVessel = null;

            foreach (BgKadouVessel kv in kadouVessels)
            {
                if (kv.Year == int.Parse(nengetsu.Trim()))
                {
                    kadouVessel = kv;
                    break;
                }
            }

            if (kadouVessel.NyukyoMonth == 0)
            {
                return 1;
            }
            else
            {
                DateTime kvDate = new DateTime(kadouVessel.Year, kadouVessel.NyukyoMonth, 1);

                int dayOfMonth = (kvDate.AddMonths(1) - kvDate).Days;

                // 2014.05.21 : やはり、元々の計算で正しいとの指摘に対応
                //// 2014.04.24 : 「入渠日数のみで計算する」指摘に対応
                ////return (decimal)(dayOfMonth - (kadouVessel.Fukadoubi1 + kadouVessel.Fukadoubi2 + kadouVessel.Fukadoubi3)) / (decimal)dayOfMonth;
                //return (decimal)(dayOfMonth - kadouVessel.Fukadoubi2) / (decimal)dayOfMonth;
                return (decimal)(dayOfMonth - (kadouVessel.Fukadoubi1 + kadouVessel.Fukadoubi2 + kadouVessel.Fukadoubi3)) / (decimal)dayOfMonth;
            }
        }



        private void _SetUnkouhi(CellItem ci, NBaseData.DS.BlobUnkouhiList unkouhiDataList, BgKadouVessel kadouVessel, decimal kadouRatio)
        {
            if (ci.Currency.Type() == CurrencyFactory.金額種別.円 || ci.Currency.Type() == CurrencyFactory.金額種別.ドル)
            {
                decimal am = 0;
                for (int i = 0; i < unkouhiDataList.List.Count; i++)
                {
                    NBaseData.DS.BlobUnkouhi u = unkouhiDataList.List[i];

                    int m;
                    if (i < 9)
                    {
                        m = i + 4;
                    }
                    else
                    {
                        m = i - 8;
                    }

                    int msHimokuId = ci.GetMsHimokuId();
                    decimal amount = 0;

                    NBaseData.DS.BlobUnkouhi.Store store = null;

                    if (ci.Currency.Type() == CurrencyFactory.金額種別.円)
                    {
                        store = u.円データ;
                    }
                    else if (ci.Currency.Type() == CurrencyFactory.金額種別.ドル)
                    {
                        store = u.ドルデータ;
                    }

                    if (msHimokuId == Constants.MS_HIMOKU_ID_貨物運賃)
                    {
                        amount = store.貨物運賃_計();
                    }
                    else if (msHimokuId == Constants.MS_HIMOKU_ID_燃料費)
                    {
                        amount = store.燃料費_計();
                    }
                    else if (msHimokuId == Constants.MS_HIMOKU_ID_港費)
                    {
                        amount = store.港費_計();
                    }
                    else if (msHimokuId == Constants.MS_HIMOKU_ID_貨物費)
                    {
                        amount = store.貨物費_計();
                    }
                    else if (msHimokuId == Constants.MS_HIMOKU_ID_その他運航費)
                    {
                        amount = store.その他運航費_計();
                    }

                    if (m == kadouVessel.NyukyoMonth)
                    {
                        am += amount * kadouRatio;
                    }
                    else
                    {
                        am += amount;
                    }
                }

                am /= 1000;

                ci.SetNewAmount(am);
                ci.SubItem.Text = NBaseCommon.Common.金額出力(am);
                ci.NotifyParents();
            }
        }


        internal void Set修繕費(Dictionary<int, Dictionary<string, decimal>> amountDic)
        {
            foreach (CellItem ci in hasYosanItemCellItems.Values)
            {
                if (ci.Currency.Type() == CurrencyFactory.金額種別.円 || ci.Currency.Type() == CurrencyFactory.金額種別.合計)
                {
                    int msHimokuId = ci.GetMsHimokuId();
                    string nengetsu = ci.GetNengetsu().Trim();

                    if (amountDic.ContainsKey(msHimokuId) && amountDic[msHimokuId].ContainsKey(nengetsu))
                    {
                        decimal am = amountDic[msHimokuId][nengetsu] / 1000;

                        ci.SetNewAmount(am);
                        ci.SubItem.Text = NBaseCommon.Common.金額出力(am);
                        ci.NotifyParents();
                    }
                }
            }
        }


        internal void Set特別修繕引当金(string year, string month, decimal totalAmount)
        {
            foreach (CellItem ci in hasYosanItemCellItems.Values)
            {
                int msHimokuId = ci.GetMsHimokuId();
                string nengetsu = ci.GetNengetsu().Trim();

                if (msHimokuId == Constants.MS_HIMOKU_ID_特別修繕引当金)
                {
                    if (ci.Currency.Type() == CurrencyFactory.金額種別.円)
                    {
                        // 定期検査の前年までの4カ年.
                        if (int.Parse(year) - 5 < int.Parse(nengetsu) && int.Parse(nengetsu) < int.Parse(year))
                        {
                            decimal am = totalAmount / 5;

                            ci.SetNewAmount(am);
                            ci.SubItem.Text = NBaseCommon.Common.金額出力(am);
                            ci.NotifyParents();
                        }
                        // 定期検査当年.
                        else if (int.Parse(nengetsu) == int.Parse(year))
                        {
                            int monthIndex = NBaseData.DS.Constants.GetPaddingMonthIndex(month);

                            decimal am = totalAmount / 60 * monthIndex;

                            ci.SetNewAmount(am);
                            ci.SubItem.Text = NBaseCommon.Common.金額出力(am);
                            ci.NotifyParents();
                        }
                    }
                }
                else if (msHimokuId == Constants.MS_HIMOKU_ID_特別修繕引当金取崩)
                {
                    if (ci.Currency.Type() == CurrencyFactory.金額種別.円)
                    {
                        // 定期検査当年.
                        if (int.Parse(nengetsu) == int.Parse(year))
                        {
                            decimal am = -totalAmount;

                            ci.SetNewAmount(am);
                            ci.SubItem.Text = NBaseCommon.Common.金額出力(am);
                            ci.NotifyParents();
                        }
                    }
                }
            }
        }


        internal void Set販管費(int year, decimal amount)
        {
            foreach (CellItem ci in hasYosanItemCellItems.Values)
            {
                int msHimokuId = ci.GetMsHimokuId();
                string nengetsu = ci.GetNengetsu().Trim();

                //if (msHimokuId == Constants.MS_HIMOKU_ID_人件費)
                if (msHimokuId == Constants.MS_HIMOKU_ID_販管費)
                {
                    if (ci.Currency.Type() == CurrencyFactory.金額種別.円)
                    {
                        if (int.Parse(nengetsu) == year)
                        {
                            ci.SetNewAmount(amount / 1000);
                            ci.SubItem.Text = NBaseCommon.Common.金額出力(amount / 1000);
                            ci.NotifyParents();

                            break;
                        }
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

                if (rate != null)
                {
                    ci.SetNewRate(r);
                }
            }
        }


        public Dictionary<int, decimal> Get売上高()
        {
            List<long> yosanItemIdList = new List<long>();
            Dictionary<int, decimal> uriagedakaList = new Dictionary<int, decimal>(); 

            foreach (CellItem ci in hasYosanItemCellItems.Values)
            {
                long yosanItemId = ci.GetBgYosanItemId();
                int msHimokuId = ci.GetMsHimokuId();
                string nengetsu = ci.GetNengetsu();

                if (yosanItemIdList.Contains(yosanItemId))
                    continue;

                yosanItemIdList.Add(yosanItemId);

                if (msHimokuId == Constants.MS_HIMOKU_ID_売上高 && nengetsu.Trim().Length == 4)
                {
                    int nen = int.Parse(nengetsu);
                    if (uriagedakaList.ContainsKey(nen))
                    {
                        uriagedakaList[nen] += ci.GetYenAmountORG();
                    }
                    else
                    {
                        uriagedakaList.Add(nen, ci.GetYenAmountORG());
                    }
                }
            }

            return uriagedakaList;
        }
    }
}
