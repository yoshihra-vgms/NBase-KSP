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
using NBaseUtil;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_SOUKIN")]
    public class SiSoukin : ISyncTable
    {
        #region データメンバ
        
        /// <summary>
        /// 船内準備金送金ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SOUKIN_ID", true)]
        public string SiSoukinID { get; set; }




        /// <summary>
        /// 船内準備金ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_JUNBIKIN_ID")]
        public string SiJunbikinID { get; set; }

        /// <summary>
        /// 船ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 送金ユーザID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SOUKIN_USER_ID")]
        public string SoukinUserID { get; set; }

        /// <summary>
        /// 受入ユーザID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("UKEIRE_USER_ID")]
        public string UkeireUserID { get; set; }

        /// <summary>
        /// 代理店ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_ID")]
        public string MsCustomerID { get; set; }

        
        
        
        /// <summary>
        /// 送金日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SOUKIN_DATE")]
        public DateTime SoukinDate { get; set; }

        /// <summary>
        /// 受入日
        /// </summary>
        [DataMember]
        [ColumnAttribute("UKEIRE_DATE")]
        public DateTime UkeireDate { get; set; }

        /// <summary>
        /// 食費
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOKUHI")]
        public int Shokuhi { get; set; }

        /// <summary>
        /// 旅費
        /// </summary>
        [DataMember]
        [ColumnAttribute("RYOHI")]
        public int Ryohi { get; set; }

        /// <summary>
        /// その他費用
        /// </summary>
        [DataMember]
        [ColumnAttribute("SONOTAHI")]
        public int Sonotahi { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("KINGAKU")]
        public int Kingaku { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        
        
        
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




        /// <summary>
        /// 送金ユーザ名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SOUKIN_USER_NAME")]
        public string SoukinUserName { get; set; }

        /// <summary>
        /// 受入ユーザ名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("UKEIRE_USER_NAME")]
        public string UkeireUserName { get; set; }

        /// <summary>
        /// 代理店名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("CUSTOMER_NAME")]
        public string CustomerName { get; set; }
        #endregion


        [DataMember]
        public List<PtAlarmInfo> AlarmInfoList { get; set; }




        public SiSoukin()
        {
            this.MsVesselID = Int32.MinValue;
        }
        
        
        
        
        public static List<SiSoukin> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), MethodBase.GetCurrentMethod());

            List<SiSoukin> ret = new List<SiSoukin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSoukin> mapping = new MappingBase<SiSoukin>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<SiSoukin> GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), MethodBase.GetCurrentMethod());

            List<SiSoukin> ret = new List<SiSoukin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSoukin> mapping = new MappingBase<SiSoukin>();


            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //ユーザーマスタ
        public static List<SiSoukin> GetRecordsByMsUserID(MsUser loginUser, string ms_user_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), MethodBase.GetCurrentMethod());

            List<SiSoukin> ret = new List<SiSoukin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSoukin> mapping = new MappingBase<SiSoukin>();


            Params.Add(new DBParameter("MS_USER_ID", ms_user_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }




        public static SiSoukin GetRecord(MsUser loginUser, string siSoukinId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), "BySiSoukinID");

            List<SiSoukin> ret = new List<SiSoukin>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSoukin> mapping = new MappingBase<SiSoukin>();
            Params.Add(new DBParameter("SI_SOUKIN_ID", siSoukinId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public static SiSoukin GetRecordByJunbikinId(MsUser loginUser, string siJunbikinId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), "BySiJunbikinID");

            List<SiSoukin> ret = new List<SiSoukin>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSoukin> mapping = new MappingBase<SiSoukin>();
            Params.Add(new DBParameter("SI_JUNBIKIN_ID", siJunbikinId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public static List<SiSoukin> GetRecordsByDateAndMsVesselID(MsUser loginUser, DateTime start, DateTime end, int msVesselId)
        {
            return GetRecordsByDateAndMsVesselID(loginUser, start, end, msVesselId, "ORDER BY SOUKIN_DATE DESC");
        }


        public static List<SiSoukin> GetRecordsByDateAndMsVesselID(MsUser loginUser, DateTime start, DateTime end, int msVesselId, string orderByStr)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), "GetRecords");

            List<SiSoukin> ret = new List<SiSoukin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSoukin> mapping = new MappingBase<SiSoukin>();

            if (msVesselId != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), "ByMsVesselID");
                Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            }

            if (start != DateTime.MinValue && end != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), "ByStartEnd");
                DateTime ds = MsSiGetsujiShimeBi.ToFrom_船員_月次締日(loginUser, start);
                Params.Add(new DBParameter("START_DATE", ds));
                DateTime de = MsSiGetsujiShimeBi.ToTo_船員_月次締日(loginUser, end);
                Params.Add(new DBParameter("END_DATE", de));
            }

            if (orderByStr != null && orderByStr != string.Empty)
            {
                SQL += " " + orderByStr;
            }

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            foreach (SiSoukin s in ret)
            {
                s.AlarmInfoList = PtAlarmInfo.GetRecordsBySanshoumotoId(loginUser, s.SiSoukinID);
            }

            return ret;
        }


        public static List<SiSoukin> GetRecords_未受入(MsUser loginUser, int msVesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), "GetRecords");
            ParameterConnection Params = new ParameterConnection();

            if (msVesselId != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), "ByMsVesselID");
                Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSoukin), "_未受入");

            List<SiSoukin> ret = new List<SiSoukin>();
            MappingBase<SiSoukin> mapping = new MappingBase<SiSoukin>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            foreach (SiSoukin s in ret)
            {
                s.AlarmInfoList = PtAlarmInfo.GetRecordsBySanshoumotoId(loginUser, s.SiSoukinID);
            }

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

            Params.Add(new DBParameter("SI_SOUKIN_ID", SiSoukinID));

            Params.Add(new DBParameter("SI_JUNBIKIN_ID", SiJunbikinID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("SOUKIN_USER_ID", SoukinUserID));
            Params.Add(new DBParameter("UKEIRE_USER_ID", UkeireUserID));
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));

            Params.Add(new DBParameter("SOUKIN_DATE", SoukinDate));
            Params.Add(new DBParameter("UKEIRE_DATE", UkeireDate));
            Params.Add(new DBParameter("SHOKUHI", Shokuhi));
            Params.Add(new DBParameter("RYOHI", Ryohi));
            Params.Add(new DBParameter("SONOTAHI", Sonotahi));
            Params.Add(new DBParameter("KINGAKU", Kingaku));
            Params.Add(new DBParameter("BIKOU", Bikou));

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

            Params.Add(new DBParameter("SI_JUNBIKIN_ID", SiJunbikinID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("SOUKIN_USER_ID", SoukinUserID));
            Params.Add(new DBParameter("UKEIRE_USER_ID", UkeireUserID));
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));

            Params.Add(new DBParameter("SOUKIN_DATE", SoukinDate));
            Params.Add(new DBParameter("UKEIRE_DATE", UkeireDate));
            Params.Add(new DBParameter("SHOKUHI", Shokuhi));
            Params.Add(new DBParameter("RYOHI", Ryohi));
            Params.Add(new DBParameter("SONOTAHI", Sonotahi));
            Params.Add(new DBParameter("KINGAKU", Kingaku));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_SOUKIN_ID", SiSoukinID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", SiSoukinID));

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
            Params.Add(new DBParameter("PK", SiSoukinID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiSoukinID == null;
        }
    }
}
