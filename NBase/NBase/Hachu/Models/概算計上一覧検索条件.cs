using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Hachu.Models
{
    public class 概算計上一覧検索条件
    {
        public bool YearMonthOnly = true;
        public string YearMonth = "";
        public MsVessel Vessel = null;
        public MsUser User = null;
        public MsThiIraiSbt msThiIraiSbt = null;
        public MsThiIraiShousai msThiIraiShousai = null;
        public bool status未計上 = false;

        public bool status計上済 = false;
        public DateTime NextNenGetsu = DateTime.MinValue;
    }
}
