using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIsl.DB
{
    /// <summary>
    /// 検索基底クラス
    /// </summary>
    public abstract class BaseSearchData
    {
        public abstract string CreateSQLWhere(out List<SqlParamData> plist);

        /// <summary>
        /// 検索開始の日付時刻を取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected DateTime GetSearchStartDate(DateTime date)
        {
            DateTime ans = date.Date;


            return ans;
        }

        /// <summary>
        /// 検索終了の日付時刻を取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected DateTime GetSearchEndDate(DateTime date)
        {
            DateTime ans = date.Date.AddDays(1).AddMilliseconds(-1);


            return ans;
        }

        /// <summary>
        /// SQL Likeで検索できるデータ文字列を作成する
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected string GetLikeString(string s)
        {
            string ans = ("%" + s + "%");

            return ans;
        }


    }

    
}
