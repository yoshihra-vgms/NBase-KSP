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
    [TableAttribute("SI_SALARY_NENDO")]
    public class SiSalaryNendo : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 給与年度ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SALARY_NENDO_ID", true)]
        public string SiSalaryNendoID { get; set; }

        /// <summary>
        /// 年
        /// </summary>
        [DataMember]
        [ColumnAttribute("NEN")]
        public string Nen { get; set; }

        /// <summary>
        /// ユーザID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_USER_ID")]
        public string MsUserID { get; set; }


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

        #endregion

        public SiSalaryNendo()
        {
        }

        public static List<SiSalaryNendo> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSalaryNendo), MethodBase.GetCurrentMethod());
            List<SiSalaryNendo> ret = new List<SiSalaryNendo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSalaryNendo> mapping = new MappingBase<SiSalaryNendo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiSalaryNendo> GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSalaryNendo), MethodBase.GetCurrentMethod());
            List<SiSalaryNendo> ret = new List<SiSalaryNendo>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_USER_ID", ms_user_id));

            MappingBase<SiSalaryNendo> mapping = new MappingBase<SiSalaryNendo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static SiSalaryNendo GetRecord(NBaseData.DAC.MsUser loginUser, string SiSalaryNendoID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSalaryNendo), MethodBase.GetCurrentMethod());
            List<SiSalaryNendo> ret = new List<SiSalaryNendo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_SALARY_NENDO_ID", SiSalaryNendoID));
            MappingBase<SiSalaryNendo> mapping = new MappingBase<SiSalaryNendo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static SiSalaryNendo GetRecordByLastDate(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSalaryNendo), MethodBase.GetCurrentMethod());
            List<SiSalaryNendo> ret = new List<SiSalaryNendo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSalaryNendo> mapping = new MappingBase<SiSalaryNendo>();
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSalaryNendo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_SALARY_NENDO_ID", SiSalaryNendoID));
            Params.Add(new DBParameter("NEN", Nen));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            if (dbConnect == null)
            {
                cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            }
            else
            {
                cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            }
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSalaryNendo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("NEN", Nen));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SI_SALARY_NENDO_ID", SiSalaryNendoID));

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
        //    Params.Add(new DBParameter("PK", SiSalaryNendoID));

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
            Params.Add(new DBParameter("PK", SiSalaryNendoID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
