using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DS;
using NBaseData.DAC;
using LidorSystems.IntegralUI.Lists;
using System.Drawing;

namespace Yojitsu.util
{
    public class CellItem
    {
        private IYojitsu Yojitsu;
        
        public CurrencyFactory Currency { get; private set; }

        public bool ReadOnly { get; set; }
        public bool Edited { get; private set; }

        private static readonly Color REV_DIFF_COLOR = Color.Plum;

        private TreeListViewSubItem subItem;
        public TreeListViewSubItem SubItem
        {
            get
            {
                return subItem;
            }

            set
            {
                subItem = value;

                if (Yojitsu is BgYosanItem &&
                        Math.Floor(Currency.PreAmount(this)) != Math.Floor(Currency.MinValue()) &&
                        Currency.Amount(this) != Currency.PreAmount(this))
                {
                    subItem.NormalStyle.BackColor = REV_DIFF_COLOR;
                }
            }
        }

        private List<CellItem> parents;
        public List<CellItem> Parents
        {
            get
            {
                if (parents == null)
                {
                    parents = new List<CellItem>();
                }

                return parents;
            }
        }

        private List<CellItem> vChildrenPlus;
        public List<CellItem> VChildrenPlus
        {
            get
            {
                if (vChildrenPlus == null)
                {
                    vChildrenPlus = new List<CellItem>();
                }

                return vChildrenPlus;
            }
        }


        private List<CellItem> vChildrenMinus;
        public List<CellItem> VChildrenMinus
        {
            get
            {
                if (vChildrenMinus == null)
                {
                    vChildrenMinus = new List<CellItem>();
                }

                return vChildrenMinus;
            }
        }


        private List<CellItem> hChildren;
        public List<CellItem> HChildren
        {
            get
            {
                if (hChildren == null)
                {
                    hChildren = new List<CellItem>();
                }

                return hChildren;
            }
        }


        public CellItem CellItem合計 { get; set; }


        public SaiCellItem saiCellItem { get; set; }

        private decimal rate;


        public CellItem(CurrencyFactory Currency)
            : this(new YojitsuTreeListViewDelegate.EmptyYojitsu(), Currency)
        {
        }


        public CellItem(IYojitsu yojitsu, CurrencyFactory Currency)
            : this(yojitsu, Currency, 0)
        {
        }


        public CellItem(IYojitsu yojitsu, CurrencyFactory Currency, decimal rate)
        {
            this.Yojitsu = yojitsu;
            this.Currency = Currency;
            this.rate = rate;
        }


        internal void SetNewAmount(decimal newAmount)
        {
            SetNewAmount(newAmount, true);
        }


        internal void SetNewAmount(decimal newAmount, bool edited)
        {
            SetNewAmount(newAmount, true, false);
        }


        private void SetNewAmount(decimal newAmount, bool edited, bool force)
        {
            if (Yojitsu != null && (Currency.Amount(this) != newAmount || force))
            {
                Currency.SetAmount(this, newAmount);
                Edited = edited;

                if (Yojitsu is BgYosanItem &&
                        Math.Floor(Currency.PreAmount(this)) != Math.Floor(Currency.MinValue()) &&
                        Currency.Amount(this) != Currency.PreAmount(this))
                {
                    SubItem.NormalStyle.BackColor = REV_DIFF_COLOR;
                }

                decimal 円換算 = calc円換算(rate);
                Yojitsu.YenAmount = 円換算;

                if (CellItem合計 != null && CellItem合計.SubItem != null)
                {
                    CellItem合計.SubItem.Text = NBaseCommon.Common.金額出力(円換算 / 1000);

                    if (Yojitsu is BgYosanItem &&
                            Yojitsu.PreYenAmount != decimal.MinValue &&
                            Yojitsu.YenAmount != Yojitsu.PreYenAmount)
                    {
                        CellItem合計.SubItem.NormalStyle.BackColor = REV_DIFF_COLOR;
                    }
                }
            }
        }


        internal void SetNewRate(decimal newRate)
        {
            rate = newRate;
            SetNewAmount(Currency.Amount(this), true, true);
        }


        private decimal calc円換算(decimal rate)
        {
            return Yojitsu.Amount + (Yojitsu.DollerAmount * rate);
        }


        internal void NotifyParents()
        {
            NotifyParents(true);
        }


        internal void NotifyParents(bool edited)
        {
            foreach (CellItem p in Parents)
            {
                p.CalcTotal(edited);
                p.NotifyParents(edited);
            }

            if (saiCellItem != null)
            {
                saiCellItem.UpdateSai();
            }
        }


