using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace ORMapping.SqlServer
{
    public class SqlCeConnectionPool : IDisposable
    {
        private static readonly SqlCeConnectionPool instance = new SqlCeConnectionPool();
        private Dictionary<int, SqlCeConnection> connections = new Dictionary<int, SqlCeConnection>();
        
        
        private SqlCeConnectionPool()
        {
        }

        public static SqlCeConnectionPool Instance()
        {
            return instance;
        }

        public SqlCeConnection GetConnection(int threadHashCode)
        {
            if (!connections.ContainsKey(threadHashCode))
            {
                SqlCeConnection conn = new SqlCeConnection(Common.DBCS);
                conn.Open();

                // オープン出来たら、保持する
                connections[threadHashCode] = conn;
            }
            else
            {
                if (connections[threadHashCode].State != System.Data.ConnectionState.Open)
                {
                    try
                    {
                        connections[threadHashCode].Close();
                        connections[threadHashCode].Dispose();
                    }
                    catch
                    {
                    }

                    connections[threadHashCode].Open();
                }
            }
            return connections[threadHashCode];
        }

        #region IDisposable メンバ

        public void Dispose()
        {
            foreach (SqlCeConnection conn in connections.Values)
            {
                conn.Dispose();
            }
        }

        #endregion
    }
}
