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
    [Serializable]
    [DataContract()]
    [TableAttribute("SI_ALLOWANCE_DETAIL")]
    public class SiAllowanceDetail : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 手当詳細ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_ALLOWANCE_DETAIL_ID", true)]
        public string SiAllowanceDetailID { get; set; }

        /// <summary>
        /// 手当ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_ALLOWANCE_ID")]
        public string SiAllowanceID { get; set; }


        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 職名 (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }


        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALLOWANCE")]
        public int Allowance { get; set; }

        /// <summary>
        /// 対象
        /// </summary>
        [DataMember]
        [ColumnAttribute("IS_TARGET")]
        public int IsTarget { get; set; }
        
        /// <summary>
        /// SI_CARD_ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_CARD_ID")]
        public string SiCardID { get; set; }


        /// <summary>
        /// MS_VESSEL_ID(Left Join)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }



        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

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
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        /// <summary>
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

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

        public bool IsNew()
        {
            return SiAllowanceDetailID == null;
        }

        public SiAllowanceDetail()
        {
            SiAllowanceDetailID = null;
        }
        public static List<SiAllowanceDetail> GetRecords(NBaseData.DAC.MsUser loginUser, string siAllowanceID)
        {
            return GetRecords(null, loginUser, siAllowanceID);
        }

        public static List<SiAllowanceDetail> GetRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string siAllowanceID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAllowanceDetail), MethodBase.GetCurrentMethod());
            List<SiAllowanceDetail> ret = new List<SiAllowanceDetail>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_ALLOWANCE_ID", siAllowanceID));
            MappingBase<SiAllowanceDetail> mapping = new MappingBase<SiAllowanceDetail>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAllowanceDetail), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_ALLOWANCE_DETAIL_ID", SiAllowanceDetailID));
            Params.Add(new DBParameter("SI_ALLOWANCE_ID", SiAllowanceID));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("ALLOWANCE", Allowance));
            Params.Add(new DBParameter("IS_TARGET", IsTarget));
            Params.Add(new DBParameter("SI_CARD_ID", SiCardID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiAllowanceDetail), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_ALLOWANCE_ID", SiAllowanceID));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("ALLOWANCE", Allowance));
            Params.Add(new DBParameter("IS_TARGET", IsTarget));
            Params.Add(new DBParameter("SI_CARD_ID", SiCardID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SI_ALLOWANCE_DETAIL_ID", SiAllowanceDetailID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", SiAllowanceDetailID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
