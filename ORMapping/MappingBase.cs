using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.PostgreSql;
//using ORMapping.Oracle;
//using ORMapping.SqlServer;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Data.Common;

namespace ORMapping
{
    [DataContract()]
    public class MappingBase<T> where T : new()
    {
        public List<T> FillRecrods2(string connectionString, string UserName, string SQL, ParameterConnection Params)
        {
            PostgreSqlCore<T> core = new PostgreSqlCore<T>();
            return core.FillRecrods(connectionString, UserName, SQL, Params);
        }

        public List<T> FillRecrods(string UserName, string SQL, ParameterConnection Params)
        {
            return FillRecrods(null, UserName, SQL, Params);
        }   
        
        public List<T> FillRecrods(DBConnect dbConnect, string UserName, string SQL, ParameterConnection Params)
        {
            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    PostgreSqlCore<T> core = new PostgreSqlCore<T>();

            //    if (dbConnect == null)
            //    {
            //        return core.FillRecrods(UserName, SQL, Params);
            //    }
            //    else
            //    {
            //        return core.FillRecrods(dbConnect, UserName, SQL, Params);
            //    }
            //}
            //else if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902add NBaseHonsenのPostgresql対応
            //{
            //    #region 処理はDB_TYPE.POSTGRESQLと同じ
            //    PostgreSqlCore<T> core = new PostgreSqlCore<T>();

            //    if (dbConnect == null)
            //    {
            //        return core.FillRecrods(UserName, SQL, Params);
            //    }
            //    else
            //    {
            //        return core.FillRecrods(dbConnect, UserName, SQL, Params);
            //    }
            //    #endregion
            //}
            //else
            //{
            //    SqlServerCore2<T> SqlServerCore = new SqlServerCore2<T>();

            //    if (dbConnect == null)
            //    {
            //        return SqlServerCore.FillRecrods(UserName, SQL, Params);
            //    }
            //    else
            //    {
            //        return SqlServerCore.FillRecrods(dbConnect, UserName, SQL, Params);
            //    }
            //}

            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    OracleCore<T> core = new OracleCore<T>();

            //    if (dbConnect == null)
            //    {
            //        return core.FillRecrods(UserName, SQL, Params);
            //    }
            //    else
            //    {
            //        return core.FillRecrods(dbConnect, UserName, SQL, Params);
            //    }
            //}
            //else
            //{
                PostgreSqlCore<T> core = new PostgreSqlCore<T>();

                if (dbConnect == null)
                {
                    return core.FillRecrods(UserName, SQL, Params);
                }
                else
                {
                    return core.FillRecrods(dbConnect, UserName, SQL, Params);
                }
            //}

        }


    }
}
