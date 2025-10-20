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
    [TableAttribute("SI_SHOBATSU_ATTACH_FILE")]
    public class SiShobatsuAttachFile : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 賞罰添付ファイルID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SHOBATSU_ATTACH_FILE_ID", true)]
        public string SiShobatsuAttachFileID { get; set; }

        /// <summary>
        /// ファイル更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }
        
        /// <summary>
        /// 添付ファイル名
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
        /// 免許/免状ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SHOBATSU_ID")]
        public string SiShobatsuID { get; set; }


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

        public override string ToString()
        {
            if (FileName != null)
            {
                return FileName;
            }
            else
            {
                return base.ToString();
            }
        }

        public SiShobatsuAttachFile()
        {
        }

        public static List<SiShobatsuAttachFile> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiShobatsuAttachFile), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiShobatsuAttachFile), "OrderBy");
            List<SiShobatsuAttachFile> ret = new List<SiShobatsuAttachFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiShobatsuAttachFile> mapping = new MappingBase<SiShobatsuAttachFile>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiShobatsuAttachFile> GetRecordByKoushuID(NBaseData.DAC.MsUser loginUser, string SiShobatsuID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiShobatsuAttachFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiShobatsuAttachFile), "BySiShobatsuID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiShobatsuAttachFile), "OrderBy");
            List<SiShobatsuAttachFile> ret = new List<SiShobatsuAttachFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiShobatsuAttachFile> mapping = new MappingBase<SiShobatsuAttachFile>();
            Params.Add(new DBParameter("SI_SHOBATSU_ID", SiShobatsuID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiShobatsuAttachFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_SHOBATSU_ATTACH_FILE_ID", SiShobatsuAttachFileID));
            Params.Add(new DBParameter("UPDATE_DATE", UpdateDate));
            Params.Add(new DBParameter("FILE_NAME", FileName));
            Params.Add(new DBParameter("DATA", Data));
            Params.Add(new DBParameter("SI_SHOBATSU_ID", SiShobatsuID));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiShobatsuAttachFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("UPDATE_DATE", UpdateDate));
            Params.Add(new DBParameter("FILE_NAME", FileName));
            Params.Add(new DBParameter("DATA", Data));
            Params.Add(new DBParameter("SI_SHOBATSU_ID", SiShobatsuID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SI_SHOBATSU_ATTACH_FILE_ID", SiShobatsuAttachFileID));

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
            Params.Add(new DBParameter("PK", SiShobatsuAttachFileID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



