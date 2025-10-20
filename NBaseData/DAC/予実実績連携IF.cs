using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;
using NBaseData.DS;

namespace NBaseData.DAC
{
    [DataContract()]
    public class 予実実績連携IF
    {
        #region データメンバ

        /// <summary>
        /// 対象年月
        /// </summary>
        [DataMember]
        [ColumnAttribute("TAISYO_YM", true)]
        public string TaisyoYm { get; set; }

        /// <summary>
        /// 船舶No
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUNE_NO", true)]
        public string FuneNo { get; set; }

        /// <summary>
        /// 勘定科目コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANJO_KMK_CD", true)]
        public string KanjoKmkCd { get; set; }

        /// <summary>
        /// 内訳科目コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("UTIWAKE_KMK_CD")]
        public string UtiwakeKmkCd { get; set; }

        /// <summary>
        /// 実績金額（$）
        /// </summary>
        [DataMember]
        [ColumnAttribute("FKIN")]
        public decimal Fkin { get; set; }

        /// <summary>
        /// 実績金額（\）
        /// </summary>
        [DataMember]
        [ColumnAttribute("YKIN")]
        public decimal Ykin { get; set; }

        /// <summary>
        /// 実績金額円換算
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANSAN_YKIN")]
        public decimal KansanYkin { get; set; }

        #endregion

        public static List<予実実績連携IF> GetRecordsByTaisyoYm(NBaseData.DAC.MsUser loginUser, string taisyoYm)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(予実実績連携IF), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(予実実績連携IF), "ByTaisyoYm");

            List<予実実績連携IF> ret = new List<予実実績連携IF>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("TAISYO_YM", taisyoYm));
            MappingBase<予実実績連携IF> mapping = new MappingBase<予実実績連携IF>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
    }
}
