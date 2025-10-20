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
using NBaseData.DS;
using ORMapping.Atts;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_USER_BUMON")]
    public class MsUserBumon : ISyncTable
    {
        #region データメンバ
        //public string BaseSQL { get; set; }
        //public string WhereSQL { get; set; }
        //public string OrderSQL { get; set; }

        [DataMember]
        [ColumnAttribute("MS_USER_BUMON_ID", true)]
        public string MsUserBumonID { get; set; }

        [DataMember]
        [ColumnAttribute("MS_BUMON_ID")]
        public string MsBumonID { get; set; }
        
        [DataMember]
        [ColumnAttribute("MS_USER_ID")]
        public string MsUserID { get; set; }

        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }
        
        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }
        
        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }      

        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public String RenewUserID { get; set; }

        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }
        #endregion

        public MsUserBumon()
        {
        }

        public static MsUserBumon GetRecordByUserID(MsUser loginUser, string msUserID)
        {
            List<MsUserBumon> ret = GetRecordsByUserID(loginUser, msUserID);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<MsUserBumon> GetRecordsByUserID(MsUser loginUser, string msUserID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUserBumon), MethodBase.GetCurrentMethod());
            List<MsUserBumon> ret = new List<MsUserBumon>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUserBumon> mapping = new MappingBase<MsUserBumon>();
            Params.Add(new DBParameter("MS_USER_ID", msUserID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUserBumon), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_USER_BUMON_ID", MsUserBumonID));
            Params.Add(new DBParameter("MS_BUMON_ID", MsBumonID));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }
        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUserBumon), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_BUMON_ID", MsBumonID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool DeleteRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUserBumon), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsUserBumonID));

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
            Params.Add(new DBParameter("PK", MsUserBumonID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

    }
}

