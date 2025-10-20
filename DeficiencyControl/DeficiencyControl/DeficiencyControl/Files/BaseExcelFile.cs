using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.DB.DAC;
using AdvanceSoftware.ExcelCreator.Xlsx;
using CIsl.DB;
using System.Drawing;

namespace DeficiencyControl.Files
{
    /// <summary>
    /// Excel出力基底
    /// </summary>
    public class BaseExcelFile
    {
        public const string VesselALLName = "ALL";

        /// <summary>
        /// データ番号の作成
        /// </summary>
        /// <param name="name">タグ名</param>
        /// <param name="no">行番号</param>
        /// <returns></returns>
        protected string CreateTemplateNo(string name, int no)
        {
            string ans = "";

            ans = string.Format("{0}_{1}", name, no);

            return ans;
        }



        /// <summary>
        /// 対象rowを削除する 削除成功可否
        /// </summary> 
        /// <param name="crea"></param>
        /// <param name="starttag">削除開始タグ</param>
        /// <param name="endtag">削除終了タグ</param>
        protected bool DeleteRows(XlsxCreator crea, string starttag, string endtag)
        {
            //開始と終了位置を取得
            Point spos = crea.GetVarNamePos(starttag, 0);
            Point epos = crea.GetVarNamePos(endtag, 0);

            //どちらかのタグが存在しなかった
            if (spos.Y < 0 || epos.Y < 0)
            {
                return false;
            }

            int delcount = epos.Y - spos.Y + 1;

            //削除列数がマイナスはおかしい。
            if (delcount < 0)
            {
                return false;
            }

            crea.RowDelete(spos.Y, delcount);


            return true;
        }

        /// <summary>
        /// 対象位置から削除する
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="starttag">削除開始タグ</param>
        /// <param name="count">削除数</param>
        /// <returns></returns>
        protected bool DeleteRows(XlsxCreator crea, string starttag, int delcount)
        {
            //開始と終了位置を取得
            Point spos = crea.GetVarNamePos(starttag, 0);
            
            //どちらかのタグが存在しなかった
            if (spos.Y < 0 )
            {
                return false;
            }

            crea.RowDelete(spos.Y, delcount);


            return true;
        }
        
    }
}
