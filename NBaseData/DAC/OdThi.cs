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
    [TableAttribute("OD_THI")]
    public class OdThi : ISyncTable, IGenericCloneable<OdThi>
    {
        #region データメンバ

        /// <summary>
        /// 手配依頼ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_ID", true)]
        public string OdThiID { get; set; }

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
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 手配依頼日
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_IRAI_DATE")]
        public DateTime ThiIraiDate { get; set; }

        /// <summary>
        /// 手配依頼種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_SBT_ID")]
        public string MsThiIraiSbtID { get; set; }

        /// <summary>
        /// 手配依頼詳細種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_SHOUSAI_ID")]
        public string MsThiIraiShousaiID { get; set; }

        /// <summary>
        /// 場所
        /// </summary>
        [DataMember]
        [ColumnAttribute("BASHO")]
        public string Basho { get; set; }

        /// <summary>
        /// 手配依頼内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAIYOU")]
        public string Naiyou { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 手配依頼番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("TEHAI_IRAI_NO")]
        public string TehaiIraiNo { get; set; }

        /// <summary>
        /// 納品希望日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIBOUBI")]
        public DateTime Kiboubi { get; set; }

        /// <summary>
        /// 納品希望港
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIBOUKOU")]
        public string Kiboukou { get; set; }

        /// <summary>
        /// 手配依頼者ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_USER_ID")]
        public string ThiUserID { get; set; }

        /// <summary>
        /// 事務担当者ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIM_TANTOU_ID")]
        public string JimTantouID { get; set; }

        /// <summary>
        /// 手配依頼ステータスID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_STATUS_ID")]
        public string MsThiIraiStatusID { get; set; }

        /// <summary>
        /// 見積フラグ  0:あり(する)　1:なし(しない)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MM_FLAG")]
        public int MmFlag { get; set; }

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
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NAME")]
        public string VesselName { get; set; }

        /// <summary>
        /// 手配依頼ステータス名
        /// </summary>
        [DataMember]
        [ColumnAttribute("ORDER_THI_IRAI_STATUS")]
        public string OrderThiIraiStatus { get; set; }

        /// <summary>
        /// 手配依頼種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_IRAI_SBT_NAME")]
        public string ThiIraiSbtName { get; set; }

        /// <summary>
        /// 依頼詳細名
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_IRAI_SHOUSAI_NAME")]
        public string ThiIraiShousaiName { get; set; }

        /// <summary>
        /// 手配依頼者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_USER_NAME")]
        public string ThiUserName { get; set; }

        /// <summary>
        /// 事務担当者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIM_TANTOU_NAME")]
        public string JimTantouName { get; set; }
        #endregion

        [DataMember]
        public readonly OdStatus OdStatusValue = new OdStatus();
        
        [DataMember]
        public bool MailSend { get; set; }

        //public static int NoLength手配依頼 = 7;
        public static int NoLength手配依頼 = 8;

        public enum STATUS { 船未手配, 事務所未手配, 手配依頼済 };
        public static readonly string[] statusStr = { "船未手配", "事務所未手配", "手配依頼済" };
        public string StatusName
        {
            get
            {
                return OdStatusValue.GetName(Status);
            }
        }
        public enum MM_FLAG { あり, なし };

        /// <summary>
        /// 手配依頼品目
        /// </summary>
        private List<OdThiItem> odThiItems;
        public List<OdThiItem> OdThiItems
        {
            get
            {
                if (odThiItems == null)
                {
                    odThiItems = new List<OdThiItem>();
                }

                return odThiItems;
            }
        }




        public string OrderStatusStr { get; set; }
        public string StatusStr { get; set; }
        public string SbtName { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OdThi()
        {
            MmFlag = (int)MM_FLAG.あり;
            MailSend = false;

            InitStatusValue();
        }

        private void InitStatusValue()
        {
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

        public static List<OdThi> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            List<OdThi> ret = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //ユーザーマスタID
        public static List<OdThi> GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            List<OdThi> ret = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_USER_ID", ms_user_id));

            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdThi> GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, OdThiFilter filter)
        {
            return GetRecordsByFilter(null, loginUser, filter);
        }
        public static List<OdThi> GetRecordsByFilter(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, OdThiFilter filter)
        {
            List<OdThi> ret = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThi> mapping = new MappingBase<OdThi>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), "GetRecords");
            if (filter.MsVesselID != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselID));
            }
            if (filter.JimTantouID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByJimTantouID");
                Params.Add(new DBParameter("JIM_TANTOU_ID", filter.JimTantouID));
            }
            if (filter.MsThiIraiSbtID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByThiIraiSbtID");
                Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", filter.MsThiIraiSbtID));
            }
            if (filter.MsThiIraiShousaiID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByThiIraiShousaiID");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", filter.MsThiIraiShousaiID));
            }
            if (filter.ThiIraiDateFrom != null && filter.ThiIraiDateFrom != DateTime.MinValue &&
                filter.ThiIraiDateTo != null && filter.ThiIraiDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByThiIraiDateFromTo");
                Params.Add(new DBParameter("THI_IRAI_DATE_FROM", filter.ThiIraiDateFrom.ToShortDateString()));
                Params.Add(new DBParameter("THI_IRAI_DATE_TO", filter.ThiIraiDateTo.ToShortDateString()));
            }
            else if (filter.ThiIraiDateFrom != null && filter.ThiIraiDateFrom != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByThiIraiDateFrom");
                Params.Add(new DBParameter("THI_IRAI_DATE_FROM", filter.ThiIraiDateFrom.ToShortDateString()));
            }
            else if (filter.ThiIraiDateTo != null && filter.ThiIraiDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByThiIraiDateTo");
                Params.Add(new DBParameter("THI_IRAI_DATE_TO", filter.ThiIraiDateTo.ToShortDateString()));
            }

            if ((filter.JryDateFrom != null && filter.JryDateFrom != DateTime.MinValue) ||
                (filter.JryDateTo != null && filter.JryDateTo != DateTime.MaxValue) ||
                (filter.JryStatus > int.MinValue))
            {
                SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdThi), "AppendJoinForOdThiFilter2"));
            }
            else if ((filter.HachuDateFrom != null && filter.HachuDateFrom != DateTime.MinValue) ||
                     (filter.HachuDateTo != null && filter.HachuDateTo != DateTime.MaxValue))
            {
                SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdThi), "AppendJoinForOdThiFilter1"));
            }
            else if (filter.Nendo != null) 
            {
                SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdThi), "AppendJoinForOdThiFilter1"));
            }
            else
            {
                SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            }
            if (filter.JryDateFrom != null && filter.JryDateFrom != DateTime.MinValue &&
                filter.JryDateTo != null && filter.JryDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByJryDateFromTo");
                Params.Add(new DBParameter("JRY_DATE_FROM", filter.JryDateFrom.ToShortDateString()));
                Params.Add(new DBParameter("JRY_DATE_TO", filter.JryDateTo.ToShortDateString()));
            }
            else if (filter.JryDateFrom != null && filter.JryDateFrom != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByJryDateFrom");
                Params.Add(new DBParameter("JRY_DATE_FROM", filter.JryDateFrom.ToShortDateString()));
            }
            else if (filter.JryDateTo != null && filter.JryDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByJryDateTo");
                Params.Add(new DBParameter("JRY_DATE_TO", filter.JryDateTo.ToShortDateString()));
            }
            if (filter.HachuDateFrom != null && filter.HachuDateFrom != DateTime.MinValue &&
                filter.HachuDateTo != null && filter.HachuDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByHachuDateFromTo");
                Params.Add(new DBParameter("HACHU_DATE_FROM", filter.HachuDateFrom.ToShortDateString()));
                Params.Add(new DBParameter("HACHU_DATE_TO", filter.HachuDateTo.ToShortDateString()));
            }
            else if (filter.HachuDateFrom != null && filter.HachuDateFrom != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByHachuDateFrom");
                Params.Add(new DBParameter("HACHU_DATE_FROM", filter.HachuDateFrom.ToShortDateString()));
            }
            else if (filter.HachuDateTo != null && filter.HachuDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByHachuDateTo");
                Params.Add(new DBParameter("HACHU_DATE_TO", filter.HachuDateTo.ToShortDateString()));
            }
            else if (filter.Nendo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByNendo");
                Params.Add(new DBParameter("NENDO", filter.Nendo));
            }

            if (filter.MsThiIraiStatusIDs.Count > 0 || filter.JryStatus > int.MinValue)
            {
                SQL += " AND ( ";
                if (filter.MsThiIraiStatusIDs.Count > 0)
                {
                    SQL += SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByMsThiIraiStatusID");
                    string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", filter.MsThiIraiStatusIDs.Count);
                    SQL = SQL.Replace("#INNER_SQL_MS_THI_IRAI_STATUS_IDS#", innerSQLStr);
                    Params.AddInnerParams("p", filter.MsThiIraiStatusIDs.ToArray());
                }
                if (filter.JryStatus > int.MinValue)
                {
                    if (filter.MsThiIraiStatusIDs.Count > 0)
                    {
                        SQL += " OR ";
                    }
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByJryStatus");
                    Params.Add(new DBParameter("JRY_STATUS", filter.JryStatus));
                }
                SQL += " ) ";
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByOrder");

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static string GetConditionString(NBaseData.DAC.MsUser loginUser, OdThiFilter filter, ref ParameterConnection Params)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), "GetOdThiIds");
            if (filter.MsVesselID != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselID));
            }
            if (filter.JimTantouID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByJimTantouID");
                Params.Add(new DBParameter("JIM_TANTOU_ID", filter.JimTantouID));
            }
            if (filter.MsThiIraiSbtID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByThiIraiSbtID");
                Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", filter.MsThiIraiSbtID));
            }
            if (filter.MsThiIraiShousaiID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByThiIraiShousaiID");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", filter.MsThiIraiShousaiID));
            }
            if (filter.ThiIraiDateFrom != null && filter.ThiIraiDateFrom != DateTime.MinValue &&
                filter.ThiIraiDateTo != null && filter.ThiIraiDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByThiIraiDateFromTo");
                Params.Add(new DBParameter("THI_IRAI_DATE_FROM", filter.ThiIraiDateFrom.ToShortDateString()));
                Params.Add(new DBParameter("THI_IRAI_DATE_TO", filter.ThiIraiDateTo.ToShortDateString()));
            }
            else if (filter.ThiIraiDateFrom != null && filter.ThiIraiDateFrom != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByThiIraiDateFrom");
                Params.Add(new DBParameter("THI_IRAI_DATE_FROM", filter.ThiIraiDateFrom.ToShortDateString()));
            }
            else if (filter.ThiIraiDateTo != null && filter.ThiIraiDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByThiIraiDateTo");
                Params.Add(new DBParameter("THI_IRAI_DATE_TO", filter.ThiIraiDateTo.ToShortDateString()));
            }

            if ((filter.JryDateFrom != null && filter.JryDateFrom != DateTime.MinValue) ||
                (filter.JryDateTo != null && filter.JryDateTo != DateTime.MaxValue) ||
                (filter.JryStatus > int.MinValue))
            {
                SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdThi), "AppendJoinForOdThiFilter2"));
            }
            else if ((filter.HachuDateFrom != null && filter.HachuDateFrom != DateTime.MinValue) ||
                     (filter.HachuDateTo != null && filter.HachuDateTo != DateTime.MaxValue) ||
                     (filter.Nendo != null))
            {
                SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdThi), "AppendJoinForOdThiFilter1"));
            }
            else
            {
                SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            }
            if (filter.JryDateFrom != null && filter.JryDateFrom != DateTime.MinValue &&
                filter.JryDateTo != null && filter.JryDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByJryDateFromTo");
                Params.Add(new DBParameter("JRY_DATE_FROM", filter.JryDateFrom.ToShortDateString()));
                Params.Add(new DBParameter("JRY_DATE_TO", filter.JryDateTo.ToShortDateString()));
            }
            else if (filter.JryDateFrom != null && filter.JryDateFrom != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByJryDateFrom");
                Params.Add(new DBParameter("JRY_DATE_FROM", filter.JryDateFrom.ToShortDateString()));
            }
            else if (filter.JryDateTo != null && filter.JryDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByJryDateTo");
                Params.Add(new DBParameter("JRY_DATE_TO", filter.JryDateTo.ToShortDateString()));
            }
            if (filter.HachuDateFrom != null && filter.HachuDateFrom != DateTime.MinValue &&
                filter.HachuDateTo != null && filter.HachuDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByHachuDateFromTo");
                Params.Add(new DBParameter("HACHU_DATE_FROM", filter.HachuDateFrom.ToShortDateString()));
                Params.Add(new DBParameter("HACHU_DATE_TO", filter.HachuDateTo.ToShortDateString()));
            }
            else if (filter.HachuDateFrom != null && filter.HachuDateFrom != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByHachuDateFrom");
                Params.Add(new DBParameter("HACHU_DATE_FROM", filter.HachuDateFrom.ToShortDateString()));
            }
            else if (filter.HachuDateTo != null && filter.HachuDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByHachuDateTo");
                Params.Add(new DBParameter("HACHU_DATE_TO", filter.HachuDateTo.ToShortDateString()));
            }
            else if (filter.Nendo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByNendo");
                Params.Add(new DBParameter("NENDO", filter.Nendo));
            }

            if (filter.MsThiIraiStatusIDs.Count > 0 || filter.JryStatus > int.MinValue)
            {
                SQL += " AND ( ";
                if (filter.MsThiIraiStatusIDs.Count > 0)
                {
                    SQL += SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByMsThiIraiStatusID");
                    string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", filter.MsThiIraiStatusIDs.Count);
                    SQL = SQL.Replace("#INNER_SQL_MS_THI_IRAI_STATUS_IDS#", innerSQLStr);
                    Params.AddInnerParams("p", filter.MsThiIraiStatusIDs.ToArray());
                }
                if (filter.JryStatus > int.MinValue)
                {
                    if (filter.MsThiIraiStatusIDs.Count > 0)
                    {
                        SQL += " OR ";
                    }
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "FilterByJryStatus");
                    Params.Add(new DBParameter("JRY_STATUS", filter.JryStatus));
                }
                SQL += " ) ";
            }

            return SQL;
        }


        public static List<OdThi> GetRecordsByVesselId(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "whereVesselId");

            List<OdThi> ret = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            Params.Add(new DBParameter("VESSEL_ID", vesselId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdThi GetLatestNyukyoRecord(NBaseData.DAC.MsUser loginUser, int msVesselID, string msThiIraiShousaiId, DateTime toDate)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "ByLatestNyukyoRecord");

            List<OdThi> ret = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", msThiIraiShousaiId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("THI_IRAI_DATE_TO", toDate.ToShortDateString()));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<OdThi> GetRecordsByMsThiIraiShousaiId(NBaseData.DAC.MsUser loginUser, int msVesselID, string msThiIraiShousaiId, DateTime fromDate, DateTime toDate)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "whereMsThiIraiShousaiId");

            List<OdThi> ret = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", msThiIraiShousaiId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("THI_IRAI_DATE_FROM", fromDate.ToShortDateString()));
            Params.Add(new DBParameter("THI_IRAI_DATE_TO", toDate.ToShortDateString()));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdThi GetRecord(NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            return GetRecord(null, loginUser, OdThiID);
        }
        public static OdThi GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThi), "ByOdThiID");

            List<OdThi> ret = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static OdThi GetRecordByOdMkID(NBaseData.DAC.MsUser loginUser, string odMkID)
        {
            return GetRecordByOdMkID(null, loginUser, odMkID);
        }
        public static OdThi GetRecordByOdMkID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string odMkID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdThi),"AppendJoinByOdMkID"));

            List<OdThi> ret = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            Params.Add(new DBParameter("OD_MK_ID", odMkID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
        public static OdThi GetRecordByOdJryID(NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            return GetRecordByOdJryID(null, loginUser, odJryID);
        }
        public static OdThi GetRecordByOdJryID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdThi), "AppendJoinByOdJryID"));

            List<OdThi> ret = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            Params.Add(new DBParameter("OD_JRY_ID", odJryID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static OdThi GetRecordByOdShrID(NBaseData.DAC.MsUser loginUser, string odShrID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdThi), "AppendJoinByOdShrID"));

            List<OdThi> ret = new List<OdThi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThi> mapping = new MappingBase<OdThi>();
            Params.Add(new DBParameter("OD_SHR_ID", odShrID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), MethodBase.GetCurrentMethod());

            // 船で生成されたレコードに手配依頼番号を採番する.
            if (Status == (int)STATUS.手配依頼済)
            {
                if ((TehaiIraiNo == null || TehaiIraiNo == string.Empty) && ORMapping.Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)// == Common.DB_TYPE.ORACLE)// 201508 Oracle >> Postgresql対応
                {
                    TehaiIraiNo = CreateTehaiIraiNo(dbConnect, loginUser);
                }
            }
            
            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("THI_IRAI_DATE", ThiIraiDate));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbtID));
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", MsThiIraiShousaiID));
            Params.Add(new DBParameter("BASHO", Basho));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("TEHAI_IRAI_NO", TehaiIraiNo));
            Params.Add(new DBParameter("KIBOUBI", Kiboubi));
            Params.Add(new DBParameter("KIBOUKOU", Kiboukou));
            Params.Add(new DBParameter("THI_USER_ID", ThiUserID));
            Params.Add(new DBParameter("JIM_TANTOU_ID", JimTantouID));
            Params.Add(new DBParameter("MS_THI_IRAI_STATUS_ID", MsThiIraiStatusID));
            Params.Add(new DBParameter("MM_FLAG", MmFlag));

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

        private string CreateTehaiIraiNo(DBConnect dbConnect, MsUser loginUser)
        {
            string prefix = 手配依頼番号を振る();
            if (prefix.Length == 0)
                return "";

            //NBaseData.BLC.GetMaxNo maxNo = NBaseData.BLC.GetMaxNo.GetMaxTehaiIraiNo(dbConnect, loginUser, NBaseData.DAC.OdThi.NoLength手配依頼 - 1, prefix);
            NBaseData.BLC.GetMaxNo maxNo = NBaseData.BLC.GetMaxNo.GetMaxTehaiIraiNo(dbConnect, loginUser, NBaseData.DAC.OdThi.NoLength手配依頼 - 2, prefix);
            string currentMaxNo = maxNo.MaxNo.Replace(prefix, "");
            int currentMaxNoDec = NBaseData.DS.NoConvert.NToInt(currentMaxNo, 36);
            string nextMaxNo = NBaseData.DS.NoConvert.IntToN(currentMaxNoDec + 1, 36);

            if (nextMaxNo.Length == 1)
            {
                nextMaxNo = "0" + nextMaxNo;
            }

            return prefix += nextMaxNo;
        }
        
        private string 手配依頼番号を振る()
        {
            string iraiNo = "";

            if (ThiIraiDate != null && ThiIraiDate != DateTime.MinValue)
            {
                iraiNo += ThiIraiDate.Year.ToString().Substring(2); // 年は下２桁
                iraiNo += ThiIraiDate.Month.ToString("00");
                iraiNo += ThiIraiDate.Day.ToString("00");
                // 最後のシーケンス番号は、NBaseService.BLC のインサート時に
            }
            return iraiNo;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }
        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), MethodBase.GetCurrentMethod());

            // 船で生成されたレコードに手配依頼番号を採番する.
            if (Status == (int)STATUS.手配依頼済)
            {
                if ((TehaiIraiNo == null || TehaiIraiNo == string.Empty) && ORMapping.Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)//== Common.DB_TYPE.ORACLE)// 201508 Oracle >> Postgresql対応
                {
                    TehaiIraiNo = CreateTehaiIraiNo(dbConnect, loginUser);
                }
            }

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("THI_IRAI_DATE", ThiIraiDate));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbtID));
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", MsThiIraiShousaiID));
            Params.Add(new DBParameter("BASHO", Basho));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("TEHAI_IRAI_NO", TehaiIraiNo));
            Params.Add(new DBParameter("KIBOUBI", Kiboubi));
            Params.Add(new DBParameter("KIBOUKOU", Kiboukou));
            Params.Add(new DBParameter("THI_USER_ID", ThiUserID));
            Params.Add(new DBParameter("JIM_TANTOU_ID", JimTantouID));
            Params.Add(new DBParameter("MS_THI_IRAI_STATUS_ID", MsThiIraiStatusID));
            Params.Add(new DBParameter("MM_FLAG", MmFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool DeleteRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThi), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
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
        //    Params.Add(new DBParameter("PK", OdThiID));

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
            Params.Add(new DBParameter("PK", OdThiID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }


        public string GetId()
        {
            return OdThiID;
        }
        #endregion

        #region IGenericCloneable<OdThi> メンバ

        public OdThi Clone()
        {
            OdThi clone = new OdThi();

            clone.OdThiID = OdThiID;
            clone.Status = Status;
            clone.CancelFlag = CancelFlag;
            clone.MsVesselID = MsVesselID;
            clone.ThiIraiDate = ThiIraiDate;
            clone.MsThiIraiSbtID = MsThiIraiSbtID;
            clone.MsThiIraiShousaiID = MsThiIraiShousaiID;
            clone.Basho = Basho;
            clone.Naiyou = Naiyou;
            clone.Bikou = Bikou;
            clone.TehaiIraiNo = TehaiIraiNo;
            clone.Kiboubi = Kiboubi;
            clone.Kiboukou = Kiboukou;
            clone.ThiUserID = ThiUserID;
            clone.JimTantouID = JimTantouID;
            clone.MsThiIraiStatusID = MsThiIraiStatusID;
            clone.MmFlag = MmFlag;
            clone.SendFlag = SendFlag;
            clone.VesselID = VesselID;
            clone.DataNo = DataNo;
            clone.UserKey = UserKey;
            clone.RenewDate = RenewDate;
            clone.RenewUserID = RenewUserID;
            clone.Ts = Ts;
            clone.VesselName = VesselName;
            clone.OrderThiIraiStatus = OrderThiIraiStatus;
            clone.ThiIraiSbtName = ThiIraiSbtName;
            clone.ThiIraiShousaiName = ThiIraiShousaiName;
            clone.ThiUserName = ThiUserName;
            clone.JimTantouName = JimTantouName;

            foreach (OdThiItem ti in OdThiItems)
            {
                clone.OdThiItems.Add(ti.Clone());
            }

            return clone;
        }

        #endregion
    }
}
