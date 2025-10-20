using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace ORMapping.PostgreSql
{
    public class PostgresqlConnectionPool : IDisposable
    {
        private static readonly PostgresqlConnectionPool instance = new PostgresqlConnectionPool();
        private Dictionary<int, NpgsqlConnection> connections = new Dictionary<int, NpgsqlConnection>();


        private PostgresqlConnectionPool()
        {
        }

        public static PostgresqlConnectionPool Instance()
        {
            return instance;
        }

        public NpgsqlConnection GetConnection(int threadHashCode)
        {
            if (!connections.ContainsKey(threadHashCode))
            {
                NpgsqlConnection conn = new NpgsqlConnection(Common.DBCS);
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
            foreach (NpgsqlConnection conn in connections.Values)
            {
                conn.Dispose();
            }
        }

        #endregion
    }
}
