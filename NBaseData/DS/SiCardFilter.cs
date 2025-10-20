using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using NBaseData.DAC;

namespace NBaseData.DS
{
    [DataContract()]
    public class SiCardFilter
    {
        /// <summary>
        /// 船員ID
        /// </summary>
        [DataMember]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 船員ID
        /// </summary>
        [DataMember]
        private List<int> msSeninIDs;
        public List<int> MsSeninIDs
        {
            get
            {
                if (msSeninIDs == null)
                {
                    msSeninIDs = new List<int>();
                }

                return msSeninIDs;
            }
        }

        /// <summary>
        /// 氏名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        [DataMember]
        private List<int> msSiShubetsuIDs;
        public List<int> MsSiShubetsuIDs
        {
            get
            {
                if (msSiShubetsuIDs == null)
                {
                    msSiShubetsuIDs = new List<int>();
                }

                return msSiShubetsuIDs;
            }
        }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        private List<int> msVesselIDs;
        public List<int> MsVesselIDs
        {
            get
            {
                if (msVesselIDs == null)
                {
                    msVesselIDs = new List<int>();
                }

                return msVesselIDs;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string OrderByStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IncludeNullVessel { get; set; }

        /// <summary>
        /// 退職
        /// </summary>
        [DataMember]
        public int RetireFlag { get; set; }

        /// <summary>
        /// 兼務通信長
        /// </summary>
        [DataMember]
        public bool KenmTushincyo { get; set; }

        /// <summary>
        /// 乗船職名ID 
        /// </summary>
        [DataMember]
        public int CardMsSiShokumeiID { get; set; }


        [DataMember]
        public bool includeSchedule { get; set; }

        public SiCardFilter()
        {
            MsSeninID = int.MinValue;
            Start = DateTime.MinValue;
            End = DateTime.MinValue;
            RetireFlag = int.MinValue;
            KenmTushincyo = false;

            Name = null;
            CardMsSiShokumeiID = int.MinValue;

            includeSchedule = true;
        }
    }
}
