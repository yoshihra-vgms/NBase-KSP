using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using Hachu.Models;

namespace Hachu.BLC
{
    [Serializable]
    public class 見積回答更新処理
    {
        public static bool 新規(ref OdMk 見積回答, List<Item見積回答品目> 見積品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            //===========================
            // 見積回答の構築
            //===========================
            セット(ref 見積回答, null);

            //===========================
            // 見積回答品目、見積回答詳細品目の構築
            //===========================
            List<OdMkItem> 見積回答品目s = new List<OdMkItem>();
            List<OdMkShousaiItem> 見積回答詳細品目s = new List<OdMkShousaiItem>();

            int itemShowOrder = 0;
            foreach (Item見積回答品目 見積回答品目 in 見積品目s)
            {
                OdMkItem mkItem = 見積回答品目.品目 as OdMkItem;
                mkItem.OdMkItemID = Hachu.Common.CommonDefine.新規ID(false);
                mkItem.OdMkID = 見積回答.OdMkID;
                mkItem.VesselID = 見積回答.VesselID;
                mkItem.RenewDate = RenewDate;
                mkItem.RenewUserID = RenewUserID;
                mkItem.ShowOrder = ++itemShowOrder;

                見積回答品目s.Add(mkItem);

                int shousaiShowOrder = 0;
                foreach (OdMkShousaiItem 詳細品目 in 見積回答品目.詳細品目s)
                {
                    詳細品目.OdMkShousaiItemID = Hachu.Common.CommonDefine.新規ID(false);
                    詳細品目.OdMkItemID = 見積回答品目.品目.OdMkItemID;
                    詳細品目.VesselID = 見積回答.VesselID;
                    詳細品目.RenewDate = RenewDate;
                    詳細品目.RenewUserID = RenewUserID;
                    詳細品目.ShowOrder = ++shousaiShowOrder;

                    見積回答詳細品目s.Add(詳細品目);
                }

            }

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_見積回答更新処理_新規(
                                                        NBaseCommon.Common.LoginUser,
                                                        見積回答,
                                                        見積回答品目s,
                                                        見積回答詳細品目s);

                見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 見積回答.OdMkID);
            }

