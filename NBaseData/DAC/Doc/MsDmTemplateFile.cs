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
    [TableAttribute("MS_DM_TEMPLATE_FILE")]
    public class MsDmTemplateFile : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 報告書雛形ファイルID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_TEMPLATE_FILE_ID", true)]
        public string MsDmTemplateFileID { get; set; }

        /// <summary>
        /// ファイル更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }
        
        /// <summary>
        /// 雛形ファイル名
        /// </summary>
        [DataMember]
        [ColumnAttribute("TEMPLATE_FILE_NAME")]
        public string TemplateFileName { get; set; }
        
        /// <summary>
        /// データ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA")]
        public byte[] Data { get; set; }
       
        /// <summary>
        /// 報告書ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_HOUKOKUSHO_ID", true)]
        public string MsDmHoukokushoID { get; set; }


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

        public MsDmTemplateFile()
        {
        }

        public static List<MsDmTemplateFile> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmTemplateFile), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmTemplateFile), "OrderBy");
            List<MsDmTemplateFile> ret = new List<MsDmTemplateFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDmTemplateFile> mapping = new MappingBase<MsDmTemplateFile>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsDmTemplateFile GetRecord(NBaseData.DAC.MsUser loginUser, string MsDmTemplateFileID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmTemplateFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmTemplateFile), "ByMsDmTemplateFileID");
            List<MsDmTemplateFile> ret = new List<MsDmTemplateFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDmTemplateFile> mapping = new MappingBase<MsDmTemplateFile>();
            Params.Add(new DBParameter("MS_DM_TEMPLATE_FILE_ID", MsDmTemplateFileID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static MsDmTemplateFile GetRecordByHoukokushoID(NBaseData.DAC.MsUser loginUser, string MsDmHoukokushoID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmTemplateFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmTemplateFile), "ByMsDmHoukokushoID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmTemplateFile), "OrderBy");
            List<MsDmTemplateFile> ret = new List<MsDmTemplateFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDmTemplateFile> mapping = new MappingBase<MsDmTemplateFile>();
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", MsDmHoukokushoID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmTemplateFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DM_TEMPLATE_FILE_ID", MsDmTemplateFileID));
            Params.Add(new DBParameter("UPDATE_DATE", UpdateDate));
            Params.Add(new DBParameter("TEMPLATE_FILE_NAME", TemplateFileName));
            Params.Add(new DBParameter("DATA", Data));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", MsDmHoukokushoID));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmTemplateFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("UPDATE_DATE", UpdateDate));
            Params.Add(new DBParameter("TEMPLATE_FILE_NAME", TemplateFileName));
            Params.Add(new DBParameter("DATA", Data));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", MsDmHoukokushoID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_DM_TEMPLATE_FILE_ID", MsDmTemplateFileID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public static bool LogicalDelete(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string msDmHoukokushoID, string msDmTemplateFileID)
        {
            int cnt = 0;
            string SQL = "";
            ParameterConnection Params = new ParameterConnection();

            if (msDmTemplateFileID != "")
            {
                // ＩＤ指定がある場合、その雛形ファイルは除いて削除フラグをたてる
                SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmTemplateFile), "LogicalDelete1");
                Params.Add(new DBParameter("MS_DM_TEMPLATE_FILE_ID", msDmTemplateFileID));
            }
            else
            {
                // ＩＤ指定がない場合、当該報告書に紐づいている雛形ファイルすべてに削除フラグをたてる
                SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmTemplateFile), "LogicalDelete2");
            }
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", msDmHoukokushoID));
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsDmTemplateFileID));

        //    int count = Convert.ToInt32(DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params));

        //    return count > 0 ? true : false;
        //}
        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsDmTemplateFileID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



