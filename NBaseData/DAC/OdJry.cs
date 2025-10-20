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
using System.Diagnostics;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("OD_JRY")]
    public class OdJry : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 受領ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ID", true)]
        public string OdJryID { get; set; }

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
        /// 見積回答ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MK_ID")]
        public string OdMkID { get; set; }

        /// <summary>
        /// 受領日
        /// </summary>
        [DataMember]
        [ColumnAttribute("JRY_DATE")]
        public DateTime JryDate { get; set; }

        /// <summary>
        /// 受領伝票番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("JRY_NO")]
        public string JryNo { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("AMOUNT")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 値引き
        /// </summary>
        [DataMember]
        [ColumnAttribute("NEBIKI_AMOUNT")]
        public decimal NebikiAmount { get; set; }

        /// <summary>
        /// 消費税額
        /// </summary>
        [DataMember]
        [ColumnAttribute("TAX")]
        public decimal Tax { get; set; }

        /// <summary>
        /// 科目NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NO")]
        public string KamokuNo { get; set; }

        /// <summary>
        /// 内訳科目NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("UTIWAKE_KAMOKU_NO")]
        public string UtiwakeKamokuNo { get; set; }

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
        /// 送料・運搬料
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARRIAGE")]
        public decimal Carriage { get; set; }


        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_CUSTOMER_NAME")]
        public string MsCustomerCustomerName { get; set; }

        /// <summary>
        /// 納期
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MK_NOUKI")]
        public DateTime OdMkNouki { get; set; }

        /// <summary>
        /// 手配依頼日
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_THI_IRAI_DATE")]
        public DateTime OdThiThiIraiDate { get; set; }

        /// <summary>
        /// 手配依頼内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_NAIYOU")]
        public string OdThiNaiyou { get; set; }

        /// <summary>
        /// 手配依頼者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_USER_THI_USER_NAME")]
        public string MsUserThiUserName { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_MS_VESSEL_ID")]
        public int OdThiMsVesselID { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_MS_VESSEL_NAME")]
        public string OdThiVesselName { get; set; }

        /// <summary>
        /// 概算計上ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("GAISAN_KEIJO_ID")]
        public int GaisanKeijoID { get; set; }

        /// <summary>
        /// 概算計上の更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("GAISAN_KEIJO_DATE")]
        public DateTime GaisanKeijoDate { get; set; }

        /// <summary>
        /// [OD_MK_MS]顧客ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MK_MS_CUSTOMER_ID")]
        public string OdMk_MsCustomerID { get; set; }

        /// <summary>
        /// [MS_VESSE]船NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_VESSEL_NO")]
        public string MsVessel_VesselNo { get; set; }

        /// <summary>
        /// 手配依頼種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_SBT_ID")]
        public string MsThiIraiSbtID { get; set; }


        /// <summary>
        /// [OD_THIから]手配依頼詳細種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_MS_THI_IRAI_SHOUSAI_ID")]
        public string OdThi_MsThiIraiShousaiID { get; set; }
        
        /// <summary>
        /// [OD_THIから]事務担当者ID
        /// </summary>
        [ColumnAttribute("OD_THI_JIM_TANTOU_ID")]
        public string OdThi_JimTantouID { get; set; }

        // 2014.02 2013年度改造
        /// <summary>
        /// 手配ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_ID")]
        public string OdThiID { get; set; }

        #endregion

        [DataMember]
        public OdStatus OdStatusValue = null;

        //public static int NoLength受領 = 10;
        public static int NoLength受領 = 11;

        public enum STATUS { 未受領, 船受領, 受領承認依頼中, 受領承認済み };
        string[] statusStr = { "未受領", "船受領", "受領承認依頼中", "受領承認済み" };
        public string StatusName
        {
            get
            {
                return OdStatusValue.GetName(Status);
            }
        }

        /// <summary>
        /// 受領品目
        /// </summary>
        private List<OdJryItem> odJryItems;
        public List<OdJryItem> OdJryItems
        {
            get
            {
                if (odJryItems == null)
                {
                    odJryItems = new List<OdJryItem>();
                }

                return odJryItems;
            }
        }

        /// <summary>
        /// 概算金額
        /// </summary>
        public decimal GaisanAmount { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OdJry()
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

        public static List<OdJry> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJry), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "OrderByNo");
            List<OdJry> ret = new List<OdJry>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJry> mapping = new MappingBase<OdJry>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdJry> GetRecordsByVesselId(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJry), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "whereVesselId");

            // 2014.03
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "OrderByNo");

            List<OdJry> ret = new List<OdJry>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJry> mapping = new MappingBase<OdJry>();
            Params.Add(new DBParameter("VESSEL_ID", vesselId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdJry> GetRecordsByOdMkId(NBaseData.DAC.MsUser loginUser, string odMkId)
        {
            return GetRecordsByOdMkId(null, loginUser, odMkId);
        }
        public static List<OdJry> GetRecordsByOdMkId(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string odMkId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJry), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "whereOdMkId");

            List<OdJry> ret = new List<OdJry>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJry> mapping = new MappingBase<OdJry>();
            Params.Add(new DBParameter("OD_MK_ID", odMkId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdJry> GetRecordsByOdThiId(NBaseData.DAC.MsUser loginUser, string odThiId)
        {
            return GetRecordsByOdThiId(null, loginUser, odThiId);
        }
        public static List<OdJry> GetRecordsByOdThiId(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string odThiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJry), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "whereOdThiId");

            List<OdJry> ret = new List<OdJry>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJry> mapping = new MappingBase<OdJry>();
            Params.Add(new DBParameter("OD_THI_ID", odThiId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<OdJry> GetRecordsByOdMmId(NBaseData.DAC.MsUser loginUser, string odMmId)
        {
            return GetRecordsByOdMmId(null, loginUser, odMmId);
        }
        public static List<OdJry> GetRecordsByOdMmId(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string odMmId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJry), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "whereOdMmId");

            List<OdJry> ret = new List<OdJry>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJry> mapping = new MappingBase<OdJry>();
            Params.Add(new DBParameter("OD_MM_ID", odMmId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static OdJry GetRecord(NBaseData.DAC.MsUser loginUser, string OdJryID)
        {
            return GetRecord(null, loginUser, OdJryID);
        }
        public static OdJry GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdJryID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJry), MethodBase.GetCurrentMethod());

            List<OdJry> ret = new List<OdJry>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            MappingBase<OdJry> mapping = new MappingBase<OdJry>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<OdJry> GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, int status, OdThiFilter filter)
        {
            List<OdJry> ret = new List<OdJry>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJry> mapping = new MappingBase<OdJry>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJry), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            string CondStr = OdThi.GetConditionString(loginUser, filter, ref Params);
            SQL = SQL.Replace("AND OD_THI.CANCEL_FLAG = 0", "AND OD_THI.CANCEL_FLAG = 0 AND OD_THI.OD_THI_ID IN ( " + CondStr + " ) ");

            if (status != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "FilterByStatus");
                Params.Add(new DBParameter("STATUS", status));
            }

            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)//(Common.DBTYPE == Common.DB_TYPE.ORACLE)// 201508 Oracle >> Postgresql対応
            {
                SQL = "SELECT DISTINCT * FROM ( " + SQL + " ) as SubTBL_ODJRY " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "OrderByNo2");
            }

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdJry> GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, OdJryFilter filter)
        {
            List<OdJry> ret = new List<OdJry>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJry> mapping = new MappingBase<OdJry>();

            string SQL = "SELECT DISTINCT * FROM ( " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdJry), "AppendColumnForOdJryFilter"));
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdJry), "AppendJoinForOdJryFilter"));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "FilterByStatus");
            Params.Add(new DBParameter("STATUS", (int)STATUS.受領承認済み));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "FilterByGaisan");
            Params.Add(new DBParameter("MS_THI_IRAI_STATUS_ID", ((int)MsThiIraiStatus.THI_IRAI_STATUS.完了).ToString()));

            if (filter.YearMonth != null)
            {
                if (filter.YearMonthOnly == true)
                {
                    // 対象年月に受領している受領データ
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "FilterByYearMonthOnly");
                }
                else
                {
                    // 対象年月以前に受領している受領データ
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "FilterByYearMonth");
                }
                Params.Add(new DBParameter("YYYYMM", filter.YearMonth));
            }
            if (filter.MsVesselID != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "FilterByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselID));
            }
            if (filter.JimTantouID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "FilterByJimTantouID");
                Params.Add(new DBParameter("JIM_TANTOU_ID", filter.JimTantouID));
            }
            if (filter.MsThiIraiSbtID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "FilterByThiIraiSbtID");
                Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", filter.MsThiIraiSbtID));
            }
            if (filter.MsThiIraiShousaiID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "FilterByThiIraiShousaiID");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", filter.MsThiIraiShousaiID));
            }
            if (filter.MiKeijyo == true)
            {
                // 支払いデータなし
                // 対象年月の概算計上データなし
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "FilterByMiKeijyo");
                Params.Add(new DBParameter("GAISAN_KEIJO_ZUKI", filter.YearMonth));
            }
            else if (filter.KeijyoZumi == true)
            {
                // 支払いデータは（あってもなくても）関係なし
                // 対象年月の概算計上データあり
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "FilterByKeijyoZumi");
                Params.Add(new DBParameter("GAISAN_KEIJO_ZUKI", filter.YearMonth));
            }
            //SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "OrderByNo");
            //SQL += " )";
            SQL += " ) as SubTBL_ODJRY_GetRecordsByFilter " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "OrderByNo2");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdJry GetRecordsFor基幹連携(NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            List<OdJry> ret = new List<OdJry>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJry> mapping = new MappingBase<OdJry>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJry), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdJry), "AppendColumnForOdJryFilter2"));
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdJry), "AppendJoinForOdJryFilter2"));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJry), "whereOdJryId");
            Params.Add(new DBParameter("OD_JRY_ID", odJryID));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJry), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("OD_MK_ID", OdMkID));
            Params.Add(new DBParameter("JRY_DATE", JryDate));
            Params.Add(new DBParameter("JRY_NO", JryNo));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("NEBIKI_AMOUNT", NebikiAmount));
            Params.Add(new DBParameter("TAX", Tax));
            Params.Add(new DBParameter("KAMOKU_NO", KamokuNo));
            Params.Add(new DBParameter("UTIWAKE_KAMOKU_NO", UtiwakeKamokuNo));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            Params.Add(new DBParameter("CARRIAGE", Carriage));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJry), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("OD_MK_ID", OdMkID));
            Params.Add(new DBParameter("JRY_DATE", JryDate));
            Params.Add(new DBParameter("JRY_NO", JryNo));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("NEBIKI_AMOUNT", NebikiAmount));
            Params.Add(new DBParameter("TAX", Tax));
            Params.Add(new DBParameter("KAMOKU_NO", KamokuNo));
            Params.Add(new DBParameter("UTIWAKE_KAMOKU_NO", UtiwakeKamokuNo));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("CARRIAGE", Carriage));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));

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
        //    Params.Add(new DBParameter("PK", OdJryID));

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
            Params.Add(new DBParameter("PK", OdJryID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
