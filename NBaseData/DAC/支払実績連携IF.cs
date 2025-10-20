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
    public class 支払実績連携IF
    {
        #region データメンバ

        /// <summary>
        /// 申請識別管理番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("SINSEI_KANRI_NO", true)]
        public string SinseiKanriNo { get; set; }

        /// <summary>
        /// 基幹申請会社コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIKAN_KISY_CD", true)]
        public string KikanKisyCd { get; set; }

        /// <summary>
        /// 基幹申請番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIKAN_SINSEI_NO", true)]
        public string KikanSinseiNo { get; set; }

        /// <summary>
        /// 申請ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_STATUS")]
        public decimal DataStatus { get; set; }

        /// <summary>
        /// 計上日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KEIJO_YMD")]
        public DateTime KeijyoYmd { get; set; }

        /// <summary>
        /// 入出金日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIN_YMD")]
        public DateTime KinYmd { get; set; }

        #endregion

        public enum STATUS
        {
            実績 = 3,
            支払済み = 9,
            エラー = -1,　　　// 2009.10.29 基幹側のステータスとしては、-9 が取り込みエラー
            差し戻し = -9,　  // 2009.10.29 基幹側のステータスとしては、-1 が棄却　らしい　　・・・・
        };

        public static 支払実績連携IF GetRecordByOdSinseiKanriNo(NBaseData.DAC.MsUser loginUser, string odSinseiKanriNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(支払実績連携IF), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(支払実績連携IF), "ByOdSinseiKanriNo");

            List<支払実績連携IF> ret = new List<支払実績連携IF>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SINSEI_KANRI_NO", odSinseiKanriNo));
            MappingBase<支払実績連携IF> mapping = new MappingBase<支払実績連携IF>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
    }
}
