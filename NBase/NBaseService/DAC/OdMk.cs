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
        List<NBaseData.DAC.OdMk> OdMk_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.OdMk OdMk_GetRecordByOdThiID(NBaseData.DAC.MsUser loginUser, string OdThiID);

        [OperationContract]
        List<NBaseData.DAC.OdMk> OdMk_GetRecordsByOdMmID(NBaseData.DAC.MsUser loginUser, string OdMmID);

        [OperationContract]
        NBaseData.DAC.OdMk OdMk_GetRecordByWebKey(NBaseData.DAC.MsUser loginUser, string WebKey);

        [OperationContract]
        List<NBaseData.DAC.OdMk> OdMk_GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, int status, NBaseData.DS.OdThiFilter filter);

        [OperationContract]
        NBaseData.DAC.OdMk OdMk_GetRecord(NBaseData.DAC.MsUser loginUser, string odMkID);

        [OperationContract]
        bool OdMk_Insert();

        [OperationContract]
        bool OdMk_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMk info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdMk> OdMk_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdMk> ret;
            ret = NBaseData.DAC.OdMk.GetRecords(loginUser);
            return ret;
        }

        public NBaseData.DAC.OdMk OdMk_GetRecordByOdThiID(NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            NBaseData.DAC.OdMk ret = null;
            ret = NBaseData.DAC.OdMk.GetRecordByOdThiID(loginUser, OdThiID);
            return ret;
        }

        public List<NBaseData.DAC.OdMk> OdMk_GetRecordsByOdMmID(NBaseData.DAC.MsUser loginUser, string OdMmID)
        {
            List<NBaseData.DAC.OdMk> ret;
            ret = NBaseData.DAC.OdMk.GetRecordsByOdMmID(loginUser, OdMmID);
            return ret;
        }

        public NBaseData.DAC.OdMk OdMk_GetRecordByWebKey(NBaseData.DAC.MsUser loginUser, string WebKey)
        {
            NBaseData.DAC.OdMk ret = null;
            ret = NBaseData.DAC.OdMk.GetRecordByWebKey(loginUser, WebKey);
            return ret;
        }

        public List<NBaseData.DAC.OdMk> OdMk_GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, int status, NBaseData.DS.OdThiFilter filter)
        {
            List<NBaseData.DAC.OdMk> ret;
            ret = NBaseData.DAC.OdMk.GetRecordsByFilter(loginUser, status, filter);
            return ret;
        }

        public NBaseData.DAC.OdMk OdMk_GetRecord(NBaseData.DAC.MsUser loginUser, string odMkID)
        {
            NBaseData.DAC.OdMk ret;
            ret = NBaseData.DAC.OdMk.GetRecord(loginUser, odMkID);
            return ret;
        }

        public bool OdMk_Insert()
        {
            return true;
        }

        public bool OdMk_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMk info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
