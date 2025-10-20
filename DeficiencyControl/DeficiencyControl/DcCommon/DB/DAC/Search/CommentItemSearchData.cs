using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIsl.DB;

namespace DcCommon.DB.DAC.Search
{
    /// <summary>
    /// CommentItem検索データ
    /// </summary>
    public class CommentItemSearchData : BaseSearchData
    {
     


        /// <summary>
        /// SQLの作成
        /// </summary>
        /// <param name="plist"></param>
        /// <returns></returns>
        public override string CreateSQLWhere(out List<SqlParamData> plist)
        {
            string ans = "";
            plist = new List<SqlParamData>();

            return ans;
        }
    }
}
