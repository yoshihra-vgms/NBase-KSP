using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NBaseData.DS
{
    [DataContract()]
    public class OdJryFilter
    {
        [DataMember]
        public bool YearMonthOnly { get; set; }
        [DataMember]
        public string YearMonth { get; set; }
        [DataMember]
        public int MsVesselID { get; set; }
        [DataMember]
        public string JimTantouID { get; set; }
        [DataMember]
        public bool MiKeijyo { get; set; }
        [DataMember]
        public bool KeijyoZumi { get; set; }
        [DataMember]
        public string MsThiIraiSbtID { get; set; }
        [DataMember]
        public string MsThiIraiShousaiID { get; set; }

        public OdJryFilter()
        {
            YearMonthOnly = true;
            MsVesselID = int.MinValue;
            JimTantouID = null;
            MiKeijyo = false;
            KeijyoZumi = false;
            MsThiIraiSbtID = null;
            MsThiIraiShousaiID = null;
        }
    }
}
