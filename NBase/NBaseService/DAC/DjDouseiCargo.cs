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
        List<DjDouseiCargo> DjDouseiCargo_GetRecordsMsCargoID(MsUser loginUser, int ms_cargo_id);
    }

    public partial class Service
    {
        public List<DjDouseiCargo> DjDouseiCargo_GetRecordsMsCargoID(MsUser loginUser, int ms_cargo_id)
        {
            return DjDouseiCargo.GetRecordsMsCargoID(loginUser, ms_cargo_id);
        }
    }
}