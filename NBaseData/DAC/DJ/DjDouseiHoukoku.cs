using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("DJ_DOUSEI_HOUKOKU")]
    public class DjDouseiHoukoku : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 動静報告ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DJ_DOUSEI_HOUKOKU_ID", true)]
        public string DjDouseiHoukokuID { get; set; }

        /// <summary>
        /// 報告日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("HOUKOKU_DATE")]
        public DateTime HoukokuDate { get; set; }

        /// <summary>
        /// 出港地
        /// </summary>
        [DataMember]
        [ColumnAttribute("LEAVE_PORT_ID")]
        public string LeavePortID { get; set; }

        /// <summary>
        /// 出航日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("LEAVE_DATE")]
        public DateTime LeaveDate { get; set; }


        /// <summary>
        /// 仕向地
        /// </summary>
        [DataMember]
        [ColumnAttribute("DESTINATION_PORT_ID")]
        public string DestinationPortID { get; set; }

        /// <summary>
        /// 入港予定日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("ARRIVAL_DATE")]
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// 現在地
        /// </summary>
        [DataMember]
        [ColumnAttribute("CURRENT_PLACE")]
        public string CurrentPlace { get; set; }
      
        /// <summary>
        /// 天候ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DJ_TENKOU_ID")]
        public string MsDjTenkouID { get; set; }

        /// <summary>
        /// 風向ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DJ_KAZAMUKI_ID")]
        public string MsDjKazamukiID { get; set; }

        /// <summary>
        /// 風速
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUSOKU")]
        public string Fusoku { get; set; }

        /// <summary>
        /// 波
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAMI")]
        public string Nami { get; set; }

        /// <summary>
        /// うねり
        /// </summary>
        [DataMember]
        [ColumnAttribute("UNERI")]
        public string Uneri { get; set; }

        /// <summary>
        /// 視程
        /// </summary>
        [DataMember]
        [ColumnAttribute("SITEI")]
        public string Sitei { get; set; }

        /// <summary>
        /// 針路
        /// </summary>
        [DataMember]
        [ColumnAttribute("SINRO")]
        public string Sinro { get; set; }

        /// <summary>
        /// 速力
        /// </summary>
        [DataMember]
        [ColumnAttribute("SOKURYOKU")]
        public string Sokuryoku { get; set; }

        /// <summary>
        /// 船体設備状況ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DJ_SENTAISETSUBI_ID")]
        public string MsDjSentaisetsubiID { get; set; }

        /// <summary>
        /// 乗組員健康状態ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DJ_KENKOUJYOUTAI_ID")]
        public string MsDjKenkoujyoutaiID { get; set; }

        /// <summary>
        /// 乗組員数
        /// </summary>
        [DataMember]
        [ColumnAttribute("NORIKUMIINSU")]
        public string Norikumiinsu { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }
        

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

        public DjDouseiHoukoku()
        {
            DjDouseiHoukokuID = null;
            HoukokuDate = DateTime.Now;
        }

        public static DjDouseiHoukoku GetRecord(MsUser loginUser, string djDouseiHoukokuID)
        {
            return GetRecord(null, loginUser, djDouseiHoukokuID);
        }
        public static DjDouseiHoukoku GetRecord(DBConnect dbConnect, MsUser loginUser, string djDouseiHoukokuID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "ByDjDouseiHoukokuID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "OrderBy");

            List<DjDouseiHoukoku> ret = new List<DjDouseiHoukoku>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DJ_DOUSEI_HOUKOKU_ID", djDouseiHoukokuID));

            MappingBase<DjDouseiHoukoku> mapping = new MappingBase<DjDouseiHoukoku>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count<DjDouseiHoukoku>() > 0)
                return ret.First<DjDouseiHoukoku>();
            else
                return null;
        }

        public static List<DjDouseiHoukoku> GetRecordsByHoukokuDate(MsUser loginUser, DateTime houkokuDate)
        {
            return GetRecordsByHoukokuDate(null, loginUser, houkokuDate);
        }
        public static List<DjDouseiHoukoku> GetRecordsByHoukokuDate(DBConnect dbConnect, MsUser loginUser, DateTime houkokuDate)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "ByHoukokuDate");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "OrderBy");

            List<DjDouseiHoukoku> ret = new List<DjDouseiHoukoku>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("HOUKOKU_DATE", houkokuDate.ToShortDateString()));

            MappingBase<DjDouseiHoukoku> mapping = new MappingBase<DjDouseiHoukoku>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DjDouseiHoukoku> GetRecordsByHoukokuDate(MsUser loginUser, int msVesselID, DateTime fromDate, DateTime toDate)
        {
            return GetRecordsByHoukokuDate(null, loginUser, msVesselID, fromDate, toDate);
        }
        public static List<DjDouseiHoukoku> GetRecordsByHoukokuDate(DBConnect dbConnect, MsUser loginUser, int msVesselID, DateTime fromDate, DateTime toDate)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "GetRecords");
            ParameterConnection Params = new ParameterConnection();
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "ByVessel");
            Params.Add(new DBParameter("VESSEL_ID", msVesselID));
            if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "ByHoukokuDateFromTo");
                Params.Add(new DBParameter("FROM_DATE", fromDate.ToShortDateString()));
                Params.Add(new DBParameter("TO_DATE", toDate.ToShortDateString()));
            }
            else if (fromDate != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "ByHoukokuDateFrom");
                Params.Add(new DBParameter("FROM_DATE", fromDate.ToShortDateString()));
            }
            else
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "ByHoukokuDateTo");
                Params.Add(new DBParameter("TO_DATE", toDate.ToShortDateString()));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), "OrderBy");

            List<DjDouseiHoukoku> ret = new List<DjDouseiHoukoku>();

            MappingBase<DjDouseiHoukoku> mapping = new MappingBase<DjDouseiHoukoku>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("DJ_DOUSEI_HOUKOKU_ID", DjDouseiHoukokuID));
            Params.Add(new DBParameter("HOUKOKU_DATE", HoukokuDate));
            Params.Add(new DBParameter("LEAVE_PORT_ID", LeavePortID));
            Params.Add(new DBParameter("LEAVE_DATE", LeaveDate));
            Params.Add(new DBParameter("DESTINATION_PORT_ID", DestinationPortID));
            Params.Add(new DBParameter("ARRIVAL_DATE", ArrivalDate));
            Params.Add(new DBParameter("CURRENT_PLACE", CurrentPlace));
            Params.Add(new DBParameter("MS_DJ_TENKOU_ID", MsDjTenkouID));
            Params.Add(new DBParameter("MS_DJ_KAZAMUKI_ID", MsDjKazamukiID));
            Params.Add(new DBParameter("FUSOKU", Fusoku));
            Params.Add(new DBParameter("NAMI", Nami));
            Params.Add(new DBParameter("UNERI", Uneri));
            Params.Add(new DBParameter("SITEI", Sitei));
            Params.Add(new DBParameter("SINRO", Sinro));
            Params.Add(new DBParameter("SOKURYOKU", Sokuryoku));
            Params.Add(new DBParameter("MS_DJ_SENTAISETSUBI_ID", MsDjSentaisetsubiID));
            Params.Add(new DBParameter("MS_DJ_KENKOUJYOUTAI_ID", MsDjKenkoujyoutaiID));
            Params.Add(new DBParameter("NORIKUMIINSU", Norikumiinsu));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect,loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }
        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjDouseiHoukoku), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("HOUKOKU_DATE", HoukokuDate));
            Params.Add(new DBParameter("LEAVE_PORT_ID", LeavePortID));
            Params.Add(new DBParameter("LEAVE_DATE", LeaveDate));
            Params.Add(new DBParameter("DESTINATION_PORT_ID", DestinationPortID));
            Params.Add(new DBParameter("ARRIVAL_DATE", ArrivalDate));
            Params.Add(new DBParameter("CURRENT_PLACE", CurrentPlace));
            Params.Add(new DBParameter("MS_DJ_TENKOU_ID", MsDjTenkouID));
            Params.Add(new DBParameter("MS_DJ_KAZAMUKI_ID", MsDjKazamukiID));
            Params.Add(new DBParameter("FUSOKU", Fusoku));
            Params.Add(new DBParameter("NAMI", Nami));
            Params.Add(new DBParameter("UNERI", Uneri));
            Params.Add(new DBParameter("SITEI", Sitei));
            Params.Add(new DBParameter("SINRO", Sinro));
            Params.Add(new DBParameter("SOKURYOKU", Sokuryoku));
            Params.Add(new DBParameter("MS_DJ_SENTAISETSUBI_ID", MsDjSentaisetsubiID));
            Params.Add(new DBParameter("MS_DJ_KENKOUJYOUTAI_ID", MsDjKenkoujyoutaiID));
            Params.Add(new DBParameter("NORIKUMIINSU", Norikumiinsu));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("DJ_DOUSEI_HOUKOKU_ID", DjDouseiHoukokuID));
            Params.Add(new DBParameter("TS", Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect,loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", DjDouseiHoukokuID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return DjDouseiHoukokuID == null;
        }
    }
}
