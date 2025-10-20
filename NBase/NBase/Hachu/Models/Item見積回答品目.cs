using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Hachu.Models
{
    [Serializable]
    public class Item見積回答品目
    {
        public OdMkItem 品目 = null;
        public List<OdMkShousaiItem> 詳細品目s = null;
        public List<OdMkShousaiItem> 削除詳細品目s = null;

        public Item見積回答品目()
        {
            品目 = new OdMkItem();
            詳細品目s = new List<OdMkShousaiItem>();
            削除詳細品目s = new List<OdMkShousaiItem>();
        }

        public static List<Item見積回答品目> GetRecords(string OdMkID)
        {
            List<Item見積回答品目> ret = new List<Item見積回答品目>();

            List<OdMkItem> OdMkItems = null;
            List<OdMkShousaiItem> OdMkShousaiItems = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                OdMkItems = serviceClient.OdMkItem_GetRecordsByOdMkID(NBaseCommon.Common.LoginUser, OdMkID);
                OdMkShousaiItems = serviceClient.OdMkShousaiItem_GetRecordByOdMkID(NBaseCommon.Common.LoginUser, OdMkID);
            }

            foreach (OdMkItem mkItem in OdMkItems)
            {
                Item見積回答品目 retMkItem = new Item見積回答品目();
                retMkItem.品目 = mkItem;

                foreach (OdMkShousaiItem mkShousaiItem in OdMkShousaiItems)
                {
                    if (mkShousaiItem.OdMkItemID == mkItem.OdMkItemID)
                    {
                        retMkItem.詳細品目s.Add(mkShousaiItem);
                    }
                }
                foreach (OdMkShousaiItem mkShousaiItem in retMkItem.詳細品目s)
                {
                    OdMkShousaiItems.Remove(mkShousaiItem);
                }

                ret.Add(retMkItem);
            }
            return ret;
        }


        /// <summary>
        /// ドックオーダから読み取った情報をリストにセットする
        /// </summary>
        /// <param name="odThiITems"></param>
        /// <returns></returns>
        #region public static List<Item見積回答品目> ConvertRecords(int msVesselId, List<OdThiItem> odThiItems)
        public static List<Item見積回答品目> ConvertRecords(int msVesselId, List<OdThiItem> odThiItems)
        {
            Dictionary<string, string> MsShoushuriItemDic = Item手配依頼品目.MakeMsShoushuriItemDic(msVesselId);
            Dictionary<string, string> MsSsShousaiItemDic = Item手配依頼品目.MakeMsSsShousaiItemDic(msVesselId);

            List<Item見積回答品目> ret = new List<Item見積回答品目>();
            if (odThiItems != null)
            {
                foreach (OdThiItem thiItem in odThiItems)
                {
                    OdMkItem mkItem = new OdMkItem();
                    mkItem.OdMkItemID = Hachu.Common.CommonDefine.新規ID();
                    mkItem.Header = thiItem.Header;

                    mkItem.MsItemSbtID = thiItem.MsItemSbtID;
                    mkItem.MsItemSbtName = thiItem.MsItemSbtName;
                    mkItem.ItemName = thiItem.ItemName;
                    mkItem.RenewDate = thiItem.RenewDate;
                    mkItem.RenewUserID = thiItem.RenewUserID;

                    if (MsShoushuriItemDic.ContainsKey(mkItem.ItemName) == false)
                    {
                        mkItem.SaveDB = true;
                    }

                    Item見積回答品目 見積回答品目 = new Item見積回答品目();
                    見積回答品目.品目 = mkItem;

                    foreach (OdThiShousaiItem otsi in thiItem.OdThiShousaiItems)
                    {
                        OdMkShousaiItem omsi = new OdMkShousaiItem();
                        omsi.OdMkShousaiItemID = System.Guid.NewGuid().ToString();
                        omsi.OdMkItemID = mkItem.OdMkItemID;
                        omsi.ShousaiItemName = otsi.ShousaiItemName;
                        omsi.MsTaniID = otsi.MsTaniID;
                        omsi.MsTaniName = otsi.MsTaniName;
                        omsi.Count = otsi.Sateisu;
                        omsi.Bikou = otsi.Bikou;
                        omsi.RenewDate = otsi.RenewDate;
                        omsi.RenewUserID = otsi.RenewUserID;

                        if (MsSsShousaiItemDic.ContainsKey(otsi.ShousaiItemName) == false)
                        {
                            omsi.SaveDB = true;
                        }

                        見積回答品目.詳細品目s.Add(omsi);
                    }


                    int insertPos = 0;
                    bool sameHeader = false;
                    foreach (Item見積回答品目 品目 in ret)
                    {
                        if (品目.品目.Header == 見積回答品目.品目.Header)
                        {
                            sameHeader = true;
                        }
                        else if (sameHeader)
                        {
                            break;
                        }
                        insertPos++;
                    }
                    if (insertPos >= ret.Count)
                    {
                        ret.Add(見積回答品目);
                    }
                    else
                    {
                        ret.Insert(insertPos, 見積回答品目);
                    }
                }
            }
            return ret;
        }
        #endregion
    }
}
