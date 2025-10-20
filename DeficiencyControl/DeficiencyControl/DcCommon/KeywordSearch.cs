using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using Npgsql;

namespace DcCommon
{
    /// <summary>
    /// キーワード検索基底
    /// </summary>
    public abstract class BaseKeywordSearch
    {
        /// <summary>
        /// キーワード区切り文字列
        /// </summary>
        public const string KeywordDev = " ";

        /// <summary>
        /// 文字列が対象に含まれるかを検索する
        /// </summary>
        /// <param name="checkstring"></param>
        /// <param name="keyvec"></param>
        /// <returns></returns>
        private bool CheckString(string checkstring, string[] keyvec)
        {
            foreach (string key in keyvec)
            {
                //int pos = checkstring.IndexOf(key);

                //すべて大文字で判断
                string upcheck = checkstring.ToUpper();
                string upkey = key.ToUpper();


                int pos = upcheck.IndexOf(upkey);

                //発見！
                if (pos >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// キーワードに一致するか true=一致した
        /// </summary>
        /// <param name="keyword">ユーザー指定キーワード入力</param>
        /// <param name="data">データ</param>
        /// <returns></returns>
        private bool WordCheck(string keyword, string data)
        {
            //空白で区切り、複数検索を許可する
            string[] keyvec = keyword.Split(KeywordDev.ToCharArray());

            //検索
            bool ret = this.CheckString(data, keyvec);
            if (ret == false)
            {
                return false;
            }

            return true;

        }



        /// <summary>
        /// 文字列の作成
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected abstract string CreateString(object obj, NpgsqlConnection cone);


        /// <summary>
        /// キーワード検索にひっかっかるか否かを調べる true=引っかかった false=引っかからない
        /// </summary>
        /// <param name="keyword">入力キーワード文字列</param>
        /// <param name="obj">チェック対象</param>
        /// <param name="cone">DB接続</param>
        /// <returns></returns>
        public bool CheckKeyword(string keyword, object obj, NpgsqlConnection cone)
        {
            //キーワードがないときは、そのまま返却！
            keyword = keyword.Trim();
            if (keyword.Length <= 0)
            {
                return true;
            }


            //空白で複数検索可能にする
            string[] keyvec = keyword.Split(KeywordDev.ToCharArray());

            //検索文字列を作成
            string checkstring = this.CreateString(obj,cone);

            //文字列チェック
            bool ret = this.WordCheck(keyword, checkstring);
            if (ret == false)
            {
                return false;
            }

            return true;
        }
    }







}
