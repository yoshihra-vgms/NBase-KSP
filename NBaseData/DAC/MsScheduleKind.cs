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
    [TableAttribute("MS_SCHEDULE_KIND")]
    public class MsScheduleKind
    {
        #region データメンバ

        /// <summary>
        /// スケジュール種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SCHEDULE_KIND_ID", true)]
        public int ScheduleKindID { get; set; }
        
        /// <summary>
        /// スケジュール区分ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SCHEDULE_CATEGORY_ID")]
        public int ScheduleCategoryID { get; set; }

        /// <summary>
        /// スケジュール種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SCHEDULE_KIND_NAME")]
        public string ScheduleKindName { get; set; }

       
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

        #endregion


        public MsScheduleKind()
        {
            ScheduleCategoryID = 0;
            DeleteFlag = false;
        }

        public static List<MsScheduleKind> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKind), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKind), "OrderBy");

            List<MsScheduleKind> ret = new List<MsScheduleKind>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsScheduleKind> mapping = new MappingBase<MsScheduleKind>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsScheduleKind> SearchRecords(MsUser loginUser, int scheduleCategoryID, string scheduleKindName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKind), "GetRecords");

            List<MsScheduleKind> ret = new List<MsScheduleKind>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsScheduleKind> mapping = new MappingBase<MsScheduleKind>();
            
            if (scheduleCategoryID > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKind), "SearchByCategoryID");
                Params.Add(new DBParameter("SCHEDULE_CATEGORY_ID", scheduleCategoryID));
            }
            if (scheduleKindName != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKind), "SearchByName");
                Params.Add(new DBParameter("SCHEDULE_KIND_NAME", "%" + scheduleKindName + "%"));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsScheduleKind), "OrderBy");

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

            Params.Add(new DBParameter("SCHEDULE_CATEGORY_ID", ScheduleCategoryID));
            Params.Add(new DBParameter("SCHEDULE_KIND_NAME", ScheduleKindName));

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

            Params.Add(new DBParameter("SCHEDULE_CATEGORY_ID", ScheduleCategoryID));
            Params.Add(new DBParameter("SCHEDULE_KIND_NAME", ScheduleKindName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("SCHEDULE_KIND_ID", ScheduleKindID));

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
            Params.Add(new DBParameter("PK", ScheduleKindID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return ScheduleCategoryID == 0;
        }


        public override string ToString()
        {
            return ScheduleKindName;
        }
    }
}
