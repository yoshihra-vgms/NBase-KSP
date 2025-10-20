using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using Hachu.Models;

namespace Hachu.BLC
{
    [Serializable]
    public class 支払更新処理
    {
        public static bool 受領から作成(ref OdShr 支払, OdJry 受領, OdMk 見積回答, List<Item受領品目> 受領品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            //===========================
            // 支払の構築
            //===========================
            支払.OdShrID = Hachu.Common.CommonDefine.新規ID(false);
            if (支払.Sbt == (int)OdShr.SBT.支払)
            {
                支払.Status = 支払.OdStatusValue.Values[(int)OdShr.STATUS.支払作成済み].Value;
                支払.ShrNo = 受領.JryNo + OdShr.Prefix支払;
            }
            else
            {
                支払.Status = 支払.OdStatusValue.Values[(int)OdShr.STATUS.未落成].Value;
                支払.ShrNo = 受領.JryNo + OdShr.Prefix落成;
            }
            支払.OdJryID = 受領.OdJryID;
            支払.Naiyou = 見積回答.OdThiNaiyou;
            支払.Bikou = 見積回答.OdThiBikou;
            支払.Amount = 受領.Amount;
            支払.NebikiAmount = 受領.NebikiAmount;
            支払.Tax = 受領.Tax;
            支払.KamokuNo = 受領.KamokuNo;
            支払.UtiwakeKamokuNo = 受領.UtiwakeKamokuNo;
            支払.VesselID = 受領.VesselID;
            支払.RenewDate = RenewDate;
            支払.RenewUserID = RenewUserID;
            支払.SyoriStatus = "--";

            支払.Carriage = 受領.Carriage;

            //===========================
            // 支払品目、支払詳細品目の構築
            //===========================
            List<OdShrItem> 支払品目s = new List<OdShrItem>();
            List<OdShrShousaiItem> 支払詳細品目s = new List<OdShrShousaiItem>();
            foreach (Item受領品目 受領品目 in 受領品目s)
            {
                if (受領品目.品目.CancelFlag == 1)
                    continue;

                OdShrItem 支払品目 = new OdShrItem();
                支払品目.OdShrItemID = Hachu.Common.CommonDefine.新規ID(false);
                支払品目.OdShrID     = 支払.OdShrID;
                支払品目.Header      = 受領品目.品目.Header;
                支払品目.MsItemSbtID = 受領品目.品目.MsItemSbtID;
                支払品目.ItemName    = 受領品目.品目.ItemName;
                支払品目.Bikou       = 受領品目.品目.Bikou;
                支払品目.VesselID    = 受領.VesselID;
                支払品目.RenewDate   = RenewDate;
                支払品目.RenewUserID = RenewUserID;
                支払品目.ShowOrder   = 受領品目.品目.ShowOrder;
                支払品目.OdJryItemID = 受領品目.品目.OdJryItemID; // 2011.05.19: Add

                支払品目s.Add(支払品目);

                foreach (OdJryShousaiItem 詳細品目 in 受領品目.詳細品目s)
                {
                    if (詳細品目.CancelFlag == 1)
                        continue;

                    OdShrShousaiItem 支払詳細品目 = new OdShrShousaiItem();
                    支払詳細品目.OdShrShousaiItemID  = Hachu.Common.CommonDefine.新規ID(false);
                    支払詳細品目.OdShrItemID         = 支払品目.OdShrItemID;
                    支払詳細品目.ShousaiItemName     = 詳細品目.ShousaiItemName;
                    支払詳細品目.MsVesselItemID      = 詳細品目.MsVesselItemID;
                    支払詳細品目.MsLoID              = 詳細品目.MsLoID;
                    支払詳細品目.Count               = 詳細品目.JryCount;
                    支払詳細品目.MsTaniID            = 詳細品目.MsTaniID;
                    支払詳細品目.Tanka               = 詳細品目.Tanka;
                    支払詳細品目.Bikou               = 詳細品目.Bikou;
                    支払詳細品目.VesselID            = 受領.VesselID;
                    支払詳細品目.RenewDate           = RenewDate;
                    支払詳細品目.RenewUserID         = RenewUserID;
                    支払詳細品目.ShowOrder           = 詳細品目.ShowOrder;
                    支払詳細品目.OdJryShousaiItemID  = 詳細品目.OdJryShousaiItemID; // 2011.05.19: Add

                    支払詳細品目s.Add(支払詳細品目);
                }

            }

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_支払更新処理_新規(
                                                        NBaseCommon.Common.LoginUser,
                                                        支払,
                                                        支払品目s,
                                                        支払詳細品目s);
                支払 = serviceClient.OdShr_GetRecord(NBaseCommon.Common.LoginUser, 支払.OdShrID);
            }

