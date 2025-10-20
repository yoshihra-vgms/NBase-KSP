using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace NBaseUtil
{
    public class StringUtils
    {
        private StringUtils()
        {
        }

        public static bool Empty(string s)
        {
            if (s == null || s.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static String ToStr(int i)
        {
            if (i == int.MinValue)
            {
                return "";
            }
            else
            {
                return i.ToString();
            }
        }


        public static String ToStr(DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                return "";
            }
            else
            {
                return date.ToShortDateString();
            }
        }


        public static String ToStr(DateTime start, DateTime end)
        {
            if (start == DateTime.MinValue || end == DateTime.MinValue)
            {
                return "";
            }
            else
            {
                TimeSpan span = DateTimeUtils.ToTo(end) - DateTimeUtils.ToFrom(start);
                return span.Days.ToString();
            }
        }


        public static string ToStatusStr(int sendFlag)
        {
            if (sendFlag == 0)
            {
                return "";
            }
            else if (sendFlag == 1)
            {
                return "同期済";
            }
            else
            {
                return "不明";
            }
        }


        public static int ToNumber(string text)
        {
            int i;
            bool ret = int.TryParse(text, out i);

            if (!ret && text.Length > 0)
            {
                return 0;
            }

            return i;
        }
        public static decimal ToDecimal(string text)
        {
            decimal i;
            bool ret = decimal.TryParse(text, out i);

            if (!ret && text.Length > 0)
            {
                return 0;
            }

            return i;
        }

        public static string CreateHash(string sourceStr)
        {
            byte[] hash = SHA1Managed.Create().ComputeHash(System.Text.Encoding.GetEncoding("shift_jis").GetBytes(sourceStr));
            return BitConverter.ToString(hash).Replace("-", "");
        }

        
        public static bool isHankaku(string str)
        {
            int num = Encoding.GetEncoding("Shift_JIS").GetByteCount(str);
            return num == str.Length;
        }

        public static string Escape(string str)
        {
            if (str.Length > 0)
            {
                str = str.Replace('&', '＆');
                str = str.Replace('"', '”');
                str = str.Replace('<', '＜');
                str = str.Replace('>', '＞');
                str = str.Replace('\'', '’');
                str = str.Replace(',', '，');
                str = str.Replace('%', '％');
            }
            return str;
        }
    }
}
