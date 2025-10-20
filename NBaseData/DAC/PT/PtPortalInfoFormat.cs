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
    [TableAttribute("PT_PORTAL_INFO_FORMAT")]
    public class PtPortalInfoFormat : ISyncTable
    {
        #region データメンバ
        
    //PT_PORTAL_INFO_FORMAT_ID       NVARCHAR2(40) NOT NULL,
    //MS_PORTAL_INFO_SHUBETU_ID      NVARCHAR2(40),
    //MS_PORTAL_INFO_KOUMOKU_ID      NVARCHAR2(40),
    //MS_PORTAL_INFO_KUBUN_ID        VARCHAR2(40),
    //NAIYOU                         NVARCHAR2(200),
    //SHOUSAI                        NVARCHAR2(200),
    //KIKAN                          NUMBER(5,0),
    //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
    //SEND_FLAG                      NUMBER(1,0) NOT NULL,
    //VESSEL_ID                      NUMBER(4,0) NOT NULL,
    //DATA_NO                        NUMBER(13,0),
    //USER_KEY                       VARCHAR2(40),
    //RENEW_DATE                     DATE NOT NULL,
    //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
    //TS                             VARCHAR2(20),
    

        /// <summary>
        /// ポータル情報表示フォーマット
        /// </summary>
        [DataMember]
        [ColumnAttribute("PT_PORTAL_INFO_FORMAT_ID", true)]
        public string PtPortalInfoFormatId { get; set; }

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
        /// ポータル情報区分マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_PORTAL_INFO_KUBUN_ID")]
        public string MsPortalInfoKubunId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAIYOU")]
        public string Naiyou { get; set; }

        /// <summary>
        /// 詳細
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOUSAI")]
        public string Shousai { get; set; }

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

        public PtPortalInfoFormat()
        {
        }

        public static List<PtPortalInfoFormat> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtPortalInfoFormat), MethodBase.GetCurrentMethod());
            List<PtPortalInfoFormat> ret = new List<PtPortalInfoFormat>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<PtPortalInfoFormat> mapping = new MappingBase<PtPortalInfoFormat>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret; ;
        }

        public static PtPortalInfoFormat GetRecordByShubet_Koumoku_Kubun(NBaseData.DAC.MsUser loginUser, string shubetuId, string koumokuId, string kubunId)
        {
            return GetRecordByShubet_Koumoku_Kubun(null, loginUser, shubetuId, koumokuId, kubunId);
        }

        public static PtPortalInfoFormat GetRecordByShubet_Koumoku_Kubun(DBConnect dbConnect, MsUser loginUser, string shubetuId, string koumokuId, string kubunId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtPortalInfoFormat), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtPortalInfoFormat), "ByShubet_Koumoku");

            if (kubunId != null && kubunId != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtPortalInfoFormat), "ByPortalInfoKubunId");
            }
            else
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtPortalInfoFormat), "ByPortalInfoKubunIdIsNull");
            }

            List<PtPortalInfoFormat> ret = new List<PtPortalInfoFormat>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_PORTAL_INFO_SHUBETU_ID", shubetuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KOUMOKU_ID", koumokuId));
  
            if (kubunId != null && kubunId != string.Empty)
            {
                Params.Add(new DBParameter("MS_PORTAL_INFO_KUBUN_ID", kubunId));
            }

            MappingBase<PtPortalInfoFormat> mapping = new MappingBase<PtPortalInfoFormat>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

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
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_PORTAL_INFO_FORMAT_ID", PtPortalInfoFormatId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_SHUBETU_ID", MsPortalInfoShubetuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KOUMOKU_ID", MsPortalInfoKoumokuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KUBUN_ID", MsPortalInfoKubunId));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("SHOUSAI", Shousai));
            Params.Add(new DBParameter("KIKAN", Kikan));

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
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_PORTAL_INFO_FORMAT_ID", PtPortalInfoFormatId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_SHUBETU_ID", MsPortalInfoShubetuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KOUMOKU_ID", MsPortalInfoKoumokuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KUBUN_ID", MsPortalInfoKubunId));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("SHOUSAI", Shousai));
            Params.Add(new DBParameter("KIKAN", Kikan));

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
        //    Params.Add(new DBParameter("PK", PtPortalInfoFormatId));

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
            Params.Add(new DBParameter("PK", PtPortalInfoFormatId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}