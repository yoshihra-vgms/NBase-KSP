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
    [TableAttribute("MS_PARAMETER")]
    public class MsParameter
    {
        #region データメンバ

        /// <summary>
        /// KEY
        /// </summary>
        [DataMember]
        [ColumnAttribute("KEY", true)]
        public string Key { get; set; }

        /// <summary>
        /// VALUE
        /// </summary>
        [DataMember]
        [ColumnAttribute("VALUE")]
        public string Value { get; set; }
        #endregion

        public static MsParameter GetRecord(MsUser loginUser, string key)
        {
            return GetRecord(null, loginUser, key);
        }
        public static MsParameter GetRecord(DBConnect dbConnect, MsUser loginUser, string key)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsParameter), "GetRecord");

            List<MsParameter> ret = new List<MsParameter>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("KEY", key));

            MappingBase<MsParameter> mapping = new MappingBase<MsParameter>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
    }
}
