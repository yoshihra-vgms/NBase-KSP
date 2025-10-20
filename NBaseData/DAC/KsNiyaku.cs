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
    [TableAttribute("KS_NIYAKU")]
    public class KsNiyaku
    {
        //KS_NIYAKU_ID                   NVARCHAR2(40) NOT NULL,
        //MS_NIYAKU_ID                   NVARCHAR2(40),
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
        /// 荷役安全設備ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("KS_NIYAKU_ID")]
        public string KsNiyakuID { get; set; }

        /// <summary>
        /// 荷役安全設備マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_NIYAKU_ID")]
        public string MsNiyakuID { get; set; }

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
        public string JisekitourokuUser { get; set; }


        /// <summary>
        /// 実績登録日
        /// </summary>
        [DataMember]
        [ColumnAttribute("JISEKITOUROKU_DATE")]
        public DateTime JisekitourokuDate { get; set; }


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

        //---------------------------------------
        /// <summary>
        /// 荷役安全設備名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_NIYAKUSETUBI_NAME")]
        public string MsNiyakusetsubiName { get; set; }
        #endregion

        /// <summary>
        /// 検査リンクされているデータを取得
        /// </summary>
        public List<MsKensa> KensaLinkDataList = new List<MsKensa>();


        public List<KsNiyaku> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsNiyaku), MethodBase.GetCurrentMethod());
            List<KsNiyaku> ret = new List<KsNiyaku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsNiyaku> mapping = new MappingBase<KsNiyaku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<KsNiyaku> GetRecordsBy船ID(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsNiyaku), MethodBase.GetCurrentMethod());
            List<KsNiyaku> ret = new List<KsNiyaku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsNiyaku> mapping = new MappingBase<KsNiyaku>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<KsNiyaku> GetRecordsBy船ID_荷役ID(MsUser loginUser, int ms_vessel_id, string ms_niyaku_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsNiyaku), MethodBase.GetCurrentMethod());
            List<KsNiyaku> ret = new List<KsNiyaku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsNiyaku> mapping = new MappingBase<KsNiyaku>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));
            Params.Add(new DBParameter("MS_NIYAKU_ID", ms_niyaku_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<KsNiyaku> GetRecordsBy船ID_予定データ(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsNiyaku), MethodBase.GetCurrentMethod());
            List<KsNiyaku> ret = new List<KsNiyaku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsNiyaku> mapping = new MappingBase<KsNiyaku>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<KsNiyaku> GetRecordsBy船ID_予定データ_荷役ID(MsUser loginUser, int ms_vessel_id, string ms_niyaku_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsNiyaku), MethodBase.GetCurrentMethod());
            List<KsNiyaku> ret = new List<KsNiyaku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsNiyaku> mapping = new MappingBase<KsNiyaku>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));
            Params.Add(new DBParameter("MS_NIYAKU_ID", ms_niyaku_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //検査リンクしてる荷役
        //船と検査をしていして関連してる荷役の予定を全部取得
        public static List<KsNiyaku> GetRecordsFor船_指定検査_予定(MsUser loginUser, string ms_kensa_id, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsNiyaku), MethodBase.GetCurrentMethod());
            List<KsNiyaku> ret = new List<KsNiyaku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsNiyaku> mapping = new MappingBase<KsNiyaku>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));
            Params.Add(new DBParameter("MS_KENSA_ID", ms_kensa_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //荷役安全設備マスタ
        public static List<KsNiyaku> GetRecordsByMsNiyakuID(MsUser loginUser, string ms_niyaku_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsNiyaku), MethodBase.GetCurrentMethod());
            List<KsNiyaku> ret = new List<KsNiyaku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsNiyaku> mapping = new MappingBase<KsNiyaku>();

            Params.Add(new DBParameter("MS_NIYAKU_ID", ms_niyaku_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static KsNiyaku GetRecord(MsUser loginUser, string ks_niyaku_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsNiyaku), MethodBase.GetCurrentMethod());
            List<KsNiyaku> ret = new List<KsNiyaku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsNiyaku> mapping = new MappingBase<KsNiyaku>();

            Params.Add(new DBParameter("KS_NIYAKU_ID", ks_niyaku_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsNiyaku), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            //KS_NIYAKU_ID                   NVARCHAR2(40) NOT NULL,
            //MS_NIYAKU_ID                   NVARCHAR2(40),
            //MS_VESSEL_ID                   NUMBER(4,0),

            //TENKEN_DATE                    DATE,
            //ALARM_DATE                     DATE,

            //JISEKITOUROKU_USER             NVARCHAR2(40),
            //JISEKITOUROKU_DATE             DATE,
            //BIKOU                          NVARCHAR2(500),
            Params.Add(new DBParameter("KS_NIYAKU_ID", this.KsNiyakuID));
            Params.Add(new DBParameter("MS_NIYAKU_ID", this.MsNiyakuID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));

            Params.Add(new DBParameter("TENKEN_DATE", this.TenkenDate));
            Params.Add(new DBParameter("ALARM_DATE", this.AlarmDate));

            Params.Add(new DBParameter("JISEKITOUROKU_USER", this.JisekitourokuUser));
            Params.Add(new DBParameter("JISEKITOUROKU_DATE", this.JisekitourokuDate));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsNiyaku), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;
            Params.Add(new DBParameter("KS_NIYAKU_ID", this.KsNiyakuID));
            Params.Add(new DBParameter("MS_NIYAKU_ID", this.MsNiyakuID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));

            Params.Add(new DBParameter("TENKEN_DATE", this.TenkenDate));
            Params.Add(new DBParameter("ALARM_DATE", this.AlarmDate));

            Params.Add(new DBParameter("JISEKITOUROKU_USER", this.JisekitourokuUser));
            Params.Add(new DBParameter("JISEKITOUROKU_DATE", this.JisekitourokuDate));
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
