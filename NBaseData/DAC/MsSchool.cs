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
    [TableAttribute("MS_SCHOOL")]
    public class MsSchool : ISyncTable
    {
        #region データメンバ
        [DataMember]
        [ColumnAttribute("MS_SCHOOL_ID", true)]
        public string MsSchoolID { get; set; }

        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_ID")]
        public string MsCustomerID { get; set; }

        [DataMember]
        [ColumnAttribute("DEPARTMENT_OF")]
        public string DepartmentOf { get; set; }

        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_ID")]
        public int MsSiMenjouID { get; set; }

        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_KIND_ID")]
        public int MsSiMenjouKindID { get; set; }



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

        public bool IsNew()
        {
            return MsSchoolID == null;
        }

        public override string ToString()
        {
            return DepartmentOf;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsSchool()
        {
        }

        public static List<MsSchool> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSchool), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSchool), "_削除を除く");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSchool), "OrderBy");
            List<MsSchool> ret = new List<MsSchool>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSchool> mapping = new MappingBase<MsSchool>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<MsSchool> GetRecordsByCustomerId(NBaseData.DAC.MsUser loginUser, string customerId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSchool), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSchool), "_削除を除く");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSchool), "ByCustomer");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSchool), "OrderBy");
            List<MsSchool> ret = new List<MsSchool>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSchool> mapping = new MappingBase<MsSchool>();
            Params.Add(new DBParameter("MS_CUSTOMER_ID", customerId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSchool), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SCHOOL_ID", MsSchoolID));
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("DEPARTMENT_OF", DepartmentOf));
            Params.Add(new DBParameter("MS_SI_MENJOU_ID", MsSiMenjouID));
            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", MsSiMenjouKindID));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSchool), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("DEPARTMENT_OF", DepartmentOf));
            Params.Add(new DBParameter("MS_SI_MENJOU_ID", MsSiMenjouID));
            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", MsSiMenjouKindID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SCHOOL_ID", MsSchoolID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool DeleteFlagRecord(MsUser loginUser, NBaseData.DAC.MsSchool customer)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSchool), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("MS_SCHOOL_ID", MsSchoolID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsSchoolID));

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
            Params.Add(new DBParameter("PK", MsSchoolID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
