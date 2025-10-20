using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DS;

namespace Yojitsu.util
{
    public abstract class CurrencyFactory
    {
        public abstract decimal Amount(CellItem yojitsu);
        public abstract decimal PreAmount(CellItem yojitsu);
        public abstract string 金額出力(CellItem yojitsu);
        public abstract string 金額出力(decimal amount);
        public abstract void SetAmount(CellItem yojitsu, decimal amount);
        public abstract decimal MinValue();
        public abstract 金額種別 Type();

        // AMOUNT, DOLLER_AMOUNT, YEN_AMOUNT
        public enum 金額種別 { 円, ドル, 合計 };
        
        
        private class 円 : CurrencyFactory
        {
            private static readonly 円 instance = new 円();

            private 円() { }
            
            
            public static 円 Instance()
            {
                return instance;
            }
            
            
            #region CurrencyFactory メンバ

            public override decimal Amount(CellItem yojitsu)
            {
                return yojitsu.GetAmount();
            }

            public override decimal PreAmount(CellItem yojitsu)
            {
                return yojitsu.GetPreAmount();
            }

            public override string 金額出力(CellItem yojitsu)
            {
                return 金額出力(yojitsu.GetAmount());
            }

            public override string 金額出力(decimal amount)
            {
                return NBaseCommon.Common.金額出力(amount);
            }

            public override void SetAmount(CellItem yojitsu, decimal amount)
            {
                yojitsu.SetAmount(amount);
            }

            public override decimal MinValue()
            {
                return decimal.MinValue / 1000;
            }

            public override 金額種別 Type()
            {
                return 金額種別.円;
            }
       
            #endregion
        }


        private class ドル : CurrencyFactory
        {
            private static readonly ドル instance = new ドル();

            private ドル() { }
            
            
            public static ドル Instance()
            {
                return instance;
            }
            
            
            #region CurrencyFactory メンバ

            public override decimal Amount(CellItem yojitsu)
            {
                return yojitsu.GetDollarAmount();
            }

            public override decimal PreAmount(CellItem yojitsu)
            {
                return yojitsu.GetPreDollarAmount();
            }

            public override string 金額出力(CellItem yojitsu)
            {
                return 金額出力(yojitsu.GetDollarAmount());
            }

            public override string 金額出力(decimal amount)
            {
                return NBaseCommon.Common.ドル金額出力(amount);
            }

            public override void SetAmount(CellItem yojitsu, decimal amount)
            {
                yojitsu.SetDollarAmount(amount);
            }

            public override decimal MinValue()
            {
                return decimal.MinValue;
            }

            public override 金額種別 Type()
            {
                return 金額種別.ドル;
            }

            #endregion
        }


        private class 合計 : CurrencyFactory
        {
            private static readonly 合計 instance = new 合計();

            private 合計() { }


            public static 合計 Instance()
            {
                return instance;
            }


            #region CurrencyFactory メンバ

            public override decimal Amount(CellItem yojitsu)
            {
                return yojitsu.GetYenAmount();
            }

            public override decimal PreAmount(CellItem yojitsu)
            {
                return yojitsu.GetPreYenAmount();
            }

            public override string 金額出力(CellItem yojitsu)
            {
                return 金額出力(yojitsu.GetYenAmount());
            }

            public override string 金額出力(decimal amount)
            {
                return NBaseCommon.Common.金額出力(amount);
            }

            public override void SetAmount(CellItem yojitsu, decimal amount)
            {
                yojitsu.SetYenAmount(amount);
            }

            public override decimal MinValue()
            {
                return decimal.MinValue / 1000;
            }

            public override 金額種別 Type()
            {
                return 金額種別.合計;
            }

            #endregion
        }


        public static CurrencyFactory Create(金額種別 kind)
        {
            if (kind == 金額種別.ドル)
            {
                return ドル.Instance();
            }
            else if(kind == 金額種別.円)
            {
                return 円.Instance();
            }
            else if (kind == 金額種別.合計)
            {
                return 合計.Instance();
            }

            return null;
        }


        public static CurrencyFactory Create(TreeListViewSubItem subItem)
        {
            if (subItem.Text.StartsWith("$"))
            {
                return ドル.Instance();
            }
            else
            {
                return 円.Instance();
            }
        }
    }
}