            return ret;
        }

        public static bool 落成(ref OdShr 支払, ref OdShr 落成, List<Item支払品目> 落成品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            //===========================
            // 落成の構築
            //===========================
            落成.RenewDate = RenewDate;
            落成.RenewUserID = RenewUserID;

            //===========================
            // 支払の構築
            //===========================
            支払.OdShrID = Hachu.Common.CommonDefine.新規ID(false);
            支払.Sbt = (int)OdShr.SBT.支払;
            支払.Status = 支払.OdStatusValue.Values[(int)OdShr.STATUS.支払作成済み].Value;
            支払.ShrNo += OdShr.Prefix支払;
            支払.OdJryID = 落成.OdJryID;
            支払.Naiyou = 落成.Naiyou;
            支払.Bikou = 落成.Bikou;
            支払.Amount = 落成.Amount;
            支払.NebikiAmount = 落成.NebikiAmount;
            支払.Tax = 落成.Tax;
            支払.KamokuNo = 落成.KamokuNo;
            支払.UtiwakeKamokuNo = 落成.UtiwakeKamokuNo;
            支払.VesselID = 落成.VesselID;
            支払.RenewDate = RenewDate;
            支払.RenewUserID = RenewUserID;
            支払.SyoriStatus = "--";

            支払.Carriage = 落成.Carriage;

            //===========================
            // 支払品目、支払詳細品目の構築
            //===========================
            List<OdShrItem> 支払品目s = new List<OdShrItem>();
            List<OdShrShousaiItem> 支払詳細品目s = new List<OdShrShousaiItem>();
            foreach (Item支払品目 落成品目 in 落成品目s)
            {
                if (落成品目.品目.CancelFlag == 1)
                    continue;

                OdShrItem 支払品目 = 落成品目.品目.Clone();
                支払品目.OdShrItemID = Hachu.Common.CommonDefine.新規ID(false);
                支払品目.OdShrID = 支払.OdShrID;
                支払品目.RenewDate = RenewDate;
                支払品目.RenewUserID = RenewUserID;

                支払品目s.Add(支払品目);

                foreach (OdShrShousaiItem 詳細品目 in 落成品目.詳細品目s)
                {
                    if (詳細品目.CancelFlag == 1)
                        continue;

                    OdShrShousaiItem 支払詳細品目 = 詳細品目.Clone();
                    支払詳細品目.OdShrShousaiItemID = Hachu.Common.CommonDefine.新規ID(false);
                    支払詳細品目.OdShrItemID = 支払品目.OdShrItemID;
                    支払詳細品目.RenewDate = RenewDate;
                    支払詳細品目.RenewUserID = RenewUserID;

                    支払詳細品目s.Add(支払詳細品目);
                }

            }

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_支払更新処理_落成(
                                                        NBaseCommon.Common.LoginUser,
                                                        落成,
                                                        支払,
                                                        支払品目s,
                                                        支払詳細品目s);
                落成 = serviceClient.OdShr_GetRecord(NBaseCommon.Common.LoginUser, 落成.OdShrID);
                支払 = serviceClient.OdShr_GetRecord(NBaseCommon.Common.LoginUser, 支払.OdShrID);
            }

