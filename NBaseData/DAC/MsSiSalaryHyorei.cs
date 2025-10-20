using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using System.Reflection;
using ORMapping;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_SI_SALARY_HYOREI")]
    public class MsSiSalaryHyorei : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 給与_役割ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SALARY_HYOREI_ID", true)]
        public int MsSiSalaryHyoreiId { get; set; }

        /// <summary>
        /// 基本給ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SALARY_ID")]
        public int MsSiSalaryId { get; set; }
        
        /// <summary>
        /// 標令
        /// </summary>
        [DataMember]
        [ColumnAttribute("HYOREI")]
        public decimal Hyorei { get; set; }

        /// <summary>
        /// 支給額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALLOWANCE")]
        public decimal Allowance { get; set; }

        /// <summary>
        /// 加算額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ADDITIONAL_AMOUNT")]
        public decimal AdditionalAmount { get; set; }

        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        /// <summary>
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

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
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public string RenewUserID { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }
        
        #endregion


        public override string ToString()
        {
            return Hyorei.ToString();
        }


        public static List<MsSiSalaryHyorei> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiSalaryHyorei), "GetRecords");

            List<MsSiSalaryHyorei> ret = new List<MsSiSalaryHyorei>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiSalaryHyorei> mapping = new MappingBase<MsSiSalaryHyorei>();

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiSalaryHyorei), "OrderBy");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }



        public static List<MsSiSalaryHyorei> GetRecords(MsUser loginUser, int MsSiSalaryId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiSalaryHyorei), "GetRecords");

            List<MsSiSalaryHyorei> ret = new List<MsSiSalaryHyorei>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiSalaryHyorei> mapping = new MappingBase<MsSiSalaryHyorei>();

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiSalaryHyorei), "SearchByMsSiSalaryId");
            Params.Add(new DBParameter("MS_SI_SALARY_ID", MsSiSalaryId));

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiSalaryHyorei), "OrderBy");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
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

            if (!IsNew())
            {
                Params.Add(new DBParameter("MS_SI_SALARY_HYOREI_ID", MsSiSalaryHyoreiId));
            }

            Params.Add(new DBParameter("MS_SI_SALARY_ID", MsSiSalaryId));
            Params.Add(new DBParameter("HYOREI", Hyorei));
            Params.Add(new DBParameter("ALLOWANCE", Allowance));
            Params.Add(new DBParameter("ADDITIONAL_AMOUNT", AdditionalAmount));

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

            Params.Add(new DBParameter("MS_SI_SALARY_ID", MsSiSalaryId));
            Params.Add(new DBParameter("HYOREI", Hyorei));
            Params.Add(new DBParameter("ALLOWANCE", Allowance));
            Params.Add(new DBParameter("ADDITIONAL_AMOUNT", AdditionalAmount));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SI_SALARY_HYOREI_ID", MsSiSalaryHyoreiId));
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
            Params.Add(new DBParameter("PK", MsSiSalaryHyoreiId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return MsSiSalaryHyoreiId == 0;
        }
    }
}
