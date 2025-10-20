using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Hachu.Models
{
    [Serializable]
    public class Item見積依頼品目
    {
        public OdMmItem 品目 = null;
        public List<OdMmShousaiItem> 詳細品目s = null;

        public Item見積依頼品目()
        {
            品目 = new OdMmItem();
            詳細品目s = new List<OdMmShousaiItem>();
        }

        public static List<Item見積依頼品目> GetRecords(string OdMmID)
        {
            List<OdMmItem> OdMmItems = null;
            List<OdMmShousaiItem> OdMmShousaiItems = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                OdMmItems = serviceClient.OdMmItem_GetRecordsByOdMmID(NBaseCommon.Common.LoginUser, OdMmID);
                OdMmShousaiItems = serviceClient.OdMmShousaiItem_GetRecordByOdMmID(NBaseCommon.Common.LoginUser, OdMmID);
            }
            return MakeItem見積依頼品目List(OdMmItems, OdMmShousaiItems);
        }

        public static List<Item見積依頼品目> GetRecordsByOdMkID(string OdMkID)
        {
            List<OdMmItem> OdMmItems = null;
            List<OdMmShousaiItem> OdMmShousaiItems = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                OdMmItems = serviceClient.OdMmItem_GetRecordsByOdMkID(NBaseCommon.Common.LoginUser, OdMkID);
                OdMmShousaiItems = serviceClient.OdMmShousaiItem_GetRecordByOdMkID(NBaseCommon.Common.LoginUser, OdMkID);
            }
            return MakeItem見積依頼品目List(OdMmItems, OdMmShousaiItems);
        }

        private static List<Item見積依頼品目> MakeItem見積依頼品目List(List<OdMmItem> OdMmItems, List<OdMmShousaiItem> OdMmShousaiItems)
        {
            List<Item見積依頼品目> ret = new List<Item見積依頼品目>();
            foreach (OdMmItem mmItem in OdMmItems)
            {
                Item見積依頼品目 retMmItem = new Item見積依頼品目();
                retMmItem.品目 = mmItem;

                foreach (OdMmShousaiItem mmShousaiItem in OdMmShousaiItems)
                {
                    if (mmShousaiItem.OdMmItemID == mmItem.OdMmItemID)
                    {
                        retMmItem.詳細品目s.Add(mmShousaiItem);
                    }
                }
                foreach (OdMmShousaiItem mmShousaiItem in retMmItem.詳細品目s)
                {
                    OdMmShousaiItems.Remove(mmShousaiItem);
                }

                ret.Add(retMmItem);
            }
            return ret;
        }

    }
}
