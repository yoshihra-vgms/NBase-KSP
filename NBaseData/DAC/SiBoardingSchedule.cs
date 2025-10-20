using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_BOARDING_SCHEDULE")]
    public class SiBoardingSchedule : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 乗船予定ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_BOARDING_SCHEDULE_ID", true)]
        public string SiBoardingScheduleID { get; set; }

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 職名ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }


        /// <summary>
        /// 船ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 乗船予定日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_ON_DATE")]
        public DateTime SignOnDate { get; set; }
       
        /// <summary>
        /// 下船予定者の船員カードID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_OFF_SI_CARD_ID")]
        public string SignOffSiCardID { get; set; }

        /// <summary>
        /// 状態 0:予定、 1:実績
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS")]
        public int Status { get; set; }


        /// <summary>
        /// 乗船予定者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_ON_CREW_NAME")]
        public string SignOnCrewName { get; set; }


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




        public SiBoardingSchedule()
        {
            this.MsSiShokumeiID = Int32.MinValue;
            this.MsSeninID = Int32.MinValue;
            this.MsVesselID = Int32.MinValue;
            this.SignOnDate = DateTime.MinValue;
        }

        public static List<SiBoardingSchedule> GetRecordsByPlan(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiBoardingSchedule), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiBoardingSchedule), "ByPlan");

            List<SiBoardingSchedule> ret = new List<SiBoardingSchedule>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiBoardingSchedule> mapping = new MappingBase<SiBoardingSchedule>();

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);
            return ret;
        }    

        public static SiBoardingSchedule GetRecordByBoardingScheduleID(ORMapping.DBConnect dbConnect, MsUser loginUser, string boardingScheduleId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiBoardingSchedule), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiBoardingSchedule), "ByBoardingScheduleID");

            List<SiBoardingSchedule> ret = new List<SiBoardingSchedule>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiBoardingSchedule> mapping = new MappingBase<SiBoardingSchedule>();
            Params.Add(new DBParameter("SI_BOARDING_SCHEDULE_ID", boardingScheduleId));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);
            if (ret.Count() > 0)
                return ret.First();
            else
                return null;
        }

        public static SiBoardingSchedule GetRecordBySignOffSiCardID(ORMapping.DBConnect dbConnect, MsUser loginUser, string cardId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiBoardingSchedule), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiBoardingSchedule), "BySignOffSiCardID");

            List<SiBoardingSchedule> ret = new List<SiBoardingSchedule>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiBoardingSchedule> mapping = new MappingBase<SiBoardingSchedule>();
            Params.Add(new DBParameter("SIGN_OFF_SI_CARD_ID", cardId));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);
            if (ret.Count() > 0)
                return ret.First();
            else
                return null;
        }


        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }




        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_BOARDING_SCHEDULE_ID", SiBoardingScheduleID));

            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("SIGN_ON_DATE", SignOnDate));         
            Params.Add(new DBParameter("SIGN_OFF_SI_CARD_ID", SignOffSiCardID));
            Params.Add(new DBParameter("STATUS", Status));

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

            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("SIGN_ON_DATE", SignOnDate));
            Params.Add(new DBParameter("SIGN_OFF_SI_CARD_ID", SignOffSiCardID));
            Params.Add(new DBParameter("STATUS", Status));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_BOARDING_SCHEDULE_ID", SiBoardingScheduleID));
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
            Params.Add(new DBParameter("PK", SiBoardingScheduleID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiBoardingScheduleID == null;
        }
    }
}
