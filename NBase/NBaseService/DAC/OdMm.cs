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
using NBaseData.DAC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.DAC.OdMm> OdMm_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.OdMm> OdMm_GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdThiFilter filter);

        [OperationContract]
        NBaseData.DAC.OdMm OdMm_GetRecord(NBaseData.DAC.MsUser loginUser, string odMmID);

        [OperationContract]
        bool OdMm_Insert();

        [OperationContract]
        bool OdMm_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMm info);

        [OperationContract]
        List<OdMm> OdMm_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdMm> OdMm_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdMm> ret;
            ret = NBaseData.DAC.OdMm.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdMm> OdMm_GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdThiFilter filter)
        {
            List<NBaseData.DAC.OdMm> ret;
            ret = NBaseData.DAC.OdMm.GetRecordsByFilter(loginUser, filter);
            return ret;
        }

        public NBaseData.DAC.OdMm OdMm_GetRecord(NBaseData.DAC.MsUser loginUser, string odMmID)
        {
            NBaseData.DAC.OdMm ret;
            ret = NBaseData.DAC.OdMm.GetRecord(loginUser, odMmID);
            return ret;
        }

        public bool OdMm_Insert()
        {
            return true;
        }

        public bool OdMm_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdMm info)
        {
            return info.UpdateRecord(loginUser);
        }


        public List<OdMm> OdMm_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id)
        {
            return OdMm.GetRecordsByMsUserID(loginUser, ms_user_id);
        }
    }
}
