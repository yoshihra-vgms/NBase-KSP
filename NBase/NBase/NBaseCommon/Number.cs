using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBaseCommon
{
    public class Number
    {

        /// <summary>
        /// NUMBERの指定した最大値を返す
        /// </summary>
        /// <param name="整数部"></param>
        /// <returns></returns>
        public static int MaxValue(int 整数部)
        {
            int val = 1;

            for (int i = 1; i <= 整数部; i++)
            {
                val *= 10;
            }
            val--;
            return val;
        }

        public static bool CheckValue(double Val, int 整数部, int 小数点部)
        {
            //数字チェック
            try
            {
                Convert.ToDouble(Val);
            }
            catch
            {

            }

            string org = Val.ToString();
            string[] str = org.Split('.');

            if (Convert.ToInt64(str[0]) > Number.MaxValue(整数部))
            {
                return false;
            }

            if (str.Length > 1 && Convert.ToInt64(str[1]) > Number.MaxValue(小数点部))
            {
                return false;
            }
            return true;
        }
    }
}
