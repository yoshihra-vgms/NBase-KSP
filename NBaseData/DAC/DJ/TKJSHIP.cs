using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;
//using Oracle.DataAccess.Client;

namespace NBaseData.DAC
{
    /// <summary>
    /// 基幹：船マスタ
    /// </summary>
    [DataContract()]
    [TableAttribute("TKJSHIP")]
    public class TKJSHIP
    {
        
        #region データメンバ
        /// <summary>
        /// 船舶NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUNE_NO")]
        public string FuneNo { get; set; }

        [DataMember]
        [ColumnAttribute("PROFITS_KBN")]
        public string ProfitsKbn { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NAME")]
        public string VesselName { get; set; }

        /// <summary>
        /// DWT
        /// </summary>
        [DataMember]
        [ColumnAttribute("DWT")]
        public int DWT { get; set; }

        /// <summary>
        /// 貨物重量
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGO_WEIGHT")]
        public decimal CargoWeight { get; set; }

        /// <summary>
        /// オフィシャルナンバー
        /// </summary>
        [DataMember]
        [ColumnAttribute("OFFICIAL_NUMBER")]
        public string OfficialNumber { get; set; }

        // 201410月度改造
        /// <summary>
        /// GRT
        /// </summary>
        [DataMember]
        [ColumnAttribute("GRT")]
        public decimal GRT { get; set; }

        #endregion

        public static List<TKJSHIP> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJSHIP), MethodBase.GetCurrentMethod());
            //SQL += SqlMapper.SqlMapper.GetSql(typeof(TKJSHIP), "OrderBy");

            List<TKJSHIP> ret = new List<TKJSHIP>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<TKJSHIP> mapping = new MappingBase<TKJSHIP>();

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
    }
}
