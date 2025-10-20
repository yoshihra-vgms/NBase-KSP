using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;
using CIsl.DB;


namespace DcCommon.DB
{
    /// <summary>
    /// DeficiencyCodeの一覧
    /// </summary>
    public class DeficiencyCategoryData
    {
        /// <summary>
        /// これのカテゴリ
        /// </summary>
        public MsDeficiencyCategory Category = null;

        /// <summary>
        /// 属するコード
        /// </summary>
        public List<MsDeficiencyCode> CodeList = new List<MsDeficiencyCode>();


        /// <summary>
        /// データ一覧の取得
        /// </summary>
        /// <param name="wingcone">DB接続</param>
        /// <returns></returns>
        public static List<DeficiencyCategoryData> GetDataList(NpgsqlConnection wingcone)
        {
            List<DeficiencyCategoryData> anslist = new List<DeficiencyCategoryData>();

            try
            {
                //カテゴリ取得
                List<MsDeficiencyCategory> catelist = MsDeficiencyCategory.GetRecords(wingcone);
                foreach (MsDeficiencyCategory cate in catelist)
                {
                    DeficiencyCategoryData ans = new DeficiencyCategoryData();
                    ans.Category = cate;

                    //カテゴリに属するものを取得
                    ans.CodeList = MsDeficiencyCode.GetRecordsByDeficiencyCategoryID(wingcone, cate.deficiency_category_id);


                    anslist.Add(ans);

                }
            }
            catch (Exception e)
            {
                throw new Exception("DeficiencyCategoryData GetDataList", e);
            }

            return anslist;
        }
    }
}
