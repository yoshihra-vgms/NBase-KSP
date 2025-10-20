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

namespace NBaseData.DAC
{
    [DataContract()]
    public class MsVesselType
    {
        #region データメンバ
        /// <summary>
        /// 船タイプID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_TYPE_ID")]
        public string MsVesselTypeID { get; set; }

        /// <summary>
        /// 船タイプ名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_TYPE_NAME")]
        public string VesselTypeName { get; set; }

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
        public String RenewUserID { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }

        /// <summary>
        /// 配乗計画タイプ
        /// </summary>
        [DataMember]
        [ColumnAttribute("PLAN_TYPE")]
        public int PlanType { get; set; }

        #endregion

        //public const string MS_VESSEL_TYPE_内航RORO_ID = "1";
        //public const string MS_VESSEL_TYPE_内航不定期_ID = "2";
        //public const string MS_VESSEL_TYPE_フェリー_ID = "3";
        //public const string MS_VESSEL_TYPE_外航_ID = "4";
        //public const string MS_VESSEL_TYPE_その他_ID = "5";

        public override string ToString()
        {
            return VesselTypeName;
        }

        public MsVesselType()
        {
        }

        public static List<MsVesselType> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselType), MethodBase.GetCurrentMethod());
            List<MsVesselType> ret = new List<MsVesselType>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselType> mapping = new MappingBase<MsVesselType>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsVesselType GetRecord(NBaseData.DAC.MsUser loginUser, string MsVesselTypeID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselType), MethodBase.GetCurrentMethod());
            List<MsVesselType> ret = new List<MsVesselType>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselType> mapping = new MappingBase<MsVesselType>();
            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", MsVesselTypeID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselType), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", MsVesselTypeID));
            Params.Add(new DBParameter("VESSEL_TYPE_NAME", VesselTypeName));
            Params.Add(new DBParameter("PLAN_TYPE", PlanType));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselType), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VESSEL_TYPE_NAME", VesselTypeName));
            Params.Add(new DBParameter("PLAN_TYPE", PlanType));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", MsVesselTypeID));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
