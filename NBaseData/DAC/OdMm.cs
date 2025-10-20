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
using NBaseData.DAC;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("OD_MM")]
    public class OdMm : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 見積依頼ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MM_ID", true)]
        public string OdMmID { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS")]
        public int Status { get; set; }

        /// <summary>
        /// 取消フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CANCEL_FLAG")]
        public int CancelFlag { get; set; }

        /// <summary>
        /// 手配依頼ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_ID")]
        public string OdThiID { get; set; }

        /// <summary>
        /// 見積依頼番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("MM_NO")]
        public string MmNo { get; set; }

        /// <summary>
        /// 見積依頼日
        /// </summary>
        [DataMember]
        [ColumnAttribute("MM_DATE")]
        public DateTime MmDate { get; set; }

        /// <summary>
        /// 支払条件ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SHR_JOUKEN_ID")]
        public string MsShrJoukenID { get; set; }

        /// <summary>
        /// 送り先
        /// </summary>
        [DataMember]
        [ColumnAttribute("OKURISAKI")]
        public string Okurisaki { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAIYOU")]
        public string Naiyou { get; set; }

        /// <summary>
        /// 見積依頼作成者
        /// </summary>
        [DataMember]
        [ColumnAttribute("MM_SAKUSEISHA")]
        public string MmSakuseisha { get; set; }

        /// <summary>
        /// 見積依頼作成者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MM_SAKUSEISHA_NAME")]
        public string MmSakuseishaName { get; set; }

        /// <summary>
        /// 入渠科目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_NYUKYO_KAMOKU_ID")]
        public string MsNyukyoKamokuID { get; set; }

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
        /// 見積期限
        /// </summary>
        [DataMember]
        [ColumnAttribute("MM_KIGEN")]
        public string MmKigen { get; set; }

        #endregion

        [DataMember]
        public OdStatus OdStatusValue = null;

        //public static int NoLength見積依頼 = 8;
        public static int NoLength見積依頼 = 9;

        public enum STATUS { 見積依頼, 見積依頼済 };
        string[] statusStr = { "見積依頼", "見積依頼済" };
        public string StatusName
        {
            get
            {
                return OdStatusValue.GetName(Status);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OdMm()
        {
            InitStatusValue();
        }

        private void InitStatusValue()
        {
            OdStatusValue = new OdStatus();
            OdStatusValue.DefaultValue = 0;
            OdStatusValue.Values = new List<OdStatus.StatusValue>();
            for (int i = 0; i < statusStr.Length; i++)
            {
                OdStatus.StatusValue value = new OdStatus.StatusValue();
                value.Value = i;
                value.Name = statusStr[i];
                OdStatusValue.Values.Add(value);
            }
        }

        public static List<OdMm> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMm), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMm), "OrderByNo");
            List<OdMm> ret = new List<OdMm>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMm> mapping = new MappingBase<OdMm>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //ユーザーマスタID
        public static List<OdMm> GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMm), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMm), "OrderByNo");
            List<OdMm> ret = new List<OdMm>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_USER_ID", ms_user_id));

            MappingBase<OdMm> mapping = new MappingBase<OdMm>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<OdMm> GetRecordsByOdThiID(NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            return GetRecordsByOdThiID(null, loginUser, OdThiID);
        }
        public static List<OdMm> GetRecordsByOdThiID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMm), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMm), "ByOdThiID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMm), "OrderByNo");

            List<OdMm> ret = new List<OdMm>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            MappingBase<OdMm> mapping = new MappingBase<OdMm>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdMm> GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, OdThiFilter filter)
        {
            return GetRecordsByFilter(null, loginUser, filter);
        }
        public static List<OdMm> GetRecordsByFilter(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, OdThiFilter filter)
        {
            List<OdMm> ret = new List<OdMm>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMm> mapping = new MappingBase<OdMm>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMm), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            string CondStr = OdThi.GetConditionString(loginUser, filter, ref Params);
            SQL = SQL.Replace( "AND OD_THI.CANCEL_FLAG = 0", "AND OD_THI.CANCEL_FLAG = 0 AND OD_THI.OD_THI_ID IN ( " + CondStr + " ) ");


            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)//(Common.DBTYPE == Common.DB_TYPE.ORACLE)// 201508 Oracle >> Postgresql対応
            {
                SQL = "SELECT DISTINCT * FROM ( " + SQL + " ) as SubTBL_ODMM " + SqlMapper.SqlMapper.GetSql(typeof(OdMm), "OrderByNo2");
            }

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static OdMm GetRecord(NBaseData.DAC.MsUser loginUser, string OdMmID)
        {
            return GetRecord(null, loginUser, OdMmID);
        }
        public static OdMm GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdMmID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMm), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMm), "ByOdMmID");

            List<OdMm> ret = new List<OdMm>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MM_ID", OdMmID));
            MappingBase<OdMm> mapping = new MappingBase<OdMm>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMm), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MM_ID", OdMmID));
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            Params.Add(new DBParameter("MM_NO", MmNo));
            Params.Add(new DBParameter("MM_DATE", MmDate));
            Params.Add(new DBParameter("MS_SHR_JOUKEN_ID", MsShrJoukenID));
            Params.Add(new DBParameter("OKURISAKI", Okurisaki));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("MM_SAKUSEISHA", MmSakuseisha));
            Params.Add(new DBParameter("MS_NYUKYO_KAMOKU_ID", MsNyukyoKamokuID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MM_KIGEN", MmKigen));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMm), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            Params.Add(new DBParameter("MM_NO", MmNo));
            Params.Add(new DBParameter("MM_DATE", MmDate));
            Params.Add(new DBParameter("MS_SHR_JOUKEN_ID", MsShrJoukenID));
            Params.Add(new DBParameter("OKURISAKI", Okurisaki));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("MM_SAKUSEISHA", MmSakuseisha));
            Params.Add(new DBParameter("MS_NYUKYO_KAMOKU_ID", MsNyukyoKamokuID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("MM_KIGEN", MmKigen));

            Params.Add(new DBParameter("OD_MM_ID", OdMmID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool DeleteRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMm), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MM_ID", OdMmID));
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
        //    Params.Add(new DBParameter("PK", OdMmID));

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
            Params.Add(new DBParameter("PK", OdMmID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
