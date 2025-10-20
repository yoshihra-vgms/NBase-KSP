using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

namespace DcCommon.DB.DAC
{
    /// <summary>
    /// DBシーケンス管理クラス
    /// </summary>
    public class MsSeq
    {
        /// <summary>
        /// 事故トラブル報告書番号の取得
        /// </summary>
        /// <param name="cone">接続物</param>
        /// <returns></returns>
        public static int GetAccidentReportNo(NpgsqlConnection cone)
        {
            int ans = -1;

            try
            {
                string sql = @"SELECT nextval( 'accident_report_no_seq' )";
                using (NpgsqlCommand com = new NpgsqlCommand(sql, cone))
                {
                    //実行
                    object o = com.ExecuteScalar();
                    if (o != null)
                    {
                        ans = Convert.ToInt32(o);
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return ans;
        }
    }
}
