using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ORMapping;
using System.Runtime.Serialization;
using ORMapping.Attrs;

namespace NBaseData.DAC
{
    [DataContract()]
    public class MsShinseiTantou
    {
        public static MsUser GetShinseiTantou(MsUser loginUser, string msUserID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShinseiTantou), MethodBase.GetCurrentMethod());
            List<MsUser> ret = new List<MsUser>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsUser> mapping = new MappingBase<MsUser>();
            Params.Add(new DBParameter("MS_USER_ID", msUserID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
    }
}
