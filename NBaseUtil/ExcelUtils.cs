using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBaseUtil
{
    public class ExcelUtils
    {
        public static string ToCellName(int x, int y)
        {
            string ten = "";
            if (x >= 26)
            {
                ten = ToAlphabet(x / 26 - 1);
            }
            
            string one = ToAlphabet(x % 26);

            return ten + one + (y + 1);
        }


        private static string ToAlphabet(int digit)
        {
            int aNum = (int)'A';

            return ((char)(aNum + digit)).ToString();
        }
    }
}
