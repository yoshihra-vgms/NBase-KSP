using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;
using ORMapping.Atts;
using NBaseData.DS;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_LO_VESSEL")]
    public class MsLoVessel : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 潤滑油船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_VESSEL_ID", true)]
        public string MsLoVesselID { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 潤滑油ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_ID")]
        public string MsLoID { get; set; }

        /// <summary>
        /// 潤滑油名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_NAME")]
        public string MsLoName { get; set; }

        /// <summary>
        /// 送信フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        /// <summary>
        /// データNo
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
        /// 更新者(UserID)
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

        /// <summary>
        /// 単位ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_ID")]
        public string MsTaniID { get; set; }

        /// <summary>
        /// 単位名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_NAME")]
        public string MsTaniName { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NAME")]
        public string VesselName { get; set; }
        #endregion

        public MsLoVessel()
        {
        }

        public static List<MsLoVessel> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), "OrderBy");
            List<MsLoVessel> ret = new List<MsLoVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsLoVessel> mapping = new MappingBase<MsLoVessel>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //潤滑油マスタ
        public static List<MsLoVessel> GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), "OrderBy");

            List<MsLoVessel> ret = new List<MsLoVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsLoVessel> mapping = new MappingBase<MsLoVessel>();

            Params.Add(new DBParameter("MS_LO_ID", ms_lo_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //船ＩＤを指定し、関連するものを取得する
        public static List<MsLoVessel> GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int vesselid)
        {
            return GetRecordsByMsVesselID(null, loginUser, vesselid);
        }
        public static List<MsLoVessel> GetRecordsByMsVesselID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int vesselid)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), "OrderBy");
            List<MsLoVessel> ret = new List<MsLoVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsLoVessel> mapping = new MappingBase<MsLoVessel>();

            Params.Add(new DBParameter("MS_VESSEL_ID", vesselid));
            if (dbConnect == null)
            {
                ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            }
            else
            {
                ret = mapping.FillRecrods(dbConnect,loginUser.MsUserID, SQL, Params);
            }
                                 
            return ret;
        }

        public static List<MsLoVessel> GetRecordsByMsVesselIDAndLoName(NBaseData.DAC.MsUser loginUser, int vesselid, string loName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), "GetRecords");
            List<MsLoVessel> ret = new List<MsLoVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsLoVessel> mapping = new MappingBase<MsLoVessel>();

            SQL += " WHERE MS_LO_VESSEL.MS_VESSEL_ID = :MS_VESSEL_ID and MS_LO.LO_NAME like :LO_NAME";
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), "OrderBy");
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselid));
            Params.Add(new DBParameter("LO_NAME", "%" + loName + "%"));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsLoVessel GetRecord(NBaseData.DAC.MsUser loginUser, string MsLoVesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), MethodBase.GetCurrentMethod());
            List<MsLoVessel> ret = new List<MsLoVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsLoVessel> mapping = new MappingBase<MsLoVessel>();
            Params.Add(new DBParameter("MS_LO_VESSEL_ID", MsLoVesselId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }        

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_LO_VESSEL_ID", MsLoVesselID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_LO_VESSEL_ID", MsLoVesselID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        public bool DeleteRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsLoVessel), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_LO_VESSEL_ID", MsLoVesselID));

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsLoVesselID));

        //    int count = Convert.ToInt32(DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params));

        //    return count > 0 ? true : false;
        //}
        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsLoVesselID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

    }
}
