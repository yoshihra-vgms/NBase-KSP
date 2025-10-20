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
    [TableAttribute("SI_KENSHIN_ATTACH_FILE")]
    public class SiKenshinAttachFile : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 講習添付ファイルID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_KENSHIN_ATTACH_FILE_ID", true)]
        public string SiKenshinAttachFileID { get; set; }

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
        [ColumnAttribute("SI_KENSHIN_ID")]
        public string SiKenshinID { get; set; }


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

        public SiKenshinAttachFile()
        {
        }

        public static SiKenshinAttachFile GetRecord(NBaseData.DAC.MsUser loginUser, string SiKenshinAttachFileID)
        {
            return GetRecord(null, loginUser, SiKenshinAttachFileID);
        }

        public static SiKenshinAttachFile GetRecord(ORMapping.DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string SiKenshinAttachFileID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), "By有効データ");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), "BySiKenshinAttachFileID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), "OrderBy");
            List<SiKenshinAttachFile> ret = new List<SiKenshinAttachFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKenshinAttachFile> mapping = new MappingBase<SiKenshinAttachFile>();
            Params.Add(new DBParameter("SI_KENSHIN_ATTACH_FILE_ID", SiKenshinAttachFileID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret != null)
                return ret.First();
            else
                return null;
        }

        public static List<SiKenshinAttachFile> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), "OrderBy");
            List<SiKenshinAttachFile> ret = new List<SiKenshinAttachFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKenshinAttachFile> mapping = new MappingBase<SiKenshinAttachFile>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<SiKenshinAttachFile> GetRecordByKensinID(NBaseData.DAC.MsUser loginUser, string SiKenshinID)
        {
            return GetRecordByKensinID(null, loginUser, SiKenshinID);
        }

            public static List<SiKenshinAttachFile> GetRecordByKensinID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string SiKenshinID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), "By有効データ");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), "BySiKenshinID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), "OrderBy");
            List<SiKenshinAttachFile> ret = new List<SiKenshinAttachFile>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKenshinAttachFile> mapping = new MappingBase<SiKenshinAttachFile>();
            Params.Add(new DBParameter("SI_KENSHIN_ID", SiKenshinID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_KENSHIN_ATTACH_FILE_ID", SiKenshinAttachFileID));
            Params.Add(new DBParameter("UPDATE_DATE", UpdateDate));
            Params.Add(new DBParameter("FILE_NAME", FileName));
            Params.Add(new DBParameter("DATA", Data));
            Params.Add(new DBParameter("SI_KENSHIN_ID", SiKenshinID));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKenshinAttachFile), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("UPDATE_DATE", UpdateDate));
            Params.Add(new DBParameter("FILE_NAME", FileName));
            Params.Add(new DBParameter("DATA", Data));
            Params.Add(new DBParameter("SI_KENSHIN_ID", SiKenshinID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SI_KENSHIN_ATTACH_FILE_ID", SiKenshinAttachFileID));

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
            Params.Add(new DBParameter("PK", SiKenshinAttachFileID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool Edited(DBConnect dbConnect, MsUser loginUser)
        {
            SiKenshinAttachFile org = SiKenshinAttachFile.GetRecord(dbConnect, loginUser, SiKenshinAttachFileID);
            if (org == null)
                return true;

            return org.Equals(this) == false;
        }

        public bool Equals(object obj)
        {
            bool ret = true;
            SiKenshinAttachFile dst = obj as SiKenshinAttachFile;

            if (this.UpdateDate != dst.UpdateDate ||
                this.FileName != dst.FileName ||
                this.SiKenshinID != dst.SiKenshinID ||
                this.DeleteFlag != dst.DeleteFlag
                )
            {
                ret = false;
            }

            if (ret != false)
            {
                if (this.Data != null && dst.Data != null)
                {
                    if (!(System.Linq.Enumerable.SequenceEqual(this.Data, dst.Data)))//配列の比較に変更 2016/08/26
                    {
                        ret = false;
                    }
                }
                else if (this.Data == null && dst.Data != null)
                {
                    ret = false;
                }
                else if (this.Data != null && dst.Data == null)
                {
                    ret = false;
                }
            }

            return ret;
        }
    }
}



