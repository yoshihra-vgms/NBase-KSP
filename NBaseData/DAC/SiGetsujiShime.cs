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
    [TableAttribute("SI_GETSUJI_SHIME")]
    public class SiGetsujiShime : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 月次締めID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_GETSUJI_SHIME_ID", true)]
        public string SiGetsujiShimeID { get; set; }

        /// <summary>
        /// 年月
        /// </summary>
        [DataMember]
        [ColumnAttribute("NEN_GETSU")]
        public string NenGetsu { get; set; }

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

        /// <summary>
        /// 月次締日
        /// </summary>
        [DataMember]
        public DateTime 月次締日 { get; private set; }


        public SiGetsujiShime()
        {
        }

        public static List<SiGetsujiShime> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }
        public static List<SiGetsujiShime> GetRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiGetsujiShime), MethodBase.GetCurrentMethod());
            List<SiGetsujiShime> ret = new List<SiGetsujiShime>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiGetsujiShime> mapping = new MappingBase<SiGetsujiShime>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiGetsujiShime> GetRecords(NBaseData.DAC.MsUser loginUser, DateTime date)
        {
            return GetRecords(null, loginUser, date);
        }
        public static List<SiGetsujiShime> GetRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, DateTime date)
        {
            DateTime st = NBaseUtil.DateTimeUtils.年度開始日(date);
            DateTime ed = NBaseUtil.DateTimeUtils.年度終了日(date);

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiGetsujiShime), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiGetsujiShime), "ByStartEnd");
            List<SiGetsujiShime> ret = new List<SiGetsujiShime>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("START_DATE", st.ToString("yyyyMM")));
            Params.Add(new DBParameter("END_DATE", ed.ToString("yyyyMM")));
            MappingBase<SiGetsujiShime> mapping = new MappingBase<SiGetsujiShime>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static SiGetsujiShime GetRecord(NBaseData.DAC.MsUser loginUser, string SiGetsujiShimeID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiGetsujiShime), MethodBase.GetCurrentMethod());
            List<SiGetsujiShime> ret = new List<SiGetsujiShime>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_GETSUJI_SHIME_ID", SiGetsujiShimeID));
            MappingBase<SiGetsujiShime> mapping = new MappingBase<SiGetsujiShime>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        /// <summary>
        /// 締めの最終月を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static SiGetsujiShime GetRecordByLastDate(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiGetsujiShime), MethodBase.GetCurrentMethod());
            List<SiGetsujiShime> ret = new List<SiGetsujiShime>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiGetsujiShime> mapping = new MappingBase<SiGetsujiShime>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;

            MsSiGetsujiShimeBi shimeBi = MsSiGetsujiShimeBi.GetRecordByMonth(loginUser, ret[0].NenGetsu.Substring(4));

            ret[0].月次締日 = DateTime.ParseExact(ret[0].NenGetsu + 
                shimeBi.ShimeBi.ToString("00"), 
                "yyyyMMdd", null);

            return ret[0];
            
        }

        //ユーザーマスタ
        public static List<SiGetsujiShime> GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiGetsujiShime), MethodBase.GetCurrentMethod());
            List<SiGetsujiShime> ret = new List<SiGetsujiShime>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiGetsujiShime> mapping = new MappingBase<SiGetsujiShime>();

            Params.Add(new DBParameter("MS_USER_ID", ms_user_id));   

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiGetsujiShime), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_GETSUJI_SHIME_ID", SiGetsujiShimeID));
            Params.Add(new DBParameter("NEN_GETSU", NenGetsu));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiGetsujiShime), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("NEN_GETSU", NenGetsu));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SI_GETSUJI_SHIME_ID", SiGetsujiShimeID));

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
        //    Params.Add(new DBParameter("PK", SiGetsujiShimeID));

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
            Params.Add(new DBParameter("PK", SiGetsujiShimeID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
