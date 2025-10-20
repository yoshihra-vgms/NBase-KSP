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
    public class OdMmItem
    {
        #region データメンバ

        /// <summary>
        /// 見積依頼品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MM_ITEM_ID")]
        public string OdMmItemID { get; set; }


        /// <summary>
        /// 見積依頼ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MM_ID")]
        public string OdMmID { get; set; }

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

        /// <summary>
        /// 見積依頼詳細品目
        /// </summary>
        private List<OdMmShousaiItem> odMmShousaiItems;
        public List<OdMmShousaiItem> OdMmShousaiItems
        {
            get
            {
                if (odMmShousaiItems == null)
                {
                    odMmShousaiItems = new List<OdMmShousaiItem>();
                }

                return odMmShousaiItems;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OdMmItem()
        {
        }

        public static List<OdMmItem> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmItem), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            List<OdMmItem> ret = new List<OdMmItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMmItem> mapping = new MappingBase<OdMmItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdMmItem> GetRecordsByOdMmID(NBaseData.DAC.MsUser loginUser, string OdMmID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmItem), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMmItem), "ByOdMmID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMmItem), "OrderByShowOrder");

            List<OdMmItem> ret = new List<OdMmItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMmItem> mapping = new MappingBase<OdMmItem>();
            Params.Add(new DBParameter("OD_MM_ID", OdMmID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdMmItem GetRecord(NBaseData.DAC.MsUser loginUser, string OdMmItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmItem), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMmItem), "ByOdMmItemID");

            List<OdMmItem> ret = new List<OdMmItem>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MM_ITEM_ID", OdMmItemID));
            MappingBase<OdMmItem> mapping = new MappingBase<OdMmItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<OdMmItem> GetRecordsByOdMkID(NBaseData.DAC.MsUser loginUser, string OdMkID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmItem), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(OdMmItem), "JoinOdMkID"));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMmItem), "ByOdMkID");

            List<OdMmItem> ret = new List<OdMmItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMmItem> mapping = new MappingBase<OdMmItem>();
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MM_ITEM_ID", OdMmItemID));
            Params.Add(new DBParameter("OD_MM_ID", OdMmID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMmItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MM_ID", OdMmID));
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

            Params.Add(new DBParameter("OD_MM_ITEM_ID", OdMmItemID));
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
