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
    [TableAttribute("DM_KOUBUNSHO_KISOKU")]
    public class DmKoubunshoKisoku : ISyncTableDoc
    {
        #region データメンバ
        /// <summary>
        /// 公文書_規則ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KOUBUNSHO_KISOKU_ID", true)]
        public string DmKoubunshoKisokuID { get; set; }
        
        /// <summary>
        /// 分類ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_BUNRUI_ID")]
        public string MsDmBunruiID { get; set; }
        
        /// <summary>
        /// 小分類ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_SHOUBUNRUI_ID")]
        public string MsDmShoubunruiID { get; set; }

        /// <summary>
        /// 文書番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUNSHO_NO")]
        public string BunshoNo { get; set; }

        /// <summary>
        /// 文書名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUNSHO_NAME")]
        public string BunshoName { get; set; }

        /// <summary>
        /// 状況
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS")]
        public int Status { get; set; }

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

        #endregion

        public DmKoubunshoKisoku()
        {
        }

        public static List<DmKoubunshoKisoku> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "OrderBy");
            List<DmKoubunshoKisoku> ret = new List<DmKoubunshoKisoku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKoubunshoKisoku> mapping = new MappingBase<DmKoubunshoKisoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static DmKoubunshoKisoku GetRecord(NBaseData.DAC.MsUser loginUser, string DmKoubunshoKisokuID)
        {
            return GetRecord(null, loginUser, DmKoubunshoKisokuID);
        }
        public static DmKoubunshoKisoku GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string DmKoubunshoKisokuID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "ByDmKoubunshoKisokuID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "OrderBy");
            List<DmKoubunshoKisoku> ret = new List<DmKoubunshoKisoku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKoubunshoKisoku> mapping = new MappingBase<DmKoubunshoKisoku>();
            Params.Add(new DBParameter("DM_KOUBUNSHO_KISOKU_ID", DmKoubunshoKisokuID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<DmKoubunshoKisoku> GetRecordsByBunruiID(NBaseData.DAC.MsUser loginUser, string msDmBunruiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "ByMsDmBunruiID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "OrderBy");
            List<DmKoubunshoKisoku> ret = new List<DmKoubunshoKisoku>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DM_BUNRUI_ID", msDmBunruiID));
            MappingBase<DmKoubunshoKisoku> mapping = new MappingBase<DmKoubunshoKisoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DmKoubunshoKisoku> GetRecordsByShoubunruiID(NBaseData.DAC.MsUser loginUser, string msDmShoubunruiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "ByMsDmShoubunruiID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "OrderBy");
            List<DmKoubunshoKisoku> ret = new List<DmKoubunshoKisoku>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DM_SHOUBUNRUI_ID", msDmShoubunruiID));
            MappingBase<DmKoubunshoKisoku> mapping = new MappingBase<DmKoubunshoKisoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }




        public static List<DmKoubunshoKisoku> GetTargetRecords(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "GetTargetRecords");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));

            List<DmKoubunshoKisoku> ret = new List<DmKoubunshoKisoku>();
            MappingBase<DmKoubunshoKisoku> mapping = new MappingBase<DmKoubunshoKisoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DmKoubunshoKisoku> GetDataFileNotFoundRecords(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "GetTargetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "AndFileNotFoundRecords");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));

            List<DmKoubunshoKisoku> ret = new List<DmKoubunshoKisoku>();
            MappingBase<DmKoubunshoKisoku> mapping = new MappingBase<DmKoubunshoKisoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DmKoubunshoKisoku> GetDataUpdateDateDiffRecords(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "GetTargetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), "AndDataUpdateDateDiffRecords");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));

            List<DmKoubunshoKisoku> ret = new List<DmKoubunshoKisoku>();
            MappingBase<DmKoubunshoKisoku> mapping = new MappingBase<DmKoubunshoKisoku>();
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DM_KOUBUNSHO_KISOKU_ID", DmKoubunshoKisokuID));
            Params.Add(new DBParameter("MS_DM_BUNRUI_ID", MsDmBunruiID));
            Params.Add(new DBParameter("MS_DM_SHOUBUNRUI_ID", MsDmShoubunruiID));
            Params.Add(new DBParameter("BUNSHO_NO", BunshoNo));
            Params.Add(new DBParameter("BUNSHO_NAME", BunshoName));
            Params.Add(new DBParameter("STATUS", Status));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisoku), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DM_BUNRUI_ID", MsDmBunruiID));
            Params.Add(new DBParameter("MS_DM_SHOUBUNRUI_ID", MsDmShoubunruiID));
            Params.Add(new DBParameter("BUNSHO_NO", BunshoNo));
            Params.Add(new DBParameter("BUNSHO_NAME", BunshoName));
            Params.Add(new DBParameter("STATUS", Status));
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
            Params.Add(new DBParameter("DM_KOUBUNSHO_KISOKU_ID", DmKoubunshoKisokuID));

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
            Params.Add(new DBParameter("PK", DmKoubunshoKisokuID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



