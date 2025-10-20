using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using Hachu.Models;

namespace Hachu.BLC
{
    [Serializable]
    public class 受領更新処理
    {
        public static bool 見積回答から作成(ref OdJry 受領, ref MsThiIraiStatus newStatus, ref OdMk 見積回答, List<Item見積回答品目> 回答品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;


            //===========================
            // 見積回答の構築
            //===========================
            見積回答.RenewDate = RenewDate;
            見積回答.RenewUserID = RenewUserID;

            //===========================
            // 受領の構築
            //===========================
            受領.OdJryID        = Hachu.Common.CommonDefine.新規ID(false);
            受領.Status         = 受領.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value;
            受領.OdMkID         = 見積回答.OdMkID;
            受領.JryNo          = 見積回答.MkNo;
            受領.Amount 　　　　= 見積回答.Amount;
            受領.NebikiAmount   = 見積回答.MkAmount;
            受領.Tax            = 見積回答.Tax;
            受領.VesselID 　　　= 見積回答.VesselID;
            受領.RenewDate      = RenewDate;
            受領.RenewUserID    = RenewUserID;

            受領.Carriage       = 見積回答.Carriage;

            //===========================
            // 受領品目、受領詳細品目の構築
            //===========================
            List<OdJryItem> 受領品目s = new List<OdJryItem>();
            List<OdJryShousaiItem> 受領詳細品目s = new List<OdJryShousaiItem>();
            foreach (Item見積回答品目 回答品目 in 回答品目s)
            {
                if (回答品目.品目.CancelFlag == 1)
                    continue;

                OdJryItem 受領品目      = new OdJryItem();
                受領品目.OdJryItemID    = Hachu.Common.CommonDefine.新規ID(false);
                受領品目.OdJryID        = 受領.OdJryID;
                受領品目.Header         = 回答品目.品目.Header;
                受領品目.MsItemSbtID    = 回答品目.品目.MsItemSbtID;
                受領品目.ItemName       = 回答品目.品目.ItemName;
                受領品目.Bikou          = 回答品目.品目.Bikou;
                受領品目.VesselID       = 見積回答.VesselID;
                受領品目.RenewDate      = RenewDate;
                受領品目.RenewUserID    = RenewUserID;
                受領品目.ShowOrder      = 回答品目.品目.ShowOrder;
                受領品目s.Add(受領品目);

                foreach (OdMkShousaiItem 詳細品目 in 回答品目.詳細品目s)
                {
                    if (詳細品目.CancelFlag == 1)
                        continue;

                    OdJryShousaiItem 受領詳細品目   = new OdJryShousaiItem();
                    受領詳細品目.OdJryShousaiItemID = Hachu.Common.CommonDefine.新規ID(false);
                    受領詳細品目.OdJryItemID        = 受領品目.OdJryItemID;
                    受領詳細品目.ShousaiItemName    = 詳細品目.ShousaiItemName;
                    受領詳細品目.MsVesselItemID     = 詳細品目.MsVesselItemID;
                    受領詳細品目.MsLoID             = 詳細品目.MsLoID;
                    受領詳細品目.Count              = 詳細品目.Count;
                    //受領詳細品目.JryCount           = 0;  // 2009.10.23:aki 受領作成時は、NULLで
                    受領詳細品目.JryCount           = int.MinValue;
                    受領詳細品目.MsTaniID           = 詳細品目.MsTaniID;
                    受領詳細品目.Tanka              = 詳細品目.Tanka;
                    受領詳細品目.Bikou              = 詳細品目.Bikou;
                    受領詳細品目.VesselID           = 見積回答.VesselID;
                    受領詳細品目.RenewDate          = RenewDate;
                    受領詳細品目.RenewUserID        = RenewUserID;
                    受領詳細品目.ShowOrder          = 詳細品目.ShowOrder;

                    受領詳細品目s.Add(受領詳細品目);
                }

            }

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_見積回答更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        見積回答,
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null);

                見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 見積回答.OdMkID);

