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
    [TableAttribute("SN_PARAMETER")]
    public class SnParameter : ISyncTable
    {
        #region データメンバ
        [DataMember]
        [ColumnAttribute("PRM_1")]
        public string Prm1 { get; set; }

        [DataMember]
        [ColumnAttribute("PRM_2")]
        public string Prm2 { get; set; }

        [DataMember]
        [ColumnAttribute("PRM_3")]
        public string Prm3 { get; set; }

        [DataMember]
        [ColumnAttribute("PRM_4")]
        public string Prm4 { get; set; }

        [DataMember]
        [ColumnAttribute("PRM_5")]
        public string Prm5 { get; set; }

        [DataMember]
        [ColumnAttribute("PRM_6")]
        public string Prm6 { get; set; }

        [DataMember]
        [ColumnAttribute("PRM_7")]
        public string Prm7 { get; set; }

        [DataMember]
        [ColumnAttribute("PRM_8")]
        public string Prm8 { get; set; }

        [DataMember]
        [ColumnAttribute("PRM_9")]
        public string Prm9 { get; set; }

        [DataMember]
        [ColumnAttribute("PRM_10")]
        public string Prm10 { get; set; }



        [DataMember]
        [ColumnAttribute("MAINTENANCE_FLAG")]
        public int MaintenanceFlag { get; set; }

        [DataMember]
        [ColumnAttribute("MAINTENANCE_MESSAGE")]
        public string MaintenanceMessage { get; set; }

        [DataMember]
        [ColumnAttribute("RELEASE_VERSION")]
        public string ReleaseVersion { get; set; }






        #region 共通項目
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

        public SnParameter()
        {
        }

        public static SnParameter GetRecord(NBaseData.DAC.MsUser loginUser)
        {
            return GetRecord(null, loginUser);
        }
        public static SnParameter GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SnParameter), MethodBase.GetCurrentMethod());
            List<SnParameter> ret = new List<SnParameter>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SnParameter> mapping = new MappingBase<SnParameter>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SnParameter), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PRM_1", Prm1));
            Params.Add(new DBParameter("PRM_2", Prm2));
            Params.Add(new DBParameter("PRM_3", Prm3));
            Params.Add(new DBParameter("PRM_4", Prm4));
            Params.Add(new DBParameter("PRM_5", Prm5));
            Params.Add(new DBParameter("PRM_6", Prm6));
            Params.Add(new DBParameter("PRM_7", Prm7));
            Params.Add(new DBParameter("PRM_8", Prm8));
            Params.Add(new DBParameter("PRM_9", Prm9));
            Params.Add(new DBParameter("PRM_10", Prm10));

            Params.Add(new DBParameter("MAINTENANCE_FLAG", MaintenanceFlag));
            Params.Add(new DBParameter("MAINTENANCE_MESSAGE", MaintenanceMessage));
            Params.Add(new DBParameter("RELEASE_VERSION", ReleaseVersion));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SnParameter), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PRM_1", Prm1));
            Params.Add(new DBParameter("PRM_2", Prm2));
            Params.Add(new DBParameter("PRM_3", Prm3));
            Params.Add(new DBParameter("PRM_4", Prm4));
            Params.Add(new DBParameter("PRM_5", Prm5));
            Params.Add(new DBParameter("PRM_6", Prm6));
            Params.Add(new DBParameter("PRM_7", Prm7));
            Params.Add(new DBParameter("PRM_8", Prm8));
            Params.Add(new DBParameter("PRM_9", Prm9));
            Params.Add(new DBParameter("PRM_10", Prm10));

            Params.Add(new DBParameter("MAINTENANCE_FLAG", MaintenanceFlag));
            Params.Add(new DBParameter("MAINTENANCE_MESSAGE", MaintenanceMessage));
            Params.Add(new DBParameter("RELEASE_VERSION", ReleaseVersion));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
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
        //    SnParameter sp = SnParameter.GetRecord(loginUser);

        //    return sp != null ? true : false;
        //}
        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            SnParameter sp = SnParameter.GetRecord(dbConnect, loginUser);

            return sp != null ? true : false;
        }

        #endregion
    }
}
