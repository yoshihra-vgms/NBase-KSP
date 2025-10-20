using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using WingData.DS;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace WingData.DAC
{
    [DataContract()]
    [TableAttribute("SI_SIMULATION_HEADER")]
    public class SiSimulationHeader : ISyncTable
    {
        public static int PromStatus = -1;
        public enum EnumStatus { 保存, 承認依頼, 実績 };

        #region データメンバ

        /// <summary>
        /// 交代計画ヘッダID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SIMULATION_HEADER_ID", true)]
        public string SiSimulationHeaderID { get; set; }

        /// <summary>
        /// 交代日
        /// </summary>
        [DataMember]
        [ColumnAttribute("CHANGE_DATE")]
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// 船ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 場所ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BASHO_ID")]
        public string MsBashoID { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS")]
        public int Status { get; set; }
        

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



        [DataMember]
        public List<SiSimulationDetail> Details;


        #endregion



        public SiSimulationHeader()
        {
            this.MsVesselID = Int32.MinValue;
            this.Details = new List<SiSimulationDetail>();
        }
        

        public static List<SiSimulationHeader> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSimulationHeader), MethodBase.GetCurrentMethod());

            List<SiSimulationHeader> ret = new List<SiSimulationHeader>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSimulationHeader> mapping = new MappingBase<SiSimulationHeader>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiSimulationHeader> SerachRecords(MsUser loginUser, DateTime dateFrom, DateTime dateTo, int msVesselId, string msBashoId, int status)
        {
            return SerachRecords(null, loginUser, dateFrom, dateTo, msVesselId, msBashoId, status);
        }
        public static List<SiSimulationHeader> SerachRecords(DBConnect dbConnect, MsUser loginUser, DateTime dateFrom, DateTime dateTo, int msVesselId, string msBashoId, int status)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSimulationHeader), "GetRecords");

            List<SiSimulationHeader> ret = new List<SiSimulationHeader>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSimulationHeader> mapping = new MappingBase<SiSimulationHeader>();

            if (dateFrom != DateTime.MinValue && dateTo != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationHeader), "ByDateFromTo");
                Params.Add(new DBParameter("DATE_FROM", dateFrom));
                Params.Add(new DBParameter("DATE_TO", dateTo));
            }
            else if (dateFrom != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationHeader), "ByDateFrom");
                Params.Add(new DBParameter("DATE_FROM", dateFrom.ToShortDateString()));
            }
            else if (dateTo != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationHeader), "ByDateTo");
                Params.Add(new DBParameter("DATE_TO", dateTo.ToShortDateString()));
            }
            if (msVesselId > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationHeader), "ByMsVesselID");
                Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            }

            if (msBashoId != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationHeader), "ByMsBashoID");
                Params.Add(new DBParameter("MS_BASHO_ID", msBashoId));
            }

            if (status == (int)EnumStatus.実績)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationHeader), "By実績");
                Params.Add(new DBParameter("STATUS", (int)EnumStatus.実績));
            }
            else
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationHeader), "By実績以外");
                Params.Add(new DBParameter("STATUS", (int)EnumStatus.実績));
            }


            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);
Debug.WriteLine("SQL:" + SQL);
            foreach (SiSimulationHeader h in ret)
            {
                h.Details = SiSimulationDetail.GetRecordsByHeaderId(loginUser, h.SiSimulationHeaderID);
            }

            return ret;
        }



        public static SiSimulationHeader GetRecord(DBConnect dbConnect, MsUser loginUser, DateTime date, int msVesselId, string msBashoId)
        {
            List<SiSimulationHeader> ret = SerachRecords(dbConnect, loginUser, date, date, msVesselId, msBashoId, -1);


            if (ret.Count == 0)
                return null;
            return ret[0];
        }
        public static SiSimulationHeader GetRecord(MsUser loginUser, string siSimulationHeaderId)
        {
            return GetRecord(null, loginUser, siSimulationHeaderId);
        }
        public static SiSimulationHeader GetRecord(DBConnect dbConnect, MsUser loginUser, string siSimulationHeaderId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSimulationHeader), "GetRecords");

            List<SiSimulationHeader> ret = new List<SiSimulationHeader>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSimulationHeader> mapping = new MappingBase<SiSimulationHeader>();

            if (siSimulationHeaderId != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationDetail), "BySiSimulationHeaderID");
                Params.Add(new DBParameter("SI_SIMULATION_HEADER_ID", siSimulationHeaderId));
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

            Params.Add(new DBParameter("SI_SIMULATION_HEADER_ID", SiSimulationHeaderID));

            Params.Add(new DBParameter("CHANGE_DATE", ChangeDate));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_BASHO_ID", MsBashoID));
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

            Params.Add(new DBParameter("CHANGE_DATE", ChangeDate));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_BASHO_ID", MsBashoID));
            Params.Add(new DBParameter("STATUS", Status));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_SIMULATION_HEADER_ID", SiSimulationHeaderID));
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
            Params.Add(new DBParameter("PK", SiSimulationHeaderID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiSimulationHeaderID == null;
        }


        public SiSimulationHeader Clone()
        {
            SiSimulationHeader clone = new SiSimulationHeader();
            clone.SiSimulationHeaderID = SiSimulationHeaderID;
            clone.ChangeDate = ChangeDate;
            clone.MsVesselID = MsVesselID;
            clone.MsBashoID = MsBashoID;
            clone.Status = Status;

            clone.DeleteFlag = DeleteFlag;
            clone.SendFlag = SendFlag;
            clone.VesselID = VesselID;
            clone.DataNo = DataNo;
            clone.UserKey = UserKey;
            clone.RenewDate = RenewDate;
            clone.RenewUserID = RenewUserID;
            clone.Ts = Ts;

            return clone;
        }
     }
}
