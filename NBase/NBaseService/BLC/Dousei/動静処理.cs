using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using NBaseData.DAC;
using System.Net.Mail;
using NBaseUtil;
using NBaseCommon;
using SendGrid.Helpers.Mail;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_動静予定登録(MsUser loginUser, List<DjDousei> Douseis);
        
        [OperationContract]
        bool BLC_動静予定削除(MsUser loginUser, PtKanidouseiInfo kanidousei);
        
        [OperationContract]
        bool BLC_動静実績登録(MsUser loginUser, List<DjDousei> Douseis);

        [OperationContract]
        bool BLC_動静報告メール送信_同期用(
                    MsUser loginUser,
                    DjDouseiHoukoku houkoku);
    }
    public partial class Service
    {
        public bool BLC_動静予定登録(MsUser loginUser, List<DjDousei> Douseis)
        {
            NBaseData.BLC.動静処理 logic = new NBaseData.BLC.動静処理();

            return logic.動静予定登録(loginUser, Douseis);
        }

        public bool BLC_動静予定削除(MsUser loginUser, PtKanidouseiInfo kanidousei)
        {
            NBaseData.BLC.動静処理 logic = new NBaseData.BLC.動静処理();

            return logic.動静予定削除(loginUser, kanidousei);
        }

        public bool BLC_動静実績登録(MsUser loginUser, List<DjDousei> Douseis)
        {
            NBaseData.BLC.動静処理 logic = new NBaseData.BLC.動静処理();

            return logic.動静実績登録(loginUser, Douseis);
        }

        public bool BLC_動静報告メール送信_同期用(
                    MsUser loginUser,
                    DjDouseiHoukoku houkoku)
        {
            string errMessage = "";

            return BLC_動静報告メール送信(loginUser, houkoku, ref errMessage);
        }

        public bool BLC_動静報告メール送信(
                    MsUser loginUser,
                    DjDouseiHoukoku houkoku,
                    ref string errMessage)
        {
            string vesselName = "";
            LogFile.Write(loginUser.FullName, "動静報告メール");

            string TmpFileName = "動静報告メール.txt";

            bool ret = false;

            #region メール送信

            #region 旧コード
            //using (MailMessage msg = new MailMessage())
            //{
            //    try
            //    {
            //        NBaseData.DAC.MsVessel msVessel = NBaseData.DAC.MsVessel.GetRecordByMsVesselID(loginUser, houkoku.VesselID);
            //        vesselName = msVessel.VesselName;

            //        NBaseData.DAC.MsUser msUser = NBaseData.DAC.MsUser.GetRecordsByUserID(loginUser, houkoku.RenewUserID);

            //        List<NBaseData.DAC.MsBasho> portList = NBaseData.DAC.MsBasho.GetRecords(loginUser);
            //        MsBasho LeavePort = null;
            //        if (portList.Any(obj => obj.MsBashoId == houkoku.LeavePortID))
            //        {
            //            LeavePort = portList.Where(obj => obj.MsBashoId == houkoku.LeavePortID).First();
            //        }
            //        MsBasho destinationPort = null;
            //        if (portList.Any(obj => obj.MsBashoId == houkoku.DestinationPortID))
            //        {
            //            destinationPort = portList.Where(obj => obj.MsBashoId == houkoku.DestinationPortID).First();
            //        }

            //        string subject = "動静報告（" + msVessel.VesselName + "）";
            //        string sendFrom = 送信元();
            //        string body = 本文(TmpFileName);
            //        string sendToStr = null;
            //        if (System.Configuration.ConfigurationManager.AppSettings["SendTo動静報告"] != "")
            //        {
            //            sendToStr = System.Configuration.ConfigurationManager.AppSettings["SendTo動静報告"];
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
            //        msg.BodyEncoding = System.Text.Encoding.GetEncoding("iso-2022-jp");
            //        body = body.Replace("[船名]", msVessel.VesselName);
            //        body = body.Replace("[ログイン者]", msUser.FullName);

            //        body = body.Replace("[報告日時]", houkoku.HoukokuDate.ToShortDateString() + " " + houkoku.HoukokuDate.ToShortTimeString());
            //        body = body.Replace("[現在地]", houkoku.CurrentPlace);
            //        body = body.Replace("[出港地]", LeavePort != null ? LeavePort.BashoName : "");
            //        body = body.Replace("[出港日時]", houkoku.LeaveDate != DateTime.MinValue ? houkoku.LeaveDate.ToShortDateString() + " " + houkoku.LeaveDate.ToShortTimeString() : "");
            //        body = body.Replace("[仕向地]", destinationPort != null ? destinationPort.BashoName : "");
            //        body = body.Replace("[入港予定日時]", houkoku.ArrivalDate != DateTime.MinValue ? houkoku.ArrivalDate.ToShortDateString() + " " + houkoku.ArrivalDate.ToShortTimeString() : "");

            //        msg.Body = body;

            //        string hostName = "";
            //        if (送信チェック(ref hostName) == false)
            //        {
            //            errMessage = "メールサーバの設定がありません。管理者に確認してください。";

            //            ret = true;

            //            LogFile.Write(loginUser.FullName, errMessage);
            //        }
            //        else
            //        {
            //            MySmtpClient client = new MySmtpClient(hostName);
            //            client.Send(msg);

            //            ret = true;

            //            LogFile.Write(loginUser.FullName, "==>[" + vesselName + "]:送信");
            //        }
            //    }
            //    catch (Exception E)
            //    {
            //        errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + E.Message;

            //        ret = false;

            //        LogFile.Write(loginUser.FullName,  "==>[" + vesselName + "]:" + errMessage);
            //    }
            //}
            #endregion
            try
            {
                NBaseData.DAC.MsVessel msVessel = NBaseData.DAC.MsVessel.GetRecordByMsVesselID(loginUser, houkoku.VesselID);
                vesselName = msVessel.VesselName;

                NBaseData.DAC.MsUser msUser = NBaseData.DAC.MsUser.GetRecordsByUserID(loginUser, houkoku.RenewUserID);

                List<NBaseData.DAC.MsBasho> portList = NBaseData.DAC.MsBasho.GetRecords(loginUser);
                MsBasho LeavePort = null;
                if (portList.Any(obj => obj.MsBashoId == houkoku.LeavePortID))
                {
                    LeavePort = portList.Where(obj => obj.MsBashoId == houkoku.LeavePortID).First();
                }
                MsBasho destinationPort = null;
                if (portList.Any(obj => obj.MsBashoId == houkoku.DestinationPortID))
                {
                    destinationPort = portList.Where(obj => obj.MsBashoId == houkoku.DestinationPortID).First();
                }

                string sendToStr = null;
                if (System.Configuration.ConfigurationManager.AppSettings["SendTo動静報告"] != "")
                {
                    sendToStr = System.Configuration.ConfigurationManager.AppSettings["SendTo動静報告"];
                }
                List<SendTo> sendToList = 送信先(sendToStr);
                if (sendToList == null)
                {
                    throw new Exception("送信先が設定されていません。");
                }

                //var tos = new List<EmailAddress>();
                //foreach (SendTo sendTo in sendToList)
                //{
                //    tos.Add(new EmailAddress(sendTo.MailAddress, sendTo.Name));
                //}

                string subject = "動静報告（" + msVessel.VesselName + "）";

                string body = 本文(TmpFileName);
                body = body.Replace("[船名]", msVessel.VesselName);
                body = body.Replace("[ログイン者]", msUser.FullName);

                body = body.Replace("[報告日時]", houkoku.HoukokuDate.ToShortDateString() + " " + houkoku.HoukokuDate.ToShortTimeString());
                body = body.Replace("[現在地]", houkoku.CurrentPlace);
                body = body.Replace("[出港地]", LeavePort != null ? LeavePort.BashoName : "");
                body = body.Replace("[出港日時]", houkoku.LeaveDate != DateTime.MinValue ? houkoku.LeaveDate.ToShortDateString() + " " + houkoku.LeaveDate.ToShortTimeString() : "");
                body = body.Replace("[仕向地]", destinationPort != null ? destinationPort.BashoName : "");
                body = body.Replace("[入港予定日時]", houkoku.ArrivalDate != DateTime.MinValue ? houkoku.ArrivalDate.ToShortDateString() + " " + houkoku.ArrivalDate.ToShortTimeString() : "");


                // 送信
                //MySendGridClient sgc = new MySendGridClient();
                //sgc.SendMail(tos, null, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, null).Wait();
                //System.Net.HttpStatusCode sc = sgc.StatusCode;

                MailSendExeHelper.MakeDirections(sendToStr, null, EncodeMailHeader(subject, System.Text.Encoding.GetEncoding("iso-2022-jp")), body, null, null);
                MailSendExeHelper.Excecute();


                ret = true;

                LogFile.Write(loginUser.FullName, errMessage);
            }
            catch (Exception E)
            {
                errMessage = "メールの送信に失敗しました。\n致命的なエラーです。\n" + E.Message;

                ret = false;

                LogFile.Write(loginUser.FullName, "==>[" + vesselName + "]:" + errMessage);
            }

            #endregion

            return ret;
        }
    }
}