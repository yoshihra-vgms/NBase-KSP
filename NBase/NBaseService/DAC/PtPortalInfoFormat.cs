using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using NBaseData.DAC;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        PtPortalInfoFormat PtPortalInfoFormat_GetRecordByShubet_Koumoku_Kubun(MsUser loginUser, string msPortalInfoShubetuId, string msPortalInfoKoumokuId);
    }

    public partial class Service
    {
        public PtPortalInfoFormat PtPortalInfoFormat_GetRecordByShubet_Koumoku_Kubun(MsUser loginUser, string msPortalInfoShubetuId, string msPortalInfoKoumokuId)
        {
            return PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(loginUser, msPortalInfoShubetuId, msPortalInfoKoumokuId, "");
        }
    }
}