            return ret;
        }

        public static bool 更新(ref OdMk 対象見積回答, List<Item見積回答品目> 回答品目s, List<Item見積回答品目> 削除回答品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            List<OdMkItem> 新規品目s = new List<OdMkItem>();
            List<OdMkItem> 変更品目s = new List<OdMkItem>();
            List<OdMkItem> 削除品目s = new List<OdMkItem>();
            List<OdMkShousaiItem> 新規詳細品目s = new List<OdMkShousaiItem>();
            List<OdMkShousaiItem> 変更詳細品目s = new List<OdMkShousaiItem>();
            List<OdMkShousaiItem> 削除詳細品目s = new List<OdMkShousaiItem>();

            // 更新データの振り分け
            #region
            対象見積回答.RenewDate = RenewDate;
            対象見積回答.RenewUserID = RenewUserID;

            foreach (Item見積回答品目 品目 in 回答品目s)
            {

                if (Hachu.Common.CommonDefine.Is新規(品目.品目.OdMkItemID))
                {
                    OdMkItem 新規品目 = 品目.品目;
                    新規品目.OdMkItemID = Hachu.Common.CommonDefine.RemovePrefix(新規品目.OdMkItemID);
                    新規品目.OdMkID = 対象見積回答.OdMkID;
                    新規品目.VesselID = 対象見積回答.VesselID;
                    新規品目.RenewDate = RenewDate;
                    新規品目.RenewUserID = RenewUserID;

                    新規品目s.Add(新規品目);

                    foreach (OdMkShousaiItem 詳細品目 in 品目.詳細品目s)
                    {
                        OdMkShousaiItem 新規詳細品目 = 詳細品目;
                        新規詳細品目.OdMkShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdMkShousaiItemID);
                        新規詳細品目.OdMkItemID = 新規品目.OdMkItemID;
                        新規詳細品目.VesselID = 対象見積回答.VesselID;
                        新規詳細品目.RenewDate = RenewDate;
                        新規詳細品目.RenewUserID = RenewUserID;

                        新規詳細品目s.Add(新規詳細品目);
                    }
                }
                else
                {
                    if (Hachu.Common.CommonDefine.Is変更(品目.品目.OdMkItemID))
                    {
                        OdMkItem 変更品目 = 品目.品目;
                        変更品目.OdMkItemID = Hachu.Common.CommonDefine.RemovePrefix(変更品目.OdMkItemID);
                        変更品目.RenewDate = RenewDate;
                        変更品目.RenewUserID = RenewUserID;

                        変更品目s.Add(変更品目);
                    }
                    string 変更品目ID = Hachu.Common.CommonDefine.RemovePrefix(品目.品目.OdMkItemID);
                    foreach (OdMkShousaiItem 詳細品目 in 品目.詳細品目s)
                    {
                        if (Hachu.Common.CommonDefine.Is新規(詳細品目.OdMkShousaiItemID))
                        {
                            OdMkShousaiItem 新規詳細品目 = 詳細品目;
                            新規詳細品目.OdMkShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdMkShousaiItemID);
                            新規詳細品目.OdMkItemID = 変更品目ID;
                            新規詳細品目.VesselID = 対象見積回答.VesselID;
                            新規詳細品目.RenewDate = RenewDate;
                            新規詳細品目.RenewUserID = RenewUserID;

                            新規詳細品目s.Add(新規詳細品目);
                        }
                        else if (Hachu.Common.CommonDefine.Is変更(詳細品目.OdMkShousaiItemID))
                        {
                            OdMkShousaiItem 変更詳細品目 = 詳細品目;
                            変更詳細品目.OdMkShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(変更詳細品目.OdMkShousaiItemID);
                            変更詳細品目.RenewDate = RenewDate;
                            変更詳細品目.RenewUserID = RenewUserID;

                            変更詳細品目s.Add(変更詳細品目);
                        }
                    }
                    foreach (OdMkShousaiItem 詳細品目 in 品目.削除詳細品目s)
                    {
                        OdMkShousaiItem 削除詳細品目 = 詳細品目;
                        削除詳細品目.OdMkShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdMkShousaiItemID);
                        削除詳細品目.RenewDate = RenewDate;
                        削除詳細品目.RenewUserID = RenewUserID;

                        削除詳細品目s.Add(削除詳細品目);
                    }
                }
            }
            foreach (Item見積回答品目 品目 in 削除回答品目s)
            {
                OdMkItem 削除品目 = 品目.品目;
                削除品目.OdMkItemID = Hachu.Common.CommonDefine.RemovePrefix(削除品目.OdMkItemID);
                削除品目.RenewDate = RenewDate;
                削除品目.RenewUserID = RenewUserID;

                削除品目s.Add(削除品目);

                foreach (OdMkShousaiItem 詳細品目 in 品目.詳細品目s)
                {
                    OdMkShousaiItem 削除詳細品目 = 詳細品目;
                    削除詳細品目.OdMkShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdMkShousaiItemID);
                    削除詳細品目.RenewDate = RenewDate;
                    削除詳細品目.RenewUserID = RenewUserID;

                    削除詳細品目s.Add(削除詳細品目);
                }
                foreach (OdMkShousaiItem 詳細品目 in 品目.削除詳細品目s)
                {
                    OdMkShousaiItem 削除詳細品目 = 詳細品目;
                    削除詳細品目.OdMkShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdMkShousaiItemID);
                    削除詳細品目.RenewDate = RenewDate;
                    削除詳細品目.RenewUserID = RenewUserID;

                    削除詳細品目s.Add(削除詳細品目);
                }
            }
            #endregion

            // 更新
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_見積回答更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象見積回答,
                                                        新規品目s,
                                                        変更品目s,
                                                        削除品目s,
                                                        新規詳細品目s,
                                                        変更詳細品目s,
                                                        削除詳細品目s);

                対象見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 対象見積回答.OdMkID);
            }

            return ret;
        }

        public static bool 取消(ref OdMk 対象見積回答)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            対象見積回答.CancelFlag = NBaseCommon.Common.CancelFlag_キャンセル;
            対象見積回答.RenewDate = RenewDate;
            対象見積回答.RenewUserID = RenewUserID;

            // 更新
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_見積回答更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象見積回答,
                                                        null, null, null, null, null, null);
                対象見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 対象見積回答.OdMkID);
            }

            return ret;
        }

        public static bool 未回答作成(OdMk 元見積回答, List<Item見積回答品目> 元回答品目s, ref OdMk 複製見積回答)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            //===========================
            // 見積回答の構築
            //===========================
            複製見積回答.OdMkID             = Hachu.Common.CommonDefine.新規ID(false);
            複製見積回答.Status             = 複製見積回答.OdStatusValue.Values[(int)OdMk.STATUS.未回答].Value;
            複製見積回答.OdMmID             = 元見積回答.OdMmID;
            複製見積回答.MsCustomerID       = 元見積回答.MsCustomerID;
            複製見積回答.Tantousha          = 元見積回答.Tantousha;
            複製見積回答.MkNo               = 元見積回答.MkNo.Substring(0,OdMk.NoLength見積回答-1);
            複製見積回答.TantouMailAddress  = 元見積回答.TantouMailAddress;
            複製見積回答.HachuNo            = "0";
            複製見積回答.VesselID           = 元見積回答.VesselID;
            複製見積回答.RenewDate          = RenewDate;
            複製見積回答.RenewUserID        = RenewUserID;

            複製見積回答.MsVesselName  = 元見積回答.MsVesselName;
            複製見積回答.MsCustomerName = 元見積回答.MsCustomerName;
            複製見積回答.OdThiNaiyou = 元見積回答.OdThiNaiyou;
            複製見積回答.OdThiBikou = 元見積回答.OdThiBikou;
            //複製見積回答.WebKey
            複製見積回答.MmDate = 元見積回答.MmDate;
            複製見積回答.Kiboubi = 元見積回答.Kiboubi;

            //===========================
            // 見積回答品目、見積回答詳細品目の構築
            //===========================
            List<OdMkItem> 複製回答品目s = new List<OdMkItem>();
            List<OdMkShousaiItem> 複製回答詳細品目s = new List<OdMkShousaiItem>();

            foreach (Item見積回答品目 元回答品目 in 元回答品目s)
            {
                OdMkItem 複製回答品目 = new OdMkItem();
                複製回答品目.OdMkItemID  = Hachu.Common.CommonDefine.新規ID(false);
                複製回答品目.OdMkID      = 複製見積回答.OdMkID;
                複製回答品目.OdMmItemID  = 元回答品目.品目.OdMmItemID;
                複製回答品目.Header      = 元回答品目.品目.Header;
                複製回答品目.MsItemSbtID = 元回答品目.品目.MsItemSbtID;
                複製回答品目.ItemName    = 元回答品目.品目.ItemName;
                複製回答品目.Bikou       = 元回答品目.品目.Bikou;
                複製回答品目.VesselID    = 複製見積回答.VesselID;
                複製回答品目.RenewDate   = RenewDate;
                複製回答品目.RenewUserID = RenewUserID;
                複製回答品目s.Add(複製回答品目);

                foreach (OdMkShousaiItem 元詳細品目 in 元回答品目.詳細品目s)
                {
                    OdMkShousaiItem 複製詳細品目 = new OdMkShousaiItem();
                    複製詳細品目.OdMkShousaiItemID  = Hachu.Common.CommonDefine.新規ID(false);
                    複製詳細品目.OdMkItemID         = 複製回答品目.OdMkItemID;
                    複製詳細品目.OdMmShousaiItemID  = 元詳細品目.OdMmShousaiItemID;
                    複製詳細品目.ShousaiItemName    = 元詳細品目.ShousaiItemName;
                    複製詳細品目.MsVesselItemID     = 元詳細品目.MsVesselItemID;
                    複製詳細品目.MsLoID             = 元詳細品目.MsLoID;
                    複製詳細品目.MsTaniID           = 元詳細品目.MsTaniID;
                    複製詳細品目.Bikou              = 元詳細品目.Bikou;
                    複製詳細品目.VesselID 　　　　　= 複製見積回答.VesselID;
                    複製詳細品目.RenewDate          = RenewDate;
                    複製詳細品目.RenewUserID        = RenewUserID;

                    複製回答詳細品目s.Add(複製詳細品目);
                }

            }

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_見積回答更新処理_新規(
                                                        NBaseCommon.Common.LoginUser,
                                                        複製見積回答,
                                                        複製回答品目s,
                                                        複製回答詳細品目s);

                複製見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 複製見積回答.OdMkID);
            }
            return ret;
        }

        public static void セット(ref OdMk 見積回答, OdMm 見積依頼)
        {
            if (見積依頼 != null)
            {
                見積回答.MkNo   = 見積依頼.MmNo;
                見積回答.OdMmID = 見積依頼.OdMmID;
            }
            見積回答.RenewDate = DateTime.Now;
            見積回答.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
        }
    }
}
