using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace ORMapping.SqlServer
{
    public class SqlServerBase
    {
        public static SqlCeDataReader ExecuteReader(string UserName, string SQL, ParameterConnection Params)
        {
            using (DBConnect Connection = new DBConnect())
            {
                return ExecuteReader(Connection, UserName, SQL, Params);
            }
        }
        public static SqlCeDataReader ExecuteReader(DBConnect Connection, string UserName, string SQL, ParameterConnection Params)
        {
            using (SqlCeCommand cmd = new SqlCeCommand(SQL, Connection.SqlServerConnection))
            {
                cmd.Transaction = Connection.SqlTransaction;
                foreach (DBParameter Parameter in Params)
                {
                    cmd.Parameters.Add(new SqlCeParameter(Parameter.ParameterName, ORMappingUtils.ValidateParamValue(Parameter.Param)));
                }

                return cmd.ExecuteReader();
            }
        }

        public static int ExecuteNonQuery(string UserName, string SQL, ParameterConnection Params)
        {
            using (DBConnect Connection = new DBConnect())
            {
                return ExecuteNonQuery(Connection, UserName, SQL, Params);
            }

        }
        public static int ExecuteNonQuery(DBConnect Connection, string UserName, string SQL, ParameterConnection Params)
        {
            using (SqlCeCommand cmd = new SqlCeCommand(SQL, Connection.SqlServerConnection))
            {
                cmd.Transaction = Connection.SqlTransaction;
                foreach (DBParameter Parameter in Params)
                {
                    cmd.Parameters.Add(new SqlCeParameter(Parameter.ParameterName, ORMappingUtils.ValidateParamValue(Parameter.Param)));
                }
                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string UserName, string SQL, ParameterConnection Params)
        {
            using (DBConnect Connection = new DBConnect())
            {
                return ExecuteScalar(Connection, UserName, SQL, Params);
            }

        }
        public static object ExecuteScalar(DBConnect Connection, string UserName, string SQL, ParameterConnection Params)
        {
            using (SqlCeCommand cmd = new SqlCeCommand(SQL, Connection.SqlServerConnection))
            {
                cmd.Transaction = Connection.SqlTransaction;
                foreach (DBParameter Parameter in Params)
                {
                    cmd.Parameters.Add(new SqlCeParameter(Parameter.ParameterName, ORMappingUtils.ValidateParamValue(Parameter.Param)));
                }

                return cmd.ExecuteScalar();
            }
        }
    }
}
