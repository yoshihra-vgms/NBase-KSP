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
    [TableAttribute("MS_PORTAL_INFO_SHUBETU")]
    public class MsPortalInfoShubetu : ISyncTable
    {
        #region データメンバ

        //MS_PORTAL_INFO_SHUBETU_ID      NVARCHAR2(40) NOT NULL,
        //PORTAL_INFO_SYUBETU_NAME       NVARCHAR2(50),
        //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
        //SEND_FLAG                      NUMBER(1,0) NOT NULL,
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //USER_KEY                       VARCHAR2(40),
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS  


        /// <summary>
        /// ポータル情報種別マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_PORTAL_INFO_SHUBETU_ID", true)]
        public string MsPortalInfoShubetuId { get; set; }

        public enum MsPortalInfoShubetuIdEnum { 検査証書, 船員, 発注, 文書 }

        /// <summary>
        /// ポータル情報種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("PORTAL_INFO_SYUBETU_NAME")]
        public string MsPortalInfoShubetuName { get; set; }

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

        public const string 検査証書 = "検査・証書";
        public const string 船員 = "船員";
        public const string 発注 = "発注";
        public const string 文書 = "文書";

        
        public MsPortalInfoShubetu()
        {
        }

        public static List<MsPortalInfoShubetu> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsPortalInfoShubetu), MethodBase.GetCurrentMethod());
            List<MsPortalInfoShubetu> ret = new List<MsPortalInfoShubetu>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsPortalInfoShubetu> mapping = new MappingBase<MsPortalInfoShubetu>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret; ;
        }

        public static MsPortalInfoShubetu GetRecordByPortalInfoSyubetuName(NBaseData.DAC.MsUser loginUser, string name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsPortalInfoShubetu), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsPortalInfoShubetu), "ByPortalInfoSyubetuName");
            List<MsPortalInfoShubetu> ret = new List<MsPortalInfoShubetu>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PORTAL_INFO_SYUBETU_NAME", name));
            MappingBase<MsPortalInfoShubetu> mapping = new MappingBase<MsPortalInfoShubetu>();
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsPortalInfoShubetu), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_PORTAL_INFO_SHUBETU_ID", MsPortalInfoShubetuId));
            Params.Add(new DBParameter("PORTAL_INFO_SYUBETU_NAME", MsPortalInfoShubetuName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbcone, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsPortalInfoShubetu), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_PORTAL_INFO_SHUBETU_ID", MsPortalInfoShubetuId));
            Params.Add(new DBParameter("PORTAL_INFO_SYUBETU_NAME", MsPortalInfoShubetuName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsPortalInfoShubetuId));

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
            Params.Add(new DBParameter("PK", MsPortalInfoShubetuId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
