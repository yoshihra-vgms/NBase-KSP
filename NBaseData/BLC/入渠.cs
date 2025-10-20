using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.BLC
{
    public class 入渠
    {
        public static List<OdThiItem> BLC_直近ドックオーダー品目(MsUser loginUser, int msVesselID, string msThiIraiShousaiID)
        {
            List<OdThiItem> result = new List<OdThiItem>();

            // 今年度を含まない直近の入渠（指定検査種類）を取得する
            DateTime toDate = NBaseData.DS.Constants.BusinessYearFrom(DateTime.Today); // 今年度の初日
            OdThi latestNyukyoThi = OdThi.GetLatestNyukyoRecord(loginUser, msVesselID, msThiIraiShousaiID, toDate);
            if (latestNyukyoThi != null)
            {
                // 直近の入渠（指定検査種類）の行われた年
                DateTime fromDate = NBaseData.DS.Constants.BusinessYearFrom(latestNyukyoThi.ThiIraiDate);
                toDate = NBaseData.DS.Constants.BusinessYearTo(latestNyukyoThi.ThiIraiDate);

                // 直近の入渠（指定検査種類）の行われた年のすべて手配（入渠：指定検査種類）
                List<OdThi> thiList = OdThi.GetRecordsByMsThiIraiShousaiId(loginUser, msVesselID, msThiIraiShousaiID, fromDate, toDate);

                foreach (OdThi t in thiList)
                {
                    List<OdThiItem> tehaiItems = OdThiItem.GetRecordsByOdThiID(loginUser, t.OdThiID);
                    List<OdThiShousaiItem> tehaiShousaiItems = OdThiShousaiItem.GetRecordsByOdThiID(loginUser, t.OdThiID);

                    foreach (OdThiItem ti in tehaiItems)
                    {
                        foreach (OdThiShousaiItem si in tehaiShousaiItems)
                        {
                            if (si.OdThiItemID == ti.OdThiItemID)
                            {
                                ti.OdThiShousaiItems.Add(si);
                            }
                        }
                        foreach (OdThiShousaiItem si in ti.OdThiShousaiItems)
                        {
                            tehaiShousaiItems.Remove(si);
                        }
                    }

                    result.AddRange(tehaiItems);
                }
            }
            return result;
        }
    }
}
