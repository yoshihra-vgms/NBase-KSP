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
    [TableAttribute("PT_DM_ALARM_INFO")]
    public class PtDmAlarmInfo : ISyncTableDoc
    {
        #region データメンバ
        /// <summary>
        /// アラーム情報ID(ドキュメント管理)
        /// </summary>
        [DataMember]
        [ColumnAttribute("PT_DM_ALARM_INFO_ID", true)]
        public string PtDmAlarmInfoID { get; set; }

        /// <summary>
        /// アラーム情報ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("PT_ALARM_INFO_ID")]
        public string PtAlarmInfoId { get; set; }
        
        /// <summary>
        /// 報告書ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_HOUKOKUSHO_ID")]
        public string MsDmHoukokushoID { get; set; }

        /// <summary>
        /// 時期（年）
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIKI_NEN")]
        public int JikiNen { get; set; }

        /// <summary>
        /// 時期（月）
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIKI_TUKI")]
        public int JikiTuki { get; set; }
        
        /// <summary>
        /// 公開先フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOUKAI_SAKI")]
        public int KoukaiSaki { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 部門ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BUMON_ID")]
        public string MsBumonID { get; set; }


        #region 共通項目
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

        #endregion

        public PtDmAlarmInfo()
        {
        }

        public static List<PtDmAlarmInfo> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), "OrderBy");
            List<PtDmAlarmInfo> ret = new List<PtDmAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<PtDmAlarmInfo> mapping = new MappingBase<PtDmAlarmInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<PtDmAlarmInfo> GetRecordsByHoukokushoID(NBaseData.DAC.MsUser loginUser, string houkokushoId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), "GetRecords");
            List<PtDmAlarmInfo> ret = new List<PtDmAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<PtDmAlarmInfo> mapping = new MappingBase<PtDmAlarmInfo>();
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), "ByHoukokushoID");
            Params.Add(new DBParameter("HOUKOKUSHO_ID", houkokushoId));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), "OrderBy");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static PtDmAlarmInfo GetRecord(NBaseData.DAC.MsUser loginUser, string houkokushoId, int jikiNen, int jikiTuki, int koukaiSaki)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), MethodBase.GetCurrentMethod());
            List<PtDmAlarmInfo> ret = new List<PtDmAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<PtDmAlarmInfo> mapping = new MappingBase<PtDmAlarmInfo>();
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", houkokushoId));
            // 2011.05.30: Update 1Line
            //Params.Add(new DBParameter("JIKI_NEN", jikiNen));
            if (jikiTuki < 4)
            {
                // 登録画面を "年度" としたので、1～3月は前年度としての表現なので、
                // アラームデータは、＋１した年で取得するように変更
                Params.Add(new DBParameter("JIKI_NEN", jikiNen + 1));
            }
            else
            {
                Params.Add(new DBParameter("JIKI_NEN", jikiNen));
            }
            // 2011.05.30: Update End
            Params.Add(new DBParameter("JIKI_TUKI", jikiTuki));
            Params.Add(new DBParameter("KOUKAI_SAKI", koukaiSaki));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static PtDmAlarmInfo GetRecord(NBaseData.DAC.MsUser loginUser, string houkokushoId, int jikiNen, int jikiTuki, int koukaiSaki, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), "ByKoukaiSakiVessel");
            List<PtDmAlarmInfo> ret = new List<PtDmAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<PtDmAlarmInfo> mapping = new MappingBase<PtDmAlarmInfo>();
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", houkokushoId));
            // 2011.05.30: Update 1Line
            //Params.Add(new DBParameter("JIKI_NEN", jikiNen));
            if (jikiTuki < 4)
            {
                // 登録画面を "年度" としたので、1～3月は前年度としての表現なので、
                // アラームデータは、＋１した年で取得するように変更
                Params.Add(new DBParameter("JIKI_NEN", jikiNen + 1));
            }
            else
            {
                Params.Add(new DBParameter("JIKI_NEN", jikiNen));
            }
            // 2011.05.30: Update End
            Params.Add(new DBParameter("JIKI_TUKI", jikiTuki));
            Params.Add(new DBParameter("KOUKAI_SAKI", koukaiSaki));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static PtDmAlarmInfo GetRecord(NBaseData.DAC.MsUser loginUser, string houkokushoId, int jikiNen, int jikiTuki, int koukaiSaki, string bumonId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), "ByKoukaiSakiBumon");
            List<PtDmAlarmInfo> ret = new List<PtDmAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<PtDmAlarmInfo> mapping = new MappingBase<PtDmAlarmInfo>();
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", houkokushoId));
            // 2011.05.30: Update 1Line
            //Params.Add(new DBParameter("JIKI_NEN", jikiNen));
            if (jikiTuki < 4)
            {
                // 登録画面を "年度" としたので、1～3月は前年度としての表現なので、
                // アラームデータは、＋１した年で取得するように変更
                Params.Add(new DBParameter("JIKI_NEN", jikiNen + 1));
            }
            else
            {
                Params.Add(new DBParameter("JIKI_NEN", jikiNen));
            }
            // 2011.05.30: Update End
            Params.Add(new DBParameter("JIKI_TUKI", jikiTuki));
            Params.Add(new DBParameter("KOUKAI_SAKI", koukaiSaki));
            Params.Add(new DBParameter("MS_BUMON_ID", bumonId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<PtDmAlarmInfo> GetSameRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string houkokushoId, int jikiNen, int jikiTuki, int koukaiSaki, int vesselId, string bumonId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), MethodBase.GetCurrentMethod());
            List<PtDmAlarmInfo> ret = new List<PtDmAlarmInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<PtDmAlarmInfo> mapping = new MappingBase<PtDmAlarmInfo>();
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", houkokushoId));
            Params.Add(new DBParameter("JIKI_NEN", jikiNen));
            Params.Add(new DBParameter("JIKI_TUKI", jikiTuki));
            Params.Add(new DBParameter("KOUKAI_SAKI", koukaiSaki));
            if (vesselId != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), "ByKoukaiSakiVessel");
                Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));
            }
            if (bumonId != null && bumonId.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), "ByKoukaiSakiBumon");
                Params.Add(new DBParameter("MS_BUMON_ID", bumonId));
            }
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

 
        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_DM_ALARM_INFO_ID", PtDmAlarmInfoID));
            Params.Add(new DBParameter("PT_ALARM_INFO_ID", PtAlarmInfoId));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", MsDmHoukokushoID));
            Params.Add(new DBParameter("JIKI_NEN", JikiNen));
            Params.Add(new DBParameter("JIKI_TUKI", JikiTuki));
            Params.Add(new DBParameter("KOUKAI_SAKI", KoukaiSaki));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_BUMON_ID", MsBumonID));
            
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

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }

        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_ALARM_INFO_ID", PtAlarmInfoId));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", MsDmHoukokushoID));
            Params.Add(new DBParameter("JIKI_NEN", JikiNen));
            Params.Add(new DBParameter("JIKI_TUKI", JikiTuki));
            Params.Add(new DBParameter("KOUKAI_SAKI", KoukaiSaki));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_BUMON_ID", MsBumonID));
            
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("PT_DM_ALARM_INFO_ID", PtDmAlarmInfoID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public static bool LogicalDelete(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string houkokushoId, int jikiNen, int jikiTuki)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtDmAlarmInfo), MethodBase.GetCurrentMethod());

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


        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", PtDmAlarmInfoID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



