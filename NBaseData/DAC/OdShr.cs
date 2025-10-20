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
    [TableAttribute("OD_SHR")]
    public class OdShr : IGenericCloneable<OdShr>, ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 支払ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_ID", true)]
        public string OdShrID { get; set; }

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
        /// 受領ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ID")]
        public string OdJryID { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("SBT")]
        public int Sbt { get; set; }

        /// <summary>
        /// 支払伝票番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHR_NO")]
        public string ShrNo { get; set; }

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
        /// 基幹システム支払番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIKAN_SYSTEM_SHR_NO")]
        public string KikanSystemShrNo { get; set; }

        /// <summary>
        /// 支払金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("AMOUNT")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 請求値引き額
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
        /// 支払依頼日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHR_IRAI_DATE")]
        public DateTime ShrIraiDate { get; set; }

        /// <summary>
        /// 支払日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHR_DATE")]
        public DateTime ShrDate { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [ColumnAttribute("TEKIYOU")]
        public string Tekiyou { get; set; }

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
        /// 支払担当者
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHR_TANTOU")]
        public string ShrTantou { get; set; }

        /// <summary>
        /// 計上日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KEIJO_DATE")]
        public DateTime KeijyoDate { get; set; }

        /// <summary>
        /// 処理ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("SYORI_STATUS")]
        public string SyoriStatus { get; set; }

        /// <summary>
        /// 起票日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIHYOUBI")]
        public DateTime Kihyoubi { get; set; }

        /// <summary>
        /// 送料・運搬料
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARRIAGE")]
        public decimal Carriage { get; set; }

        /// <summary>
        /// 手配依頼内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_NAIYOU")]
        public string OdThiNaiyou { get; set; }

        /// <summary>
        /// [OD_JRY]受領日
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_JRY_DATE")]
        public DateTime OdJry_JryDate { get; set; }

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
        /// [MS_VESSE]船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_VESSEL_NAME")]
        public string MsVessel_VesselName { get; set; }

        /// <summary>
        /// 発注番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("HACHU_NO")]
        public string HachuNo { get; set; }

        /// <summary>
        /// 支払合算ヘッダID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_GASSAN_HEAD_ID", true)]
        public string OdShrGassanHeadID { get; set; }



        /// <summary>
        /// [OD_THIから]船ID
        /// </summary>
        [ColumnAttribute("OD_THI_MS_VESSEL_ID")]
        public int OdThi_MsVesselID { get; set; }

        /// <summary>
        /// [OD_THIから]手配依頼種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_MS_THI_IRAI_SBT_ID")]
        public string OdThi_MsThiIraiSbtID { get; set; }

        /// <summary>
        /// [OD_THIから]手配依頼詳細種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_MS_THI_IRAI_SHOUSAI_ID")]
        public string OdThi_MsThiIraiShousaiID { get; set; }

        /// <summary>
        /// [OD_JRYから]受領伝票番号
        /// </summary>
        [ColumnAttribute("OD_JRY_JRY_NO")]
        public string OdJry_JryNo { get; set; }

        /// <summary>
        /// [OD_THIから]事務担当者ID
        /// </summary>
        [ColumnAttribute("OD_THI_JIM_TANTOU_ID")]
        public string OdThi_JimTantouID { get; set; }

        #endregion

        /// <summary>
        /// 支払合算項目
        /// </summary>
        [DataMember]
        private List<OdShrGassanItem> odShrGassanItems;
        public List<OdShrGassanItem> OdShrGassanItems
        {
            get
            {
                if (odShrGassanItems == null)
                {
                    odShrGassanItems = new List<OdShrGassanItem>();
                }

                return odShrGassanItems;
            }
        }


        [DataMember]
        public OdStatus OdStatusValue = null;

        //public static int NoLength支払 = 12;
        public static int NoLength支払 = 13;
        public static string Prefix落成 = "R";
        public static string Prefix支払 = "S";
        public enum SBT { 支払, 落成 };
        //支払
        //0:支払作成済み
        //1:支払依頼済み
        //2:支払依頼基幹連携済み
        //3:支払済
        // 落成
        //10:未落成
        //11:落成承認依頼中
        //12:落成承認済み
        //13:落成済み
        public enum STATUS { 支払作成済み = 0, 
                             支払依頼済み = 1,
                             支払依頼基幹連携済み = 2,
                             支払済 = 3,
                             未落成 = 10,
                             落成承認依頼中 = 11,
                             落成承認済み = 12,
                             落成済み = 13,
                           };
        string[] statusStr1 = { "支払作成済み", "支払依頼済み", "支払依頼基幹連携済み", "支払済" };
        string[] statusStr2 = { "未落成", "落成承認依頼中", "落成承認済み", "落成済み" };
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
        public OdShr()
        {
            InitStatusValue();
        }

        private void InitStatusValue()
        {
            OdStatusValue = new OdStatus();
            OdStatusValue.DefaultValue = 0;
            OdStatusValue.Values = new List<OdStatus.StatusValue>();
            for (int i = 0; i < statusStr1.Length; i++)
            {
                OdStatus.StatusValue value = new OdStatus.StatusValue();
                value.Value = i;
                value.Name = statusStr1[i];
                OdStatusValue.Values.Add(value);
            }
            for (int i = statusStr1.Length; i < (int)STATUS.未落成; i++)
            {
                OdStatus.StatusValue value = new OdStatus.StatusValue();
                OdStatusValue.Values.Add(value);
            }
            for (int i = 0; i < statusStr2.Length; i++)
            {
                OdStatus.StatusValue value = new OdStatus.StatusValue();
                value.Value = (int)STATUS.未落成 + i;
                value.Name = statusStr2[i];
                OdStatusValue.Values.Add(value);
            }
        }

        public static List<OdShr> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static OdShr GetRecord(NBaseData.DAC.MsUser loginUser, string OdShrID)
        {
            return GetRecord(null, loginUser, OdShrID);
        }

        public static OdShr GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdShrID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "ByOdShrID");

            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_SHR_ID", OdShrID));
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<OdShr> GetRecordByOdJryID(NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            return GetRecordByOdJryID(null, loginUser, odJryID);
        }
        public static List<OdShr> GetRecordByOdJryID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendColumnForOdJryID"));
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendJoinForOdJryID"));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "ByOdJryID");

            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_ID", odJryID));
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();
            ret = mapping.FillRecrods(dbConnect,loginUser.MsUserID, SQL, Params);

            return ret;
        }

        
        public static List<OdShr> GetRecordByStatus(NBaseData.DAC.MsUser loginUser, string status)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByStatus");

            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("STATUS", status));
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdShr> GetRecordBySyoriStatus(NBaseData.DAC.MsUser loginUser, string status, string syoriStatus)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterBySyoriStatus");

            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("STATUS", status));
            Params.Add(new DBParameter("SYORI_STATUS", syoriStatus));
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdShr> GetRecords落成済み未払い(NBaseData.DAC.MsUser loginUser, OdJryFilter filter)
        {
            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), "GetRecords");
            //SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendColumnForOdThiFilter2"));
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendJoinForOdThiFilter2"));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByMibaraiRakuseizumi");
            Params.Add(new DBParameter("MS_THI_IRAI_STATUS_ID", ((int)MsThiIraiStatus.THI_IRAI_STATUS.完了).ToString()));
            if (filter.YearMonth != null)
            {
                if (filter.YearMonthOnly == true)
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByYearMonthOnly");
                }
                else
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByYearMonth");
                }
                Params.Add(new DBParameter("YYYYMM", filter.YearMonth));
            }
            if (filter.MsVesselID != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselID));
            }
            if (filter.JimTantouID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByJimTantouID");
                Params.Add(new DBParameter("JIM_TANTOU_ID", filter.JimTantouID));
            }
            if (filter.MsThiIraiSbtID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByThiIraiSbtID");
                Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", filter.MsThiIraiSbtID));
            }
            if (filter.MsThiIraiShousaiID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByThiIraiShousaiID");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", filter.MsThiIraiShousaiID));
            }
            if (filter.MiKeijyo == true)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByMiKeijyo");
                Params.Add(new DBParameter("GAISAN_KEIJO_ZUKI", filter.YearMonth)); // 2009.10.29:aki 対象月に概算計上されていないものを取得
            }
            else if (filter.KeijyoZumi == true)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByKeijyoZumi");
                Params.Add(new DBParameter("GAISAN_KEIJO_ZUKI", filter.YearMonth));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "OrderByNo");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdShr> GetRecords未払い(NBaseData.DAC.MsUser loginUser, OdJryFilter filter)
        {
            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), "GetRecords");
            //SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendColumnForOdThiFilter2"));
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendJoinForOdThiFilter2"));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByMibarai");
            Params.Add(new DBParameter("MS_THI_IRAI_STATUS_ID", ((int)MsThiIraiStatus.THI_IRAI_STATUS.完了).ToString()));
            Params.Add(new DBParameter("SHR_DATE", filter.YearMonth));
            if (filter.YearMonth != null)
            {
                if (filter.YearMonthOnly == true)
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByYearMonthOnly");
                }
                else
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByYearMonth");
                }
                Params.Add(new DBParameter("YYYYMM", filter.YearMonth));
            }
            if (filter.MsVesselID != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselID));
            }
            if (filter.JimTantouID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByJimTantouID");
                Params.Add(new DBParameter("JIM_TANTOU_ID", filter.JimTantouID));
            }
            if (filter.MsThiIraiSbtID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByThiIraiSbtID");
                Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", filter.MsThiIraiSbtID));
            }
            if (filter.MsThiIraiShousaiID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByThiIraiShousaiID");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", filter.MsThiIraiShousaiID));
            }
            if (filter.MiKeijyo == true)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByMiKeijyo");
                Params.Add(new DBParameter("GAISAN_KEIJO_ZUKI", filter.YearMonth)); // 2009.10.29:aki 対象月に概算計上されていないものを取得
            }
            else if (filter.KeijyoZumi == true)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByKeijyoZumi");
                Params.Add(new DBParameter("GAISAN_KEIJO_ZUKI", filter.YearMonth));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "OrderByNo");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdShr> GetRecordsBy計上月(NBaseData.DAC.MsUser loginUser, OdJryFilter filter)
        {
            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendColumnForOdThiFilter2"));
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendJoinForOdThiFilter2"));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByKeijoYearMonth");
            Params.Add(new DBParameter("KEIJO_DATE", filter.YearMonth));
            if (filter.MsVesselID != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselID));
            }
            if (filter.JimTantouID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByJimTantouID");
                Params.Add(new DBParameter("JIM_TANTOU_ID", filter.JimTantouID));
            }
            if (filter.MsThiIraiSbtID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByThiIraiSbtID");
                Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", filter.MsThiIraiSbtID));
            }
            if (filter.MsThiIraiShousaiID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByThiIraiShousaiID");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", filter.MsThiIraiShousaiID));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "OrderByNo");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdShr> GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, int status, OdThiFilter filter)
        {
            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendColumnForOdThiFilter"));
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendJoinForOdThiFilter"));
            
            string CondStr = OdThi.GetConditionString(loginUser, filter, ref Params);
            SQL = SQL.Replace("AND OD_THI.CANCEL_FLAG = 0", "AND OD_THI.CANCEL_FLAG = 0 AND OD_THI.OD_THI_ID IN ( " + CondStr + " ) ");

            if (status != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "FilterByStatus");
                Params.Add(new DBParameter("STATUS", status));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "OrderByNo");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            // パフォーマンスは落ちる
            foreach (OdShr shr in ret)
            {
                List<OdShrGassanItem> workList = OdShrGassanItem.GetRecordsByOdShrId(loginUser, shr.OdShrID);
                if (workList != null)
                {
                    shr.OdShrGassanItems.AddRange(workList);
                }
            }

            return ret;
        }

        public static OdShr GetRecordsFor基幹連携(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string odShrID)
        {
            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendColumnForOdThiFilter2"));
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendJoinForOdThiFilter2"));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "ByOdShrID");
            Params.Add(new DBParameter("OD_SHR_ID", odShrID));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public static List<OdShr> GetRecordByGassanItem(NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            return GetRecordByGassanItem(null, loginUser, odJryID);
        }
        public static List<OdShr> GetRecordByGassanItem(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendColumnForGassanItem"));
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendJoinForGassanItem"));
            Params.Add(new DBParameter("OD_JRY_ID", odJryID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdShr> GetRecordsByOdThiId(NBaseData.DAC.MsUser loginUser, string odThiId)
        {
            return GetRecordsByOdThiId(null, loginUser, odThiId);
        }
        public static List<OdShr> GetRecordsByOdThiId(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string odThiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), "GetRecords");
            SQL = SQL.Replace("#APPEND_COLUMN_STR#", "");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdShr), "AppendJoinForOdThiFilter"));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShr), "ByOdThiID");

            List<OdShr> ret = new List<OdShr>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShr> mapping = new MappingBase<OdShr>();
            Params.Add(new DBParameter("OD_THI_ID", odThiId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_SHR_ID", OdShrID));
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            Params.Add(new DBParameter("SBT", Sbt));
            Params.Add(new DBParameter("SHR_NO", ShrNo));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("KIKAN_SYSTEM_SHR_NO", KikanSystemShrNo));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("NEBIKI_AMOUNT", NebikiAmount));
            Params.Add(new DBParameter("TAX", Tax));
            Params.Add(new DBParameter("KAMOKU_NO", KamokuNo));
            Params.Add(new DBParameter("UTIWAKE_KAMOKU_NO", UtiwakeKamokuNo));
            Params.Add(new DBParameter("SHR_IRAI_DATE", ShrIraiDate));
            Params.Add(new DBParameter("SHR_DATE", ShrDate));
            Params.Add(new DBParameter("TEKIYOU", Tekiyou));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SHR_TANTOU", ShrTantou));
            Params.Add(new DBParameter("KEIJO_DATE", KeijyoDate));
            Params.Add(new DBParameter("SYORI_STATUS", SyoriStatus));
            Params.Add(new DBParameter("KIHYOUBI", Kihyoubi));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShr), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            Params.Add(new DBParameter("SBT", Sbt));
            Params.Add(new DBParameter("SHR_NO", ShrNo));
            Params.Add(new DBParameter("NAIYOU", Naiyou));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("KIKAN_SYSTEM_SHR_NO", KikanSystemShrNo));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("NEBIKI_AMOUNT", NebikiAmount));
            Params.Add(new DBParameter("TAX", Tax));
            Params.Add(new DBParameter("KAMOKU_NO", KamokuNo));
            Params.Add(new DBParameter("UTIWAKE_KAMOKU_NO", UtiwakeKamokuNo));
            Params.Add(new DBParameter("SHR_IRAI_DATE", ShrIraiDate));
            Params.Add(new DBParameter("SHR_DATE", ShrDate));
            Params.Add(new DBParameter("TEKIYOU", Tekiyou));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SHR_TANTOU", ShrTantou));
            Params.Add(new DBParameter("KEIJO_DATE", KeijyoDate));
            Params.Add(new DBParameter("SYORI_STATUS", SyoriStatus));
            Params.Add(new DBParameter("KIHYOUBI", Kihyoubi));

            Params.Add(new DBParameter("CARRIAGE", Carriage));

            Params.Add(new DBParameter("OD_SHR_ID", OdShrID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region IGenericCloneable<OdShr> メンバ

        public OdShr Clone()
        {
            OdShr clone = new OdShr();

            clone.OdShrID = OdShrID;
            clone.Status = Status;
            clone.CancelFlag = CancelFlag;
            clone.OdJryID = OdJryID;
            clone.Sbt = Sbt;
            clone.ShrNo = ShrNo;
            clone.Naiyou = Naiyou;
            clone.Bikou = Bikou;
            clone.KikanSystemShrNo = KikanSystemShrNo;
            clone.Amount = Amount;
            clone.NebikiAmount = NebikiAmount;
            clone.Tax = Tax;
            clone.ShrIraiDate = ShrIraiDate;
            clone.ShrDate = ShrDate;
            clone.Tekiyou = Tekiyou;
            clone.KamokuNo = KamokuNo;
            clone.UtiwakeKamokuNo = UtiwakeKamokuNo;
            clone.ShrDate = ShrDate;
            clone.Tekiyou = Tekiyou;
            clone.SendFlag = SendFlag;
            clone.VesselID = VesselID;
            clone.DataNo = DataNo;
            clone.UserKey = UserKey;
            clone.RenewDate = RenewDate;
            clone.RenewUserID = RenewUserID;
            clone.Ts = Ts;
            clone.ShrTantou = ShrTantou;
            clone.KeijyoDate = KeijyoDate;
            clone.SyoriStatus = SyoriStatus;
            clone.Kihyoubi = Kihyoubi;
            clone.OdThiNaiyou = OdThiNaiyou;
            clone.OdJry_JryDate = OdJry_JryDate;
            clone.OdMk_MsCustomerID = OdMk_MsCustomerID;
            clone.MsVessel_VesselNo = MsVessel_VesselNo;
            clone.MsVessel_VesselName = MsVessel_VesselName;
            clone.Carriage = Carriage;

            return clone;
        }

        #endregion

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", OdShrID));

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
            Params.Add(new DBParameter("PK", OdShrID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
