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
    [TableAttribute("OD_FURIKAE_TORITATE")]
    public class OdFurikaeToritate
    {
        #region データメンバ

        /// <summary>
        /// 振替取立ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_FURIKAE_TORITATE_ID", true)]
        public string OdFurikaeToritateID { get; set; }

        /// <summary>
        /// 取消フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CANCEL_FLAG")]
        public int CancelFlag { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 手配依頼種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_SBT_ID")]
        public string MsThiIraiSbtID { get; set; }

        /// <summary>
        /// 手配依頼詳細種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_SHOUSAI_ID")]
        public string MsThiIraiShousaiID { get; set; }

        /// <summary>
        /// 品目種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ITEM_SBT_ID")]
        public string MsItemSbtID { get; set; }

        /// <summary>
        /// 入渠科目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_NYUKYO_KAMOKU_ID")]
        public string MsNyukyoKamokuID { get; set; }

        /// <summary>
        /// 発注日
        /// </summary>
        [DataMember]
        [ColumnAttribute("HACHU_DATE")]
        public DateTime HachuDate { get; set; }

        /// <summary>
        /// 項目
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOUMOKU")]
        public string Koumoku { get; set; }

        /// <summary>
        /// 顧客ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_ID")]
        public string MsCustomerID { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [ColumnAttribute("COUNT")]
        public int Count { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANKA")]
        public decimal Tanka { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("AMOUNT")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 完了日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANRYOBI")]
        public DateTime Kanryobi { get; set; }

        /// <summary>
        /// 請求書日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEIKYUSHOBI")]
        public DateTime Seikyushobi { get; set; }

        /// <summary>
        /// 起票日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIHYOBI")]
        public DateTime Kihyobi { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 登録日
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 登録者
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREATE_USER_ID")]
        public string CreateUserID { get; set; }

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
        /// 船ID >> 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_NAME")]
        public string MsVesselName { get; set; }

        /// <summary>
        /// 手配依頼種別ID >> 手配依頼種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_SBT_NAME")]
        public string MsThiIraiSbtName { get; set; }

        /// <summary>
        /// 手配依頼詳細ID >> 手配依頼詳細名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_SHOUSAI_NAME")]
        public string MsThiIraiShousaiName { get; set; }

        /// <summary>
        /// 品目種別ID >> 品目種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ITEM_SBT_NAME")]
        public string MsItemSbtName { get; set; }

        /// <summary>
        /// 入渠科目ID >> 入渠科目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_NYUKYO_KAMOKU_NAME")]
        public string MsNyukyoKamokuName { get; set; }

        /// <summary>
        /// 顧客ID >>  顧客名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_NAME")]
        public string MsCustomerName { get; set; }

        /// <summary>
        /// 登録者 >> 登録者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREATE_USER_NAME")]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 更新者 >> 更新者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_NAME")]
        public string RenewUserName { get; set; }
        #endregion


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OdFurikaeToritate()
        {
            OdFurikaeToritateID = null;
            Count = int.MinValue;
            Tanka = decimal.MinValue;
            Amount = decimal.MinValue;
        }

        public static List<OdFurikaeToritate> GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, OdFurikaeToritateFilter filter)
        {
            return GetRecordsByFilter(null, loginUser, filter);
        }
        public static List<OdFurikaeToritate> GetRecordsByFilter(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, OdFurikaeToritateFilter filter)
        {
            List<OdFurikaeToritate> ret = new List<OdFurikaeToritate>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdFurikaeToritate> mapping = new MappingBase<OdFurikaeToritate>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), "GetRecords");
            if (filter.MsVesselID != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), "FilterByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselID));
            }
            if (filter.MsThiIraiSbtID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), "FilterByThiIraiSbtID");
                Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", filter.MsThiIraiSbtID));
            }
            if (filter.MsThiIraiShousaiID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), "FilterByThiIraiShousaiID");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", filter.MsThiIraiShousaiID));
            }
            if (filter.HachuDateFrom != null && filter.HachuDateFrom != DateTime.MinValue &&
                filter.HachuDateTo != null && filter.HachuDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), "FilterByHachuDateFromTo");
                Params.Add(new DBParameter("HACHU_DATE_FROM", filter.HachuDateFrom.ToShortDateString()));
                Params.Add(new DBParameter("HACHU_DATE_TO", filter.HachuDateTo.ToShortDateString()));
            }
            else if (filter.HachuDateFrom != null && filter.HachuDateFrom != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), "FilterByHachuDateFrom");
                Params.Add(new DBParameter("HACHU_DATE_FROM", filter.HachuDateFrom.ToShortDateString()));
            }
            else if (filter.HachuDateTo != null && filter.HachuDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), "FilterByHachuDateTo");
                Params.Add(new DBParameter("HACHU_DATE_TO", filter.HachuDateTo.ToShortDateString()));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), "FilterByOrder");

            ret = mapping.FillRecrods(dbConnect,loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdFurikaeToritate GetRecord(NBaseData.DAC.MsUser loginUser, string OdFurikaeToritateID)
        {
            return GetRecord(null, loginUser, OdFurikaeToritateID);
        }
        public static OdFurikaeToritate GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdFurikaeToritateID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), "ByOdFurikaeToritateID");

            List<OdFurikaeToritate> ret = new List<OdFurikaeToritate>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdFurikaeToritate> mapping = new MappingBase<OdFurikaeToritate>();
            Params.Add(new DBParameter("OD_FURIKAE_TORITATE_ID", OdFurikaeToritateID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_FURIKAE_TORITATE_ID", OdFurikaeToritateID));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbtID));
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", MsThiIraiShousaiID));
            Params.Add(new DBParameter("MS_ITEM_SBT_ID", MsItemSbtID));
            Params.Add(new DBParameter("MS_NYUKYO_KAMOKU_ID", MsNyukyoKamokuID));
            Params.Add(new DBParameter("HACHU_DATE", HachuDate));
            Params.Add(new DBParameter("KOUMOKU", Koumoku));
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("COUNT", Count));
            Params.Add(new DBParameter("TANKA", Tanka));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("KANRYOBI", Kanryobi));
            Params.Add(new DBParameter("SEIKYUSHOBI", Seikyushobi));
            Params.Add(new DBParameter("KIHYOBI", Kihyobi));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("CREATE_DATE", CreateDate));
            Params.Add(new DBParameter("CREATE_USER_ID", CreateUserID));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdFurikaeToritate), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbtID));
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", MsThiIraiShousaiID));
            Params.Add(new DBParameter("MS_ITEM_SBT_ID", MsItemSbtID));
            Params.Add(new DBParameter("MS_NYUKYO_KAMOKU_ID", MsNyukyoKamokuID));
            Params.Add(new DBParameter("HACHU_DATE", HachuDate));
            Params.Add(new DBParameter("KOUMOKU", Koumoku));
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("COUNT", Count));
            Params.Add(new DBParameter("TANKA", Tanka));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("KANRYOBI", Kanryobi));
            Params.Add(new DBParameter("SEIKYUSHOBI", Seikyushobi));
            Params.Add(new DBParameter("KIHYOBI", Kihyobi));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("OD_FURIKAE_TORITATE_ID", OdFurikaeToritateID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
