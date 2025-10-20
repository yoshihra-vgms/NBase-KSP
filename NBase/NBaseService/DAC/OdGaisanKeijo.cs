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
        List<OdGaisanKeijo> OdGaisanKeijo_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id);
        
        [OperationContract]
        string OdGaisanKeijo_GetLatestNengetsu(NBaseData.DAC.MsUser loginUser);
    }

    public partial class Service
    {
        public List<OdGaisanKeijo> OdGaisanKeijo_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id)
        {
            return OdGaisanKeijo.GetRecordsByMsUserID(loginUser, ms_user_id);
        }

        public string OdGaisanKeijo_GetLatestNengetsu(NBaseData.DAC.MsUser loginUser)
        {
            return OdGaisanKeijo.GetLatestNengetsu(loginUser);
        }

    }
}
