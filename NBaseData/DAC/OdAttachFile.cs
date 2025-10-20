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
    [TableAttribute("OD_ATTACH_FILE")]
    public class OdAttachFile : ISyncTable, IGenericCloneable<OdAttachFile>
    {
        #region データメンバ
        /// <summary>
        /// 添付ファイルID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_ATTACH_FILE_ID", true)]
        public string OdAttachFileID { get; set; }
        
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

        public OdAttachFile()
        {
        }

        public static OdAttachFile GetRecord(NBaseData.DAC.MsUser loginUser, string OdAttachFileID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "ByOdAttachFileID");
            List<OdAttachFile> ret = new List<OdAttachFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdAttachFile> mapping = new MappingBase<OdAttachFile>();
            Params.Add(new DBParameter("OD_ATTACH_FILE_ID", OdAttachFileID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static OdAttachFile GetRecordNoData(NBaseData.DAC.MsUser loginUser, string OdAttachFileID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "ByOdAttachFileID");
            List<OdAttachFile> ret = new List<OdAttachFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdAttachFile> mapping = new MappingBase<OdAttachFile>();
            Params.Add(new DBParameter("OD_ATTACH_FILE_ID", OdAttachFileID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;

            OdAttachFile retAttachFile = ret[0];
            retAttachFile.Data = null;
            return retAttachFile;
        }

        public static List<OdAttachFile> GetRecordsByOdThiId(NBaseData.DAC.MsUser loginUser, string odThiId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "ByOdThiId");
            List<OdAttachFile> ret = new List<OdAttachFile>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ID", odThiId));
            MappingBase<OdAttachFile> mapping = new MappingBase<OdAttachFile>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //public static List<OdAttachFile> GetRecordsByOdMmId(NBaseData.DAC.MsUser loginUser, string odMmId)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "GetRecords");
        //    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "ByOdMmID");
        //    List<OdAttachFile> ret = new List<OdAttachFile>();
        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("OD_MM_ID", odMmId));
        //    MappingBase<OdAttachFile> mapping = new MappingBase<OdAttachFile>();
        //    ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

        //    return ret;
        //}

        //public static List<OdAttachFile> GetRecordsByOdMkId(NBaseData.DAC.MsUser loginUser, string odMkId)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "GetRecords");
        //    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "ByOdMkID");
        //    List<OdAttachFile> ret = new List<OdAttachFile>();
        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("OD_MK_ID", odMkId));
        //    MappingBase<OdAttachFile> mapping = new MappingBase<OdAttachFile>();
        //    ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

        //    return ret;
        //}

        public static List<OdAttachFile> GetFileNotFoundRecords(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "GetTargetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), "AndFileNotFoundRecords");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));

            List<OdAttachFile> ret = new List<OdAttachFile>();
            MappingBase<OdAttachFile> mapping = new MappingBase<OdAttachFile>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_ATTACH_FILE_ID", OdAttachFileID));
            Params.Add(new DBParameter("FILE_NAME", FileName));
            Params.Add(new DBParameter("DATA", Data));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdAttachFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("FILE_NAME", FileName));
            Params.Add(new DBParameter("DATA", Data));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("OD_ATTACH_FILE_ID", OdAttachFileID));

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
            Params.Add(new DBParameter("PK", OdAttachFileID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        #region IGenericCloneable<OdAttachFile> メンバ

        public OdAttachFile Clone()
        {
            OdAttachFile clone = new OdAttachFile();

            clone.OdAttachFileID = OdAttachFileID;
            clone.FileName = FileName;
            clone.Data = Data;
            clone.SendFlag = SendFlag;
            clone.VesselID = VesselID;
            clone.DataNo = DataNo;
            clone.UserKey = UserKey;
            clone.RenewDate = RenewDate;
            clone.RenewUserID = RenewUserID;
            clone.Ts = Ts;
            clone.DeleteFlag = DeleteFlag;

            return clone;
        }

        #endregion
    }
}



