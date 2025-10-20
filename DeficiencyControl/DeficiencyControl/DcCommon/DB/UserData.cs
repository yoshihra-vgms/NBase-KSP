using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;

namespace DcCommon.DB
{
    
    /// <summary>
    /// ユーザー情報まとめ
    /// </summary>
    public class UserData
    {
        
        /// <summary>
        /// ユーザーマスタ
        /// </summary>
        public MsUser User = null;


        /// <summary>
        /// 所属部門
        /// </summary>
        public MsBumon Bumon = null;

        /// <summary>
        /// これのロール
        /// </summary>
        public List<MsRole> RoleList = new List<MsRole>();

        /// <summary>
        /// 対象のデータを取得 (Roleを除く)
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="ms_user_id"></param>
        /// <returns></returns>
        public static UserData GetDataByMsUserID(NpgsqlConnection cone, string ms_user_id)
        {
            UserData ans = new UserData();
            try
            {
               ans.User= MsUser.GetRecordByMsUserID(cone, ms_user_id);

               ans.Bumon = MsBumon.GetRecordByMsUserID(cone, ms_user_id);
                
            }
            catch (Exception e)
            {
                throw new Exception("UserData GetDataByMsUserID", e);
            }


            return ans;
        }
         
    }
}
