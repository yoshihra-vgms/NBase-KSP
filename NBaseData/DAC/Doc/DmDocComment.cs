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
    [TableAttribute("DM_DOC_COMMENT")]
    public class DmDocComment : ISyncTableDoc
    {
        #region データメンバ
        /// <summary>
        /// コメント登録ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_DOC_COMMENT_ID", true)]
        public string DmDocCommentID { get; set; }

        /// <summary>
        /// 登録日
        /// </summary>
        [DataMember]
        [ColumnAttribute("REG_DATE")]
        public DateTime RegDate { get; set; }

        /// <summary>
        /// ユーザID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_USER_ID")]
        public string MsUserID { get; set; }


        [DataMember]
        [ColumnAttribute("DOC_FLAG_CEO")]
        public int DocFlag_CEO { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_ADMIN")]
        public int DocFlag_Admin { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_MSI_FERRY")]
        public int DocFlag_MsiFerry { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_CREW_FERRY")]
        public int DocFlag_CrewFerry { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_TSI_FERRY")]
        public int DocFlag_TsiFerry { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_MSI_CARGO")]
        public int DocFlag_MsiCargo { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_CREW_CARGO")]
        public int DocFlag_CrewCargo { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_TSI_CARGO")]
        public int DocFlag_TsiCargo { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_OFFICER")]
        public int DocFlag_Officer { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_GL")]
        public int DocFlag_GL { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_TL")]
        public int DocFlag_TL { get; set; }

        [DataMember]
        [ColumnAttribute("DOC_FLAG_SD_MANAGER")]
        public int DocFlag_SdManager { get; set; }


        /// <summary>
        /// 公開先フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOUKAI_SAKI")]
        public int KoukaiSaki { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 部門ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BUMON_ID")]
        public string MsBumonID { get; set; }

        /// <summary>
        /// LINK先フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("LINK_SAKI")]
        public int LinkSaki { get; set; }

        /// <summary>
        /// LINK先ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("LINK_SAKI_ID")]
        public string LinkSakiID { get; set; }



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

        public DmDocComment()
        {
        }

        public static List<DmDocComment> GetRecordsByLinkSaki(NBaseData.DAC.MsUser loginUser, int linkSaki, string linkSakiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmDocComment), "GetRecords");
            List<DmDocComment> ret = new List<DmDocComment>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmDocComment> mapping = new MappingBase<DmDocComment>();
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmDocComment), "ByLinkSaki");
            Params.Add(new DBParameter("LINK_SAKI", linkSaki));
            Params.Add(new DBParameter("LINK_SAKI_ID", linkSakiId));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmDocComment), "OrderBy");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static DmDocComment GetRecord(NBaseData.DAC.MsUser loginUser, string DmDocCommentID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmDocComment), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmDocComment), "ByDmDocCommentID");
            List<DmDocComment> ret = new List<DmDocComment>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmDocComment> mapping = new MappingBase<DmDocComment>();
            Params.Add(new DBParameter("DM_DOC_COMMENT_ID", DmDocCommentID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmDocComment), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DM_DOC_COMMENT_ID", DmDocCommentID));
            Params.Add(new DBParameter("REG_DATE", RegDate));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));

            Params.Add(new DBParameter("DOC_FLAG_CEO", DocFlag_CEO));
            Params.Add(new DBParameter("DOC_FLAG_ADMIN", DocFlag_Admin));
            Params.Add(new DBParameter("DOC_FLAG_MSI_FERRY", DocFlag_MsiFerry));
            Params.Add(new DBParameter("DOC_FLAG_CREW_FERRY", DocFlag_CrewFerry));
            Params.Add(new DBParameter("DOC_FLAG_TSI_FERRY", DocFlag_TsiFerry));
            Params.Add(new DBParameter("DOC_FLAG_MSI_CARGO", DocFlag_MsiCargo));
            Params.Add(new DBParameter("DOC_FLAG_CREW_CARGO", DocFlag_CrewCargo));
            Params.Add(new DBParameter("DOC_FLAG_TSI_CARGO", DocFlag_TsiCargo));
            Params.Add(new DBParameter("DOC_FLAG_OFFICER", DocFlag_Officer));
            Params.Add(new DBParameter("DOC_FLAG_GL", DocFlag_GL));
            Params.Add(new DBParameter("DOC_FLAG_TL", DocFlag_TL));
            Params.Add(new DBParameter("DOC_FLAG_SD_MANAGER", DocFlag_SdManager));

            Params.Add(new DBParameter("KOUKAI_SAKI", KoukaiSaki));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_BUMON_ID", MsBumonID));
            Params.Add(new DBParameter("LINK_SAKI", LinkSaki));
            Params.Add(new DBParameter("LINK_SAKI_ID", LinkSakiID));
            
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

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }

        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmDocComment), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("REG_DATE", RegDate));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));

            Params.Add(new DBParameter("DOC_FLAG_CEO", DocFlag_CEO));
            Params.Add(new DBParameter("DOC_FLAG_ADMIN", DocFlag_Admin));
            Params.Add(new DBParameter("DOC_FLAG_MSI_FERRY", DocFlag_MsiFerry));
            Params.Add(new DBParameter("DOC_FLAG_CREW_FERRY", DocFlag_CrewFerry));
            Params.Add(new DBParameter("DOC_FLAG_TSI_FERRY", DocFlag_TsiFerry));
            Params.Add(new DBParameter("DOC_FLAG_MSI_CARGO", DocFlag_MsiCargo));
            Params.Add(new DBParameter("DOC_FLAG_CREW_CARGO", DocFlag_CrewCargo));
            Params.Add(new DBParameter("DOC_FLAG_TSI_CARGO", DocFlag_TsiCargo));
            Params.Add(new DBParameter("DOC_FLAG_OFFICER", DocFlag_Officer));
            Params.Add(new DBParameter("DOC_FLAG_SD_MANAGER", DocFlag_SdManager));
            Params.Add(new DBParameter("DOC_FLAG_GL", DocFlag_GL));
            Params.Add(new DBParameter("DOC_FLAG_TL", DocFlag_TL));

            Params.Add(new DBParameter("KOUKAI_SAKI", KoukaiSaki));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_BUMON_ID", MsBumonID));
            Params.Add(new DBParameter("LINK_SAKI", LinkSaki));
            Params.Add(new DBParameter("LINK_SAKI_ID", LinkSakiID));
            
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("DM_DOC_COMMENT_ID", DmDocCommentID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public static bool LogicalDelete(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string linkSakiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmDocComment), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("LINK_SAKI_ID", linkSakiId));
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", DmDocCommentID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



