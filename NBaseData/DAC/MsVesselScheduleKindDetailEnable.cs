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
    [TableAttribute("MS_VESSEL_SCHEDULE_KIND_DETAIL_ENABLE")]
    public class MsVesselScheduleKindDetailEnable
    {
        #region データメンバ
 
        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }
       
        /// <summary>
        /// スケジュール種別詳細ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SCHEDULE_KIND_DETAIL_ID")]
        public int ScheduleKindDetailID { get; set; }

        /// <summary>
        /// 有効・無効
        /// </summary>
        [DataMember]
        [ColumnAttribute("ENABLED")]
        public bool Enabled { get; set; }




       
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
        /// スケジュール種別詳細名 (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SCHEDULE_KIND_DETAIL_NAME")]
        public string ScheduleKindDetailName { get; set; }


        #endregion


        public MsVesselScheduleKindDetailEnable()
        {
            MsVesselID = 0;
            ScheduleKindDetailID = 0;
            DeleteFlag = false;

            CreateDate = DateTime.MinValue;
            UpdateDate = DateTime.MinValue;
        }

        public static List<MsVesselScheduleKindDetailEnable> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselScheduleKindDetailEnable), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselScheduleKindDetailEnable), "OrderBy");

            List<MsVesselScheduleKindDetailEnable> ret = new List<MsVesselScheduleKindDetailEnable>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselScheduleKindDetailEnable> mapping = new MappingBase<MsVesselScheduleKindDetailEnable>();
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

            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("SCHEDULE_KIND_DETAIL_ID", ScheduleKindDetailID));
            Params.Add(new DBParameter("ENABLED", Enabled));
            
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

            Params.Add(new DBParameter("ENABLED", Enabled));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("UPDATE_MS_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
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
            return CreateDate == DateTime.MinValue && UpdateDate == DateTime.MinValue;
        }
    }
}
