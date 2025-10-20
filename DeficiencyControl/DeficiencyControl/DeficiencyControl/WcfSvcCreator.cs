using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DeficiencyControl.WcfServiceDeficiency;
using DeficiencyControl.Files;

namespace DeficiencyControl
{
    /// <summary>
    /// WCFサービス作成関数
    /// </summary>
    public class WcfSvcCreator
    {
        /// <summary>
        /// サービス参照設定関数
        /// </summary>
        /// <returns></returns>
        public static ServiceDeficiencyClient Create(SvcManager.SvcInfoData info)
        {
            //サービスConfig名
            //string endpointname = "WcfServiceDeficiency_Config_Cloud";
            //string endpointname = "WcfServiceDeficiency_Config_SV";
            string endpointname = info.EpName;



            //設定読み込み
            ProxySetting settings = ProxySetting.Read();
            ServiceDeficiencyClient ans = new ServiceDeficiencyClient(endpointname);

            //プロキシを使用する？
            if (settings.IsUseProxy() == true)
            {
                System.ServiceModel.WSHttpBinding b = (ans.Endpoint.Binding as System.ServiceModel.WSHttpBinding);
                b.ProxyAddress = new Uri(settings.ProxyURL);
            }

            return ans;
        }
    }
}
