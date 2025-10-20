using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using Hachu.Models;

namespace Hachu.BLC
{
    [Serializable]
    public class 手配依頼更新処理
    {
        public static bool 新規(ref OdThi 対象手配依頼, List<Item手配依頼品目> 手配品目s)
        {
            List<OdThiItem> 新規品目s = new List<OdThiItem>();
            List<OdThiShousaiItem> 新規詳細品目s = new List<OdThiShousaiItem>();
            List<OdAttachFile> 新規添付s = new List<OdAttachFile>();

            セット(ref 対象手配依頼, 手配品目s, ref 新規品目s, ref 新規詳細品目s, ref 新規添付s);

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_手配依頼更新処理_新規(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象手配依頼,
                                                        新規品目s,
                                                        新規詳細品目s,
                                                        新規添付s);

                // 2009.10.08:aki 山本さんの指示により、事務所でメール送信をしないようにコメントアウト
                //if (対象手配依頼.MailSend)
                //{
                //    string errMessage = "";
                //    serviceClient.BLC_燃料_潤滑油メール送信(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID, ref errMessage);
                //}

                対象手配依頼 = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID);
            }
            return ret;
        }

        public static bool 更新(ref OdThi 対象手配依頼, List<Item手配依頼品目> 手配品目s, List<Item手配依頼品目> 削除手配品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            List<OdThiItem> 新規品目s = new List<OdThiItem>();
            List<OdThiItem> 変更品目s = new List<OdThiItem>();
            List<OdThiItem> 削除品目s = new List<OdThiItem>();
            List<OdThiShousaiItem> 新規詳細品目s = new List<OdThiShousaiItem>();
            List<OdThiShousaiItem> 変更詳細品目s = new List<OdThiShousaiItem>();
            List<OdThiShousaiItem> 削除詳細品目s = new List<OdThiShousaiItem>();
            List<OdAttachFile> 添付s = new List<OdAttachFile>();

            #region
            対象手配依頼.RenewDate = RenewDate;
            対象手配依頼.RenewUserID = RenewUserID;

            // データに番号を付け直す
            int itemShowOrder = 0;
            foreach (Item手配依頼品目 品目 in 手配品目s)
            {
                品目.品目.ShowOrder = ++itemShowOrder;
                if (Hachu.Common.CommonDefine.Is新規(品目.品目.OdThiItemID) == false &&
                    Hachu.Common.CommonDefine.Is変更(品目.品目.OdThiItemID) == false && 
                    Hachu.Common.CommonDefine.Is削除(品目.品目.OdThiItemID) == false)
                {
                    品目.品目.OdThiItemID = Hachu.Common.CommonDefine.Prefix変更 + 品目.品目.OdThiItemID;
                }

                int shousaiShowOrder = 0;
                foreach (OdThiShousaiItem 詳細品目 in 品目.詳細品目s)
                {
                    詳細品目.ShowOrder = ++shousaiShowOrder;
                    if (Hachu.Common.CommonDefine.Is新規(詳細品目.OdThiShousaiItemID) == false &&
                        Hachu.Common.CommonDefine.Is変更(詳細品目.OdThiShousaiItemID) == false &&
                        Hachu.Common.CommonDefine.Is削除(詳細品目.OdThiShousaiItemID) == false)
                    {
                        詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.Prefix変更 + 詳細品目.OdThiShousaiItemID;
                    }
                }
            }


            // データの振り分け
            foreach (Item手配依頼品目 品目 in 手配品目s)
            {
                int sousaiCount = 0;

                if (Hachu.Common.CommonDefine.Is新規(品目.品目.OdThiItemID))
                {
                    OdThiItem 新規品目 = 品目.品目.Clone();
                    新規品目.OdThiItemID = Hachu.Common.CommonDefine.RemovePrefix(新規品目.OdThiItemID);
                    新規品目.OdThiID = 対象手配依頼.OdThiID;
                    新規品目.VesselID = 対象手配依頼.VesselID;
                    新規品目.RenewDate = RenewDate;
                    新規品目.RenewUserID = RenewUserID;
                    新規品目.OdThiShousaiItems.Clear();
                    // 2012.05.09:Add 8Lines
                    if (新規品目.OdAttachFileID != null && 新規品目.OdAttachFileID.Length > 0)
                    {
                        新規品目.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(新規品目.OdAttachFileID);
                    }
                    else
                    {
                        新規品目.OdAttachFileID = null;
                    }
                    //<--

                    新規品目s.Add(新規品目);

                    foreach (OdThiShousaiItem 詳細品目 in 品目.詳細品目s)
                    {
                        //===========================
                        // 2014.1 [2013年度改造]
                        //if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                        //{
                        //    if (詳細品目.CancelFlag != NBaseCommon.Common.CancelFlag_キャンセル && 詳細品目.Sateisu == 0)
                        //    {
                        //        continue;
                        //    }
                        //}
                        OdThiShousaiItem 新規詳細品目 = 詳細品目.Clone();
                        新規詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdThiShousaiItemID);
                        新規詳細品目.OdThiItemID = 新規品目.OdThiItemID;
                        新規詳細品目.Count = 新規詳細品目.Count;
                        新規詳細品目.Sateisu = 新規詳細品目.Sateisu;
                        新規詳細品目.Tanka = 新規詳細品目.Tanka;
                        新規詳細品目.VesselID = 対象手配依頼.VesselID;
                        新規詳細品目.RenewDate = RenewDate;
                        新規詳細品目.RenewUserID = RenewUserID;
                        // 2012.05.09:Add 8Lines
                        if (新規詳細品目.OdAttachFileID != null && 新規詳細品目.OdAttachFileID.Length > 0)
                        {
                            新規詳細品目.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdAttachFileID);
                        }
                        else
                        {
                            新規詳細品目.OdAttachFileID = null;
                        }
                        //<--

                        新規詳細品目s.Add(新規詳細品目);
                        sousaiCount++;
                    }

                    //===========================
                    // 2014.1 [2013年度改造]
                    //if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                    //{
                    //    if (sousaiCount == 0)
                    //    {
                    //        新規品目s.Remove(新規品目);
                    //    }
                    //}
                }
                else 
                {
                    OdThiItem 変更品目 = null;
                    if (Hachu.Common.CommonDefine.Is変更(品目.品目.OdThiItemID))
                    {
                        変更品目 = 品目.品目.Clone();
                        変更品目.OdThiItemID = Hachu.Common.CommonDefine.RemovePrefix(変更品目.OdThiItemID);
                        変更品目.RenewDate = RenewDate;
                        変更品目.RenewUserID = RenewUserID;
                        変更品目.OdThiShousaiItems.Clear();
                        // 2012.05.09:Add 8Lines
                        if (変更品目.OdAttachFileID != null && 変更品目.OdAttachFileID.Length > 0)
                        {
                            変更品目.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(変更品目.OdAttachFileID);
                        }
                        else
                        {
                            変更品目.OdAttachFileID = null;
                        }
                        //<--

                        変更品目s.Add(変更品目);
                    }
                    string 変更品目ID = Hachu.Common.CommonDefine.RemovePrefix(品目.品目.OdThiItemID);
                    foreach (OdThiShousaiItem 詳細品目 in 品目.詳細品目s)
                    {
                        if (Hachu.Common.CommonDefine.Is新規(詳細品目.OdThiShousaiItemID))
                        {
                            //===========================
                            // 2014.1 [2013年度改造]
                            //if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                            //{
                            //    if (詳細品目.CancelFlag != NBaseCommon.Common.CancelFlag_キャンセル && 詳細品目.Sateisu == 0)
                            //    {
                            //        continue;
                            //    }
                            //}
                            OdThiShousaiItem 新規詳細品目 = 詳細品目.Clone();
                            新規詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdThiShousaiItemID);
                            新規詳細品目.OdThiItemID = 変更品目ID;
                            新規詳細品目.Count = 新規詳細品目.Count;
                            新規詳細品目.Sateisu = 新規詳細品目.Sateisu;
                            新規詳細品目.Tanka = 新規詳細品目.Tanka;
                            新規詳細品目.VesselID = 対象手配依頼.VesselID;
                            新規詳細品目.RenewDate = RenewDate;
                            新規詳細品目.RenewUserID = RenewUserID;
                            // 2012.05.09:Add 8Lines
                            if (新規詳細品目.OdAttachFileID != null && 新規詳細品目.OdAttachFileID.Length > 0)
                            {
                                新規詳細品目.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdAttachFileID);
                            }
                            else
                            {
                                新規詳細品目.OdAttachFileID = null;
                            }
                            //<--

                            新規詳細品目s.Add(新規詳細品目);
                            sousaiCount++;
                        }
                        else if (Hachu.Common.CommonDefine.Is変更(詳細品目.OdThiShousaiItemID))
                        {
                            //===========================
                            // 2014.1 [2013年度改造]
                            //if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                            //{
                            //    if (詳細品目.CancelFlag != NBaseCommon.Common.CancelFlag_キャンセル && 詳細品目.Sateisu == 0)
                            //    {
                            //        詳細品目.CancelFlag = NBaseCommon.Common.CancelFlag_キャンセル;
                            //    }
                            //    else
                            //    {
                            //        sousaiCount++;
                            //    }
                            //}
                            //else
                            //{
                            //    sousaiCount++;
                            //}
                            sousaiCount++;

                            OdThiShousaiItem 変更詳細品目 = 詳細品目.Clone();
                            変更詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(変更詳細品目.OdThiShousaiItemID);
                            変更詳細品目.RenewDate = RenewDate;
                            変更詳細品目.RenewUserID = RenewUserID;
                            // 2012.05.09:Add 8Lines
                            if (変更詳細品目.OdAttachFileID != null && 変更詳細品目.OdAttachFileID.Length > 0)
                            {
                                変更詳細品目.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(変更詳細品目.OdAttachFileID);
                            }
                            else
                            {
                                変更詳細品目.OdAttachFileID = null;
                            }
                            //<--
                            変更詳細品目s.Add(変更詳細品目);
                        }
                    }
                    foreach (OdThiShousaiItem 詳細品目 in 品目.削除詳細品目s)
                    {
                        OdThiShousaiItem 削除詳細品目 = 詳細品目.Clone();
                        削除詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdThiShousaiItemID);
                        削除詳細品目.RenewDate = RenewDate;
                        削除詳細品目.RenewUserID = RenewUserID;

                        削除詳細品目s.Add(削除詳細品目);
                    }

                    //===========================
                    // 2014.1 [2013年度改造]
                    //if (変更品目 != null && 対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                    //{
                    //    if (sousaiCount == 0)
                    //    {
                    //        変更品目s.Remove(変更品目);
                    //    }
                    //}
                }
            }
            foreach (Item手配依頼品目 品目 in 削除手配品目s)
            {
                OdThiItem 削除品目 = 品目.品目.Clone();
                削除品目.OdThiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除品目.OdThiItemID);
                削除品目.RenewDate = RenewDate;
                削除品目.RenewUserID = RenewUserID;
                削除品目.OdThiShousaiItems.Clear();

                削除品目s.Add(削除品目);

                foreach (OdThiShousaiItem 詳細品目 in 品目.詳細品目s)
                {
                    OdThiShousaiItem 削除詳細品目 = 詳細品目.Clone();
                    削除詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdThiShousaiItemID);
                    削除詳細品目.RenewDate = RenewDate;
                    削除詳細品目.RenewUserID = RenewUserID;

                    削除詳細品目s.Add(削除詳細品目);
                }
                foreach (OdThiShousaiItem 詳細品目 in 品目.削除詳細品目s)
                {
                    OdThiShousaiItem 削除詳細品目 = 詳細品目.Clone();
                    削除詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdThiShousaiItemID);
                    削除詳細品目.RenewDate = RenewDate;
                    削除詳細品目.RenewUserID = RenewUserID;

                    削除詳細品目s.Add(削除詳細品目);
                }
            }
            #endregion

            foreach (Item手配依頼品目 品目 in 手配品目s)
            {
                foreach (OdAttachFile attach in 品目.添付Files)
                {
                    OdAttachFile 添付 = attach.Clone();
                    if (Hachu.Common.CommonDefine.Is新規(attach.OdAttachFileID))
                    {
                        添付.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(添付.OdAttachFileID);
                        添付.VesselID = 対象手配依頼.VesselID;
                        添付.RenewDate = RenewDate;
                        添付.RenewUserID = RenewUserID;

                        添付s.Add(添付);
                    }
                    else if (Hachu.Common.CommonDefine.Is変更(attach.OdAttachFileID))
                    {
                        添付.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(添付.OdAttachFileID);
                        添付.VesselID = 対象手配依頼.VesselID;
                        添付.RenewDate = RenewDate;
                        添付.RenewUserID = RenewUserID;

                        添付s.Add(添付);
                    }
                    if (attach.DeleteFlag == 1)
                    {
                        添付.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(添付.OdAttachFileID);
                        添付.VesselID = 対象手配依頼.VesselID;
                        添付.RenewDate = RenewDate;
                        添付.RenewUserID = RenewUserID;
                        添付.DeleteFlag = 1;

                        添付s.Add(添付);
                    }
                }

            }

            // 更新
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_手配依頼更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象手配依頼, 
                                                        新規品目s, 
                                                        変更品目s, 
                                                        削除品目s, 
                                                        新規詳細品目s, 
                                                        変更詳細品目s, 
                                                        削除詳細品目s,
                                                        添付s);

                // 2009.10.08:aki 山本さんの指示により、事務所でメール送信をしないようにコメントアウト
                //if (対象手配依頼.MailSend)
                //{
                //    string errMessage = "";
                //    serviceClient.BLC_燃料_潤滑油メール送信(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID, ref errMessage);
                //}

                対象手配依頼 = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID);
            }

            return ret;
        }
        
        public static bool 取消(ref OdThi 対象手配依頼)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            List<OdThiItem> 新規品目s = new List<OdThiItem>();
            List<OdThiItem> 変更品目s = new List<OdThiItem>();
            List<OdThiItem> 削除品目s = new List<OdThiItem>();
            List<OdThiShousaiItem> 新規詳細品目s = new List<OdThiShousaiItem>();
            List<OdThiShousaiItem> 変更詳細品目s = new List<OdThiShousaiItem>();
            List<OdThiShousaiItem> 削除詳細品目s = new List<OdThiShousaiItem>();
            List<OdAttachFile> 添付s = new List<OdAttachFile>();

            対象手配依頼.CancelFlag = NBaseCommon.Common.CancelFlag_キャンセル;
            対象手配依頼.RenewDate = RenewDate;
            対象手配依頼.RenewUserID = RenewUserID;

            // 更新
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                添付s = serviceClient.OdAttachFile_GetRecordsByOdThiID(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID);
                foreach (OdAttachFile odAttachFile in 添付s)
                {
                    odAttachFile.DeleteFlag = 1;
                    odAttachFile.RenewDate = RenewDate;
                    odAttachFile.RenewUserID = RenewUserID;
                }
                ret = serviceClient.BLC_手配依頼更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象手配依頼,
                                                        新規品目s,
                                                        変更品目s,
                                                        削除品目s,
                                                        新規詳細品目s,
                                                        変更詳細品目s,
                                                        削除詳細品目s,
                                                        添付s);

            }

            return ret;
        }

        public static void セット(ref OdThi 対象手配依頼, List<Item手配依頼品目> 手配品目s, ref List<OdThiItem> 新規品目s, ref List<OdThiShousaiItem> 新規詳細品目s, ref List<OdAttachFile> 新規添付s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            対象手配依頼.VesselID = 対象手配依頼.MsVesselID;
            対象手配依頼.RenewDate = RenewDate;
            対象手配依頼.RenewUserID = RenewUserID;

            int itemShowOrder = 0;
            foreach (Item手配依頼品目 品目 in 手配品目s)
            {
                if (Hachu.Common.CommonDefine.Is新規(品目.品目.OdThiItemID))
                {
                    OdThiItem 新規品目 = 品目.品目.Clone();
                    新規品目.OdThiItemID = Hachu.Common.CommonDefine.RemovePrefix(新規品目.OdThiItemID);
                    新規品目.OdThiID = 対象手配依頼.OdThiID;
                    新規品目.VesselID = 対象手配依頼.VesselID;
                    新規品目.RenewDate = RenewDate;
                    新規品目.RenewUserID = RenewUserID;
                    新規品目.ShowOrder = ++itemShowOrder;
                    新規品目.OdThiShousaiItems.Clear();
                    if (新規品目.OdAttachFileID != null && 新規品目.OdAttachFileID.Length > 0)
                    {
                        新規品目.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(新規品目.OdAttachFileID);
                    }
                    else
                    {
                        新規品目.OdAttachFileID = null;
                    }

                    新規品目s.Add(新規品目);

                    int sousaiCount = 0;
                    int shousaiShowOrder = 0;
                    foreach (OdThiShousaiItem 詳細品目 in 品目.詳細品目s)
                    {
                        //===========================
                        // 2014.1 [2013年度改造]
                        //if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                        //{
                        //    if (詳細品目.CancelFlag != NBaseCommon.Common.CancelFlag_キャンセル && 詳細品目.Sateisu == 0)
                        //    {
                        //        continue;
                        //    }
                        //}
                        OdThiShousaiItem 新規詳細品目 = 詳細品目.Clone();
                        新規詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdThiShousaiItemID);
                        新規詳細品目.OdThiItemID = 新規品目.OdThiItemID;
                        if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                        {
                            新規詳細品目.Count = 新規詳細品目.Sateisu;
                        }
                        else
                        {
                            新規詳細品目.Sateisu = 新規詳細品目.Count;
                        }
                        新規詳細品目.Tanka = 新規詳細品目.Tanka;
                        新規詳細品目.VesselID = 対象手配依頼.VesselID;
                        新規詳細品目.RenewDate = RenewDate;
                        新規詳細品目.RenewUserID = RenewUserID;
                        新規詳細品目.ShowOrder = ++shousaiShowOrder;
                        if (新規詳細品目.OdAttachFileID != null && 新規詳細品目.OdAttachFileID.Length > 0)
                        {
                            新規詳細品目.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdAttachFileID);
                        }
                        else
                        {
                            新規詳細品目.OdAttachFileID = null;
                        }

                        新規詳細品目s.Add(新規詳細品目);
                        sousaiCount++;
                    }

                    //===========================
                    // 2014.1 [2013年度改造]
                    //if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                    //{
                    //    if (sousaiCount == 0)
                    //    {
                    //        新規品目s.Remove(新規品目);
                    //    }
                    //}
                }
            }

            foreach (Item手配依頼品目 品目 in 手配品目s)
            {
                foreach(OdAttachFile attach in 品目.添付Files)
                {
                    if (Hachu.Common.CommonDefine.Is新規(attach.OdAttachFileID))
                    { 
                        OdAttachFile 新規添付 = attach.Clone();
                        新規添付.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(新規添付.OdAttachFileID);
                        新規添付.VesselID = 対象手配依頼.VesselID;
                        新規添付.RenewDate = RenewDate;                      
                        新規添付.RenewUserID = RenewUserID;

                        新規添付s.Add(新規添付);
                    }
                }

            }

        }    
    }
}
