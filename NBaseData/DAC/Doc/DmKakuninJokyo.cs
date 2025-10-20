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
    [TableAttribute("DM_KAKUNIN_JOKYO")]
    public class DmKakuninJokyo : ISyncTableDoc
    {
       #region データメンバ
        /// <summary>
        /// 確認状況ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KAKUNIN_JOKYO_ID", true)]
        public string DmKakuninJokyoID { get; set; }

        /// <summary>
        /// 表示日
        /// </summary>
        [DataMember]
        [ColumnAttribute("VIEW_DATE")]
        public DateTime ViewDate { get; set; }

        /// <summary>
        /// 確認日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAKUNIN_DATE")]
        public DateTime KakuninDate { get; set; }

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
        
        /// <summary>
        /// 表示順序
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }


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
        /// 姓
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEI")]
        public string Sei { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MEI")]
        public string Mei { get; set; }

        public string FullName
        {
            get
            {
                return Sei + " " + Mei;
            }
        }
        #endregion

        public DmKakuninJokyo()
        {
        }

        public static List<DmKakuninJokyo> GetRecordsByLinkSaki(NBaseData.DAC.MsUser loginUser, int linkSaki, string linkSakiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "GetRecords");
            List<DmKakuninJokyo> ret = new List<DmKakuninJokyo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKakuninJokyo> mapping = new MappingBase<DmKakuninJokyo>();
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "ByLinkSaki");
            Params.Add(new DBParameter("LINK_SAKI", linkSaki));
            Params.Add(new DBParameter("LINK_SAKI_ID", linkSakiId));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "OrderBy");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DmKakuninJokyo> GetRecordsByLinkSaki(NBaseData.DAC.MsUser loginUser, int linkSaki, string innerSQL, ParameterConnection Params)
        {
            List<DmKakuninJokyo> ret = new List<DmKakuninJokyo>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "ByLinkSakiIDs");
            Params.Add(new DBParameter("LINK_SAKI_KUBUN", linkSaki));
            SQL = SQL.Replace("#INNER_SELECT_STR#", innerSQL);
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "OrderBy");

            MappingBase<DmKakuninJokyo> mapping = new MappingBase<DmKakuninJokyo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static DmKakuninJokyo GetRecordByLinkSakiUser(NBaseData.DAC.MsUser loginUser, int linkSaki, string linkSakiId, string userId)
        {
            return GetRecordByLinkSakiUser(null, loginUser, linkSaki, linkSakiId, userId);
        }
        public static DmKakuninJokyo GetRecordByLinkSakiUser(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int linkSaki, string linkSakiId, string userId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "GetRecords");
            List<DmKakuninJokyo> ret = new List<DmKakuninJokyo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKakuninJokyo> mapping = new MappingBase<DmKakuninJokyo>();
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "ByLinkSaki");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "ByUserID");
            Params.Add(new DBParameter("LINK_SAKI", linkSaki));
            Params.Add(new DBParameter("LINK_SAKI_ID", linkSakiId));
            Params.Add(new DBParameter("MS_USER_ID", userId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static DmKakuninJokyo GetRecordByLinkSakiKoukaiSakiUser(NBaseData.DAC.MsUser loginUser, int linkSaki, string linkSakiId, int koukaiSaki, string userId)
        {
            return GetRecordByLinkSakiKoukaiSakiUser(null, loginUser, linkSaki, linkSakiId, koukaiSaki, userId);
        }
        public static DmKakuninJokyo GetRecordByLinkSakiKoukaiSakiUser(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int linkSaki, string linkSakiId, int koukaiSaki, string userId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "GetRecords");
            List<DmKakuninJokyo> ret = new List<DmKakuninJokyo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKakuninJokyo> mapping = new MappingBase<DmKakuninJokyo>();
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "ByLinkSaki");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "ByKoukaiSaki");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "ByUserID");
            Params.Add(new DBParameter("LINK_SAKI", linkSaki));
            Params.Add(new DBParameter("LINK_SAKI_ID", linkSakiId));
            Params.Add(new DBParameter("KOUKAI_SAKI", koukaiSaki));
            Params.Add(new DBParameter("MS_USER_ID", userId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static DmKakuninJokyo GetRecord(NBaseData.DAC.MsUser loginUser, string DmKakuninJokyoID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), "ByDmKakuninJokyoID");
            List<DmKakuninJokyo> ret = new List<DmKakuninJokyo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKakuninJokyo> mapping = new MappingBase<DmKakuninJokyo>();
            Params.Add(new DBParameter("DM_KAKUNIN_JOKYO_ID", DmKakuninJokyoID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DM_KAKUNIN_JOKYO_ID", DmKakuninJokyoID));
            Params.Add(new DBParameter("VIEW_DATE", ViewDate));
            Params.Add(new DBParameter("KAKUNIN_DATE", KakuninDate));
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
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VIEW_DATE", ViewDate));
            Params.Add(new DBParameter("KAKUNIN_DATE", KakuninDate));
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
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
           
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("DM_KAKUNIN_JOKYO_ID", DmKakuninJokyoID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public static bool LogicalDelete(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string linkSakiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKakuninJokyo), MethodBase.GetCurrentMethod());

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
            Params.Add(new DBParameter("PK", DmKakuninJokyoID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



