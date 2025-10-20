using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using NBaseData.DAC;

namespace NBaseData.DS
{
    [DataContract()]
    public class MsSeninAdvancedFilter
    {
        public static string[] Array基本情報 = { "Rank", "Nationality", "Age" };
        public enum Enum基本情報 { Rank, Nationality, Age };
        public enum EnumAge { None, Over50, Over40, Over30 };

        public static string[] Array乗下船履歴 = { "Boarding days", "SignOff", "Sickness" };
        public enum Enum乗下船履歴 { 乗船日数, 下船日, 傷病 };

        public static string[] Array免許免状 = { "Validity" };

        public static string[] ArrayCrewMatrix = { "Years with Operator", "Years in Rank", "Years on This Type of", "Years on All Type of" };
        public enum EnumCrewMatrix { YearsWithOperator, YearsInRank, YearsOnThisType, YearsOnAllType };
       
        public static string[] ArrayConductReport = { "評価" };
        public static string[] Array健康診断 = { "Validity" };
        public static string[] Array英語テスト = { "Score" };

        /// <summary>
        /// 職名ID
        /// </summary>
        [DataMember]
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        [DataMember]
        public string MsRegionalCode { get; set; }

        /// <summary>
        /// 年齢
        /// </summary>
        [DataMember]
        public EnumAge Age { get; set; }

        /// <summary>
        /// 乗船日数
        /// </summary>
        [DataMember]
        public int AfterSignOn { get; set; }

        /// <summary>
        /// 下船予定日
        /// </summary>
        [DataMember]
        public int SignOffYotei { get; set; }



        [DataMember]
        public SiAdvancedSearchConditionHead Head { get; set; }
        
        [DataMember]
        public List<SiAdvancedSearchConditionItem> ItemList { get; set; }

        [DataMember]
        public List<SiAdvancedSearchConditionValue> ValueList { get; set; }




        public int 乗船ID = int.MinValue;


        public MsSeninAdvancedFilter()
        {
            MsSiShokumeiID = int.MinValue;
            MsRegionalCode = null;
            Age = EnumAge.None;
        }

        public static string Format乗船日数(int n)
        {
            return n.ToString() + " Months";
        }
        public static string Format下船日(int n)
        {
            return n.ToString() + " Months";
        }
    }
}
