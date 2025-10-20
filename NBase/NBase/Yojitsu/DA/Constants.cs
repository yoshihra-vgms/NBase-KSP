using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseCommon;
using System.Drawing;

namespace Yojitsu.DA
{
    public class Constants
    {
        public static readonly string MS_VESSEL_TYPE_ID_外航 = "5";

        public static readonly int MS_HIMOKU_ID_貨物運賃 = 3;
        public static readonly int MS_HIMOKU_ID_燃料費 = 18;
        public static readonly int MS_HIMOKU_ID_港費 = 19;
        public static readonly int MS_HIMOKU_ID_貨物費 = 17;
        public static readonly int MS_HIMOKU_ID_その他運航費 = 20;

        public static readonly int MS_HIMOKU_ID_営業損益 = 68;
        public static readonly int MS_HIMOKU_ID_売上高 = 1;
        public static readonly int MS_HIMOKU_ID_売上原価 = 15;
        public static readonly int MS_HIMOKU_ID_販管費 = 65;
        public static readonly int MS_HIMOKU_ID_経常損益 = 73;
        public static readonly int MS_HIMOKU_ID_営業外費用 = 70;
        public static readonly int MS_HIMOKU_ID_売上総利益 = 64;
        public static readonly int MS_HIMOKU_ID_税引前当期損益 = 76;
        public static readonly int MS_HIMOKU_ID_法人税等 = 77;
        public static readonly int MS_HIMOKU_ID_当期損益 = 78;
        public static readonly int MS_HIMOKU_ID_特別利益 = 74;
        public static readonly int MS_HIMOKU_ID_特別損失 = 75;

        public static readonly int MS_HIMOKU_ID_人件費 = 66;

        public static readonly int MS_HIMOKU_ID_特別修繕引当金 = 55;
        public static readonly int MS_HIMOKU_ID_特別修繕引当金取崩 = 56;

        public static readonly int MS_HIMOKU_ID_船員保険料A = 33;
        public static readonly int MS_HIMOKU_ID_船員保険料B = 34;
        public static readonly int MS_HIMOKU_ID_予備船員費B = 38;

        public static readonly int MS_HIMOKU_ID_貸船料 = 9;
        public static readonly int MS_HIMOKU_ID_ADDCOMM = 10;
        public static readonly int MS_HIMOKU_ID_その他海運業費用 = 63;

        public static readonly int MS_HIMOKU_ID_借船料 = 62;



        public static readonly string[] KIKAN = { "年度（ 4月～ 3月）",
                                                    " 4月",
                                                    " 5月",
                                                    " 6月",
                                                    " 7月",
                                                    " 8月",
                                                    " 9月",
                                                    "10月",
                                                    "11月",
                                                    "12月",
                                                    " 1月",
                                                    " 2月",
                                                    " 3月",
                                                    "四半期（ 4月～ 6月）",
                                                    "四半期（ 7月～ 9月）",
                                                    "四半期（10月～12月）",
                                                    "四半期（ 1月～ 3月）",
                                                    "上期（ 4月～ 9月）",
                                                    "下期（10月～ 3月）",
                                                };

        public static readonly string[] MONTH = { " 4月",
                                                    " 5月",
                                                    " 6月",
                                                    " 7月",
                                                    " 8月",
                                                    " 9月",
                                                    "10月",
                                                    "11月",
                                                    "12月",
                                                    " 1月",
                                                    " 2月",
                                                    " 3月",
                                                };

        public static readonly string[] TANI = { "千円",
                                                    "百万円",
                                                };

        public static readonly string[] HANI = { "単月",
                                                    "累計",
                                                };

        public static readonly Dictionary<string, bool> BUMON_VISIBILITY = new Dictionary<string, bool>();


        private Constants()
        {
        }


        public static bool BelongMsBumon(string msBumonId)
        {
            foreach (MsUserBumon userBumons in DbTableCache.instance().MsUserBumonList)
            {
                if (userBumons.MsBumonID == msBumonId)
                {
                    return true;
                }
            }
            
            return false;
        }


        public static Color GetMsBumonColor(string msBumonId)
        {
            // "営業G"
            if (msBumonId == "1")
            {
                return Color.Salmon;
            }
            // "船員G"
            else if (msBumonId == "2")
            {
                return Color.LightBlue;
            }
            // "工務G"
            else if (msBumonId == "3")
            {
                return Color.Yellow;
            }
            // "営業G"
            else if (msBumonId == "4")
            {
                return Color.LightGreen;
            }

            return Color.White;
        }


        public static bool IncludeHimoku(string bumonId)
        {

            HimokuTreeNode node = HimokuTreeReader.GetHimokuTree();


            foreach (HimokuTreeNode n in node)
            {
                if (n.MsHimoku.MsBumonID == bumonId)
                {
                    return true;
                }
            }

            return false;
        }


        public static bool IsKamiki(string nengetsu)
        {
            string month = nengetsu.Substring(4, 2);

            for (int i = 0; i < NBaseData.DS.Constants.PADDING_MONTHS.Length; i++)
            {
                if (month == NBaseData.DS.Constants.PADDING_MONTHS[i])
                {
                    return i < 6 ? true : false;
                }
            }

            return false;
        }


        internal static bool IsKamiki(int month)
        {
            for (int i = 0; i < NBaseData.DS.Constants.INT_MONTHS.Length; i++)
            {
                if (month == NBaseData.DS.Constants.INT_MONTHS[i])
                {
                    return i < 6 ? true : false;
                }
            }

            return true;
        }
    }
}
