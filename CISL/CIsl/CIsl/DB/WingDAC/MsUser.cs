using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// ユーザーマスタ
    /// </summary>
    public class MsUser : BaseWingDac
    {
        #region メンバ変数
        /// <summary>
        /// UserID
        /// </summary>
        public string ms_user_id = "";

        /// <summary>
        /// 区分
        /// </summary>
        public decimal user_kbn = EVal;
        
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string mail_address = "";


        public string sei = "";
        public string mei = "";

        public string sei_kana = "";
        public string mei_kana = "";

        public decimal admin_flag = EVal;

        public decimal sex = EVal;
        #endregion


        public override string ToString()
        {
            string ans = this.sei + " " + this.mei;
            return ans;
        }


        /// <summary>
        /// 基本SELEC文
        /// </summary>
//        private static string DefaultSelect = @"
//SELECT
//ms_user.ms_user_id,
//ms_user.user_kbn,
//ms_user.mail_address,
//ms_user.sei,
//ms_user.mei,
//ms_user.sei_kana,
//ms_user.mei_kana,
//ms_user.sex,
//ms_user.admin_flag,
//ms_user.delete_flag,
//ms_user.send_flag,
//ms_user.vessel_id,
//ms_user.data_no,
//ms_user.user_key,
//ms_user.renew_date,
//ms_user.renew_user_id,
//ms_user.ts,
//ms_user.kaicho_flag,
//ms_user.shacho_flag,
//ms_user.sekininsha_flag,
//ms_user.gl_flag,
//ms_user.tl_flag
//FROM
//ms_user

//";
        private static string DefaultSelect = @"
SELECT
ms_user.ms_user_id,
ms_user.user_kbn,
ms_user.mail_address,
ms_user.sei,
ms_user.mei,
ms_user.admin_flag,

ms_user.delete_flag,
ms_user.renew_date,
ms_user.renew_user_id,
ms_user.ts

FROM
ms_user

";
        /// <summary>
        /// レコード取得
        /// </summary>
        /// <param name="cone"></param>
        /// <returns></returns>
        public static List<MsUser> GetRecords(NpgsqlConnection cone)
        {
            List<MsUser> anslist = new List<MsUser>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_user.delete_flag = 0
ORDER BY
ms_user.ms_user_id

";

                //取得
                anslist = GetRecordsList<MsUser>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsUser GetRecords", e);
            }

            return anslist;
        }


        /// <summary>
        /// ログインユーザーの取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="loginid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static MsUser GetLoginUser(NpgsqlConnection cone, string loginid, string password)
        {
            MsUser ans = null;

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_user.delete_flag = 0
AND
ms_user.login_id = :login_id
AND
ms_user.password = :password

ORDER BY
ms_user.ms_user_id
";


                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "login_id", Value = loginid });
                plist.Add(new SqlParamData() { Name = "password", Value = password });

                //取得
                ans = GetRecordData<MsUser>(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("MsUser GetLoginUser", e);
            }

            return ans;
        }



        /// <summary>
        /// 対象のﾃﾞｰﾀ取得
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="ms_user_id"></param>
        /// <returns></returns>
        public static MsUser GetRecordByMsUserID(NpgsqlConnection cone, string ms_user_id)
        {
            MsUser ans = null;

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_user.delete_flag = 0
AND
ms_user.ms_user_id = :ms_user_id

";


                List<SqlParamData> plist = new List<SqlParamData>();
                plist.Add(new SqlParamData() { Name = "ms_user_id", Value = ms_user_id });

                //取得
                ans = GetRecordData<MsUser>(cone, sql, plist);
            }
            catch (Exception e)
            {
                throw new Exception("MsUser GetRecordByMsUserID", e);
            }

            return ans;
        }
    }
}
