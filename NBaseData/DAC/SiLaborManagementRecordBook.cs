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
    [TableAttribute("SI_LABOR_MANAGEMENT_RECORD_BOOK")]
    public class SiLaborManagementRecordBook : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 労務管理記録簿ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_LABOR_MANAGEMENT_RECORD_BOOK_ID", true)]
        public string SiLaborManagementRecordBookID { get; set; }


        /// <summary>
        /// 時間外労働協定の有無
        /// </summary>
        [DataMember]
        [ColumnAttribute("OVERTIME_WORK_AGREEMENT")]
        public int OvertimeWorkAgreement { get; set; }

        /// <summary>
        /// 補償休日労働協定の有無
        /// </summary>
        [DataMember]
        [ColumnAttribute("COMPENSATION_HOLIDAY_LABOR_AGREEMENT")]
        public int CompensationHolidayLaborAgreement { get; set; }

        /// <summary>
        /// 休息時間分割協定の有無
        /// </summary>
        [DataMember]
        [ColumnAttribute("BREAK_TIME_DIVISION_AGREEMENT")]
        public int BreakTimeDivisionAgreement { get; set; }




        /// <summary>
        /// 基準労働期間
        /// </summary>
        [DataMember]
        [ColumnAttribute("STANDARD_WORKING_PERIOD")]
        public int StandardWorkingPeriod { get; set; }

        /// <summary>
        /// 基準労働期間の起算日
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_STANDARD_WORKING_PERIOD")]
        public DateTime StartStandardWorkingPeriod { get; set; }

        /// <summary>
        /// 基準労働期間の末日
        /// </summary>
        [DataMember]
        [ColumnAttribute("LAST_STANDARD_WORKING_PERIOD")]
        public DateTime LastStandardWorkingPeriod { get; set; }



        
        
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




        public SiLaborManagementRecordBook()
        {
            this.SiLaborManagementRecordBookID = null;
        }



        public static SiLaborManagementRecordBook GetRecord(NBaseData.DAC.MsUser loginUser, string SiLaborManagementRecordBookID = null)
        {
            return GetRecord(null, loginUser, SiLaborManagementRecordBookID);
        }

        public static SiLaborManagementRecordBook GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string SiLaborManagementRecordBookID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiLaborManagementRecordBook), "GetRecords");

            List<SiLaborManagementRecordBook> ret = new List<SiLaborManagementRecordBook>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiLaborManagementRecordBook> mapping = new MappingBase<SiLaborManagementRecordBook>();
            if (SiLaborManagementRecordBookID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiLaborManagementRecordBook), "BySiLaborManagementRecordBookID");
                Params.Add(new DBParameter("SI_LABOR_MANAGEMENT_RECORD_BOOK_ID", SiLaborManagementRecordBookID));
            }

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
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

            Params.Add(new DBParameter("SI_LABOR_MANAGEMENT_RECORD_BOOK_ID", SiLaborManagementRecordBookID));

            Params.Add(new DBParameter("OVERTIME_WORK_AGREEMENT", OvertimeWorkAgreement));
            Params.Add(new DBParameter("COMPENSATION_HOLIDAY_LABOR_AGREEMENT", CompensationHolidayLaborAgreement));
            Params.Add(new DBParameter("BREAK_TIME_DIVISION_AGREEMENT", BreakTimeDivisionAgreement));
           
            Params.Add(new DBParameter("STANDARD_WORKING_PERIOD", StandardWorkingPeriod));
            Params.Add(new DBParameter("START_STANDARD_WORKING_PERIOD", StartStandardWorkingPeriod));
            Params.Add(new DBParameter("LAST_STANDARD_WORKING_PERIOD", LastStandardWorkingPeriod));

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

            Params.Add(new DBParameter("OVERTIME_WORK_AGREEMENT", OvertimeWorkAgreement));
            Params.Add(new DBParameter("COMPENSATION_HOLIDAY_LABOR_AGREEMENT", CompensationHolidayLaborAgreement));
            Params.Add(new DBParameter("BREAK_TIME_DIVISION_AGREEMENT", BreakTimeDivisionAgreement));

            Params.Add(new DBParameter("STANDARD_WORKING_PERIOD", StandardWorkingPeriod));
            Params.Add(new DBParameter("START_STANDARD_WORKING_PERIOD", StartStandardWorkingPeriod));
            Params.Add(new DBParameter("LAST_STANDARD_WORKING_PERIOD", LastStandardWorkingPeriod));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_LABOR_MANAGEMENT_RECORD_BOOK_ID", SiLaborManagementRecordBookID));
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
            Params.Add(new DBParameter("PK", SiLaborManagementRecordBookID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiLaborManagementRecordBookID == null;
        }
    }
}
