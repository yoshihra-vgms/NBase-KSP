using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using Yojitsu.DA;

namespace Yojitsu.util
{
    class RateBuilder
    {
        private RateBuilder()
        {
        }


        internal static Dictionary<int, BgRate> Build_レート(List<BgRate> rates)
        {
            var rateDic = new Dictionary<int, BgRate>();

            foreach (BgRate r in rates)
            {
                rateDic[r.Year] = r;
            }

            return rateDic;
        }


        internal static decimal DetectRate(Dictionary<int, BgRate> rateDic, string nengetsu)
        {
            int year = DetectYear(nengetsu);

            if (!rateDic.ContainsKey(year))
            {
                return 0;
            }

            if (nengetsu.Trim().Length == 4 || Constants.IsKamiki(nengetsu))
            {
                return rateDic[year].KamikiRate;
            }
            else
            {
                return rateDic[year].ShimokiRate;
            }
        }


        internal static decimal DetectRate(Dictionary<int, BgRate> rateDic, string nengetsu, out BgRate rate)
        {
            int year = DetectYear(nengetsu);

            if (!rateDic.ContainsKey(year))
            {
                rate = null;
                return 0;
            }

            if (nengetsu.Trim().Length == 4 || Constants.IsKamiki(nengetsu))
            {
                rate = rateDic[year];
                return rateDic[year].KamikiRate;
            }
            else
            {
                rate = rateDic[year];
                return rateDic[year].ShimokiRate;
            }
        }
        

        private static int DetectYear(string nengetsu)
        {
            int year = Int32.Parse(nengetsu.Substring(0, 4));
            string month = nengetsu.Substring(4, 2);
            
            if (month == "01" || month == "02" || month == "03")
            {
                year--;
            }
            
            return year;
        }
    }
}
