using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Hachu.Models
{
    [Serializable]
    public class Item受領品目
    {
        public OdJryItem 品目 = null;
        public List<OdJryShousaiItem> 詳細品目s = null;
        public List<OdJryShousaiItem> 削除詳細品目s = null;

        public Item受領品目()
        {
            品目 = new OdJryItem();
            詳細品目s = new List<OdJryShousaiItem>();
            削除詳細品目s = new List<OdJryShousaiItem>();
        }

        public static List<Item受領品目> GetRecords(string OdJryID)
        {
            List<Item受領品目> ret = new List<Item受領品目>();

            List<OdJryItem> OdJryItems = null;
            List<OdJryShousaiItem> OdJryShousaiItems = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                OdJryItems = serviceClient.OdJryItem_GetRecordsByOdJryID(NBaseCommon.Common.LoginUser, OdJryID);
                OdJryShousaiItems = serviceClient.OdJryShousaiItem_GetRecordByOdJryID(NBaseCommon.Common.LoginUser, OdJryID);
            }

            foreach (OdJryItem jryItem in OdJryItems)
            {
                Item受領品目 retJryItem = new Item受領品目();
                retJryItem.品目 = jryItem;

                foreach (OdJryShousaiItem jryShousaiItem in OdJryShousaiItems)
                {
                    if (jryShousaiItem.OdJryItemID == jryItem.OdJryItemID)
                    {
                        retJryItem.詳細品目s.Add(jryShousaiItem);
                    }
                }
                foreach (OdJryShousaiItem jryShousaiItem in retJryItem.詳細品目s)
                {
                    OdJryShousaiItems.Remove(jryShousaiItem);
                }

                ret.Add(retJryItem);
            }
            return ret;
        }

    }
}
