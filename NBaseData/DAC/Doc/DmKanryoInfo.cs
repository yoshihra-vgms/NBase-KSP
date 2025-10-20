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
    [TableAttribute("DM_KANRYO_INFO")]
    public class DmKanryoInfo : ISyncTableDoc
    {
        #region データメンバ
        /// <summary>
        /// 完了情報ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KANRYO_INFO_ID", true)]
        public string DmKanryoInfoID { get; set; }

        /// <summary>
        /// 完了日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANRYO_DATE")]
        public DateTime KanryoDate { get; set; }

        /// <summary>
        /// ユーザID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_USER_ID")]
        public string MsUserID { get; set; }

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

        public DmKanryoInfo()
        {
        }

        public static List<DmKanryoInfo> GetRecordsByLinkSaki(NBaseData.DAC.MsUser loginUser, int linkSaki, string linkSakiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanryoInfo), "GetRecords");
            List<DmKanryoInfo> ret = new List<DmKanryoInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanryoInfo> mapping = new MappingBase<DmKanryoInfo>();
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanryoInfo), "ByLinkSaki");
            Params.Add(new DBParameter("LINK_SAKI", linkSaki));
            Params.Add(new DBParameter("LINK_SAKI_ID", linkSakiId));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanryoInfo), "OrderBy");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static DmKanryoInfo GetRecord(NBaseData.DAC.MsUser loginUser, string DmKanryoInfoID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanryoInfo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanryoInfo), "ByDmKanryoInfoID");
            List<DmKanryoInfo> ret = new List<DmKanryoInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanryoInfo> mapping = new MappingBase<DmKanryoInfo>();
            Params.Add(new DBParameter("DM_KANRYO_INFO_ID", DmKanryoInfoID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanryoInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DM_KANRYO_INFO_ID", DmKanryoInfoID));
            Params.Add(new DBParameter("KANRYO_DATE", KanryoDate));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanryoInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("KANRYO_DATE", KanryoDate));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
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
            Params.Add(new DBParameter("DM_KANRYO_INFO_ID", DmKanryoInfoID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public static bool LogicalDelete(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string linkSakiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanryoInfo), MethodBase.GetCurrentMethod());

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
            Params.Add(new DBParameter("PK", DmKanryoInfoID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



