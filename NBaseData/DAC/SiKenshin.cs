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
        /// 検査ID（MS_SI_OPTIONS)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_OPTIONS_ID")]
        public string MsSiOptionsID { get; set; }

        /// <summary>
        /// 検査名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KENSA_NAME")]
        public string KensaName { get; set; }

        /// <summary>
        /// 病院名
        /// </summary>
        [DataMember]
        [ColumnAttribute("HOSPITAL")]
        public string Hospital { get; set; }

        /// <summary>
        /// 検査日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KENSA_DAY")]
        public DateTime KensaDay { get; set; }

        /// <summary>
        /// 有効日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("EXPIRY_DATE")]
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// 診断名
        /// </summary>
        [DataMember]
        [ColumnAttribute("DIAGNOSIS")]
        public string Diagnosis { get; set; }

        /// <summary>
        /// 判定
        /// </summary>
        [DataMember]
        [ColumnAttribute("RESULTS")]
        public string Results { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("REMARKS")]
        public string Remarks { get; set; }


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



        /// <summary>
        /// 添付ファイル
        /// </summary>
        [DataMember]
        public List<SiKenshinAttachFile> AttachFiles { get; set; }


        [DataMember]
        public List<PtAlarmInfo> AlarmInfoList { get; set; }


        #endregion

        public bool EditFlag = false;


        public SiKenshin()
        {
            this.MsSeninID = Int32.MinValue;
            this.AttachFiles = new List<SiKenshinAttachFile>();
        }




        public static List<SiKenshin> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "By有効データ");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            List<SiKenshin> ret = new List<SiKenshin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKenshin> mapping = new MappingBase<SiKenshin>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static SiKenshin GetRecord(MsUser loginUser, string siKenshinID)
        {
            return GetRecord(null, loginUser, siKenshinID);
        }
        public static SiKenshin GetRecord(ORMapping.DBConnect dbConnect, MsUser loginUser, string siKenshinID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "By有効データ");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "BySiKenshinID");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            List<SiKenshin> ret = new List<SiKenshin>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_KENSHIN_ID", siKenshinID));
            MappingBase<SiKenshin> mapping = new MappingBase<SiKenshin>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;

            return ret[0];
        }

        public static List<SiKenshin> GetRecordsByMsSeninID( MsUser loginUser, int msSeninId)
        {
            return GetRecordsByMsSeninID(null, loginUser, msSeninId);
        }
        
        public static List<SiKenshin> GetRecordsByMsSeninID( ORMapping.DBConnect dbConnect, MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "By有効データ");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshin), "ByMsSeninID");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            List<SiKenshin> ret = new List<SiKenshin>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKenshin> mapping = new MappingBase<SiKenshin>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);


            foreach (SiKenshin k in ret)
            {
                k.AlarmInfoList = PtAlarmInfo.GetRecordsBySanshoumotoId(dbConnect, loginUser, k.SiKenshinID);

                if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
                {
                    k.AttachFiles = SiKenshinAttachFile.GetRecordByKensinID(dbConnect, loginUser, k.SiKenshinID);
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

            Params.Add(new DBParameter("MS_SI_OPTIONS_ID", MsSiOptionsID));           
            Params.Add(new DBParameter("KENSA_NAME", KensaName));
            Params.Add(new DBParameter("HOSPITAL", Hospital));
            Params.Add(new DBParameter("KENSA_DAY", KensaDay));
            Params.Add(new DBParameter("EXPIRY_DATE", ExpiryDate));
            Params.Add(new DBParameter("DIAGNOSIS", Diagnosis));
            Params.Add(new DBParameter("RESULTS", Results));
            Params.Add(new DBParameter("REMARKS", Remarks));

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
            return _UpdateRecord(dbConnect, loginUser, true, false);
        }

        public bool UpdateRecordWithoutAttachFile(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            return _UpdateRecord(dbConnect, loginUser, false, true);
        }

        public bool _UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser, bool isUpdateAttach, bool is同期)
        {
            if (is同期 == false)
            {
                UserKey = null;
            }
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), "UpdateRecord");

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));

            Params.Add(new DBParameter("MS_SI_OPTIONS_ID", MsSiOptionsID));
            Params.Add(new DBParameter("KENSA_NAME", KensaName));
            Params.Add(new DBParameter("HOSPITAL", Hospital));
            Params.Add(new DBParameter("KENSA_DAY", KensaDay));
            Params.Add(new DBParameter("EXPIRY_DATE", ExpiryDate));
            Params.Add(new DBParameter("DIAGNOSIS", Diagnosis));
            Params.Add(new DBParameter("RESULTS", Results));
            Params.Add(new DBParameter("REMARKS", Remarks));

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

            if (isUpdateAttach)
            {
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

        public bool Edited(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            SiKenshin org = SiKenshin.GetRecord(dbConnect, loginUser, SiKenshinID);
            if (org == null)
                return true;
            org.AttachFiles = SiKenshinAttachFile.GetRecordByKensinID(dbConnect, loginUser, org.SiKenshinID);

            //return org.Equals(this) == false;// 引数違う　下記述のEquals()が呼ばれない
            return org.Equals(dbConnect, loginUser, this) == false;  // 2016/08/30 修正
        }

        public bool Equals(ORMapping.DBConnect dbConnect, MsUser loginUser, SiKenshin dst)
        {
            bool ret = true;

            if (
                this.MsSiOptionsID != dst.MsSiOptionsID ||
                this.KensaName != dst.KensaName ||
                this.Hospital != dst.Hospital ||
                this.KensaDay != dst.KensaDay ||
                this.ExpiryDate != dst.ExpiryDate ||
                this.Remarks != dst.Remarks ||
                this.DeleteFlag != dst.DeleteFlag
                )
            {
                ret = false;
            }
            else if (this.AttachFiles.Count != dst.AttachFiles.Count)
            {
                ret = false;
            }
            else
            {
                foreach (SiKenshinAttachFile attach in dst.AttachFiles)
                {
                    if (attach.Edited(dbConnect, loginUser))
                    {
                        ret = false;
                        break;
                    }
                }
            }

            return ret;
        }
    }
}
