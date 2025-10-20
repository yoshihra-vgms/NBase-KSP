using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading;
using System.Globalization;
using System.Configuration;

namespace NBaseUtil
{
    public class DateTimeUtils
    {
        private static readonly DateTimeUtils INSTANCE = new DateTimeUtils();
        public static DateTimeUtils instance()
        {
            return INSTANCE;
        }

        private DateTimeUtils()
        {
        }


        //public static readonly string[] MONTH = { " 4",
        //                                            " 5",
        //                                            " 6",
        //                                            " 7",
        //                                            " 8",
        //                                            " 9",
        //                                            "10",
        //                                            "11",
        //                                            "12",
        //                                            " 1",
        //                                            " 2",
        //                                            " 3",
        //                                        };

        public static string[] _MONTH = null;
        public string[] MONTH
        {
            get
            {
                Init();
                return _MONTH;
            }
        }
        public static int[] _INT_MONTH = null;
        public int[] INT_MONTH
        {
            get
            {
                Init();
                return _INT_MONTH;
            }
        }
        public static readonly string[] ZENKAKU_MONTH = { "１",
                                                          "２",
                                                          "３",
                                                          "４",
                                                          "５",
                                                          "６",
                                                          "７",
                                                          "８",
                                                          "９",
                                                          "１０",
                                                          "１１",
                                                          "１２",
                                                         };
        public static int 年度開始月
        {
            get
            {
                int startMonth = 4;
                try
                {
                    startMonth = int.Parse(ConfigurationSettings.AppSettings["年度開始月"]);
                }
                catch
                {
                }
                return startMonth;
            }
        }

