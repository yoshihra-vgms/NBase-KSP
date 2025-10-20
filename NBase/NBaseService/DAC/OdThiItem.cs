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
        List<NBaseData.DAC.OdThiItem> OdThiItem_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.OdThiItem> OdThiItem_GetRecordByOdThiID(NBaseData.DAC.MsUser loginUser, string odThiID);

        [OperationContract]
        bool OdThiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdThiItem info);

        [OperationContract]
        bool OdThiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdThiItem info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdThiItem> OdThiItem_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdThiItem> ret;
            ret = NBaseData.DAC.OdThiItem.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdThiItem> OdThiItem_GetRecordByOdThiID(NBaseData.DAC.MsUser loginUser, string odThiID)
        {
            List<NBaseData.DAC.OdThiItem> ret;
            ret = NBaseData.DAC.OdThiItem.GetRecordsByOdThiID(loginUser, odThiID);
            return ret;
        }

        public bool OdThiItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdThiItem info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool OdThiItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdThiItem info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
