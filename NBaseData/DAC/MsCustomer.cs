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
    [TableAttribute("MS_CUSTOMER")]
    public class MsCustomer : ISyncTable
    {
        public enum 種別
        {
            取引先 = 0,
            代理店 = 1,
            荷主 = 2,
        };
        public enum 種別2
        {
            企業 = 0,
            学校 = 1
        };

        #region データメンバ
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_ID", true)]
        public string MsCustomerID { get; set; }

        [DataMember]
        [ColumnAttribute("CUSTOMER_NAME")]
        public string CustomerName { get; set; }

        [DataMember]
        [ColumnAttribute("TEL")]
        public string Tel { get; set; }

        [DataMember]
        [ColumnAttribute("FAX")]
        public string Fax { get; set; }

        [DataMember]
        [ColumnAttribute("ZIP_CODE")]
        public string ZipCode { get; set; }

        [DataMember]
        [ColumnAttribute("ADDRESS1")]
        public string Address1 { get; set; }

        [DataMember]
        [ColumnAttribute("ADDRESS2")]
        public string Address2 { get; set; }

        [DataMember]
        [ColumnAttribute("BUILDING_NAME")]
        public string BuildingName { get; set; }

        [DataMember]
        [ColumnAttribute("BANK_NAME")]
        public string BankName { get; set; }

        [DataMember]
        [ColumnAttribute("BRANCH_NAME")]
        public string BranchName { get; set; }

        [DataMember]
        [ColumnAttribute("ACCOUNT_NO")]
        public string AccountNo { get; set; }

        [DataMember]
        [ColumnAttribute("ACCOUNT_ID")]
        public string AccountId { get; set; }

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

        [DataMember]
        [ColumnAttribute("SHUBETSU")]
        public String Shubetsu { get; set; }

        [DataMember]
        [ColumnAttribute("LOGIN_ID")]
        public string LoginID { get; set; }

        [DataMember]
        [ColumnAttribute("PASSWORD")]
        public string Password { get; set; }

        [DataMember]
        [ColumnAttribute("BIKOU")]
        public String Bikou { get; set; }

        [DataMember]
        [ColumnAttribute("SHUBETSU2")]
        public String Shubetsu2 { get; set; }

        [DataMember]
        [ColumnAttribute("TEACHER1")]
        public String Teacher1 { get; set; }

        [DataMember]
        [ColumnAttribute("TEACHER2")]
        public String Teacher2 { get; set; }

        [DataMember]
        [ColumnAttribute("APPOINTED_FLAG")]
        public int AppointedFlag { get; set; }

        [DataMember]
        [ColumnAttribute("INSPECTION_FLAG")]
        public int InspectionFlag { get; set; }

        #endregion

        [DataMember]
        private List<MsSchool> msSchools;
        public List<MsSchool> MsSchools
        {
            get
            {
                if (msSchools == null)
                {
                    msSchools = new List<MsSchool>();
                }

                return msSchools;
            }
        }

        /// <summary>
        /// 顧客担当者
        /// </summary>
        private List<MsCustomerTantou> msCustomerTantous;
        public List<MsCustomerTantou> MsCustomerTantous
        {
            get
            {
                if (msCustomerTantous == null)
                {
                    msCustomerTantous = new List<MsCustomerTantou>();
                }

                return msCustomerTantous;
            }
        }


        public override string ToString()
        {
            return CustomerName;
        }

        public bool Is取引先()
        {
            string [] s = Shubetsu.Split(',');
            if (s[0] == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Is代理店()
        {
            string[] s = Shubetsu.Split(',');
            if (s[1] == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Is荷主()
        {
            string[] s = Shubetsu.Split(',');
            if (s[2] == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Is企業()
        {
            string[] s = Shubetsu2.Split(',');
            if (s[0] == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Is学校()
        {
            string[] s = Shubetsu2.Split(',');
            if (s[1] == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsNew()
        {
            return MsCustomerID == null;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsCustomer()
        {
        }

        public static List<MsCustomer> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), "_削除を除く");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), "OrderBy");
            List<MsCustomer> ret = new List<MsCustomer>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCustomer> mapping = new MappingBase<MsCustomer>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsCustomer> GetRecords削除を含む(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), "OrderBy");
            List<MsCustomer> ret = new List<MsCustomer>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCustomer> mapping = new MappingBase<MsCustomer>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsCustomer GetRecord(NBaseData.DAC.MsUser loginUser, string MsCustomerID)
        {
            return GetRecord(null, loginUser, MsCustomerID);
        }
        public static MsCustomer GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string MsCustomerID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), MethodBase.GetCurrentMethod());
            List<MsCustomer> ret = new List<MsCustomer>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCustomer> mapping = new MappingBase<MsCustomer>();
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            ret = mapping.FillRecrods(dbConnect,loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static MsCustomer GetRecordByLoginId(NBaseData.DAC.MsUser loginUser, string LoginId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), MethodBase.GetCurrentMethod());
            List<MsCustomer> ret = new List<MsCustomer>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCustomer> mapping = new MappingBase<MsCustomer>();
            Params.Add(new DBParameter("LOGIN_ID", LoginId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<MsCustomer> GetRecords_代理店(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), "_削除を除く");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), "_代理店");
            List<MsCustomer> ret = new List<MsCustomer>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCustomer> mapping = new MappingBase<MsCustomer>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsCustomer> SearchRecords(MsUser loginUser, string customerID, string customerName, bool isClient, bool isAgency, bool isConsignor, bool isCompany, bool isSchool, bool isAppointed, bool isInspection)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), "_削除を除く");
            List<MsCustomer> ret = new List<MsCustomer>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCustomer> mapping = new MappingBase<MsCustomer>();

            if (customerID != "")
            {
                SQL += " and MS_CUSTOMER_ID like :MS_CUSTOMER_ID";
                Params.Add(new DBParameter("MS_CUSTOMER_ID", "%" + customerID + "%"));
            }
            if (customerName != "")
            {
                SQL += " and CUSTOMER_NAME like :CUSTOMER_NAME";
                Params.Add(new DBParameter("CUSTOMER_NAME", "%" + customerName + "%"));
            }
            if (isClient || isAgency || isConsignor || isCompany || isSchool || isAppointed || isInspection)
            {
                SQL += " and ( ";
                if (isClient)
                {
                    SQL += " SHUBETSU LIKE '1%' ";
                }
                if (isAgency)
                {
                    if (isClient)
                    {
                        SQL += " OR ";
                    }
                    SQL += " SHUBETSU LIKE '%,1,%' ";
                }
                if (isConsignor)
                {
                    if (isClient || isAgency)
                    {
                        SQL += " OR ";
                    }
                    SQL += " SHUBETSU LIKE '%,1' ";
                }

                if (isCompany)
                {
                    if (isClient || isAgency || isConsignor)
                    {
                        SQL += " OR ";
                    }
                    SQL += " SHUBETSU2 LIKE '1%' ";
                }
                if (isSchool)
                {
                    if (isClient || isAgency || isConsignor || isCompany)
                    {
                        SQL += " OR ";
                    }
                    SQL += " SHUBETSU2 LIKE '%,1' ";
                }
                if (isAppointed)
                {
                    if (isClient || isAgency || isConsignor || isCompany || isSchool)
                    {
                        SQL += " OR ";
                    }
                    SQL += " APPOINTED_FLAG = 1 ";
                }
                if (isInspection)
                {
                    if (isClient || isAgency || isConsignor || isCompany || isSchool || isAppointed)
                    {
                        SQL += " OR ";
                    }
                    SQL += " INSPECTION_FLAG = 1 ";
                }
                SQL += "  ) ";
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), "OrderBy");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;

        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("CUSTOMER_NAME", CustomerName));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("FAX", Fax));
            Params.Add(new DBParameter("ZIP_CODE", ZipCode));
            Params.Add(new DBParameter("ADDRESS1", Address1));
            Params.Add(new DBParameter("ADDRESS2", Address2));
            Params.Add(new DBParameter("BUILDING_NAME", BuildingName));
            Params.Add(new DBParameter("BANK_NAME", BankName));
            Params.Add(new DBParameter("BRANCH_NAME", BranchName));
            Params.Add(new DBParameter("ACCOUNT_NO", AccountNo));
            Params.Add(new DBParameter("ACCOUNT_ID", AccountId));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            Params.Add(new DBParameter("SHUBETSU", Shubetsu));
            Params.Add(new DBParameter("LOGIN_ID", LoginID));
            Params.Add(new DBParameter("PASSWORD", Password));

            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            {
                Params.Add(new DBParameter("TEACHER1", Teacher1));
                Params.Add(new DBParameter("TEACHER2", Teacher2));
                Params.Add(new DBParameter("BIKOU", Bikou));
                Params.Add(new DBParameter("SHUBETSU2", Shubetsu2));

                Params.Add(new DBParameter("APPOINTED_FLAG", AppointedFlag));
                Params.Add(new DBParameter("INSPECTION_FLAG", InspectionFlag));
            } 

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCustomer customer)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("CUSTOMER_NAME", CustomerName));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("FAX", Fax));
            Params.Add(new DBParameter("ZIP_CODE", ZipCode));
            Params.Add(new DBParameter("ADDRESS1", Address1));
            Params.Add(new DBParameter("ADDRESS2", Address2));
            Params.Add(new DBParameter("BUILDING_NAME", BuildingName));
            Params.Add(new DBParameter("BANK_NAME", BankName));
            Params.Add(new DBParameter("BRANCH_NAME", BranchName));
            Params.Add(new DBParameter("ACCOUNT_NO", AccountNo));
            Params.Add(new DBParameter("ACCOUNT_ID", AccountId));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            Params.Add(new DBParameter("SHUBETSU", Shubetsu));
            Params.Add(new DBParameter("LOGIN_ID", LoginID));
            Params.Add(new DBParameter("PASSWORD", Password));

            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            {
                Params.Add(new DBParameter("TEACHER1", Teacher1));
                Params.Add(new DBParameter("TEACHER2", Teacher2));
                Params.Add(new DBParameter("BIKOU", Bikou));
                Params.Add(new DBParameter("SHUBETSU2", Shubetsu2));

                Params.Add(new DBParameter("APPOINTED_FLAG", AppointedFlag));
                Params.Add(new DBParameter("INSPECTION_FLAG", InspectionFlag));
            } 

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCustomer customer)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("CUSTOMER_NAME", CustomerName));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("FAX", Fax));
            Params.Add(new DBParameter("ZIP_CODE", ZipCode));
            Params.Add(new DBParameter("ADDRESS1", Address1));
            Params.Add(new DBParameter("ADDRESS2", Address2));
            Params.Add(new DBParameter("BUILDING_NAME", BuildingName));
            Params.Add(new DBParameter("BANK_NAME", BankName));
            Params.Add(new DBParameter("BRANCH_NAME", BranchName));
            Params.Add(new DBParameter("ACCOUNT_NO", AccountNo));
            Params.Add(new DBParameter("ACCOUNT_ID", AccountId));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("TS", Ts));

            Params.Add(new DBParameter("SHUBETSU", Shubetsu));
            Params.Add(new DBParameter("LOGIN_ID", LoginID));
            Params.Add(new DBParameter("PASSWORD", Password));

            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            {
                Params.Add(new DBParameter("TEACHER1", Teacher1));
                Params.Add(new DBParameter("TEACHER2", Teacher2));
                Params.Add(new DBParameter("BIKOU", Bikou));
                Params.Add(new DBParameter("SHUBETSU2", Shubetsu2));

                Params.Add(new DBParameter("APPOINTED_FLAG", AppointedFlag));
                Params.Add(new DBParameter("INSPECTION_FLAG", InspectionFlag));
            } 
            
            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("CUSTOMER_NAME", CustomerName));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("FAX", Fax));
            Params.Add(new DBParameter("ZIP_CODE", ZipCode));
            Params.Add(new DBParameter("ADDRESS1", Address1));
            Params.Add(new DBParameter("ADDRESS2", Address2));
            Params.Add(new DBParameter("BUILDING_NAME", BuildingName));
            Params.Add(new DBParameter("BANK_NAME", BankName));
            Params.Add(new DBParameter("BRANCH_NAME", BranchName));
            Params.Add(new DBParameter("ACCOUNT_NO", AccountNo));
            Params.Add(new DBParameter("ACCOUNT_ID", AccountId));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("TS", Ts));

            Params.Add(new DBParameter("SHUBETSU", Shubetsu));
            Params.Add(new DBParameter("LOGIN_ID", LoginID));
            Params.Add(new DBParameter("PASSWORD", Password));

            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            {
                Params.Add(new DBParameter("TEACHER1", Teacher1));
                Params.Add(new DBParameter("TEACHER2", Teacher2));
                Params.Add(new DBParameter("BIKOU", Bikou));
                Params.Add(new DBParameter("SHUBETSU2", Shubetsu2));

                Params.Add(new DBParameter("APPOINTED_FLAG", AppointedFlag));
                Params.Add(new DBParameter("INSPECTION_FLAG", InspectionFlag));
            } 
            
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool DeleteFlagRecord(MsUser loginUser, NBaseData.DAC.MsCustomer customer)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomer), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
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
        //    Params.Add(new DBParameter("PK", MsCustomerID));

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
            Params.Add(new DBParameter("PK", MsCustomerID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
