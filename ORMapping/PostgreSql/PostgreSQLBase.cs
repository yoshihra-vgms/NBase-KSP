using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace ORMapping.PostgreSql
{
    public class PostgreSqlBase
    {
        public static NpgsqlDataReader ExecuteReader(string UserName, string SQL, ParameterConnection Params)
        {
            using (DBConnect Connection = new DBConnect())
            {
                return ExecuteReader(Connection, UserName, SQL, Params);
            }
        }
        public static NpgsqlDataReader ExecuteReader(DBConnect Connection, string UserName, string SQL, ParameterConnection Params)
        {
            if (Common.EXECUTE_NON_QUERY_LOG == true)
            {
                //ログ保存
                LogWriter.Write(Connection, SQL, Params, UserName);
            }
            using (NpgsqlCommand cmd = new NpgsqlCommand(SQL, Connection.NpgsqlConnection))
            {
                cmd.Transaction = Connection.NpgsqlTrans;
                foreach (DBParameter Parameter in Params)
                {
                    NpgsqlParameter p = new NpgsqlParameter(Parameter.ParameterName, ORMappingUtils.ValidateParamValue(Parameter.Param));
                    cmd.Parameters.Add(p);
                }
                //
                //cmd.BindByName = true;

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
            if (Common.EXECUTE_NON_QUERY_LOG == true)
            {
                //ログ保存 
                LogWriter.Write(Connection, SQL, Params, UserName);
            }
            using (NpgsqlCommand cmd = new NpgsqlCommand(SQL, Connection.NpgsqlConnection))
            {
                cmd.Transaction = Connection.NpgsqlTrans;
                foreach (DBParameter Parameter in Params)
                {
                    NpgsqlParameter p = new NpgsqlParameter(Parameter.ParameterName, ORMappingUtils.ValidateParamValue(Parameter.Param));
                    cmd.Parameters.Add(p);
                }
                //
                //cmd.BindByName = true;

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
            if (Common.EXECUTE_NON_QUERY_LOG == true)
            {
                //ログ保存 
                LogWriter.Write(Connection, SQL, Params, UserName);
            }
            using (NpgsqlCommand cmd = new NpgsqlCommand(SQL, Connection.NpgsqlConnection))
            {
                cmd.Transaction = Connection.NpgsqlTrans;
                foreach (DBParameter Parameter in Params)
                {
                    NpgsqlParameter p = new NpgsqlParameter(Parameter.ParameterName, ORMappingUtils.ValidateParamValue(Parameter.Param));
                    cmd.Parameters.Add(p);
                }
                //
                //cmd.BindByName = true;

                return cmd.ExecuteScalar();
            }
        }
    }
}
