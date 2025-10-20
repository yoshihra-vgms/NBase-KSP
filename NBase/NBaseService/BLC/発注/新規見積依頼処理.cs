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
        bool BLC_新規見積依頼処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            NBaseData.DAC.OdMm OdMm,
            NBaseData.DAC.OdMk OdMk,
            List<NBaseData.DAC.OdThiItem> OdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItems,
            List<NBaseData.DAC.OdAttachFile> OdAttachFiles
            );
    }
    public partial class Service
    {
        #region public bool BLC_新規見積依頼処理_新規(...)
        public bool BLC_新規見積依頼処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            NBaseData.DAC.OdMm OdMm,
            NBaseData.DAC.OdMk OdMk,
            List<NBaseData.DAC.OdThiItem> OdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItems,
            List<NBaseData.DAC.OdAttachFile> OdAttachFiles
            )
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                bool ret = false;

                dbConnect.BeginTransaction();

                foreach (NBaseData.DAC.OdThiItem thiItem in OdThiItems)
                {
                    foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in OdThiShousaiItems)
                    {
                        if (thiShousaiItem.OdThiItemID == thiItem.OdThiItemID)
                        {
                            thiItem.OdThiShousaiItems.Add(thiShousaiItem);
                        }
                    }
                }

                ret = 手配依頼登録(dbConnect, loginUser, ref OdThi, OdThiItems, OdAttachFiles);
                if (ret)
                {
                    List<NBaseData.DAC.OdMmItem> OdMmItems = new List<NBaseData.DAC.OdMmItem>();
                    手配依頼から見積依頼を作成(ref OdMm, ref OdMmItems, OdThi, OdThiItems);
                    ret = 見積依頼登録(dbConnect, loginUser, ref OdMm, OdMmItems);
                    if (ret)
                    {
                        List<NBaseData.DAC.OdMkItem> OdMkItems = new List<NBaseData.DAC.OdMkItem>();
                        見積依頼から見積回答を作成(ref OdMk, ref OdMkItems, OdMm, OdMmItems);
                        ret = 見積回答登録(dbConnect, loginUser, ref OdMk, OdMkItems);
                    }
                }
                if (ret)
                {
                    bool alarmRet = false;
                    alarmRet = NBaseData.BLC.発注アラーム処理.見積回答アラーム登録(dbConnect, loginUser, OdMk);
                }
                if (ret == true)
                {
                    ret = NBaseData.BLC.事務所更新情報処理.見積依頼登録(dbConnect, loginUser, OdMm.OdThiID);
                }
                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }

            return true;
        }
        #endregion

        #region private void 手配依頼から見積依頼を作成(...)
        private void 手配依頼から見積依頼を作成(ref NBaseData.DAC.OdMm OdMm, ref List<NBaseData.DAC.OdMmItem> OdMmItems, NBaseData.DAC.OdThi OdThi, List<NBaseData.DAC.OdThiItem> OdThiItems)
        {
            OdMm.MmNo = OdThi.TehaiIraiNo;

            foreach (NBaseData.DAC.OdThiItem thiItem in OdThiItems)
            {
                //===========================================
                // 手配依頼品目 →　見積依頼品目
                //===========================================
                NBaseData.DAC.OdMmItem mmItem = new NBaseData.DAC.OdMmItem();
                mmItem.OdMmItemID     = 新規ID();
                mmItem.OdMmID         = OdMm.OdMmID;
                mmItem.Header         = thiItem.Header;
                mmItem.MsItemSbtID    = thiItem.MsItemSbtID;
                mmItem.ItemName       = thiItem.ItemName;
                mmItem.Bikou          = thiItem.Bikou;
                mmItem.VesselID       = thiItem.VesselID;
                mmItem.RenewDate      = OdMm.RenewDate;
                mmItem.RenewUserID    = OdMm.RenewUserID;
                mmItem.ShowOrder      = thiItem.ShowOrder;
                mmItem.OdAttachFileID = thiItem.OdAttachFileID;
                OdMmItems.Add(mmItem);

                foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in thiItem.OdThiShousaiItems)
                {
                    //===========================================
                    // 手配依頼詳細品目 →　見積依頼詳細品目
                    //===========================================
                    NBaseData.DAC.OdMmShousaiItem mmShousaiItem = new NBaseData.DAC.OdMmShousaiItem();
                    mmShousaiItem.OdMmShousaiItemID = 新規ID();
                    mmShousaiItem.OdMmItemID        = mmItem.OdMmItemID;
                    mmShousaiItem.ShousaiItemName   = thiShousaiItem.ShousaiItemName;
                    mmShousaiItem.MsVesselItemID    = thiShousaiItem.MsVesselItemID;
                    mmShousaiItem.MsLoID            = thiShousaiItem.MsLoID;
                    mmShousaiItem.Count             = thiShousaiItem.Sateisu;  // 見積依頼詳細品目の数量は、手配依頼詳細品目の査定数
                    mmShousaiItem.MsTaniID          = thiShousaiItem.MsTaniID;
                    mmShousaiItem.Bikou             = thiShousaiItem.Bikou;
                    mmShousaiItem.VesselID          = mmItem.VesselID;
                    mmShousaiItem.RenewDate         = mmItem.RenewDate;
                    mmShousaiItem.RenewUserID       = mmItem.RenewUserID;
                    mmShousaiItem.ShowOrder         = thiShousaiItem.ShowOrder;
                    mmShousaiItem.OdAttachFileID    = thiShousaiItem.OdAttachFileID;

                    mmItem.OdMmShousaiItems.Add(mmShousaiItem);
                }

            }
        }
        #endregion
        #region private void 見積依頼から見積回答を作成(...)
        private void 見積依頼から見積回答を作成(ref NBaseData.DAC.OdMk OdMk, ref List<NBaseData.DAC.OdMkItem> OdMkItems, NBaseData.DAC.OdMm OdMm, List<NBaseData.DAC.OdMmItem> OdMmItems)
        {
            OdMk.MkNo = OdMm.MmNo;

            foreach (NBaseData.DAC.OdMmItem mmItem in OdMmItems)
            {
                //===========================================
                // 見積依頼品目 →　見積回答品目
                //===========================================
                NBaseData.DAC.OdMkItem mkItem = new NBaseData.DAC.OdMkItem();
                mkItem.OdMkItemID     = 新規ID();
                mkItem.OdMkID         = OdMk.OdMkID;
                mkItem.OdMmItemID     = mmItem.OdMmItemID;
                mkItem.Header         = mmItem.Header;
                mkItem.MsItemSbtID    = mmItem.MsItemSbtID;
                mkItem.ItemName       = mmItem.ItemName;
                mkItem.Bikou          = mmItem.Bikou;
                mkItem.VesselID       = mmItem.VesselID;
                mkItem.RenewDate      = OdMk.RenewDate;
                mkItem.RenewUserID    = OdMk.RenewUserID;
                mkItem.ShowOrder      = mmItem.ShowOrder;
                mkItem.OdAttachFileID = mmItem.OdAttachFileID;

                OdMkItems.Add(mkItem);

                foreach (NBaseData.DAC.OdMmShousaiItem mmShousaiItem in mmItem.OdMmShousaiItems)
                {
                    //===========================================
                    // 見積依頼詳細品目 →　見積回答詳細品目
                    //===========================================
                    NBaseData.DAC.OdMkShousaiItem mkShousaiItem = new NBaseData.DAC.OdMkShousaiItem();
                    mkShousaiItem.OdMkShousaiItemID = 新規ID();
                    mkShousaiItem.OdMkItemID        = mkItem.OdMkItemID;
                    mkShousaiItem.OdMmShousaiItemID = mmShousaiItem.OdMmShousaiItemID;
                    mkShousaiItem.ShousaiItemName   = mmShousaiItem.ShousaiItemName;
                    mkShousaiItem.MsVesselItemID    = mmShousaiItem.MsVesselItemID;
                    mkShousaiItem.MsLoID            = mmShousaiItem.MsLoID;
                    mkShousaiItem.Count             = mmShousaiItem.Count;
                    mkShousaiItem.MsTaniID          = mmShousaiItem.MsTaniID;
                    mkShousaiItem.Bikou             = mmShousaiItem.Bikou;
                    mkShousaiItem.VesselID          = mkItem.VesselID;
                    mkShousaiItem.RenewDate         = mkItem.RenewDate;
                    mkShousaiItem.RenewUserID       = mkItem.RenewUserID;
                    mkShousaiItem.ShowOrder         = mmShousaiItem.ShowOrder;
                    mkShousaiItem.OdAttachFileID    = mmShousaiItem.OdAttachFileID;

                    mkItem.OdMkShousaiItems.Add(mkShousaiItem);
                }

            }
        }
        #endregion
    }
 }
