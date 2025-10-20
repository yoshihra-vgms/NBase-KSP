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
    [TableAttribute("MS_SI_SALARY")]
    public class MsSiSalary : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 給与計算マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SALARY_ID", true)]
        public int MsSiSalaryID { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIND")]
        public int Kind { get; set; }
        public enum KIND { 航機通砲手, 下級海技士, 部員 }

        

        /// <summary>
        /// 開始日
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_DATE")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        [DataMember]
        [ColumnAttribute("END_DATE")]
        public DateTime EndDate { get; set; }


        /// 給与計算マスタID（旧）
        /// </summary>
        [DataMember]
        [ColumnAttribute("PREV_MS_SI_SALARY_ID")]
        public int PrevMsSiSalaryID { get; set; }


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


        [DataMember]
        public List<MsSiSalaryHyorei> SalaryHyoreiList = null;

        [DataMember]
        public List<MsSiSalaryRank> SalaryRankList = null;



        #endregion


        public static string KindStr(int kind)
        {
            var ret = "";
            if (kind == 0)
            {
                ret = "航機通砲手";
            }
            else if (kind == 1)
            {
                ret = "４・５級海技士";
            }
            else if (kind == 2)
            {
                ret = "部員";
            }
            return ret;
        }


        public static MsSiSalary GetRecord(MsUser loginUser, int msSiSalaryId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiSalary), "GetRecords");

            List<MsSiSalary> ret = new List<MsSiSalary>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiSalary> mapping = new MappingBase<MsSiSalary>();

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiSalary), "SearchBySiSalaryId");
            Params.Add(new DBParameter("MS_SI_SALARY_ID", msSiSalaryId));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            if (ret.Count > 0)
                return ret.First<MsSiSalary>();
            else
                return null;
        }


        public static List<MsSiSalary> SearchRecords(MsUser loginUser, bool kind0, bool kind1, bool kind2)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiSalary), "GetRecords");

            List<MsSiSalary> ret = new List<MsSiSalary>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiSalary> mapping = new MappingBase<MsSiSalary>();

            if (kind0 || kind1 || kind2)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiSalary), "SearchByKinds");
                var vals = "";
                if (kind0)
                {
                    vals += "0";
                }
                if (kind1)
                {
                    if (string.IsNullOrEmpty(vals) == false)
                        vals += ",";
                    vals += "1";
                }
                if (kind2)
                {
                    if (string.IsNullOrEmpty(vals) == false)
                        vals += ",";
                    vals += "2";
                }
                SQL = SQL.Replace("#KINDS#", vals);
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiSalary), "OrderBy");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsSiSalary> SearchRecords(MsUser loginUser, int Kind)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiSalary), "GetRecords");

            List<MsSiSalary> ret = new List<MsSiSalary>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiSalary> mapping = new MappingBase<MsSiSalary>();

            if (Kind != -1)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiSalary), "SearchByKind");
                Params.Add(new DBParameter("KIND", Kind));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiSalary), "OrderBy");

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
                Params.Add(new DBParameter("MS_SI_SALARY_ID", MsSiSalaryID));
            }

            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));
            Params.Add(new DBParameter("PREV_MS_SI_SALARY_ID", PrevMsSiSalaryID));

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

            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));
            Params.Add(new DBParameter("PREV_MS_SI_SALARY_ID", PrevMsSiSalaryID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SI_SALARY_ID", MsSiSalaryID));
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
            Params.Add(new DBParameter("PK", MsSiSalaryID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return MsSiSalaryID == 0;
        }
    }
}
