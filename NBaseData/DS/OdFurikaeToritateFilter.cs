using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NBaseData.DS
{
    [DataContract()]
    public class OdFurikaeToritateFilter
    {
        [DataMember]
        public int MsVesselID { get; set; }
        [DataMember]
        public string HachuUserID { get; set; }
        [DataMember]
        public string MsThiIraiSbtID { get; set; }
        [DataMember]
        public string MsThiIraiShousaiID { get; set; }
        [DataMember]
        public DateTime HachuDateFrom { get; set; }
        [DataMember]
        public DateTime HachuDateTo { get; set; }

        public OdFurikaeToritateFilter()
        {
            MsVesselID = int.MinValue;
            HachuUserID = null;
            MsThiIraiSbtID = null;
            MsThiIraiShousaiID = null;
            HachuDateFrom = DateTime.MinValue;
            HachuDateTo = DateTime.MaxValue;
        }
    }
}
