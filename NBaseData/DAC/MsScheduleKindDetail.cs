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
    [TableAttribute("MS_SCHEDULE_KIND_DETAIL")]
    public class MsScheduleKindDetail
    {
        #region データメンバ

        /// <summary>
        /// スケジュール種別詳細ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SCHEDULE_KIND_DETAIL_ID", true)]
        public int ScheduleKindDetailID { get; set; }
 
        /// <summary>
        /// スケジュール種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SCHEDULE_KIND_ID")]
        public int ScheduleKindID { get; set; }
       
        /// <summary>
        /// スケジュール種別詳細名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SCHEDULE_KIND_DETAIL_NAME")]
        public string ScheduleKindDetailName { get; set; }

        /// <summary>
        /// 色
        /// </summary>
        [DataMember]
        [ColumnAttribute("COLOR_R")]
        public int ColorR { get; set; }

        /// <summary>
        /// 色
        /// </summary>
        [DataMember]
        [ColumnAttribute("COLOR_G")]
        public int ColorG { get; set; }

        /// <summary>
        /// 色
        /// </summary>
        [DataMember]
        [ColumnAttribute("COLOR_B")]
        public int ColorB { get; set; }




       
        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public bool DeleteFlag { get; set; }

        /// <summary>
        /// 作成者
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREATE_MS_USER_ID")]
        public string CreateMsUserID { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDATE_MS_USER_ID")]
        public string UpdateMsUserID { get; set; }

        /// <summary>
        /// 作成日
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }



        /// <summary>
        /// スケジュール種別名 (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SCHEDULE_KIND_NAME")]
        public string ScheduleKindName { get; set; }

        /// <summary>
        /// スケジュール区分ID(INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SCHEDULE_CATEGORY_ID")]
        public int ScheduleCategoryID { get; set; }



        #endregion


        public MsScheduleKindDetail()
        {
            ScheduleKindDetailID = 0;
            DeleteFlag = false;
        }

        public static List<MsScheduleKindDetail> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKindDetail), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKindDetail), "OrderBy");

            List<MsScheduleKindDetail> ret = new List<MsScheduleKindDetail>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsScheduleKindDetail> mapping = new MappingBase<MsScheduleKindDetail>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsScheduleKindDetail> SearchRecords(MsUser loginUser, int scheduleKindID, string scheduleKindDetailName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKindDetail), "GetRecords");

            List<MsScheduleKindDetail> ret = new List<MsScheduleKindDetail>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsScheduleKindDetail> mapping = new MappingBase<MsScheduleKindDetail>();

            if (scheduleKindID > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKindDetail), "SearchByKindID");
                Params.Add(new DBParameter("SCHEDULE_KIND_ID", scheduleKindID));
            }
            if (scheduleKindDetailName != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKindDetail), "SearchByName");
                Params.Add(new DBParameter("SCHEDULE_KIND_DETAIL_NAME", "%" + scheduleKindDetailName + "%"));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKindDetail), "OrderBy");

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

            Params.Add(new DBParameter("SCHEDULE_KIND_ID", ScheduleKindID));
            Params.Add(new DBParameter("SCHEDULE_KIND_DETAIL_NAME", ScheduleKindDetailName));
            Params.Add(new DBParameter("COLOR_R", ColorR));
            Params.Add(new DBParameter("COLOR_G", ColorG));
            Params.Add(new DBParameter("COLOR_B", ColorB));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("CREATE_MS_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SCHEDULE_KIND_ID", ScheduleKindID));
            Params.Add(new DBParameter("SCHEDULE_KIND_DETAIL_NAME", ScheduleKindDetailName));
            Params.Add(new DBParameter("COLOR_R", ColorR));
            Params.Add(new DBParameter("COLOR_G", ColorG));
            Params.Add(new DBParameter("COLOR_B", ColorB));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("SCHEDULE_KIND_DETAIL_ID", ScheduleKindDetailID));

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
            Params.Add(new DBParameter("PK", ScheduleKindDetailID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return ScheduleKindDetailID == 0;
        }


        public override string ToString()
        {
            return ScheduleKindDetailName;
        }
    }
}
