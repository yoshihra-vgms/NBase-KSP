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
    [TableAttribute("MS_THI_IRAI_SBT")]
    public class MsThiIraiSbt : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 手配依頼種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_SBT_ID", true)]
        public string MsThiIraiSbtID { get; set; }

        /// <summary>
        /// 手配依頼種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_IRAI_SBT_NAME")]
        public string ThiIraiSbtName { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }

        /// <summary>
        /// 送信フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        /// <summary>
        /// データNo
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
        /// 更新者(UserID)
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

        public enum ThiIraiSbtEnum { 修繕 = 1, 燃料_潤滑油, 船用品 };
        private static readonly Dictionary<string, string> idToName =
                                                            new Dictionary<string, string>
                                                                {{((int)ThiIraiSbtEnum.修繕).ToString(), ThiIraiSbtEnum.修繕.ToString()},
                                                                 {((int)ThiIraiSbtEnum.燃料_潤滑油).ToString(), ThiIraiSbtEnum.燃料_潤滑油.ToString()},
                                                                 {((int)ThiIraiSbtEnum.船用品).ToString(), ThiIraiSbtEnum.船用品.ToString()},
                                                                };

        public static string ToName(string id)
        {
            return idToName[id];
        }

        public static string ToId(ThiIraiSbtEnum en)
        {
            return ((int)en).ToString();
        }
        
        public override string ToString()
        {
            return ThiIraiSbtName;
        }

        public MsThiIraiSbt()
        {
        }

        public static List<MsThiIraiSbt> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiSbt), MethodBase.GetCurrentMethod());
            List<MsThiIraiSbt> ret = new List<MsThiIraiSbt>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsThiIraiSbt> mapping = new MappingBase<MsThiIraiSbt>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsThiIraiSbt GetRecord(MsUser loginUser, string id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiSbt), MethodBase.GetCurrentMethod());
            List<MsThiIraiSbt> ret = new List<MsThiIraiSbt>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsThiIraiSbt> mapping = new MappingBase<MsThiIraiSbt>();
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", id));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiSbt), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbtID));
            Params.Add(new DBParameter("THI_IRAI_SBT_NAME", ThiIraiSbtName));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }

        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiSbt), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("THI_IRAI_SBT_NAME", ThiIraiSbtName));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbtID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsThiIraiSbtID));

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
            Params.Add(new DBParameter("PK", MsThiIraiSbtID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
