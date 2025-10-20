using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ServiceModel;

using NBaseData.DAC;


namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        DateTime SiGetsujiShimeBi_ToFrom_船員_月次締日(NBaseData.DAC.MsUser loginUser, DateTime date);
    }


    public partial class Service
    {
        /// <summary>
        /// 締め日を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DateTime SiGetsujiShimeBi_ToFrom_船員_月次締日(NBaseData.DAC.MsUser loginUser, DateTime date)
        {       
            return MsSiGetsujiShimeBi.ToFrom_船員_月次締日(loginUser, date);
        }

    }
}
