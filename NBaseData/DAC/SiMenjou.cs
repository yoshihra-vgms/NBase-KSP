using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using System.Reflection;
using ORMapping;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_MENJOU")]
    public class SiMenjou : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 免状／免許ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_MENJOU_ID", true)]
        public string SiMenjouID { get; set; }

        
        
        
        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 免許／免状ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_ID")]
        public int MsSiMenjouID { get; set; }

        /// <summary>
        /// 免許／免状種別ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_KIND_ID")]
        public int MsSiMenjouKindID { get; set; }

        
        
        
        /// <summary>
        /// 番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("NO")]
        public string No { get; set; }

        /// <summary>
        /// 有効期限
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIGEN")]
        public DateTime Kigen { get; set; }

        /// <summary>
        /// 取得／受講日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHUTOKU_DATE")]
        public DateTime ShutokuDate { get; set; }

        /// <summary>
        /// 帳票表示フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CHOUHYOU_FLAG")]
        public int ChouhyouFlag { get; set; }

        /// <summary>
        /// 筆記試験フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("WRITTEN_TEST")]
        public int WrittenTest { get; set; }


        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }





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
        /// 船員名(カナ) (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_NAME_KANA")]
        public string SeninNameKana { get; set; }

        /// <summary>
        /// 職名ID (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_SHOKUMEI_ID")]
        public int SeninShokumeiID { get; set; }

        /// <summary>
        /// 免許／免状表示順 (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_SHOW_ORDER")]
        public int MsSiMenjouShowOrder { get; set; }

        /// <summary>
        /// 免許／免状種別表示順 (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_KIND_SHOW_ORDER")]
        public int MsSiMenjouKindShowOrder { get; set; }


        #endregion

        /// <summary>
        /// 添付ファイル
        /// </summary>
        [DataMember]
        public List<SiMenjouAttachFile> AttachFiles { get; set; }

        [DataMember]
        public List<PtAlarmInfo> AlarmInfoList { get; set; }




        public SiMenjou()
        {
            this.MsSeninID = Int32.MinValue;
            this.MsSiMenjouID = Int32.MinValue;
            this.MsSiMenjouKindID= Int32.MinValue;
            this.AttachFiles = new List<SiMenjouAttachFile>();
        }




        public static List<SiMenjou> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), MethodBase.GetCurrentMethod());

            List<SiMenjou> ret = new List<SiMenjou>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiMenjou> mapping = new MappingBase<SiMenjou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiMenjou> GetRecordsByMsSiMenjouKindID(MsUser loginUser, int ms_si_menjyo_kind_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), MethodBase.GetCurrentMethod());

            List<SiMenjou> ret = new List<SiMenjou>();

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", ms_si_menjyo_kind_id));

            MappingBase<SiMenjou> mapping = new MappingBase<SiMenjou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiMenjou> GetRecordsByMsSiMenjouID(MsUser loginUser, int ms_si_menjyo_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), MethodBase.GetCurrentMethod());

            List<SiMenjou> ret = new List<SiMenjou>();

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SI_MENJOU_ID", ms_si_menjyo_id));

            MappingBase<SiMenjou> mapping = new MappingBase<SiMenjou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        // 2014.02 2013年度改造
        public static SiMenjou GetRecord(MsUser loginUser, string siMenjouID)
        {
            return GetRecord(null, loginUser, siMenjouID);
        }
        public static SiMenjou GetRecord(ORMapping.DBConnect dbConnect,　MsUser loginUser, string siMenjouID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "BySiMenjouID");

            List<SiMenjou> ret = new List<SiMenjou>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_MENJOU_ID", siMenjouID));
            MappingBase<SiMenjou> mapping = new MappingBase<SiMenjou>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<SiMenjou> GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            return GetRecordsByMsSeninID(null, loginUser, msSeninId);
        }

        public static List<SiMenjou> GetRecordsByMsSeninID( ORMapping.DBConnect dbConnect, MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "ByMsSeninID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "OrderByShowOrder");

            List<SiMenjou> ret = new List<SiMenjou>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiMenjou> mapping = new MappingBase<SiMenjou>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            foreach (SiMenjou m in ret)
            {
                m.AlarmInfoList = PtAlarmInfo.GetRecordsBySanshoumotoId(dbConnect, loginUser, m.SiMenjouID);

                if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)//(Common.DBTYPE == Common.DB_TYPE.ORACLE)// 201508 Oracle >> Postgresql対応
                {
                    m.AttachFiles = SiMenjouAttachFile.GetRecordByMenjouID(dbConnect, loginUser, m.SiMenjouID);
                }
            }

            return ret;
        }


        public static List<SiMenjou> GetRecordsByMsSeninIDAndMsSiMenjouID(MsUser loginUser, int msSeninId, int msSiMenjouId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "ByMsSeninID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "ByMsSiMenjouID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "OrderByShowOrder");

            List<SiMenjou> ret = new List<SiMenjou>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiMenjou> mapping = new MappingBase<SiMenjou>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            Params.Add(new DBParameter("MS_SI_MENJOU_ID", msSiMenjouId));
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

            Params.Add(new DBParameter("SI_MENJOU_ID", SiMenjouID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_SI_MENJOU_ID", MsSiMenjouID));
            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", MsSiMenjouKindID));

            Params.Add(new DBParameter("NO", No));
            Params.Add(new DBParameter("KIGEN", Kigen));
            Params.Add(new DBParameter("SHUTOKU_DATE", ShutokuDate));
            Params.Add(new DBParameter("CHOUHYOU_FLAG", ChouhyouFlag));
            Params.Add(new DBParameter("WRITTEN_TEST", WrittenTest));
            Params.Add(new DBParameter("BIKOU", Bikou));

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
            
            foreach (SiMenjouAttachFile a in AttachFiles)
            {
                a.SiMenjouID = SiMenjouID;
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
            Params.Add(new DBParameter("MS_SI_MENJOU_ID", MsSiMenjouID));
            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", MsSiMenjouKindID));

            Params.Add(new DBParameter("NO", No));
            Params.Add(new DBParameter("KIGEN", Kigen));
            Params.Add(new DBParameter("SHUTOKU_DATE", ShutokuDate));
            Params.Add(new DBParameter("CHOUHYOU_FLAG", ChouhyouFlag));
            Params.Add(new DBParameter("WRITTEN_TEST", WrittenTest));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_MENJOU_ID", SiMenjouID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            
            foreach (SiMenjouAttachFile a in AttachFiles)
            {
                a.RenewDate = RenewDate;
                a.RenewUserID = RenewUserID;

                bool ret = true;
                if (a.SiMenjouID == SiMenjouID)
                {
                    ret = a.UpdateRecord(dbConnect, loginUser);
                }
                else
                {
                    a.SiMenjouID = SiMenjouID;
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
            Params.Add(new DBParameter("PK", SiMenjouID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect,loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return SiMenjouID == null;
        }


        public static List<SiMenjou> GetRecordsByFilter(MsUser loginUser, SiMenjouFilter filter)
        {
            List<SiMenjou> ret = new List<SiMenjou>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), MethodBase.GetCurrentMethod());
            ParameterConnection Params = new ParameterConnection();

            if (filter.MsSiShokumeiIDs.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "FilterByMsSiShokumeiIDs");
                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", filter.MsSiShokumeiIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_SHOKUMEIS#", innerSQLStr);
                Params.AddInnerParams("p", filter.MsSiShokumeiIDs);
            }

            if (filter.ShimeiCode != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "FilterByShimeiCode");
                Params.Add(new DBParameter("SHIMEI_CODE", filter.ShimeiCode));
            }

            if (filter.Name != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "FilterBySeninName");
                Params.Add(new DBParameter("SENIN_NAME", "%" + filter.Name + "%"));
            }

            if (filter.Is退職者を除く)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiMenjou), "FilterByRetireFlag");
            }

            MappingBase<SiMenjou> mapping = new MappingBase<SiMenjou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

    }
}
