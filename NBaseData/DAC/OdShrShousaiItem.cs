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
    [TableAttribute("OD_SHR_SHOUSAI_ITEM")]
    public class OdShrShousaiItem : IGenericCloneable<OdShrShousaiItem>, ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 支払詳細品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_SHOUSAI_ITEM_ID", true)]
        public string OdShrShousaiItemID { get; set; }

        /// <summary>
        /// 支払品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_ITEM_ID")]
        public string OdShrItemID { get; set; }

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
        /// 品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("ITEM_NAME")]
        public string ItemName { get; set; }

        /// <summary>
        /// 船用品名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ITEM_NAME")]
        public string MsVesselItemName { get; set; }

        /// <summary>
        /// 潤滑油名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_NAME")]
        public string MsLoName { get; set; }

        /// <summary>
        /// 単位名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_NAME")]
        public string MsTaniName { get; set; }

        /// <summary>
        /// 表示順序
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }


        /// <summary>
        /// 受領詳細品目ID
        /// 2011.05 Add
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_SHOUSAI_ITEM_ID")]
        public string OdJryShousaiItemID { get; set; }

        #endregion

        /// <summary>
        /// 小修理詳細品目マスタ（フリーメンテナンス）に登録するフラグ
        /// </summary>
        [DataMember]
        public bool SaveDB { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OdShrShousaiItem()
        {
        }

        public static List<OdShrShousaiItem> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrShousaiItem), MethodBase.GetCurrentMethod());
            List<OdShrShousaiItem> ret = new List<OdShrShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShrShousaiItem> mapping = new MappingBase<OdShrShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdShrShousaiItem> GetRecordsByOdShrID(NBaseData.DAC.MsUser loginUser, string OdShrID)
        {
            return GetRecordsByOdShrID(null, loginUser, OdShrID);
        }
        public static List<OdShrShousaiItem> GetRecordsByOdShrID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdShrID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrShousaiItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrShousaiItem), "ByOdShrID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrShousaiItem), "OrderByShowOrder");

            List<OdShrShousaiItem> ret = new List<OdShrShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShrShousaiItem> mapping = new MappingBase<OdShrShousaiItem>();
            Params.Add(new DBParameter("OD_SHR_ID", OdShrID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }



        //潤滑油
        public static List<OdShrShousaiItem> GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrShousaiItem), MethodBase.GetCurrentMethod());

            List<OdShrShousaiItem> ret = new List<OdShrShousaiItem>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_LO_ID", ms_lo_id));

            MappingBase<OdShrShousaiItem> mapping = new MappingBase<OdShrShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //船用品
        public static List<OdShrShousaiItem> GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrShousaiItem), MethodBase.GetCurrentMethod());

            List<OdShrShousaiItem> ret = new List<OdShrShousaiItem>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", ms_vesselitem_id));

            MappingBase<OdShrShousaiItem> mapping = new MappingBase<OdShrShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdShrShousaiItem GetRecord(NBaseData.DAC.MsUser loginUser, string OdShrShousaiItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrShousaiItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrShousaiItem), "ByOdShrShousaiItemID");

            List<OdShrShousaiItem> ret = new List<OdShrShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OS_SHR_SHOUSAI_ITEM_ID", OdShrShousaiItemID));
            MappingBase<OdShrShousaiItem> mapping = new MappingBase<OdShrShousaiItem>();
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrShousaiItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_SHR_SHOUSAI_ITEM_ID", OdShrShousaiItemID));
            Params.Add(new DBParameter("OD_SHR_ITEM_ID", OdShrItemID));
            Params.Add(new DBParameter("SHOUSAI_ITEM_NAME", ShousaiItemName));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("COUNT", Count));
            Params.Add(new DBParameter("MS_TANI_ID", MsTaniID));
            Params.Add(new DBParameter("TANKA", Tanka));
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
            Params.Add(new DBParameter("OD_JRY_SHOUSAI_ITEM_ID", OdJryShousaiItemID));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrShousaiItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_SHR_ITEM_ID", OdShrItemID));
            Params.Add(new DBParameter("SHOUSAI_ITEM_NAME", ShousaiItemName));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("COUNT", Count));
            Params.Add(new DBParameter("MS_TANI_ID", MsTaniID));
            Params.Add(new DBParameter("TANKA", Tanka));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("OD_JRY_SHOUSAI_ITEM_ID", OdJryShousaiItemID));

            Params.Add(new DBParameter("OD_SHR_SHOUSAI_ITEM_ID", OdShrShousaiItemID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region IGenericCloneable<OdShrShousaiItem> メンバ

        public OdShrShousaiItem Clone()
        {
            OdShrShousaiItem clone = new OdShrShousaiItem();

            clone.OdShrShousaiItemID = OdShrShousaiItemID;
            clone.OdShrItemID = OdShrItemID;
            clone.ShousaiItemName = ShousaiItemName;
            clone.MsVesselItemID = MsVesselItemID;
            clone.MsLoID = MsLoID;
            clone.Count = Count;
            clone.MsTaniID = MsTaniID;
            clone.Tanka = Tanka;
            clone.Bikou = Bikou;
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
            clone.OdJryShousaiItemID = OdJryShousaiItemID;
            
            return clone;
        }

        #endregion

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", OdShrShousaiItemID));

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
            Params.Add(new DBParameter("PK", OdShrShousaiItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
