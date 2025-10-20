using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

namespace CIsl.DB.WingDAC
{
    public enum ERoleName1
    {
        指摘事項管理,


        MAX,
    }

    public enum ERoleName2
    {
        PSC,
        事故トラブル,
        検船,
        スケジュール,
        マスタ,


        MAX,
    }

    public enum ERoleName3
    {
        スケジュール,
        予定実績,
        会社,
        その他,


        MAX,
    }



    /// <summary>
    /// ユーザー権限マスタ
    /// </summary>
    public class MsRole : BaseWingDac
    {
        #region メンバ変数


        public decimal ms_role_id = EVal;
        public string ms_bumon_id = "";
        public decimal admin_flag = EVal;

        public string name1 = "";
        public string name2 = "";
        public string name3 = "";

        public decimal enable_flag = EVal;
        #endregion

        

        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_role.ms_role_id,
ms_role.ms_bumon_id,
ms_role.admin_flag,
ms_role.name1,
ms_role.name2,
ms_role.name3,
ms_role.enable_flag,
ms_role.renew_date,
ms_role.renew_user_id,
ms_role.ts
FROM
ms_role

";


        /// <summary>
        /// 対象部門のデータ一覧取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <returns></returns>
        public static List<MsRole> GetRecordsByMsBumonID(NpgsqlConnection cone, string ms_bumon_id)
        {
            List<MsRole> anslist = new List<MsRole>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_role.ms_bumon_id = :ms_bumon_id

ORDER BY
ms_role.name1, ms_role.name2, ms_role.name3
";

                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "ms_bumon_id", Value = ms_bumon_id });

                //取得
                anslist = GetRecordsList<MsRole>(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("MsRole GetRecordsByMsBumonID", e);
            }

            return anslist;
        }


        /// <summary>
        /// 対象部門、対象Name1の一覧取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="ms_bumon_id"></param>
        /// <returns></returns>
        public static List<MsRole> GetRecordsByMsBumonIDAdminName1(NpgsqlConnection cone, string ms_bumon_id, decimal admin_flag, string name1)
        {
            List<MsRole> anslist = new List<MsRole>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_role.ms_bumon_id = :ms_bumon_id
AND
ms_role.admin_flag = :admin_flag
AND
ms_role.name1 = :name1

ORDER BY
ms_role.name1, ms_role.name2, ms_role.name3
";

                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "ms_bumon_id", Value = ms_bumon_id });
                plist.Add(new SqlParamData() { Name = "admin_flag", Value = admin_flag });
                plist.Add(new SqlParamData() { Name = "name1", Value = name1 });

                //取得
                anslist = GetRecordsList<MsRole>(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("MsRole GetRecordsByMsBumonIDAdminName1", e);
            }

            return anslist;
        }

    }
}
