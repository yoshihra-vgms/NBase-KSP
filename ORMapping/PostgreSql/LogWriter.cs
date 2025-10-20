using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

namespace ORMapping
{
    /// <summary>
    /// ログ出力クラス
    /// </summary>
    public class LogWriter
    {
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="SQLData"></param>
        /// <param name="Params"></param>
        /// <param name="UserName"></param>
        public static void Write(DBConnect connection, string SQLData, ParameterConnection Params, string UserName)
        {
            #region SQL生成
            string SQL = @"
insert into OPERATION_LOG(
OPERATION_LOG_ID,
MS_USER_ID,
SQL,
PARAMS,
OPERATION_DATE)
values(
Nextval( 'SEQ_OPERATION_LOG_ID' ),
:MS_USER_ID,
:SQL,
:PARAMS,
CURRENT_TIMESTAMP)
";
            #endregion

            using (NpgsqlCommand cmd = new NpgsqlCommand(SQL, connection.NpgsqlConnection))
            {
                cmd.Parameters.Add(new NpgsqlParameter("MS_USER_ID", DbType.String, 50));
                cmd.Parameters.Add(new NpgsqlParameter("SQL", DbType.String, 4000));
                cmd.Parameters.Add(new NpgsqlParameter("PARAMS", DbType.String, 1000));


                cmd.Parameters["MS_USER_ID"].Value = UserName;
                cmd.Parameters["SQL"].Value = SQLData;
                cmd.Parameters["PARAMS"].Value = GetParameters(Params);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// パラメータを文字列化する
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        private static string GetParameters(ParameterConnection Params)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DBParameter param in Params)
            {
                sb.AppendFormat("{0} = {1} , ", param.ParameterName, param.Param);
            }

            return sb.ToString();
        }
    }
}
