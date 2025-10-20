using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseCommon;
using System.Drawing;

namespace NBaseService
{
    public class Constants
    {
        public static MsUser LoginUser = null;

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

        // 年予算を生成する年数.
        private static readonly int YEAR_RANGE_当初予算 = 20;
        private static readonly int YEAR_RANGE_見直し予算 = 2;

        // 月予算を生成する年数.
        private static readonly int MONTH_RANGE_当初予算 = 1;
        private static readonly int MONTH_RANGE_見直し予算 = 1;


        //販菅費関連費目ID
        public static readonly int 販菅費関連費目ID = 66;

        public static readonly Dictionary<string, bool> BUMON_VISIBILITY = new Dictionary<string, bool>();


        private Constants()
        {
        }


        /*public static bool BelongMsBumon(string msBumonId)
        {
            foreach (MsUserBumon userBumons in DbTableCache.instance().MsUserBumonList)
            {
                if (userBumons.MsBumonID == msBumonId)
                {
                    return true;
                }
            }
            
            return false;
        }*/


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


        public static int GetYearRange(int yosanSbtId)
        {
            if (yosanSbtId == (int)BgYosanSbt.BgYosanSbtEnum.当初)
            {
                return YEAR_RANGE_当初予算;
            }
            else if (yosanSbtId == (int)BgYosanSbt.BgYosanSbtEnum.見直し)
            {
                return YEAR_RANGE_見直し予算;
            }

            return 0;
        }


        public static int GetMonthRange(int yosanSbtId)
        {
            if (yosanSbtId == (int)BgYosanSbt.BgYosanSbtEnum.当初)
            {
                return MONTH_RANGE_当初予算;
            }
            else if (yosanSbtId == (int)BgYosanSbt.BgYosanSbtEnum.見直し)
            {
                return MONTH_RANGE_見直し予算;
            }

            return 0;
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
    }
}
