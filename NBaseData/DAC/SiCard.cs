using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using System.Reflection;
using ORMapping;
using System.Runtime.Serialization;
using NBaseUtil;
using System.Diagnostics;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_CARD")]
    public class SiCard : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 船員カードID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_CARD_ID", true)]
        public string SiCardID { get; set; }


        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 種別ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHUBETSU_ID")]
        public int MsSiShubetsuID { get; set; }

        /// <summary>
        /// 種別詳細ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHUBETSU_SHOUSAI_ID")]
        public int MsSiShubetsuShousaiID { get; set; }

        /// <summary>
        /// 船ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public short MsVesselID { get; set; }




        /// <summary>
        /// 開始日
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_DATE")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        [DataMember]
        [ColumnAttribute("END_DATE")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 日数
        /// </summary>
        [DataMember]
        [ColumnAttribute("DAYS")]
        public int Days { get; set; }


        /// <summary>
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NAME")]
        public string VesselName { get; set; }

        /// <summary>
        /// 管理会社
        /// </summary>
        [DataMember]
        [ColumnAttribute("COMPANY_NAME")]
        public string CompanyName { get; set; }

        /// <summary>
        /// CrewMatrixType
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CREW_MATRIX_TYPE_ID")]
        public int MsCrewMatrixTypeID { get; set; }

        /// <summary>
        /// 船タイプID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_TYPE_ID")]
        public string MsVesselTypeID { get; set; }

        /// <summary>
        /// グロストン
        /// </summary>
        [DataMember]
        [ColumnAttribute("GROSS_TON")]
        public string GrossTon { get; set; }

        /// <summary>
        /// 航行区域
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAVIGATION_AREA")]
        public string NavigationArea { get; set; }

        /// <summary>
        /// 船主
        /// </summary>
        [DataMember]
        [ColumnAttribute("OWNER_NAME")]
        public string OwnerName { get; set; }




        /// <summary>
        /// 兼務通信長
        /// </summary>
        [DataMember]
        [ColumnAttribute("KENM_TUSHINCYO")]
        public int KenmTushincyo { get; set; }

        /// <summary>
        /// 兼務通信長（開始）
        /// </summary>
        [DataMember]
        [ColumnAttribute("KENM_TUSHINCYO_START")]
        public DateTime KenmTushincyoStart { get; set; }

        /// <summary>
        /// 兼務通信長（終了）
        /// </summary>
        [DataMember]
        [ColumnAttribute("KENM_TUSHINCYO_END")]
        public DateTime KenmTushincyoEnd { get; set; }






        /// <summary>
        /// 乗船時労働　　1.労働 2.半休 3.全休
        /// </summary>
        [DataMember]
        [ColumnAttribute("LABOR_ON_BOARDING")]
        public int LaborOnBoarding { get; set; }

        /// <summary>
        /// 下船時労働　　1.労働 2.半休 3.全休
        /// </summary>
        [DataMember]
        [ColumnAttribute("LABOR_ON_DISEMBARKING")]
        public int LaborOnDisembarking { get; set; }




        /// <summary>
        /// 乗船場所
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_ON_BASHO_ID")]
        public string SignOnBashoID { get; set; }





        /// <summary>
        /// 下船理由
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_OFF_REASON")]
        public string SignOffReason { get; set; }

        /// <summary>
        /// 下船場所
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_OFF_BASHO_ID")]
        public string SignOffBashoID { get; set; }


        /// <summary>
        /// 交代者の船員カードID
        /// </summary>
        [DataMember]
        [ColumnAttribute("REPLACEMENT_ID")]
        public string ReplacementID { get; set; }



        /// <summary>
        /// WTM連携ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("WTM_LINKAGE_ID")]
        public string WTMLinkageID { get; set; }




        #region 共通情報

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



        /// <summary>
        /// ユーザID (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_USER_ID")]
        public string MsUserID { get; set; }

        /// <summary>
        /// 船員名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_NAME")]
        public string SeninName { get; set; }

        /// <summary>
        /// 船員名カナ (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_NAME_KANA")]
        public string SeninNameKana { get; set; }

        /// <summary>
        /// 船員氏名コード (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_SHIMEI_CODE")]
        public string SeninShimeiCode { get; set; }

        /// <summary>
        /// 船員保険番号 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_HOKEN_NO")]
        public string SeninHokenNo { get; set; }

        /// <summary>
        /// 船員誕生日 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_BIRTHDAY")]
        public DateTime SeninBirthday { get; set; }

        /// <summary>
        /// 船員職名ID (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_MS_SI_SHOKUMEI_ID")]
        public int SeninMsSiShokumeiID { get; set; }

        /// <summary>
        /// 船員区分 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_KUBUN")]
        public int SeninKubun { get; set; }


        /// <summary>
        /// 乗船職名ID (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARD_MS_SI_SHOKUMEI_ID")]
        public int CardMsSiShokumeiID { get; set; }


        /// <summary>
        /// 退職フラグ(LEFT JOIN)
        /// </summary>
        [DataMember]
        public int SeninRetireFlag { get; set; }

        /// <summary>
        /// 合計日数
        /// </summary>
        [DataMember]
        public Dictionary<int, int> 合計日数 { get; set; }

        /// <summary>
        /// 休暇残日
        /// </summary>
        [DataMember]
        public int 休暇残日 { get; set; }


        #endregion


        public static List<string> SignOffReasonStrings = new List<string> { "", "一括公認", "職務変更", "有給休暇", "社命転船" };
        public static string ConvertSignOffReasonStrings(string id)
        {
            int index = 0;
            if (int.TryParse(id, out index))
                return SignOffReasonStrings[index];
            else
                return "";
        }
        public static string ConvertSignOffReasonId(string reasonStr)
        {
            if (SignOffReasonStrings.Contains(reasonStr))
                return SignOffReasonStrings.IndexOf(reasonStr).ToString();
            else
                return "0";
        }

        public string SeninKubunStr
        {
            get
            {
                return SeninKubun == 0 ? "社員" : "派遣";
            }
        }


        public string CalcDaysStr
        {
            get
            {
                string retDaysStr = "";

                if (EndDate == DateTime.MinValue || (DateTimeUtils.ToFrom(StartDate) <= DateTime.Now && DateTime.Now < DateTimeUtils.ToTo(EndDate)))
                {
                    retDaysStr = StringUtils.ToStr(StartDate, DateTime.Now);
                }
                else
                {
                    retDaysStr = StringUtils.ToStr(StartDate, EndDate);
                }

                return retDaysStr;
            }
        }

        public int CalcDays
        {
            get
            {
                return int.Parse(CalcDaysStr);
            }
        }


        /// <summary>
        /// 職名船員カードリンクリスト
        /// </summary>
        [DataMember]
        private List<SiLinkShokumeiCard> siLinkShokumeiCards;
        public List<SiLinkShokumeiCard> SiLinkShokumeiCards
        {
            get
            {
                if (siLinkShokumeiCards == null)
                {
                    siLinkShokumeiCards = new List<SiLinkShokumeiCard>();
                }

                return siLinkShokumeiCards;
            }
        }


        public enum LABOR { 労働 = 1, 半休, 全休 };



        public SiCard()
        {
            this.MsSeninID = Int32.MinValue;
            this.MsSiShubetsuID = Int32.MinValue;
            this.MsVesselID = short.MinValue;
        }

        public static SiCard GetRecord(MsUser loginUser, string siCardId)
        {
            return GetRecord(null, loginUser, siCardId);
        }
        public static SiCard GetRecord(DBConnect dbConnect, MsUser loginUser, string siCardId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCard), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "BySiCardID");

            List<SiCard> ret = new List<SiCard>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_CARD_ID", siCardId));

            MappingBase<SiCard> mapping = new MappingBase<SiCard>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);
            if (ret != null && ret.Count > 0)
            {
                JoinSiLinkShokumeiCards(dbConnect, loginUser, ret);
                return ret[0];
            }
            else
            {
                return null;
            }
        }

        public static SiCard GetRecordParent(MsUser loginUser, string id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCard), "GetRecordParent");

            List<SiCard> ret = new List<SiCard>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("REPLACEMENT_ID", id));

            MappingBase<SiCard> mapping = new MappingBase<SiCard>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret != null && ret.Count > 0)
            {
                return ret[0];
            }
            else
            {
                return null;
            }
        }

        public static List<SiCard> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCard), MethodBase.GetCurrentMethod());

            List<SiCard> ret = new List<SiCard>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiCard> mapping = new MappingBase<SiCard>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            JoinSiLinkShokumeiCards(null, loginUser, ret);

            return ret;
        }


        public static List<SiCard> GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCard), MethodBase.GetCurrentMethod());

            List<SiCard> ret = new List<SiCard>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiCard> mapping = new MappingBase<SiCard>();


            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            JoinSiLinkShokumeiCards(null, loginUser, ret);

            return ret;
        }


        public static List<SiCard> GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, SiCardFilter filter)
        {
            return GetRecordsByFilter(null, loginUser, filter);
        }


        public static List<SiCard> GetRecordsByFilter(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, SiCardFilter filter)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCard), "GetRecords");

            List<SiCard> ret = new List<SiCard>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiCard> mapping = new MappingBase<SiCard>();

            if (filter.MsSeninID != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByMsSeninID");
                Params.Add(new DBParameter("MS_SENIN_ID", filter.MsSeninID));
            }

            if (filter.MsSiShubetsuIDs.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByMsSiShubetsuIDs");
                string innerSQLStr0 = SqlMapper.SqlMapper.CreateInnerSql("q", filter.MsSiShubetsuIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_MS_SI_SHUBETSUS#", innerSQLStr0);
                Params.AddInnerParams("q", filter.MsSiShubetsuIDs);
            }

            if (filter.MsVesselIDs.Count > 0)
            {
                if (filter.IncludeNullVessel)
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByMsVesselIDsIncludeNull");
                }
                else
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByMsVesselIDs");
                }

                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", filter.MsVesselIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_MSVESSELIDS#", innerSQLStr);
                Params.AddInnerParams("p", filter.MsVesselIDs);
            }

            if (filter.Start != DateTime.MinValue && filter.End != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByStartEnd");

                Params.Add(new DBParameter("START_DATE", filter.Start));
                Params.Add(new DBParameter("END_DATE", filter.End));
            }

            if (filter.RetireFlag != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByRetireFlag");

                Params.Add(new DBParameter("RETIRE_FLAG", filter.RetireFlag));
            }

            // 201801: ２０１７年度改造
            if (filter.KenmTushincyo)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByKenmTushincyo");
            }

            if (filter.OrderByStr != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), filter.OrderByStr);
            }
            else
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "OrderByStartDateDesc");
            }

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            JoinSiLinkShokumeiCards(dbConnect, loginUser, ret);

            return ret;
        }


        public static List<SiCard> GetRecordsMinimum(NBaseData.DAC.MsUser loginUser, SiCardFilter filter)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCard), "GetRecords");

            List<SiCard> ret = new List<SiCard>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiCard> mapping = new MappingBase<SiCard>();

            if (filter.MsSeninIDs.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByMsSeninIDs");
                string innerSQLStr0 = SqlMapper.SqlMapper.CreateInnerSql("r", filter.MsSeninIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_MS_SENINS#", innerSQLStr0);
                Params.AddInnerParams("r", filter.MsSeninIDs);
            }

            if (filter.MsSiShubetsuIDs.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByMsSiShubetsuIDs");
                string innerSQLStr0 = SqlMapper.SqlMapper.CreateInnerSql("q", filter.MsSiShubetsuIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_MS_SI_SHUBETSUS#", innerSQLStr0);
                Params.AddInnerParams("q", filter.MsSiShubetsuIDs);
            }

            if (filter.Start != DateTime.MinValue && filter.End != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByStartEnd");

                Params.Add(new DBParameter("START_DATE", filter.Start));
                Params.Add(new DBParameter("END_DATE", filter.End));
            }

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        private static void JoinSiLinkShokumeiCards(DBConnect dbConnect, MsUser loginUser, List<SiCard> cards)
        {
            foreach (SiCard c in cards)
            {
                c.SiLinkShokumeiCards.AddRange(SiLinkShokumeiCard.GetRecordsBySiCardID(dbConnect, loginUser, c.SiCardID));
            }
        }


        public static List<SiCard> Get_期間重複(MsUser loginUser, int msSeninId, string siCardId, DateTime start, DateTime end)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCard), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "FilterByMsSeninID");

            List<SiCard> ret = new List<SiCard>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiCard> mapping = new MappingBase<SiCard>();
            Params.Add(new DBParameter("SI_CARD_ID", siCardId != null ? siCardId : "NULL"));
            Params.Add(new DBParameter("START_DATE", DateTimeUtils.ToFrom(start)));
            //Params.Add(new DBParameter("END_DATE", DateTimeUtils.ToTo(end).AddSeconds(-1)));
            Params.Add(new DBParameter("END_DATE", DateTimeUtils.ToFrom(end)));
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static SiCard Get_船長(MsUser loginUser, int msVesselId)
        {
            SiCardFilter filter = new SiCardFilter();

            filter.MsVesselIDs.Add(msVesselId);
            MsSiShubetsu shubetsu = MsSiShubetsu.GetRecordByName(loginUser, "乗船");
            filter.MsSiShubetsuIDs.Add(shubetsu.MsSiShubetsuID);
            filter.Start = filter.End = DateTime.Now;
            filter.OrderByStr = "OrderByMsSiShokumeiId";
            filter.RetireFlag = 0;

            List<SiCard> cards = GetRecordsByFilter(loginUser, filter);

            MsSiShokumei shokumei = MsSiShokumei.GetRecordByName(loginUser, "船長");

            foreach (SiCard c in cards)
            {
                foreach (SiLinkShokumeiCard link in c.siLinkShokumeiCards)
                {
                    if (link.MsSiShokumeiID == shokumei.MsSiShokumeiID)
                    {
                        return c;
                    }
                }
            }

            return null;
        }
        public static List<SiCard> GetRecordsJoinMsUserByFilter(MsUser loginUser, SiCardFilter filter)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCard), "GetRecordsJoinMsUserByFilter");

            List<SiCard> ret = new List<SiCard>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiCard> mapping = new MappingBase<SiCard>();

            Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselIDs[0]));
            Params.Add(new DBParameter("MS_SI_SHUBETSU_ID", filter.MsSiShubetsuIDs[0]));
            Params.Add(new DBParameter("START_DATE", filter.Start));
            Params.Add(new DBParameter("END_DATE", filter.End));

            if (filter.OrderByStr != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), filter.OrderByStr);
            }
            else
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCard), "OrderByStartDateDesc");
            }

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            JoinSiLinkShokumeiCards(null, loginUser, ret);
            SetDays(ret);

            return ret;
        }
        private static void SetDays(List<SiCard> cards)
        {
            foreach (SiCard c in cards)
            {
                if (c.EndDate == DateTime.MinValue)
                {
                    try
                    {
                        c.Days = int.Parse(StringUtils.ToStr(c.StartDate, DateTime.Now));
                    }
                    catch
                    {
                    }
                }
            }
        }



        public static void RemoveReplacement(ORMapping.DBConnect dbConnect, MsUser loginUser, string replacementId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCard), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("REPLACEMENT_ID", replacementId));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            return;
        }

        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_CARD_ID", SiCardID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_SI_SHUBETSU_ID", MsSiShubetsuID));
            Params.Add(new DBParameter("MS_SI_SHUBETSU_SHOUSAI_ID", MsSiShubetsuShousaiID));

            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));

            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));
            Params.Add(new DBParameter("DAYS", Days));

            Params.Add(new DBParameter("VESSEL_NAME", VesselName));
            Params.Add(new DBParameter("COMPANY_NAME", CompanyName));
            Params.Add(new DBParameter("MS_CREW_MATRIX_TYPE_ID", MsCrewMatrixTypeID));

            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", MsVesselTypeID));
            Params.Add(new DBParameter("GROSS_TON", GrossTon));
            Params.Add(new DBParameter("NAVIGATION_AREA", NavigationArea));
            Params.Add(new DBParameter("OWNER_NAME", OwnerName));

            Params.Add(new DBParameter("KENM_TUSHINCYO", KenmTushincyo));
            Params.Add(new DBParameter("KENM_TUSHINCYO_START", KenmTushincyoStart));
            Params.Add(new DBParameter("KENM_TUSHINCYO_END", KenmTushincyoEnd));

            Params.Add(new DBParameter("LABOR_ON_BOARDING", LaborOnBoarding));
            Params.Add(new DBParameter("LABOR_ON_DISEMBARKING", LaborOnDisembarking));

            Params.Add(new DBParameter("SIGN_ON_BASHO_ID", SignOnBashoID));

            Params.Add(new DBParameter("SIGN_OFF_REASON", SignOffReason));
            Params.Add(new DBParameter("SIGN_OFF_BASHO_ID", SignOffBashoID));
            Params.Add(new DBParameter("REPLACEMENT_ID", ReplacementID));

            Params.Add(new DBParameter("WTM_LINKAGE_ID", WTMLinkageID));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;

            return true;
        }


        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_SI_SHUBETSU_ID", MsSiShubetsuID));
            Params.Add(new DBParameter("MS_SI_SHUBETSU_SHOUSAI_ID", MsSiShubetsuShousaiID));

            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));

            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));
            Params.Add(new DBParameter("DAYS", Days));

            Params.Add(new DBParameter("VESSEL_NAME", VesselName));
            Params.Add(new DBParameter("COMPANY_NAME", CompanyName));
            Params.Add(new DBParameter("MS_CREW_MATRIX_TYPE_ID", MsCrewMatrixTypeID));

            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", MsVesselTypeID));
            Params.Add(new DBParameter("GROSS_TON", GrossTon));
            Params.Add(new DBParameter("NAVIGATION_AREA", NavigationArea));
            Params.Add(new DBParameter("OWNER_NAME", OwnerName));

            Params.Add(new DBParameter("KENM_TUSHINCYO", KenmTushincyo));
            Params.Add(new DBParameter("KENM_TUSHINCYO_START", KenmTushincyoStart));
            Params.Add(new DBParameter("KENM_TUSHINCYO_END", KenmTushincyoEnd));

            Params.Add(new DBParameter("LABOR_ON_BOARDING", LaborOnBoarding));
            Params.Add(new DBParameter("LABOR_ON_DISEMBARKING", LaborOnDisembarking));

            Params.Add(new DBParameter("SIGN_ON_BASHO_ID", SignOnBashoID));

            Params.Add(new DBParameter("SIGN_OFF_REASON", SignOffReason));
            Params.Add(new DBParameter("SIGN_OFF_BASHO_ID", SignOffBashoID));
            Params.Add(new DBParameter("REPLACEMENT_ID", ReplacementID));

            Params.Add(new DBParameter("WTM_LINKAGE_ID", WTMLinkageID));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_CARD_ID", SiCardID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;

            return true;
        }

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", SiCardID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }
        #endregion

        public bool IsNew()
        {
            return SiCardID == null;
        }
    }
}
