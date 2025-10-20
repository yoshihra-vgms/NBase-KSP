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
        List<NBaseData.DAC.MsSsShousaiItem> MsSsShousaiItem_GetRecordByMsVesselID(NBaseData.DAC.MsUser loginUser, int MsVesslID);

        [OperationContract]
        bool MsSsShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsSsShousaiItem info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsSsShousaiItem> MsSsShousaiItem_GetRecordByMsVesselID(NBaseData.DAC.MsUser loginUser, int MsVesslID)
        {
            List<NBaseData.DAC.MsSsShousaiItem> ret;
            ret = NBaseData.DAC.MsSsShousaiItem.GetRecordsByMsVesselID(loginUser, MsVesslID);
            return ret;
        }

        public bool MsSsShousaiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsSsShousaiItem info)
        {
            return info.InsertRecord(loginUser);
        }
    }
}
