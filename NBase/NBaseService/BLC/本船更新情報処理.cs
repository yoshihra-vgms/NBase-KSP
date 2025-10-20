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


namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool 本船更新_船員_乗船_登録(NBaseData.DAC.MsUser logiuser, SiCard siCard);

    }

    public partial class Service
    {
        public bool 本船更新_船員_乗船_登録(NBaseData.DAC.MsUser logiuser, SiCard siCard)
        {
            return NBaseData.BLC.本船更新情報処理.船員_乗船_登録(logiuser, siCard);
        }
    }
}
