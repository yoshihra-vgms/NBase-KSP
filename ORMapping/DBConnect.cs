using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Npgsql;
using ORMapping.PostgreSql;
//using Oracle.DataAccess.Client;
//using ORMapping.Oracle;
//using System.Data.SqlServerCe;
//using ORMapping.SqlServer;

namespace ORMapping
{
    public class DBConnect : IDisposable
    {
        public NpgsqlConnection NpgsqlConnection = null;
        public NpgsqlTransaction NpgsqlTrans;

        //public readonly SqlCeConnection SqlServerConnection = null;
        //public SqlCeTransaction SqlTransaction = null;

        //public OracleConnection OracleConnection = null;
        //public OracleTransaction OracleTrans;

        public DBConnect()
        {
            //if (Common.DBTYPE == Common.DB_TYPE.SQLSERVER)
            //{
            //    SqlServerConnection = SqlCeConnectionPool.Instance().GetConnection(System.Threading.Thread.CurrentThread.GetHashCode());
            //}
            //else if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902 Postgresql_Client化対応　elseif終わりまで
            //{
            //    NpgsqlConnection = PostgresqlConnectionPool.Instance().GetConnection(System.Threading.Thread.CurrentThread.GetHashCode());
            //}
            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902 Postgresql_Client化対応　elseif終わりまで
            {
                NpgsqlConnection = PostgresqlConnectionPool.Instance().GetConnection(System.Threading.Thread.CurrentThread.GetHashCode());
            }

            ConnectionOpen();
        }

        public DBConnect(string ConnectionString)
        {
            NpgsqlConnection = new NpgsqlConnection(ConnectionString);
            NpgsqlConnection.Open();
        }

        public void ConnectionOpen()
        {
            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    NpgsqlConnection = new NpgsqlConnection(Common.DBCS);
            //    NpgsqlConnection.Open();
            //}
            try
            {
                //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
                //{
                //    OracleConnection = new OracleConnection(Common.DBCS);
                //    OracleConnection.Open();
                //}
                //else
                //{
                    NpgsqlConnection = new NpgsqlConnection(Common.DBCS);
                    NpgsqlConnection.Open();
                //}
            }
            catch(Exception e)
            {
                throw e;
            }
 
        }

        public void BeginTransaction()
        {
            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    NpgsqlTrans = NpgsqlConnection.BeginTransaction();
            //}
            //else if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902 Postgresql_Client化対応　elseif終わりまで
            //{
            //    NpgsqlTrans = NpgsqlConnection.BeginTransaction();
            //}
            //else
            //{
            //    SqlTransaction = SqlServerConnection.BeginTransaction();
            //}
            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    OracleTrans = OracleConnection.BeginTransaction();
            //}
            //else
            //{
                NpgsqlTrans = NpgsqlConnection.BeginTransaction();
            //}
        }

        public void Commit()
        {
            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    NpgsqlTrans.Commit();
            //}
            //else if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902 Postgresql_Client化対応　elseif終わりまで
            //{
            //    NpgsqlTrans.Commit();
            //}
            //else
            //{
            //    SqlTransaction.Commit();
            //}
            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    OracleTrans.Commit();
            //}
            //else
            //{
                NpgsqlTrans.Commit();
            //}
        }

        public void RollBack()
        {
            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    NpgsqlTrans.Rollback();
            //}
            //else if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902 Postgresql_Client化対応　elseif終わりまで
            //{
            //    NpgsqlTrans.Rollback();
            //}
            //else
            //{
            //    SqlTransaction.Rollback();
            //}
            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    OracleTrans.Rollback();
            //}
            //else
            //{
                NpgsqlTrans.Rollback();
            //}
        }

        public static DbDataReader ExecuteReader(string UserName, string SQL, ParameterConnection Params)
        {
            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return PostgreSqlBase.ExecuteReader(Connection, UserName, SQL, Params);
            //    }
            //}
            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902 Postgresql_Client化対応　elseif終わりまで
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return PostgreSqlBase.ExecuteReader(Connection, UserName, SQL, Params);
            //    }
            //}
            //else
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return SqlServerBase.ExecuteReader(Connection, UserName, SQL, Params);
            //    }
            //}
            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return OracleBase.ExecuteReader(Connection, UserName, SQL, Params);
            //    }
            //}
            //else
            //{
                using (DBConnect Connection = new DBConnect())
                {
                    return PostgreSqlBase.ExecuteReader(Connection, UserName, SQL, Params);
                }
            //}
        }
        public static DbDataReader ExecuteReader(DBConnect Connection, string UserName, string SQL, ParameterConnection Params)
        {
            if (Connection == null)
            {
                return ExecuteReader(UserName, SQL, Params);
            }

            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    return PostgreSqlBase.ExecuteReader(Connection, UserName, SQL, Params);
            //}
            //else if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902 Postgresql_Client化対応　elseif終わりまで
            //{
            //    return PostgreSqlBase.ExecuteReader(Connection, UserName, SQL, Params);
            //}
            //else
            //{
            //    return SqlServerBase.ExecuteReader(Connection, UserName, SQL, Params);
            //}

            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    return OracleBase.ExecuteReader(Connection, UserName, SQL, Params);
            //}
            //else
            //{
                return PostgreSqlBase.ExecuteReader(Connection, UserName, SQL, Params);
            //}
        }


