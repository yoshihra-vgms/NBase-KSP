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
    [TableAttribute("MS_SI_GETSUJI_SHIME_BI")]
    public class MsSiGetsujiShimeBi : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 月次締め日ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_GETSUJI_SHIME_BI_ID", true)]
        public int MsSiGetsujiShimeBiID { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        [DataMember]
        [ColumnAttribute("MONTH")]
        public string Month { get; set; }

        /// <summary>
        /// 締め日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHIME_BI")]
        public int ShimeBi { get; set; }


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


        public MsSiGetsujiShimeBi()
        {
        }

        public static List<MsSiGetsujiShimeBi> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }

        public static List<MsSiGetsujiShimeBi> GetRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiGetsujiShimeBi), MethodBase.GetCurrentMethod());

            List<MsSiGetsujiShimeBi> ret = new List<MsSiGetsujiShimeBi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiGetsujiShimeBi> mapping = new MappingBase<MsSiGetsujiShimeBi>();
            ret = mapping.FillRecrods(dbConnect,loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsSiGetsujiShimeBi GetRecordByMonth(NBaseData.DAC.MsUser loginUser, string month)
        {
            return GetRecordByMonth(null, loginUser, month);
        }
        public static MsSiGetsujiShimeBi GetRecordByMonth(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string month)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiGetsujiShimeBi), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiGetsujiShimeBi), "ByMonth");

            List<MsSiGetsujiShimeBi> ret = new List<MsSiGetsujiShimeBi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiGetsujiShimeBi> mapping = new MappingBase<MsSiGetsujiShimeBi>();

            Params.Add(new DBParameter("MONTH", month));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiGetsujiShimeBi), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SI_GETSUJI_SHIME_BI_ID", MsSiGetsujiShimeBiID));
            Params.Add(new DBParameter("MONTH", Month));
            Params.Add(new DBParameter("SHIME_BI", ShimeBi));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiGetsujiShimeBi), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MONTH", Month));
            Params.Add(new DBParameter("SHIME_BI", ShimeBi));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_SI_GETSUJI_SHIME_BI_ID", MsSiGetsujiShimeBiID));

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
        //    Params.Add(new DBParameter("PK", MsSiGetsujiShimeBiID));

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
            Params.Add(new DBParameter("PK", MsSiGetsujiShimeBiID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public static DateTime ToFrom_船員_月次締日(MsUser loginUser, DateTime date)
        {
            return ToFrom_船員_月次締日(null, loginUser, date);
        }
        public static DateTime ToFrom_船員_月次締日(DBConnect dbConnect, MsUser loginUser, DateTime date)
        {
            DateTime d = date.AddMonths(-1);

            int m = GetRecordByMonth(dbConnect, loginUser, d.Month.ToString("00")).ShimeBi + 1;
            DateTime result = new DateTime(d.Year, d.Month, m, 0, 0, 0);

            return result;
        }


        public static DateTime ToTo_船員_月次締日(MsUser loginUser, DateTime date)
        {
            return ToTo_船員_月次締日(null, loginUser, date);
        }
        public static DateTime ToTo_船員_月次締日(DBConnect dbConnect, MsUser loginUser, DateTime date)
        {
            return ToFrom_船員_月次締日(dbConnect, loginUser, date.AddMonths(1));
        }
    }
}
