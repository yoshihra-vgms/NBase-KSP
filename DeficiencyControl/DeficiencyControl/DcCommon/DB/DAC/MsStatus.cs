using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;
using CIsl.DB;

namespace DcCommon.DB.DAC
{
    /// <summary>
    /// ステータスの値定義
    /// </summary>
    public enum EStatus
    {
        Pending = 1,        
        Complete,
    }


    /// <summary>
    /// ステータスマスタ
    /// ms_status
    /// </summary>
    public class MsStatus : BaseDac
    {
        public override int ID
        {
            get
            {
                return this.status_id;
            }
        }


        #region メンバ変数

        /// <summary>
        /// ステータスID
        /// </summary>
        public int status_id = EVal;

        /// <summary>
        /// ステータス名
        /// </summary>
        public string status_name = "";

        #endregion


        public EStatus StsutsNo
        {
            get
            {
                return (EStatus)this.status_id;
            }
        }



        public override string ToString()
        {
            return this.status_name;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_status.status_id,
ms_status.status_name,
ms_status.delete_flag,
ms_status.create_ms_user_id,
ms_status.update_ms_user_id,
ms_status.create_date,
ms_status.update_date
FROM
ms_status

";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">db接続</param>
        /// <returns></returns>
        public static List<MsStatus> GetRecords(NpgsqlConnection cone)
        {
            List<MsStatus> anslist = new List<MsStatus>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_status.delete_flag = false
";

                //取得
                anslist = GetRecordsList<MsStatus>(cone, sql);
            }
            catch(Exception e)
            {
                throw new Exception("MsStatus GetRecords", e);
            }

            return anslist;
        }
    }
}
