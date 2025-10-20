using System;
using System.Collections.Generic;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using System.Reflection;
using ORMapping;
using System.Runtime.Serialization;
using NBaseUtil;
using System.Linq;
namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_CARD_PLAN")]
    public class SiCardPlan : ISyncTable
    {
        #region データメンバー
        /// <summary>
        /// 配乗計画ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_CARD_PLAN_ID", true)]
        public string SiCardPlanID { get; set; }

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
        /// 船ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 職名ID 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 職名詳細ID 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_SHOUSAI_ID")]
        public int MsSiShokumeiShousaiID { get; set; }

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
        /// ヘッダID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_CARD_PLAN_HEAD_ID")]
        public string SiCardPlanHeadID { get; set; }

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
        /// 交代予定カード　　=SiCardPlanID
        /// </summary>
        [DataMember]
        [ColumnAttribute("REPLACEMENT")]
        public string Replacement { get; set; }

        /// <summary>
        /// 交代場所
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BASHO_ID")]
        public string MsBashoID { get; set; }

        #region ISync
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

        //////////////////////////////////////////////////////
        // JOIN
        //////////////////////////////////////////////////////
        /// <summary> 
        /// 船員名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_NAME")]
        public string SeninName { get; set; }

        /// <summary>
        ///月締フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("HEAD_SHIME_FLAG")]
        public int HeadShimeFlag { get; set; }


        //////////////////////////////////////////////////////
        // BLC
        //////////////////////////////////////////////////////
        /// <summary>
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NAME")]
        public string VesselName { get; set; }

        /// <summary>
        /// 職名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string ShokuName { get; set; }

        /// <summary>
        /// 職名略称
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME_ABBR")]
        public string ShokuNameAbbr { get; set; }

        /// <summary>
        /// 職名(英語)
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME_ENG")]
        public string ShokuNameEng { get; set; }

        /// <summary>
        /// 交代者（船員ID）
        /// </summary>
        [DataMember]

        public int ReplacementSeninID { get; set; }


        [DataMember]
        public bool LinkageReplacement { get; set; }

        #endregion

        public enum LABOR { 労働 = 1, 半休, 全休 };

        public override string ToString()
        {
            return ShokuNameEng;
        }

        public SiCardPlan()
        {
            this.MsSeninID =0;
            this.MsSiShubetsuID = 0;
            this.MsVesselID = 0;
            this.MsSiShokumeiID = 0;
            this.MsSiShokumeiShousaiID = 0;
            this.StartDate = DateTime.MinValue;
            this.EndDate = DateTime.MinValue;
            this.Replacement = "";
            this.MsBashoID = "";

            this.ReplacementSeninID = 0;
            this.LinkageReplacement = true;
        }

        public static List<SiCardPlan> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCardPlan), MethodBase.GetCurrentMethod());

            List<SiCardPlan> ret = new List<SiCardPlan>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiCardPlan> mapping = new MappingBase<SiCardPlan>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiCardPlan> GetRecordsByStartEnd(MsUser loginUser, DateTime startdate, DateTime enddate, int vessel_kind)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCardPlan), "GetRecords");

            List<SiCardPlan> ret = new List<SiCardPlan>();

            ParameterConnection Params = new ParameterConnection();

            if (startdate != DateTime.MinValue && enddate != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCardPlan), "ByStartEnd");

                Params.Add(new DBParameter("START_DATE", startdate));
                Params.Add(new DBParameter("END_DATE", enddate));
                Params.Add(new DBParameter("VESSEL_KIND", vessel_kind));
            }

            MappingBase<SiCardPlan> mapping = new MappingBase<SiCardPlan>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiCardPlan> GetRecordsByHead(MsUser loginUser,SiCardPlanHead head)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCardPlan), MethodBase.GetCurrentMethod());

            List<SiCardPlan> ret = new List<SiCardPlan>();

            ParameterConnection Params = new ParameterConnection();

           
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCardPlan), "ByHeadID");

            Params.Add(new DBParameter("SI_CARD_PLAN_HEAD_ID", head.SiCardPlanHeadID));
            
            MappingBase<SiCardPlan> mapping = new MappingBase<SiCardPlan>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiCardPlan> GetRecordsBySenin(MsUser loginUser, int seninID, int vessel_kind)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCardPlan), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCardPlan), "BySeninID");

            List<SiCardPlan> ret = new List<SiCardPlan>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SENIN_ID", seninID));
            Params.Add(new DBParameter("VESSEL_KIND", vessel_kind));

            MappingBase<SiCardPlan> mapping = new MappingBase<SiCardPlan>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static SiCardPlan GetRecord(MsUser loginUser, string id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCardPlan), "GetRecord");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCardPlan), "BySiCardPlanID");

            List<SiCardPlan> ret = new List<SiCardPlan>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_CARD_PLAN_ID", id));

            MappingBase<SiCardPlan> mapping = new MappingBase<SiCardPlan>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            
            if (ret != null && ret.Count>0)
            {
                return ret[0];
            }
            else
            {
                return null;
            }
        }

        public static SiCardPlan GetRecordParent(MsUser loginUser, string id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCardPlan), "GetRecordParent");

            List<SiCardPlan> ret = new List<SiCardPlan>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("REPLACEMENT", id));

            MappingBase<SiCardPlan> mapping = new MappingBase<SiCardPlan>();
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

        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_CARD_PLAN_ID", SiCardPlanID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_SI_SHUBETSU_ID", MsSiShubetsuID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_SHOUSAI_ID", MsSiShokumeiShousaiID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));
            Params.Add(new DBParameter("SI_CARD_PLAN_HEAD_ID", SiCardPlanHeadID));
            Params.Add(new DBParameter("LABOR_ON_BOARDING", LaborOnBoarding));
            Params.Add(new DBParameter("LABOR_ON_DISEMBARKING", LaborOnDisembarking));
            Params.Add(new DBParameter("REPLACEMENT", Replacement));
            Params.Add(new DBParameter("MS_BASHO_ID", MsBashoID));

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
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_SHOUSAI_ID", MsSiShokumeiShousaiID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));
            Params.Add(new DBParameter("SI_CARD_PLAN_HEAD_ID", SiCardPlanHeadID));
            Params.Add(new DBParameter("LABOR_ON_BOARDING", LaborOnBoarding));
            Params.Add(new DBParameter("LABOR_ON_DISEMBARKING", LaborOnDisembarking));
            Params.Add(new DBParameter("REPLACEMENT", Replacement));
            Params.Add(new DBParameter("MS_BASHO_ID", MsBashoID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_CARD_PLAN_ID", SiCardPlanID));
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
            Params.Add(new DBParameter("PK", SiCardPlanID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }
        #endregion

        public bool IsNew()
        {
            return SiCardPlanID == null;
        }

        public SiCardPlan Clone()
        {
            SiCardPlan clone = new SiCardPlan();

            clone.SiCardPlanID = SiCardPlanID;
            clone.MsSeninID = MsSeninID;
            clone.MsSiShubetsuID = MsSiShubetsuID;
            clone.MsVesselID = MsVesselID;
            clone.MsSiShokumeiID = MsSiShokumeiID;
            clone.MsSiShokumeiShousaiID = MsSiShokumeiShousaiID;
            clone.StartDate = StartDate;
            clone.EndDate = EndDate;
            clone.SiCardPlanHeadID = SiCardPlanHeadID;
            clone.LaborOnBoarding = LaborOnBoarding;
            clone.LaborOnDisembarking = LaborOnDisembarking;
            clone.Replacement = Replacement;
            clone.MsBashoID = MsBashoID;

            clone.HeadShimeFlag = HeadShimeFlag;
            
            clone.DeleteFlag = DeleteFlag;
            clone.SendFlag = SendFlag;
            clone.VesselID = VesselID;
            clone.DataNo = DataNo;
            clone.UserKey = UserKey;
            clone.RenewDate = RenewDate;
            clone.RenewUserID = RenewUserID;
            clone.Ts = Ts;

            clone.SeninName = SeninName;
            clone.ShokuName = ShokuName;
            clone.ShokuNameAbbr = ShokuNameAbbr;
            clone.ShokuNameEng = ShokuNameEng;
            clone.VesselName = VesselName;

            return clone;
        }

        
        
    }
}
