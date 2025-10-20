using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping.Atts;
using ORMapping;
using System.Reflection;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SN_SYNC_INFO")]
    public class SnSyncInfo
    {
        #region データメンバ

        /// <summary>
        /// コンピュータ名
        /// </summary>
        [DataMember]
        [ColumnAttribute("HOST_NAME")]
        public string HostName { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// NBaseHonsenログインユーザID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_USER_ID")]
        public string MsUserID { get; set; }

        /// <summary>
        /// 船側同期日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_DATE")]
        public DateTime VesselDate { get; set; }

        /// <summary>
        /// 船からの送信データ
        /// </summary>
        [DataMember]
        [ColumnAttribute("FROM_VESSEL_FLAG")]
        public int FromVesselFlag { get; set; }

        /// <summary>
        /// サーバ側同期日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("SERVER_DATE")]
        public DateTime ServerDate { get; set; }

        /// <summary>
        /// サーバからの送信データ
        /// </summary>
        [DataMember]
        [ColumnAttribute("FROM_SERVER_FLAG")]
        public int FromServerFlag { get; set; }

        /// <summary>
        /// 同期進捗
        /// </summary>
        [DataMember]
        [ColumnAttribute("SYNC_STEP")]
        public int SyncStep { get; set; }

        /// <summary>
        /// モジュールVersion
        /// </summary>
        [DataMember]
        [ColumnAttribute("MODULE_VERSION")]
        public string ModuleVersion { get; set; }

        /// <summary>
        /// 管理番号（共通）
        /// </summary>
        [DataMember]
        [ColumnAttribute("MAX_DATA_NO_0")]
        public Int64 MaxDataNoOfVesselIdZero { get; set; }

        /// <summary>
        /// 管理番号（個別）
        /// </summary>
        [DataMember]
        [ColumnAttribute("MAX_DATA_NO_1")]
        public Int64 MaxDataNo { get; set; }

        /// <summary>
        /// 同期状況
        /// </summary>
        [DataMember]
        [ColumnAttribute("SYNC_STATUS")]
        public int SyncStatus { get; set; }
       
        #endregion

        public enum 同期進捗
        {
            同期開始,
            船データ登録,
            サーバデータ送信
        }

        public enum データFLAG
        {
            なし,
            あり
        }

        private static string[] StrデータFlag = { "同期ﾃﾞｰﾀなし", "同期ﾃﾞｰﾀあり" };
        public static string FormatデータFlag(int dataFlag)
        {
            return StrデータFlag[dataFlag];
        }

        private static string[] Str同期進捗 = { "同期開始", "船ﾃﾞｰﾀ登録", "ｻｰﾊﾞﾃﾞｰﾀ送信" };
        public static string Format同期進捗(int syncStep)
        {
            return Str同期進捗[syncStep];
        }



        public SnSyncInfo()
        {
        }

        public static List<SnSyncInfo> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SnSyncInfo), MethodBase.GetCurrentMethod());
            List<SnSyncInfo> ret = new List<SnSyncInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SnSyncInfo> mapping = new MappingBase<SnSyncInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static SnSyncInfo GetRecord(NBaseData.DAC.MsUser loginUser, string hostName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SnSyncInfo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SnSyncInfo), "ByHostName");
            List<SnSyncInfo> ret = new List<SnSyncInfo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("HOST_NAME", hostName));
            MappingBase<SnSyncInfo> mapping = new MappingBase<SnSyncInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SnSyncInfo), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("HOST_NAME", HostName));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
            Params.Add(new DBParameter("VESSEL_DATE", VesselDate));
            Params.Add(new DBParameter("FROM_VESSEL_FLAG", FromVesselFlag));
            Params.Add(new DBParameter("SERVER_DATE", ServerDate));
            Params.Add(new DBParameter("FROM_SERVER_FLAG", FromServerFlag));
            Params.Add(new DBParameter("SYNC_STEP", SyncStep));
            Params.Add(new DBParameter("MODULE_VERSION", ModuleVersion));
            Params.Add(new DBParameter("MAX_DATA_NO_0", MaxDataNoOfVesselIdZero));
            Params.Add(new DBParameter("MAX_DATA_NO_1", MaxDataNo));
            Params.Add(new DBParameter("SYNC_STATUS", SyncStatus));
            #endregion

            //int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            int cnt = 0;
            try
            {
                cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            }
            catch (Exception ex)
            {
            }

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SnSyncInfo), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
            Params.Add(new DBParameter("VESSEL_DATE", VesselDate));
            Params.Add(new DBParameter("FROM_VESSEL_FLAG", FromVesselFlag));
            Params.Add(new DBParameter("SERVER_DATE", ServerDate));
            Params.Add(new DBParameter("FROM_SERVER_FLAG", FromServerFlag));
            Params.Add(new DBParameter("SYNC_STEP", SyncStep));
            Params.Add(new DBParameter("MODULE_VERSION", ModuleVersion));
            Params.Add(new DBParameter("MAX_DATA_NO_0", MaxDataNoOfVesselIdZero));
            Params.Add(new DBParameter("MAX_DATA_NO_1", MaxDataNo));
            Params.Add(new DBParameter("SYNC_STATUS", SyncStatus));

            Params.Add(new DBParameter("HOST_NAME", HostName));
            #endregion

            //int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            int cnt = 0;
            try
            {
                cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            }
            catch (Exception ex)
            {
            }

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }
    }
}