        private void CalcTotal()
        {
            CalcTotal(true);
        }


        private void CalcTotal(bool edited)
        {
            CalcAmountTotal(edited);
        }


        private void CalcAmountTotal(bool edited)
        {
            decimal total = 0;

            foreach (CellItem c in VChildrenPlus)
            {
                total += Currency.Amount(c);
            }

            foreach (CellItem c in VChildrenMinus)
            {
                total -= Currency.Amount(c);
            }

            if (VChildrenPlus.Count == 0 && vChildrenMinus.Count == 0)
            {
                foreach (CellItem c in HChildren)
                {
                    total += Currency.Amount(c);
                }
            }

            if (SubItem != null)
            {
                SubItem.Text = Currency.金額出力(total);
            }
            
            SetNewAmount(total, edited);
        }


        public string GetNengetsu()
        {
            return Yojitsu.Nengetsu;
        }


        public decimal GetAmount()
        {
            return Yojitsu.Amount / 1000;
        }


        public decimal GetDollarAmount()
        {
            return Yojitsu.DollerAmount;
        }


        public decimal GetYenAmount()
        {
            return Yojitsu.YenAmount / 1000;
        }

        public decimal GetYenAmountORG()
        {
            return Yojitsu.YenAmount;
        }


        public decimal GetPreAmount()
        {
            return Yojitsu.PreAmount / 1000;
        }


        public decimal GetPreDollarAmount()
        {
            return Yojitsu.PreDollerAmount;
        }


        public decimal GetPreYenAmount()
        {
            return Yojitsu.PreYenAmount / 1000;
        }


        internal void SetAmount(decimal amount)
        {
            Yojitsu.Amount = amount * 1000;
        }


        internal void SetDollarAmount(decimal amount)
        {
            Yojitsu.DollerAmount = amount;
        }


        internal void SetYenAmount(decimal amount)
        {
            Yojitsu.YenAmount = amount * 1000;
        }


        public int GetMsHimokuId()
        {
            if (Yojitsu is BgYosanItem)
            {
                return ((BgYosanItem)Yojitsu).MsHimokuID;
            }
            else
            {
                return int.MinValue;
            }
        }

        public long GetBgYosanItemId()
        {
            if (Yojitsu is BgYosanItem)
            {
                return ((BgYosanItem)Yojitsu).YsanItemID;
            }
            else
            {
                return long.MinValue;
            }
        }


        public bool HasBgYosanItem()
        {
            return Yojitsu != null && Yojitsu is BgYosanItem;
        }


        internal void BuildYosanItem(List<BgYosanItem> result)
        {
            if (Yojitsu is BgYosanItem)
            {
                BgYosanItem item = Yojitsu as BgYosanItem;

                item.RenewDate = DateTime.Now;
                item.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

                result.Add(item);
            }
        }
       

        public class SaiCellItem : CellItem
        {
            private CellItem yosanCellItem;
            private CellItem jisekiCellItem;


            public SaiCellItem(CellItem yosanCellItem, CellItem jisekiCellItem, CurrencyFactory Currency) : base(Currency)
            {
                this.yosanCellItem = yosanCellItem;
                this.jisekiCellItem = jisekiCellItem;
                this.Currency = Currency;

                Yojitsu = new YojitsuTreeListViewDelegate.EmptyYojitsu();

                Currency.SetAmount(this, Currency.Amount(jisekiCellItem) - Currency.Amount(yosanCellItem));
            }


            internal void UpdateSai()
            {
                Currency.SetAmount(this, Currency.Amount(jisekiCellItem) - Currency.Amount(yosanCellItem));

                SubItem.Text = Currency.金額出力(this);

                if (CellItem合計 != null)
                {
                    ((SaiCellItem)CellItem合計).UpdateSai();
                }
            }
        }


        public class CellItemKey
        {
            private int msHimokuID;
            private CurrencyFactory.金額種別 amountKind;


            public CellItemKey(int msHimokuID, CurrencyFactory.金額種別 amountKind)
            {
                this.msHimokuID = msHimokuID;
                this.amountKind = amountKind;
            }
            
            
            public override bool Equals(object obj)
            {
                CellItemKey other = obj as CellItemKey;

                return msHimokuID == other.msHimokuID && amountKind == other.amountKind;
            }


            public override int GetHashCode()
            {
                return msHimokuID ^ (int)amountKind;
            }
        }
    }
}
