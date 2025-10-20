using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using NBaseData.DAC;

namespace NBaseData.DS
{
    [DataContract()]
    public class SiAdvancedSearchFilter
    {
        /// <summary>
        /// 表示項目に免状含む
        /// </summary>
        [DataMember]
        public bool includeMenjou { get; set; }

        /// <summary>
        /// 表示項目に家族含む
        /// </summary> 
        [DataMember]
        public bool includeKazoku { get; set; }

        /// <summary>
        /// 表示項目に健康診断含む
        /// </summary>
        [DataMember]
        public bool includeKenshin { get; set; }

        /// <summary>
        /// 表示項目に賞罰含む
        /// </summary>
        [DataMember]
        public bool includeShobatsu { get; set; }

        /// <summary>
        /// 表示項目に特記事項含む
        /// </summary>
        [DataMember]
        public bool includeRemarks { get; set; }

        /// <summary>
        /// 表示項目に基本給含む
        /// </summary>
        [DataMember]
        public bool includeSalary { get; set; }


        public SiAdvancedSearchFilter()
        {
            includeMenjou = false;
            includeKazoku = false;
            includeKenshin = false;
            includeShobatsu = false;
            includeRemarks = false;
            includeSalary = false;
        }
    }
}
