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
    [TableAttribute("OD_MK")]
    public class OdMk : ISyncTable, IGenericCloneable<OdMk>
    {
        #region データメンバ
        /// <summary>
        /// 見積回答ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MK_ID", true)]
        public string OdMkID { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS")]
        public int Status { get; set; }

        /// <summary>
        /// 取消フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CANCEL_FLAG")]
        public int CancelFlag { get; set; }

        /// <summary>
        /// 見積依頼ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MM_ID")]
        public string OdMmID { get; set; }

        /// <summary>
        /// 顧客ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_ID")]
        public string MsCustomerID { get; set; }

        /// <summary>
        /// 顧客ID >>  顧客名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_NAME")]
        public string MsCustomerName { get; set; }

        /// <summary>
        /// 担当者
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANTOUSHA")]
        public string Tantousha { get; set; }

        /// <summary>
        /// 見積回答日
        /// </summary>
        [DataMember]
        [ColumnAttribute("MK_DATE")]
        public DateTime MkDate { get; set; }

        /// <summary>
        /// 納期
        /// </summary>
        [DataMember]
        [ColumnAttribute("NOUKI")]
        public DateTime Nouki { get; set; }

        /// <summary>
        /// 工期
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOUKI")]
        public string Kouki { get; set; }

        /// <summary>
        /// 見積回答番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("MK_NO")]
        public string MkNo { get; set; }

        /// <summary>
        /// 見積値引き
        /// </summary>
        [DataMember]
        [ColumnAttribute("MK_AMOUNT")]
        public decimal MkAmount { get; set; }

        /// <summary>
        /// 担当者メールアドレス
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANTOU_MAIL_ADDRESS")]
        public string TantouMailAddress { get; set; }

        /// <summary>
        /// 発注日
        /// </summary>
        [DataMember]
        [ColumnAttribute("HACHU_DATE")]
        public DateTime HachuDate { get; set; }

        /// <summary>
        /// 発注番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("HACHU_NO")]
        public string HachuNo { get; set; }

        /// <summary>
        /// 入渠科目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_NYUKYO_KAMOKU_ID")]
        public string MsNyukyoKamokuID { get; set; }

        /// <summary>
        /// 消費税額
        /// </summary>
        [DataMember]
        [ColumnAttribute("TAX")]
        public decimal Tax { get; set; }

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
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("AMOUNT")]
        public decimal Amount { get; set; }

        /// <summary>
        /// WEB_KEY
        /// </summary>
        [DataMember]
        [ColumnAttribute("WEB_KEY")]
        public string WebKey { get; set; }

        /// <summary>
        /// MK_KIGEN
        /// </summary>
        [DataMember]
        [ColumnAttribute("MK_KIGEN")]
        public string MkKigen { get; set; }

        /// <summary>
        /// MK_YUKOU_KIGEN
        /// </summary>
        [DataMember]
        [ColumnAttribute("MK_YUKOU_KIGEN")]
        public string MkYukouKigen { get; set; }

        /// <summary>
        /// 希望納期
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIBOUBI")]
        public DateTime Kiboubi { get; set; }

        /// <summary>
        /// 送料・運搬料
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARRIAGE")]
        public decimal Carriage { get; set; }

        /// <summary>
        /// 作成日
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 作成者
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREATE_USER_ID")]
        public string CreateUserID { get; set; }

        /// <summary>
        /// 見積依頼日
        /// </summary>
        [DataMember]
        [ColumnAttribute("MM_DATE")]
        public DateTime MmDate { get; set; }


        /// <summary>
        /// 船ID　← 手配依頼
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 船　← 手配依頼
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_NAME")]
        public string MsVesselName { get; set; }

        /// <summary>
        /// 手配依頼内容　← 手配依頼
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_NAIYOU")]
        public string OdThiNaiyou { get; set; }

        /// <summary>
        /// 備考　← 手配依頼
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_BIKOU")]
        public string OdThiBikou { get; set; }

        /// <summary>
        /// 手配依頼ID　← 手配依頼
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_ID")]
        public string OdThiID { get; set; }

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
        /// メール件名
        /// </summary>
        [DataMember]
        public string Subject { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        [DataMember]
        public string Tel { get; set; }

        /// <summary>
        /// FAX番号
        /// </summary>
        [DataMember]
        public string Fax { get; set; }


        /// <summary>
        /// 年度
        /// </summary>
        [DataMember]
        [ColumnAttribute("NENDO")]
        public string Nendo { get; set; }

        #endregion

        [DataMember]
        public OdStatus OdStatusValue = null;

        //public static int NoLength見積回答 = 9;
        public static int NoLength見積回答 = 10;
        public enum STATUS { 未回答, 回答済み, 発注承認依頼中, 発注承認済み, 発注済み };
        private string[] statusStr = { "未回答", "回答済み", "発注承認依頼中", "発注承認済み", "発注済み" };
        public string StatusName
        {
            get
            {
                return OdStatusValue.GetName(Status);
            }
        }


        // 発注承認なしの改造
        public static  decimal 承認しきい値 = 1000000;



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OdMk()
        {
            InitStatusValue();
            Nendo = null;
        }

        private void InitStatusValue()
        {

            OdStatusValue = new OdStatus();
            OdStatusValue.DefaultValue = 0;
            OdStatusValue.Values = new List<OdStatus.StatusValue>();
            for ( int i = 0; i < statusStr.Length; i ++ )
            {
                OdStatus.StatusValue value = new OdStatus.StatusValue();
                value.Value = i;
                value.Name = statusStr[i];
                OdStatusValue.Values.Add(value);
            }
        }

        public static List<OdMk> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMk), MethodBase.GetCurrentMethod());
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMk), "OrderByNo");
            List<OdMk> ret = new List<OdMk>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMk> mapping = new MappingBase<OdMk>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<OdMk> GetRecordsByOdMmID(NBaseData.DAC.MsUser loginUser, string OdMmID)
        {
            return GetRecordsByOdMmID(null, loginUser, OdMmID);
        }
        public static List<OdMk> GetRecordsByOdMmID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdMmID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMk), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMk), "ByOdMmID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMk), "OrderByNo");

            List<OdMk> ret = new List<OdMk>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MM_ID", OdMmID));
            MappingBase<OdMk> mapping = new MappingBase<OdMk>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static OdMk GetRecordByOdThiID(NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMk), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMk), "ByOdThiID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMk), "OrderByNo");

            List<OdMk> ret = new List<OdMk>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            MappingBase<OdMk> mapping = new MappingBase<OdMk>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<OdMk> GetRecordsByOdThiID(NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            return GetRecordsByOdThiID(null, loginUser, OdThiID);
        }

        public static List<OdMk> GetRecordsByOdThiID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMk), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMk), "ByOdThiID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMk), "OrderByNo");

            List<OdMk> ret = new List<OdMk>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            MappingBase<OdMk> mapping = new MappingBase<OdMk>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret;
        }

        public static OdMk GetRecordByWebKey(NBaseData.DAC.MsUser loginUser, string WebKey)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMk), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMk), "ByWebKey");

            List<OdMk> ret = new List<OdMk>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("WEB_KEY", WebKey));
            MappingBase<OdMk> mapping = new MappingBase<OdMk>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<OdMk> GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, int status, OdThiFilter filter)
        {
            List<OdMk> ret = new List<OdMk>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdMk> mapping = new MappingBase<OdMk>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMk), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            string CondStr = OdThi.GetConditionString(loginUser, filter, ref Params);
            SQL = SQL.Replace("AND OD_THI.CANCEL_FLAG = 0", "AND OD_THI.CANCEL_FLAG = 0 AND OD_THI.OD_THI_ID IN ( " + CondStr + " ) ");
            
            if (status != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMk), "FilterByStatus");
                Params.Add(new DBParameter("STATUS", status));
            }

            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)//(Common.DBTYPE == Common.DB_TYPE.ORACLE)// 201508 Oracle >> Postgresql対応
            {
                SQL = "SELECT DISTINCT * FROM ( " + SQL + " ) as SubTBL_ODMK " + SqlMapper.SqlMapper.GetSql(typeof(OdMk), "OrderByNo2");
            }

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (filter.Nendo != null && ret.Count() > 0)
            {
                ret = ret.Where(obj => obj.Nendo == filter.Nendo).ToList();
            }

            return ret;
        }

        public static OdMk GetRecord(NBaseData.DAC.MsUser loginUser, string OdMkID)
        {
            return GetRecord(null, loginUser, OdMkID);
        }
        public static OdMk GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdMkID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMk), "GetRecords");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdMk), "ByOdMkID");

            List<OdMk> ret = new List<OdMk>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MK_ID", OdMkID));
            MappingBase<OdMk> mapping = new MappingBase<OdMk>();
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMk), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MK_ID", OdMkID));
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("OD_MM_ID", OdMmID));
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("TANTOUSHA", Tantousha));
            Params.Add(new DBParameter("MK_DATE", MkDate));
            Params.Add(new DBParameter("NOUKI", Nouki));
            Params.Add(new DBParameter("KOUKI", Kouki));
            Params.Add(new DBParameter("MK_NO", MkNo));
            Params.Add(new DBParameter("MK_AMOUNT", MkAmount));
            Params.Add(new DBParameter("TANTOU_MAIL_ADDRESS", TantouMailAddress));
            Params.Add(new DBParameter("HACHU_DATE", HachuDate));
            Params.Add(new DBParameter("HACHU_NO", HachuNo));
            Params.Add(new DBParameter("MS_NYUKYO_KAMOKU_ID", MsNyukyoKamokuID));
            Params.Add(new DBParameter("TAX", Tax));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("WEB_KEY", WebKey));
            Params.Add(new DBParameter("MK_KIGEN", MkKigen));
            Params.Add(new DBParameter("MK_YUKOU_KIGEN", MkYukouKigen));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("KIBOUBI", Kiboubi));
            Params.Add(new DBParameter("CREATE_DATE", RenewDate));
            Params.Add(new DBParameter("CREATE_USER_ID", RenewUserID));
            Params.Add(new DBParameter("MM_DATE", MmDate));

            Params.Add(new DBParameter("CARRIAGE", Carriage));
            Params.Add(new DBParameter("NENDO", Nendo));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMk), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("OD_MM_ID", OdMmID));
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("TANTOUSHA", Tantousha));
            Params.Add(new DBParameter("MK_DATE", MkDate));
            Params.Add(new DBParameter("NOUKI", Nouki));
            Params.Add(new DBParameter("KOUKI", Kouki));
            Params.Add(new DBParameter("MK_NO", MkNo));
            Params.Add(new DBParameter("MK_AMOUNT", MkAmount));
            Params.Add(new DBParameter("TANTOU_MAIL_ADDRESS", TantouMailAddress));
            Params.Add(new DBParameter("HACHU_DATE", HachuDate));
            Params.Add(new DBParameter("HACHU_NO", HachuNo));
            Params.Add(new DBParameter("MS_NYUKYO_KAMOKU_ID", MsNyukyoKamokuID));
            Params.Add(new DBParameter("TAX", Tax));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("WEB_KEY", WebKey));
            Params.Add(new DBParameter("MK_KIGEN", MkKigen));
            Params.Add(new DBParameter("MK_YUKOU_KIGEN", MkYukouKigen));
            Params.Add(new DBParameter("KIBOUBI", Kiboubi));
            Params.Add(new DBParameter("MM_DATE", MmDate));
            Params.Add(new DBParameter("CREATE_DATE", CreateDate));
            Params.Add(new DBParameter("CREATE_USER_ID", CreateUserID));

            Params.Add(new DBParameter("CARRIAGE", Carriage));
            Params.Add(new DBParameter("NENDO", Nendo));

            Params.Add(new DBParameter("OD_MK_ID", OdMkID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool DeleteRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdMk), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MK_ID", OdMkID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", OdMkID));

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
            Params.Add(new DBParameter("PK", OdMkID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        #region IGenericCloneable<OdMk> メンバ

        public OdMk Clone()
        {
            OdMk clone = new OdMk();

            clone.OdMkID = OdMkID;
            clone.Status = Status;
            clone.CancelFlag = CancelFlag;
            clone.OdMmID = OdMmID;
            clone.MsCustomerID = MsCustomerID;
            clone.MsCustomerName = MsCustomerName;
            clone.Tantousha = Tantousha;
            clone.MkDate = MkDate;
            clone.Nouki = Nouki;
            clone.Kouki = Kouki;
            clone.MkNo = MkNo;
            clone.MkAmount = MkAmount;
            clone.TantouMailAddress = TantouMailAddress;
            clone.HachuDate = HachuDate;
            clone.HachuNo = HachuNo;
            clone.MsNyukyoKamokuID = MsNyukyoKamokuID;
            clone.Tax = Tax;
            clone.SendFlag = SendFlag;
            clone.VesselID = VesselID;
            clone.DataNo = DataNo;
            clone.UserKey = UserKey;
            clone.RenewDate = RenewDate;
            clone.RenewUserID = RenewUserID;
            clone.Ts = Ts;
            clone.Amount = Amount;
            clone.WebKey = WebKey;
            clone.MkKigen = MkKigen;
            clone.MkYukouKigen = MkYukouKigen;
            clone.Kiboubi = Kiboubi;
            clone.CreateDate = CreateDate;
            clone.CreateUserID = CreateUserID;
            clone.MmDate = MmDate;
            clone.Carriage = Carriage;

            clone.MsVesselID = MsVesselID;
            clone.MsVesselName = MsVesselName;

            return clone;
        }

        #endregion
    }
}
