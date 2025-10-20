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
    public class OdJryGaisan
    {
        #region データメンバ

        /// <summary>
        /// 受領概算ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_GAISAN_ID")]
        public string OdJryGaisanID { get; set; }

        /// <summary>
        /// 受領ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ID")]
        public string OdJryID { get; set; }

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
        /// 取消フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CANCEL_FLAG")]
        public int CancelFlag { get; set; }

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

        public OdJryGaisan()
        {
        }

        public static List<OdJryGaisan> GetRecordsByOdJryID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdJryID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryGaisan), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryGaisan), "ByOdJryID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryGaisan), "Order");

            List<OdJryGaisan> ret = new List<OdJryGaisan>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJryGaisan> mapping = new MappingBase<OdJryGaisan>();
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

           return ret;
        }

        public static OdJryGaisan GetRecordByOdJryID( NBaseData.DAC.MsUser loginUser, string OdJryID)
        {
            return GetRecordByOdJryID(null, loginUser, OdJryID);
        }
        public static OdJryGaisan GetRecordByOdJryID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdJryID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryGaisan), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryGaisan), "ByOdJryID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdJryGaisan), "Order");

            List<OdJryGaisan> ret = new List<OdJryGaisan>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdJryGaisan> mapping = new MappingBase<OdJryGaisan>();
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {

            return InsertRecord(null,loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryGaisan), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_GAISAN_ID", OdJryGaisanID));
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
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

        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdJryGaisan), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("OD_JRY_GAISAN_ID", OdJryGaisanID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
