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
    [TableAttribute("MS_REGIONAL")]
    public class MsRegional : ISyncTable
    {
        #region データメンバ
        [DataMember]
        [ColumnAttribute("MS_REGIONAL_CODE", true)]
        public string MsRegionalCode { get; set; }

        [DataMember]
        [ColumnAttribute("REGIONAL_NAME")]
        public string RegionalName { get; set; }

        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
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

        public override string ToString()
        {
            return RegionalName;
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsRegional()
        {
        }

        public static List<MsRegional> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsRegional), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsRegional), "OrderBy");
            List<MsRegional> ret = new List<MsRegional>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsRegional> mapping = new MappingBase<MsRegional>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsRegional> SearchRecords(NBaseData.DAC.MsUser loginUser, string regionalCode, string regionalName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsRegional), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsRegional), "OrderBy");
            List<MsRegional> ret = new List<MsRegional>();
            ParameterConnection Params = new ParameterConnection();
            if (regionalCode.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "LikeRegionalCode");
                Params.Add(new DBParameter("MS_REGIONAL_CODE", "%" + regionalCode + "%"));
            }
            if (regionalName.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsBasho), "LikeRegionalName");
                Params.Add(new DBParameter("REGIONAL_NAME", "%" + regionalName + "%"));
            }
            MappingBase<MsRegional> mapping = new MappingBase<MsRegional>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsRegional), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_REGIONAL_CODE", MsRegionalCode));
            Params.Add(new DBParameter("REGIONAL_NAME", RegionalName));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
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

        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsRegional), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("REGIONAL_NAME", RegionalName));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_REGIONAL_CODE", MsRegionalCode));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsRegionalCode));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
