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
    [TableAttribute("MS_NYUKYO_KAMOKU")]
    public class MsNyukyoKamoku : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 入渠科目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_NYUKYO_KAMOKU_ID", true)]
        public string MsNyukyoKamokuID { get; set; }

        /// <summary>
        /// 入渠科目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NYUKYO_KAMOKU_NAME")]
        public string NyukyoKamokuName { get; set; }

        /// <summary>
        /// 科目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KAMOKU_ID")]
        public int MsKamokuID { get; set; }

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
        /// 表示順序
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }
        #endregion

        public override string ToString()
        {
            return NyukyoKamokuName;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsNyukyoKamoku()
        {
        }

        public static List<MsNyukyoKamoku> GetRecords(MsUser loginUser)
        {
            return GetRecords( null, loginUser);
        }
        public static List<MsNyukyoKamoku> GetRecords(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsNyukyoKamoku), MethodBase.GetCurrentMethod());
            List<MsNyukyoKamoku> ret = new List<MsNyukyoKamoku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsNyukyoKamoku> mapping = new MappingBase<MsNyukyoKamoku>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsNyukyoKamoku GetRecord(MsUser loginUser, string MsNyukyoKamokuID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsNyukyoKamoku), MethodBase.GetCurrentMethod());
            List<MsNyukyoKamoku> ret = new List<MsNyukyoKamoku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsNyukyoKamoku> mapping = new MappingBase<MsNyukyoKamoku>();
            Params.Add(new DBParameter("MS_NYUKYO_KAMOKU_ID", MsNyukyoKamokuID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsNyukyoKamoku), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_NYUKYO_KAMOKU_ID", MsNyukyoKamokuID));
            Params.Add(new DBParameter("NYUKYO_KAMOKU_NAME", NyukyoKamokuName));
            Params.Add(new DBParameter("MS_KAMOKU_ID", MsKamokuID));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsNyukyoKamoku), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("NYUKYO_KAMOKU_NAME", NyukyoKamokuName));
            Params.Add(new DBParameter("MS_KAMOKU_ID", MsKamokuID));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_NYUKYO_KAMOKU_ID", MsNyukyoKamokuID));

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
        //    Params.Add(new DBParameter("PK", MsNyukyoKamokuID));

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
            Params.Add(new DBParameter("PK", MsNyukyoKamokuID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
