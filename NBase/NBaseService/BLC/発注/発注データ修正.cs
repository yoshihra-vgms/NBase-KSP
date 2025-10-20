using NBaseData.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_区分_仕様型式編集(MsUser loginUser, string thiIraiNo, string msItemSbtId, int categoryNo);
    }

    public partial class Service
    {
        public bool BLC_区分_仕様型式編集(MsUser loginUser, string thiIraiNo, string msItemSbtId, int categoryNo)
        {
            bool ret = true;

            var thiList = OdThi.GetRecords(loginUser);
            var thi = thiList.Where(o => o.TehaiIraiNo == thiIraiNo).FirstOrDefault();
            if (thi == null)
                return false;

            var odThiId = thi.OdThiID;

            bool Is船用品 = false;
            if (thi.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品))
                Is船用品 = true;


            List<MsItemSbt> itemSbtList = MsItemSbt.GetRecords(loginUser);
            List<MsVesselItemCategory> vesselItemCategoryList = MsVesselItemCategory.GetRecords(loginUser);

            List<OdMm> mmList = OdMm.GetRecordsByOdThiID(loginUser, odThiId);
            List<OdMk> mkList = OdMk.GetRecordsByOdThiID(loginUser, odThiId);
            List<OdJry> jryList = OdJry.GetRecordsByOdThiId(loginUser, odThiId);
            List<OdShr> shrList = OdShr.GetRecordsByOdThiId(loginUser, odThiId);

            List<OdThiItem> thiItemList = OdThiItem.GetRecordsByOdThiID(loginUser, odThiId);
            foreach (OdThiItem item in thiItemList)
            {
                item.MsItemSbtID = msItemSbtId;
                item.Header = itemSbtList.Where(o => o.MsItemSbtID == msItemSbtId).First().ItemSbtName;

                if (Is船用品)
                {
                    item.ItemName = vesselItemCategoryList.Where(o => o.CategoryNumber == categoryNo).First().CategoryName;
                }

                item.UpdateRecord(loginUser);
            }


            foreach (OdMm mm in mmList)
            {
                List<OdMmItem> itemList = OdMmItem.GetRecordsByOdMmID(loginUser, mm.OdMmID);
                foreach (OdMmItem item in itemList)
                {
                    item.MsItemSbtID = msItemSbtId;
                    item.Header = itemSbtList.Where(o => o.MsItemSbtID == msItemSbtId).First().ItemSbtName;

                    if (Is船用品)
                    {
                        item.ItemName = vesselItemCategoryList.Where(o => o.CategoryNumber == categoryNo).First().CategoryName;
                    }

                    item.UpdateRecord(loginUser);
                }
            }

            if (mkList != null)
            { 
                foreach (OdMk mk in mkList)
                {
                    List<OdMkItem> itemList = OdMkItem.GetRecordsByOdMkID(loginUser, mk.OdMkID);
                    foreach (OdMkItem item in itemList)
                    {
                        item.MsItemSbtID = msItemSbtId;
                        item.Header = itemSbtList.Where(o => o.MsItemSbtID == msItemSbtId).First().ItemSbtName;

                        if (Is船用品)
                        {
                            item.ItemName = vesselItemCategoryList.Where(o => o.CategoryNumber == categoryNo).First().CategoryName;
                        }

                        item.UpdateRecord(loginUser);
                    }
                }
            }

            foreach (OdJry jry in jryList)
            {
                List<OdJryItem> itemList = OdJryItem.GetRecordsByOdJryID(loginUser, jry.OdJryID);
                foreach (OdJryItem item in itemList)
                {
                    item.MsItemSbtID = msItemSbtId;
                    item.Header = itemSbtList.Where(o => o.MsItemSbtID == msItemSbtId).First().ItemSbtName;

                    if (Is船用品)
                    {
                        item.ItemName = vesselItemCategoryList.Where(o => o.CategoryNumber == categoryNo).First().CategoryName;
                    }

                    item.UpdateRecord(loginUser);
                }
            }

            foreach (OdShr shr in shrList)
            {
                List<OdShrItem> itemList = OdShrItem.GetRecordsByOdShrID(loginUser, shr.OdShrID);
                foreach (OdShrItem item in itemList)
                {
                    item.MsItemSbtID = msItemSbtId;
                    item.Header = itemSbtList.Where(o => o.MsItemSbtID == msItemSbtId).First().ItemSbtName;

                    if (Is船用品)
                    {
                        item.ItemName = vesselItemCategoryList.Where(o => o.CategoryNumber == categoryNo).First().CategoryName;
                    }

                    item.UpdateRecord(loginUser);
                }
            }


            return ret;
        }
    }
}