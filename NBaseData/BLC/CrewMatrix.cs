using ORMapping.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NBaseData.BLC
{
    public class CrewMatrix
    {
        public enum CREW_MATRIX_TYPE
        {
            YEARS_IN_OPERATOR,
            YEARS_IN_RANK,
            YEARS_ON_THIS_TYPE_OF_TANKER
        };

        /// <summary>
        /// TYPE
        /// </summary>
        [DataMember]
        public CREW_MATRIX_TYPE Type { get; set; }

        /// <summary>
        /// 船員ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// TYPE_OF_TANKER
        /// </summary>
        [DataMember]
        [ColumnAttribute("TYPE_OF_TANKER")]
        public string TypeOfTanker { get; set; }


        /// <summary>
        /// DAYS
        /// </summary>
        [DataMember]
        [ColumnAttribute("DAYS")]
        public int Days { get; set; }


    }
}
