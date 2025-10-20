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
    [TableAttribute("WORK")]
    public class Work
    {

        public class WorkContentDetail
        {
            public DateTime WorkDate { get; set; }
            public string WorkContentID { get; set; }
            public bool NightTime { get; set; } = false;

        }





        #region データメンバ
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("WORK_ID")]
        public int WorkID { get; set; }



        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("WTM_WORK_ID")]
        public string WtmWorkID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREW_NO")]
        public string CrewNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_WORK")]
        public DateTime StartWork { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("FINISH_WORK")]
        public DateTime FinishWork { get; set; }




        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

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
        [ColumnAttribute("SI_CARD_ID")]
        public string SiCardID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATE_DIFF")]
        public int DateDiff { get; set; }






        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_REG_HOSTNAME")]
        public string StartRegHostname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_REG_DATE")]
        public DateTime StartRegDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_REG_STATUS")]
        public bool StartRegStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_REG_ERROR_MESSAGE")]
        public string StartRegErrorMessage { get; set; }



        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("FINISH_REG_HOSTNAME")]
        public string FinishRegHostname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("FINISH_REG_DATE")]
        public DateTime FinishRegDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("FINISH_REG_STATUS")]
        public bool FinishRegStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("FINISH_REG_ERROR_MESSAGE")]
        public string FinishRegErrorMessage { get; set; }










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



        public List<WorkContentDetail> WorkContentDetails { get; set; } = new List<WorkContentDetail>();

        #endregion



        public Work()
        {
            WorkID = 0;
            WtmWorkID = null;
        }


        public static List<Work> GetStartWorkRecords(MsUser loginUser, string crewNo)
        {
            List<Work> ret = new List<Work>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(Work), "GetStartWorkRecords");

            ParameterConnection Params = new ParameterConnection();
            MappingBase<Work> mapping = new MappingBase<Work>();

            Params.Add(new DBParameter("CREW_NO", crewNo));


            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }




        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(Work), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("WTM_WORK_ID", this.WtmWorkID));
            Params.Add(new DBParameter("CREW_NO", this.CrewNo));
            Params.Add(new DBParameter("START_WORK", this.StartWork));
            Params.Add(new DBParameter("FINISH_WORK", this.FinishWork));

            Params.Add(new DBParameter("MS_SENIN_ID", this.MsSeninID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", this.MsSiShokuemiID));
            Params.Add(new DBParameter("SI_CARD_ID", this.SiCardID));
            Params.Add(new DBParameter("DATE_DIFF", this.DateDiff));

            Params.Add(new DBParameter("START_REG_HOSTNAME", this.StartRegHostname));
            Params.Add(new DBParameter("START_REG_DATE", this.StartRegDate));
            Params.Add(new DBParameter("START_REG_STATUS", this.StartRegStatus));
            Params.Add(new DBParameter("START_REG_ERROR_MESSAGE", this.StartRegErrorMessage));

            Params.Add(new DBParameter("FINISH_REG_HOSTNAME", this.FinishRegHostname));
            Params.Add(new DBParameter("FINISH_REG_DATE", this.FinishRegDate));
            Params.Add(new DBParameter("FINISH_REG_STATUS", this.FinishRegStatus));
            Params.Add(new DBParameter("FINISH_REG_ERROR_MESSAGE", this.FinishRegErrorMessage));

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

            this.WorkID = Sequences.GetSequenceId(dbConnect, loginUser, "SEQ_WORK_ID");

            return true;
        }



        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }
        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(Work), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("WTM_WORK_ID", this.WtmWorkID));
            Params.Add(new DBParameter("CREW_NO", this.CrewNo));
            Params.Add(new DBParameter("START_WORK", this.StartWork));
            Params.Add(new DBParameter("FINISH_WORK", this.FinishWork));

            Params.Add(new DBParameter("MS_SENIN_ID", this.MsSeninID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", this.MsSiShokuemiID));
            Params.Add(new DBParameter("SI_CARD_ID", this.SiCardID));
            Params.Add(new DBParameter("DATE_DIFF", this.DateDiff));

            Params.Add(new DBParameter("START_REG_HOSTNAME", this.StartRegHostname));
            Params.Add(new DBParameter("START_REG_DATE", this.StartRegDate));
            Params.Add(new DBParameter("START_REG_STATUS", this.StartRegStatus));
            Params.Add(new DBParameter("START_REG_ERROR_MESSAGE", this.StartRegErrorMessage));

            Params.Add(new DBParameter("FINISH_REG_HOSTNAME", this.FinishRegHostname));
            Params.Add(new DBParameter("FINISH_REG_DATE", this.FinishRegDate));
            Params.Add(new DBParameter("FINISH_REG_STATUS", this.FinishRegStatus));
            Params.Add(new DBParameter("FINISH_REG_ERROR_MESSAGE", this.FinishRegErrorMessage));

            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("DATA_NO", this.DataNo));
            Params.Add(new DBParameter("TS", this.Ts));

            Params.Add(new DBParameter("WORK_ID", this.WorkID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

    }
}
