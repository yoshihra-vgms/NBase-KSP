using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.ServiceModel;
using System.Net.Mail;
using System.IO;
using System.Text;
using NBaseUtil;
using SendGrid.Helpers.Mail;


namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_発注メール送信(
                    NBaseData.DAC.MsUser loginUser,
                    string odMkId,
                    string customerName,
                    string tantousha,
                    string mailAddress,
                    string subject,
                    string hachuNo,
                    string msVesselName,
                    string telNo,
                    string faxNo,
                    string bikou,
                    ref string errMessage);

    }
    public partial class Service
    {
        public bool BLC_発注メール送信(
                    NBaseData.DAC.MsUser loginUser,
                    string odMkId,
                    string customerName,
                    string tantousha,
                    string mailAddress,
                    string subject,
                    string hachuNo,
                    string msVesselName,
                    string telNo,
                    string faxNo,
                    string bikou,
                    ref string errMessage)
        {
            string TmpFileName = "発注メール.txt";
            bool ret = false;

            #region メール送信

            #region 旧コード
            //using (MailMessage msg = new MailMessage())
            //{
            //    string pdfFileName = "";
            //    try
            //    {
            //        // 発注メール情報を登録
            //        NBaseData.DAC.OdHachuMail hachuMail = NBaseData.DAC.OdHachuMail.GetRecord(loginUser, odMkId, tantousha, mailAddress, subject);
            //        if (hachuMail == null)
            //        {
            //            hachuMail = new NBaseData.DAC.OdHachuMail();
            //            hachuMail.OdHachuMailID = System.Guid.NewGuid().ToString();
            //            hachuMail.OdMkID = odMkId;
            //            hachuMail.Tantousha = tantousha;
            //            hachuMail.TantouMailAddress = mailAddress;
            //            hachuMail.Subject = subject;
            //            hachuMail.Bikou = bikou;
            //            hachuMail.RenewDate = DateTime.Now;
            //            hachuMail.RenewUserID = loginUser.MsUserID;
            //            ret = hachuMail.InsertRecord(loginUser);
            //        }
            //        else
            //        {
            //            hachuMail.Bikou = bikou;
            //            hachuMail.RenewDate = DateTime.Now;
            //            hachuMail.RenewUserID = loginUser.MsUserID;

            //            ret = hachuMail.UpdateRecord(loginUser);
            //        }

            //        // メール情報
            //        string sendFrom = 送信元();
            //        string body = 本文(TmpFileName);
            //        string baseURL = BaseURL();

            //        msg.Subject = EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp"));
            //        msg.From = new MailAddress(sendFrom);
            //        msg.To.Add(new MailAddress(mailAddress));
            //        msg.BodyEncoding = System.Text.Encoding.GetEncoding("iso-2022-jp");
            //        body = body.Replace("[見積依頼先]", customerName);
            //        body = body.Replace("[担当者]", tantousha);
            //        body = body.Replace("[発注番号]", hachuNo);
            //        body = body.Replace("[船舶名]", msVesselName);
            //        body = body.Replace("[見積依頼担当]", loginUser.FullName);
            //        msg.Body = body;

            //        // 注文書作成
            //        byte[] excelData = BLC_PDF注文書_取得(odMkId, telNo, faxNo, bikou);

            //        string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            //        pdfFileName = path + "注文書[" + loginUser.FullName + "].pdf";
            //        using (FileStream objFileStream = new FileStream(pdfFileName, FileMode.Create))
            //        {
            //            objFileStream.Write(excelData, 0, excelData.Length);
            //            objFileStream.Close();
            //        }
            //        Attachment pdfData = new Attachment(pdfFileName);
            //        pdfData.Name = "注文書[" + hachuNo + "].pdf";
            //        msg.Attachments.Add(pdfData);


            //        // 送信
            //        string hostName = "";
            //        if (送信チェック(ref hostName) == false)
            //        {
            //            errMessage = "メールサーバの設定がありません。管理者に確認してください。";
            //            ret = true;
            //        }
            //        else
            //        {
            //            MySmtpClient client = new MySmtpClient(hostName);
            //            client.Send(msg);
            //            ret = true;
            //        }
            //    }
            //    catch (Exception E)
            //    {
            //        errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + E.Message;
            //        ret = false;
            //    }
            //}
            #endregion

            string pdfFileName = "";
            try
            {
                // 発注メール情報を登録
                NBaseData.DAC.OdHachuMail hachuMail = NBaseData.DAC.OdHachuMail.GetRecord(loginUser, odMkId, tantousha, mailAddress, subject);
                if (hachuMail == null)
                {
                    hachuMail = new NBaseData.DAC.OdHachuMail();
                    hachuMail.OdHachuMailID = System.Guid.NewGuid().ToString();
                    hachuMail.OdMkID = odMkId;
                    hachuMail.Tantousha = tantousha;
                    hachuMail.TantouMailAddress = mailAddress;
                    hachuMail.Subject = subject;
                    hachuMail.Bikou = bikou;
                    hachuMail.RenewDate = DateTime.Now;
                    hachuMail.RenewUserID = loginUser.MsUserID;
                    ret = hachuMail.InsertRecord(loginUser);
                }
                else
                {
                    hachuMail.Bikou = bikou;
                    hachuMail.RenewDate = DateTime.Now;
                    hachuMail.RenewUserID = loginUser.MsUserID;

                    ret = hachuMail.UpdateRecord(loginUser);
                }

                string toStr = "";
                //var tos = new List<EmailAddress>();
                {
                    toStr += ":" + mailAddress;
                    //tos.Add(new EmailAddress(mailAddress));
                }

                string body = 本文(TmpFileName);
                body = body.Replace("[見積依頼先]", customerName);
                body = body.Replace("[担当者]", tantousha);
                body = body.Replace("[発注番号]", hachuNo);
                body = body.Replace("[船舶名]", msVesselName);
                body = body.Replace("[見積依頼担当]", loginUser.FullName);

                // 注文書作成
                //byte[] excelData = BLC_PDF注文書_取得(odMkId, telNo, faxNo, bikou);
                byte[] excelData = _BLC_KK発注帳票_取得(loginUser, NBaseCommon.Hachu.Excel.発注帳票Common.kubun発注帳票.注文書, odMkId, true);
                string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
                pdfFileName = path + "注文書[" + loginUser.FullName + "].pdf";
                using (FileStream objFileStream = new FileStream(pdfFileName, FileMode.Create))
                {
                    objFileStream.Write(excelData, 0, excelData.Length);
                    objFileStream.Close();
                }
                //MySendGridClient.AttachFile pdfData = new MySendGridClient.AttachFile("注文書[" + hachuNo + "].pdf", pdfFileName);


                // 送信
                //MySendGridClient sgc = new MySendGridClient();
                //sgc.SendMail(tos, null, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, pdfData).Wait();
                //System.Net.HttpStatusCode sc = sgc.StatusCode;

                MailSendExeHelper.MakeDirections(toStr, null, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, "注文書[" + hachuNo + "].pdf", pdfFileName);
                MailSendExeHelper.Excecute();

                ret = true;
            }
            catch (Exception E)
            {
                errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + E.Message;
                ret = false;
            }
            #endregion

            return ret;
        }
    }
}
