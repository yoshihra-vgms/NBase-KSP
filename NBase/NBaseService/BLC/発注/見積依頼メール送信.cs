
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
        bool BLC_見積依頼メール送信(
                    NBaseData.DAC.MsUser loginUser,
                    string odMkId,
                    string customerName,
                    string tantousha,
                    string subject,
                    string mailAddress,
                    string mkNo,
                    string mkKigen,
                    string webKey,
                    DateTime mmDate,
                    DateTime kiboubi,
                    ref string errMessage);

        [OperationContract]
        bool BLC_燃料_潤滑油メール送信_同期用(
                    NBaseData.DAC.MsUser loginUser,
                    string odThiID);

        [OperationContract]
        bool BLC_燃料_潤滑油メール送信(
                    NBaseData.DAC.MsUser loginUser,
                    string odThiID,
                    ref string errMessage);

        [OperationContract]
        bool BLC_手配依頼メール送信_同期用(
                    NBaseData.DAC.MsUser loginUser,
                    string odThiID);
        
        [OperationContract]
        bool BLC_燃料_潤滑油以外メール送信(
                    NBaseData.DAC.MsUser loginUser,
                    string odThiID,
                    ref string errMessage);
    }
    public partial class Service
    {
        public bool BLC_見積依頼メール送信(
                    NBaseData.DAC.MsUser loginUser,
                    string odMkId,
                    string customerName,
                    string tantousha,
                    string subject,
                    string mailAddress,
                    string mkNo,
                    string mkKigen,
                    string webKey,
                    DateTime mmDate,
                    DateTime kiboubi,
                    ref string errMessage)
        {
            string TmpFileName = "見積依頼メール.txt";
            bool ret = false;

            #region メール送信

            #region 旧コード
            //using (MailMessage msg = new MailMessage())
            //{
            //    try
            //    {
            //        string sendFrom = 送信元();
            //        string body = 本文(TmpFileName);
            //        string baseURL = BaseURL();

            //        msg.Subject = EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp"));
            //        msg.From = new MailAddress(sendFrom);
            //        msg.To.Add(new MailAddress(mailAddress));
            //        msg.BodyEncoding = System.Text.Encoding.GetEncoding("iso-2022-jp");
            //        body = body.Replace("[見積依頼先]", customerName);
            //        body = body.Replace("[担当者]", tantousha);
            //        body = body.Replace("[見積依頼番号]", mkNo);
            //        if (mkKigen.Length > 0)
            //        {
            //            body = body.Replace("[見積回答期限]", mkKigen);
            //        }
            //        else
            //        {
            //            body = body.Replace("見積回答期限:[見積回答期限]", "");
            //        }
            //        body = body.Replace("[URL]", baseURL + webKey);
            //        body = body.Replace("[URL]", "<" + baseURL + webKey + ">");
            //        body = body.Replace("[見積依頼担当]", loginUser.FullName);
            //        msg.Body = body;

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


            //             2010.01.06 aki 追加
            //            NBaseData.DAC.OdMk odMk = NBaseData.DAC.OdMk.GetRecord(loginUser, odMkId);
            //            odMk.Tantousha = tantousha;
            //            odMk.TantouMailAddress = mailAddress;
            //            odMk.MkKigen = mkKigen;
            //            odMk.WebKey = webKey;
            //            odMk.Kiboubi = kiboubi;
            //            odMk.MmDate = mmDate;
            //            odMk.RenewDate = DateTime.Now;
            //            odMk.RenewUserID = loginUser.MsUserID;
            //            odMk.CreateDate = DateTime.Now;
            //            odMk.CreateUserID = loginUser.MsUserID;
            //            odMk.UpdateRecord(loginUser);

            //        }

            //    }
            //    catch (Exception E)
            //    {
            //        errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + E.Message;
            //        ret = false;
            //    }
            //}
            #endregion

            try
            {           

                //var tos = new List<EmailAddress>();
                //{
                //    tos.Add(new EmailAddress(mailAddress));
                //}
                string sendToStr = ":" + mailAddress;

                string body = 本文(TmpFileName);
                string baseURL = BaseURL();

                body = body.Replace("[見積依頼先]", customerName);
                body = body.Replace("[担当者]", tantousha);
                body = body.Replace("[見積依頼番号]", mkNo);
                if (mkKigen.Length > 0)
                {
                    body = body.Replace("[見積回答期限]", mkKigen);
                }
                else
                {
                    body = body.Replace("見積回答期限:[見積回答期限]", "");
                }
                body = body.Replace("[URL]", "<" + baseURL + webKey + ">");
                body = body.Replace("[見積依頼担当]", loginUser.FullName);


                // 送信
                //MySendGridClient sgc = new MySendGridClient();
                //sgc.SendMail(tos, null, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, null).Wait();
                //System.Net.HttpStatusCode sc = sgc.StatusCode;

                //MailSendExeHelper.MakeDirections(sendToStr, null, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, null, null);
                MailSendExeHelper.MakeDirections(sendToStr, null, subject, body, null, null);
                MailSendExeHelper.Excecute();
                ret = true;


                NBaseData.DAC.OdMk odMk = NBaseData.DAC.OdMk.GetRecord(loginUser, odMkId);
                odMk.Tantousha = tantousha;
                odMk.TantouMailAddress = mailAddress;
                odMk.MkKigen = mkKigen;
                odMk.WebKey = webKey;
                odMk.Kiboubi = kiboubi;
                odMk.MmDate = mmDate;
                odMk.RenewDate = DateTime.Now;
                odMk.RenewUserID = loginUser.MsUserID;
                odMk.CreateDate = DateTime.Now;
                odMk.CreateUserID = loginUser.MsUserID;
                odMk.UpdateRecord(loginUser);
            }
            catch (Exception E)
            {
                errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + E.Message;
                ret = false;
            }
            #endregion

            return ret;
        }
        public bool BLC_燃料_潤滑油メール送信_同期用(
                    NBaseData.DAC.MsUser loginUser,
                    string odThiID)
        {
            string errMessage = "";
            return BLC_燃料_潤滑油メール送信(loginUser, odThiID, ref errMessage);
        }

        public bool BLC_燃料_潤滑油メール送信(
                    NBaseData.DAC.MsUser loginUser,
                    string odThiID,
                    ref string errMessage)
        {
            string TmpFileName = "燃料_潤滑油発注メール.txt";

            bool ret = false;

            #region メール送信

            #region 旧コード
            //using (MailMessage msg = new MailMessage())
            //{
            //    try
            //    {
            //        NBaseData.DAC.OdThi odThi = NBaseData.DAC.OdThi.GetRecord(loginUser, odThiID);
                    
            //        if (odThi.TehaiIraiNo == null || odThi.TehaiIraiNo.Length == 0)　// 手配依頼番号がない
            //        {
            //            errMessage = "送信対象となる手配依頼ではありません。";
            //            // ログ登録
            //            InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
            //            return true;
            //        }
            //        if (odThi.Status != odThi.OdStatusValue.Values[(int)NBaseData.DAC.OdThi.STATUS.手配依頼済].Value)　// 手配依頼済みではない
            //        {
            //            errMessage = "送信対象となる手配依頼ではありません。";
            //            // ログ登録
            //            InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
            //            return true;
            //        }

            //        NBaseData.DAC.OdThiBunkerMail bunkerMail = NBaseData.DAC.OdThiBunkerMail.GetRecord(loginUser, odThiID);
            //        if (bunkerMail != null)
            //        {
            //            errMessage = "すでに送信済みです。";
            //            // ログ登録
            //            InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
            //            return true;
            //        }


            //        List<NBaseData.DAC.OdThiShousaiItem> odThiShousaiItem = NBaseData.DAC.OdThiShousaiItem.GetRecordsByOdThiID(loginUser, odThiID);
            //        NBaseData.DAC.MsVessel msVessel = NBaseData.DAC.MsVessel.GetRecordByMsVesselID(loginUser, odThi.MsVesselID);

            //        if(!Is_送信対象(odThiShousaiItem))
            //        {
            //            return true;
            //        }

            //        string subject = "補油オーダーの件（" + odThi.VesselName + "）";
            //        string sendFrom = 送信元();
            //        string sendToStr = null;
            //        if (System.Configuration.ConfigurationManager.AppSettings["SendTo燃料"] != "")
            //        {
            //            sendToStr = System.Configuration.ConfigurationManager.AppSettings["SendTo燃料"];
            //        }
            //        List<SendTo> sendToList = 送信先(sendToStr);
            //        if (sendToList == null)
            //        {
            //            throw new Exception("送信先が設定されていません。");
            //        }

            //        msg.Subject = EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp"));
            //        msg.From = new MailAddress(sendFrom);
            //        string sendToName = "";
            //        foreach (SendTo sendTo in sendToList)
            //        {
            //            sendToName += "," + sendTo.Name;
            //            msg.To.Add(new MailAddress(sendTo.MailAddress));
            //        }
            //        if (msVessel != null && msVessel.MailAddress != null && msVessel.MailAddress.Length > 0)
            //        {
            //            msg.CC.Add(new MailAddress(msVessel.MailAddress));
            //        }
            //        msg.BodyEncoding = System.Text.Encoding.GetEncoding("iso-2022-jp");

            //        string body = 本文(TmpFileName);
            //        string requestBody = 依頼項目(odThiShousaiItem);
            //        body = body.Replace("[グループ名]", sendToName.Substring(1));
            //        body = body.Replace("[船]", odThi.VesselName);
            //        body = body.Replace("[手配依頼者]", odThi.ThiUserName);
            //        body = body.Replace("[手配内容]", odThi.Naiyou);
            //        body = body.Replace("[希望納期]", odThi.Kiboubi.ToShortDateString());
            //        body = body.Replace("[希望港]", odThi.Kiboukou);
            //        body = body.Replace("[備考]", odThi.Bikou);
            //        body = body.Replace("[依頼番号]", odThi.TehaiIraiNo);
            //        body = body.Replace("[依頼項目]", requestBody);

            //        msg.Body = body;

            //        string hostName = "";
            //        if (送信チェック(ref hostName) == false)
            //        {
            //            errMessage = "メールサーバの設定がありません。管理者に確認してください。";
            //            // ログ登録
            //            InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
            //            ret = true;
            //        }
            //        else
            //        {
            //            MySmtpClient client = new MySmtpClient(hostName);
            //            client.Send(msg);
            //            ret = true;


            //            // ログ登録
            //            InsertOdThiBunkerMail(loginUser, odThi, body.Replace('\n', ' ').Replace('\r', ' '), NBaseData.DAC.OdThiBunkerMail.送信済み);
            //        }          
            //    }
            //    catch (Exception E)
            //    {
            //        errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + E.Message;
            //        // ログ登録
            //        NBaseData.DAC.OdThi odThi = new NBaseData.DAC.OdThi();
            //        odThi.OdThiID = odThiID;
            //        InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
            //        ret = false;
            //    }
            //}
            #endregion



            try
            {
                NBaseData.DAC.OdThi odThi = NBaseData.DAC.OdThi.GetRecord(loginUser, odThiID);

                if (odThi.TehaiIraiNo == null || odThi.TehaiIraiNo.Length == 0)　// 手配依頼番号がない
                {
                    errMessage = "送信対象となる手配依頼ではありません。";
                    // ログ登録
                    InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
                    return true;
                }
                if (odThi.Status != odThi.OdStatusValue.Values[(int)NBaseData.DAC.OdThi.STATUS.手配依頼済].Value)　// 手配依頼済みではない
                {
                    errMessage = "送信対象となる手配依頼ではありません。";
                    // ログ登録
                    InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
                    return true;
                }

                NBaseData.DAC.OdThiBunkerMail bunkerMail = NBaseData.DAC.OdThiBunkerMail.GetRecord(loginUser, odThiID);
                if (bunkerMail != null)
                {
                    errMessage = "すでに送信済みです。";
                    // ログ登録
                    InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
                    return true;
                }


                List<NBaseData.DAC.OdThiShousaiItem> odThiShousaiItem = NBaseData.DAC.OdThiShousaiItem.GetRecordsByOdThiID(loginUser, odThiID);
                NBaseData.DAC.MsVessel msVessel = NBaseData.DAC.MsVessel.GetRecordByMsVesselID(loginUser, odThi.MsVesselID);

                if (!Is_送信対象(odThiShousaiItem))
                {
                    return true;
                }
                string sendToStr = null;
                if (System.Configuration.ConfigurationManager.AppSettings["SendTo燃料"] != "")
                {
                    sendToStr = System.Configuration.ConfigurationManager.AppSettings["SendTo燃料"];
                }
                List<SendTo> sendToList = 送信先(sendToStr);
                if (sendToList == null)
                {
                    throw new Exception("送信先が設定されていません。");
                }
                string sendToName = "";
                //var tos = new List<EmailAddress>();
                foreach (SendTo sendTo in sendToList)
                {
                    sendToName += "," + sendTo.Name;
                    //tos.Add(new EmailAddress(sendTo.MailAddress, sendTo.Name));
                }

                //var ccs = new List<EmailAddress>();
                var ccStr = "";
                if (msVessel != null && msVessel.MailAddress != null && msVessel.MailAddress.Length > 0)
                {
                    ccStr = msVessel.VesselName + ":" + msVessel.MailAddress;

                    //ccs.Add(new EmailAddress(msVessel.MailAddress, msVessel.VesselName));
                }

                string subject = "補油オーダーの件（" + odThi.VesselName + "）";

                string body = 本文(TmpFileName);
                string requestBody = 依頼項目(odThiShousaiItem);
                body = body.Replace("[グループ名]", sendToName.Substring(1));
                body = body.Replace("[船]", odThi.VesselName);
                body = body.Replace("[手配依頼者]", odThi.ThiUserName);
                body = body.Replace("[手配内容]", odThi.Naiyou);
                body = body.Replace("[希望納期]", odThi.Kiboubi.ToShortDateString());
                body = body.Replace("[希望港]", odThi.Kiboukou);
                body = body.Replace("[備考]", odThi.Bikou);
                body = body.Replace("[依頼番号]", odThi.TehaiIraiNo);
                body = body.Replace("[依頼項目]", requestBody);


                // 送信
                //MySendGridClient sgc = new MySendGridClient();
                //sgc.SendMail(tos, ccs, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, null).Wait();
                //System.Net.HttpStatusCode sc = sgc.StatusCode;

                MailSendExeHelper.MakeDirections(sendToStr, ccStr, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, null, null);
                MailSendExeHelper.Excecute();

                ret = true;

                // ログ登録
                InsertOdThiBunkerMail(loginUser, odThi, body.Replace('\n', ' ').Replace('\r', ' '), NBaseData.DAC.OdThiBunkerMail.送信済み);
            }
            catch (Exception E)
            {
                errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + E.Message;
                // ログ登録
                NBaseData.DAC.OdThi odThi = new NBaseData.DAC.OdThi();
                odThi.OdThiID = odThiID;
                InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
                ret = false;
            }
            #endregion

            return ret;
        }

        private void InsertOdThiBunkerMail(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdThi odThi, string msg, int sendFlag)
        {
            NBaseData.DAC.OdThiBunkerMail bunkerMail = new NBaseData.DAC.OdThiBunkerMail();
            if (odThi != null)
            {
                bunkerMail.OdThiID = odThi.OdThiID;
                bunkerMail.Status = odThi.Status;
                bunkerMail.MsVesselID = odThi.MsVesselID;
                bunkerMail.TehaiIraiNo = odThi.TehaiIraiNo;
                bunkerMail.ThiIraiDate = odThi.ThiIraiDate;
                bunkerMail.ThiRenewDate = odThi.RenewDate;
                bunkerMail.ThiRenewUserID = odThi.RenewUserID;
            }
            bunkerMail.RenewDate = DateTime.Now;
            bunkerMail.RenewUserID = loginUser.MsUserID;
            bunkerMail.MailBody = msg;
            bunkerMail.MailSendFlag = sendFlag;

            bunkerMail.InsertRecord(loginUser);
        }

        /// <summary>
        /// LO, FO が1件でも含まれるときにメール送信する。「その他」だけでは送信しない。
        /// </summary>
        /// <param name="shousaiItems"></param>
        /// <returns></returns>
        private bool Is_送信対象(List<NBaseData.DAC.OdThiShousaiItem> shousaiItems)
        {
            foreach (NBaseData.DAC.OdThiShousaiItem shousai in shousaiItems)
            {
                if (Is_FO_or_LO(shousai))
                {
                    return true;
                }
            }

            return false;
        }
       
        private static bool Is_FO_or_LO(NBaseData.DAC.OdThiShousaiItem shousai)
        {
            return (shousai.MsLoID == null || shousai.MsLoID.Length == 0 ||
                                shousai.MsLoID.StartsWith(NBaseCommon.Common.PrefixMsLoID_LO)) && shousai.Count > 0;
        }

        private bool Is_送信対象燃料_潤滑油以外(List<NBaseData.DAC.OdThiShousaiItem> shousaiItems)
        {
            foreach (NBaseData.DAC.OdThiShousaiItem shousai in shousaiItems)
            {
                if (shousai.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        
        private string EncodeMailHeader(string str, System.Text.Encoding enc)
        {
            //Base64でエンコードする
            string ret = System.Convert.ToBase64String(enc.GetBytes(str));
            //RFC2047形式に
            ret = string.Format("=?{0}?B?{1}?=", enc.BodyName, ret);
            return ret;
        }

        private bool 送信チェック(ref string hostName)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["メールサーバ"] == "")
            {
                return false;
            }
            hostName = System.Configuration.ConfigurationManager.AppSettings["メールサーバ"];
            return true;
        }

        private string 送信元()
        {
            string sendFrom = "";

            if (System.Configuration.ConfigurationManager.AppSettings["SendFrom"] != "")
            {
                sendFrom = System.Configuration.ConfigurationManager.AppSettings["SendFrom"];
            }

            return sendFrom;
        }

        private List<SendTo> 送信先(string sendToStr)
        {
            //string sendToStr = "";

            //if (System.Configuration.ConfigurationManager.AppSettings["sendTo"] != "")
            //{
            //    sendToStr = System.Configuration.ConfigurationManager.AppSettings["sendTo"];
            //}
            //if (sendToStr == "")
            //{
            //    return null;
            //}
            if (sendToStr == "")
            {
                return null;
            }
            List<SendTo> sendToList = new List<SendTo>();
            string[] sendToStrArray = sendToStr.Split(',');
            for (int i = 0; i < sendToStrArray.Length; i++)
            {
                try
                {
                    string[] tmp = sendToStrArray[i].Split(':');
                    SendTo st = new SendTo();
                    st.Name = tmp[0];
                    st.MailAddress = tmp[1];
                    sendToList.Add(st);
                }
                catch
                {
                }
            }
            return sendToList;
        }
        private string 本文(string tmpFileName)
        {
            string body = "";

            string FullPath = System.Configuration.ConfigurationManager.AppSettings["メールテンプレートディレクトリ"];
            FullPath += tmpFileName;
            using (StreamReader sr = new StreamReader(FullPath, Encoding.GetEncoding("Shift_Jis")))
            {
                body = sr.ReadToEnd();
            }

            return body;
        }
        private string BaseURL()
        {
            string baseURL = "";

            if (System.Configuration.ConfigurationManager.AppSettings["BaseURL"] != "")
            {
                baseURL = System.Configuration.ConfigurationManager.AppSettings["BaseURL"];
            }

            return baseURL;
        }
        private string 依頼項目(List<NBaseData.DAC.OdThiShousaiItem> shousaiItems)
        {
            StringBuilder sb = new StringBuilder();
            foreach (NBaseData.DAC.OdThiShousaiItem shousai in shousaiItems)
            {
                if (shousai.MsLoID == null || shousai.MsLoID.Length == 0)
                {
                    if (shousai.Count > 0)
                    {
                        sb.AppendLine(shousai.ShousaiItemName + ".FO:" + shousai.Count.ToString() + " K/L");
                    }
                }
            }
            StringBuilder sbLO = new StringBuilder();
            foreach (NBaseData.DAC.OdThiShousaiItem shousai in shousaiItems)
            {
                if (shousai.MsLoID.StartsWith(NBaseCommon.Common.PrefixMsLoID_LO))
                {
                    if (shousai.Count > 0)
                    {
                        sbLO.AppendLine(shousai.ShousaiItemName + ":" + shousai.Count.ToString() + " " + shousai.MsTaniName);
                    }
                }
            }
            if (sbLO.Length > 0)
            {
                sb.AppendLine();
                sb.AppendLine("潤滑油 ");
                sb.Append(sbLO);
            }
            return sb.ToString();
        }

        private class SendTo
        {
            public string Name;
            public string MailAddress;
        }





        public bool BLC_手配依頼メール送信_同期用(
                    NBaseData.DAC.MsUser loginUser,
                    string odThiID)
        {
            NBaseData.DAC.OdThi odThi = NBaseData.DAC.OdThi.GetRecord(loginUser, odThiID);
            string errMessage = "";
            if (odThi.MsThiIraiSbtID == NBaseData.DAC.MsThiIraiSbt.ToId(NBaseData.DAC.MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油))
            {
                return BLC_燃料_潤滑油メール送信(loginUser, odThiID, ref errMessage);
            }
            else
            {
                return BLC_燃料_潤滑油以外メール送信(loginUser, odThiID, ref errMessage);
            }
        }

        public bool BLC_燃料_潤滑油以外メール送信(
                    NBaseData.DAC.MsUser loginUser,
                    string odThiID,
                    ref string errMessage)
        {
            string TmpFileName = "手配依頼メール.txt";

            bool ret = false;

            #region メール送信

            #region 旧コード
            //using (MailMessage msg = new MailMessage())
            //{
            //    try
            //    {
            //        NBaseData.DAC.OdThi odThi = NBaseData.DAC.OdThi.GetRecord(loginUser, odThiID);

            //        // 2010.04.26:aki メール送信のチェック機能を追加
            //        if (odThi.TehaiIraiNo == null || odThi.TehaiIraiNo.Length == 0)　// 手配依頼番号がない
            //        {
            //            errMessage = "送信対象となる手配依頼ではありません。";
            //            // ログ登録
            //            InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
            //            return true;
            //        }
            //        if (odThi.Status != odThi.OdStatusValue.Values[(int)NBaseData.DAC.OdThi.STATUS.手配依頼済].Value)　// 手配依頼済みではない
            //        {
            //            errMessage = "送信対象となる手配依頼ではありません。";
            //            // ログ登録
            //            InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
            //            return true;
            //        }

            //        NBaseData.DAC.OdThiBunkerMail bunkerMail = NBaseData.DAC.OdThiBunkerMail.GetRecord(loginUser, odThiID);
            //        if (bunkerMail != null)
            //        {
            //            errMessage = "すでに送信済みです。";
            //            // ログ登録
            //            InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
            //            return true;
            //        }

            //        List<NBaseData.DAC.OdThiShousaiItem> odThiShousaiItem = NBaseData.DAC.OdThiShousaiItem.GetRecordsByOdThiID(loginUser, odThiID);
            //        NBaseData.DAC.MsVessel msVessel = NBaseData.DAC.MsVessel.GetRecordByMsVesselID(loginUser, odThi.MsVesselID);

            //        if (!Is_送信対象燃料_潤滑油以外(odThiShousaiItem))
            //        {
            //            return true;
            //        }

            //        string subject = "";
            //        if (odThi.MsThiIraiSbtID == NBaseData.DAC.MsThiIraiSbt.ToId(NBaseData.DAC.MsThiIraiSbt.ThiIraiSbtEnum.船用品))
            //        {
            //            subject = "船用品オーダーの件（" + odThi.VesselName + "）";
            //        }
            //        else
            //        {
            //            string name = NBaseData.DAC.MsThiIraiShousai.ToName(odThi.MsThiIraiShousaiID);
            //            subject = name + "オーダーの件（" + odThi.VesselName + "）";
            //        }
            //        string sendFrom = 送信元();
            //        string body = 本文(TmpFileName);
            //        string sendToStr = null;
            //        if (System.Configuration.ConfigurationManager.AppSettings["SendTo燃料以外"] != "")
            //        {
            //            sendToStr = System.Configuration.ConfigurationManager.AppSettings["SendTo燃料以外"];
            //        }
            //        List<SendTo> sendToList = 送信先(sendToStr);
            //        if (sendToList == null)
            //        {
            //            throw new Exception("送信先が設定されていません。");
            //        }

            //        msg.Subject = EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp"));
            //        msg.From = new MailAddress(sendFrom);
            //        string sendToName = "";
            //        foreach (SendTo sendTo in sendToList)
            //        {
            //            sendToName += "," + sendTo.Name;
            //            msg.To.Add(new MailAddress(sendTo.MailAddress));
            //        }

            //        // 2012.10.11 
            //        // 一度、コメントにしているのだが「最初から船にも送る話になっていた」とのことなので…有効へ
            //        if (msVessel != null && msVessel.MailAddress != null && msVessel.MailAddress.Length > 0)
            //        {
            //            msg.CC.Add(new MailAddress(msVessel.MailAddress));
            //        }
            //        msg.BodyEncoding = System.Text.Encoding.GetEncoding("iso-2022-jp");
            //        body = body.Replace("[グループ名]", sendToName.Substring(1));
            //        body = body.Replace("[船]", odThi.VesselName);
            //        body = body.Replace("[手配依頼者]", odThi.ThiUserName);
            //        body = body.Replace("[手配内容]", odThi.Naiyou);
            //        body = body.Replace("[依頼番号]", odThi.TehaiIraiNo);
            //        body = body.Replace("[備考]", odThi.Bikou);

            //        msg.Body = body;

            //        string hostName = "";
            //        if (送信チェック(ref hostName) == false)
            //        {
            //            errMessage = "メールサーバの設定がありません。管理者に確認してください。";
            //            // ログ登録
            //            InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
            //            ret = true;
            //        }
            //        else
            //        {
            //            MySmtpClient client = new MySmtpClient(hostName);
            //            client.Send(msg);
            //            ret = true;

            //            // ログ登録
            //            InsertOdThiBunkerMail(loginUser, odThi, body.Replace('\n', ' ').Replace('\r', ' '), NBaseData.DAC.OdThiBunkerMail.送信済み);
            //        }
            //    }
            //    catch (Exception E)
            //    {
            //        errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + E.Message;
            //        // ログ登録
            //        NBaseData.DAC.OdThi odThi = new NBaseData.DAC.OdThi();
            //        odThi.OdThiID = odThiID;
            //        InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
            //        ret = false;
            //    }
            //}
            #endregion

            try
            {
                NBaseData.DAC.OdThi odThi = NBaseData.DAC.OdThi.GetRecord(loginUser, odThiID);

                if (odThi.TehaiIraiNo == null || odThi.TehaiIraiNo.Length == 0)　// 手配依頼番号がない
                {
                    errMessage = "送信対象となる手配依頼ではありません。";
                    // ログ登録
                    InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
                    return true;
                }
                if (odThi.Status != odThi.OdStatusValue.Values[(int)NBaseData.DAC.OdThi.STATUS.手配依頼済].Value)　// 手配依頼済みではない
                {
                    errMessage = "送信対象となる手配依頼ではありません。";
                    // ログ登録
                    InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
                    return true;
                }

                NBaseData.DAC.OdThiBunkerMail bunkerMail = NBaseData.DAC.OdThiBunkerMail.GetRecord(loginUser, odThiID);
                if (bunkerMail != null)
                {
                    errMessage = "すでに送信済みです。";
                    // ログ登録
                    InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
                    return true;
                }

                List<NBaseData.DAC.OdThiShousaiItem> odThiShousaiItem = NBaseData.DAC.OdThiShousaiItem.GetRecordsByOdThiID(loginUser, odThiID);
                NBaseData.DAC.MsVessel msVessel = NBaseData.DAC.MsVessel.GetRecordByMsVesselID(loginUser, odThi.MsVesselID);

                if (!Is_送信対象燃料_潤滑油以外(odThiShousaiItem))
                {
                    return true;
                }

                string sendToStr = null;
                if (System.Configuration.ConfigurationManager.AppSettings["SendTo燃料以外"] != "")
                {
                    sendToStr = System.Configuration.ConfigurationManager.AppSettings["SendTo燃料以外"];
                }
                List<SendTo> sendToList = 送信先(sendToStr);
                if (sendToList == null)
                {
                    throw new Exception("送信先が設定されていません。");
                }
                string sendToName = "";
                //var tos = new List<EmailAddress>();
                foreach (SendTo sendTo in sendToList)
                {
                    sendToName += "," + sendTo.Name;
                    //tos.Add(new EmailAddress(sendTo.MailAddress, sendTo.Name));
                }

                //var ccs = new List<EmailAddress>();
                var ccStr = "";
                if (msVessel != null && msVessel.MailAddress != null && msVessel.MailAddress.Length > 0)
                {
                    ccStr = msVessel.VesselName + ":" + msVessel.MailAddress;
                    //ccs.Add(new EmailAddress(msVessel.MailAddress, msVessel.VesselName));
                }

                string subject = "";
                if (odThi.MsThiIraiSbtID == NBaseData.DAC.MsThiIraiSbt.ToId(NBaseData.DAC.MsThiIraiSbt.ThiIraiSbtEnum.船用品))
                {
                    subject = "船用品オーダーの件（" + odThi.VesselName + "）";
                }
                else
                {
                    string name = NBaseData.DAC.MsThiIraiShousai.ToName(odThi.MsThiIraiShousaiID);
                    subject = name + "オーダーの件（" + odThi.VesselName + "）";
                }

                string body = 本文(TmpFileName);
                body = body.Replace("[グループ名]", sendToName.Substring(1));
                body = body.Replace("[船]", odThi.VesselName);
                body = body.Replace("[手配依頼者]", odThi.ThiUserName);
                body = body.Replace("[手配内容]", odThi.Naiyou);
                body = body.Replace("[依頼番号]", odThi.TehaiIraiNo);
                body = body.Replace("[備考]", odThi.Bikou);

                // 送信
                //MySendGridClient sgc = new MySendGridClient();
                //sgc.SendMail(tos, ccs, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, null).Wait();
                //System.Net.HttpStatusCode sc = sgc.StatusCode;

                MailSendExeHelper.MakeDirections(sendToStr, ccStr, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, null, null);
                MailSendExeHelper.Excecute();

                ret = true;

                // ログ登録
                InsertOdThiBunkerMail(loginUser, odThi, body.Replace('\n', ' ').Replace('\r', ' '), NBaseData.DAC.OdThiBunkerMail.送信済み);

            }
            catch (Exception E)
            {
                errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + E.Message;
                // ログ登録
                NBaseData.DAC.OdThi odThi = new NBaseData.DAC.OdThi();
                odThi.OdThiID = odThiID;
                InsertOdThiBunkerMail(loginUser, odThi, errMessage, NBaseData.DAC.OdThiBunkerMail.未送信);
                ret = false;
            }
            #endregion

            return ret;
        }


    }
}
