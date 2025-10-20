using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Hachu.Models
{
    public class 振替取立一覧検索条件
    {
        public MsVessel Vessel = null;
        public MsThiIraiSbt ThiIraiSbt = null;
        public MsThiIraiShousai ThiIraiShousai = null;
        public string hachuDateFrom = "";
        public string hachuDateTo = "";
    }
}
