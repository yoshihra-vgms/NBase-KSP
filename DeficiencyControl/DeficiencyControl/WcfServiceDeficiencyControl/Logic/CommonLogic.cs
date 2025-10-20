using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfServiceDeficiencyControl.Logic
{
    /// <summary>
    /// サーバー側汎用ロジック
    /// </summary>
    public class CommonLogic
    {

        /// <summary>
        /// 対象日の年度を計算する
        /// </summary>
        /// <param name="date">日付</param>
        /// <returns></returns>
        public static int CalcuYear(DateTime date)
        {
            int ans = date.Year;

            //三月以下の場合は年度はひとつ前になる
            if (date.Month < 4)
            {
                ans -= 1;
            }

            return ans;
        }
    }
}