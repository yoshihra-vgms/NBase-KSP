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
        List<NBaseData.DAC.OdThi> OdThi_GetRecords(NBaseData.DAC.MsUser loginUser);
        
        [OperationContract]
        List<NBaseData.DAC.OdThi> OdThi_GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdThiFilter filter);

        [OperationContract]
        NBaseData.DAC.OdThi OdThi_GetRecord(NBaseData.DAC.MsUser loginUser, string odThiID);
        
        [OperationContract]
        NBaseData.DAC.OdThi OdThi_GetRecordByOdMkID(NBaseData.DAC.MsUser loginUser, string odOkID);
        
        [OperationContract]
        NBaseData.DAC.OdThi OdThi_GetRecordByOdJryID(NBaseData.DAC.MsUser loginUser, string odJryID);

        [OperationContract]
        NBaseData.DAC.OdThi OdThi_GetRecordByOdShrID(NBaseData.DAC.MsUser loginUser, string odShrID);

        [OperationContract]
        bool OdThi_Insert();

        [OperationContract]
        bool OdThi_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdThi info);


        [OperationContract]
        List<NBaseData.DAC.OdThi> OdThi_GetRecordsByVesselId(NBaseData.DAC.MsUser loginUser, int vesselId);

        [OperationContract]
        List<OdThi> OdThi_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdThi> OdThi_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdThi> ret;
            ret = NBaseData.DAC.OdThi.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdThi> OdThi_GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdThiFilter filter)
        {
            List<NBaseData.DAC.OdThi> ret;
            ret = NBaseData.DAC.OdThi.GetRecordsByFilter(loginUser, filter);
            return ret;
        }

        public NBaseData.DAC.OdThi OdThi_GetRecord(NBaseData.DAC.MsUser loginUser, string odThiID)
        {
            NBaseData.DAC.OdThi ret;
            ret = NBaseData.DAC.OdThi.GetRecord(loginUser, odThiID);
            return ret;
        }

        public NBaseData.DAC.OdThi OdThi_GetRecordByOdMkID(NBaseData.DAC.MsUser loginUser, string odMkID)
        {
            NBaseData.DAC.OdThi ret;
            ret = NBaseData.DAC.OdThi.GetRecordByOdMkID(loginUser, odMkID);
            return ret;
        }

        public NBaseData.DAC.OdThi OdThi_GetRecordByOdJryID(NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            NBaseData.DAC.OdThi ret;
            ret = NBaseData.DAC.OdThi.GetRecordByOdJryID(loginUser, odJryID);
            return ret;
        }

        public NBaseData.DAC.OdThi OdThi_GetRecordByOdShrID(NBaseData.DAC.MsUser loginUser, string odShrID)
        {
            NBaseData.DAC.OdThi ret;
            ret = NBaseData.DAC.OdThi.GetRecordByOdShrID(loginUser, odShrID);
            return ret;
        }

        public bool OdThi_Insert()
        {
            return true;
        }

        public bool OdThi_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdThi info)
        {
            return info.UpdateRecord(loginUser);
        }


        public List<NBaseData.DAC.OdThi> OdThi_GetRecordsByVesselId(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            return NBaseData.DAC.OdThi.GetRecordsByVesselId(loginUser, vesselId);
        }


        public List<OdThi> OdThi_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id)
        {
            return OdThi.GetRecordsByMsUserID(loginUser, ms_user_id);
        }
    }
}
