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
    [TableAttribute("SI_PERSONAL_SCHEDULE")]
    public class SiPersonalSchedule : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 乗り合わせID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_PERSONAL_SCHEDULE_ID", true)]
        public string SiPersonalScheduleID { get; set; }



        /// <summary>
        /// 職名ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 開始日
        /// </summary>
        [DataMember]
        [ColumnAttribute("FROM_DATE")]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        [DataMember]
        [ColumnAttribute("TO_DATE")]
        public DateTime ToDate { get; set; }
       
        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 船員氏名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }


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




        public SiPersonalSchedule()
        {
            this.MsSiShokumeiID = Int32.MinValue;
            this.MsSeninID = Int32.MinValue;
            this.FromDate = DateTime.MinValue;
            this.ToDate = DateTime.MinValue;
        }

        
        

        public static List<SiPersonalSchedule> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiPersonalSchedule), MethodBase.GetCurrentMethod());

            List<SiPersonalSchedule> ret = new List<SiPersonalSchedule>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiPersonalSchedule> mapping = new MappingBase<SiPersonalSchedule>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<SiPersonalSchedule> GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiPersonalSchedule), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiPersonalSchedule), "ByMsSeninID");

            List<SiPersonalSchedule> ret = new List<SiPersonalSchedule>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiPersonalSchedule> mapping = new MappingBase<SiPersonalSchedule>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiPersonalSchedule> SearchRecords(MsUser loginUser, int msSiShokumeiId, string name, DateTime fromDate, DateTime toDate)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiPersonalSchedule), "GetRecords");

            if (msSiShokumeiId > 0)
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiPersonalSchedule), "ByMsSiShokumeiID");

            if (name != null)
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiPersonalSchedule), "ByName");

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiPersonalSchedule), "ByFromTo");



            List<SiPersonalSchedule> ret = new List<SiPersonalSchedule>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiPersonalSchedule> mapping = new MappingBase<SiPersonalSchedule>();

            if (msSiShokumeiId > 0)
            {
                Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", msSiShokumeiId));
            }
            if (name != null)
            {
                Params.Add(new DBParameter("NAME", "%" + name + "%"));
            }

            Params.Add(new DBParameter("FROM_DATE", fromDate));
            Params.Add(new DBParameter("TO_DATE", toDate));


            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
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

            Params.Add(new DBParameter("SI_PERSONAL_SCHEDULE_ID", SiPersonalScheduleID));

            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("FROM_DATE", FromDate));
            Params.Add(new DBParameter("TO_DATE", ToDate));         
            Params.Add(new DBParameter("BIKOU", Bikou));

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
            Params.Add(new DBParameter("FROM_DATE", FromDate));
            Params.Add(new DBParameter("TO_DATE", ToDate));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_PERSONAL_SCHEDULE_ID", SiPersonalScheduleID));
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
            Params.Add(new DBParameter("PK", SiPersonalScheduleID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiPersonalScheduleID == null;
        }
    }
}