        private void Init()
        {
            int idx = 0;
            if (_INT_MONTH == null)
            {
                _INT_MONTH = new int[12];
                idx = 0;

                for (int i = 年度開始月; i <= 12; i++)
                {
                    _INT_MONTH[idx] = i;
                    idx++;
                }
                for (int i = 1; i < 年度開始月; i++)
                {
                    _INT_MONTH[idx] = i;
                    idx++;
                }
            }
            if (_MONTH == null)
            {
                _MONTH = new string[12];
                idx = 0;

                for (int i = 年度開始月; i <= 12; i++)
                {
                    if (i < 10)
                    {
                        _MONTH[idx] = " " + i.ToString();
                    }
                    else
                    {
                        _MONTH[idx] = i.ToString();
                    }
                    idx++;
                }
                for (int i = 1; i < 年度開始月; i++)
                {
                    if (i < 10)
                    {
                        _MONTH[idx] = " " + i.ToString();
                    }
                    else
                    {
                        _MONTH[idx] = i.ToString();
                    }
                    idx++;
                }
            }
        }
        public static int GetMonthIndex(int month)
        {
            int ret = -1;
            for (int i = 0; i < 12; i++)
            {
                if (_INT_MONTH[i] == month)
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }

        public static string ToString(DateTime date)
        {
            return date != DateTime.MinValue ? date.ToShortDateString() : "";
        }


        public static DateTime ToFrom(DateTime date)
        {
            DateTime result = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            return result;
        }


        public static DateTime ToTo(DateTime date)
        {
            return ToFrom(date).AddDays(1);
        }


        public static DateTime ToFromMonth(DateTime date)
        {
            DateTime result = new DateTime(date.Year, date.Month, 1, 0, 0, 0);

            return result;
        }


        public static DateTime ToToMonth(DateTime date)
        {
            return ToFromMonth(date).AddMonths(1);
        }


        public static DateTime 年度開始日()
        {
            return 年度開始日(DateTime.Now);
        }


        public static DateTime 年度終了日()
        {
            return 年度終了日(DateTime.Now);
        }

        public static DateTime 年度開始日(DateTime date)
        {
            int year = date.Year;
            if (date.Month < 年度開始月)

            {
                year--;
            }
            return new DateTime(year, 年度開始月, 1, 0, 0, 0);

        }

        public static DateTime 年度終了日(DateTime date)
        {
            DateTime start = 年度開始日(date);
            return start.AddYears(1).AddSeconds(-1);
        }

        public static string 年度終了月(string ym)
        {
            DateTime d = DateTime.Parse(ym.Substring(0, 4) + "/" + ym.Substring(4, 2) + "/01");

            DateTime end = 年度終了日(d);
            return end.ToString("yyyyMM");
        }


        public static int 年齢計算(DateTime birthday)
        {
            DateTime b = new DateTime(birthday.Year, birthday.Month, birthday.Day);

            if (DateTime.Today > b)
            {
                return (new DateTime((DateTime.Today - b).Ticks)).Year - 1;
            }
            else
            {
                return -1;
            }
        }


        public static bool Validate(string text)
        {
            //DateTime result;

            //return DateTime.TryParse(text, out result);
            DateTime result;
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            DateTimeFormatInfo dtf = ci.DateTimeFormat;
            //switch (_format)
            //{
            //    case DateTimePickerFormat.Long:
            //        FormatAsString = dtf.LongDatePattern;
            //        break;
            //    case DateTimePickerFormat.Short:
            //        FormatAsString = dtf.ShortDatePattern;
            //        break;
            //    case DateTimePickerFormat.Time:
            //        FormatAsString = dtf.ShortTimePattern;
            //        break;
            //    case DateTimePickerFormat.Custom:
            //        FormatAsString = this.CustomFormat;
            //        break;
            //}
            try
            {
                result = DateTime.Parse(text, ci, DateTimeStyles.AssumeLocal);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool Empty(string text)
        {
            string emptyText = "    /  /";
            //CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            //if (ci.Name == "ja-JP")
            //{
            //    emptyText = "    /  /";
            //}
            //else if (ci.Name == "ko-KR")
            //{
            //    emptyText = "    -  -";
            //}
            return text == emptyText;
        }


        public static int 月数(DateTime start, DateTime end)
        {
            if (start == DateTime.MinValue || end == DateTime.MinValue)
                return 0;

            int sm = start.Month;
            int em = end.Month;

            if (1 <= sm && sm < 年度開始月)
            {
                sm += 12;
            }

            if (1 <= em && em < 年度開始月)
            {
                em += 12;
            }

            return em - sm + 1;
        }

        public static int 日数計算(DateTime start, DateTime end)
        {
            int days = 0;
            if (start != DateTime.MinValue && end != DateTime.MinValue)
            {
                days = int.Parse(StringUtils.ToStr(start, end));
            }
            else if (start == DateTime.MinValue)
            {
                days = 0;
            }
            else if (end == DateTime.MinValue)
            {
                days = int.Parse(StringUtils.ToStr(start, DateTime.Now));
            }
            return days;
        }

        public static int GetElapsedMonths(DateTime baseDay, DateTime day)
        {
            if (day < baseDay)
                // 日付が基準日より前の場合は例外とする
                throw new ArgumentException();

            // 経過月数を求める(満月数を考慮しない単純計算)
            var elapsedMonths = (day.Year - baseDay.Year) * 12 + (day.Month - baseDay.Month);

            if (baseDay.Day <= day.Day)
                // baseDayの日部分がdayの日部分以上の場合は、その月を満了しているとみなす
                // (例:1月30日→3月30日以降の場合は満(3-1)ヶ月)
                return elapsedMonths;
            else if (day.Day == DateTime.DaysInMonth(day.Year, day.Month) && day.Day <= baseDay.Day)
                // baseDayの日部分がdayの表す月の末日以降の場合は、その月を満了しているとみなす
                // (例:1月30日→2月28日(平年2月末日)/2月29日(閏年2月末日)以降の場合は満(2-1)ヶ月)
                return elapsedMonths;
            else
                // それ以外の場合は、その月を満了していないとみなす
                // (例:1月30日→3月29日以前の場合は(3-1)ヶ月未満、よって満(3-1-1)ヶ月)
                return elapsedMonths - 1;
        }

        // 基準日baseDayからdayまでの経過年数を求める
        public static int GetElapsedYears(DateTime baseDay, DateTime day)
        {
            // 経過月数÷12(端数切り捨て)したものを経過年数とする
            // (満12ヶ月で満1年とする)
            return GetElapsedMonths(baseDay, day) / 12;
        }
    }
}
