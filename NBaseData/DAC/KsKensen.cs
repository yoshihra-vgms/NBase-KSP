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
    [TableAttribute("KS_KENSEN")]
    public class KsKensen
    {
        //KS_KENSEN_ID                   NVARCHAR2(40) NOT NULL,
        //MS_KENSEN_ID                   NVARCHAR2(40),
        //MS_VESSEL_ID                   NUMBER(4,0),
        
        //SHINSA_DATE                    DATE,
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
        /// 検船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("KS_KENSEN_ID")]
        public string KsKensenID { get; set; }

        /// <summary>
        /// 検船マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KENSEN_ID")]
        public string MsKensenID { get; set; }


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

        //----------------------------------------
        /// <summary>
        /// 検船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KENSEN_NAME")]
        public string MsKensenName { get; set; }

        #endregion

        
        public static List<KsKensen> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensen), MethodBase.GetCurrentMethod());
            List<KsKensen> ret = new List<KsKensen>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKensen> mapping = new MappingBase<KsKensen>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //船IDを指定して取得する
        public static List<KsKensen> GetRecordBy船ID(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensen), MethodBase.GetCurrentMethod());
            List<KsKensen> ret = new List<KsKensen>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKensen> mapping = new MappingBase<KsKensen>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //船IDを指定して予定データを取得する
        public static List<KsKensen> GetRecordBy船ID予定データ(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensen), MethodBase.GetCurrentMethod());
            List<KsKensen> ret = new List<KsKensen>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKensen> mapping = new MappingBase<KsKensen>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }



        public static List<KsKensen> GetRecordsByMsKensenID(MsUser loginUser, string ms_kensen_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensen), MethodBase.GetCurrentMethod());
            List<KsKensen> ret = new List<KsKensen>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKensen> mapping = new MappingBase<KsKensen>();

            Params.Add(new DBParameter("MS_KENSEN_ID", ms_kensen_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static KsKensen GetRecord(MsUser loginUser, string ks_kensen_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensen), MethodBase.GetCurrentMethod());
            List<KsKensen> ret = new List<KsKensen>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsKensen> mapping = new MappingBase<KsKensen>();

            Params.Add(new DBParameter("KS_KENSEN_ID", ks_kensen_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }



        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensen), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("KS_KENSEN_ID", this.KsKensenID));
            Params.Add(new DBParameter("MS_KENSEN_ID", this.MsKensenID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));

            Params.Add(new DBParameter("SHINSA_DATE", this.ShinsaDate));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsKensen), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;
            Params.Add(new DBParameter("KS_KENSEN_ID", this.KsKensenID));
            Params.Add(new DBParameter("MS_KENSEN_ID", this.MsKensenID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));

            Params.Add(new DBParameter("SHINSA_DATE", this.ShinsaDate));
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
