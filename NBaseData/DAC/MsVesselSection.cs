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
    public class MsVesselSection
    {
        #region データメンバ
        /// <summary>
        /// 船管轄部門ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_SECTION_ID")]
        public string MsVesselSectionID { get; set; }

        /// <summary>
        /// 管轄部門名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_SECTION_NAME")]
        public string VesselSectionName { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

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

        #endregion

        public override string ToString()
        {
            return VesselSectionName;
        }

        public MsVesselSection()
        {
        }

        public static List<MsVesselSection> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselSection), MethodBase.GetCurrentMethod());
            List<MsVesselSection> ret = new List<MsVesselSection>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselSection> mapping = new MappingBase<MsVesselSection>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsVesselSection GetRecord(NBaseData.DAC.MsUser loginUser, string MsVesselSectionID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselSection), MethodBase.GetCurrentMethod());
            List<MsVesselSection> ret = new List<MsVesselSection>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselSection> mapping = new MappingBase<MsVesselSection>();
            Params.Add(new DBParameter("MS_VESSEL_SECTION_ID", MsVesselSectionID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselSection), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_SECTION_ID", MsVesselSectionID));
            Params.Add(new DBParameter("VESSEL_SECTION_NAME", VesselSectionName));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselSection), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VESSEL_SECTION_NAME", VesselSectionName));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_VESSEL_SECTION_ID", MsVesselSectionID));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
