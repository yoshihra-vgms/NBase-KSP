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
    [TableAttribute("KS_SHINSA")]
    public class KsShinsa
    {
        //KS_SHINSA_ID                   NVARCHAR2(40) NOT NULL,
        //MS_SHINSA_ID                   NVARCHAR2(40),
        //MS_VESSEL_ID                   NUMBER(4,0),
        
        //SHINSA_DATE                    DATE,
        //SHINSA_ALARM_DATE              DATE,
        //SHINSA_JISEKITOUROKU_DATE      DATE,
        //SHINSA_JISEKITOUROKU_USER      NVARCHAR2(40),
        
        //NAIBU_DATE                     DATE,
        //NAIBU_ALARM_DATE               DATE,
        //NAIBU_JISEKITOUROKU_USER       NVARCHAR2(40),
        //NAIBU_JISEKITOUROKU_DATE       DATE,
        
        //REVIEW_DATE                    DATE,
        //REVIEW_ALARM_DATE              DATE,
        //REVIEW_JISEKITOUROKU_USER      NVARCHAR2(40),
        //REVIEW_JISEKITOUROKU_DATE      DATE,
        
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
        /// 審査ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("KS_SHINSA_ID")]
        public string KsShinsaID { get; set; }


        /// <summary>
        /// 審査マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SHINSA_ID")]
        public string MsShinsaID { get; set; }

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
        /// 審査日アラーム
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHINSA_ALARM_DATE")]
        public DateTime ShinsaAlarmDate { get; set; }

        /// <summary>
        /// 審査実績登録日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHINSA_JISEKITOUROKU_DATE")]
        public DateTime ShinsaJisekiTourokuDate { get; set; }

        /// <summary>
        /// 審査実績登録者
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHINSA_JISEKITOUROKU_USER")]
        public string ShinsaJisekiTourokuUser { get; set; }

        /// <summary>
        /// 内部審査日
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAIBU_DATE")]
        public DateTime NaibuDate { get; set; }

        /// <summary>
        /// 内部審査アラーム日
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAIBU_ALARM_DATE")]
        public DateTime NaibuAlarmDate { get; set; }

        /// <summary>
        /// 内部実績登録ユーザー
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAIBU_JISEKITOUROKU_USER")]
        public string NaibuJisekitourokuUser { get; set; }

        /// <summary>
        /// 内部実績登録日
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAIBU_JISEKITOUROKU_DATE")]
        public DateTime NaibuJisekitourokuDate { get; set; }

        /// <summary>
        /// レビュー日
        /// </summary>
        [DataMember]
        [ColumnAttribute("REVIEW_DATE")]
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// レビューアラーム日
        /// </summary>
        [DataMember]
        [ColumnAttribute("REVIEW_ALARM_DATE")]
        public DateTime ReviewAlarmDate { get; set; }

        /// <summary>
        /// レビュー実績登録者
        /// </summary>
        [DataMember]
        [ColumnAttribute("REVIEW_JISEKITOUROKU_USER")]
        public string ReviewJisekiTouokuUser { get; set; }

        /// <summary>
        /// レビュー実績登録日
        /// </summary>
        [DataMember]
        [ColumnAttribute("REVIEW_JISEKITOUROKU_DATE")]
        public DateTime ReviewJisekiTouokuDate { get; set; }

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

        //--------------------------------------------------
        /// <summary>
        /// 審査マスタ名前
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SHINSA_NAME")]
        public string MsShinsaName { get; set; }
        #endregion


        public static List<KsShinsa> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShinsa), MethodBase.GetCurrentMethod());
            List<KsShinsa> ret = new List<KsShinsa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsShinsa> mapping = new MappingBase<KsShinsa>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<KsShinsa> GetRecordsBy船ID(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShinsa), MethodBase.GetCurrentMethod());
            List<KsShinsa> ret = new List<KsShinsa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsShinsa> mapping = new MappingBase<KsShinsa>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


            return ret;
        }


        //審査マスタ
        public static List<KsShinsa> GetRecordsByMsShinsaID(MsUser loginUser, string ms_shinsa_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShinsa), MethodBase.GetCurrentMethod());
            List<KsShinsa> ret = new List<KsShinsa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsShinsa> mapping = new MappingBase<KsShinsa>();

            Params.Add(new DBParameter("MS_SHINSA_ID", ms_shinsa_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


            return ret;
        }




        public static List<KsShinsa> GetRecordsBy船ID_予定データ(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShinsa), MethodBase.GetCurrentMethod());
            List<KsShinsa> ret = new List<KsShinsa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsShinsa> mapping = new MappingBase<KsShinsa>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


            return ret;
        }

        public static KsShinsa GetRecord(MsUser loginUser, string ks_shinsa_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShinsa), MethodBase.GetCurrentMethod());
            List<KsShinsa> ret = new List<KsShinsa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsShinsa> mapping = new MappingBase<KsShinsa>();

            Params.Add(new DBParameter("KS_SHINSA_ID", ks_shinsa_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public static List<KsShinsa> GetRecordsBy期間データ(MsUser loginUser, DateTime start, DateTime end)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShinsa), MethodBase.GetCurrentMethod());
            List<KsShinsa> ret = new List<KsShinsa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<KsShinsa> mapping = new MappingBase<KsShinsa>();

            Params.Add(new DBParameter("START_DATE", start));
            Params.Add(new DBParameter("END_DATE", end));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


            return ret;
        }



        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShinsa), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("KS_SHINSA_ID", this.KsShinsaID));
            Params.Add(new DBParameter("MS_SHINSA_ID", this.MsShinsaID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("SHINSA_DATE", this.ShinsaDate));
            Params.Add(new DBParameter("SHINSA_ALARM_DATE", this.ShinsaAlarmDate));
            Params.Add(new DBParameter("SHINSA_JISEKITOUROKU_DATE", this.ShinsaJisekiTourokuDate));
            Params.Add(new DBParameter("SHINSA_JISEKITOUROKU_USER", this.ShinsaJisekiTourokuUser));
            Params.Add(new DBParameter("NAIBU_DATE", this.NaibuDate));
            Params.Add(new DBParameter("NAIBU_ALARM_DATE", this.NaibuAlarmDate));
            Params.Add(new DBParameter("NAIBU_JISEKITOUROKU_USER", this.NaibuJisekitourokuUser));
            Params.Add(new DBParameter("NAIBU_JISEKITOUROKU_DATE", this.NaibuJisekitourokuDate));
            Params.Add(new DBParameter("REVIEW_DATE", this.ReviewDate));
            Params.Add(new DBParameter("REVIEW_ALARM_DATE", this.ReviewAlarmDate));
            Params.Add(new DBParameter("REVIEW_JISEKITOUROKU_USER", this.ReviewJisekiTouokuUser));
            Params.Add(new DBParameter("REVIEW_JISEKITOUROKU_DATE", this.ReviewJisekiTouokuDate));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(KsShinsa), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;
            Params.Add(new DBParameter("KS_SHINSA_ID", this.KsShinsaID));
            Params.Add(new DBParameter("MS_SHINSA_ID", this.MsShinsaID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("SHINSA_DATE", this.ShinsaDate));
            Params.Add(new DBParameter("SHINSA_ALARM_DATE", this.ShinsaAlarmDate));
            Params.Add(new DBParameter("SHINSA_JISEKITOUROKU_DATE", this.ShinsaJisekiTourokuDate));
            Params.Add(new DBParameter("SHINSA_JISEKITOUROKU_USER", this.ShinsaJisekiTourokuUser));
            Params.Add(new DBParameter("NAIBU_DATE", this.NaibuDate));
            Params.Add(new DBParameter("NAIBU_ALARM_DATE", this.NaibuAlarmDate));
            Params.Add(new DBParameter("NAIBU_JISEKITOUROKU_USER", this.NaibuJisekitourokuUser));
            Params.Add(new DBParameter("NAIBU_JISEKITOUROKU_DATE", this.NaibuJisekitourokuDate));
            Params.Add(new DBParameter("REVIEW_DATE", this.ReviewDate));
            Params.Add(new DBParameter("REVIEW_ALARM_DATE", this.ReviewAlarmDate));
            Params.Add(new DBParameter("REVIEW_JISEKITOUROKU_USER", this.ReviewJisekiTouokuUser));
            Params.Add(new DBParameter("REVIEW_JISEKITOUROKU_DATE", this.ReviewJisekiTouokuDate));
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
