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
    [TableAttribute("PT_ALARM_INFO")]
    public class PtAlarmInfo : ISyncTableDoc
    {
        public enum CancelFlagEnum { ＯＦＦ, ＯＮ };
        public enum AlarmShowFlagEnum { アラームＯＮ, アラームＯＦＦ };

        #region データメンバ

    //PT_ALARM_INFO_ID               NVARCHAR2(40) NOT NULL,
    //MS_PORTAL_INFO_SHUBETU_ID      NVARCHAR2(40),
    //MS_PORTAL_INFO_KOUMOKU_ID      NVARCHAR2(40),
    //MS_PORTAL_INFO_KUBUN_ID        NVARCHAR2(40),
    //SANSHOUMOTO_ID                 NVARCHAR2(40),
    //HAASEI_DATE                    DATE,
    //MS_VESSEL_ID                   NUMBER(4,0) NOT NULL,
    //YUUKOUKIGEN                    DATE,
    //NAIYOU                         NVARCHAR2(200),
    //SHOUSAI                        NVARCHAR2(200),
    //ALARM_SHOW_FLAG                NUMBER(1,0),
    //ALARM_STOP_USER                NVARCHAR2(40),
    //ALARM_STOP_DATE                DATE,
    //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
    //SEND_FLAG                      NUMBER(1,0) NOT NULL,
    //VESSEL_ID                      NUMBER(4,0) NOT NULL,
    //DATA_NO                        NUMBER(13,0),
    //USER_KEY                       VARCHAR2(40),
    //RENEW_DATE                     DATE NOT NULL,
    //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
    //TS                             VARCHAR2(20),

        /// <summary>
        /// アラーム情報ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("PT_ALARM_INFO_ID", true)]
        public string PtAlarmInfoId { get; set; }

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
        /// 発生日
        /// </summary>
        [DataMember]
        [ColumnAttribute("HAASEI_DATE")]
        public DateTime HasseiDate { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselId { get; set; }

        /// <summary>
        /// 有効期限
        /// </summary>
        [DataMember]
        [ColumnAttribute("YUUKOUKIGEN")]
        public DateTime Yuukoukigen { get; set; }

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
        /// アラーム表示フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALARM_SHOW_FLAG")]
        public int AlarmShowFlag { get; set; }

        /// <summary>
        /// アラーム削除者
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALARM_STOP_USER")]
        public string AlarmStopUser { get; set; }

        /// <summary>
        /// アラーム削除日
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALARM_STOP_DATE")]
        public DateTime AlarmStopDate { get; set; }

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

        #endregion

        public PtAlarmInfo()
        {
        }

        #region public static List<PtAlarmInfo> GetRecords(NBaseData.DAC.MsUser loginUser)
        public static List<PtAlarmInfo> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), MethodBase.GetCurrentMethod());
            List<PtAlarmInfo> ret = new List<PtAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<PtAlarmInfo> mapping = new MappingBase<PtAlarmInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        #endregion

        public static PtAlarmInfo GetRecordsByPtAlarmInfoId(NBaseData.DAC.MsUser loginUser, string id)
        {
            return GetRecordsByPtAlarmInfoId(null, loginUser, id);
        }

        #region public static PtAlarmInfo GetRecordsByPtAlarmInfoId(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string id)
        public static PtAlarmInfo GetRecordsByPtAlarmInfoId(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), "ByPtAlarmInfoId");

            List<PtAlarmInfo> ret = new List<PtAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_ALARM_INFO_ID", id));
            MappingBase<PtAlarmInfo> mapping = new MappingBase<PtAlarmInfo>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
        #endregion

        #region public static PtAlarmInfo GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(NBaseData.DAC.MsUser loginUser, string SanshoumotoId, string shubetu, string koumoku, string kubun)
        public static PtAlarmInfo GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(NBaseData.DAC.MsUser loginUser, string SanshoumotoId, string shubetu, string koumoku, string kubun)
        {
            return GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(null, loginUser, SanshoumotoId, shubetu, koumoku, kubun);
        }
        public static PtAlarmInfo GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string SanshoumotoId, string shubetu, string koumoku, string kubun)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), "BySanshoumotoId");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), "_shubetu_koumoku_kubun");

            List<PtAlarmInfo> ret = new List<PtAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SANSHOUMOTO_ID", SanshoumotoId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_SHUBETU_ID", shubetu));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KOUMOKU_ID", koumoku));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KUBUN_ID", kubun));
            MappingBase<PtAlarmInfo> mapping = new MappingBase<PtAlarmInfo>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
        #endregion

        #region public static List<PtAlarmInfo> GetRecordsBySanshoumotoId(NBaseData.DAC.MsUser loginUser, string id)
        public static List<PtAlarmInfo> GetRecordsBySanshoumotoId(NBaseData.DAC.MsUser loginUser, string id)
        {
            return GetRecordsBySanshoumotoId(null, loginUser, id);
        }

        public static List<PtAlarmInfo> GetRecordsBySanshoumotoId(DBConnect dbConnect, MsUser loginUser, string id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), "GetRecords");
            SQL += SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), "BySanshoumotoId");

            List<PtAlarmInfo> ret = new List<PtAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SANSHOUMOTO_ID", id));
            MappingBase<PtAlarmInfo> mapping = new MappingBase<PtAlarmInfo>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }
        #endregion

        #region public static List<PtAlarmInfo> GetRecordByCondition(NBaseData.DAC.MsUser loginUser, PtAlarmInfoCondition condition)
        public static List<PtAlarmInfo> GetRecordByCondition(NBaseData.DAC.MsUser loginUser, PtAlarmInfoCondition condition)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), "GetRecords");

            // 期間
            SQL += SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), "ByHaaseiDate");

            // 種別
            // 2010.03.04:aki 種別はなにかチェックされていないとダメなので、上位で確認している
            //if (condition.Shubetu_list.Count > 0)
            //{
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
            //}

            // 船
            if (condition.Vessel_list.Count == 0)
            {
                SQL += " and NOT EXISTS (SELECT MS_VESSEL.MS_VESSEL_ID FROM MS_VESSEL WHERE MS_VESSEL.MS_VESSEL_ID = PT_ALARM_INFO.MS_VESSEL_ID) ";
            }
            else
            {
                SQL += " and((";
                num = 0;
                foreach (int vessel in condition.Vessel_list)
                {
                    if (num > 0)
                    {
                        SQL += " or ";
                    }
                    SQL += " PT_ALARM_INFO.MS_VESSEL_ID = " + vessel;
                    num++;
                }
                SQL += ") ";

                if (condition.Shubetu_list.Contains(MsPortalInfoShubetu.船員) || condition.Shubetu_list.Contains(MsPortalInfoShubetu.文書))
                {
                    SQL += " or NOT EXISTS (SELECT MS_VESSEL.MS_VESSEL_ID FROM MS_VESSEL WHERE MS_VESSEL.MS_VESSEL_ID = PT_ALARM_INFO.MS_VESSEL_ID)) ";
                }
                else
                {
                    SQL += " )";
                }
            }

            // 2010.06.29 有効期限はすべてのデータにないので、発生日順、船の表示順、アラーム種別順
            //SQL += " order by PT_ALARM_INFO.YUUKOUKIGEN";
            SQL += " order by PT_ALARM_INFO.HAASEI_DATE, MS_VESSEL.SHOW_ORDER, PT_ALARM_INFO.MS_PORTAL_INFO_SHUBETU_ID";

            List<PtAlarmInfo> ret = new List<PtAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("HAASEI_DATE", condition.HasseiDate));
            MappingBase<PtAlarmInfo> mapping = new MappingBase<PtAlarmInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        #endregion


        // 2011.07.25
        public static List<PtAlarmInfo> GetSameRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), "GetSameRecords");

            List<PtAlarmInfo> ret = new List<PtAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_ALARM_INFO_ID", id));
            MappingBase<PtAlarmInfo> mapping = new MappingBase<PtAlarmInfo>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        #region public bool InsertRecord()
        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_ALARM_INFO_ID", PtAlarmInfoId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_SHUBETU_ID", MsPortalInfoShubetuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KOUMOKU_ID", MsPortalInfoKoumokuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KUBUN_ID", MsPortalInfoKubunId));
            Params.Add(new DBParameter("SANSHOUMOTO_ID", SanshoumotoId));
            Params.Add(new DBParameter("HAASEI_DATE", HasseiDate));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselId));
            Params.Add(new DBParameter("YUUKOUKIGEN", Yuukoukigen));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("SHOUSAI", Shousai));
            Params.Add(new DBParameter("ALARM_SHOW_FLAG", AlarmShowFlag));
            Params.Add(new DBParameter("ALARM_STOP_USER", AlarmStopUser));
            Params.Add(new DBParameter("ALARM_STOP_DATE", AlarmStopDate));

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
        #endregion

        #region public bool UpdateRecord()
        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_ALARM_INFO_ID", PtAlarmInfoId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_SHUBETU_ID", MsPortalInfoShubetuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KOUMOKU_ID", MsPortalInfoKoumokuId));
            Params.Add(new DBParameter("MS_PORTAL_INFO_KUBUN_ID", MsPortalInfoKubunId));
            Params.Add(new DBParameter("SANSHOUMOTO_ID", SanshoumotoId));
            Params.Add(new DBParameter("HAASEI_DATE", HasseiDate));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselId));
            Params.Add(new DBParameter("YUUKOUKIGEN", Yuukoukigen));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("SHOUSAI", Shousai));
            Params.Add(new DBParameter("ALARM_SHOW_FLAG", AlarmShowFlag));
            Params.Add(new DBParameter("ALARM_STOP_USER", AlarmStopUser));
            Params.Add(new DBParameter("ALARM_STOP_DATE", AlarmStopDate));

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
        #endregion

        #region public static bool LogicalDelete(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string houkokushoId, int jikiNen, int jikiTuki)
        public static bool LogicalDelete(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string houkokushoId, int jikiNen, int jikiTuki)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtAlarmInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", houkokushoId));
            Params.Add(new DBParameter("JIKI_NEN", jikiNen));
            Params.Add(new DBParameter("JIKI_TUKI", jikiTuki));
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
        #endregion

        
        
        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", PtAlarmInfoId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return PtAlarmInfoId == null;
        }
    }

    [DataContract()]
    public class PtAlarmInfoCondition
    {
        /// <summary>
        /// 発生日
        /// </summary>
        [DataMember]
        public DateTime HasseiDate { get; set; }

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

        public PtAlarmInfoCondition()
        {

        }
    }
}
