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
    [TableAttribute("OD_JRY_ITEM")]
    public class OdJryItem : ISyncTable, IGenericCloneable<OdJryItem>
    {
        #region データメンバ

        /// <summary>
        /// 受領品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ITEM_ID", true)]
        public string OdJryItemID { get; set; }

        /// <summary>
        /// 受領ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ID")]
        public string OdJryID { get; set; }

        /// <summary>
        /// 品目種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ITEM_SBT_ID")]
        public string MsItemSbtID { get; set; }

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
        /// 品目種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ITEM_SBT_NAME")]
        public string MsItemSbtName { get; set; }

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

        #endregion

        /// <summary>
        /// 受領詳細品目
        /// </summary>
        private List<OdJryShousaiItem> odJryShousaiItems;
        public List<OdJryShousaiItem> OdJryShousaiItems
        {
            get
            {
                if (odJryShousaiItems == null)
                {
                    odJryShousaiItems = new List<OdJryShousaiItem>();
                }

                return odJryShousaiItems;
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
        public OdJryItem()
        {
        }

        public static List<OdJryItem> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryItem), MethodBase.GetCurrentMethod());
            List<OdJryItem> ret = new List<OdJryItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJryItem> mapping = new MappingBase<OdJryItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdJryItem> GetRecordsByOdJryID(NBaseData.DAC.MsUser loginUser, string OdJryID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryItem), "ByOdJryID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryItem), "OrderByShowOrder");

            List<OdJryItem> ret = new List<OdJryItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJryItem> mapping = new MappingBase<OdJryItem>();
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdJryItem> GetRecordsByOdJryIDs(NBaseData.DAC.MsUser loginUser, List<string> OdJryIDs)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryItem), "GetRecords");

            List<OdJryItem> ret = new List<OdJryItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJryItem> mapping = new MappingBase<OdJryItem>();

            if (OdJryIDs.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryItem), "FilterByOdJryIDs");
                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", OdJryIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_OD_JRY_IDS#", innerSQLStr);
                Params.AddInnerParams("p", OdJryIDs);
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryItem), "OrderByShowOrder");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdJryItem GetRecord(NBaseData.DAC.MsUser loginUser, string OdJryItem)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryItem), "ByOdJryItemID");

            List<OdJryItem> ret = new List<OdJryItem>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_ITEM_ID", OdJryItem));
            MappingBase<OdJryItem> mapping = new MappingBase<OdJryItem>();
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_ITEM_ID", OdJryItemID));
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            Params.Add(new DBParameter("MS_ITEM_SBT_ID", MsItemSbtID));
            Params.Add(new DBParameter("HEADER", Header));
            Params.Add(new DBParameter("ITEM_NAME", ItemName));
            Params.Add(new DBParameter("BIKOU", Bikou));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            Params.Add(new DBParameter("MS_ITEM_SBT_ID", MsItemSbtID));
            Params.Add(new DBParameter("HEADER", Header));
            Params.Add(new DBParameter("ITEM_NAME", ItemName));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("OD_JRY_ITEM_ID", OdJryItemID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region 2009/08/26 物理削除はしないように修正
        //public bool DeleteRecord(NBaseData.DAC.MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiItem), MethodBase.GetCurrentMethod());

        //    #region パラメタ
        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("OD_JRY_ITEM_ID", OdJryItemID));
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
        //    Params.Add(new DBParameter("PK", OdJryItemID));

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
            Params.Add(new DBParameter("PK", OdJryItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        #region IGenericCloneable<OdJryItem> メンバ

        public OdJryItem Clone()
        {
            OdJryItem clone = new OdJryItem();

            clone.OdJryItemID = OdJryItemID;
            clone.OdJryID = OdJryID;
            clone.MsItemSbtID = MsItemSbtID;
            clone.Header = Header;
            clone.ItemName = ItemName;
            clone.Bikou = Bikou;
            clone.SendFlag = SendFlag;
            clone.VesselID = VesselID;
            clone.DataNo = DataNo;
            clone.UserKey = UserKey;
            clone.RenewDate = RenewDate;
            clone.RenewUserID = RenewUserID;
            clone.Ts = Ts;
            clone.MsItemSbtName = MsItemSbtName;
            clone.CancelFlag = CancelFlag;
            clone.SaveDB = SaveDB;
            clone.ShowOrder = ShowOrder;
            
            foreach (OdJryShousaiItem si in OdJryShousaiItems)
            {
                clone.OdJryShousaiItems.Add(si.Clone());
            }

            return clone;
        }

        #endregion
    }
}
