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
        NBaseData.DAC.OdJryGaisan OdJryGaisan_GetRecordByOdJryId(NBaseData.DAC.MsUser loginUser, string odJryId);

        [OperationContract]
        bool OdJryGaisan_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJryGaisan info);
    }

    public partial class Service
    {
        public NBaseData.DAC.OdJryGaisan OdJryGaisan_GetRecordByOdJryId(NBaseData.DAC.MsUser loginUser, string odJryId)
        {
            return NBaseData.DAC.OdJryGaisan.GetRecordByOdJryID(loginUser, odJryId);
        }

        public bool OdJryGaisan_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJryGaisan info)
        {
            return info.InsertRecord(loginUser);
        }
    }
}
