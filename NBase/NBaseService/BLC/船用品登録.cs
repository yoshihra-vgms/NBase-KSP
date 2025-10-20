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
using NBaseData.DAC;
using NBaseData.BLC;
using System.Collections.Generic;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_船用品登録(MsUser loginUser, MsVesselItemVessel msVesselItemVessel);
        
        [OperationContract]
        List<OdChozoShousai> BLC_Get特定船用品在庫(MsUser loginUser, int msVesselId);
    }

    public partial class Service
    {
        public bool BLC_船用品登録(MsUser loginUser, MsVesselItemVessel msVesselItemVessel)
        {
            return 船用品.BLC_船用品登録(loginUser, msVesselItemVessel);
        }

        public List<OdChozoShousai> BLC_Get特定船用品在庫(MsUser loginUser, int msVesselId)
        {
            return 船用品.BLC_Get特定船用品在庫(loginUser, msVesselId);
        }
    }
}
