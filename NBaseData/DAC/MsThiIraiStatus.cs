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
    [TableAttribute("MS_THI_IRAI_STATUS")]
    public class MsThiIraiStatus : ISyncTable
    {
        public enum THI_IRAI_STATUS { 未対応, 見積中, 発注済, 受領済, 完了 };
 
       private static readonly Dictionary<string, string> idToName =
                                                            new Dictionary<string, string>
                                                                {{((int)THI_IRAI_STATUS.未対応).ToString(), THI_IRAI_STATUS.未対応.ToString()},
                                                                 {((int)THI_IRAI_STATUS.見積中).ToString(), THI_IRAI_STATUS.見積中.ToString()},
                                                                 {((int)THI_IRAI_STATUS.発注済).ToString(), THI_IRAI_STATUS.発注済.ToString()}, 
                                                                 {((int)THI_IRAI_STATUS.受領済).ToString(), THI_IRAI_STATUS.受領済.ToString()}, 
                                                                 {((int)THI_IRAI_STATUS.完了).ToString(), THI_IRAI_STATUS.完了.ToString()}, 
                                                                };

        public static string ToName(string id)
        {
            return idToName[id];
        }

        public static string ToId(THI_IRAI_STATUS en)
        {
            return ((int)en).ToString();
        }

        #region データメンバ

        /// <summary>
        /// ステータスID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_STATUS_ID", true)]
        public string MsThiIraiStatusID { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("ORDER_THI_IRAI_STATUS")]
        public string OrderThiIraiStatus { get; set; }

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
        /// 同期:データNo
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

        public override string ToString()
        {
            return OrderThiIraiStatus;
        }
        public static List<MsThiIraiStatus> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiStatus), MethodBase.GetCurrentMethod());
            List<MsThiIraiStatus> ret = new List<MsThiIraiStatus>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsThiIraiStatus> mapping = new MappingBase<MsThiIraiStatus>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsThiIraiStatus GetRecord(NBaseData.DAC.MsUser loginUser, string id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiStatus), MethodBase.GetCurrentMethod());
            List<MsThiIraiStatus> ret = new List<MsThiIraiStatus>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsThiIraiStatus> mapping = new MappingBase<MsThiIraiStatus>();
            Params.Add(new DBParameter("MS_THI_IRAI_STATUS_ID", id));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiStatus), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_THI_IRAI_STATUS_ID", MsThiIraiStatusID));
            Params.Add(new DBParameter("ORDER_THI_IRAI_STATUS", OrderThiIraiStatus));
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

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiStatus), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_THI_IRAI_STATUS_ID", MsThiIraiStatusID));
            Params.Add(new DBParameter("ORDER_THI_IRAI_STATUS", OrderThiIraiStatus));
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

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsThiIraiStatusID));

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
            Params.Add(new DBParameter("PK", MsThiIraiStatusID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
