using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseMaster
{
    public class 基本給管理Common
    {
        public static MsSiSalary Salary;
        public static MsSiSalary PrevSalary;

        public static string 金額出力(decimal val)
        {
            return val.ToString("N0");
        }
    }
}
