using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_SI_KAMOKU")]
    public class MsSiKamoku : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 船員科目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_KAMOKU_ID", true)]
        public int MsSiKamokuId { get; set; }

        
        
        
        /// <summary>
        /// 科目ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KAMOKU_ID")]
        public int MsKamokuId { get; set; }

        
        
        
        /// <summary>
        /// 科目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NAME")]
        public string KamokuName { get; set; }

        /// <summary>
        /// 課税フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("TAX_FLAG")]
        public int TaxFlag { get; set; }
        public enum 税金フラグ { 非課税, 課税 }

        /// <summary>
        /// 費用種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("HIYOU_KIND")]
        public int HiyouKind { get; set; }
        public enum 費用種別 { 船員費用, 全社費用 }
        
        
        
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




        /// <summary>
        /// 勘定科目名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KAMOKU_NAME")]
        public string MsKamokuName { get; set; }
        
        /// <summary>
        /// 内訳科目名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_UTIWAKE_KAMOKU_NAME")]
        public string MsUtiwakeKamokuName { get; set; }
        #endregion


        public static List<MsSiKamoku> GetRecords(MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }
        public static List<MsSiKamoku> GetRecords(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiKamoku), MethodBase.GetCurrentMethod());

            List<MsSiKamoku> ret = new List<MsSiKamoku>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiKamoku> mapping = new MappingBase<MsSiKamoku>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static MsSiKamoku GetRecord(MsUser loginUser, int msSiKamokuId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiKamoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiKamoku), "ByMsSiKamokuID");

            List<MsSiKamoku> ret = new List<MsSiKamoku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiKamoku> mapping = new MappingBase<MsSiKamoku>();
            Params.Add(new DBParameter("MS_SI_KAMOKU_ID", msSiKamokuId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public static MsSiKamoku GetRecordByMsSiKamokuID(MsUser loginUser, int msSiKamokuID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiKamoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiKamoku), "ByMsSiKamokuID");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SI_KAMOKU_ID", msSiKamokuID));
            List<MsSiKamoku> ret = new List<MsSiKamoku>();
            MappingBase<MsSiKamoku> mapping = new MappingBase<MsSiKamoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }


        public static List<MsSiKamoku> SearchRecords(MsUser loginUser, string name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiKamoku), "GetRecords");

            List<MsSiKamoku> ret = new List<MsSiKamoku>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiKamoku> mapping = new MappingBase<MsSiKamoku>();

            if (name != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiKamoku), "SearchByKamokuName");
                Params.Add(new DBParameter("KAMOKU_NAME", "%" + name + "%"));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiKamoku), "OrderBy");

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

            if (!IsNew())
            {
                Params.Add(new DBParameter("MS_SI_KAMOKU_ID", MsSiKamokuId));
            }

            Params.Add(new DBParameter("MS_KAMOKU_ID", MsKamokuId));

            Params.Add(new DBParameter("KAMOKU_NAME", KamokuName));
            Params.Add(new DBParameter("TAX_FLAG", TaxFlag));
            Params.Add(new DBParameter("HIYOU_KIND", HiyouKind));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
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

            Params.Add(new DBParameter("MS_KAMOKU_ID", MsKamokuId));

            Params.Add(new DBParameter("KAMOKU_NAME", KamokuName));
            Params.Add(new DBParameter("TAX_FLAG", TaxFlag));
            Params.Add(new DBParameter("HIYOU_KIND", HiyouKind));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SI_KAMOKU_ID", MsSiKamokuId));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsSiKamokuId));

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
            Params.Add(new DBParameter("PK", MsSiKamokuId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return MsSiKamokuId == 0;
        }


        public override string ToString()
        {
            return KamokuName;
        }
    }
}
