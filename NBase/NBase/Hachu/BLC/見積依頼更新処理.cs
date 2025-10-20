using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using Hachu.Models;

namespace Hachu.BLC
{
    [Serializable]
    public class 見積依頼更新処理
    {

        public static bool 手配依頼から作成(ref OdThi 手配依頼, ref OdMm 見積依頼, List<Item手配依頼品目> 手配品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            //===========================
            // 見積依頼の構築
            //===========================
            セット(ref 見積依頼, 手配依頼);

            //===========================
            // 見積依頼品目、見積依頼詳細品目の構築
            //===========================
            List<OdMmItem> 見積品目s = new List<OdMmItem>();
            List<OdMmShousaiItem> 見積詳細品目s = new List<OdMmShousaiItem>();
            foreach (Item手配依頼品目 手配品目 in 手配品目s)
            {
                if (手配品目.品目.CancelFlag == 1)
                    continue;

                OdMmItem 見積品目       = new OdMmItem();
                見積品目.OdMmItemID     = Hachu.Common.CommonDefine.新規ID(false);
                見積品目.OdMmID         = 見積依頼.OdMmID;
                見積品目.Header         = 手配品目.品目.Header;
                見積品目.MsItemSbtID    = 手配品目.品目.MsItemSbtID;
                見積品目.ItemName       = 手配品目.品目.ItemName;
                見積品目.Bikou          = 手配品目.品目.Bikou;
                見積品目.VesselID       = 手配依頼.VesselID;
                見積品目.RenewDate      = RenewDate;
                見積品目.RenewUserID    = RenewUserID;
                見積品目.ShowOrder      = 手配品目.品目.ShowOrder;
                //見積品目.OdAttachFileID = 手配品目.品目.OdAttachFileID;
                // 2012.05.09:Add 8Lines
                if (手配品目.品目.OdAttachFileID != null && 手配品目.品目.OdAttachFileID.Length > 0)
                {
                    見積品目.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(手配品目.品目.OdAttachFileID);
                }
                else
                {
                    見積品目.OdAttachFileID = null;
                }
                //<--

                見積品目s.Add(見積品目);

                foreach (OdThiShousaiItem 詳細品目 in 手配品目.詳細品目s)
                {
                    if (詳細品目.CancelFlag == 1)
                        continue;

                    OdMmShousaiItem 見積詳細品目    = new OdMmShousaiItem();
                    見積詳細品目.OdMmShousaiItemID  = Hachu.Common.CommonDefine.新規ID(false);
                    見積詳細品目.OdMmItemID         = 見積品目.OdMmItemID;
                    見積詳細品目.ShousaiItemName    = 詳細品目.ShousaiItemName;
                    見積詳細品目.MsVesselItemID     = 詳細品目.MsVesselItemID;
                    見積詳細品目.MsLoID             = 詳細品目.MsLoID;
                    見積詳細品目.Count              = 詳細品目.Sateisu;  // 見積詳細品目の数量は、手配詳細品目の査定数
                    見積詳細品目.MsTaniID           = 詳細品目.MsTaniID;
                    見積詳細品目.Bikou              = 詳細品目.Bikou;
                    見積詳細品目.VesselID           = 手配依頼.VesselID;
                    見積詳細品目.RenewDate          = RenewDate;
                    見積詳細品目.RenewUserID        = RenewUserID;
                    見積詳細品目.ShowOrder          = 詳細品目.ShowOrder;
                    //見積詳細品目.OdAttachFileID     = 詳細品目.OdAttachFileID;
                    // 2012.05.09:Add 8Lines
                    if (詳細品目.OdAttachFileID != null && 詳細品目.OdAttachFileID.Length > 0)
                    {
                        見積詳細品目.OdAttachFileID = Hachu.Common.CommonDefine.RemovePrefix(詳細品目.OdAttachFileID);
                    }
                    else
                    {
                        見積詳細品目.OdAttachFileID = null;
                    }
                    //<--

                    見積詳細品目s.Add(見積詳細品目);
                }

            }

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_見積依頼更新処理_新規(
                                                        NBaseCommon.Common.LoginUser,
                                                        見積依頼,
                                                        見積品目s,
                                                        見積詳細品目s);
                見積依頼 = serviceClient.OdMm_GetRecord(NBaseCommon.Common.LoginUser, 見積依頼.OdMmID);
                手配依頼 = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, 手配依頼.OdThiID);
            }

            return ret;
        }

        public static bool 更新(ref OdMm 対象見積依頼)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            対象見積依頼.RenewDate      = RenewDate;
            対象見積依頼.RenewUserID    = RenewUserID;

            // 更新
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_見積依頼更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象見積依頼);
                対象見積依頼 = serviceClient.OdMm_GetRecord(NBaseCommon.Common.LoginUser, 対象見積依頼.OdMmID);
            }

            return ret;
        }

        public static bool 取消(ref OdThi 手配依頼, ref OdMm 見積依頼)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            見積依頼.CancelFlag  = NBaseCommon.Common.CancelFlag_キャンセル;
            見積依頼.RenewDate   = RenewDate;
            見積依頼.RenewUserID = RenewUserID;

            // 更新
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_見積依頼更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        見積依頼);
                手配依頼 = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, 手配依頼.OdThiID);
            }

            return ret;
        }

        public static void セット(ref OdMm 見積依頼, OdThi 手配依頼)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            見積依頼.OdMmID         = Hachu.Common.CommonDefine.新規ID(false);
            見積依頼.Status         = 見積依頼.OdStatusValue.Values[(int)OdMm.STATUS.見積依頼].Value;
            見積依頼.MmNo           = 手配依頼.TehaiIraiNo;
            見積依頼.OdThiID        = 手配依頼.OdThiID;
            見積依頼.MmDate         = RenewDate;
            見積依頼.MmSakuseisha   = RenewUserID;
            見積依頼.VesselID       = 手配依頼.VesselID;
            見積依頼.RenewDate      = RenewDate;
            見積依頼.RenewUserID    = RenewUserID;
        }
    }
}
