using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DS;
using NBaseData.DAC;

namespace NBase.util
{
    public class DouseiProc
    {
        public static double MinutesToNextPort(MsVessel vessel, string fromPortId, string toPortId)
        {
            double ret = 0;

            // 速力（設定がない場合、何もしない）
            var knot = vessel.Knot;
            if (knot == 0)
                return ret;

            // 港間の距離設定がない場合、何もしない
            List<MsBashoKyori> bashoKyoriList = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                bashoKyoriList = serviceClient.MsBashoKyori_GetRecordsByKyori1Kyori2(NBaseCommon.Common.LoginUser, fromPortId, toPortId);
                bashoKyoriList = bashoKyoriList.Where(o => (o.MsBashoID1 == fromPortId && o.MsBashoID2 == toPortId) || (o.MsBashoID1 == toPortId && o.MsBashoID2 == fromPortId)).ToList();
            }
            if (bashoKyoriList.Count == 0)
                return ret;

            // 距離（Km)
            var kyori = bashoKyoriList[0].Kyori;

            // マイルに換算する
            var mile = kyori * 1000 / 1852;

            // 時間(分)
            ret = mile / (double)knot * 60;

            return ret;
        }
    }
}
