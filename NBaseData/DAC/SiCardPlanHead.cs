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
    [TableAttribute("SI_CARD_PLAN_HEAD")]
    public class SiCardPlanHead : ISyncTable
    {
        #region データメンバー
        /// <summary>
        /// 配乗計画ヘッダID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_CARD_PLAN_HEAD_ID", true)]
        public string SiCardPlanHeadID { get; set; }

        /// <summary>
        /// 0ならフェリー、1なら内航(RORO 不定期)
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_KIND")]
        public int VesselKind { get; set; }

        /// <summary>
        /// 年月
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEAR_MONTH")]
        public DateTime YearMonth { get; set; }

        /// <summary>
        /// Revision No
        /// </summary>
        [DataMember]
        [ColumnAttribute("REV_NO")]
        public int RevNo { get; set; }

        /// <summary>
        /// 締めフラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHIME_FLAG")]
        public int ShimeFlag { get; set; }

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


        #endregion

        //public const int VESSEL_KIND_フェリー = 0;
        //public const int VESSEL_KIND_内航 = 1;

        public override string ToString()
        {
            return RevNo.ToString();
        }

        /// <summary>
        /// ヘッダを全部取得(最新リビジョンの)
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<SiCardPlanHead> GetRecordsOfLatestRev(MsUser loginUser, int vessel_kind)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCardPlanHead), "GetRecordsOfLatestRev");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCardPlanHead), "OrderByYearMonth");

            List<SiCardPlanHead> ret = new List<SiCardPlanHead>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VESSEL_KIND", vessel_kind));

            MappingBase<SiCardPlanHead> mapping = new MappingBase<SiCardPlanHead>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<SiCardPlanHead> GetRecordsByYearMonth(MsUser loginUser, DateTime dt, int vessel_kind)
        {
            return GetRecordsByYearMonth(null, loginUser, dt, vessel_kind);
        }

        public static List<SiCardPlanHead> GetRecordsByYearMonth(DBConnect dbConnect, MsUser loginUser, DateTime dt, int vessel_kind)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCardPlanHead), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCardPlanHead), "ByYearMonth");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCardPlanHead), "OrderByRevNo");

            List<SiCardPlanHead> ret = new List<SiCardPlanHead>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YEAR_MONTH", dt));
            Params.Add(new DBParameter("VESSEL_KIND", vessel_kind));

            MappingBase<SiCardPlanHead> mapping = new MappingBase<SiCardPlanHead>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static SiCardPlanHead GetRecord(MsUser loginUser, string id, int vessel_kind)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCardPlanHead), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCardPlanHead), "BySiCardPlanHeadID");

            List<SiCardPlanHead> ret = new List<SiCardPlanHead>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_CARD_PLAN_HEAD_ID", id));
            Params.Add(new DBParameter("VESSEL_KIND", vessel_kind));

            MappingBase<SiCardPlanHead> mapping = new MappingBase<SiCardPlanHead>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            
            if (ret != null)
            {
                return ret[0];
            }
            else
            {
                return null;
            }
        }

        public static SiCardPlanHead GetRecord(MsUser loginUser, DateTime dt, int rno)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiCardPlanHead), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCardPlanHead), "ByYearMonth");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiCardPlanHead), "ByRevNo");

            List<SiCardPlanHead> ret = new List<SiCardPlanHead>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YEAR_MONTH", dt));
            Params.Add(new DBParameter("REV_NO", rno));

            MappingBase<SiCardPlanHead> mapping = new MappingBase<SiCardPlanHead>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret != null)
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

            Params.Add(new DBParameter("SI_CARD_PLAN_HEAD_ID", SiCardPlanHeadID));

            Params.Add(new DBParameter("VESSEL_KIND", VesselKind));
            Params.Add(new DBParameter("YEAR_MONTH", YearMonth));
            Params.Add(new DBParameter("REV_NO", RevNo));
            Params.Add(new DBParameter("SHIME_FLAG", ShimeFlag));

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

            Params.Add(new DBParameter("YEAR_MONTH", YearMonth));
            Params.Add(new DBParameter("REV_NO", RevNo));
            Params.Add(new DBParameter("SHIME_FLAG", ShimeFlag));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_CARD_PLAN_HEAD_ID", SiCardPlanHeadID));
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
            Params.Add(new DBParameter("PK", SiCardPlanHeadID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }
        #endregion

        public bool IsNew()
        {
            return SiCardPlanHeadID == null;
        } 
    }
}
