using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.DB.DAC;
using AdvanceSoftware.ExcelCreator.Xlsx;
using CIsl.DB;

namespace DeficiencyControl.Files
{
    /// <summary>
    /// ExcelImport基底
    /// </summary>
    public abstract class BaseExcelImportFile
    {
        /// <summary>
        /// 文字列取得
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        protected string GetString(XlsxCreator crea, string cell)
        {
            string ans = "";

            string s = crea.Cell(cell).FormattedString.Trim();

            ans = s.Replace("\n", Environment.NewLine);

            return ans;
        }

        /// <summary>
        /// 時間取得
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        protected DateTime GetDateTime(XlsxCreator crea, string cell)
        {
            DateTime ans = BaseDac.EDate;

            long val = crea.Cell(cell).Long;

            ans = DateTime.FromOADate(val);

            return ans;
        }


        /// <summary>
        /// Int取得
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        protected int GetInt(XlsxCreator crea, string cell)
        {
            int ans = 0;
            string s = crea.Cell(cell).FormattedString.Trim();

            ans = Convert.ToInt32(s);
            

            return ans;
        }

        /// <summary>
        /// Cell位置の作成
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        protected string CreateCellNo(string col, int row)
        {
            string ans = col + row.ToString();
            return ans;
        }
    }
}
