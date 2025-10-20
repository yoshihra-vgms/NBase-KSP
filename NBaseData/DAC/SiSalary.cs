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

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_SALARY")]
    public class SiSalary : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 基本給ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SALARY_ID", true)]
        public string SiSalaryID { get; set; }


        /// <summary>
        /// 基本給マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SALARY_ID")]
        public int MsSiSalaryID { get; set; }


        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 種別　フェリー　内航
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIND")]
        public int Kind { get; set; }

        
        
        
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


        /// <summary>
        /// 標令
        /// </summary>
        [DataMember]
        [ColumnAttribute("HYOREI")]
        public decimal Hyorei { get; set; }

        /// <summary>
        /// 標令給
        /// </summary>
        [DataMember]
        [ColumnAttribute("HYOREI_ALLOWANCE")]
        public decimal HyoreiAllowance { get; set; }




        /// <summary>
        /// 職名ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_SALARY_ID")]
        public int MsSiShokumeiSalaryId { get; set; }


        /// <summary>
        /// 経験年数
        /// </summary>
        [DataMember]
        [ColumnAttribute("EXPERIENCE")]
        public decimal Experience { get; set; }

        /// <summary>
        /// 職務給
        /// </summary>
        [DataMember]
        [ColumnAttribute("RANK_ALLOWANCE")]
        public decimal RankAllowance { get; set; }


        /// <summary>
        /// 資格給
        /// </summary>
        [DataMember]
        [ColumnAttribute("QUALIFICATION_ALLOWANCE")]
        public decimal QualificationAllowance { get; set; }



        /// <summary>
        /// 基本給
        /// </summary>
        [DataMember]
        [ColumnAttribute("BASIC_SALARY")]
        public decimal BasicSalary { get; set; }

        /// <summary>
        /// 組合費
        /// </summary>
        [DataMember]
        [ColumnAttribute("UNION_DUES")]
        public decimal UnionDues { get; set; }



        /// <summary>
        /// 共有備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHARED_REMARKS")]
        public string SharedRemarks { get; set; }

        /// <summary>
        /// 対象年度備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("FISCAL_YEAR_REMARKS")]
        public string FiscalYearRemarks { get; set; }



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






        public SiSalary()
        {
            this.MsSeninID = Int32.MinValue;
            this.Kind = Int32.MinValue;
        }



        public static SiSalary GetRecord(NBaseData.DAC.MsUser loginUser, string siShoshinShokakuId)
        {
            return GetRecord(null, loginUser, siShoshinShokakuId);
        }

        public static SiSalary GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string siShoshinShokakuId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "BySiSalaryID");

            List<SiSalary> ret = new List<SiSalary>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSalary> mapping = new MappingBase<SiSalary>();
            Params.Add(new DBParameter("SI_SHOSHIN_SHOKAKU_ID", siShoshinShokakuId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
        public static List<SiSalary> SearchRecords(NBaseData.DAC.MsUser loginUser, int msSeninId, DateTime start, DateTime end, bool kind0, bool kind1, bool kind2)
        {
            return SearchRecords(null, loginUser, msSeninId, start, end, kind0, kind1, kind2);
        }

        public static List<SiSalary> SearchRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int msSeninId, DateTime start, DateTime end, bool kind0, bool kind1, bool kind2)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "GetRecords");

            ParameterConnection Params = new ParameterConnection();
            if (msSeninId > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "ByMsSeninID");
                Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            }
            //if (kind == (int)MsSiSalary.KIND.フェリー || kind == (int)MsSiSalary.KIND.内航)
            //{
            //    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "ByKind");
            //    Params.Add(new DBParameter("KIND", kind));
            //}

            if (kind0 || kind1 || kind2)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "ByKinds");
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

            if (start != DateTime.MinValue && end != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "ByStartEndDate");
                Params.Add(new DBParameter("START_DATE", start));
                Params.Add(new DBParameter("END_DATE", end));
            }
            else if (start != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "ByStartDate");
                Params.Add(new DBParameter("START_DATE", start));
            }
            else if (end != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "ByEndDate");
                Params.Add(new DBParameter("END_DATE", end));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "OrderBy");

            List<SiSalary> ret = new List<SiSalary>();
            MappingBase<SiSalary> mapping = new MappingBase<SiSalary>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);
            return ret;
        }

        public static List<SiSalary> SearchRecords(NBaseData.DAC.MsUser loginUser, int msSeninId, int msSiSalaryId)
        {
            return SearchRecords(null, loginUser, msSeninId, msSiSalaryId);
        }

        public static List<SiSalary> SearchRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int msSeninId, int msSiSalaryId)
        {
            ParameterConnection Params = new ParameterConnection();
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "GetRecords");
            if (msSeninId > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "ByMsSeninID");
                Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            }
            if (msSiSalaryId > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "ByMsSiSalaryID");
                Params.Add(new DBParameter("MS_SI_SALARY_ID", msSiSalaryId));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSalary), "OrderBy");

            List<SiSalary> ret = new List<SiSalary>();
            MappingBase<SiSalary> mapping = new MappingBase<SiSalary>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);
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

            Params.Add(new DBParameter("SI_SALARY_ID", SiSalaryID));
            Params.Add(new DBParameter("MS_SI_SALARY_ID", MsSiSalaryID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("KIND", Kind));
           
            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));
            Params.Add(new DBParameter("HYOREI", Hyorei));
            Params.Add(new DBParameter("HYOREI_ALLOWANCE", HyoreiAllowance));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_SALARY_ID", MsSiShokumeiSalaryId));
            Params.Add(new DBParameter("EXPERIENCE", Experience));
            Params.Add(new DBParameter("RANK_ALLOWANCE", RankAllowance));
            Params.Add(new DBParameter("QUALIFICATION_ALLOWANCE", QualificationAllowance));

            Params.Add(new DBParameter("BASIC_SALARY", BasicSalary));
            Params.Add(new DBParameter("UNION_DUES", UnionDues));

            Params.Add(new DBParameter("SHARED_REMARKS", SharedRemarks));
            Params.Add(new DBParameter("FISCAL_YEAR_REMARKS", FiscalYearRemarks));

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

            Params.Add(new DBParameter("MS_SI_SALARY_ID", MsSiSalaryID));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("KIND", Kind));

            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));
            Params.Add(new DBParameter("HYOREI", Hyorei));
            Params.Add(new DBParameter("HYOREI_ALLOWANCE", HyoreiAllowance));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_SALARY_ID", MsSiShokumeiSalaryId));
            Params.Add(new DBParameter("EXPERIENCE", Experience));
            Params.Add(new DBParameter("RANK_ALLOWANCE", RankAllowance));
            Params.Add(new DBParameter("QUALIFICATION_ALLOWANCE", QualificationAllowance));

            Params.Add(new DBParameter("BASIC_SALARY", BasicSalary));
            Params.Add(new DBParameter("UNION_DUES", UnionDues));

            Params.Add(new DBParameter("SHARED_REMARKS", SharedRemarks));
            Params.Add(new DBParameter("FISCAL_YEAR_REMARKS", FiscalYearRemarks));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_SALARY_ID", SiSalaryID));
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
            Params.Add(new DBParameter("PK", SiSalaryID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiSalaryID == null;
        }
    }
}
