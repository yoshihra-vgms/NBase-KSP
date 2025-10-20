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
    [TableAttribute("OD_CHOZO_SHOUSAI")]
    public class OdChozoShousai : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 貯蔵品詳細ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_CHOZO_SHOUSAI_ID", true)]
        public string OdChozoShousaiID { get; set; }

        /// <summary>
        /// 貯蔵品ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_CHOZO_ID")]
        public string OdChozoID { get; set; }


        /// <summary>
        /// 潤滑油ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_ID")]
        public string MsLoID { get; set; }

        /// <summary>
        /// 船用品ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ITEM_ID")]
        public string MsVesselItemID { get; set; }

        /// <summary>
        /// 詳細品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("ITEM_NAME")]
        public string ItemName { get; set; }

        /// <summary>
        /// 単位ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_ID")]
        public string MsTaniID { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [ColumnAttribute("COUNT")]
        public int Count { get; set; }
  
        
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
        /// このレコードの残量に対する最初の受入年月
        /// </summary>
        [DataMember]
        [ColumnAttribute("UKEIRE_NENGETSU")]
        public string UkeireNengetsu { get; set; }


        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 潤滑油名
        /// </summary>
        [DataMember]
        [ColumnAttribute("LO_NAME")]
        public string LoName { get; set; }

        /// <summary>
        /// 単位名
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANI_NAME")]
        public string TaniName { get; set; }

        /// <summary>
        /// 船用品名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ITEM_NAME")]
        public string VesselItemName { get; set; }
        
        #endregion

        public OdChozoShousai()
        {
        }

        public static List<OdChozoShousai> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdChozoShousai GetRecord(NBaseData.DAC.MsUser loginUser, string OdChozoShousaiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_CHOZO_SHOUSAI_ID", OdChozoShousaiID));
            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        //トランザクションに対応
        public static OdChozoShousai GetRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser, string OdChozoShousaiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_CHOZO_SHOUSAI_ID", OdChozoShousaiID));
            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(dbcone, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        //年月と船ＩＤを指定してデータを取得する
        public static List<OdChozoShousai> GetRecordsByVesselID_Date(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate)
        {
            
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();            
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("NENGETSU", sdate));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselid));            
            
            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;

        }
        

        //船と年月と種別を指定してデータを取得する
        public static List<OdChozoShousai> GetRecordsByVesselID_Date_Shubetsu(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate, int syubetsu)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("NENGETSU", sdate));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselid));
            Params.Add(new DBParameter("SHUBETSU", syubetsu));

            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;

        }

        //船と年月の期間と種別を指定してデータを取得する
        public static List<OdChozoShousai> GetRecordsByVesselID_Period_Shubetsu(NBaseData.DAC.MsUser loginUser, int vesselid, string start_date, string end_date, int syubetsu)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("START_DATE", start_date));
            Params.Add(new DBParameter("END_DATE", end_date));
            
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselid));
            Params.Add(new DBParameter("SHUBETSU", syubetsu));

            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;

        }




        //船と年月の期間とMsLoIDを指定してデータを取得する。
        public static List<OdChozoShousai> GetRecordsByVesselID_Period_MsLoID(NBaseData.DAC.MsUser loginUser, int vesselid, string start_date, string end_date, string ms_lo_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("START_DATE", start_date));
            Params.Add(new DBParameter("END_DATE", end_date));

            Params.Add(new DBParameter("MS_VESSEL_ID", vesselid));

            Params.Add(new DBParameter("MS_LO_ID", ms_lo_id));

            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;

        }


        //船と年月の期間とMsVesselItemIDを指定してデータを取得する。
        public static List<OdChozoShousai> GetRecordsByVesselID_Period_MsVesselItemID(NBaseData.DAC.MsUser loginUser, int vesselid, string start_date, string end_date, string ms_item_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("START_DATE", start_date));
            Params.Add(new DBParameter("END_DATE", end_date));

            Params.Add(new DBParameter("MS_VESSEL_ID", vesselid));

            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", ms_item_id));

            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;

        }




        //潤滑油
        public static List<OdChozoShousai> GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_LO_ID", ms_lo_id));


            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;

        }





        public static List<OdChozoShousai> GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id)
        {

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", ms_vesselitem_id));


            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;

        }











        // 年月と種別を指定してデータを取得する
        // Phase3で追加
        public static List<OdChozoShousai> GetRecordsByDate_Shubetsu(NBaseData.DAC.MsUser loginUser, string sdate, int shubetsu)
        {

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("NENGETSU", sdate));
            Params.Add(new DBParameter("SHUBETSU", shubetsu));

            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;

        }
        // 船と年月と潤滑油を指定してデータを取得する
        // Phase3で追加
        public static OdChozoShousai GetRecordsByVesselID_Date_LoID(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate, string loid)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("NENGETSU", sdate));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselid));
            Params.Add(new DBParameter("MS_LO_ID", loid));

            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
        // 船と年月と船用品を指定してデータを取得する
        // Phase3で追加
        public static OdChozoShousai GetRecordsByVesselID_Date_VesselItemID(NBaseData.DAC.MsUser loginUser, int vesselid, string sdate, string vesselitemid)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());
            List<OdChozoShousai> ret = new List<OdChozoShousai>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("NENGETSU", sdate));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselid));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", vesselitemid));

            MappingBase<OdChozoShousai> mapping = new MappingBase<OdChozoShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }




        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_CHOZO_SHOUSAI_ID", OdChozoShousaiID));         
            Params.Add(new DBParameter("OD_CHOZO_ID", OdChozoID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", this.MsVesselItemID));

            Params.Add(new DBParameter("ITEM_NAME", ItemName));
            Params.Add(new DBParameter("COUNT", Count));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("UKEIRE_NENGETSU", UkeireNengetsu));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool InsertRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_CHOZO_SHOUSAI_ID", OdChozoShousaiID));
            Params.Add(new DBParameter("OD_CHOZO_ID", OdChozoID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", this.MsVesselItemID));

            Params.Add(new DBParameter("ITEM_NAME", ItemName));
            Params.Add(new DBParameter("COUNT", Count));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("UKEIRE_NENGETSU", UkeireNengetsu));

            cnt = DBConnect.ExecuteNonQuery(dbcone, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_CHOZO_ID", OdChozoID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("ITEM_NAME", ItemName));
            Params.Add(new DBParameter("COUNT", Count)); 

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("UKEIRE_NENGETSU", UkeireNengetsu));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("OD_CHOZO_SHOUSAI_ID", OdChozoShousaiID)); 

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        //トランザクションはります
        public bool UpdateRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdChozoShousai), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_CHOZO_ID", OdChozoID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("ITEM_NAME", ItemName));
            Params.Add(new DBParameter("COUNT", Count));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("UKEIRE_NENGETSU", UkeireNengetsu));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("OD_CHOZO_SHOUSAI_ID", OdChozoShousaiID));

            cnt = DBConnect.ExecuteNonQuery(dbcone, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;


            return true;
        }

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", OdChozoShousaiID));

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
            Params.Add(new DBParameter("PK", OdChozoShousaiID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
