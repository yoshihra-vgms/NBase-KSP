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
    [TableAttribute("KS_KYUMEISETUBI")]
    public class KsKyumeisetsubi
    {
        //KS_KYUMEISETUBI_ID             NVARCHAR2(40) NOT NULL,
        //MS_KYUMEISETUBI_ID             NVARCHAR2(40),
        //MS_VESSEL_ID                   NUMBER(4,0),
        
        //TENKEN_DATE                    DATE,
        //ALARM_DATE                     DATE,
       
        //JISEKITOUROKU_USER             NVARCHAR2(40),
        //JISEKITOUROKU_DATE             DATE,
        //BIKOU                          NVARCHAR2(500),
        
        //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
        //SEND_FLAG                      NUMBER(1,0) NOT NULL,
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //USER_KEY                       VARCHAR2(40),
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),
        #region データメンバ

        /// <summary>
        /// 救命設備ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("KS_KYUMEISETUBI_ID")]
        public string KsKyumeisetsubiID { get; set; }

        /// <summary>
        /// 救命設備マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KYUMEISETUBI_ID")]
        public string MsKyumeisetsubiID { get; set; }

        /// <summary>
        /// 船ID    
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 点検日
        /// </summary>
        [DataMember]
        [ColumnAttribute("TENKEN_DATE")]
        public DateTime TenkenDate { get; set; }

        /// <summary>
        /// アラーム日
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALARM_DATE")]
        public DateTime AlarmDate { get; set; }

        /// <summary>
        /// 実績登録者
        /// </summary>
        [DataMember]
        [ColumnAttribute("JISEKITOUROKU_USER")]
        public string JisekitouokuUser { get; set; }

        /// <summary>
        /// 実績登録日
        /// </summary>
        [DataMember]
        [ColumnAttribute("JISEKITOUROKU_DATE")]
        public DateTime JisekitouokuDate { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; } 
        //------------------------------------------------------------------

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

        //----------------------------------------------------
        /// <summary>
        /// 救命設備名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KYUMEISETUBI_NAME")]
        public string MsKyumeisetsubiName { get; set; }
        #endregion


        public static List<KsKyumeisetsubi> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKyumeisetsubi), MethodBase.GetCurrentMethod());
            List<KsKyumeisetsubi> ret = new List<KsKyumeisetsubi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKyumeisetsubi> mapping = new MappingBase<KsKyumeisetsubi>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<KsKyumeisetsubi> GetRecordsBy船ID(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKyumeisetsubi), MethodBase.GetCurrentMethod());
            List<KsKyumeisetsubi> ret = new List<KsKyumeisetsubi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKyumeisetsubi> mapping = new MappingBase<KsKyumeisetsubi>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
                        
            return ret;
        }

        public static List<KsKyumeisetsubi> GetRecordsBy船ID_救命設備ID(MsUser loginUser, int ms_vessel_id, string ms_kyumei_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKyumeisetsubi), MethodBase.GetCurrentMethod());
            List<KsKyumeisetsubi> ret = new List<KsKyumeisetsubi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKyumeisetsubi> mapping = new MappingBase<KsKyumeisetsubi>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));
            Params.Add(new DBParameter("MS_KYUMEISETUBI_ID", ms_kyumei_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<KsKyumeisetsubi> GetRecordBy船ID予定データ(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKyumeisetsubi), MethodBase.GetCurrentMethod());
            List<KsKyumeisetsubi> ret = new List<KsKyumeisetsubi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKyumeisetsubi> mapping = new MappingBase<KsKyumeisetsubi>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<KsKyumeisetsubi> GetRecordBy船ID予定データ_救命設備ID(MsUser loginUser, int ms_vessel_id, string ms_kyumei_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKyumeisetsubi), MethodBase.GetCurrentMethod());
            List<KsKyumeisetsubi> ret = new List<KsKyumeisetsubi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKyumeisetsubi> mapping = new MappingBase<KsKyumeisetsubi>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));
            Params.Add(new DBParameter("MS_KYUMEISETUBI_ID", ms_kyumei_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //救命設備マスタ
        public static List<KsKyumeisetsubi> GetRecordsByMsKyumeisetsubiID(MsUser loginUser, string ms_kyu_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKyumeisetsubi), MethodBase.GetCurrentMethod());
            List<KsKyumeisetsubi> ret = new List<KsKyumeisetsubi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKyumeisetsubi> mapping = new MappingBase<KsKyumeisetsubi>();

            Params.Add(new DBParameter("MS_KYUMEISETUBI_ID", ms_kyu_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static KsKyumeisetsubi GetRecord(MsUser loginUser, string ks_kyumeisetsubi_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKyumeisetsubi), MethodBase.GetCurrentMethod());
            List<KsKyumeisetsubi> ret = new List<KsKyumeisetsubi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKyumeisetsubi> mapping = new MappingBase<KsKyumeisetsubi>();

            Params.Add(new DBParameter("KS_KYUMEISETUBI_ID", ks_kyumeisetsubi_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKyumeisetsubi), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("KS_KYUMEISETUBI_ID", this.KsKyumeisetsubiID));
            Params.Add(new DBParameter("MS_KYUMEISETUBI_ID", this.MsKyumeisetsubiID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));

            Params.Add(new DBParameter("TENKEN_DATE", this.TenkenDate));
            Params.Add(new DBParameter("ALARM_DATE", this.AlarmDate));

            Params.Add(new DBParameter("JISEKITOUROKU_USER", this.JisekitouokuUser));
            Params.Add(new DBParameter("JISEKITOUROKU_DATE", this.JisekitouokuDate));
            Params.Add(new DBParameter("BIKOU", this.Bikou));

            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKyumeisetsubi), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;
            Params.Add(new DBParameter("KS_KYUMEISETUBI_ID", this.KsKyumeisetsubiID));
            Params.Add(new DBParameter("MS_KYUMEISETUBI_ID", this.MsKyumeisetsubiID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));

            Params.Add(new DBParameter("TENKEN_DATE", this.TenkenDate));
            Params.Add(new DBParameter("ALARM_DATE", this.AlarmDate));

            Params.Add(new DBParameter("JISEKITOUROKU_USER", this.JisekitouokuUser));
            Params.Add(new DBParameter("JISEKITOUROKU_DATE", this.JisekitouokuDate));
            Params.Add(new DBParameter("BIKOU", this.Bikou));

            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
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
    }
}
