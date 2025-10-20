using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using Hachu.Models;

namespace Hachu.BLC
{
    [Serializable]
    public class 新規見積依頼更新処理
    {
        public static bool 新規(ref OdThi 対象手配依頼, ref OdMm 対象見積依頼, ref OdMk 対象見積回答, List<Item手配依頼品目> 手配品目s)
        {
            List<OdThiItem> 新規品目s = new List<OdThiItem>();
            List<OdThiShousaiItem> 新規詳細品目s = new List<OdThiShousaiItem>();
            List<OdAttachFile> 新規添付s = new List<OdAttachFile>();
            手配依頼更新処理.セット(ref 対象手配依頼, 手配品目s, ref 新規品目s, ref 新規詳細品目s, ref 新規添付s);
            対象手配依頼.MsThiIraiStatusID = ((int)NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.見積中).ToString();
            
            // 新規見積の場合、査定数は、入力された依頼数とする
            foreach (OdThiShousaiItem thiShousaiItem in 新規詳細品目s)
            {
                thiShousaiItem.Sateisu = thiShousaiItem.Count;
            }
            見積依頼更新処理.セット(ref 対象見積依頼, 対象手配依頼);
            見積回答更新処理.セット(ref 対象見積回答, 対象見積依頼);

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_新規見積依頼処理_新規(
                                                        NBaseCommon.Common.LoginUser,
                                                        対象手配依頼,
                                                        対象見積依頼,
                                                        対象見積回答,
                                                        新規品目s,
                                                        新規詳細品目s,
                                                        新規添付s);

                対象手配依頼 = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID);
                対象見積依頼 = serviceClient.OdMm_GetRecord(NBaseCommon.Common.LoginUser, 対象見積依頼.OdMmID);
                対象見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 対象見積回答.OdMkID);
            }
            return ret;
        }
    }
}
