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
    [TableAttribute("OD_THI_SHOUSAI_ITEM")]
    public class OdThiShousaiItem : ISyncTable, IGenericCloneable<OdThiShousaiItem>
    {
        #region データメンバ
        /// <summary>
        /// 手配依頼詳細品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_SHOUSAI_ITEM_ID", true)]
        public string OdThiShousaiItemID { get; set; }

        /// <summary>
        /// 手配依頼品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_ITEM_ID")]
        public string OdThiItemID { get; set; }

        /// <summary>
        /// 手配依頼品目ID >> 手配依頼品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_ITEM_NAME")]
        public string OdThiItemName { get; set; }

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
        /// 船用品ID >> 船用品名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ITEM_NAME")]
        public string MsVesselItemName { get; set; }

        /// <summary>
        /// 潤滑油ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_ID")]
        public string MsLoID { get; set; }

        /// <summary>
        /// 潤滑油ID >> 潤滑油名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_NAME")]
        public string MsLoName { get; set; }

        /// <summary>
        /// 在庫数
        /// </summary>
        [DataMember]
        [ColumnAttribute("ZAIKO_COUNT")]
        public int ZaikoCount { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [ColumnAttribute("COUNT")]
        public int Count { get; set; }

        /// <summary>
        /// 査定数
        /// </summary>
        [DataMember]
        [ColumnAttribute("SATEISU")]
        public int Sateisu { get; set; }

        /// <summary>
        /// 単位ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_ID")]
        public string MsTaniID { get; set; }

        /// <summary>
        /// 単位ID >> 単位名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_NAME")]
        public string MsTaniName { get; set; }

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
        /// 同期:データNo
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
        /// 単価
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANKA")]
        public decimal Tanka { get; set; }

        /// <summary>
        /// 手配ID (JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_ID")]
        public string OdThiID { get; set; }

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



        /// <summary>
        /// カテゴリ番号 (JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("CATEGORY_NUMBER")]
        public int CategoryNumber { get; set; }

        /// <summary>
        /// カテゴリ名 (JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 特定船用品フラグ (JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SPECIFIC_FLAG")]
        public int SpecificFlag { get; set; }


        #endregion


        /// <summary>
        /// 小修理詳細品目マスタ（フリーメンテナンス）に登録するフラグ
        /// </summary>
        [DataMember]
        public bool SaveDB { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OdThiShousaiItem()
        {
        }

        public static List<OdThiShousaiItem> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), MethodBase.GetCurrentMethod());
            List<OdThiShousaiItem> ret = new List<OdThiShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiShousaiItem> mapping = new MappingBase<OdThiShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdThiShousaiItem> GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), MethodBase.GetCurrentMethod());
            List<OdThiShousaiItem> ret = new List<OdThiShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiShousaiItem> mapping = new MappingBase<OdThiShousaiItem>();
            Params.Add(new DBParameter("MS_LO_ID", ms_lo_id));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdThiShousaiItem> GetRecordsByOdThiID(NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), "ByOdThiID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), "OrderByShowOrder");

            List<OdThiShousaiItem> ret = new List<OdThiShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiShousaiItem> mapping = new MappingBase<OdThiShousaiItem>();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdThiShousaiItem> GetRecordsByOdThiIDs(NBaseData.DAC.MsUser loginUser, List<string> OdThiIDs)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), "GetRecords");

            List<OdThiShousaiItem> ret = new List<OdThiShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiShousaiItem> mapping = new MappingBase<OdThiShousaiItem>();

            if (OdThiIDs.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), "FilterByOdJryIDs");
                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", OdThiIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_OD_THI_IDS#", innerSQLStr);
                Params.AddInnerParams("p", OdThiIDs);
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), "OrderByShowOrder");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdThiShousaiItem> GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), MethodBase.GetCurrentMethod());

            List<OdThiShousaiItem> ret = new List<OdThiShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiShousaiItem> mapping = new MappingBase<OdThiShousaiItem>();

            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", ms_vesselitem_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdThiShousaiItem GetRecord(NBaseData.DAC.MsUser loginUser, string OdThiShousaiItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), "ByOdThiShousaiItemID");

            List<OdThiShousaiItem> ret = new List<OdThiShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiShousaiItem> mapping = new MappingBase<OdThiShousaiItem>();
            Params.Add(new DBParameter("OD_THI_SHOUSAI_ITEM_ID", OdThiShousaiItemID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }



        public static List<OdThiShousaiItem> GetRecordByThiIraiSbtID(NBaseData.DAC.MsUser loginUser, string thiIraiSbtId, int msVesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), "ByThiIraiSbtID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), "OrderByShowOrder");

            List<OdThiShousaiItem> ret = new List<OdThiShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiShousaiItem> mapping = new MappingBase<OdThiShousaiItem>();
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", thiIraiSbtId));
            Params.Add(new DBParameter("VESSEL_ID", msVesselId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }



        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_SHOUSAI_ITEM_ID", OdThiShousaiItemID));
            Params.Add(new DBParameter("OD_THI_ITEM_ID", OdThiItemID));
            Params.Add(new DBParameter("SHOUSAI_ITEM_NAME", ShousaiItemName));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("ZAIKO_COUNT", ZaikoCount));
            Params.Add(new DBParameter("COUNT", Count));
            Params.Add(new DBParameter("SATEISU", Sateisu));
            Params.Add(new DBParameter("MS_TANI_ID", MsTaniID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ITEM_ID", OdThiItemID));
            Params.Add(new DBParameter("SHOUSAI_ITEM_NAME", ShousaiItemName));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("ZAIKO_COUNT", ZaikoCount));
            Params.Add(new DBParameter("COUNT", Count));
            if (Sateisu == int.MinValue)
            {
                Params.Add(new DBParameter("SATEISU", null));
            }
            else
            {
                Params.Add(new DBParameter("SATEISU", Sateisu));
            }
            Params.Add(new DBParameter("MS_TANI_ID", MsTaniID));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("OD_THI_SHOUSAI_ITEM_ID", OdThiShousaiItemID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), MethodBase.GetCurrentMethod());

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
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), MethodBase.GetCurrentMethod());

        //    #region パラメタ
        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("OD_THI_SHOUSAI_ITEM_ID", OdThiShousaiItemID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiShousaiItem), MethodBase.GetCurrentMethod());

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
        //    Params.Add(new DBParameter("PK", OdThiShousaiItemID));

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
            Params.Add(new DBParameter("PK", OdThiShousaiItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        #region IGenericCloneable<OdThiShousaiItem> メンバ

        public OdThiShousaiItem Clone()
        {
            OdThiShousaiItem clone = new OdThiShousaiItem();

            clone.OdThiShousaiItemID = OdThiShousaiItemID;
            clone.OdThiItemID = OdThiItemID;
            clone.OdThiItemName = OdThiItemName;
            clone.ShousaiItemName = ShousaiItemName;
            clone.MsVesselItemID = MsVesselItemID;
            clone.MsVesselItemName = MsVesselItemName;
            clone.MsLoID = MsLoID;
            clone.MsLoName = MsLoName;
            clone.ZaikoCount = ZaikoCount;
            clone.Count = Count;
            clone.Sateisu = Sateisu;
            clone.MsTaniID = MsTaniID;
            clone.MsTaniName = MsTaniName;
            clone.Bikou = Bikou;
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

            clone.SpecificFlag = SpecificFlag;
        
            return clone;
        }

        #endregion
    }
}
