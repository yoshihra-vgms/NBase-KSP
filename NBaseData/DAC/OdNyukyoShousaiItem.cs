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

namespace NBaseData.DAC
{
    [DataContract()]
    public class OdNyukyoShousaiItem
    {
        #region データメンバ
    //OR_NYUKYO_SHOUSAI_ITEM_ID      VARCHAR2(40) NOT NULL,
    //OR_NYUKYO_ITEM_ID              VARCHAR2(40),
    //NAME                           NVARCHAR2(50),
    //BIKOU                          NVARCHAR2(50),
    //MS_TANI_ID                     VARCHAR2(40),
    //SEND_FLAG                      NUMBER(1,0) DEFAULT 0 NOT NULL,
    //VESSEL_ID                      NUMBER(4,0) NOT NULL,
    //DATA_NO                        NUMBER(13,0),
    //USER_KEY                       VARCHAR2(40) DEFAULT 0,
    //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
    //RENEW_DATE                     DATE NOT NULL,
    //TS                             TIMESTAMP(6),

        /// <summary>
        /// 入渠詳細品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OR_NYUKYO_SHOUSAI_ITEM_ID")]
        public string OrNyukyoShousaiItemID { get; set; }

        /// <summary>
        /// 入渠品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OR_NYUKYO_ITEM_ID")]
        public string OrNyukyoItemID { get; set; }

        /// <summary>
        /// 詳細品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 単位ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_ID")]
        public string MsTaniID { get; set; }

        /// <summary>
        /// 同期:送信フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        /// <summary>
        /// 同期:船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        /// <summary>
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public string RenewUserID { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }

        /// <summary>
        /// 入渠品目 品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("ITEN_NAME")]
        public string ItemName { get; set; }

        /// <summary>
        /// 単位名
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANI_NAME")]
        public string TaniName { get; set; }

        #endregion

        public OdNyukyoShousaiItem()
        {
        }

        public static List<OdNyukyoShousaiItem> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdNyukyoShousaiItem), MethodBase.GetCurrentMethod());
            List<OdNyukyoShousaiItem> ret = new List<OdNyukyoShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdNyukyoShousaiItem> mapping = new MappingBase<OdNyukyoShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdNyukyoShousaiItem GetRecord(NBaseData.DAC.MsUser loginUser, string OrNyukyoShousaiItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdNyukyoShousaiItem), MethodBase.GetCurrentMethod());
            List<OdNyukyoShousaiItem> ret = new List<OdNyukyoShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OR_NYUKYO_SHOUSAI_ITEM_ID", OrNyukyoShousaiItemID));
            MappingBase<OdNyukyoShousaiItem> mapping = new MappingBase<OdNyukyoShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdNyukyoShousaiItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OR_NYUKYO_SHOUSAI_ITEM_ID", OrNyukyoShousaiItemID));
            Params.Add(new DBParameter("OR_NYUKYO_ITEM_ID", OrNyukyoItemID));
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("MS_TANI_ID", MsTaniID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdNyukyoShousaiItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OR_NYUKYO_ITEM_ID", OrNyukyoItemID));
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("MS_TANI_ID", MsTaniID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("OR_NYUKYO_SHOUSAI_ITEM_ID", OrNyukyoShousaiItemID));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
