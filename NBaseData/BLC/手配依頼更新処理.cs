using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;

namespace NBaseData.BLC
{
    public class 手配依頼更新処理
    {
        public static bool 未対応(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            // 見積依頼がある場合、未対応にはできない
            List<OdMm> OdMms = OdMm.GetRecordsByOdThiID(dbConnect, loginUser, OdThiID);
            int count = 0;
            foreach (OdMm mm in OdMms)
            {
                if (mm.CancelFlag == 0)
                    count++;
            }
            if (count > 0)
            {
                return true;
            }

            OdThi odThi = OdThi.GetRecord(dbConnect, loginUser, OdThiID);
            string 未対応StatusID = ((int)MsThiIraiStatus.THI_IRAI_STATUS.未対応).ToString();
            if (odThi.MsThiIraiStatusID == 未対応StatusID)
            {
                return true;
            }

            odThi.MsThiIraiStatusID = 未対応StatusID;
            bool ret = odThi.UpdateRecord(dbConnect, loginUser);
            return ret;
        }

        public static bool 見積中(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            return 見積中(dbConnect, loginUser, OdThiID, null);
        }
        public static bool 見積中(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdThiID, string OdMkID)
        {
            //if (OdMkID != null)
            //{
            //    // 受領がある場合、見積中にはできない
            //    List<OdJry> OdJrys = OdJry.GetRecordsByOdMkId(dbConnect, loginUser, OdMkID);
            //    int count = 0;
            //    foreach (OdJry jry in OdJrys)
            //    {
            //        if (jry.CancelFlag == 0)
            //            count++;
            //    }
            //    if (count > 0)
            //    {
            //        return true;
            //    }
            //}
           List<OdJry> OdJrys = OdJry.GetRecordsByOdThiId(dbConnect, loginUser, OdThiID);
            int count = 0;
            foreach (OdJry jry in OdJrys)
            {
                if (jry.CancelFlag == 0)
                    count++;
            }
            if (count > 0)
            {
                return true;
            }

            OdThi odThi = OdThi.GetRecord(dbConnect, loginUser, OdThiID);
            string 見積中StatusID = ((int)MsThiIraiStatus.THI_IRAI_STATUS.見積中).ToString();
            if (odThi.MsThiIraiStatusID == 見積中StatusID)
            {
                return true;
            }

            odThi.MsThiIraiStatusID = 見積中StatusID;
            bool ret = odThi.UpdateRecord(dbConnect, loginUser);
            return ret;
        }

        public static bool 発注済(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int status, string OdThiID)
        {
            if (status != (int)OdMk.STATUS.発注済み)
            {
                return true;
            }

            string 発注済StatusID = ((int)NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.発注済).ToString();
            NBaseData.DAC.OdThi odThi = NBaseData.DAC.OdThi.GetRecord(dbConnect, loginUser, OdThiID);

            if (odThi.MsThiIraiStatusID == 発注済StatusID)
            {
                return true;
            }

            odThi.MsThiIraiStatusID = 発注済StatusID;
            bool ret = odThi.UpdateRecord(dbConnect, loginUser);
            return ret;
        }

        public static bool 完了(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            string 完了StatusID = ((int)NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.完了).ToString();
            NBaseData.DAC.OdThi odThi = NBaseData.DAC.OdThi.GetRecord(dbConnect, loginUser, OdThiID);

            if (odThi.MsThiIraiStatusID == 完了StatusID)
            {
                return true;
            }

            odThi.MsThiIraiStatusID = 完了StatusID;
            bool ret = odThi.UpdateRecord(dbConnect, loginUser);
            return ret;
        }
    }
}
