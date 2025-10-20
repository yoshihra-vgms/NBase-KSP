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
        bool BLC_新規発注処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            NBaseData.DAC.OdMm OdMm,
            NBaseData.DAC.OdMk OdMk,
            NBaseData.DAC.OdJry OdJry,
            List<NBaseData.DAC.OdThiItem> OdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItems,
            List<NBaseData.DAC.OdJryItem> OdJryItems,
            List<NBaseData.DAC.OdJryShousaiItem> OdJryShousaiItems
            );

        [OperationContract]
        bool BLC_新規発注処理_手配あり(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            NBaseData.DAC.OdMm OdMm,
            NBaseData.DAC.OdMk OdMk,
            NBaseData.DAC.OdJry OdJry,
            List<NBaseData.DAC.OdThiItem> OdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItems,
            List<NBaseData.DAC.OdJryItem> OdJryItems,
            List<NBaseData.DAC.OdJryShousaiItem> OdJryShousaiItems
            );

        [OperationContract]
        bool BLC_新規発注処理_保存(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            NBaseData.DAC.OdMm OdMm,
            NBaseData.DAC.OdMk OdMk,
            List<NBaseData.DAC.OdThiItem> OdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItems
            );

        [OperationContract]
        bool BLC_新規発注処理_保存情報削除(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            NBaseData.DAC.OdMm OdMm,
            NBaseData.DAC.OdMk OdMk);
    }
    public partial class Service
    {
        #region public bool BLC_新規発注処理_新規(...)
        public bool BLC_新規発注処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            NBaseData.DAC.OdMm OdMm,
            NBaseData.DAC.OdMk OdMk,
            NBaseData.DAC.OdJry OdJry,
            List<NBaseData.DAC.OdThiItem> OdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItems,
            List<NBaseData.DAC.OdJryItem> OdJryItems,
            List<NBaseData.DAC.OdJryShousaiItem> OdJryShousaiItems
            )
        {
            foreach (NBaseData.DAC.OdThiItem thiItem in OdThiItems)
            {
                thiItem.OdThiShousaiItems.Clear();
                foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in OdThiShousaiItems)
                {
                    if (thiShousaiItem.OdThiItemID == thiItem.OdThiItemID)
                    {
                        thiItem.OdThiShousaiItems.Add(thiShousaiItem);
                    }
                }
            }
            foreach (NBaseData.DAC.OdJryItem jryItem in OdJryItems)
            {
                foreach (NBaseData.DAC.OdJryShousaiItem jryShousaiItem in OdJryShousaiItems)
                {
                    if (jryShousaiItem.OdJryItemID == jryItem.OdJryItemID)
                    {
                        jryItem.OdJryShousaiItems.Add(jryShousaiItem);
                    }
                }
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                bool ret = false;

                dbConnect.BeginTransaction();

                //if (OdThi.TehaiIraiNo.Length == (NBaseData.DAC.OdThi.NoLength手配依頼 - 1))
                if (OdThi.TehaiIraiNo.Length == (NBaseData.DAC.OdThi.NoLength手配依頼 - 2))
                {
                        OdThi.TehaiIraiNo = 手配依頼SetMaxNo(dbConnect, loginUser, OdThi.TehaiIraiNo);
                }
                OdMm.MmNo = OdThi.TehaiIraiNo;
                if (OdMm.MmNo.Length == (NBaseData.DAC.OdMm.NoLength見積依頼 - 1))
                {
                    OdMm.MmNo = 見積依頼SetMaxNo(dbConnect, loginUser, OdMm.MmNo);
                }
                OdMk.MkNo = OdMm.MmNo;
                if (OdMk.MkNo.Length == (NBaseData.DAC.OdMk.NoLength見積回答 - 1))
                {
                    OdMk.MkNo = 見積回答SetMaxNo(dbConnect, loginUser, OdMk.MkNo);
                    OdMk.HachuNo = OdMk.MkNo;
                    OdMk.HachuDate = DateTime.Now;
                    OdMk.Nendo = NBaseUtil.DateTimeUtils.年度開始日(OdMk.HachuDate).Year.ToString();
                }
                OdJry.JryNo = OdMk.MkNo;

                // 新規発注処理で作成される、見積依頼、見積回答は、内部データとして扱いたいので
                // 各番号を加工しておく
                OdMm.MmNo = "Enabled" + OdMm.MmNo;
                OdMk.MkNo = "Enabled" + OdMk.MkNo;


                ret = 手配依頼登録(dbConnect, loginUser, ref OdThi, OdThiItems, null);
                if (ret)
                {
                    ret = 見積依頼登録(dbConnect, loginUser, ref OdMm, null);
                    if (ret)
                    {
                        //ret = 見積回答登録(dbConnect, loginUser, ref OdMk, null);
                        List<NBaseData.DAC.OdMkItem> OdMkItems = new List<NBaseData.DAC.OdMkItem>();
                        手配依頼から見積回答を作成(ref OdMk, ref OdMkItems, OdThi, OdThiItems);
                        ret = 見積回答登録(dbConnect, loginUser, ref OdMk, OdMkItems);
                        if (ret)
                        {
                            ret = 受領登録(dbConnect, loginUser, ref OdJry, OdJryItems);
                            if (ret)
                            {
                                bool alarmRet = false;
                                alarmRet = NBaseData.BLC.発注アラーム処理.受領アラーム登録(dbConnect, loginUser, OdJry);
                            }
                            if (ret == true)
                            {
                                ret = NBaseData.BLC.事務所更新情報処理.発注登録(dbConnect, loginUser, OdJry);
                            }
                        }
                    }
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

        #region public bool BLC_新規発注処理_手配あり(...)
        public bool BLC_新規発注処理_手配あり(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            NBaseData.DAC.OdMm OdMm,
            NBaseData.DAC.OdMk OdMk,
            NBaseData.DAC.OdJry OdJry,
            List<NBaseData.DAC.OdThiItem> OdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItems,
            List<NBaseData.DAC.OdJryItem> OdJryItems,
            List<NBaseData.DAC.OdJryShousaiItem> OdJryShousaiItems
            )
        {
            foreach (NBaseData.DAC.OdThiItem thiItem in OdThiItems)
            {
                thiItem.OdThiShousaiItems.Clear();
                foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in OdThiShousaiItems)
                {
                    if (thiShousaiItem.OdThiItemID == thiItem.OdThiItemID)
                    {
                        thiItem.OdThiShousaiItems.Add(thiShousaiItem);
                    }
                }
            }
            foreach (NBaseData.DAC.OdJryItem jryItem in OdJryItems)
            {
                foreach (NBaseData.DAC.OdJryShousaiItem jryShousaiItem in OdJryShousaiItems)
                {
                    if (jryShousaiItem.OdJryItemID == jryItem.OdJryItemID)
                    {
                        jryItem.OdJryShousaiItems.Add(jryShousaiItem);
                    }
                }
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                bool ret = false;

                dbConnect.BeginTransaction();

                //if (OdThi.TehaiIraiNo.Length == (NBaseData.DAC.OdThi.NoLength手配依頼 - 1))
                if (OdThi.TehaiIraiNo.Length == (NBaseData.DAC.OdThi.NoLength手配依頼 - 2))
                {
                    OdThi.TehaiIraiNo = 手配依頼SetMaxNo(dbConnect, loginUser, OdThi.TehaiIraiNo);
                }
                OdMm.MmNo = OdThi.TehaiIraiNo;
                if (OdMm.MmNo.Length == (NBaseData.DAC.OdMm.NoLength見積依頼 - 1))
                {
                    OdMm.MmNo = 見積依頼SetMaxNo(dbConnect, loginUser, OdMm.MmNo);
                }
                OdMk.MkNo = OdMm.MmNo;
                if (OdMk.MkNo.Length == (NBaseData.DAC.OdMk.NoLength見積回答 - 1))
                {
                    OdMk.MkNo = 見積回答SetMaxNo(dbConnect, loginUser, OdMk.MkNo);
                    OdMk.HachuNo = OdMk.MkNo;
                    OdMk.HachuDate = DateTime.Now;
                    OdMk.Nendo = NBaseUtil.DateTimeUtils.年度開始日(OdMk.HachuDate).Year.ToString();
                }
                OdJry.JryNo = OdMk.MkNo;

                // 新規発注処理で作成される、見積依頼、見積回答は、内部データとして扱いたいので
                // 各番号を加工しておく
                OdMm.MmNo = "Enabled" + OdMm.MmNo;
                OdMk.MkNo = "Enabled" + OdMk.MkNo;


                ret = 手配依頼更新(dbConnect, loginUser, ref OdThi, OdThiItems);
                if (ret)
                {
                    ret = 見積依頼登録(dbConnect, loginUser, ref OdMm, null);
                    if (ret)
                    {
                        //ret = 見積回答登録(dbConnect, loginUser, ref OdMk, null);
                        List<NBaseData.DAC.OdMkItem> OdMkItems = new List<NBaseData.DAC.OdMkItem>();
                        手配依頼から見積回答を作成(ref OdMk, ref OdMkItems, OdThi, OdThiItems);
                        ret = 見積回答登録(dbConnect, loginUser, ref OdMk, OdMkItems);
                        if (ret)
                        {
                            ret = 受領登録(dbConnect, loginUser, ref OdJry, OdJryItems);
                        }
                    }
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

        #region public bool BLC_新規発注処理_保存(...)
        public bool BLC_新規発注処理_保存(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            NBaseData.DAC.OdMm OdMm,
            NBaseData.DAC.OdMk OdMk,
            List<NBaseData.DAC.OdThiItem> OdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItems
            )
        {
            foreach (NBaseData.DAC.OdThiItem thiItem in OdThiItems)
            {
                thiItem.OdThiShousaiItems.Clear();
                foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in OdThiShousaiItems)
                {
                    if (thiShousaiItem.OdThiItemID == thiItem.OdThiItemID)
                    {
                        thiItem.OdThiShousaiItems.Add(thiShousaiItem);
                    }
                }
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                bool ret = false;

                dbConnect.BeginTransaction();

                OdThi.TehaiIraiNo = "";
                OdMm.MmNo = "";
                OdMk.MkNo = "";
                OdMk.HachuNo = " ";

                ret = 手配依頼登録(dbConnect, loginUser, ref OdThi, OdThiItems, null);
                if (ret)
                {
                    ret = 見積依頼登録(dbConnect, loginUser, ref OdMm, null);
                    if (ret)
                    {
                        List<NBaseData.DAC.OdMkItem> OdMkItems = new List<NBaseData.DAC.OdMkItem>();
                        手配依頼から見積回答を作成(ref OdMk, ref OdMkItems, OdThi, OdThiItems);
                        ret = 見積回答登録(dbConnect, loginUser, ref OdMk, OdMkItems);
                    }
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

        #region public bool BLC_新規発注処理_保存情報削除(...)
        public bool BLC_新規発注処理_保存情報削除(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            NBaseData.DAC.OdMm OdMm,
            NBaseData.DAC.OdMk OdMk
            )
        {

            using (DBConnect dbConnect = new DBConnect())
            {
                bool ret = false;

                dbConnect.BeginTransaction();

                ret = NBaseData.DAC.OdMkShousaiItem.DeleteRecords(dbConnect, loginUser, OdMk.OdMkID);
                ret = NBaseData.DAC.OdMkItem.DeleteRecords(dbConnect, loginUser, OdMk.OdMkID);
                ret = OdMk.DeleteRecord(dbConnect, loginUser);
                if (ret)
                {
                    ret = OdMm.DeleteRecord(dbConnect, loginUser);
                    if (ret)
                    {
                        ret = NBaseData.DAC.OdThiShousaiItem.DeleteRecords(dbConnect, loginUser, OdThi.OdThiID);
                        ret = NBaseData.DAC.OdThiItem.DeleteRecords(dbConnect, loginUser, OdThi.OdThiID);
                        ret = OdThi.DeleteRecord(dbConnect, loginUser);
                    }
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


        #region private void 手配依頼から見積回答を作成(...)
        private void 手配依頼から見積回答を作成(ref NBaseData.DAC.OdMk OdMk, ref List<NBaseData.DAC.OdMkItem> OdMkItems, NBaseData.DAC.OdThi OdThi, List<NBaseData.DAC.OdThiItem> OdThiItems)
        {
            foreach (NBaseData.DAC.OdThiItem thiItem in OdThiItems)
            {
                //===========================================
                // 手配依頼品目 →　見積回答品目
                //===========================================
                NBaseData.DAC.OdMkItem mkItem = new NBaseData.DAC.OdMkItem();
                mkItem.OdMkItemID = 新規ID();
                mkItem.OdMkID = OdMk.OdMkID;
                mkItem.Header = thiItem.Header;
                mkItem.MsItemSbtID = thiItem.MsItemSbtID;
                mkItem.ItemName = thiItem.ItemName;
                mkItem.Bikou = thiItem.Bikou;
                mkItem.VesselID = thiItem.VesselID;
                mkItem.RenewDate = OdMk.RenewDate;
                mkItem.RenewUserID = OdMk.RenewUserID;
                mkItem.ShowOrder = thiItem.ShowOrder;

                OdMkItems.Add(mkItem);

                foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in thiItem.OdThiShousaiItems)
                {
                    //===========================================
                    // 手配依頼詳細品目 →　見積回答詳細品目
                    //===========================================
                    NBaseData.DAC.OdMkShousaiItem mkShousaiItem = new NBaseData.DAC.OdMkShousaiItem();
                    mkShousaiItem.OdMkShousaiItemID = 新規ID();
                    mkShousaiItem.OdMkItemID = mkItem.OdMkItemID;
                    mkShousaiItem.ShousaiItemName = thiShousaiItem.ShousaiItemName;
                    mkShousaiItem.MsVesselItemID = thiShousaiItem.MsVesselItemID;
                    mkShousaiItem.MsLoID = thiShousaiItem.MsLoID;
                    mkShousaiItem.Count = thiShousaiItem.Count;
                    mkShousaiItem.MsTaniID = thiShousaiItem.MsTaniID;
                    mkShousaiItem.Bikou = thiShousaiItem.Bikou;
                    mkShousaiItem.VesselID = mkItem.VesselID;
                    mkShousaiItem.RenewDate = mkItem.RenewDate;
                    mkShousaiItem.RenewUserID = mkItem.RenewUserID;
                    mkShousaiItem.ShowOrder = thiShousaiItem.ShowOrder;

                    mkItem.OdMkShousaiItems.Add(mkShousaiItem);
                }

            }
        }
        #endregion
    }
}
