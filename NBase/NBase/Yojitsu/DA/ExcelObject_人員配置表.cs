using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace Yojitsu.DA
{
    public class ExcelObject_人員配置表
    {
        public Dictionary<string, 人員配置_職種別_Col> 職種別_Dic { get; private set; }
        public Dictionary<string, 人員配置_費目別_Col> 費目別_Dic { get; private set; }

        public static readonly string 予備員 = "予備員";


        private ExcelObject_人員配置表()
        {
            職種別_Dic = new Dictionary<string, 人員配置_職種別_Col>();
            費目別_Dic = new Dictionary<string, 人員配置_費目別_Col>();
        }


        internal static ExcelObject_人員配置表 Create(string fileName, ExcelCreator.Xlsx.XlsxCreator xls)
        {
            ExcelObject_人員配置表 obj = new ExcelObject_人員配置表();

            int sheetNo = xls.SheetNo2("人員配置表");
            xls.SheetNo = sheetNo;

            int emptyRowNo = 0;

            for (int i = 1; true; i++)
            {
                string vesselName = xls.Pos(i, 0).Value.ToString().Trim();

                if (vesselName.Length == 0)
                {
                    break;
                }

                人員配置_職種別_Col v = new 人員配置_職種別_Col();
                obj.職種別_Dic[vesselName] = v;

                for (int k = 1; true; k++)
                {
                    string shokumei = xls.Pos(0, k).Value.ToString();

                    if (shokumei.Trim().Length == 0)
                    {
                        emptyRowNo = k;
                        break;
                    }

                    string countStr = xls.Pos(i, k).Value.ToString();
                    //int count;
                    //int.TryParse(countStr, out count);

                    //v.AddCount(shokumei, count);
                    decimal count;
                    decimal.TryParse(countStr, out count);

                    v.AddCount(shokumei, count);
                }
            }

            for (int i = 1; true; i++)
            {
                string vesselName = xls.Pos(i, 0).Value.ToString().Trim();

                if (vesselName.Length == 0)
                {
                    break;
                }

                if (vesselName == ExcelObject_人員配置表.予備員)
                {
                    continue;
                }

                人員配置_費目別_Col h = new 人員配置_費目別_Col();
                obj.費目別_Dic[vesselName] = h;

                for (int k = emptyRowNo + 1; true; k++)
                {
                    string himokuName = xls.Pos(0, k).Value.ToString();

                    if (himokuName.Length == 0)
                    {
                        if (xls.Pos(i, k).Value.ToString().Length == 0)
                        {
                            break;
                        }

                        continue;
                    }
                    else if (himokuName.IndexOf(' ') == 0 || himokuName.IndexOf('　') == 0)
                    {
                        continue;
                    }

                    string amountStr = xls.Pos(i, k).Value.ToString();
                    decimal amount;
                    decimal.TryParse(amountStr, out amount);

                    h.SetAmount(himokuName, amount);
                }
            }

            return obj;
        }


        public class 人員配置_職種別_Col
        {
            // <Shokumei, Count>
            //private Dictionary<string, int> countDic;
            private Dictionary<string, decimal> countDic;


            public 人員配置_職種別_Col()
            {
                //countDic = new Dictionary<string, int>();
                countDic = new Dictionary<string, decimal>();
            }


            //public void AddCount(string shokumei, int count)
            public void AddCount(string shokumei, decimal count)
            {
                countDic[shokumei] = count;
            }


            //public int GetCount(string shokumei)
            public decimal GetCount(string shokumei)
            {
                if (countDic.ContainsKey(shokumei))
                {
                    return countDic[shokumei];
                }

                //return int.MinValue;
                return decimal.MinValue;
            }


            //internal int GetCount()
            internal decimal GetCount()
            {
                //int total = 0;
                decimal total = 0;

                //foreach (int c in countDic.Values)
                foreach (decimal c in countDic.Values)
                {
                    total += c;
                }

                return total;
            }
        }


        public class 人員配置_費目別_Col
        {
            // <MsHimokuId, Amount>
            public Dictionary<string, decimal> AmountDic { get; private set; }


            public 人員配置_費目別_Col()
            {
                AmountDic = new Dictionary<string, decimal>();
            }


            public void SetAmount(string himokuName, decimal amount)
            {
                AmountDic[himokuName] = amount;
            }
        }
    }
}
