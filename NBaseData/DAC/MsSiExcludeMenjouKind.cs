using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using System.Reflection;
using ORMapping;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_SI_EXCLUDE_MENJOU_KIND")]
    public class MsSiExcludeMenjouKind : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 免許／免状種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_KIND_ID", true)]
        public int MsSiMenjouKindID { get; set; }
        
        /// <summary>
        /// 未取得除外対象とする免許／免状種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("EXCLUDE_MENJOU_KIND_ID", true)]
        public int ExcludeMenjouKindID { get; set; }
        
        
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        /// <summary>
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

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


        /// <summary>
        /// 新規フラグ
        /// </summary>
        [DataMember]
        public bool NewFlag { get; set; }

        #endregion


        public MsSiExcludeMenjouKind()
        {
            NewFlag = false;
        }

        public static List<MsSiExcludeMenjouKind> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiExcludeMenjouKind), MethodBase.GetCurrentMethod());

            List<MsSiExcludeMenjouKind> ret = new List<MsSiExcludeMenjouKind>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiExcludeMenjouKind> mapping = new MappingBase<MsSiExcludeMenjouKind>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsSiExcludeMenjouKind> GetRecordsByMsSiMenjouKindID(MsUser loginUser, int msSiMenjouKindID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiExcludeMenjouKind), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiExcludeMenjouKind), "ByMsSiMenjouKindID");
            
            List<MsSiExcludeMenjouKind> ret = new List<MsSiExcludeMenjouKind>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", msSiMenjouKindID));
            MappingBase<MsSiExcludeMenjouKind> mapping = new MappingBase<MsSiExcludeMenjouKind>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
                        
            return ret;
        }

        public static List<MsSiExcludeMenjouKind> GetRecordsByExcludeMenjouKindID(MsUser loginUser, int ExcludeMenjouKindID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiExcludeMenjouKind), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiExcludeMenjouKind), "ByExcludeMenjouKindID");

            List<MsSiExcludeMenjouKind> ret = new List<MsSiExcludeMenjouKind>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("EXCLUDE_MENJOU_KIND_ID", ExcludeMenjouKindID));
            MappingBase<MsSiExcludeMenjouKind> mapping = new MappingBase<MsSiExcludeMenjouKind>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", MsSiMenjouKindID));
            Params.Add(new DBParameter("EXCLUDE_MENJOU_KIND_ID", ExcludeMenjouKindID));
           
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", MsSiMenjouKindID));
            Params.Add(new DBParameter("EXCLUDE_MENJOU_KIND_ID", ExcludeMenjouKindID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        //public bool Exists(MsUser loginUser)
        //{
        //    return Exists(null, loginUser);
        //}
        //public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsSiMenjouKindID));
        //    Params.Add(new DBParameter("PK", ExcludeMenjouKindID));

        //    int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

        //    return count > 0 ? true : false;
        //}
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            return true;
        }

        #endregion

        public bool IsNew()
        {
            return NewFlag;
        }
    }
}
