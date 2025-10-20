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
    [TableAttribute("MS_SI_SALARY_RANK")]
    public class MsSiSalaryRank : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 給与_職種ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SALARY_RANK_ID", true)]
        public int MsSiSalaryRankId { get; set; }

        /// <summary>
        /// 基本給ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SALARY_ID")]
        public int MsSiSalaryId { get; set; }
        

        /// <summary>
        /// 職名ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_SALARY_ID")]
        public int MsSiShokumeiSalaryId { get; set; }

        /// <summary>
        /// 職名ID -> 名称
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 支給額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALLOWANCE0")]
        public decimal Allowance0 { get; set; }

        /// <summary>
        /// 支給額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALLOWANCE1")]
        public decimal Allowance1 { get; set; }

        /// <summary>
        /// 支給額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALLOWANCE2")]
        public decimal Allowance2 { get; set; }

        /// <summary>
        /// 支給額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALLOWANCE3")]
        public decimal Allowance3 { get; set; }

        /// <summary>
        /// 支給額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALLOWANCE4")]
        public decimal Allowance4 { get; set; }

        /// <summary>
        /// 支給額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALLOWANCE5")]
        public decimal Allowance5 { get; set; }

        /// <summary>
        /// 支給額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALLOWANCE6")]
        public decimal Allowance6 { get; set; }


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




        /// <summary>
        /// 表示順序(MsSiShokumeiSalary)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }

        #endregion

        public override string ToString()
        {
            return Name;
        }


        public static List<MsSiSalaryRank> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiSalaryRank), "GetRecords");

            List<MsSiSalaryRank> ret = new List<MsSiSalaryRank>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiSalaryRank> mapping = new MappingBase<MsSiSalaryRank>();

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiSalaryRank), "OrderBy");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsSiSalaryRank> GetRecords(MsUser loginUser, int MsSiSalaryId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiSalaryRank), "GetRecords");

            List<MsSiSalaryRank> ret = new List<MsSiSalaryRank>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiSalaryRank> mapping = new MappingBase<MsSiSalaryRank>();

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiSalaryRank), "SearchByMsSiSalaryId");
            Params.Add(new DBParameter("MS_SI_SALARY_ID", MsSiSalaryId));

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiSalaryRank), "OrderBy");

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
                Params.Add(new DBParameter("MS_SI_SALARY_RANK_ID", MsSiSalaryRankId));
            }

            Params.Add(new DBParameter("MS_SI_SALARY_ID", MsSiSalaryId));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_SALARY_ID", MsSiShokumeiSalaryId));
            Params.Add(new DBParameter("ALLOWANCE0", Allowance0));
            Params.Add(new DBParameter("ALLOWANCE1", Allowance1));
            Params.Add(new DBParameter("ALLOWANCE2", Allowance2));
            Params.Add(new DBParameter("ALLOWANCE3", Allowance3));
            Params.Add(new DBParameter("ALLOWANCE4", Allowance4));
            Params.Add(new DBParameter("ALLOWANCE5", Allowance5));
            Params.Add(new DBParameter("ALLOWANCE6", Allowance6));

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
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_SALARY_ID", MsSiShokumeiSalaryId));
            Params.Add(new DBParameter("ALLOWANCE0", Allowance0));
            Params.Add(new DBParameter("ALLOWANCE1", Allowance1));
            Params.Add(new DBParameter("ALLOWANCE2", Allowance2));
            Params.Add(new DBParameter("ALLOWANCE3", Allowance3));
            Params.Add(new DBParameter("ALLOWANCE4", Allowance4));
            Params.Add(new DBParameter("ALLOWANCE5", Allowance5));
            Params.Add(new DBParameter("ALLOWANCE6", Allowance6));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SI_SALARY_RANK_ID", MsSiSalaryRankId));
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
            Params.Add(new DBParameter("PK", MsSiSalaryRankId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return MsSiSalaryRankId == 0;
        }
    }
}
