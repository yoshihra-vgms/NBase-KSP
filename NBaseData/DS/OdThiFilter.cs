using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NBaseData.DS
{
    [DataContract()]
    public class OdThiFilter
    {
        [DataMember]
        public int MsVesselID { get; set; }
        [DataMember]
        public string JimTantouID { get; set; }
        [DataMember]
        public string MsThiIraiSbtID { get; set; }
        [DataMember]
        public string MsThiIraiShousaiID { get; set; }
        [DataMember]
        public DateTime ThiIraiDateFrom { get; set; }
        [DataMember]
        public DateTime ThiIraiDateTo { get; set; }
        [DataMember]
        public DateTime JryDateFrom { get; set; }
        [DataMember]
        public DateTime JryDateTo { get; set; }
        [DataMember]
        public DateTime HachuDateFrom { get; set; }
        [DataMember]
        public DateTime HachuDateTo { get; set; }
        [DataMember]
        public List<string> MsThiIraiStatusIDs { get; set; }

        // 2014.02 2013年度改造
        [DataMember]
        public int JryStatus { get; set; }

        // 2018.01 2017年度改造
        [DataMember]
        public string Nendo { get; set; }

        public OdThiFilter()
        {
            MsVesselID = int.MinValue;
            JimTantouID = null;
            MsThiIraiSbtID = null;
            MsThiIraiShousaiID = null;
            ThiIraiDateFrom = DateTime.MinValue;
            ThiIraiDateTo = DateTime.MaxValue;
            JryDateFrom = DateTime.MinValue;
            JryDateTo = DateTime.MaxValue;
            HachuDateFrom = DateTime.MinValue;
            HachuDateTo = DateTime.MaxValue;
            MsThiIraiStatusIDs = new List<string>();

            JryStatus = int.MinValue; // 2014.02 2013年度改造

            Nendo = null; // 2018.01 2017年度改造
        }
    }
}
