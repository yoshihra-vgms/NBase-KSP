using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_KENSHIN")]
    public class SiKenshin : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 健康診断ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_KENSHIN_ID", true)]
        public string SiKenshinID { get; set; }

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }


        /// <summary>
        /// 保険番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("HOKEN_NO")]
        public string HokenNo { get; set; }

        /// <summary>
        /// 職名ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }


        /// <summary>
        /// 種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIND")]
        public int Kind { get; set; }     
        
        /// <summary>
        /// 受診日
        /// </summary>
        [DataMember]
        [ColumnAttribute("CONSULTATION_DATE")]
        public DateTime ConsultationDate { get; set; }

        /// <summary>
        /// 有効期限
        /// </summary>
        [DataMember]
        [ColumnAttribute("EXPIRATION_DATE")]
        public DateTime ExpirationDate  { get; set; }

        /// <summary>
        /// 結果
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT")]
        public int Result { get; set; }

        /// <summary>
        /// 結果詳細
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULT_DETAIL")]
        public string ResultDatail { get; set; }


        
        
        
        #region 共通項目

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
        
        #endregion

        #endregion


        /// <summary>
        /// 添付ファイル
        /// </summary>
        [DataMember]
        public List<SiKenshinAttachFile> AttachFiles { get; set; }

        [DataMember]
        public List<PtAlarmInfo> AlarmInfoList { get; set; }



        public static string[] KIND = new string[] { "生活習慣病検診", "生活習慣病再検査", "定期健診" };
        public static string[] RESULT = new string[] { "異常なし", "経過観察", "要精密検査", "要治療", "治療中" };



        public SiKenshin()
        {
            this.MsSeninID = Int32.MinValue;
            this.MsSiShokumeiID = Int32.MinValue;
            this.AttachFiles = new List<SiKenshinAttachFile>();

            this.HokenNo = "";
        }
 

        public static List<SiKenshin> GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "ByMsSeninID");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");


            List<SiKenshin> ret = new List<SiKenshin>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKenshin> mapping = new MappingBase<SiKenshin>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            foreach (SiKenshin m in ret)
            {
                m.AlarmInfoList = PtAlarmInfo.GetRecordsBySanshoumotoId(loginUser, m.SiKenshinID);

                if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
                {
                    m.AttachFiles = SiKenshinAttachFile.GetRecordByKoushuID(loginUser, m.SiKenshinID);
                }
            }

            return ret;
        }


        public static List<SiKenshin> SearchRecords(MsUser loginUser, DateTime fromDate, DateTime toDate, int msSiShukumeiId, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "GetRecords");

            ParameterConnection Params = new ParameterConnection();

            if (fromDate != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "ByFromDate");
                Params.Add(new DBParameter("FROM_DATE", fromDate));
            }

            if (toDate != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "ByToDate");
                Params.Add(new DBParameter("TO_DATE", toDate));
            } 
            
            if (msSiShukumeiId > 0)
            {
                SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "ByMsSiShokumeiID"));
                Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", msSiShukumeiId));
            }
            else
            {
                SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            }

            if (msSeninId > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "ByMsSeninID");
                Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            }

            List<SiKenshin> ret = new List<SiKenshin>();
            MappingBase<SiKenshin> mapping = new MappingBase<SiKenshin>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


            return ret;
        }




        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }




        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_KENSHIN_ID", SiKenshinID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("HOKEN_NO", HokenNo));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("CONSULTATION_DATE", ConsultationDate));
            Params.Add(new DBParameter("EXPIRATION_DATE", ExpirationDate));
            Params.Add(new DBParameter("RESULT", Result));
            Params.Add(new DBParameter("RESULT_DETAIL", ResultDatail));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;

            foreach (SiKenshinAttachFile a in AttachFiles)
            {
                a.SiKenshinID = SiKenshinID;
                a.RenewDate = RenewDate;
                a.RenewUserID = RenewUserID;
                bool ret = a.InsertRecord(dbConnect, loginUser);
                if (ret == false)
                    return false;
            }
           
            return true;
        }

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("HOKEN_NO", HokenNo));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("CONSULTATION_DATE", ConsultationDate));
            Params.Add(new DBParameter("EXPIRATION_DATE", ExpirationDate));
            Params.Add(new DBParameter("RESULT", Result));
            Params.Add(new DBParameter("RESULT_DETAIL", ResultDatail));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_KENSHIN_ID", SiKenshinID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;

            foreach (SiKenshinAttachFile a in AttachFiles)
            {
                a.RenewDate = RenewDate;
                a.RenewUserID = RenewUserID;

                bool ret = true;
                if (a.SiKenshinID == SiKenshinID)
                {
                    ret = a.UpdateRecord(dbConnect, loginUser);
                }
                else
                {
                    a.SiKenshinID = SiKenshinID;
                    ret = a.InsertRecord(dbConnect, loginUser);
                }
                if (ret == false)
                    return false;
            }

            return true;
        }

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", SiKenshinID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiKenshinID == null;
        }
    }
}
