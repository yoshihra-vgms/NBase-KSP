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
    [TableAttribute("OD_THI_ITEM")]
    public class OdThiItem : ISyncTable, IGenericCloneable<OdThiItem>
    {
        #region データメンバ

        /// <summary>
        /// 手配依頼品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_ITEM_ID", true)]
        public string OdThiItemID { get; set; }

        /// <summary>
        /// 品目種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ITEM_SBT_ID")]
        public string MsItemSbtID { get; set; }

        /// <summary>
        /// 品目種別ID >> 品目種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ITEM_SBT_NAME")]
        public string MsItemSbtName { get; set; }

        /// <summary>
        /// ヘッダ
        /// </summary>
        [DataMember]
        [ColumnAttribute("HEADER")]
        public string Header { get; set; }

        /// <summary>
        /// 品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("ITEM_NAME")]
        public string ItemName { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 手配依頼ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_ID")]
        public string OdThiID { get; set; }

        /// <summary>
        /// 送信フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        /// <summary>
        /// データNo
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
        /// 更新者(UserID)
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
        /// 添付ファイルID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_ATTACH_FILE_ID")]
        public string OdAttachFileID { get; set; }

        /// <summary>
        /// 添付ファイル名
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_ATTACH_FILE_NAME")]
        public string OdAttachFileName { get; set; }

        #endregion

        /// <summary>
        /// 手配依頼詳細品目
        /// </summary>
        [DataMember]
        private List<OdThiShousaiItem> odThiShousaiItems;
        public List<OdThiShousaiItem> OdThiShousaiItems
        {
            get
            {
                if (odThiShousaiItems == null)
                {
                    odThiShousaiItems = new List<OdThiShousaiItem>();
                }

                return odThiShousaiItems;
            }
        }

        /// <summary>
        /// 小修理品目マスタ（フリーメンテナンス）に登録するフラグ
        /// </summary>
        [DataMember]
        public bool SaveDB { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OdThiItem()
        {
        }

        public static List<OdThiItem> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), MethodBase.GetCurrentMethod());
            List<OdThiItem> ret = new List<OdThiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiItem> mapping = new MappingBase<OdThiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdThiItem> GetRecordsByOdThiID(NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), "ByOdThiID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), "OrderByShowOrder");

            List<OdThiItem> ret = new List<OdThiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiItem> mapping = new MappingBase<OdThiItem>();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdThiItem> GetRecordsByOdThiIDs(NBaseData.DAC.MsUser loginUser, List<string> OdThiIDs)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), "GetRecords");

            List<OdThiItem> ret = new List<OdThiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiItem> mapping = new MappingBase<OdThiItem>();

            if (OdThiIDs.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), "FilterByOdJryIDs");
                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", OdThiIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_OD_THI_IDS#", innerSQLStr);
                Params.AddInnerParams("p", OdThiIDs);
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), "OrderByShowOrder");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdThiItem GetRecord(NBaseData.DAC.MsUser loginUser, string OdThiItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), "ByOdThiItemID");

            List<OdThiItem> ret = new List<OdThiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiItem> mapping = new MappingBase<OdThiItem>();
            Params.Add(new DBParameter("OD_THI_ITEM_ID", OdThiItemID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ITEM_ID", OdThiItemID));
            Params.Add(new DBParameter("MS_ITEM_SBT_ID", MsItemSbtID));
            Params.Add(new DBParameter("HEADER", Header));
            Params.Add(new DBParameter("ITEM_NAME", ItemName));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            Params.Add(new DBParameter("OD_ATTACH_FILE_ID", OdAttachFileID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }
        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_ITEM_SBT_ID", MsItemSbtID));
            Params.Add(new DBParameter("HEADER", Header));
            Params.Add(new DBParameter("ITEM_NAME", ItemName));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("OD_THI_ITEM_ID", OdThiItemID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            Params.Add(new DBParameter("OD_ATTACH_FILE_ID", OdAttachFileID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        public static bool CancelByOdThiID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        #region 2009/08/26 物理削除はしないように修正
        //public bool DeleteRecord(NBaseData.DAC.MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), MethodBase.GetCurrentMethod());

        //    #region パラメタ
        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("OD_THI_ITEM_ID", OdThiItemID));
        //    Params.Add(new DBParameter("TS", Ts));
        //    #endregion

        //    int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

        //    bool rv = true;
        //    if (cnt == 0) rv = false;
        //    return rv;
        //}
        #endregion

        public static bool DeleteRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", OdThiItemID));

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
            Params.Add(new DBParameter("PK", OdThiItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        #region IGenericCloneable<OdThiItem> メンバ

        public OdThiItem Clone()
        {
            OdThiItem clone = new OdThiItem();

            clone.OdThiItemID = OdThiItemID;
            clone.MsItemSbtID = MsItemSbtID;
            clone.MsItemSbtName = MsItemSbtName;
            clone.Header = Header;
            clone.ItemName = ItemName;
            clone.Bikou = Bikou;
            clone.OdThiID = OdThiID;
            clone.SendFlag = SendFlag;
            clone.VesselID = VesselID;
            clone.DataNo = DataNo;
            clone.UserKey = UserKey;
            clone.RenewDate = RenewDate;
            clone.RenewUserID = RenewUserID;
            clone.Ts = Ts;
            clone.CancelFlag = CancelFlag;
            clone.SaveDB = SaveDB;
            clone.ShowOrder = ShowOrder;
            clone.OdAttachFileID = OdAttachFileID;
            clone.OdAttachFileName = OdAttachFileName;

            foreach (OdThiShousaiItem si in OdThiShousaiItems)
            {
                clone.OdThiShousaiItems.Add(si.Clone());
            }
            
            return clone;
        }
        
        #endregion
    }
}
