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

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("OD_JRY_SHOUSAI_ITEM")]
    public class OdJryShousaiItem : ISyncTable, IGenericCloneable<OdJryShousaiItem>
    {
        #region データメンバ

        /// <summary>
        /// 受領詳細品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_SHOUSAI_ITEM_ID", true)]
        public string OdJryShousaiItemID { get; set; }

        /// <summary>
        /// 受領品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ITEM_ID")]
        public string OdJryItemID { get; set; }

        /// <summary>
        /// 詳細品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOUSAI_ITEM_NAME")]
        public string ShousaiItemName { get; set; }

        /// <summary>
        /// 船用品ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ITEM_ID")]
        public string MsVesselItemID { get; set; }

        /// <summary>
        /// 潤滑油ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_ID")]
        public string MsLoID { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [ColumnAttribute("COUNT")]
        public int Count { get; set; }

        /// <summary>
        /// 受領数
        /// </summary>
        [DataMember]
        [ColumnAttribute("JRY_COUNT")]
        public int JryCount { get; set; }

        /// <summary>
        /// 単位ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_ID")]
        public string MsTaniID { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANKA")]
        public decimal Tanka { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 納品日
        /// </summary>
        [DataMember]
        [ColumnAttribute("NOUHINBI")]
        public DateTime Nouhinbi { get; set; }
        
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
        /// 船用品ID >> 船用品名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ITEM_NAME")]
        public string MsVesselItemName { get; set; }

        /// <summary>
        /// 潤滑油ID >> 潤滑油名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_NAME")]
        public string MsLoName { get; set; }

        /// <summary>
        /// 単位ID >> 単位名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_NAME")]
        public string MsTaniName { get; set; }

        /// <summary>
        /// 取消フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CANCEL_FLAG")]
        public int CancelFlag { get; set; }

        /// <summary>
        /// 表示順序
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }

        /// <summary>
        /// 受領ID (JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ID")]
        public string OdJryID { get; set; }

        #endregion

        /// <summary>
        /// 小修理詳細品目マスタ（フリーメンテナンス）に登録するフラグ
        /// </summary>
        [DataMember]
        public bool SaveDB { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OdJryShousaiItem()
        {
        }

        public static List<OdJryShousaiItem> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), MethodBase.GetCurrentMethod());
            List<OdJryShousaiItem> ret = new List<OdJryShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJryShousaiItem> mapping = new MappingBase<OdJryShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdJryShousaiItem> GetRecordsByOdJryID(NBaseData.DAC.MsUser loginUser, string OdJryID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), "ByOdJryID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), "OrderByShowOrder");

            List<OdJryShousaiItem> ret = new List<OdJryShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJryShousaiItem> mapping = new MappingBase<OdJryShousaiItem>();
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdJryShousaiItem> GetRecordsByOdJryIDs(NBaseData.DAC.MsUser loginUser, List<string> OdJryIDs)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), "GetRecords");

            List<OdJryShousaiItem> ret = new List<OdJryShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJryShousaiItem> mapping = new MappingBase<OdJryShousaiItem>();

            if (OdJryIDs.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), "FilterByOdJryIDs");
                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", OdJryIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_OD_JRY_IDS#", innerSQLStr);
                Params.AddInnerParams("p", OdJryIDs);
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), "OrderByShowOrder");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<OdJryShousaiItem> GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), MethodBase.GetCurrentMethod());

            List<OdJryShousaiItem> ret = new List<OdJryShousaiItem>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_LO_ID", ms_lo_id));

            MappingBase<OdJryShousaiItem> mapping = new MappingBase<OdJryShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }




        public static List<OdJryShousaiItem> GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), MethodBase.GetCurrentMethod());

            List<OdJryShousaiItem> ret = new List<OdJryShousaiItem>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", ms_vesselitem_id));

            MappingBase<OdJryShousaiItem> mapping = new MappingBase<OdJryShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdJryShousaiItem GetRecord(NBaseData.DAC.MsUser loginUser, string OdJryShousaiItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), "ByOdJryShousaiItemID");

            List<OdJryShousaiItem> ret = new List<OdJryShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_SHOUSAI_ITEM_ID", OdJryShousaiItemID));
            MappingBase<OdJryShousaiItem> mapping = new MappingBase<OdJryShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
        
        //船ID、種別、依頼フラグ、期間を指定して関連するものを取得する
        public static List<OdJryShousaiItem> GetRecordsByMsVesselID_JryStatus_ShousaiID_Date
            (NBaseData.DAC.MsUser loginUser, string syubetsu, int status, int ms_vessel_id, DateTime startdate, DateTime enddate)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), MethodBase.GetCurrentMethod());
            List<OdJryShousaiItem> ret = new List<OdJryShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
                        
            Params.Add(new DBParameter("OD_JRY_STATUS", status));
            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", syubetsu));
            Params.Add(new DBParameter("START_DATE", startdate));
            Params.Add(new DBParameter("END_DATE", enddate));
           
            MappingBase<OdJryShousaiItem> mapping = new MappingBase<OdJryShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            
            return ret;
        }



        //船ID、種別、依頼フラグ、期間、MsLoIDを指定して関連するものを取得する
        public static List<OdJryShousaiItem> GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsLoID
            (NBaseData.DAC.MsUser loginUser, string syubetsu, int status, int ms_vessel_id, DateTime startdate, DateTime enddate, string ms_lo_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), MethodBase.GetCurrentMethod());
            List<OdJryShousaiItem> ret = new List<OdJryShousaiItem>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("OD_JRY_STATUS", status));
            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", syubetsu));

            Params.Add(new DBParameter("MS_LO_ID", ms_lo_id));

            Params.Add(new DBParameter("START_DATE", startdate));
            Params.Add(new DBParameter("END_DATE", enddate));

            MappingBase<OdJryShousaiItem> mapping = new MappingBase<OdJryShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


            return ret;
        }





        //船ID、種別、依頼フラグ、期間、MsVesselItemIDを指定して関連するものを取得する
        public static List<OdJryShousaiItem> GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsVesselItemID
            (NBaseData.DAC.MsUser loginUser, string syubetsu, int status, int ms_vessel_id, DateTime startdate, DateTime enddate, string ms_ves_item_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), MethodBase.GetCurrentMethod());
            List<OdJryShousaiItem> ret = new List<OdJryShousaiItem>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("OD_JRY_STATUS", status));
            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", syubetsu));

            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", ms_ves_item_id));

            Params.Add(new DBParameter("START_DATE", startdate));
            Params.Add(new DBParameter("END_DATE", enddate));

            MappingBase<OdJryShousaiItem> mapping = new MappingBase<OdJryShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


            return ret;
        }





















        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_SHOUSAI_ITEM_ID", OdJryShousaiItemID));         
            Params.Add(new DBParameter("OD_JRY_ITEM_ID", OdJryItemID));                  
            Params.Add(new DBParameter("SHOUSAI_ITEM_NAME", ShousaiItemName));               
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));               
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));                        
            Params.Add(new DBParameter("COUNT", Count));
            if (JryCount == int.MinValue)
            {
                Params.Add(new DBParameter("JRY_COUNT", null));
            }
            else
            {
                Params.Add(new DBParameter("JRY_COUNT", JryCount));                       
            }
            Params.Add(new DBParameter("MS_TANI_ID", MsTaniID));
            Params.Add(new DBParameter("TANKA", Tanka));    
            Params.Add(new DBParameter("BIKOU", Bikou));                           
            Params.Add(new DBParameter("NOUHINBI", Nouhinbi)); 

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryShousaiItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_ITEM_ID", OdJryItemID));
            Params.Add(new DBParameter("SHOUSAI_ITEM_NAME", ShousaiItemName));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("COUNT", Count));
            Params.Add(new DBParameter("JRY_COUNT", JryCount));
            Params.Add(new DBParameter("MS_TANI_ID", MsTaniID));
            Params.Add(new DBParameter("TANKA", Tanka));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("NOUHINBI", Nouhinbi)); 

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("OD_JRY_SHOUSAI_ITEM_ID", OdJryShousaiItemID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region 2009/08/26 物理削除はしないように修正
        //public bool DeleteRecord(NBaseData.DAC.MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), MethodBase.GetCurrentMethod());

        //    #region パラメタ
        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("OD_JRY_SHOUSAI_ITEM_ID", OdJryShousaiItemID));
        //    Params.Add(new DBParameter("TS", Ts));
        //    #endregion

        //    int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

        //    bool rv = true;
        //    if (cnt == 0) rv = false;
        //    return rv;
        //}
        #endregion

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", OdJryShousaiItemID));

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
            Params.Add(new DBParameter("PK", OdJryShousaiItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        #region IGenericCloneable<OdJryShousaiItem> メンバ

        public OdJryShousaiItem Clone()
        {
            OdJryShousaiItem clone = new OdJryShousaiItem();

            clone.OdJryShousaiItemID = OdJryShousaiItemID;
            clone.OdJryItemID = OdJryItemID;
            clone.ShousaiItemName = ShousaiItemName;
            clone.MsVesselItemID = MsVesselItemID;
            clone.MsLoID = MsLoID;
            clone.Count = Count;
            clone.JryCount = JryCount;
            clone.MsTaniID = MsTaniID;
            clone.Tanka = Tanka;
            clone.Bikou = Bikou;
            clone.Nouhinbi = Nouhinbi;
            clone.SendFlag = SendFlag;
            clone.VesselID = VesselID;
            clone.DataNo = DataNo;
            clone.UserKey = UserKey;
            clone.RenewDate = RenewDate;
            clone.RenewUserID = RenewUserID;
            clone.Ts = Ts;
            clone.MsVesselItemName = MsVesselItemName;
            clone.MsLoName = MsLoName;
            clone.MsTaniName = MsTaniName;
            clone.CancelFlag = CancelFlag;
            clone.SaveDB = SaveDB;
            clone.ShowOrder = ShowOrder;
            
            return clone;
        }

        #endregion
    }
}
