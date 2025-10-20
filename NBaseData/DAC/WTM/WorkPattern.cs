using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("WORK_PATTERN")]
    public class WorkPattern
    {
        public class WorkPatternEventKind
        {
            public int Kind { set; get; }
            public string Name { set; get; }
            public bool Pattern24 { set; get; }

            public override string ToString()
            {
                return Name;
            }

            public WorkPatternEventKind(int kind, string name, bool pattern24)
            {
                Kind = kind;
                Name = name;
                Pattern24 = pattern24;
            }


            private static List<WorkPatternEventKind> EventKindList = null;

            public static List<WorkPatternEventKind> List()
            {
                if (EventKindList == null)
                {
                    EventKindList = new List<WorkPatternEventKind>();
                    EventKindList.Add(new WorkPatternEventKind(0, "航海中", true));
                }

                return EventKindList;
            }

            public static int Kind航海中
            {
                get { return List().Where(o => o.Name == "航海中").First().Kind; }
            }
        }



        #region データメンバ
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("WORK_PATTERN_ID")]
        public int WorkPatternID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("GID")]
        public string GID { get; set; }


        /// <summary>
        /// イベント種別 ０：航海中
        /// </summary>
        [DataMember]
        [ColumnAttribute("EVENT_KIND")]
        public int EventKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokuemiID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("WORK_DATE")]
        public DateTime WorkDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("WORK_DATE_DIFF")]
        public int WorkDateDiff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("WORK_CONTENT_ID")]
        public string WorkContentID { get; set; }




        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }


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
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public int DataNo { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }

        #endregion

        public WorkPattern()
        {
            WorkPatternID = 0;
            GID = null;
        }

        public static List<WorkPattern> GetRecords(MsUser loginUser, int eventKind, int msVesselID = 0)
        {
            List<WorkPattern> ret = new List<WorkPattern>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(WorkPattern), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(WorkPattern), "ByEventKind");


            ParameterConnection Params = new ParameterConnection();
            MappingBase<WorkPattern> mapping = new MappingBase<WorkPattern>();

            Params.Add(new DBParameter("EVENT_KIND", eventKind));


            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            if (msVesselID > 0)
            {
                ret = ret.Where(o => o.MsVesselID == msVesselID).ToList();
            }

            return ret;
        }



        public static List<WorkPattern> GetRecordsByVesselAndShokumei(MsUser loginUser, int msVesselID, int msSiShokumeiID)
        {
            List<WorkPattern> ret = new List<WorkPattern>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(WorkPattern), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(WorkPattern), "ByEventKind");


            ParameterConnection Params = new ParameterConnection();
            MappingBase<WorkPattern> mapping = new MappingBase<WorkPattern>();

            Params.Add(new DBParameter("EVENT_KIND", WorkPatternEventKind.Kind航海中));


            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            if (msVesselID > 0)
            {
                ret = ret.Where(o => o.MsVesselID == msVesselID).ToList();
            }
            if (msSiShokumeiID > 0)
            {
                ret = ret.Where(o => o.MsSiShokuemiID == msSiShokumeiID).ToList();
            }

            return ret;
        }



        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(WorkPattern), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("GID", this.GID));
            Params.Add(new DBParameter("EVENT_KIND", this.EventKind));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", this.MsSiShokuemiID));
            Params.Add(new DBParameter("WORK_DATE", this.WorkDate));
            Params.Add(new DBParameter("WORK_DATE_DIFF", this.WorkDateDiff));
            Params.Add(new DBParameter("WORK_CONTENT_ID", this.WorkContentID));

            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("DATA_NO", this.DataNo));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }
        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(WorkPattern), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("GID", this.GID));
            Params.Add(new DBParameter("EVENT_KIND", this.EventKind));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", this.MsSiShokuemiID));
            Params.Add(new DBParameter("WORK_DATE", this.WorkDate));
            Params.Add(new DBParameter("WORK_DATE_DIFF", this.WorkDateDiff));
            Params.Add(new DBParameter("WORK_CONTENT_ID", this.WorkContentID));

            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("DATA_NO", this.DataNo));
            Params.Add(new DBParameter("TS", this.Ts));

            Params.Add(new DBParameter("WORK_PATTERN_ID", this.WorkPatternID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public static bool DeleteRecords(MsUser loginUser, string gid)
        {
            List<WorkPattern> ret = new List<WorkPattern>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(WorkPattern), "DeleteRecords");


            ParameterConnection Params = new ParameterConnection();
            MappingBase<WorkPattern> mapping = new MappingBase<WorkPattern>();

            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("GID", gid));


            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }
    }
}
