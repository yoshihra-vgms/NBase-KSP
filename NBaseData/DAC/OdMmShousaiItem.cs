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

namespace NBaseData.DAC
{
    [DataContract()]
    public class OdMmShousaiItem
    {
        #region データメンバ
        /// <summary>
        /// 見積依頼詳細品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MM_SHOUSAI_ITEM_ID")]
        public string OdMmShousaiItemID { get; set; }

        /// <summary>
        /// 見積依頼品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MM_ITEM_ID")]
        public string OdMmItemID { get; set; }

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
        /// 添付するかしないか
        /// </summary>
        [DataMember]
        public bool IsAttached { get; set; }

        #endregion

        public OdMmShousaiItem()
        {
        }

        public static List<OdMmShousaiItem> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            List<OdMmShousaiItem> ret = new List<OdMmShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMmShousaiItem> mapping = new MappingBase<OdMmShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdMmShousaiItem> GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string ms_lo_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            List<OdMmShousaiItem> ret = new List<OdMmShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMmShousaiItem> mapping = new MappingBase<OdMmShousaiItem>();

            Params.Add(new DBParameter("MS_LO_ID", ms_lo_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<OdMmShousaiItem> GetRecordsByMsVesselItemID(NBaseData.DAC.MsUser loginUser, string ms_vesselitem_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            List<OdMmShousaiItem> ret = new List<OdMmShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMmShousaiItem> mapping = new MappingBase<OdMmShousaiItem>();

            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", ms_vesselitem_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdMmShousaiItem> GetRecordsByOdMmID(NBaseData.DAC.MsUser loginUser, string OdMmID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), "ByOdMmID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), "OrderByShowOrder");

            List<OdMmShousaiItem> ret = new List<OdMmShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMmShousaiItem> mapping = new MappingBase<OdMmShousaiItem>();
            Params.Add(new DBParameter("OD_MM_ID", OdMmID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdMmShousaiItem GetRecord(NBaseData.DAC.MsUser loginUser, string OdMmShousaiItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), "ByOdMmShousaiItemID");

            List<OdMmShousaiItem> ret = new List<OdMmShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MM_SHOUSAI_ITEM_ID", OdMmShousaiItemID));
            MappingBase<OdMmShousaiItem> mapping = new MappingBase<OdMmShousaiItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<OdMmShousaiItem> GetRecordsByOdMkID(NBaseData.DAC.MsUser loginUser, string OdMkID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), "JoinOdMkID"));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), "ByOdMkID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), "OrderByShowOrder");

            List<OdMmShousaiItem> ret = new List<OdMmShousaiItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMmShousaiItem> mapping = new MappingBase<OdMmShousaiItem>();
            Params.Add(new DBParameter("OD_MK_ID", OdMkID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MM_SHOUSAI_ITEM_ID", OdMmShousaiItemID));
            Params.Add(new DBParameter("OD_MM_ITEM_ID", OdMmItemID));
            Params.Add(new DBParameter("SHOUSAI_ITEM_NAME", ShousaiItemName));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("COUNT", Count));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmShousaiItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MM_ITEM_ID", OdMmItemID));
            Params.Add(new DBParameter("SHOUSAI_ITEM_NAME", ShousaiItemName));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("COUNT", Count));
            Params.Add(new DBParameter("MS_TANI_ID", MsTaniID));
            Params.Add(new DBParameter("BIKOU", Bikou)); 

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));

            Params.Add(new DBParameter("OD_MM_SHOUSAI_ITEM_ID", OdMmShousaiItemID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            Params.Add(new DBParameter("OD_ATTACH_FILE_ID", OdAttachFileID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
