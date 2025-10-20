using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using Hachu.Models;

namespace Hachu.BLC
{
    [Serializable]
    public class 新規発注更新処理
    {
        public static bool 手配依頼から発注(bool isNew, ref OdThi 対象手配依頼, ref OdMm 対象見積依頼, ref OdMk 対象見積回答, ref OdJry 対象受領, List<Item手配依頼品目> 依頼品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            //===========================
            // 手配依頼の構築
            //===========================
            対象手配依頼.MsThiIraiStatusID = ((int)NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.発注済).ToString();
            対象手配依頼.RenewDate = RenewDate;
            対象手配依頼.RenewUserID = RenewUserID;

            //===========================
            // 見積依頼の構築
            //===========================
            対象見積依頼.RenewDate = RenewDate;
            対象見積依頼.RenewUserID = RenewUserID;

            //===========================
            // 見積回答の構築
            //===========================
            対象見積回答.HachuDate = RenewDate;
            対象見積回答.RenewDate = RenewDate;
            対象見積回答.RenewUserID = RenewUserID;

            //===========================
            // 受領の構築
            //===========================
            対象受領.OdJryID = Hachu.Common.CommonDefine.新規ID(false);
            対象受領.Status = 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value;
            対象受領.OdMkID = 対象見積回答.OdMkID;
            対象受領.JryNo = 対象見積回答.MkNo;
            対象受領.Amount = 対象見積回答.Amount;
            対象受領.NebikiAmount = 対象見積回答.MkAmount;
            対象受領.Tax = 対象見積回答.Tax;
            対象受領.VesselID = 対象見積回答.VesselID;
            対象受領.RenewDate = RenewDate;
            対象受領.RenewUserID = RenewUserID;

            対象受領.Carriage = 対象見積回答.Carriage;

            //===========================
            // 手配品目、手配詳細品目の構築, 受領品目、受領詳細品目の構築
            //===========================
            decimal Amount = 0;
            int itemShowOrder = 0;
            List<OdThiItem> 手配品目s = new List<OdThiItem>();
            List<OdThiShousaiItem> 手配詳細品目s = new List<OdThiShousaiItem>();
            List<OdJryItem> 受領品目s = new List<OdJryItem>();
            List<OdJryShousaiItem> 受領詳細品目s = new List<OdJryShousaiItem>();
            foreach (Item手配依頼品目 依頼品目 in 依頼品目s)
            {
                int shousaiCount = 0;

                if (依頼品目.品目.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    continue;

                OdThiItem 手配品目 = new OdThiItem();
                手配品目.OdThiItemID = Hachu.Common.CommonDefine.新規ID(false);
                手配品目.OdThiID = 対象手配依頼.OdThiID;
                手配品目.Header = 依頼品目.品目.Header;
                手配品目.MsItemSbtID = 依頼品目.品目.MsItemSbtID;
                手配品目.ItemName = 依頼品目.品目.ItemName;
                手配品目.Bikou = 依頼品目.品目.Bikou;
                手配品目.VesselID = 対象手配依頼.VesselID;
                手配品目.RenewDate = RenewDate;
                手配品目.RenewUserID = RenewUserID;
                手配品目.ShowOrder = ++itemShowOrder;

                手配品目s.Add(手配品目);

                OdJryItem 受領品目 = new OdJryItem();
                受領品目.OdJryItemID = Hachu.Common.CommonDefine.新規ID(false);
                受領品目.OdJryID = 対象受領.OdJryID;
                受領品目.Header = 依頼品目.品目.Header;
                受領品目.MsItemSbtID = 依頼品目.品目.MsItemSbtID;
                受領品目.ItemName = 依頼品目.品目.ItemName;
                受領品目.Bikou = 依頼品目.品目.Bikou;
                受領品目.VesselID = 対象受領.VesselID;
                受領品目.RenewDate = RenewDate;
                受領品目.RenewUserID = RenewUserID;
                受領品目.ShowOrder = itemShowOrder;

                受領品目s.Add(受領品目);

                int shousaiShowOrder = 0;
                foreach (OdThiShousaiItem 詳細品目 in 依頼品目.詳細品目s)
                {
                    if (詳細品目.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                        continue;

                    // 2014.2 2013年度改造
                    //if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                    //{
                    //    if (詳細品目.Sateisu == 0)
                    //    {
                    //        continue;
                    //    }
                    //}
                    shousaiCount++;

                    OdThiShousaiItem 手配詳細品目 = new OdThiShousaiItem();
                    手配詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.新規ID(false);
                    手配詳細品目.OdThiItemID = 手配品目.OdThiItemID;
                    手配詳細品目.ShousaiItemName = 詳細品目.ShousaiItemName;
                    手配詳細品目.MsVesselItemID = 詳細品目.MsVesselItemID;
                    手配詳細品目.MsLoID = 詳細品目.MsLoID;
                    手配詳細品目.Count = 詳細品目.Sateisu;
                    手配詳細品目.Sateisu = 詳細品目.Sateisu;
                    手配詳細品目.MsTaniID = 詳細品目.MsTaniID;
                    手配詳細品目.Tanka = 詳細品目.Tanka;
                    手配詳細品目.Bikou = 詳細品目.Bikou;
                    手配詳細品目.VesselID = 対象手配依頼.VesselID;
                    手配詳細品目.RenewDate = RenewDate;
                    手配詳細品目.RenewUserID = RenewUserID;
                    手配詳細品目.ShowOrder = ++shousaiShowOrder;

                    手配詳細品目s.Add(手配詳細品目);

                    OdJryShousaiItem 受領詳細品目 = new OdJryShousaiItem();
                    受領詳細品目.OdJryShousaiItemID = Hachu.Common.CommonDefine.新規ID(false);
                    受領詳細品目.OdJryItemID = 受領品目.OdJryItemID;
                    受領詳細品目.ShousaiItemName = 詳細品目.ShousaiItemName;
                    受領詳細品目.MsVesselItemID = 詳細品目.MsVesselItemID;
                    受領詳細品目.MsLoID = 詳細品目.MsLoID;
                    受領詳細品目.Count = 詳細品目.Sateisu;     // 査定数を発注数とする
                    //受領詳細品目.JryCount = 0; //  詳細品目.Sateisu;  // 同時に受領数とする: 受領数はDefault=0とする
                    受領詳細品目.JryCount = int.MinValue;
                    受領詳細品目.MsTaniID = 詳細品目.MsTaniID;
                    受領詳細品目.Tanka = 詳細品目.Tanka;
                    受領詳細品目.Bikou = 詳細品目.Bikou;
                    受領詳細品目.VesselID = 対象受領.VesselID;
                    受領詳細品目.RenewDate = RenewDate;
                    受領詳細品目.RenewUserID = RenewUserID;
                    受領詳細品目.ShowOrder = shousaiShowOrder;

                    受領詳細品目s.Add(受領詳細品目);

                    if (受領詳細品目.Count > 0 && 受領詳細品目.Tanka > 0)
                    {
                        Amount += 受領詳細品目.Count * 受領詳細品目.Tanka;
                    }
                }
                if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                {
                    if (shousaiCount == 0)
                    {
                        手配品目s.Remove(手配品目);
                        受領品目s.Remove(受領品目);
                    }
                }

            }
            対象見積回答.Amount = Amount;
            対象受領.Amount     = Amount;

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsKamoku kamoku = serviceClient.MsKamoku_GetRecordByHachuKamoku(NBaseCommon.Common.LoginUser, 対象手配依頼.MsThiIraiSbtID, 対象手配依頼.MsThiIraiShousaiID, 対象見積回答.MsNyukyoKamokuID);
                if (kamoku != null)
                {
                    対象受領.KamokuNo = kamoku.KamokuNo;
                    対象受領.UtiwakeKamokuNo = kamoku.UtiwakeKamokuNo;
                }
                if (isNew)
                {
                    ret = serviceClient.BLC_新規発注処理_新規(
                                                            NBaseCommon.Common.LoginUser,
                                                            対象手配依頼,
                                                            対象見積依頼,
                                                            対象見積回答,
                                                            対象受領,
                                                            手配品目s,
                                                            手配詳細品目s,
                                                            受領品目s,
                                                            受領詳細品目s);
                }
                else
                {
                    ret = serviceClient.BLC_新規発注処理_手配あり(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象手配依頼,
                                                        対象見積依頼,
                                                        対象見積回答,
                                                        対象受領,
                                                        手配品目s,
                                                        手配詳細品目s,
                                                        受領品目s,
                                                        受領詳細品目s);
                }
                // 2009.10.08:aki 山本さんの指示により、事務所でメール送信をしないようにコメントアウト
                //if (対象手配依頼.MailSend)
                //{
                //    string errMessage = "";
                //    serviceClient.BLC_燃料_潤滑油メール送信(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID, ref errMessage);
                //}

                対象手配依頼 = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID);
                対象見積依頼 = serviceClient.OdMm_GetRecord(NBaseCommon.Common.LoginUser, 対象見積依頼.OdMmID);
                対象見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 対象見積回答.OdMkID);
                対象受領 = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);
            }
            return ret;
        }

        public static bool 新規発注から発注(ref OdThi 対象手配依頼, ref OdMm 対象見積依頼, ref OdMk 対象見積回答, ref OdJry 対象受領, List<Item見積回答品目> 回答品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
            
            //===========================
            // 手配依頼の構築
            //===========================
            対象手配依頼.MsThiIraiStatusID = ((int)NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.発注済).ToString();
            対象手配依頼.RenewDate = RenewDate;
            対象手配依頼.RenewUserID = RenewUserID;
            
            //===========================
            // 見積依頼の構築
            //===========================
            対象見積依頼.OdThiID = 対象手配依頼.OdThiID;
            対象見積依頼.RenewDate   = RenewDate;
            対象見積依頼.RenewUserID = RenewUserID;
            
            //===========================
            // 見積回答の構築
            //===========================
            対象見積回答.OdMmID = 対象見積依頼.OdMmID;
            対象見積回答.RenewDate = RenewDate;
            対象見積回答.RenewUserID = RenewUserID;

            //===========================
            // 受領の構築
            //===========================
            対象受領.OdJryID      = Hachu.Common.CommonDefine.新規ID(false);
            対象受領.Status       = 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value;
            対象受領.OdMkID       = 対象見積回答.OdMkID;
            対象受領.JryNo        = 対象見積回答.MkNo;
            対象受領.Amount       = 対象見積回答.Amount;
            対象受領.NebikiAmount = 対象見積回答.MkAmount;
            対象受領.Tax          = 対象見積回答.Tax;
            対象受領.VesselID     = 対象見積回答.VesselID;
            対象受領.RenewDate    = RenewDate;
            対象受領.RenewUserID  = RenewUserID;

            対象受領.Carriage     = 対象見積回答.Carriage;

            //===========================
            // 手配品目、手配詳細品目の構築, 受領品目、受領詳細品目の構築
            //===========================
            int itemShowOrder = 0;
            List<OdThiItem> 手配品目s = new List<OdThiItem>();
            List<OdThiShousaiItem> 手配詳細品目s = new List<OdThiShousaiItem>();
            List<OdJryItem> 受領品目s = new List<OdJryItem>();
            List<OdJryShousaiItem> 受領詳細品目s = new List<OdJryShousaiItem>();
            foreach (Item見積回答品目 回答品目 in 回答品目s)
            {
                if (回答品目.品目.CancelFlag == 1)
                    continue;

                OdThiItem 手配品目 = new OdThiItem();
                手配品目.OdThiItemID = Hachu.Common.CommonDefine.新規ID(false);
                手配品目.OdThiID     = 対象手配依頼.OdThiID;
                手配品目.Header      = 回答品目.品目.Header;
                手配品目.MsItemSbtID = 回答品目.品目.MsItemSbtID;
                手配品目.ItemName    = 回答品目.品目.ItemName;
                手配品目.Bikou       = 回答品目.品目.Bikou;
                手配品目.VesselID    = 対象手配依頼.VesselID;
                手配品目.RenewDate   = RenewDate;
                手配品目.RenewUserID = RenewUserID;
                手配品目.ShowOrder   = ++itemShowOrder;

                手配品目s.Add(手配品目);

                OdJryItem 受領品目 = new OdJryItem();
                受領品目.OdJryItemID = Hachu.Common.CommonDefine.新規ID(false);
                受領品目.OdJryID     = 対象受領.OdJryID;
                受領品目.Header      = 回答品目.品目.Header;
                受領品目.MsItemSbtID = 回答品目.品目.MsItemSbtID;
                受領品目.ItemName    = 回答品目.品目.ItemName;
                受領品目.Bikou       = 回答品目.品目.Bikou;
                受領品目.VesselID    = 対象受領.VesselID;
                受領品目.RenewDate   = RenewDate;
                受領品目.RenewUserID = RenewUserID;
                受領品目.ShowOrder   = itemShowOrder;

                受領品目s.Add(受領品目);

                int shousaiShowOrder = 0;
                foreach (OdMkShousaiItem 詳細品目 in 回答品目.詳細品目s)
                {
                    if (詳細品目.CancelFlag == 1)
                        continue;

                    OdThiShousaiItem 手配詳細品目 = new OdThiShousaiItem();
                    手配詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.新規ID(false);
                    手配詳細品目.OdThiItemID        = 手配品目.OdThiItemID;
                    手配詳細品目.ShousaiItemName    = 詳細品目.ShousaiItemName;
                    手配詳細品目.MsVesselItemID     = 詳細品目.MsVesselItemID;
                    手配詳細品目.MsLoID             = 詳細品目.MsLoID;
                    手配詳細品目.Count              = 詳細品目.Count;
                    手配詳細品目.Sateisu            = 詳細品目.Count;
                    手配詳細品目.MsTaniID           = 詳細品目.MsTaniID;
                    手配詳細品目.Tanka              = 詳細品目.Tanka;
                    手配詳細品目.Bikou              = 詳細品目.Bikou;
                    手配詳細品目.VesselID           = 対象手配依頼.VesselID;
                    手配詳細品目.RenewDate          = RenewDate;
                    手配詳細品目.RenewUserID        = RenewUserID;
                    手配詳細品目.ShowOrder          = ++shousaiShowOrder;

                    手配詳細品目s.Add(手配詳細品目);

                    OdJryShousaiItem 受領詳細品目 = new OdJryShousaiItem();
                    受領詳細品目.OdJryShousaiItemID = Hachu.Common.CommonDefine.新規ID(false);
                    受領詳細品目.OdJryItemID        = 受領品目.OdJryItemID;
                    受領詳細品目.ShousaiItemName    = 詳細品目.ShousaiItemName;
                    受領詳細品目.MsVesselItemID     = 詳細品目.MsVesselItemID;
                    受領詳細品目.MsLoID             = 詳細品目.MsLoID;
                    受領詳細品目.Count              = 詳細品目.Count;
                    //受領詳細品目.JryCount           = 0; // 詳細品目.Count;  // 同時に受領数とする
                    受領詳細品目.JryCount           = int.MinValue;
                    受領詳細品目.MsTaniID           = 詳細品目.MsTaniID;
                    受領詳細品目.Tanka              = 詳細品目.Tanka;
                    受領詳細品目.Bikou              = 詳細品目.Bikou;
                    受領詳細品目.VesselID           = 対象受領.VesselID;
                    受領詳細品目.RenewDate          = RenewDate;
                    受領詳細品目.RenewUserID        = RenewUserID;
                    受領詳細品目.ShowOrder          = shousaiShowOrder;

                    受領詳細品目s.Add(受領詳細品目);
                }

            }

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsKamoku kamoku = serviceClient.MsKamoku_GetRecordByHachuKamoku(NBaseCommon.Common.LoginUser, 対象手配依頼.MsThiIraiSbtID, 対象手配依頼.MsThiIraiShousaiID, 対象見積回答.MsNyukyoKamokuID);
                if (kamoku != null)
                {
                    対象受領.KamokuNo = kamoku.KamokuNo;
                    対象受領.UtiwakeKamokuNo = kamoku.UtiwakeKamokuNo;
                }
                ret = serviceClient.BLC_新規発注処理_新規(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象手配依頼,
                                                        対象見積依頼,
                                                        対象見積回答,
                                                        対象受領,
                                                        手配品目s,
                                                        手配詳細品目s,
                                                        受領品目s,
                                                        受領詳細品目s);

                対象手配依頼 = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID);
                対象見積依頼 = serviceClient.OdMm_GetRecord(NBaseCommon.Common.LoginUser, 対象見積依頼.OdMmID);
                対象見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 対象見積回答.OdMkID);
                対象受領 = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);
            }
            return ret;
        }

        public static bool 一時保存(ref OdThi 対象手配依頼, ref OdMm 対象見積依頼, ref OdMk 対象見積回答, List<Item見積回答品目> 回答品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            //===========================
            // 手配依頼の構築
            //===========================
            対象手配依頼.MsThiIraiStatusID = ((int)NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.未対応).ToString();
            対象手配依頼.RenewDate = RenewDate;
            対象手配依頼.RenewUserID = RenewUserID;

            //===========================
            // 見積依頼の構築
            //===========================
            対象見積依頼.OdThiID = 対象手配依頼.OdThiID;
            対象見積依頼.RenewDate = RenewDate;
            対象見積依頼.RenewUserID = RenewUserID;

            //===========================
            // 見積回答の構築
            //===========================
            対象見積回答.OdMmID = 対象見積依頼.OdMmID;
            対象見積回答.RenewDate = RenewDate;
            対象見積回答.RenewUserID = RenewUserID;

            //===========================
            // 手配品目、手配詳細品目の構築, 受領品目、受領詳細品目の構築
            //===========================
            int itemShowOrder = 0;
            List<OdThiItem> 手配品目s = new List<OdThiItem>();
            List<OdThiShousaiItem> 手配詳細品目s = new List<OdThiShousaiItem>();
            foreach (Item見積回答品目 回答品目 in 回答品目s)
            {
                if (回答品目.品目.CancelFlag == 1)
                    continue;

                OdThiItem 手配品目 = new OdThiItem();
                手配品目.OdThiItemID = Hachu.Common.CommonDefine.新規ID(false);
                手配品目.OdThiID = 対象手配依頼.OdThiID;
                手配品目.Header = 回答品目.品目.Header;
                手配品目.MsItemSbtID = 回答品目.品目.MsItemSbtID;
                手配品目.ItemName = 回答品目.品目.ItemName;
                手配品目.Bikou = 回答品目.品目.Bikou;
                手配品目.VesselID = 対象手配依頼.VesselID;
                手配品目.RenewDate = RenewDate;
                手配品目.RenewUserID = RenewUserID;
                手配品目.ShowOrder = ++itemShowOrder;

                手配品目s.Add(手配品目);

                int shousaiShowOrder = 0;
                foreach (OdMkShousaiItem 詳細品目 in 回答品目.詳細品目s)
                {
                    if (詳細品目.CancelFlag == 1)
                        continue;

                    OdThiShousaiItem 手配詳細品目 = new OdThiShousaiItem();
                    手配詳細品目.OdThiShousaiItemID = Hachu.Common.CommonDefine.新規ID(false);
                    手配詳細品目.OdThiItemID = 手配品目.OdThiItemID;
                    手配詳細品目.ShousaiItemName = 詳細品目.ShousaiItemName;
                    手配詳細品目.MsVesselItemID = 詳細品目.MsVesselItemID;
                    手配詳細品目.MsLoID = 詳細品目.MsLoID;
                    手配詳細品目.Count = 詳細品目.Count;
                    手配詳細品目.Sateisu = 詳細品目.Count;
                    手配詳細品目.MsTaniID = 詳細品目.MsTaniID;
                    手配詳細品目.Tanka = 詳細品目.Tanka;
                    手配詳細品目.Bikou = 詳細品目.Bikou;
                    手配詳細品目.VesselID = 対象手配依頼.VesselID;
                    手配詳細品目.RenewDate = RenewDate;
                    手配詳細品目.RenewUserID = RenewUserID;
                    手配詳細品目.ShowOrder = ++shousaiShowOrder;

                    手配詳細品目s.Add(手配詳細品目);
                }

            }

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_新規発注処理_保存(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象手配依頼,
                                                        対象見積依頼,
                                                        対象見積回答,
                                                        手配品目s,
                                                        手配詳細品目s);

                対象手配依頼 = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID);
                対象見積依頼 = serviceClient.OdMm_GetRecord(NBaseCommon.Common.LoginUser, 対象見積依頼.OdMmID);
                対象見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 対象見積回答.OdMkID);
            }
            return ret;
        }

        public static bool 保存情報削除(OdThi 対象手配依頼, OdMm 対象見積依頼, OdMk 対象見積回答)
        {
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_新規発注処理_保存情報削除(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象手配依頼,
                                                        対象見積依頼,
                                                        対象見積回答);
            }
            return ret;
        }
    }
}
