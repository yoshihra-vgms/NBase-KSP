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
    [TableAttribute("DM_KANRI_KIROKU_FILE")]
    public class DmKanriKirokuFile : ISyncTableDoc
    {
        #region データメンバ
        /// <summary>
        /// 管理記録ファイルID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KANRI_KIROKU_FILE_ID", true)]
        public string DmKanriKirokuFileID { get; set; }

        /// <summary>
        /// ファイル更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }
        
        /// <summary>
        /// 管理記録ファイル名
        /// </summary>
        [DataMember]
        [ColumnAttribute("FILE_NAME")]
        public string FileName { get; set; }
        
        /// <summary>
        /// データ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA")]
        public byte[] Data { get; set; }
       
        /// <summary>
        /// 管理記録ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KANRI_KIROKU_ID", true)]
        public string DmKanriKirokuID { get; set; }


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

        public DmKanriKirokuFile()
        {
        }

        public static List<DmKanriKirokuFile> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), "OrderBy");
            List<DmKanriKirokuFile> ret = new List<DmKanriKirokuFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanriKirokuFile> mapping = new MappingBase<DmKanriKirokuFile>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DmKanriKirokuFile> Get未送信Records(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), "OrderBy");
            List<DmKanriKirokuFile> ret = new List<DmKanriKirokuFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanriKirokuFile> mapping = new MappingBase<DmKanriKirokuFile>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static DmKanriKirokuFile GetRecord(NBaseData.DAC.MsUser loginUser, string DmKanriKirokuFileID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), "ByDmKanriKirokuFileID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), "OrderBy");
            List<DmKanriKirokuFile> ret = new List<DmKanriKirokuFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanriKirokuFile> mapping = new MappingBase<DmKanriKirokuFile>();
            Params.Add(new DBParameter("DM_KANRI_KIROKU_FILE_ID", DmKanriKirokuFileID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static DmKanriKirokuFile GetRecordByKanriKirokuID(NBaseData.DAC.MsUser loginUser, string DmKanriKirokuID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), "ByDmKanriKirokuID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), "OrderBy");
            List<DmKanriKirokuFile> ret = new List<DmKanriKirokuFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKanriKirokuFile> mapping = new MappingBase<DmKanriKirokuFile>();
            Params.Add(new DBParameter("DM_KANRI_KIROKU_ID", DmKanriKirokuID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DM_KANRI_KIROKU_FILE_ID", DmKanriKirokuFileID));
            Params.Add(new DBParameter("UPDATE_DATE", UpdateDate));
            Params.Add(new DBParameter("FILE_NAME", FileName));
            Params.Add(new DBParameter("DATA", Data));
            Params.Add(new DBParameter("DM_KANRI_KIROKU_ID", DmKanriKirokuID));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKanriKirokuFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("UPDATE_DATE", UpdateDate));
            Params.Add(new DBParameter("FILE_NAME", FileName));
            Params.Add(new DBParameter("DATA", Data));
            Params.Add(new DBParameter("DM_KANRI_KIROKU_ID", DmKanriKirokuID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("DM_KANRI_KIROKU_FILE_ID", DmKanriKirokuFileID));

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
            Params.Add(new DBParameter("PK", DmKanriKirokuFileID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



