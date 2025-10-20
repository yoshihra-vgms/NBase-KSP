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
    [TableAttribute("SI_SHOBATSU")]   // reward and punishment
    public class SiShobatsu : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 賞罰ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SHOBATSU_ID", true)]
        public string SiShobatsuID { get; set; }

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOBATSU_DATE")]
        public DateTime ShobatsuDate { get; set; }

        /// <summary>
        /// 賞罰名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOBATSU_NAME")]
        public string ShobatsuName { get; set; }
               

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("REMARKS")]
        public string Remarks { get; set; }
   
        
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
        public List<SiShobatsuAttachFile> AttachFiles { get; set; }



        public SiShobatsu()
        {
            this.MsSeninID = Int32.MinValue;
            this.AttachFiles = new List<SiShobatsuAttachFile>();
        }
 

        public static List<SiShobatsu> GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiShobatsu), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiShobatsu), "ByMsSeninID");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            List<SiShobatsu> ret = new List<SiShobatsu>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiShobatsu> mapping = new MappingBase<SiShobatsu>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            foreach (SiShobatsu m in ret)
            {
                if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
                {
                    m.AttachFiles = SiShobatsuAttachFile.GetRecordByKoushuID(loginUser, m.SiShobatsuID);
                }
            }

            return ret;
        }


        //public static List<SiShobatsu> SearchRecords(MsUser loginUser, DateTime fromDate, DateTime toDate, int msSiShukumeiId, int msSeninId)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiShobatsu), "GetRecords");

        //    ParameterConnection Params = new ParameterConnection();

        //    if (fromDate != DateTime.MinValue)
        //    {
        //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiShobatsu), "ByFromDate");
        //        Params.Add(new DBParameter("FROM_DATE", fromDate));
        //    }

        //    if (toDate != DateTime.MinValue)
        //    {
        //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiShobatsu), "ByToDate");
        //        Params.Add(new DBParameter("TO_DATE", toDate));
        //    }

        //    if (msSiShukumeiId > 0)
        //    {
        //        SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(SiShobatsu), "ByMsSiShokumeiID"));
        //        Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", msSiShukumeiId));
        //    }
        //    else
        //    {
        //        SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
        //    }

        //    if (msSeninId > 0)
        //    {
        //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiShobatsu), "ByMsSeninID");
        //        Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
        //    }

        //    List<SiShobatsu> ret = new List<SiShobatsu>();
        //    MappingBase<SiShobatsu> mapping = new MappingBase<SiShobatsu>();
        //    ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


        //    return ret;
        //}




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

            Params.Add(new DBParameter("SI_SHOBATSU_ID", SiShobatsuID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));

            Params.Add(new DBParameter("SHOBATSU_DATE", ShobatsuDate));
            Params.Add(new DBParameter("SHOBATSU_NAME", ShobatsuName));
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

            foreach (SiShobatsuAttachFile a in AttachFiles)
            {
                a.SiShobatsuID = SiShobatsuID;
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

            Params.Add(new DBParameter("SHOBATSU_DATE", ShobatsuDate));
            Params.Add(new DBParameter("SHOBATSU_NAME", ShobatsuName));
            Params.Add(new DBParameter("REMARKS", Remarks));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_SHOBATSU_ID", SiShobatsuID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;

            foreach (SiShobatsuAttachFile a in AttachFiles)
            {
                a.RenewDate = RenewDate;
                a.RenewUserID = RenewUserID;

                bool ret = true;
                if (a.SiShobatsuID == SiShobatsuID)
                {
                    ret = a.UpdateRecord(dbConnect, loginUser);
                }
                else
                {
                    a.SiShobatsuID = SiShobatsuID;
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
            Params.Add(new DBParameter("PK", SiShobatsuID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiShobatsuID == null;
        }
    }
}