            return ret;
        }

        public static bool 更新(ref OdShr 対象支払, List<Item支払品目> 支払品目s, List<Item支払品目> 削除支払品目s)
        {
            MsThiIraiStatus newStatus = new MsThiIraiStatus();
            return 更新(ref 対象支払, ref newStatus, 支払品目s, 削除支払品目s);
        }
        public static bool 更新(ref OdShr 対象支払, ref MsThiIraiStatus newStatus, List<Item支払品目> 支払品目s, List<Item支払品目> 削除支払品目s)
        {
            対象支払.RenewDate = DateTime.Now;
            対象支払.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            List<OdShrItem> 新規品目s = new List<OdShrItem>();
            List<OdShrItem> 変更品目s = new List<OdShrItem>();
            List<OdShrItem> 削除品目s = new List<OdShrItem>();
            List<OdShrItem> 変更なし品目s = new List<OdShrItem>();
            List<OdShrShousaiItem> 新規詳細品目s = new List<OdShrShousaiItem>();
            List<OdShrShousaiItem> 変更詳細品目s = new List<OdShrShousaiItem>();
            List<OdShrShousaiItem> 削除詳細品目s = new List<OdShrShousaiItem>();
            List<OdShrShousaiItem> 変更なし詳細品目s = new List<OdShrShousaiItem>();

            データ振り分け(
                ref 対象支払,
                支払品目s,
                削除支払品目s,
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
                ret = serviceClient.BLC_支払更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象支払,
                                                        新規品目s,
                                                        変更品目s,
                                                        削除品目s,
                                                        新規詳細品目s,
                                                        変更詳細品目s,
                                                        削除詳細品目s);
                対象支払 = serviceClient.OdShr_GetRecord(NBaseCommon.Common.LoginUser, 対象支払.OdShrID);

                OdThi 手配 = serviceClient.OdThi_GetRecordByOdShrID(NBaseCommon.Common.LoginUser, 対象支払.OdShrID);
                newStatus = serviceClient.MsThiIraiStatus_GetRecord(NBaseCommon.Common.LoginUser, 手配.MsThiIraiStatusID);
            }

            return ret;
        }

        public static bool 分割(ref OdShr 対象支払, ref OdShr 分割支払, ref List<Item支払品目> 支払品目s, ref List<Item支払品目> 削除支払品目s, List<Item支払品目> 分割支払品目s)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            List<OdShrItem> 新規品目s = new List<OdShrItem>();
            List<OdShrItem> 変更品目s = new List<OdShrItem>();
            List<OdShrItem> 削除品目s = new List<OdShrItem>();
            List<OdShrItem> 変更なし品目s = new List<OdShrItem>();
            List<OdShrShousaiItem> 新規詳細品目s = new List<OdShrShousaiItem>();
            List<OdShrShousaiItem> 変更詳細品目s = new List<OdShrShousaiItem>();
            List<OdShrShousaiItem> 削除詳細品目s = new List<OdShrShousaiItem>();
            List<OdShrShousaiItem> 変更なし詳細品目s = new List<OdShrShousaiItem>();

            int shousaiCount元 = 0;
            int shousaiCount先 = 0;


            List<string> すべて分割対象の品目IDs = new List<string>();

            // 分割とマークされた品目/詳細品目を、元のリストから削除品目へ移行する
            foreach (Item支払品目 元品目 in 支払品目s)
            {
                bool is分割 = false;
                List<OdShrShousaiItem> 分割対象詳細品目s = null;
                foreach (Item支払品目 分割品目 in 分割支払品目s)
                {
                    if (元品目.品目.OdShrItemID == 分割品目.品目.OdShrItemID)
                    {
                        is分割 = true;
                        分割対象詳細品目s = 分割品目.詳細品目s;
                        break;
                    }
                }
                if (is分割 == false)
                {
                    continue;
                }
                foreach (OdShrShousaiItem 分割詳細品目 in 分割対象詳細品目s)
                {
                    foreach (OdShrShousaiItem 元詳細品目 in 元品目.詳細品目s)
                    {
                        if (元詳細品目.OdShrShousaiItemID == 分割詳細品目.OdShrShousaiItemID)
                        {
                            元品目.削除詳細品目s.Add(元詳細品目);
                            元品目.詳細品目s.Remove(元詳細品目);
                            break;
                        }
                    }
                }
                if (元品目.詳細品目s.Count == 0)
                {
                    すべて分割対象の品目IDs.Add(元品目.品目.OdShrItemID);
                }
            }
            foreach (string id in すべて分割対象の品目IDs)
            {
                foreach (Item支払品目 元品目 in 支払品目s)
                {
                    if (元品目.品目.OdShrItemID == id)
                    {
                        削除支払品目s.Add(元品目);
                        支払品目s.Remove(元品目);
                        break;
                    }
                }
            }

            //======================================================
            // 現在の支払データを処理ステータスで振り分ける
            //======================================================
            対象支払.RenewDate = RenewDate;
            対象支払.RenewUserID = RenewUserID;
            データ振り分け(
                ref 対象支払,
                支払品目s,
                削除支払品目s,
                ref 新規品目s,
                ref 変更品目s,
                ref 削除品目s,
                ref 変更なし品目s,
                ref 新規詳細品目s,
                ref 変更詳細品目s,
                ref 削除詳細品目s,
                ref 変更なし詳細品目s);
            対象支払.Amount = 0;
            foreach (Item支払品目 支払品目 in 支払品目s)
            {
                foreach (OdShrShousaiItem 詳細品目 in 支払品目.詳細品目s)
                {
                    対象支払.Amount += (詳細品目.Count * 詳細品目.Tanka);
                    shousaiCount元 += 詳細品目.Count;
                }
            }

            //======================================================
            // 分割データの構築
            //======================================================
            分割支払.OdShrID = Hachu.Common.CommonDefine.新規ID(false);
            分割支払.Sbt = 対象支払.Sbt;
            分割支払.Status = 分割支払.OdStatusValue.Values[(int)OdShr.STATUS.支払作成済み].Value;
            分割支払.ShrNo = 対象支払.ShrNo.Substring(0, OdShr.NoLength支払-1);
            分割支払.OdJryID = 対象支払.OdJryID;
            分割支払.Naiyou = 対象支払.Naiyou;
            分割支払.Bikou = 対象支払.Bikou;
            分割支払.Amount = 0;
            分割支払.NebikiAmount = 0;
            分割支払.Tax = 対象支払.Tax;
            分割支払.KamokuNo = 対象支払.KamokuNo;
            分割支払.UtiwakeKamokuNo = 対象支払.UtiwakeKamokuNo;
            分割支払.VesselID = 対象支払.VesselID;
            分割支払.RenewDate = RenewDate;
            分割支払.RenewUserID = RenewUserID;
            分割支払.SyoriStatus = "--";

            分割支払.Carriage = 0;

            List<OdShrItem> 分割品目s = new List<OdShrItem>();
            List<OdShrShousaiItem> 分割詳細品目s = new List<OdShrShousaiItem>();
            int itemShowOrder = 0;
            foreach (Item支払品目 分割支払品目 in 分割支払品目s)
            {
                分割支払品目.品目.OdShrID = 分割支払.OdShrID;
                分割支払品目.品目.OdShrItemID = Hachu.Common.CommonDefine.新規ID(false);
                分割支払品目.品目.RenewDate = RenewDate;
                分割支払品目.品目.RenewUserID = RenewUserID;
                分割支払品目.品目.VesselID = 対象支払.VesselID;
                分割支払品目.品目.ShowOrder = ++itemShowOrder;

                分割品目s.Add(分割支払品目.品目);

                int shousaiShowOrder = 0;
                foreach (OdShrShousaiItem 詳細品目 in 分割支払品目.詳細品目s)
                {
                    詳細品目.OdShrShousaiItemID = Hachu.Common.CommonDefine.新規ID(false);
                    詳細品目.OdShrItemID = 分割支払品目.品目.OdShrItemID;
                    詳細品目.RenewDate = RenewDate;
                    詳細品目.RenewUserID = RenewUserID;
                    詳細品目.VesselID = 対象支払.VesselID;
                    詳細品目.ShowOrder = ++shousaiShowOrder;

                    分割詳細品目s.Add(詳細品目);

                    分割支払.Amount += (詳細品目.Count * 詳細品目.Tanka);
                    shousaiCount先 += 詳細品目.Count;
                }
            }

            // 値引きの按分
            if (対象支払.NebikiAmount > 0)
            {
                decimal 一品目の値引き = (対象支払.NebikiAmount / (shousaiCount元 + shousaiCount先));
                対象支払.NebikiAmount = Math.Round(((decimal)(一品目の値引き * shousaiCount元)), 0, MidpointRounding.AwayFromZero);
                分割支払.NebikiAmount -= 対象支払.NebikiAmount;
            }

            // 2014.03: 2013年度改造
            // 消費税の計算
            //if (対象支払.Tax > 0)
            //{
            //    対象支払.Tax = NBaseCommon.Common.消費税額(対象支払.Amount);
            //    分割支払.Tax = NBaseCommon.Common.消費税額(分割支払.Amount);
            //}

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_支払更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象支払,
                                                        新規品目s,
                                                        変更品目s,
                                                        削除品目s,
                                                        新規詳細品目s,
                                                        変更詳細品目s,
                                                        削除詳細品目s);
                対象支払 = serviceClient.OdShr_GetRecord(NBaseCommon.Common.LoginUser, 対象支払.OdShrID);


                ret = serviceClient.BLC_支払更新処理_新規(
                                                        NBaseCommon.Common.LoginUser,
                                                        分割支払,
                                                        分割品目s,
                                                        分割詳細品目s);
                分割支払 = serviceClient.OdShr_GetRecord(NBaseCommon.Common.LoginUser, 分割支払.OdShrID);
            }

            return ret;
        }

        public static bool 取消(ref OdShr 対象支払)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            対象支払.CancelFlag = NBaseCommon.Common.CancelFlag_キャンセル;
            対象支払.RenewDate = RenewDate;
            対象支払.RenewUserID = RenewUserID;

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_支払更新処理_更新(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象支払,
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null,
                                                        null);
            }

            return ret;
        }


        private static void データ振り分け(
                        ref OdShr 対象支払,
                        List<Item支払品目> 支払品目s,
                        List<Item支払品目> 削除支払品目s,
                        ref List<OdShrItem> 新規品目s,
                        ref List<OdShrItem> 変更品目s,
                        ref List<OdShrItem> 削除品目s,
                        ref List<OdShrItem> 変更なし品目s,
                        ref List<OdShrShousaiItem> 新規詳細品目s,
                        ref List<OdShrShousaiItem> 変更詳細品目s,
                        ref List<OdShrShousaiItem> 削除詳細品目s,
                        ref List<OdShrShousaiItem> 変更なし詳細品目s
            )
        {
            #region
            DateTime RenewDate = 対象支払.RenewDate;
            string RenewUserID = 対象支払.RenewUserID;

            foreach (Item支払品目 品目 in 支払品目s)
            {

                if (Hachu.Common.CommonDefine.Is新規(品目.品目.OdShrItemID))
                {
                    OdShrItem 新規品目 = 品目.品目;
                    新規品目.OdShrItemID = Hachu.Common.CommonDefine.RemovePrefix(新規品目.OdShrItemID);
                    新規品目.OdShrID = 対象支払.OdShrID;
                    新規品目.VesselID = 対象支払.VesselID;
                    新規品目.RenewDate = RenewDate;
                    新規品目.RenewUserID = RenewUserID;

                    新規品目s.Add(新規品目);

                    foreach (OdShrShousaiItem 詳細品目 in 品目.詳細品目s)
                    {
                        OdShrShousaiItem 新規詳細品目 = 詳細品目;
                        新規詳細品目.OdShrShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdShrShousaiItemID);
                        新規詳細品目.OdShrItemID = 新規品目.OdShrItemID;
                        新規詳細品目.VesselID = 対象支払.VesselID;
                        新規詳細品目.RenewDate = RenewDate;
                        新規詳細品目.RenewUserID = RenewUserID;

                        新規詳細品目s.Add(新規詳細品目);
                    }
                }
                else
                {
                    if (Hachu.Common.CommonDefine.Is変更(品目.品目.OdShrItemID))
                    {
                        OdShrItem 変更品目 = 品目.品目;
                        変更品目.OdShrItemID = Hachu.Common.CommonDefine.RemovePrefix(変更品目.OdShrItemID);
                        変更品目.RenewDate = RenewDate;
                        変更品目.RenewUserID = RenewUserID;

                        変更品目s.Add(変更品目);
                    }
                    else
                    {
                        変更なし品目s.Add(品目.品目);
                    }
                    string 変更品目ID = Hachu.Common.CommonDefine.RemovePrefix(品目.品目.OdShrItemID);
                    foreach (OdShrShousaiItem 詳細品目 in 品目.詳細品目s)
                    {
                        if (Hachu.Common.CommonDefine.Is新規(詳細品目.OdShrShousaiItemID))
                        {
                            OdShrShousaiItem 新規詳細品目 = 詳細品目;
                            新規詳細品目.OdShrShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(新規詳細品目.OdShrShousaiItemID);
                            新規詳細品目.OdShrItemID = 変更品目ID;
                            新規詳細品目.VesselID = 対象支払.VesselID;
                            新規詳細品目.RenewDate = RenewDate;
                            新規詳細品目.RenewUserID = RenewUserID;

                            新規詳細品目s.Add(新規詳細品目);
                        }
                        else if (Hachu.Common.CommonDefine.Is変更(詳細品目.OdShrShousaiItemID))
                        {
                            OdShrShousaiItem 変更詳細品目 = 詳細品目;
                            変更詳細品目.OdShrShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(変更詳細品目.OdShrShousaiItemID);
                            変更詳細品目.RenewDate = RenewDate;
                            変更詳細品目.RenewUserID = RenewUserID;

                            変更詳細品目s.Add(変更詳細品目);
                        }
                        else
                        {
                            変更なし詳細品目s.Add(詳細品目);
                        }
                    }
                    foreach (OdShrShousaiItem 詳細品目 in 品目.削除詳細品目s)
                    {
                        OdShrShousaiItem 削除詳細品目 = 詳細品目;
                        削除詳細品目.OdShrShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdShrShousaiItemID);
                        削除詳細品目.RenewDate = RenewDate;
                        削除詳細品目.RenewUserID = RenewUserID;

                        削除詳細品目s.Add(削除詳細品目);
                    }
                }
            }
            foreach (Item支払品目 品目 in 削除支払品目s)
            {
                OdShrItem 削除品目 = 品目.品目;
                削除品目.OdShrItemID = Hachu.Common.CommonDefine.RemovePrefix(削除品目.OdShrItemID);
                削除品目.RenewDate = RenewDate;
                削除品目.RenewUserID = RenewUserID;

                削除品目s.Add(削除品目);

                foreach (OdShrShousaiItem 詳細品目 in 品目.詳細品目s)
                {
                    OdShrShousaiItem 削除詳細品目 = 詳細品目;
                    削除詳細品目.OdShrShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdShrShousaiItemID);
                    削除詳細品目.RenewDate = RenewDate;
                    削除詳細品目.RenewUserID = RenewUserID;

                    削除詳細品目s.Add(削除詳細品目);
                }
                foreach (OdShrShousaiItem 詳細品目 in 品目.削除詳細品目s)
                {
                    OdShrShousaiItem 削除詳細品目 = 詳細品目;
                    削除詳細品目.OdShrShousaiItemID = Hachu.Common.CommonDefine.RemovePrefix(削除詳細品目.OdShrShousaiItemID);
                    削除詳細品目.RenewDate = RenewDate;
                    削除詳細品目.RenewUserID = RenewUserID;

                    削除詳細品目s.Add(削除詳細品目);
                }
            }
            #endregion
        }
    }
}
