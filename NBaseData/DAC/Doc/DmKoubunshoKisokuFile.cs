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
    [TableAttribute("DM_KOUBUNSHO_KISOKU_FILE")]
    public class DmKoubunshoKisokuFile : ISyncTableDoc
    {
        #region データメンバ
        /// <summary>
        /// 公文書_規則ファイルID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KOUBUNSHO_KISOKU_FILE_ID", true)]
        public string DmKoubunshoKisokuFileID { get; set; }

        /// <summary>
        /// ファイル更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }
        
        /// <summary>
        /// 公文書_規則ファイル名
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
        /// 公文書_規則ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("DM_KOUBUNSHO_KISOKU_ID", true)]
        public string DmKoubunshoKisokuID { get; set; }


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

        public DmKoubunshoKisokuFile()
        {
        }

        public static List<DmKoubunshoKisokuFile> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), "OrderBy");
            List<DmKoubunshoKisokuFile> ret = new List<DmKoubunshoKisokuFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKoubunshoKisokuFile> mapping = new MappingBase<DmKoubunshoKisokuFile>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<DmKoubunshoKisokuFile> Get未送信Records(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), "OrderBy");
            List<DmKoubunshoKisokuFile> ret = new List<DmKoubunshoKisokuFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKoubunshoKisokuFile> mapping = new MappingBase<DmKoubunshoKisokuFile>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static DmKoubunshoKisokuFile GetRecord(NBaseData.DAC.MsUser loginUser, string DmKoubunshoKisokuFileID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), "ByDmKoubunshoKisokuFileID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), "OrderBy");
            List<DmKoubunshoKisokuFile> ret = new List<DmKoubunshoKisokuFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKoubunshoKisokuFile> mapping = new MappingBase<DmKoubunshoKisokuFile>();
            Params.Add(new DBParameter("DM_KOUBUNSHO_KISOKU_FILE_ID", DmKoubunshoKisokuFileID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static DmKoubunshoKisokuFile GetRecordByKoubunshoKisokuID(NBaseData.DAC.MsUser loginUser, string DmKoubunshoKisokuID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), "ByDmKoubunshoKisokuID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), "OrderBy");
            List<DmKoubunshoKisokuFile> ret = new List<DmKoubunshoKisokuFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<DmKoubunshoKisokuFile> mapping = new MappingBase<DmKoubunshoKisokuFile>();
            Params.Add(new DBParameter("DM_KOUBUNSHO_KISOKU_ID", DmKoubunshoKisokuID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DM_KOUBUNSHO_KISOKU_FILE_ID", DmKoubunshoKisokuFileID));
            Params.Add(new DBParameter("UPDATE_DATE", UpdateDate));
            Params.Add(new DBParameter("FILE_NAME", FileName));
            Params.Add(new DBParameter("DATA", Data));
            Params.Add(new DBParameter("DM_KOUBUNSHO_KISOKU_ID", DmKoubunshoKisokuID));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DmKoubunshoKisokuFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("UPDATE_DATE", UpdateDate));
            Params.Add(new DBParameter("FILE_NAME", FileName));
            Params.Add(new DBParameter("DATA", Data));
            Params.Add(new DBParameter("DM_KOUBUNSHO_KISOKU_ID", DmKoubunshoKisokuID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("DM_KOUBUNSHO_KISOKU_FILE_ID", DmKoubunshoKisokuFileID));

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
            Params.Add(new DBParameter("PK", DmKoubunshoKisokuFileID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



