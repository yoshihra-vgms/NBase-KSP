using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Hachu.Models
{
    [Serializable]
    public class Item支払品目
    {
        public OdShrItem 品目 = null;
        public List<OdShrShousaiItem> 詳細品目s = null;
        public List<OdShrShousaiItem> 削除詳細品目s = null;

        public Item支払品目()
        {
            品目 = new OdShrItem();
            詳細品目s = new List<OdShrShousaiItem>();
            削除詳細品目s = new List<OdShrShousaiItem>();
        }

        public static List<Item支払品目> GetRecords(string OdShrID)
        {
            List<Item支払品目> ret = new List<Item支払品目>();

            List<OdShrItem> OdShrItems = null;
            List<OdShrShousaiItem> OdShrShousaiItems = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                OdShrItems = serviceClient.OdShrItem_GetRecordsByOdShrID(NBaseCommon.Common.LoginUser, OdShrID);
                OdShrShousaiItems = serviceClient.OdShrShousaiItem_GetRecordByOdShrID(NBaseCommon.Common.LoginUser, OdShrID);
            }

            foreach (OdShrItem shrItem in OdShrItems)
            {
                Item支払品目 retShrItem = new Item支払品目();
                retShrItem.品目 = shrItem;

                foreach (OdShrShousaiItem shrShousaiItem in OdShrShousaiItems)
                {
                    if (shrShousaiItem.OdShrItemID == shrItem.OdShrItemID)
                    {
                        retShrItem.詳細品目s.Add(shrShousaiItem);
                    }
                }
                foreach (OdShrShousaiItem shrShousaiItem in retShrItem.詳細品目s)
                {
                    OdShrShousaiItems.Remove(shrShousaiItem);
                }

                ret.Add(retShrItem);
            }
            return ret;
        }

    }
}
