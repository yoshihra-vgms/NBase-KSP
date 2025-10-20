using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WingCommon;
using System.Net.Mail;
using System.Configuration;
using System.Windows.Forms;

namespace Hachu.Utils
{
    public class 見積依頼メール : MailBase
    {
        // TODO: 以下の３行を設定ファイルに移す
        // TODO: ベースURL
        private static string SendFrom = "wing_sys@ex.iino.co.jp";
        private static string BaseURL = "http://localhost:3949/WingVendor/default.aspx?WebKey=";
        private static string TmpFileName = "見積依頼メール.txt";

        public bool 送信(string customerName, string tantousha, string header, string mailAddress, string mkNo, string MkKigen, string webKey, string iraisha)
        {
            bool ret = false;

            #region メール送信
            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    msg.Subject = EncodeMailHeader(header, System.Text.Encoding.GetEncoding("iso-2022-jp"));
                    msg.From = new MailAddress(SendFrom);
                    msg.To.Add(new MailAddress(mailAddress));
                    msg.BodyEncoding = System.Text.Encoding.GetEncoding("iso-2022-jp");
                    string body = 本文(TmpFileName);

                    body = body.Replace("[見積依頼先]", customerName);
                    body = body.Replace("[担当者]", tantousha);
                    body = body.Replace("[見積依頼番号]", mkNo);
                    if (MkKigen.Length > 0)
                    {
                        body = body.Replace("[見積回答期限]", MkKigen);
                    }
                    else
                    {
                        body = body.Replace("見積回答期限:[見積回答期限]", "");
                    }
                    body = body.Replace("[URL]", BaseURL + webKey);
                    body = body.Replace("[見積依頼担当]", iraisha);
                    msg.Body = body;

                    string hostName = "";
                    if (送信チェック(ref hostName) == false)
                    {
                        MessageBox.Show("メールサーバの設定がありません。管理者に確認してください。", "エラー");
                        MessageBox.Show(msg.Body);
                        ret = true;
                    }
                    else
                    {
                        SmtpClient client = new SmtpClient(hostName);
                        client.ServicePoint.MaxIdleTime = 1000;
                        client.Send(msg);
                        MessageBox.Show("見積依頼メールを送信しました。", "確認");
                        ret = true;
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show("メールの送信に失敗しました。\n致命的なエラーです。\n" + E.Message, "エラー");
                    ret = false;
                }
            }
            #endregion

            return ret;
        }
    }
}
