using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace WingCommon
{
    public class MailBase
    {
        // TODO: メールサーバ
        //       メールテンプレートディレクトリ
        //       を、App.Config に設定してください
        protected string EncodeMailHeader(string str, System.Text.Encoding enc)
        {
            //Base64でエンコードする
            string ret = System.Convert.ToBase64String(enc.GetBytes(str));
            //RFC2047形式に
            ret = string.Format("=?{0}?B?{1}?=", enc.BodyName, ret);
            return ret;
        }

        protected bool 送信チェック(ref string hostName)
        {
            if (ConfigurationSettings.AppSettings["メールサーバ"] == "")
            {
                return false;
            }
            hostName = ConfigurationSettings.AppSettings["メールサーバ"];
            return true;
        }

        protected string 本文(string tmpFileName)
        {
            string body = "";

            string FullPath = ConfigurationSettings.AppSettings["メールテンプレートディレクトリ"];
            FullPath += tmpFileName;
            using (StreamReader sr = new StreamReader(FullPath, Encoding.GetEncoding("Shift_Jis")))
            {
                body = sr.ReadToEnd();
            }

            return body;
        }
    }
}
