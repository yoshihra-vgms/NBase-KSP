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
    [TableAttribute("OD_SHR_GASSAN_HEAD")]
    public class OdShrGassanHead
    {
        public enum StatusEnum { 支払未作成, 支払作成済, 支払済 };
        public string StatusStr
        {
            get{
                if (Status == 0)
                {
                    return "支払未作成";
                }
                else if (Status == 1)
                {
                    return "支払作成済";
                }
                else
                {
                    return "支払済";
                }
            }
        }

        #region データメンバ

        /// <summary>
        /// 支払合算ヘッダID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_GASSAN_HEAD_ID", true)]
        public string OdShrGassanHeadID { get; set; }

        /// <summary>
        /// 取消フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CANCEL_FLAG")]
        public int CancelFlag { get; set; }

        /// <summary>
        /// 顧客ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_ID")]
        public string MsCustomerID { get; set; }

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
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS")]
        public int Status { get; set; }

        /// <summary>
        /// 代表発注番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("HACHU_NO")]
        public string HachuNo { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("AMOUNT")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 受領ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ID")]
        public string OdJryID { get; set; }

        /// <summary>
        /// 支払ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_ID")]
        public string OdShrID { get; set; }

        #region ＤＢ共通項目

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

        #endregion

        /// <summary>
        /// 顧客ID >>  顧客名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_NAME")]
        public string MsCustomerName { get; set; }

        /// <summary>
        /// 手配依頼種別ID >> 手配依頼種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_IRAI_SBT_NAME")]
        public string ThiIraiSbtName { get; set; }

        /// <summary>
        /// 手配依頼詳細種別ID >> 依頼詳細名
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_IRAI_SHOUSAI_NAME")]
        public string ThiIraiShousaiName { get; set; }

        #endregion

        /// <summary>
        /// 支払合算項目
        /// </summary>
        private List<OdShrGassanItem> odShrGassanItems;
        public List<OdShrGassanItem> OdShrGassanItems
        {
            get
            {
                if (odShrGassanItems == null)
                {
                    odShrGassanItems = new List<OdShrGassanItem>();
                }

                return odShrGassanItems;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="msCustomerId"></param>
        /// <param name="msThiIraiSbtId"></param>
        /// <param name="msThiIraiShousaiId"></param>
        /// <param name="msVesselId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static List<OdShrGassanHead> GetRecords(NBaseData.DAC.MsUser loginUser, string msCustomerId, string msThiIraiSbtId, string msThiIraiShousaiId, int msVesselId, int status)
        {
            List<OdShrGassanHead> ret = new List<OdShrGassanHead>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShrGassanHead> mapping = new MappingBase<OdShrGassanHead>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), "GetRecords");
            if (msCustomerId != null && msCustomerId.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), "ByMsCustomerId");
                Params.Add(new DBParameter("MS_CUSTOMER_ID", msCustomerId));
            }
            if (msThiIraiSbtId != null && msThiIraiSbtId.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), "ByMsThiIraiSbtId");
                Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", msThiIraiSbtId));
            }
            if (msThiIraiShousaiId != null && msThiIraiShousaiId.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), "ByMsThiIraiShousaiId");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", msThiIraiShousaiId));
            }
            if (msVesselId > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), "ByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            }
            if (status != (int)StatusEnum.支払済)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), "ByStatus");
                Params.Add(new DBParameter("STATUS", (int)StatusEnum.支払済));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), "OrderBy");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static OdShrGassanHead GetRecord(NBaseData.DAC.MsUser loginUser, string OdShrGassanHeadId)
        {
            return GetRecord(null, loginUser, OdShrGassanHeadId);
        }
        public static OdShrGassanHead GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdShrGassanHeadId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), "ByOdShrGassanHeadId");

            List<OdShrGassanHead> ret = new List<OdShrGassanHead>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShrGassanHead> mapping = new MappingBase<OdShrGassanHead>();
            Params.Add(new DBParameter("OD_SHR_GASSAN_HEAD_ID", OdShrGassanHeadId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
        public static OdShrGassanHead GetRecordByOdShrId(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdShrId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), "ByOdShrId");

            List<OdShrGassanHead> ret = new List<OdShrGassanHead>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShrGassanHead> mapping = new MappingBase<OdShrGassanHead>();
            Params.Add(new DBParameter("OD_SHR_ID", OdShrId));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_SHR_GASSAN_HEAD_ID", OdShrGassanHeadID));
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbtID));
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", MsThiIraiShousaiID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("HACHU_NO", HachuNo));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            Params.Add(new DBParameter("OD_SHR_ID", OdShrID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanHead), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbtID));
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", MsThiIraiShousaiID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("HACHU_NO", HachuNo));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            Params.Add(new DBParameter("OD_SHR_ID", OdShrID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("OD_SHR_GASSAN_HEAD_ID", OdShrGassanHeadID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
