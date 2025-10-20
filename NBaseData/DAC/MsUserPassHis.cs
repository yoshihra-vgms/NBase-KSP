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
    [TableAttribute("MS_USER_PASS_HIS")]
    public class MsUserPassHis
    {
        #region データメンバ
        [DataMember]
        [ColumnAttribute("MS_USER_PASS_HIS_ID", true)]
        public string MsUserPassHisID { get; set; }

        [DataMember]
        [ColumnAttribute("MS_USER_ID", true)]
        public string MsUserID { get; set; }

        [DataMember]
        [ColumnAttribute("PASSWORD", true)]
        public string Password { get; set; }

        [DataMember]
        [ColumnAttribute("SEND_FLAG", true)]
        public int SendFlag { get; set; }

        [DataMember]
        [ColumnAttribute("VESSEL_ID", true)]
        public int VesselID { get; set; }

        [DataMember]
        [ColumnAttribute("DATA_NO", true)]
        public Int64 DataNo { get; set; }

        [DataMember]
        [ColumnAttribute("USER_KEY", true)]
        public string UserKey { get; set; }

        [DataMember]
        [ColumnAttribute("TS", true)]
        public string TS { get; set; }

        [DataMember]
        [ColumnAttribute("RENEW_DATE", true)]
        public DateTime RenewDate { get; set; }

        [DataMember]
        [ColumnAttribute("RENEW_USER_ID", true)]
        public string RenewUserID { get; set; }


        #endregion

        public static List<MsUserPassHis> GetRecords(MsUser loginUser, MsUser ChangeUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUserPassHis), MethodBase.GetCurrentMethod());
            List<MsUserPassHis> ret = new List<MsUserPassHis>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUserPassHis> mapping = new MappingBase<MsUserPassHis>();
            Params.Add(new DBParameter("MS_USER_ID", ChangeUser.MsUserID));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsUserPassHis GetRecordByMaxDate(string loginUserID, string ChangeUserID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUserPassHis), MethodBase.GetCurrentMethod());
            List<MsUserPassHis> ret = new List<MsUserPassHis>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUserPassHis> mapping = new MappingBase<MsUserPassHis>();
            Params.Add(new DBParameter("MS_USER_ID", ChangeUserID));

            ret = mapping.FillRecrods(loginUserID, SQL, Params);

            if (ret.Count > 0)
            {
                return ret[0];
            }

            return null;
        }


        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsUserPassHis), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_USER_PASS_HIS_ID", MsUserPassHisID));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
            Params.Add(new DBParameter("PASSWORD", Password));
            Params.Add(new DBParameter("SEND_FLAG", 0));
            Params.Add(new DBParameter("VESSEL_ID", 0));
            Params.Add(new DBParameter("USER_KEY", "1"));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));

            DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            return true;
        }
    }
}
