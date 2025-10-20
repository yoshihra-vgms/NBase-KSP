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
    [TableAttribute("PT_HONSENKOUSHIN_INFO")]
    public class PtHonsenkoushinInfo : ISyncTableDoc
    {
        #region データメンバ

        //PT_HONSENKOUSHIN_INFO_ID       NVARCHAR2(40) NOT NULL,
        //MS_PORTAL_INFO_SHUBETU_ID      NVARCHAR2(40),
        //MS_PORTAL_INFO_KOUMOKU_ID      NVARCHAR2(40),
        //MS_PORTAL_INFO_KUBUN_ID        NVARCHAR2(40),
        //SANSHOUMOTO_ID                 NVARCHAR2(40),
        //EVENT_DATE                     DATE,
        //MS_VESSEL_ID                   NUMBER(4,0) NOT NULL,
        //HONSENKOUSHIN_INFO_USER        NVARCHAR2(40),
        //NAIYOU                         NVARCHAR2(200),
        //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
        //SEND_FLAG                      NUMBER(1,0) NOT NULL,
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //USER_KEY                       NUMBER(9,0) NOT NULL,
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),
        

        /// <summary>
        /// 本船更新情報ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("PT_HONSENKOUSHIN_INFO_ID", true)]
        public string PtHonsenkoushinInfoId { get; set; }

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
        /// 参照元情報ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SANSHOUMOTO_ID")]
        public string SanshoumotoId { get; set; }

        /// <summary>
        /// 日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("EVENT_DATE")]
        public DateTime EventDate { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselId { get; set; }

        /// <summary>
        /// 本船更新情報更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("HONSENKOUSHIN_INFO_USER")]
        public string HonsenkoushinInfoUser { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHUBETSU")]
        public string Shubetsu { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAIYOU")]
        public string Naiyou { get; set; }

        /// <summary>
        /// 更新内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOUSHIN_NAIYOU")]
        public string KoushinNaiyou { get; set; }

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

        /// <summary>
        /// 船ID >> 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NAME")]
        public string VesselName { get; set; }

        /// <summary>
        /// 本船更新情報更新者 >> 姓
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEI")]
        public string Sei { get; set; }

        /// <summary>
        /// 本船更新情報更新者 >> 名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MEi")]
        public string Mei { get; set; }

        /// <summary>
        /// ポータル情報種別マスタID >> ポータル情報種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("PORTAL_INFO_SYUBETU_NAME")]
        public string PortalInfoSyubetuName { get; set; }

        /// <summary>
        /// ポータル情報項目マスタID >> ポータル情報項目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("PORTAL_INFO_KOUMOKU_NAME")]
        public string PortalInfoKoumokuName { get; set; }

        /// <summary>
        /// ポータル情報区分マスタID >> ポータル情報区分名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_PORTAL_INFO_KUBUN_NAME")]
        public string PortalInfoKubunName { get; set; }

        /// <summary>
        /// 本船更新情報更新者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("HONSENKOUSHIN_INFO_USER_NAME")]
        public string HonsenkoushinInfoUserName { get; set; }
 
        #endregion

        public PtHonsenkoushinInfo()
        {
        }

        public static List<PtHonsenkoushinInfo> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtHonsenkoushinInfo), MethodBase.GetCurrentMethod());
            List<PtHonsenkoushinInfo> ret = new List<PtHonsenkoushinInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<PtHonsenkoushinInfo> mapping = new MappingBase<PtHonsenkoushinInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<PtHonsenkoushinInfo> GetRecordsByCondition(NBaseData.DAC.MsUser loginUser, PtHonsenkoushinInfoCondition condition)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtHonsenkoushinInfo), "GetRecords");

            // 期間
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtHonsenkoushinInfo), "ByEventDate");

            // 船
            if (condition.Vessel_list.Count > 0)
            {
                SQL += " and(";
                int num = 0;
                foreach (int vessel in condition.Vessel_list)
                {
                    if (num > 0)
                    {
                        SQL += " or ";
                    }
                    SQL += " PT_HONSENKOUSHIN_INFO.MS_VESSEL_ID = " + vessel;
                    num++;
                }
                SQL += ") ";
            }

            // 種別
            if (condition.Shubetu_list.Count > 0)
            {
                SQL += " and(";
                int num = 0;
                foreach (string shubetu in condition.Shubetu_list)
                {
                    if (num > 0)
                    {
                        SQL += " or ";
                    }
                    SQL += " MS_PORTAL_INFO_SHUBETU.PORTAL_INFO_SYUBETU_NAME = '" + shubetu + "'";
                    num++;
                }
                SQL += ") ";
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtHonsenkoushinInfo), "OrderByEventDateDesc");
            
            List<PtHonsenkoushinInfo> ret = new List<PtHonsenkoushinInfo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("EVENT_DATE_From", condition.EventDate_From));
            Params.Add(new DBParameter("EVENT_DATE_To", condition.EventDate_To));
            MappingBase<PtHonsenkoushinInfo> mapping = new MappingBase<PtHonsenkoushinInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<PtHonsenkoushinInfo> GetRecordsBySanshoumotoId(NBaseData.DAC.MsUser loginUser, string id)
        {
            return GetRecordsBySanshoumotoId(null, loginUser, id);
        }


        public static List<PtHonsenkoushinInfo> GetRecordsBySanshoumotoId(DBConnect dbConnect, MsUser loginUser, string id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtHonsenkoushinInfo), "GetRecords");
            SQL += SqlMapper.SqlMapper.GetSql(typeof(PtHonsenkoushinInfo), "BySanshoumotoId");

            List<PtHonsenkoushinInfo> ret = new List<PtHonsenkoushinInfo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SANSHOUMOTO_ID", id));
            MappingBase<PtHonsenkoushinInfo> mapping = new MappingBase<PtHonsenkoushinInfo>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtHonsenkoushinInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_HONSENKOUSHIN_INFO_ID", PtHonsenkoushinInfoId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_SHUBETU_ID", MsPortalInfoShubetuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KOUMOKU_ID", MsPortalInfoKoumokuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KUBUN_ID", MsPortalInfoKubunId));
            Params.Add(new DBParameter("SANSHOUMOTO_ID", SanshoumotoId));
            Params.Add(new DBParameter("EVENT_DATE", EventDate));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselId));
            Params.Add(new DBParameter("HONSENKOUSHIN_INFO_USER", HonsenkoushinInfoUser));
            Params.Add(new DBParameter("SHUBETSU", Shubetsu));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("KOUSHIN_NAIYOU", KoushinNaiyou));

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


        public bool UpdateRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtHonsenkoushinInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_HONSENKOUSHIN_INFO_ID", PtHonsenkoushinInfoId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_SHUBETU_ID", MsPortalInfoShubetuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KOUMOKU_ID", MsPortalInfoKoumokuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KUBUN_ID", MsPortalInfoKubunId));
            Params.Add(new DBParameter("SANSHOUMOTO_ID", SanshoumotoId));
            Params.Add(new DBParameter("EVENT_DATE", EventDate));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselId));
            Params.Add(new DBParameter("HONSENKOUSHIN_INFO_USER", HonsenkoushinInfoUser));
            Params.Add(new DBParameter("SHUBETSU", Shubetsu));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("KOUSHIN_NAIYOU", KoushinNaiyou));

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

        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(DBConnect dbcone, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", PtHonsenkoushinInfoId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbcone, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

    }

    [DataContract()]
    public class PtHonsenkoushinInfoCondition
    {
        /// <summary>
        /// 日付From
        /// </summary>
        [DataMember]
        public DateTime EventDate_From { get; set; }

        /// <summary>
        /// 日付To
        /// </summary>
        [DataMember]
        public DateTime EventDate_To { get; set; }

        /// <summary>
        /// 船
        /// </summary>
        [DataMember]
        public List<int> Vessel_list = new List<int>();

        /// <summary>
        /// 種別
        /// </summary>
        [DataMember]
        public List<string> Shubetu_list = new List<string>();

        public PtHonsenkoushinInfoCondition()
        {

        }
    }
}
