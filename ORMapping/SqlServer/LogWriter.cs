using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.IO;

namespace ORMapping.SqlServer
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
        public static void Write(string SQLData, ParameterConnection Params)
        {
            using (StreamWriter writer = new StreamWriter("sql.log", true))
            {
                writer.WriteLine(SQLData);
                writer.WriteLine(GetParameters(Params));
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
