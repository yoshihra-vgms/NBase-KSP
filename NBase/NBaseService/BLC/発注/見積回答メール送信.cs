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
        bool BLC_見積回答メール送信(
                    NBaseData.DAC.MsUser loginUser,
                    string odMkId,
                    ref string errMessage);

    }
    public partial class Service
    {
        public bool BLC_見積回答メール送信(
                    NBaseData.DAC.MsUser loginUser,
                    string odMkId,
                    ref string errMessage)
        {
            string TmpFileName = "見積回答メール.txt";
            bool ret = false;

            #region メール送信

            #region 旧コード
            //using (MailMessage msg = new MailMessage())
            //{
            //    try
            //    {
            //        NBaseData.DAC.OdMk mk = NBaseData.DAC.OdMk.GetRecord(loginUser, odMkId);
            //        NBaseData.DAC.OdThi thi = NBaseData.DAC.OdThi.GetRecordByOdMkID(loginUser, odMkId);
            //        NBaseData.DAC.MsUser jimUser = NBaseData.DAC.MsUser.GetRecordsByUserID(loginUser, thi.JimTantouID);

            //        // メール情報
            //        string subject = "見積回答（" + mk.MsCustomerName + ")";
            //        string sendFrom = 送信元();
            //        string body = 本文(TmpFileName);
            //        string baseURL = BaseURL();
            //        //string mailAddress = jimUser.MailAddress;
            //        string sendToStr = null;
            //        if (System.Configuration.ConfigurationManager.AppSettings["SendTo見積回答"] != "")
            //        {
            //            sendToStr = System.Configuration.ConfigurationManager.AppSettings["SendTo見積回答"];
            //        }
            //        List<SendTo> sendToList = 送信先(sendToStr);
            //        if (sendToList == null)
            //        {
            //            throw new Exception("送信先が設定されていません。");
            //        }
            //        step = "1";

            //        msg.Subject = EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp"));
            //        msg.From = new MailAddress(sendFrom);
            //        //msg.To.Add(new MailAddress(mailAddress));
            //        string sendToName = "";
            //        foreach (SendTo sendTo in sendToList)
            //        {
            //            sendToName += "," + sendTo.Name;
            //            msg.To.Add(new MailAddress(sendTo.MailAddress));
            //        }
            //        step = "2";

            //        msg.BodyEncoding = System.Text.Encoding.GetEncoding("iso-2022-jp");
            //        body = body.Replace("[事務担当者]", jimUser.FullName);
            //        body = body.Replace("[船名]", thi.VesselName);
            //        body = body.Replace("[種別]", thi.ThiIraiSbtName);
            //        body = body.Replace("[詳細種別]", thi.ThiIraiShousaiName);
            //        body = body.Replace("[取引先名]", mk.MsCustomerName);
            //        body = body.Replace("[見積依頼番号]", mk.MkNo);
            //        body = body.Replace("[件名]", thi.Naiyou);
            //        msg.Body = body;

            //        step = "3";

            //        // 送信
            //        string hostName = "";
            //        if (送信チェック(ref hostName) == false)
            //        {
            //            errMessage = "メールサーバの設定がありません。管理者に確認してください。";
            //            ret = true;
            //        }
            //        else
            //        {
            //            step = "4";
            //            MySmtpClient client = new MySmtpClient(hostName);
            //            client.Send(msg);
            //            ret = true;
            //        }
            //    }
            //    catch (Exception E)
            //    {
            //        errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + step + ":" + E.Message;
            //        ret = false;
            //    }
            //}
            #endregion

            try
            {
                NBaseData.DAC.OdMk mk = NBaseData.DAC.OdMk.GetRecord(loginUser, odMkId);
                NBaseData.DAC.OdThi thi = NBaseData.DAC.OdThi.GetRecordByOdMkID(loginUser, odMkId);
                NBaseData.DAC.MsUser jimUser = NBaseData.DAC.MsUser.GetRecordsByUserID(loginUser, thi.JimTantouID);


                //var tos = new List<EmailAddress>();
                if (jimUser != null && jimUser.MailAddress != null && jimUser.MailAddress.Length > 0)
                {
                    //tos.Add(new EmailAddress(jimUser.MailAddress, jimUser.FullName));
                    string toStr = jimUser.FullName + ":" + jimUser.MailAddress;

                    string subject = "見積回答（" + mk.MsCustomerName + ")";

                    string body = 本文(TmpFileName);
                    body = body.Replace("[事務担当者]", jimUser.FullName);
                    body = body.Replace("[船名]", thi.VesselName);
                    body = body.Replace("[種別]", thi.ThiIraiSbtName);
                    body = body.Replace("[詳細種別]", thi.ThiIraiShousaiName);
                    body = body.Replace("[取引先名]", mk.MsCustomerName);
                    body = body.Replace("[見積依頼番号]", mk.MkNo);
                    body = body.Replace("[件名]", thi.Naiyou);

                    // 送信
                    //MySendGridClient sgc = new MySendGridClient();
                    //sgc.SendMail(tos, null, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, null).Wait();
                    //System.Net.HttpStatusCode sc = sgc.StatusCode;

                    MailSendExeHelper.MakeDirections(toStr, null, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, null, null);
                    MailSendExeHelper.Excecute();

                    ret = true;
                }
            }
            catch (Exception E)
            {
                errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + ":" + E.Message;
                ret = false;
            }
            #endregion

            return ret;
        }

    }
}
