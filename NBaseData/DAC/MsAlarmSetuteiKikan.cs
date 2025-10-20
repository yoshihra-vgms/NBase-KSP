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
using ORMapping.Atts;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_ALARM_SETUTEI_KIKAN")]
    public class MsAlarmSetuteiKikan : ISyncTable
    {
        #region データメンバ

        //MS_ALARM_SETUTEI_KIKAN_ID      NVARCHAR2(40) NOT NULL,
        //MS_PORTAL_INFO_SHUBETU_ID      NVARCHAR2(40),
        //MS_PORTAL_INFO_KOUMOKU_ID      NVARCHAR2(40),
        //KIKAN                          NUMBER(9,0),
        //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
        //SEND_FLAG                      NUMBER(1,0) NOT NULL,
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //USER_KEY                       VARCHAR2(40),
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),

        /// <summary>
        /// アラーム設定期間マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ALARM_SETUTEI_KIKAN_ID", true)]
        public string MsAlarmSetuteiKikaknId { get; set; }

        /// <summary>
        /// ポータル情報種別マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_PORTAL_INFO_SHUBETU_ID")]
        public string MsPortalInfoShubetuId { get; set; }

        /// <summary>
        /// ポータル情報項目マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_PORTAL_INFO_KOUMOKU_ID")]
        public string MsPortalInfoKoumokuId { get; set; }

        /// <summary>
        /// 期間
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIKAN")]
        public int Kikan { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

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

        #endregion

        public MsAlarmSetuteiKikan()
        {
        }

        public static List<MsAlarmSetuteiKikan> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAlarmSetuteiKikan), MethodBase.GetCurrentMethod());
            List<MsAlarmSetuteiKikan> ret = new List<MsAlarmSetuteiKikan>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsAlarmSetuteiKikan> mapping = new MappingBase<MsAlarmSetuteiKikan>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsAlarmSetuteiKikan GetRecordsByShubetu_koumoku_(NBaseData.DAC.MsUser loginUser, string shubetuId, string koumokuId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAlarmSetuteiKikan), "GetRecords");
            SQL += " and MS_ALARM_SETUTEI_KIKAN.MS_PORTAL_INFO_SHUBETU_ID = :SHUBETU_ID";
            SQL += " and MS_ALARM_SETUTEI_KIKAN.MS_PORTAL_INFO_KOUMOKU_ID = :KOUMOKU_ID";

            List<MsAlarmSetuteiKikan> ret = new List<MsAlarmSetuteiKikan>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SHUBETU_ID", shubetuId));
            Params.Add(new DBParameter("KOUMOKU_ID", koumokuId));
            MappingBase<MsAlarmSetuteiKikan> mapping = new MappingBase<MsAlarmSetuteiKikan>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            return false;
        }


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            return false;
        }

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsAlarmSetuteiKikaknId));

        //    int count = Convert.ToInt32(DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params));

        //    return count > 0 ? true : false;
        //}
        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsAlarmSetuteiKikaknId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect,loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }

}
