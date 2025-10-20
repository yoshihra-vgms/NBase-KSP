using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using System.IO;
using NBaseData.DAC;
using NBaseData.DS;
using System.Collections.Generic;
using NBaseData.BLC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_KK船_種別変更(MsUser loginUser, OdThi odThi);
    }
    public partial class Service
    {
        public bool BLC_KK船_種別変更(MsUser loginUser, OdThi odThi)
        {
            return KK船_種別変更.変更(loginUser, odThi);
        }
    }
}