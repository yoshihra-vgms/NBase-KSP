using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBaseData.DS
{
    public class Constants
    {
        public static int[] INT_MONTHS = { 4, 5, 6, 7, 8, 9, 10, 11, 12, 1, 2, 3 };
        public static string[] MONTHS = { "4", "5", "6", "7", "8", "9", "10", "11", "12", "1", "2", "3" };
        public static string[] PADDING_MONTHS = { "04", "05", "06", "07", "08", "09", "10", "11", "12", "01", "02", "03" };


        public static DateTime BusinessYearFrom(DateTime ymd)
        {
            int y = ymd.Year;
            int m = 4;
            int d = 1;

            if (ymd.Month < 4)
            {
                y -= 1;
            }
            return new DateTime(y, m, d);
        }


        public static DateTime BusinessYearTo(DateTime ymd)
        {
            int y = ymd.Year;
            int m = 3;
            int d = 31;

            if (ymd.Month > 3)
            {
                y += 1;
            }
            return new DateTime(y, m, d);
        }


        public static int GetPaddingMonthIndex(string paddingMonth)
        {
            for (int i = 0; i < NBaseData.DS.Constants.PADDING_MONTHS.Length; i++)
            {
                if (paddingMonth == NBaseData.DS.Constants.PADDING_MONTHS[i])
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
