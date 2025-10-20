using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Hachu.Models
{
    public class 発注一覧検索条件
    {
        public MsVessel Vessel = null;
        public MsUser User = null;
        public MsThiIraiSbt ThiIraiSbt = null;
        public MsThiIraiShousai ThiIraiShousai = null;
        public string thiIraiDateFrom = "";
        public string thiIraiDateTo = "";
        public string jryDateFrom = "";
        public string jryDateTo = "";
        public bool status未対応 = false;
        public bool status見積中 = false;
        public bool status発注済 = false;
        public bool status受領済 = false;
        public bool status完了 = false;

        // 2014.02 2013年度改造
        public bool status船受領 = false;

        // 2017.12 2017年度改造
        public string hachuDateFrom = "";
        public string hachuDateTo = "";
        public string nendo = "";
    }
}
