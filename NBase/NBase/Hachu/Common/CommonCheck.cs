using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace Hachu.Common
{
    [Serializable]
    public class CommonCheck
    {

        /// <summary>
        /// 指定年月を指定し、保存ができるかどうかを確認する
        /// 引数：年月日
        /// 返り値：保存可能かどうか？
        /// </summary>
        /// <param name="ymd"></param>
        /// <returns></returns>
        public static bool Check保存(DateTime ymd)
        {
            OdGetsujiShime shime = null;

            //最終月を取得
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                shime = serviceClient.OdGetsujiShime_GetRecordByLastDate(NBaseCommon.Common.LoginUser);
            }

            //取得できない時も保存可能
            if (shime == null)
            {
                return true;
            }

            //年と月に分解
            int shimey = 0;
            int shimem = 0;
            try
            {
                shimey = Convert.ToInt32(shime.NenGetsu.Substring(0, 4));
                shimem = Convert.ToInt32(shime.NenGetsu.Substring(4));
            }
            catch
            {
            }

            //年が前の時は編集できない
            if (ymd.Year < shimey)
            {
                return false;
            }

            //年が同じで月が若かった時も編集不可
            if (ymd.Year == shimey && ymd.Month <= shimem)
            {
                return false;
            }

            return true;
        }
    }
}
