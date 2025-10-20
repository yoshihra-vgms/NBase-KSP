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
        List<NBaseData.DAC.MsRegional> MsRegional_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.MsRegional> MsRegional_SearchRecords(NBaseData.DAC.MsUser loginUser, string regionalCode, string regionalName);

        [OperationContract]
        bool MsRegional_InsertRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsRegional msRegional);

        [OperationContract]
        bool MsRegional_UpdateRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsRegional msRegional);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsRegional> MsRegional_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return NBaseData.DAC.MsRegional.GetRecords(loginUser);
        }

        public List<NBaseData.DAC.MsRegional> MsRegional_SearchRecords(NBaseData.DAC.MsUser loginUser, string regionalCode, string regionalName)
        {
            return NBaseData.DAC.MsRegional.SearchRecords(loginUser, regionalCode, regionalName);
        }
        public bool MsRegional_InsertRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsRegional msRegional)
        {
            return msRegional.InsertRecord(loginUser);
        }
        public bool MsRegional_UpdateRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsRegional msRegional)
        {
            return msRegional.UpdateRecord(loginUser);
        }
    }
}
