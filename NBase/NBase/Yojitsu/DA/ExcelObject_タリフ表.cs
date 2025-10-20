using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace Yojitsu.DA
{
    public class ExcelObject_タリフ表
    {
        public Dictionary<string, HimokuCol> HimokuDic { get; private set; }
        public Dictionary<string, decimal> HimokuDic2 { get; private set; }

        public static readonly string 保険料 = "保険料";
        public static readonly string 船員給料_手当 = "船員給料・手当";
        public static readonly string 賞与引当金 = "賞与引当金";
        public static readonly string 予備船員費A = "予備船員費(A)";
        public static readonly string 予備船員費B = "予備船員費(B)";

        // 2013.02 : Add-->
        public static readonly string 航海手当 = "航海手当";

        private ExcelObject_タリフ表()
        {
            HimokuDic = new Dictionary<string, HimokuCol>();
            HimokuDic2 = new Dictionary<string, decimal>();
        }


        internal static ExcelObject_タリフ表 Create(string fileName, ExcelCreator.Xlsx.XlsxCreator xls)
        {
            ExcelObject_タリフ表 obj = new ExcelObject_タリフ表();

            int sheetNo = xls.SheetNo2("タリフ表");
            xls.SheetNo = sheetNo;

            int emptyRowNo = 0;

            for (int i = 1; true; i++)
            {
                string himokuName = xls.Pos(i, 0).Value.ToString().Trim();

                if (himokuName.Length == 0)
                {
                    break;
                }

                HimokuCol h = new HimokuCol(himokuName);
                obj.HimokuDic[himokuName] = h;

                for (int k = 1; true; k++)
                {
                    string shokumei = xls.Pos(0, k).Value.ToString();

                    if (shokumei.Trim().Length == 0)
                    {
                        emptyRowNo = k;
                        break;
                    }

                    string amountStr = xls.Pos(i, k).Value.ToString();
                    decimal amount;
                    decimal.TryParse(amountStr, out amount);

                    h.AddAmount(shokumei, amount);
                }
            }



            // 2013.08: 「船員給料・手当」から「航海手当」を引くロジック
            HimokuCol himoku_船員給料手当 = obj.HimokuDic[船員給料_手当];
            HimokuCol himoku_航海手当 = obj.HimokuDic[航海手当];
            foreach (string shokumei in himoku_航海手当.AmountDic.Keys)
            {
                decimal amount = himoku_航海手当.AmountDic[shokumei];

                try
                {
                    himoku_船員給料手当.AmountDic[shokumei] -= amount;
                }
                catch (Exception e)
                {
                }
            }




            for (int k = emptyRowNo + 1; true; k++)
            {
                string himokuName = xls.Pos(0, k).Value.ToString();

                for (int m = k; true; m++, k++)
                {
                    for (int n = 1; true; n++)
                    {
                        string f = xls.Pos(n, m).Value.ToString();

                        if (f.Trim().Length == 0)
                        {
                            goto OUT;
                        }

                        decimal am;
                        if (decimal.TryParse(f, out am))
                        {
                            if (!obj.HimokuDic2.ContainsKey(himokuName))
                            {
                                obj.HimokuDic2[himokuName] = 0;
                            }

                            obj.HimokuDic2[himokuName] += am;
                            break;
                        }
                    }

                    if (xls.Pos(0, m + 1).Value.ToString().Trim().Length > 0)
                    {
                        break;
                    }
                }
            }
        OUT:
            return obj;
        }


        public class HimokuCol
        {
            public string Name { get; set; }
            public Dictionary<string, decimal> AmountDic { get; private set; }


            public HimokuCol(string name)
            {
                this.Name = name;
                AmountDic = new Dictionary<string, decimal>();
            }


            public void AddAmount(string shokumei, decimal amount)
            {
                AmountDic[shokumei] = amount;
            }
        }
    }
}
