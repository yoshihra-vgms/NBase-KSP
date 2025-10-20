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
        List<NBaseData.DAC.OdMmItem> OdMmItem_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.OdMmItem> OdMmItem_GetRecordsByOdMmID(NBaseData.DAC.MsUser loginUser, string odMmID);
        
        [OperationContract]
        List<NBaseData.DAC.OdMmItem> OdMmItem_GetRecordsByOdMkID(NBaseData.DAC.MsUser loginUser, string odMkID);
        
        //[OperationContract]
        //bool OdMmItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMmItem info);

        //[OperationContract]
        //bool OdMmItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMmItem info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdMmItem> OdMmItem_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdMmItem> ret;
            ret = NBaseData.DAC.OdMmItem.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdMmItem> OdMmItem_GetRecordsByOdMmID(NBaseData.DAC.MsUser loginUser, string odMmID)
        {
            List<NBaseData.DAC.OdMmItem> ret;
            ret = NBaseData.DAC.OdMmItem.GetRecordsByOdMmID(loginUser, odMmID);
            return ret;
        }

        public List<NBaseData.DAC.OdMmItem> OdMmItem_GetRecordsByOdMkID(NBaseData.DAC.MsUser loginUser, string odMkID)
        {
            List<NBaseData.DAC.OdMmItem> ret;
            ret = NBaseData.DAC.OdMmItem.GetRecordsByOdMkID(loginUser, odMkID);
            return ret;
        }

        //public bool OdMmItem_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMmItem info)
        //{
        //    return info.InsertRecord(loginUser);
        //}

        //public bool OdMmItem_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMmItem info)
        //{
        //    return info.UpdateRecord(loginUser);
        //}
    }
}
