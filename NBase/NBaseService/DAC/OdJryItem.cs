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
        List<NBaseData.DAC.OdJryItem> OdJryItem_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.OdJryItem> OdJryItem_GetRecordsByOdJryID(NBaseData.DAC.MsUser loginUser, string odJryID);
        
        [OperationContract]
        bool OdJryItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJryItem info);

        [OperationContract]
        bool OdJryItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJryItem info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdJryItem> OdJryItem_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdJryItem> ret;
            ret = NBaseData.DAC.OdJryItem.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdJryItem> OdJryItem_GetRecordsByOdJryID(NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            List<NBaseData.DAC.OdJryItem> ret;
            ret = NBaseData.DAC.OdJryItem.GetRecordsByOdJryID(loginUser, odJryID);
            return ret;
        }

        public bool OdJryItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJryItem info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool OdJryItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJryItem info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
