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
    [TableAttribute("KS_KENSA")]
    public class KsKensa
    {
        //KS_KENSA_ID                    NVARCHAR2(40) NOT NULL,
        //MS_KENSA_ID                    NVARCHAR2(40),

        //MS_VESSEL_ID                   NUMBER(4,0),

        //SHINSA_DATE                    DATE

        //ALARM180_DATE                  DATE,
        //ALARM90_DATE                   DATE,
        
        //ALARM_DELETE_DATE              DATE,
        //ALARM_DELETE_USER              NVARCHAR2(40),

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
        /// 検査ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("KS_KENSA_ID")]
        public string KsKensaID { get; set; }

        /// <summary>
        /// 検査マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KENSA_ID")]
        public string MsKensaID { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }                

        /// <summary>
        /// 審査日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHINSA_DATE")]
        public DateTime ShinsaDate { get; set; }

        /// <summary>
        /// 180日前アラーム
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALARM180_DATE")]
        public DateTime Alarm180Date { get; set; }

        /// <summary>
        /// 90日前アラーム
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALARM90_DATE")]
        public DateTime Alarm90Date { get; set; }
                
        /// <summary>
        /// アラーム削除日
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALARM_DELETE_DATE")]
        public DateTime AlarmDaleteDate { get; set; }

        /// <summary>
        /// アラーム削除ユーザー
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALARM_DELETE_USER")]
        public string AlarmDaleteUser { get; set; }

        /// <summary>
        /// 実績登録者
        /// </summary>
        [DataMember]
        [ColumnAttribute("JISEKITOUROKU_USER")]
        public string JisekiTourokuUser { get; set; }

        /// <summary>
        /// 実績登録日
        /// </summary>
        [DataMember]
        [ColumnAttribute("JISEKITOUROKU_DATE")]
        public DateTime JisekiTourokuDate { get; set; }

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

        //---------------------------------------------------
        /// <summary>
        /// 検査名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KENSA_NAME")]
        public string KensaName { get; set; }

        #endregion

        /// <summary>
        /// 全レコード取得
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static List<KsKensa> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensa), MethodBase.GetCurrentMethod());
            List<KsKensa> ret = new List<KsKensa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKensa> mapping = new MappingBase<KsKensa>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        /// <summary>
        /// 船ID指定の全データの取得
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static List<KsKensa> GetRecordsBy船ID(MsUser loginUser, int msvessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensa), MethodBase.GetCurrentMethod());
            List<KsKensa> ret = new List<KsKensa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKensa> mapping = new MappingBase<KsKensa>();

            Params.Add(new DBParameter("MS_VESSEL_ID", msvessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        /// <summary>
        /// 実績登録のない船ID指定の全予定データの取得
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static List<KsKensa> GetRecordsBy船ID予定データ(MsUser loginUser, int msvessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensa), MethodBase.GetCurrentMethod());
            List<KsKensa> ret = new List<KsKensa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKensa> mapping = new MappingBase<KsKensa>();

            Params.Add(new DBParameter("MS_VESSEL_ID", msvessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //検査マスタ
        public static List<KsKensa> GetRecordsByMsKensaID(MsUser loginUser, string ms_kensa_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensa), MethodBase.GetCurrentMethod());
            List<KsKensa> ret = new List<KsKensa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKensa> mapping = new MappingBase<KsKensa>();

            Params.Add(new DBParameter("MS_KENSA_ID", ms_kensa_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static KsKensa GetRecord(MsUser loginUser, string kensa_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensa), MethodBase.GetCurrentMethod());
            List<KsKensa> ret = new List<KsKensa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKensa> mapping = new MappingBase<KsKensa>();

            Params.Add(new DBParameter("KS_KENSA_ID", kensa_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        

        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensa), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("KS_KENSA_ID", this.KsKensaID));
            Params.Add(new DBParameter("MS_KENSA_ID", this.MsKensaID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("SHINSA_DATE", this.ShinsaDate));
            Params.Add(new DBParameter("ALARM180_DATE", this.Alarm180Date));
            Params.Add(new DBParameter("ALARM90_DATE", this.Alarm90Date));
            Params.Add(new DBParameter("ALARM_DELETE_DATE", this.AlarmDaleteDate));
            Params.Add(new DBParameter("ALARM_DELETE_USER", this.AlarmDaleteUser));
            Params.Add(new DBParameter("JISEKITOUROKU_USER", this.JisekiTourokuUser));
            Params.Add(new DBParameter("JISEKITOUROKU_DATE", this.JisekiTourokuDate));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensa), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;
            Params.Add(new DBParameter("KS_KENSA_ID", this.KsKensaID));
            Params.Add(new DBParameter("MS_KENSA_ID", this.MsKensaID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("SHINSA_DATE", this.ShinsaDate));
            Params.Add(new DBParameter("ALARM180_DATE", this.Alarm180Date));
            Params.Add(new DBParameter("ALARM90_DATE", this.Alarm90Date));
            Params.Add(new DBParameter("ALARM_DELETE_DATE", this.AlarmDaleteDate));
            Params.Add(new DBParameter("ALARM_DELETE_USER", this.AlarmDaleteUser));
            Params.Add(new DBParameter("JISEKITOUROKU_USER", this.JisekiTourokuUser));
            Params.Add(new DBParameter("JISEKITOUROKU_DATE", this.JisekiTourokuDate));
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
