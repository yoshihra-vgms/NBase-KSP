using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using NBaseData.DS;
using System.Reflection;
using ORMapping.Atts;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("OD_SHR_ITEM")]
    public class OdShrItem : IGenericCloneable<OdShrItem>, ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 支払品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_ITEM_ID", true)]
        public string OdShrItemID { get; set; }

        /// <summary>
        /// 支払ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_ID")]
        public string OdShrID { get; set; }

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
        /// 取消フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CANCEL_FLAG")]
        public int CancelFlag { get; set; }

        /// <summary>
        /// 品目種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ITEM_SBT_NAME")]
        public string MsItemSbtName { get; set; }

        /// <summary>
        /// 表示順序
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }


        /// <summary>
        /// 受領品目ID
        /// 2011.05 Add
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ITEM_ID")]
        public string OdJryItemID { get; set; }

        #endregion

        /// <summary>
        /// 支払詳細品目
        /// </summary>
        private List<OdShrShousaiItem> odShrShousaiItems;
        public List<OdShrShousaiItem> OdShrShousaiItems
        {
            get
            {
                if (odShrShousaiItems == null)
                {
                    odShrShousaiItems = new List<OdShrShousaiItem>();
                }

                return odShrShousaiItems;
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
        public OdShrItem()
        {
        }

        public static List<OdShrItem> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrItem), MethodBase.GetCurrentMethod());
            List<OdShrItem> ret = new List<OdShrItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShrItem> mapping = new MappingBase<OdShrItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<OdShrItem> GetRecordsByOdShrID(NBaseData.DAC.MsUser loginUser, string OdShrID)
        {
            return GetRecordsByOdShrID(null, loginUser, OdShrID);
        }
        public static List<OdShrItem> GetRecordsByOdShrID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdShrID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrItem), "ByOdShrID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrItem), "OrderByShowOrder");

            List<OdShrItem> ret = new List<OdShrItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShrItem> mapping = new MappingBase<OdShrItem>();
            Params.Add(new DBParameter("OD_SHR_ID", OdShrID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdShrItem GetRecord(NBaseData.DAC.MsUser loginUser, string OdShrItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrItem), "ByOdShrItemID");

            List<OdShrItem> ret = new List<OdShrItem>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_SHR_ITEM_ID", OdShrItemID));
            MappingBase<OdShrItem> mapping = new MappingBase<OdShrItem>();
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_SHR_ITEM_ID", OdShrItemID));
            Params.Add(new DBParameter("OD_SHR_ID", OdShrID));
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


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }

        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_SHR_ID", OdShrID));
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
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("OD_JRY_ITEM_ID", OdJryItemID));

            Params.Add(new DBParameter("OD_SHR_ITEM_ID", OdShrItemID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region IGenericCloneable<OdShrItem> メンバ

        public OdShrItem Clone()
        {
            OdShrItem clone = new OdShrItem();

            clone.OdShrItemID = OdShrItemID;
            clone.OdShrID = OdShrID;
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
            clone.OdJryItemID = OdJryItemID;

            foreach (OdShrShousaiItem si in OdShrShousaiItems)
            {
                clone.OdShrShousaiItems.Add(si.Clone());
            }

            return clone;
        }

        #endregion

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", OdShrItemID));

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
            Params.Add(new DBParameter("PK", OdShrItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
