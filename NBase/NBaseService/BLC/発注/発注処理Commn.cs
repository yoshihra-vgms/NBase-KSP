using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_手配状況変更_受領済(
            NBaseData.DAC.MsUser loginUser,
            string OdJryId);
    }


    public partial class Service
    {
        public bool BLC_手配状況変更_受領済(
            NBaseData.DAC.MsUser loginUser,
            string OdJryId)
        {
            bool ret = true;

            // 受領ID より 手配データを取得する
            NBaseData.DAC.OdThi thi = NBaseData.DAC.OdThi.GetRecordByOdJryID(loginUser, OdJryId);
            if (thi == null)
            {
                return false; // 手配依頼がないのはおかしい
            }

            // 手配にぶら下がる見積を確認
            List<NBaseData.DAC.OdMm> mms = NBaseData.DAC.OdMm.GetRecordsByOdThiID(loginUser, thi.OdThiID);
            foreach (NBaseData.DAC.OdMm mm in mms)
            {
                if (mm.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                {
                    continue;　// キャンセルデータは無視する
                }

                // 見積にぶら下がる見積回答（発注）
                List<NBaseData.DAC.OdMk> mks = NBaseData.DAC.OdMk.GetRecordsByOdMmID(loginUser, mm.OdMmID);
                foreach (NBaseData.DAC.OdMk mk in mks)
                {
                    if (mk.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;　// キャンセルデータは無視する
                    }
                    if (mk.Status != (int)NBaseData.DAC.OdMk.STATUS.発注済み)
                    {
                        continue;　// 発注済でない場合は無視する
                    }

                    // 見積回答（発注）にぶら下がる受領
                    List<NBaseData.DAC.OdJry> jrys = NBaseData.DAC.OdJry.GetRecordsByOdMkId(loginUser, mk.OdMkID);
                    foreach (NBaseData.DAC.OdJry jry in jrys)
                    {
                        if (jry.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                        {
                            continue;　// キャンセルデータは無視する
                        }
                        // 2014.02 2013年度改造
                        //if (jry.Status != (int)NBaseData.DAC.OdJry.STATUS.受領済み &&
                        //if (jry.Status != (int)NBaseData.DAC.OdJry.STATUS.船受領 &&
                        //    jry.Status != (int)NBaseData.DAC.OdJry.STATUS.受領承認済み)
                        if (jry.Status != (int)NBaseData.DAC.OdJry.STATUS.受領承認済み)
                        {
                            ret = false;
                            break;
                        }
                    }
                }
            }
            if (ret == true)
            {
                // すべて受領済み
                thi.MsThiIraiStatusID = ((int)NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.受領済).ToString();
                thi.UpdateRecord(loginUser);
            }

            return ret;
        }


        public bool BLC_手配状況変更_受領の取消し(
            DBConnect dbConnect,
            NBaseData.DAC.MsUser loginUser,
            string OdJryId)
        {
            bool ret = true;
            int 発注済み数 = 0;
            int 受領済み数 = 0;
            int 回答内受領数 = 0;

            // 受領ID より 手配データを取得する
            NBaseData.DAC.OdThi thi = NBaseData.DAC.OdThi.GetRecordByOdJryID(dbConnect, loginUser, OdJryId);
            if (thi == null)
            {
                return false; // 手配依頼がないのはおかしい
            }

            // 手配にぶら下がる見積を確認
            List<NBaseData.DAC.OdMm> mms = NBaseData.DAC.OdMm.GetRecordsByOdThiID(dbConnect, loginUser, thi.OdThiID);
            foreach (NBaseData.DAC.OdMm mm in mms)
            {
                if (mm.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                {
                    continue;　// キャンセルデータは無視する
                }

                // 見積にぶら下がる見積回答（発注）
                List<NBaseData.DAC.OdMk> mks = NBaseData.DAC.OdMk.GetRecordsByOdMmID(dbConnect, loginUser, mm.OdMmID);
                foreach (NBaseData.DAC.OdMk mk in mks)
                {
                    if (mk.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;　// キャンセルデータは無視する
                    }

                    // 見積回答（発注）にぶら下がる受領
                    回答内受領数 = 0;
                    List<NBaseData.DAC.OdJry> jrys = NBaseData.DAC.OdJry.GetRecordsByOdMkId(dbConnect, loginUser, mk.OdMkID);
                    foreach (NBaseData.DAC.OdJry jry in jrys)
                    {
                        if (jry.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                        {
                            continue;　// キャンセルデータは無視する
                        }
                        if (jry.OdJryID == OdJryId)
                        {
                            continue;　// 今回、取り消し操作をした受領データは無視する
                        }
                        // 2014.02 2013年度改造
                        //if (jry.Status == (int)NBaseData.DAC.OdJry.STATUS.受領済み ||
                        //if (jry.Status == (int)NBaseData.DAC.OdJry.STATUS.船受領 ||
                        //    jry.Status == (int)NBaseData.DAC.OdJry.STATUS.受領承認済み)
                        if (jry.Status == (int)NBaseData.DAC.OdJry.STATUS.受領承認済み)
                        {
                            受領済み数++;
                        }
                        回答内受領数++;
                    }
                    if (回答内受領数 > 0)
                    {
                        発注済み数++;
                    }
                }
            }
            if (発注済み数 == 0 && 受領済み数 == 0)
            {
                thi.MsThiIraiStatusID = ((int)NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.見積中).ToString();
            }
            else if (発注済み数 != 受領済み数)
            {
                thi.MsThiIraiStatusID = ((int)NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.発注済).ToString();
            }
            else
            {
                thi.MsThiIraiStatusID = ((int)NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.受領済).ToString();
            }
            ret = thi.UpdateRecord(dbConnect, loginUser);

            return ret;
        }

        /// <summary>
        /// 2011.05 Add
        /// </summary>
        /// <param name="dbConnect"></param>
        /// <param name="loginUser"></param>
        /// <param name="OdJryId"></param>
        /// <returns></returns>
        public bool BLC_手配状況変更_受領解除(
            DBConnect dbConnect,
            NBaseData.DAC.MsUser loginUser,
            string OdJryId)
        {
            bool ret = true;

            List<NBaseData.DAC.OdJryGaisan> jryGaisan = NBaseData.DAC.OdJryGaisan.GetRecordsByOdJryID(dbConnect, loginUser, OdJryId);
            foreach (NBaseData.DAC.OdJryGaisan g in jryGaisan)
            {
                g.CancelFlag = 1;
                g.RenewDate = DateTime.Now;
                g.RenewUserID = loginUser.MsUserID;

                g.UpdateRecord(dbConnect, loginUser);
            }

            // 受領ID より 手配データを取得する
            NBaseData.DAC.OdThi thi = NBaseData.DAC.OdThi.GetRecordByOdJryID(dbConnect, loginUser, OdJryId);
            if (thi == null)
            {
                return false; // 手配依頼がないのはおかしい
            }
            thi.MsThiIraiStatusID = ((int)NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.発注済).ToString();
            ret = thi.UpdateRecord(dbConnect, loginUser);

            return ret;
        }



        private string 手配依頼SetMaxNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string TehaiIraiNo)
        {
            //NBaseData.BLC.GetMaxNo maxNo = NBaseData.BLC.GetMaxNo.GetMaxTehaiIraiNo(dbConnect, loginUser, NBaseData.DAC.OdThi.NoLength手配依頼 - 1, TehaiIraiNo);
            NBaseData.BLC.GetMaxNo maxNo = NBaseData.BLC.GetMaxNo.GetMaxTehaiIraiNo(dbConnect, loginUser, NBaseData.DAC.OdThi.NoLength手配依頼 - 2, TehaiIraiNo);
            string currentMaxNo = maxNo.MaxNo.Replace(TehaiIraiNo, "");
            int currentMaxNoDec = NBaseData.DS.NoConvert.NToInt(currentMaxNo, 36);
            string nextMaxNo = NBaseData.DS.NoConvert.IntToN(currentMaxNoDec + 1, 36);

            if (nextMaxNo.Length == 1)
            {
                nextMaxNo = "0" + nextMaxNo;
            }

            return TehaiIraiNo += nextMaxNo;
        }

        private string 見積依頼SetMaxNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string MmNo)
        {
            NBaseData.BLC.GetMaxNo maxNo = NBaseData.BLC.GetMaxNo.GetMaxMmNo(dbConnect, loginUser, NBaseData.DAC.OdMm.NoLength見積依頼 - 1, MmNo);
            string currentMaxNo = maxNo.MaxNo.Replace(MmNo, "");
            int currentMaxNoDec = NBaseData.DS.NoConvert.NToInt(currentMaxNo, 36);
            string nextMaxNo = NBaseData.DS.NoConvert.IntToN(currentMaxNoDec + 1, 36);

            return MmNo += nextMaxNo;
        }

        private string 見積回答SetMaxNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string MkNo)
        {
            NBaseData.BLC.GetMaxNo maxNo = NBaseData.BLC.GetMaxNo.GetMaxMkNo(dbConnect, loginUser, NBaseData.DAC.OdMk.NoLength見積回答 - 1, MkNo);
            string currentMaxNo = maxNo.MaxNo.Replace(MkNo, "");
            int currentMaxNoDec = NBaseData.DS.NoConvert.NToInt(currentMaxNo, 36);
            string nextMaxNo = NBaseData.DS.NoConvert.IntToN(currentMaxNoDec + 1, 36);

            return MkNo += nextMaxNo;
        }

        private string 受領SetMaxNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string JryNo)
        {
            NBaseData.BLC.GetMaxNo maxNo = NBaseData.BLC.GetMaxNo.GetMaxJryNo(dbConnect, loginUser, NBaseData.DAC.OdJry.NoLength受領 - 1, JryNo);
            string currentMaxNo = maxNo.MaxNo.Replace(JryNo, "");
            int currentMaxNoDec = NBaseData.DS.NoConvert.NToInt(currentMaxNo, 36);
            string nextMaxNo = NBaseData.DS.NoConvert.IntToN(currentMaxNoDec + 1, 36);
            return JryNo += nextMaxNo;
        }

        private string 支払SetMaxNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string ShrNo)
        {
            NBaseData.BLC.GetMaxNo maxNo = NBaseData.BLC.GetMaxNo.GetMaxShrNo(dbConnect, loginUser, NBaseData.DAC.OdShr.NoLength支払 - 1, ShrNo);
            string currentMaxNo = maxNo.MaxNo.Replace(ShrNo, "");
            int currentMaxNoDec = NBaseData.DS.NoConvert.NToInt(currentMaxNo, 36);
            string nextMaxNo = NBaseData.DS.NoConvert.IntToN(currentMaxNoDec + 1, 36);
            return ShrNo += nextMaxNo;
        }


        private string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        private bool 小修理品目マスタ登録(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int vesselID, string itemName, string renewUserID, DateTime renewDate)
        {
            NBaseData.DAC.MsShoushuriItem shoushuriItem = new NBaseData.DAC.MsShoushuriItem();

            shoushuriItem.MsSsItemID    = 新規ID();
            shoushuriItem.MsVesselID    = vesselID;
            shoushuriItem.ItemName      = itemName;
            shoushuriItem.VesselID      = vesselID;
            shoushuriItem.RenewDate     = renewDate;
            shoushuriItem.RenewUserID   = renewUserID;

            return shoushuriItem.InsertRecord(dbConnect, loginUser);
        }

        private bool 小修理詳細品目マスタ登録(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int vesselID, string itemName, string renewUserID, DateTime renewDate)
        {
            NBaseData.DAC.MsSsShousaiItem ssShousaiItem = new NBaseData.DAC.MsSsShousaiItem();

            ssShousaiItem.MsSsShousaiItemID = 新規ID();
            ssShousaiItem.MsVesselID        = vesselID;
            ssShousaiItem.ShousaiItemName   = itemName;
            ssShousaiItem.VesselID          = vesselID;
            ssShousaiItem.RenewDate         = renewDate;
            ssShousaiItem.RenewUserID       = renewUserID;

            return ssShousaiItem.InsertRecord(dbConnect, loginUser);
        }
   }
 }