        public static int ExecuteNonQuery(string UserName, string SQL, ParameterConnection Params)
        {
            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return PostgreSqlBase.ExecuteNonQuery(Connection, UserName, SQL, Params);
            //    }
            //}
            //else if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902 Postgresql_Client化対応　elseif終わりまで
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return PostgreSqlBase.ExecuteNonQuery(Connection, UserName, SQL, Params);
            //    }
            //}
            //else
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return SqlServerBase.ExecuteNonQuery(Connection, UserName, SQL, Params);
            //    }
            //}
            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return OracleBase.ExecuteNonQuery(Connection, UserName, SQL, Params);
            //    }
            //}
            //else
            //{
                using (DBConnect Connection = new DBConnect())
                {
                    return PostgreSqlBase.ExecuteNonQuery(Connection, UserName, SQL, Params);
                }
            //}
        }

        public static int ExecuteNonQuery(DBConnect dbConnect, string UserName, string SQL, ParameterConnection Params)
        {
            if (dbConnect == null)
            {
                return ExecuteNonQuery(UserName, SQL, Params);
            }

            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    return PostgreSqlBase.ExecuteNonQuery(dbConnect, UserName, SQL, Params);
            //}
            //else if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902 Postgresql_Client化対応　elseif終わりまで
            //{
            //    return PostgreSqlBase.ExecuteNonQuery(dbConnect, UserName, SQL, Params);
            //}
            //else
            //{
            //    return SqlServerBase.ExecuteNonQuery(dbConnect, UserName, SQL, Params);
            //}
            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    return OracleBase.ExecuteNonQuery(dbConnect, UserName, SQL, Params);
            //}
            //else
            //{
                return PostgreSqlBase.ExecuteNonQuery(dbConnect, UserName, SQL, Params);
            //}
        }

        //public static int ExecuteNonQuery(string connectionString, string UserName, string SQL, ParameterConnection Params)
        //{
        //    DBConnect dbConnect = new DBConnect(connectionString);
        //    return PostgreSqlBase.ExecuteNonQuery(dbConnect, UserName, SQL, Params);
        //}

        public static object ExecuteScalar(string UserName, string SQL, ParameterConnection Params)
        {
            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return PostgreSqlBase.ExecuteScalar(Connection, UserName, SQL, Params);
            //    }
            //}
            //else if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902 Postgresql_Client化対応　elseif終わりまで
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return PostgreSqlBase.ExecuteScalar(Connection, UserName, SQL, Params);
            //    }
            //}
            //else
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return SqlServerBase.ExecuteScalar(Connection, UserName, SQL, Params);
            //    }
            //}
            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    using (DBConnect Connection = new DBConnect())
            //    {
            //        return OracleBase.ExecuteScalar(Connection, UserName, SQL, Params);
            //    }
            //}
            //else
            //{
                using (DBConnect Connection = new DBConnect())
                {
                    return PostgreSqlBase.ExecuteScalar(Connection, UserName, SQL, Params);
                }
            //}
        }

        public static object ExecuteScalar(DBConnect dbConnect, string UserName, string SQL, ParameterConnection Params)
        {
            if (dbConnect == null)
            {
                return ExecuteScalar(UserName, SQL, Params);
            }

            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    return PostgreSqlBase.ExecuteScalar(dbConnect, UserName, SQL, Params);
            //}
            //else if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)// 20150902 Postgresql_Client化対応　elseif終わりまで
            //{
            //    return PostgreSqlBase.ExecuteScalar(dbConnect, UserName, SQL, Params);
            //}
            //else
            //{
            //    return SqlServerBase.ExecuteScalar(dbConnect, UserName, SQL, Params);
            //}

            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    return OracleBase.ExecuteScalar(dbConnect, UserName, SQL, Params);
            //}
            //else
            //{
                return PostgreSqlBase.ExecuteScalar(dbConnect, UserName, SQL, Params);
            //}
        }

        public static string ExecuteProcedure(DBConnect dbConnect, string UserName, string SQL, ParameterConnection Params)
        {
            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    return OracleBase.ExecuteProcedure(dbConnect, UserName, SQL, Params);
            //}
            //else
            //{
                return null;
            //}
        }

        #region IDisposable メンバ

        public void Dispose()
        {
            //if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            //{
            //    NpgsqlConnection.Close();
            //    NpgsqlConnection = null;
            //}

            //if (Common.DBTYPE == Common.DB_TYPE.ORACLE)
            //{
            //    OracleConnection.Close();
            //    OracleConnection = null;
            //}
            //else
            //{
                NpgsqlConnection.Close();
                NpgsqlConnection = null;
            //}
        }

        #endregion
    }
}
