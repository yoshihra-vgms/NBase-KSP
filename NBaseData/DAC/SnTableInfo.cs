using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;

namespace NBaseData.DAC
{
    [DataContract()]
    public class SnTableInfo
    {
        #region データメンバ
        /// <summary>
        /// テーブル名.
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }
        #endregion

        public SnTableInfo()
        {
        }

        public static List<SnTableInfo> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SnTableInfo), MethodBase.GetCurrentMethod());
            List<SnTableInfo> ret = new List<SnTableInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SnTableInfo> mapping = new MappingBase<SnTableInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static SnTableInfo GetRecord(NBaseData.DAC.MsUser loginUser, string id)
        {
            return null;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SnTableInfo), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("Name", Name));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return false;
        }
    }
}
