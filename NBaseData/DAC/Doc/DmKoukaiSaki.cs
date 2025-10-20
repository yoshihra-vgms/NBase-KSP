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
    [TableAttribute("DM_KOUKAI_SAKI")]
    public class DmKoukaiSaki : ISyncTableDoc
    {
        #region データメンバ
        /// <summary>
        /// 公開先ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KOUKAI_SAKI_ID", true)]
        public string DmKoukaiSakiID { get; set; }

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

        public DmKoukaiSaki()
        {
        }

        public static List<DmKoukaiSaki> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), "OrderBy");
            List<DmKoukaiSaki> ret = new List<DmKoukaiSaki>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKoukaiSaki> mapping = new MappingBase<DmKoukaiSaki>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<DmKoukaiSaki> GetRecordsByLinkSakiID(NBaseData.DAC.MsUser loginUser, string linkSakiId)
        {
            return GetRecordsByLinkSakiID(null, loginUser, linkSakiId);
        }

        public static List<DmKoukaiSaki> GetRecordsByLinkSakiID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string linkSakiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), "GetRecords");
            List<DmKoukaiSaki> ret = new List<DmKoukaiSaki>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKoukaiSaki> mapping = new MappingBase<DmKoukaiSaki>();
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), "ByLinkSakiID");
            Params.Add(new DBParameter("LINK_SAKI_ID", linkSakiId));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), "OrderBy");
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DmKoukaiSaki> GetRecordsByLinkSakiIDs(NBaseData.DAC.MsUser loginUser, string innerSQL, ParameterConnection Params)
        {
            List<DmKoukaiSaki> ret = new List<DmKoukaiSaki>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), "ByLinkSakiIDs");
            SQL = SQL.Replace("#INNER_SELECT_STR#", innerSQL);
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), "OrderBy");

            MappingBase<DmKoukaiSaki> mapping = new MappingBase<DmKoukaiSaki>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static DmKoukaiSaki GetRecord(NBaseData.DAC.MsUser loginUser, string DmKoukaiSakiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), "ByDmKoukaiSakiID");
            List<DmKoukaiSaki> ret = new List<DmKoukaiSaki>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKoukaiSaki> mapping = new MappingBase<DmKoukaiSaki>();
            Params.Add(new DBParameter("DM_KOUKAI_SAKI_ID", DmKoukaiSakiID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DM_KOUKAI_SAKI_ID", DmKoukaiSakiID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
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
            Params.Add(new DBParameter("DM_KOUKAI_SAKI_ID", DmKoukaiSakiID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public static bool LogicalDelete(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string linkSakiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), MethodBase.GetCurrentMethod());

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

        // m.yoshihara 2017/5/30  一致する船IDの論理削除
        public static bool LogicalDeleteByMsVesseId(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int msVID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoukaiSaki), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVID));
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
            Params.Add(new DBParameter("PK", DmKoukaiSakiID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



