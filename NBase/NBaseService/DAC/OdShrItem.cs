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
        List<NBaseData.DAC.OdShrItem> OdShrItem_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.OdShrItem> OdShrItem_GetRecordsByOdShrID(NBaseData.DAC.MsUser loginUser, string odShrID);
        
        [OperationContract]
        bool OdShrItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrItem info);

        [OperationContract]
        bool OdShrItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrItem info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdShrItem> OdShrItem_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdShrItem> ret;
            ret = NBaseData.DAC.OdShrItem.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdShrItem> OdShrItem_GetRecordsByOdShrID(NBaseData.DAC.MsUser loginUser, string odShrID)
        {
            List<NBaseData.DAC.OdShrItem> ret;
            ret = NBaseData.DAC.OdShrItem.GetRecordsByOdShrID(loginUser, odShrID);
            return ret;
        }

        public bool OdShrItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrItem info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool OdShrItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShrItem info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
