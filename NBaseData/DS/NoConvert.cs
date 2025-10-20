using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBaseData.DS
{
    public class NoConvert
    {
        /// <summary>
        /// int 型の値を N 進数を示す文字列に変換します。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static string IntToN(int a, int sp)
        {
            return ByteToN(BitConverter.GetBytes(a), sp).ToUpper();
        }

        /// <summary>
        /// バイト列を N 進数を示す文字列に変換します。
        /// </summary>
        /// <param name="b"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        private static string ByteToN(byte[] b, int sp)
        {
            long n = BitConverter.ToUInt32(b, 0);
            long num = n;

            char[] ch = getChar(sp);

            string result = string.Empty;

            long amari = 0;
            while (true)
            {
                if (num < sp)
                {
                    result = ch[num] + result;
                    break;
                }
                amari = num % sp;
                num = num / sp;
                result = ch[amari] + result;
            }
            return result;
        }

        /// <summary>
        /// N 進数を示す文字列を数字に変換します。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static int NToInt(string s, int sp)
        {
            return BitConverter.ToInt32(NToByte(s, sp), 0);
        }

        /// <summary>
        /// N 進数を示す文字列をバイト列に変換します。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        private static byte[] NToByte(string s, int sp)
        {
            s = s.ToLower();
            char[] ch = getChar(sp);

            long result = 0;
            int len = s.Length;

            int keta = 0;
            for (int i = len - 1; i >= 0; i--)
            {
                keta = len - i - 1;

                char target = s[i];
                int index = Array.IndexOf(ch, target);
                if (index < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                result += index * ((long)Math.Pow(sp, keta));
            }

            uint are = (uint)(result & 0xFFFFFFFF);
            byte[] b = BitConverter.GetBytes(are);
            return b;
        }

        /// <summary>
        /// 文字テーブルを作成します。
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private static char[] getChar(int count)
        {
            char[] result = new char[count];
            for (int i = 0; i < 10 && i < count; i++)
            {
                result[i] = (char)('0' + ((char)i));
            }
            if (count > 10)
            {
                for (int i = 0; i < count - 10; i++)
                {
                    result[i + 10] = (char)('a' + ((char)i));
                }
            }
            return result;
        }
    }
}
