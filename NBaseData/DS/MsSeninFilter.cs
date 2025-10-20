using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using NBaseData.DAC;

namespace NBaseData.DS
{
    [DataContract()]
    public class MsSeninFilter
    {
        public enum enumRetire { EXCEPT, INCLUDE, ONLY }

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int SeninID { get; set; }

        /// <summary>
        /// 従業員番号
        /// </summary>
        [DataMember]
        public string ShimeiCode { get; set; }

        /// <summary>
        /// 保険番号
        /// </summary>
        [DataMember]
        public string HokenNo { get; set; }

        /// <summary>
        /// 氏名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 氏名（カナ）
        /// </summary>
        [DataMember]
        public string NameKana { get; set; }

        /// <summary>
        /// 姓（カナ）
        /// </summary>
        [DataMember]
        public string SeiKana { get; set; }

        /// <summary>
        /// 名（カナ）
        /// </summary>
        [DataMember]
        public string MeiKana { get; set; }

        /// <summary>
        /// 所属会社ID
        /// </summary>
        [DataMember]
        public string MsSeninCompanyID { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 職名ID
        /// </summary>
        [DataMember]
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        [DataMember]
        private List<int> kubuns;
        public List<int> Kubuns
        {
            get
            {
                if (kubuns == null)
                {
                    kubuns = new List<int>();
                }

                return kubuns;
            }
        }

        /// <summary>
        /// 退職
        /// </summary>
        [DataMember]
        public int RetireFlag { get; set; }

        /// <summary>
        /// 住所
        /// </summary>
        [DataMember]
        public string Juusho { get; set; }

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
        /// 免状種別
        /// </summary>
        [DataMember]
        private List<int> msSiMenjouKindIDs;
        public List<int> MsSiMenjouKindIDs
        {
            get
            {
                if (msSiMenjouKindIDs == null)
                {
                    msSiMenjouKindIDs = new List<int>();
                }

                return msSiMenjouKindIDs;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string OrderByStr { get; set; }


        [DataMember]
        public DateTime 退職年度 { get; set; }


        [DataMember]
        public bool 種別無し { get; set; }
        
        [DataMember]
        public bool 船員テーブルのみ対象 { get; set; }

        [DataMember]
        public bool 職別海技免許等資格一覧対象 { get; set; }

        [DataMember]
        public bool 詳細検索 { get; set; }


        [DataMember]
        public JoinSiCard joinSiCard { get; set; }

        public enum JoinSiCard { NOT_JOIN, LEFT_JOIN, INNER_JOIN };

        /// <summary>
        /// 年度
        /// </summary>
        [DataMember]
        public string Nendo { get; set; }

        /// <summary>
        /// 休暇日数を含む検索
        /// </summary>
        [DataMember]
        public bool IncludeKyuka { get; set; }

        
        public MsSeninFilter()
        {
            ShimeiCode = null;
            HokenNo = null;
            Name = null;
            NameKana = null;
            MsVesselID = int.MinValue;
            MsSeninCompanyID = null;
            MsSiShokumeiID = int.MinValue;
            RetireFlag = 0;
            Juusho = null;
            退職年度 = DateTime.MinValue;
            船員テーブルのみ対象 = false;
            職別海技免許等資格一覧対象 = false;
            詳細検索 = false;


            Nendo = null;
            IncludeKyuka = false;
        }
    }
}
