using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Hachu.Models
{
    [Serializable]
    public class 見積依頼先
    {
        public string OdMkID;
        public string CustomerName;
        public string TantouMailAddress;
        public DateTime CreateDate;
        public List<Item見積回答品目> 見積回答品目s;

        public static List<見積依頼先> 取得(string OdMmID)
        {
            List<見積依頼先> ret = new List<見積依頼先>();

            List<OdMk> OdMks = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                OdMks = serviceClient.OdMk_GetRecordsByOdMmID(NBaseCommon.Common.LoginUser, OdMmID);
            }
            foreach (OdMk mk in OdMks)
            {
                見積依頼先 mmSaki = new 見積依頼先();
                mmSaki.OdMkID = mk.OdMkID;
                mmSaki.CustomerName = mk.MsCustomerName;
                mmSaki.TantouMailAddress = mk.TantouMailAddress;
                mmSaki.CreateDate = mk.CreateDate;
                mmSaki.見積回答品目s = Item見積回答品目.GetRecords(mk.OdMkID);

                ret.Add(mmSaki);            
            }
            return ret;
        }
    }
}
