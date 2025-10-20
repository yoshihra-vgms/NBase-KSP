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
using NBaseUtil;

namespace NBaseData.DAC
{
    [Serializable]
    [DataContract()]
    [TableAttribute("SI_ALLOWANCE")]
    public class SiAllowance : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 手当ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_ALLOWANCE_ID", true)]
        public string SiAllowanceID { get; set; }

        /// <summary>
        /// 手当マスタID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_ALLOWANCE_ID")]
        public int MsSiAllowanceID { get; set; }


        /// <summary>
        /// 船ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 船長（船員ID (FK)）
        /// </summary>
        [DataMember]
        [ColumnAttribute("CAPTAIN_SENIN_ID")]
        public int CaptainSeninID { get; set; }

        /// <summary>
        /// 年月
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEAR_MONTH")]
        public string YearMonth { get; set; }

        /// <summary>
        /// 作業内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("CONTENTS")]
        public string Contents { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [ColumnAttribute("QUANTITY")]
        public int Quantity { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALLOWANCE")]
        public int Allowance { get; set; }

        /// <summary>
        /// 作業責任者
        /// </summary>
        [DataMember]
        [ColumnAttribute("PERSON_IN_CHARGE")]
        public string PersonInCharge { get; set; }




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

        public bool IsNew()
        {
            return SiAllowanceID == null;
        }

        public SiAllowance()
        {
            SiAllowanceID = null;
        }

        public static SiAllowance GetRecord(NBaseData.DAC.MsUser loginUser, string siAllowanceId)
        {
            return GetRecord(null, loginUser, siAllowanceId);
        }

        public static SiAllowance GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string siAllowanceId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAllowance), "GetRecords");
            List <SiAllowance> ret = new List<SiAllowance>();
            ParameterConnection Params = new ParameterConnection();

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiAllowance), "BySiAllowanceID");
            Params.Add(new DBParameter("SI_ALLOWANCE_ID", siAllowanceId));

            MappingBase<SiAllowance> mapping = new MappingBase<SiAllowance>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret.FirstOrDefault();
        }

        public static List<SiAllowance> GetRecords(NBaseData.DAC.MsUser loginUser, string from, string to, int msVesselID, string allowanceName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAllowance), MethodBase.GetCurrentMethod());
            List<SiAllowance> ret = new List<SiAllowance>();
            ParameterConnection Params = new ParameterConnection();

            if (from != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiAllowance), "ByFrom");
                Params.Add(new DBParameter("FROM_YEAR_MONTH", from));
            }
            if (to != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiAllowance), "ByTo");
                Params.Add(new DBParameter("TO_YEAR_MONTH", to));
            }
            if (msVesselID > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiAllowance), "ByVesselID");
                Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            }
            if (StringUtils.Empty(allowanceName) == false)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiAllowance), "ByAllowanceName");
                Params.Add(new DBParameter("ALLOWANCE_NAME", $"%{allowanceName}%"));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiAllowance), "OrderBy");

            MappingBase<SiAllowance> mapping = new MappingBase<SiAllowance>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAllowance), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_ALLOWANCE_ID", SiAllowanceID));
            Params.Add(new DBParameter("MS_SI_ALLOWANCE_ID", MsSiAllowanceID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("CAPTAIN_SENIN_ID", CaptainSeninID));
            Params.Add(new DBParameter("YEAR_MONTH", YearMonth));
            Params.Add(new DBParameter("CONTENTS", Contents));
            Params.Add(new DBParameter("QUANTITY", Quantity));
            Params.Add(new DBParameter("ALLOWANCE", Allowance));
            Params.Add(new DBParameter("PERSON_IN_CHARGE", PersonInCharge));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAllowance), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SI_ALLOWANCE_ID", MsSiAllowanceID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("CAPTAIN_SENIN_ID", CaptainSeninID));
            Params.Add(new DBParameter("YEAR_MONTH", YearMonth));
            Params.Add(new DBParameter("CONTENTS", Contents));
            Params.Add(new DBParameter("QUANTITY", Quantity));
            Params.Add(new DBParameter("ALLOWANCE", Allowance));
            Params.Add(new DBParameter("PERSON_IN_CHARGE", PersonInCharge));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SI_ALLOWANCE_ID", SiAllowanceID));

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
            Params.Add(new DBParameter("PK", SiAllowanceID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
