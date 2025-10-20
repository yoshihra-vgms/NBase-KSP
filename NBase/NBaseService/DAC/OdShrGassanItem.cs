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
using System.Collections.Generic;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.DAC.OdShrGassanItem> OdShrGassanItem_GetRecords(NBaseData.DAC.MsUser loginUser, string odShrGassanHeadId);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdShrGassanItem> OdShrGassanItem_GetRecords(NBaseData.DAC.MsUser loginUser, string odShrGassanHeadId)
        {
            return NBaseData.DAC.OdShrGassanItem.GetRecordsByOdShrGassanHeadID(loginUser, odShrGassanHeadId);
        }
    }
}