                MsKamoku kamoku = serviceClient.MsKamoku_GetRecordByHachuKamoku(NBaseCommon.Common.LoginUser, 見積回答.MsThiIraiSbtID, 見積回答.MsThiIraiShousaiID, 見積回答.MsNyukyoKamokuID);
                if (kamoku != null)
                {
                    受領.KamokuNo = kamoku.KamokuNo;
                    受領.UtiwakeKamokuNo = kamoku.UtiwakeKamokuNo;
                }
                ret = serviceClient.BLC_受領更新処理_新規(
                                                        NBaseCommon.Common.LoginUser,
                                                        受領,
                                                        受領品目s,
                                                        受領詳細品目s);
                受領 = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, 受領.OdJryID);
                newStatus = serviceClient.MsThiIraiStatus_GetRecord(NBaseCommon.Common.LoginUser, ((int)MsThiIraiStatus.THI_IRAI_STATUS.発注済).ToString());
            }

            return ret;
        }

        public static bool 更新(ref OdJry 対象受領, List<Item受領品目> 受領品目s, List<Item受領品目> 削除受領品目s, ref MsThiIraiStatus newStatus)
        {
            対象受領.RenewDate = DateTime.Now;
            対象受領.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            List<OdJryItem> 新規品目s = new List<OdJryItem>();
            List<OdJryItem> 変更品目s = new List<OdJryItem>();
            List<OdJryItem> 削除品目s = new List<OdJryItem>();
            List<OdJryItem> 変更なし品目s = new List<OdJryItem>();
            List<OdJryShousaiItem> 新規詳細品目s = new List<OdJryShousaiItem>();
            List<OdJryShousaiItem> 変更詳細品目s = new List<OdJryShousaiItem>();
            List<OdJryShousaiItem> 削除詳細品目s = new List<OdJryShousaiItem>();
            List<OdJryShousaiItem> 変更なし詳細品目s = new List<OdJryShousaiItem>();

            データ振り分け(
                ref 対象受領,
                受領品目s,
                削除受領品目s,
                ref 新規品目s,
                ref 変更品目s,
                ref 削除品目s,
                ref 変更なし品目s,
                ref 新規詳細品目s,
                ref 変更詳細品目s,
                ref 削除詳細品目s,
                ref 変更なし詳細品目s);

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_受領更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象受領,
                                                        新規品目s,
                                                        変更品目s,
                                                        削除品目s,
                                                        新規詳細品目s,
                                                        変更詳細品目s,
                                                        削除詳細品目s);
                対象受領 = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);

                bool isStatusChage = serviceClient.BLC_手配状況変更_受領済(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);
                if (isStatusChage)
                {
                    newStatus = serviceClient.MsThiIraiStatus_GetRecord(NBaseCommon.Common.LoginUser, ((int)MsThiIraiStatus.THI_IRAI_STATUS.受領済).ToString());
                }
            }

            return ret;
        }

        public static bool 分納(ref OdJry 対象受領, ref OdJry 受領残分, List<Item受領品目> 受領品目s, List<Item受領品目> 削除受領品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            List<OdJryItem> 新規品目s = new List<OdJryItem>();
            List<OdJryItem> 変更品目s = new List<OdJryItem>();
            List<OdJryItem> 削除品目s = new List<OdJryItem>();
            List<OdJryItem> 変更なし品目s = new List<OdJryItem>();
            List<OdJryShousaiItem> 新規詳細品目s = new List<OdJryShousaiItem>();
            List<OdJryShousaiItem> 変更詳細品目s = new List<OdJryShousaiItem>();
            List<OdJryShousaiItem> 削除詳細品目s = new List<OdJryShousaiItem>();
            List<OdJryShousaiItem> 変更なし詳細品目s = new List<OdJryShousaiItem>();

            //======================================================
            // 現在の受領データを処理ステータスで振り分ける
            //======================================================
            対象受領.RenewDate = RenewDate;
            対象受領.RenewUserID = RenewUserID;
            データ振り分け(
                ref 対象受領,
                受領品目s, 
                削除受領品目s,
                ref 新規品目s,
                ref 変更品目s,
                ref 削除品目s,
                ref 変更なし品目s,
                ref 新規詳細品目s,
                ref 変更詳細品目s,
                ref 削除詳細品目s,
                ref 変更なし詳細品目s);


            //======================================================
            // 未受領データ（残りの分の受領のデータ）の構築
            //======================================================
            受領残分.OdJryID = Hachu.Common.CommonDefine.新規ID(false);
            受領残分.Status = 受領残分.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value;
            受領残分.OdMkID = 対象受領.OdMkID;
            受領残分.JryNo = 対象受領.JryNo.Substring(0, OdJry.NoLength受領-1);
            受領残分.Amount = 0;
            受領残分.NebikiAmount = 0;
            受領残分.Tax = 対象受領.Tax;
            受領残分.KamokuNo = 対象受領.KamokuNo;
            受領残分.UtiwakeKamokuNo = 対象受領.UtiwakeKamokuNo;
            受領残分.VesselID = 対象受領.VesselID;
            受領残分.RenewDate = RenewDate;
            受領残分.RenewUserID = RenewUserID;
            
            受領残分.Carriage = 0;

            Dictionary<string, string> 分割対象となる品目IDs = new Dictionary<string, string>();
            List<OdJryItem> 残り受領品目s = new List<OdJryItem>();
            List<OdJryShousaiItem> 残り受領詳細品目s = new List<OdJryShousaiItem>();

            詳細品目分割(ref 新規詳細品目s, ref 残り受領詳細品目s, ref 分割対象となる品目IDs);
            詳細品目分割(ref 変更詳細品目s, ref 残り受領詳細品目s, ref 分割対象となる品目IDs);
            詳細品目分割(ref 変更なし詳細品目s, ref 残り受領詳細品目s, ref 分割対象となる品目IDs);

            品目分割(受領残分.OdJryID, ref 新規品目s, ref 残り受領品目s, ref 分割対象となる品目IDs);
            品目分割(受領残分.OdJryID, ref 変更品目s, ref 残り受領品目s, ref 分割対象となる品目IDs);
            品目分割(受領残分.OdJryID, ref 変更なし品目s, ref 残り受領品目s, ref 分割対象となる品目IDs);

            分割した詳細品目に分割した品目のIDを振りなおす(ref 残り受領詳細品目s, ref 分割対象となる品目IDs);

            分割処理で削除とマークされたものを処理する(ref 新規詳細品目s, ref 変更詳細品目s, ref 削除詳細品目s, ref 変更なし詳細品目s);

            // 受領予定の金額（この時点では、納品予定数×単価）
            foreach (OdJryShousaiItem 詳細品目 in 残り受領詳細品目s)
            {
                受領残分.Amount += (詳細品目.Count * 詳細品目.Tanka);
            }

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_受領更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象受領,
                                                        新規品目s,
                                                        変更品目s,
                                                        削除品目s,
                                                        新規詳細品目s,
                                                        変更詳細品目s,
                                                        削除詳細品目s);
                対象受領 = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);
                
                
                ret = serviceClient.BLC_受領更新処理_新規(
                                                        NBaseCommon.Common.LoginUser,
                                                        受領残分,
                                                        残り受領品目s,
                                                        残り受領詳細品目s);
                受領残分 = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, 受領残分.OdJryID);
            }

            return ret;
        }

        public static bool 取消(ref OdJry 対象受領, ref OdMk 見積回答, ref MsThiIraiStatus newStatus)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            対象受領.CancelFlag = NBaseCommon.Common.CancelFlag_キャンセル;
            対象受領.RenewDate = RenewDate;
            対象受領.RenewUserID = RenewUserID;

            // 更新
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_受領更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象受領,
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null);
                if (見積回答.MkNo.Substring(0, 7) != "Enabled")
                {
                    見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 見積回答.OdMkID);
                    OdThi thi = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, 見積回答.OdThiID);
                    newStatus = serviceClient.MsThiIraiStatus_GetRecord(NBaseCommon.Common.LoginUser, thi.MsThiIraiStatusID);
                }
            }

            return ret;
        }

        public static bool 受領解除(ref OdJry 対象受領, ref MsThiIraiStatus newStatus)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            対象受領.RenewDate = RenewDate;
            対象受領.RenewUserID = RenewUserID;

            // 更新
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_受領更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象受領,
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null);
                対象受領 = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);

                OdThi odThi = serviceClient.OdThi_GetRecordByOdJryID(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);

                newStatus = serviceClient.MsThiIraiStatus_GetRecord(NBaseCommon.Common.LoginUser, odThi.MsThiIraiStatusID);
            }

            return ret;
        }

        private static void データ振り分け(
                        ref OdJry 対象受領, 
                        List<Item受領品目> 受領品目s, 
                        List<Item受領品目> 削除受領品目s, 
                        ref List<OdJryItem> 新規品目s,
                        ref List<OdJryItem> 変更品目s,
                        ref List<OdJryItem> 削除品目s,
                        ref List<OdJryItem> 変更なし品目s,
                        ref List<OdJryShousaiItem> 新規詳細品目s,
                        ref List<OdJryShousaiItem> 変更詳細品目s,
                        ref List<OdJryShousaiItem> 削除詳細品目s,
                        ref List<OdJryShousaiItem> 変更なし詳細品目s
            )

        {
            #region
            DateTime RenewDate = 対象受領.RenewDate;
            string RenewUserID = 対象受領.RenewUserID;

            foreach (Item受領品目 品目 in 受領品目s)
            {

                if (Hachu.Common.CommonDefine.Is新規(品目.品目.OdJryItemID))
                {
                    OdJryItem 新規品目 = 品目.品目;
                    新規品目.OdJryItemID = Hachu.Common.CommonDefine.RemovePrefix(新規品目.OdJryItemID);
                    新規品目.OdJryID = 対象受領.OdJryID;
                    新規品目.VesselID = 対象受領.VesselID;
                    新規品目.RenewDate = RenewDate;
                    新規品目.RenewUserID = RenewUserID;

                    新規品目s.Add(新規品目);

                    foreach (OdJryShousaiItem 詳細品目 in 品目.詳細品目s)
                    {
                        OdJryShousaiItem 新規詳細品目 = 詳細品目;
                        新規詳細品目.OdJryShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdJryShousaiItemID);
                        新規詳細品目.OdJryItemID = 新規品目.OdJryItemID;
                        新規詳細品目.VesselID = 対象受領.VesselID;
                        新規詳細品目.RenewDate = RenewDate;
                        新規詳細品目.RenewUserID = RenewUserID;

                        新規詳細品目s.Add(新規詳細品目);
                    }
                }
                else
                {
                    if (Hachu.Common.CommonDefine.Is変更(品目.品目.OdJryItemID))
                    {
                        OdJryItem 変更品目 = 品目.品目;
                        変更品目.OdJryItemID = Hachu.Common.CommonDefine.RemovePrefix(変更品目.OdJryItemID);
                        変更品目.RenewDate = RenewDate;
                        変更品目.RenewUserID = RenewUserID;

                        変更品目s.Add(変更品目);
                    }
                    else
                    {
                        変更なし品目s.Add(品目.品目);
                    }
                    string 変更品目ID = Hachu.Common.CommonDefine.RemovePrefix(品目.品目.OdJryItemID);
                    foreach (OdJryShousaiItem 詳細品目 in 品目.詳細品目s)
                    {
                        if (Hachu.Common.CommonDefine.Is新規(詳細品目.OdJryShousaiItemID))
                        {
                            OdJryShousaiItem 新規詳細品目 = 詳細品目;
                            新規詳細品目.OdJryShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdJryShousaiItemID);
                            新規詳細品目.OdJryItemID = 変更品目ID;
                            新規詳細品目.VesselID = 対象受領.VesselID;
                            新規詳細品目.RenewDate = RenewDate;
                            新規詳細品目.RenewUserID = RenewUserID;
                            if (新規詳細品目.JryCount > 0 && 新規詳細品目.Nouhinbi == DateTime.MinValue) // 2009.10.27:aki 以下４行追加
                            {
                                新規詳細品目.Nouhinbi = 対象受領.JryDate;
                            }
                            新規詳細品目s.Add(新規詳細品目);
                        }
                        else if (Hachu.Common.CommonDefine.Is変更(詳細品目.OdJryShousaiItemID))
                        {
                            OdJryShousaiItem 変更詳細品目 = 詳細品目;
                            変更詳細品目.OdJryShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(変更詳細品目.OdJryShousaiItemID);
                            変更詳細品目.RenewDate = RenewDate;
                            変更詳細品目.RenewUserID = RenewUserID;
                            if (変更詳細品目.JryCount > 0 && 変更詳細品目.Nouhinbi == DateTime.MinValue) // 2009.10.27:aki 以下４行追加
                            {
                                変更詳細品目.Nouhinbi = 対象受領.JryDate;
                            }
                            変更詳細品目s.Add(変更詳細品目);
                        }
                        else
                        {
                            変更なし詳細品目s.Add(詳細品目);
                        }
                    }
                    foreach (OdJryShousaiItem 詳細品目 in 品目.削除詳細品目s)
                    {
                        OdJryShousaiItem 削除詳細品目 = 詳細品目;
                        削除詳細品目.OdJryShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdJryShousaiItemID);
                        削除詳細品目.RenewDate = RenewDate;
                        削除詳細品目.RenewUserID = RenewUserID;

                        削除詳細品目s.Add(削除詳細品目);
                    }
                }
            }
            foreach (Item受領品目 品目 in 削除受領品目s)
            {
                OdJryItem 削除品目 = 品目.品目;
                削除品目.OdJryItemID = Hachu.Common.CommonDefine.RemovePrefix(削除品目.OdJryItemID);
                削除品目.RenewDate = RenewDate;
                削除品目.RenewUserID = RenewUserID;

                削除品目s.Add(削除品目);

                foreach (OdJryShousaiItem 詳細品目 in 品目.詳細品目s)
                {
                    OdJryShousaiItem 削除詳細品目 = 詳細品目;
                    削除詳細品目.OdJryShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdJryShousaiItemID);
                    削除詳細品目.RenewDate = RenewDate;
                    削除詳細品目.RenewUserID = RenewUserID;

                    削除詳細品目s.Add(削除詳細品目);
                }
                foreach (OdJryShousaiItem 詳細品目 in 品目.削除詳細品目s)
                {
                    OdJryShousaiItem 削除詳細品目 = 詳細品目;
                    削除詳細品目.OdJryShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdJryShousaiItemID);
                    削除詳細品目.RenewDate = RenewDate;
                    削除詳細品目.RenewUserID = RenewUserID;

                    削除詳細品目s.Add(削除詳細品目);
                }
            }
            #endregion
        }

        private static void 詳細品目分割(
            ref List<OdJryShousaiItem> 元詳細s,
            ref List<OdJryShousaiItem> 分割詳細s,
            ref Dictionary<string, string> 分割詳細の親品目IDs
            )
        {
            foreach (OdJryShousaiItem src in 元詳細s)
            {
                if (src.CancelFlag == 1)
                    continue;

                if (src.Count > src.JryCount)
                {
                    OdJryShousaiItem dst = src.Clone();

                    dst.OdJryShousaiItemID = Hachu.Common.CommonDefine.新規ID(false);
                    if (src.JryCount == int.MinValue)
                    {
                        dst.Count = src.Count;
                    }
                    else
                    {
                        dst.Count = src.Count - src.JryCount;
                    }
                    //dst.JryCount           = 0;    // 2009.10.23:aki 受領作成時は、NULLで          
                    dst.JryCount           = int.MinValue;
                    dst.Nouhinbi           = DateTime.MinValue;
                    分割詳細s.Add(dst);

                    if (src.JryCount == 0 || src.JryCount == int.MinValue)
                    {
                        src.CancelFlag = 1;
                    }
                    else
                    {
                        src.Count = src.JryCount;
                    }

                    if (!分割詳細の親品目IDs.ContainsKey(dst.OdJryItemID))
                    {
                        分割詳細の親品目IDs.Add(dst.OdJryItemID, dst.OdJryItemID);
                    }

                }
            }
        }

        private static void 品目分割(
            string OdJryID,
            ref List<OdJryItem> 元品目s,
            ref List<OdJryItem> 分割品目s,
            ref Dictionary<string, string> 分割品目IDs
            )
        {

            foreach (OdJryItem src in 元品目s)
            {
                if (src.CancelFlag == 1)
                    continue;

                if (分割品目IDs.ContainsKey(src.OdJryItemID))
                {
                    OdJryItem dst = src.Clone();
                    dst.OdJryID = OdJryID;
                    dst.OdJryItemID = Hachu.Common.CommonDefine.新規ID(false);

                    分割品目s.Add(dst);

                    // 再度、詳細品目の OdJryItemID を振りなおすために新しいIDを紐付けておく
                    分割品目IDs[src.OdJryItemID] = dst.OdJryItemID;
                }
            }

        }

        private static void 分割した詳細品目に分割した品目のIDを振りなおす(
            ref List<OdJryShousaiItem> 分割詳細s,
            ref Dictionary<string, string> 分割詳細の親品目IDs
            )
        {
            foreach (OdJryShousaiItem 詳細 in 分割詳細s)
            {
                if (分割詳細の親品目IDs.ContainsKey(詳細.OdJryItemID))
                {
                    詳細.OdJryItemID = 分割詳細の親品目IDs[詳細.OdJryItemID];
                }
            }
        }

        private static void 分割処理で削除とマークされたものを処理する(
            ref List<OdJryShousaiItem> 新規詳細品目s, 
            ref List<OdJryShousaiItem> 変更詳細品目s,
            ref List<OdJryShousaiItem> 削除詳細品目s,
            ref List<OdJryShousaiItem> 変更なし詳細品目s
           )
        {
            // 「受領」Formで、新規に追加した詳細品目のうち、
            // 受領数が０のものは、分納データとするため
            // 新規追加リストから削除する
            List<string> 削除IDs = new List<string>();
            foreach (OdJryShousaiItem shousaiItem in 新規詳細品目s)
            {
                if (shousaiItem.CancelFlag == 1)
                {
                    削除IDs.Add(shousaiItem.OdJryShousaiItemID);
                }
            }
            foreach (string id in 削除IDs)
            {
                foreach (OdJryShousaiItem shousaiItem in 新規詳細品目s)
                {
                    if (shousaiItem.OdJryShousaiItemID == id)
                    {
                        新規詳細品目s.Remove(shousaiItem);
                        break;
                    }
                }
            }

            // 「受領」Formで、変更があった詳細品目のうち、
            // 受領数が０のものは、分納データとするため
            // 変更リストから削除し、削除リストに追加する
            削除IDs.Clear();
            foreach (OdJryShousaiItem shousaiItem in 変更詳細品目s)
            {
                if (shousaiItem.CancelFlag == 1)
                {
                    削除IDs.Add(shousaiItem.OdJryShousaiItemID);
                }
            }
            foreach (string id in 削除IDs)
            {
                foreach (OdJryShousaiItem shousaiItem in 変更詳細品目s)
                {
                    if (shousaiItem.OdJryShousaiItemID == id)
                    {
                        削除詳細品目s.Add(shousaiItem);
                        変更詳細品目s.Remove(shousaiItem);
                        break;
                    }
                }
            }

            // 「受領」Formで、変更がなかった詳細品目のうち、
            // 受領数が０のものは、分納データとするため
            // 削除リストに追加する
            削除IDs.Clear();
            foreach (OdJryShousaiItem shousaiItem in 変更なし詳細品目s)
            {
                if (shousaiItem.CancelFlag == 1)
                {
                    削除IDs.Add(shousaiItem.OdJryShousaiItemID);
                }
            }
            foreach (string id in 削除IDs)
            {
                foreach (OdJryShousaiItem shousaiItem in 変更なし詳細品目s)
                {
                    if (shousaiItem.OdJryShousaiItemID == id)
                    {
                        削除詳細品目s.Add(shousaiItem);
                        break;
                    }
                }
            }
        }

    }

}
