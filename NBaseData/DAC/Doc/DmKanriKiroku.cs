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
    [TableAttribute("DM_KANRI_KIROKU")]
    public class DmKanriKiroku : ISyncTableDoc
    {
        #region データメンバ
        /// <summary>
        /// 管理記録ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KANRI_KIROKU_ID", true)]
        public string DmKanriKirokuID { get; set; }
        
        /// <summary>
        /// 報告書ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_HOUKOKUSHO_ID")]
        public string MsDmHoukokushoID { get; set; }

        /// <summary>
        /// 状況
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS")]
        public int Status { get; set; }

        /// <summary>
        /// 時期（年）
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIKI_NEN")]
        public int JikiNen { get; set; }

        /// <summary>
        /// 時期（月）
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIKI_TUKI")]
        public int JikiTuki { get; set; }

        /// <summary>
        /// 発行日
        /// </summary>
        [DataMember]
        [ColumnAttribute("ISSUE_DATE")]
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// ファイル名
        /// </summary>
        [DataMember]
        [ColumnAttribute("FILE_NAME")]
        public string FileName { get; set; }

        /// <summary>
        /// ファイル更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("FILE_UPDATE_DATE")]
        public DateTime FileUpdateDate { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

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


        /// <summary>
        /// 分類名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_BUNRUI_NAME")]
        public string BunruiName { get; set; }


        /// <summary>
        /// 小分類名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_SHOUBUNRUI_NAME")]
        public string ShoubunruiName { get; set; }

        /// <summary>
        /// 文書番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_HOUKOKUSHO_BUNSHO_NO")]
        public string BunshoNo { get; set; }

        /// <summary>
        /// 文書名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_HOUKOKUSHO_BUNSHO_NAME")]
        public string BunshoName { get; set; }

        #endregion

        public string StatusStr
        {
            get
            {
                return Status == (int)DocConstants.StatusEnum.未完了 ? "未完了" : "完了";
            }
        }

        public DmKanriKiroku()
        {
        }

        public static List<DmKanriKiroku> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "OrderBy");
            List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static DmKanriKiroku GetRecord(NBaseData.DAC.MsUser loginUser, string DmKanriKirokuID)
        {
            return GetRecord(null, loginUser, DmKanriKirokuID);
        }
        public static DmKanriKiroku GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string DmKanriKirokuID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "ByDmKanriKirokuID");
            List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();
            Params.Add(new DBParameter("DM_KANRI_KIROKU_ID", DmKanriKirokuID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<DmKanriKiroku> GetRecordsByHoukokushoID(NBaseData.DAC.MsUser loginUser, string msDmHoukokushoId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "ByMsDmHoukokushoID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "OrderBy");
            List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", msDmHoukokushoId));
            MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //public static DmKanriKiroku GetLatestRecord(NBaseData.DAC.MsUser loginUser, string houkokushoId, string userId, bool kaicho_shacho, bool sekininsha)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetLatestRecord");
        //    List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
        //    ParameterConnection Params = new ParameterConnection();
        //    MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();

        //    string INNER_SQL = "";
        //    string SQL1 = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetLatestRecord_G");
        //    string SQL2 = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetLatestRecord_KS");
        //    string SQL3 = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetLatestRecord_S");

        //    INNER_SQL = SQL1;
        //    Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録));
        //    Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.部門));
        //    Params.Add(new DBParameter("MS_USER_ID", userId));
        //    Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", houkokushoId));
        //    //if (kaicho_shacho)
        //    //{
        //    //    INNER_SQL += " UNION " + SQL2;
        //    //    Params.Add(new DBParameter("KOUKAI_SAKI_KAICHO_SHACHO", (int)NBaseData.DS.DocConstants.RoleEnum.会長社長));
        //    //}
        //    if (sekininsha)
        //    {
        //        INNER_SQL += " UNION " + SQL3;
        //        Params.Add(new DBParameter("KOUKAI_SAKI_SEKININSHA", (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者));
        //    }
        //    SQL = SQL.Replace("#INNER_SQL#", INNER_SQL);
        //    ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

        //    if (ret.Count == 0)
        //        return null;
        //    return ret[0];
        //}
        public static DmKanriKiroku GetLatestRecord(NBaseData.DAC.MsUser loginUser, string houkokushoId, string userId, int role)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetLatestRecord");
            List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();

            string INNER_SQL = "";
            string SQL1 = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetLatestRecord_G");
            string SQL2 = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetLatestRecord_R");

            INNER_SQL = SQL1;
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.部門));
            Params.Add(new DBParameter("MS_USER_ID", userId));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", houkokushoId));
            if (role > 0)
            {
                INNER_SQL += " UNION " + SQL2;
                Params.Add(new DBParameter("KOUKAI_SAKI_ROLE", role));
            }
            SQL = SQL.Replace("#INNER_SQL#", INNER_SQL);
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static DmKanriKiroku GetLatestRecord(NBaseData.DAC.MsUser loginUser, string houkokushoId, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetHonsenLatestRecord");
            List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();

            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", houkokushoId));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        #region 20210824 下記に変更
        //public static List<DmKanriKiroku> GetPastRecords(NBaseData.DAC.MsUser loginUser, string houkokushoId, string userId, bool kaicho_shacho, bool sekininsha)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetPastRecords");
        //    List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
        //    ParameterConnection Params = new ParameterConnection();
        //    MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();

        //    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "ByPastRecordsG");
        //    Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録));
        //    Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.部門));
        //    Params.Add(new DBParameter("MS_USER_ID", userId));
        //    Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", houkokushoId));
        //    
        //    if (kaicho_shacho)
        //    {
        //        SQL += " UNION " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetPastRecords");
        //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "ByPastRecordsKS");
        //        Params.Add(new DBParameter("KOUKAI_SAKI_KAICHO_SHACHO", (int)NBaseData.DS.DocConstants.RoleEnum.会長社長));
        //    }
        //    if (sekininsha)
        //    {
        //        SQL += " UNION " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetPastRecords");
        //        SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "ByPastRecordsS");
        //        Params.Add(new DBParameter("KOUKAI_SAKI_SEKININSHA", (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者));
        //    }
        //    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "OrderByPastRecords");

        //    ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

        //    return ret;
        //}
        #endregion

        public static List<DmKanriKiroku> GetPastRecords(NBaseData.DAC.MsUser loginUser, string houkokushoId, string userId, int role)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetPastRecords");
            List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "ByPastRecordsG");
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.部門));
            Params.Add(new DBParameter("MS_USER_ID", userId));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", houkokushoId));
            if (role > 0)
            {
                SQL += " UNION " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetPastRecords");
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "ByPastRecordsR");
                Params.Add(new DBParameter("KOUKAI_SAKI_ROLE", role));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "OrderByPastRecords");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DmKanriKiroku> GetPastRecords(NBaseData.DAC.MsUser loginUser, string houkokushoId, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetHonsenPastRecords");
            List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();

            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", houkokushoId));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }




        public static List<DmKanriKiroku> GetTargetRecords(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetTargetRecords");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));

            List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
            MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DmKanriKiroku> GetDataFileNotFoundRecords(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetTargetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "AndFileNotFoundRecords");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));

            List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
            MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DmKanriKiroku> GetDataUpdateDateDiffRecords(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "GetTargetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), "AndDataUpdateDateDiffRecords");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));

            List<DmKanriKiroku> ret = new List<DmKanriKiroku>();
            MappingBase<DmKanriKiroku> mapping = new MappingBase<DmKanriKiroku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        #region INSERT
        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DM_KANRI_KIROKU_ID", DmKanriKirokuID));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", MsDmHoukokushoID));
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("JIKI_NEN", JikiNen));
            Params.Add(new DBParameter("JIKI_TUKI", JikiTuki));
            Params.Add(new DBParameter("ISSUE_DATE", IssueDate));
            Params.Add(new DBParameter("FILE_NAME",FileName));
            Params.Add(new DBParameter("FILE_UPDATE_DATE", FileUpdateDate));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
        #endregion

        #region UPDATE
        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }

        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKiroku), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", MsDmHoukokushoID));
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("JIKI_NEN", JikiNen));
            Params.Add(new DBParameter("JIKI_TUKI", JikiTuki));
            Params.Add(new DBParameter("ISSUE_DATE", IssueDate));
            Params.Add(new DBParameter("FILE_NAME", FileName));
            Params.Add(new DBParameter("FILE_UPDATE_DATE", FileUpdateDate));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("DM_KANRI_KIROKU_ID", DmKanriKirokuID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
        #endregion

        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", DmKanriKirokuID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



