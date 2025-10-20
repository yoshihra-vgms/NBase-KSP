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
    [TableAttribute("KS_SHOUSHO_SHINSA_LINK")]
    public class KsShoushoShinsaLink
    {
        //KS_SHOUSHO_SHINSA_LINK_ID      NUMBER(9,0) NOT NULL,
        //MS_SHINSA_ID                   NVARCHAR2(40),
        //KS_SHOUSHO_ID                  NVARCHAR2(40),
        //SEND_FLAG                      NUMBER(1,0) NOT NULL,
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //USER_KEY                       VARCHAR2(40) NOT NULL,
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),

        #region データメンバ
        /// <summary>
        /// 証書審査リンクID
        /// </summary>
        [DataMember]
        [ColumnAttribute("KS_SHOUSHO_SHINSA_LINK_ID")]
        public string KsShoushoShinsaLinkID { get; set; }

        /// <summary>
        /// 証書ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("KS_SHOUSHO_ID")]
        public string KsShoushoID { get; set; }

        /// <summary>
        /// 審査マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SHINSA_ID")]
        public string MsShinsaID { get; set; }



        //-----------------------------------------------------------
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


        public static List<KsShoushoShinsaLink> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShoushoShinsaLink), MethodBase.GetCurrentMethod());
            List<KsShoushoShinsaLink> ret = new List<KsShoushoShinsaLink>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsShoushoShinsaLink> mapping = new MappingBase<KsShoushoShinsaLink>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<KsShoushoShinsaLink> GetRecordsByMsShinsaID(MsUser loginUser, string ms_shisa_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShoushoShinsaLink), MethodBase.GetCurrentMethod());
            List<KsShoushoShinsaLink> ret = new List<KsShoushoShinsaLink>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsShoushoShinsaLink> mapping = new MappingBase<KsShoushoShinsaLink>();
            
            //条件挿入
            Params.Add(new DBParameter("MS_SHINSA_ID", ms_shisa_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static KsShoushoShinsaLink GetRecord(MsUser loginUser, string ks_shoushoshinsa_link_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShoushoShinsaLink), MethodBase.GetCurrentMethod());
            List<KsShoushoShinsaLink> ret = new List<KsShoushoShinsaLink>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsShoushoShinsaLink> mapping = new MappingBase<KsShoushoShinsaLink>();
            //条件挿入
            Params.Add(new DBParameter("KS_SHOUSHO_SHINSA_LINK_ID", ks_shoushoshinsa_link_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShoushoShinsaLink), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("KS_SHOUSHO_SHINSA_LINK_ID", this.KsShoushoShinsaLinkID));
            Params.Add(new DBParameter("KS_SHOUSHO_ID", this.KsShoushoID));
            Params.Add(new DBParameter("MS_SHINSA_ID", this.MsShinsaID));

            Params.Add(new DBParameter("SEND_FLAG", this.SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", this.VesselID));
            Params.Add(new DBParameter("DATA_NO", this.DataNo));
            Params.Add(new DBParameter("USER_KEY", this.UserKey));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));

            DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            return true;
        }


        public bool UpdateRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShoushoShinsaLink), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;
            Params.Add(new DBParameter("KS_SHOUSHO_SHINSA_LINK_ID", this.KsShoushoShinsaLinkID));
            Params.Add(new DBParameter("KS_SHOUSHO_ID", this.KsShoushoID));
            Params.Add(new DBParameter("MS_SHINSA_ID", this.MsShinsaID));

            Params.Add(new DBParameter("SEND_FLAG", this.SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", this.VesselID));
            Params.Add(new DBParameter("DATA_NO", this.DataNo));
            Params.Add(new DBParameter("USER_KEY", this.UserKey));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        public static bool DeleteRecordBy審査マスタID証書ID(MsUser loginUser, string ms_shinsa_id, string ks_shousho_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShoushoShinsaLink), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;

            Params.Add(new DBParameter("KS_SHOUSHO_ID", ks_shousho_id));
            Params.Add(new DBParameter("MS_SHINSA_ID", ms_shinsa_id));


            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
