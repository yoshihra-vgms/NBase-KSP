using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace NBaseUtil
{
    public class NumberUtils
    {
        private NumberUtils()
        {
        }

        public static bool Empty(string text)
        {
            if (text == null)
                return true;

            if (text.Length == 0)
                return true;

            return false;
        }

        public static bool Validate(string text)
        {
            int result;

            return int.TryParse(text, out result);
        }

        public static bool ValidateDecimal(string text)
        {
            decimal result;

            return decimal.TryParse(text, out result);
        }

        public static bool ValidateDecimal(string text, int accuracy)
        {
            bool ret = true;
            if (text.Contains(".") && accuracy > 0)
            {
                //if (((text.Length - 1) - text.IndexOf(".")) != accuracy)
                if (((text.Length - 1) - text.IndexOf(".")) > accuracy)
                {
                    ret = false;
                }
            }
            else
            {
                decimal result;

                ret = decimal.TryParse(text, out result);
            }

            return ret;
        }
        public static double ToRoundDown(double dValue, int iDigits)
        {
            double dCoef = System.Math.Pow(10, iDigits);

            return dValue > 0 ? System.Math.Floor(dValue * dCoef) / dCoef :
                                System.Math.Ceiling(dValue * dCoef) / dCoef;
        }


        public static string ToString(int val)
        {
            if (val != null && val != int.MinValue)
            {
                return val.ToString();
            }
            else
            {
                return "";
            }
        }
        public static string ToString(decimal val)
        {
            if (val != null && val != decimal.MinValue)
            {
                return val.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
