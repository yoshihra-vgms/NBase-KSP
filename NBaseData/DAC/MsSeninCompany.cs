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
using NBaseUtil;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_SENIN_COMPANY")]
    public class MsSeninCompany : ISyncTable
    {
        #region データメンバ
        [DataMember]
        [ColumnAttribute("MS_SENIN_COMPANY_ID", true)]
        public string MsSeninCompanyID { get; set; }

        [DataMember]
        [ColumnAttribute("COMPANY_NAME")]
        public string CompanyName { get; set; }

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
            return CompanyName;
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsSeninCompany()
        {
        }

        public static List<MsSeninCompany> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninCompany), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSeninCompany), "OrderBy");
            List<MsSeninCompany> ret = new List<MsSeninCompany>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSeninCompany> mapping = new MappingBase<MsSeninCompany>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninCompany), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SENIN_COMPANY_ID", MsSeninCompanyID));
            Params.Add(new DBParameter("COMPANY_NAME", CompanyName));
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

        //public bool InsertRecord(WingData.DAC.MsUser loginUser, WingData.DAC.MsSeninCompany customer)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninCompany), MethodBase.GetCurrentMethod());

        //    int cnt = 0;
        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("MS_SENIN_COMPANY_ID", MsSeninCompanyID));
        //    Params.Add(new DBParameter("COMPANY_NAME", CompanyName));
        //    Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

        //    Params.Add(new DBParameter("SEND_FLAG", SendFlag));
        //    Params.Add(new DBParameter("VESSEL_ID", VesselID));
        //    Params.Add(new DBParameter("DATA_NO", DataNo));
        //    Params.Add(new DBParameter("USER_KEY", UserKey));
        //    Params.Add(new DBParameter("RENEW_DATE", RenewDate));
        //    Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
        //    Params.Add(new DBParameter("TS", Ts));

        //    cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
        //    if (cnt == 0)
        //        return false;
        //    return true;
        //}

        //public bool UpdateRecord(WingData.DAC.MsUser loginUser, WingData.DAC.MsSeninCompany customer)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninCompany), MethodBase.GetCurrentMethod());

        //    int cnt = 0;
        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("COMPANY_NAME", CompanyName));
        //    Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

        //    Params.Add(new DBParameter("SEND_FLAG", SendFlag));
        //    Params.Add(new DBParameter("VESSEL_ID", VesselID));
        //    Params.Add(new DBParameter("DATA_NO", DataNo));
        //    Params.Add(new DBParameter("USER_KEY", UserKey));
        //    Params.Add(new DBParameter("RENEW_DATE", RenewDate));
        //    Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

        //    Params.Add(new DBParameter("MS_SENIN_COMPANY_ID", MsSeninCompanyID));
        //    Params.Add(new DBParameter("TS", Ts));

        //    cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
        //    if (cnt == 0)
        //        return false;
        //    return true;
        //}

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninCompany), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("COMPANY_NAME", CompanyName));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SENIN_COMPANY_ID", MsSeninCompanyID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        //public bool DeleteFlagRecord(MsUser loginUser, WingData.DAC.MsSeninCompany customer)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninCompany), MethodBase.GetCurrentMethod());

        //    int cnt = 0;
        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
        //    Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
        //    Params.Add(new DBParameter("MS_SENIN_COMPANY_ID", MsSeninCompanyID));
        //    Params.Add(new DBParameter("TS", Ts));

        //    cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
        //    if (cnt == 0)
        //        return false;
        //    return true;
        //}

        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsSeninCompanyID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
