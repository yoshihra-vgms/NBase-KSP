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
    public class MsVesselCategory
    {
        #region データメンバ
        /// <summary>
        /// 船タイプID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_CATEGORY_ID")]
        public string MsVesselCategoryID { get; set; }

        /// <summary>
        /// 船タイプ名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_CATEGORY_NAME")]
        public string VesselCategoryName { get; set; }
        
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
            return VesselCategoryName;
        }

        public MsVesselCategory()
        {
        }

        public static List<MsVesselCategory> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselCategory), MethodBase.GetCurrentMethod());
            List<MsVesselCategory> ret = new List<MsVesselCategory>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselCategory> mapping = new MappingBase<MsVesselCategory>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsVesselCategory GetRecord(NBaseData.DAC.MsUser loginUser, string MsVesselCategoryID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselCategory), MethodBase.GetCurrentMethod());
            List<MsVesselCategory> ret = new List<MsVesselCategory>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselCategory> mapping = new MappingBase<MsVesselCategory>();
            Params.Add(new DBParameter("MS_VESSEL_CATEGORY_ID", MsVesselCategoryID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselCategory), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_CATEGORY_ID", MsVesselCategoryID));
            Params.Add(new DBParameter("VESSEL_CATEGORY_NAME", VesselCategoryName));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselCategory), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VESSEL_CATEGORY_NAME", VesselCategoryName));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_VESSEL_CATEGORY_ID", MsVesselCategoryID));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
