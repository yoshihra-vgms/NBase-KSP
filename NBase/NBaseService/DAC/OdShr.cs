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
        List<NBaseData.DAC.OdShr> OdShr_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.OdShr OdShr_GetRecord(NBaseData.DAC.MsUser loginUser, string odShrID);

        [OperationContract]
        List<NBaseData.DAC.OdShr> OdShr_GetRecordByOdJryID(NBaseData.DAC.MsUser loginUser, string odJryID);

        [OperationContract]
        List<NBaseData.DAC.OdShr> OdShr_GetRecordsByOdThiFilter(NBaseData.DAC.MsUser loginUser, int status, NBaseData.DS.OdThiFilter filter);
        
        [OperationContract]
        List<NBaseData.DAC.OdShr> OdShr_GetRecords落成済み未払い(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdJryFilter filter);

        [OperationContract]
        List<NBaseData.DAC.OdShr> OdShr_GetRecords未払い(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdJryFilter filter);

        [OperationContract]
        List<NBaseData.DAC.OdShr> OdShr_GetRecordByGassanItem(NBaseData.DAC.MsUser loginUser, string odJryID);

        [OperationContract]
        bool OdShr_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShr info);

        [OperationContract]
        bool OdShr_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShr info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdShr> OdShr_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdShr> ret;
            ret = NBaseData.DAC.OdShr.GetRecords(loginUser);
            return ret;
        }

        public NBaseData.DAC.OdShr OdShr_GetRecord(NBaseData.DAC.MsUser loginUser, string odShrID)
        {
            NBaseData.DAC.OdShr ret;
            ret = NBaseData.DAC.OdShr.GetRecord(loginUser, odShrID);
            return ret;
        }

        public List<NBaseData.DAC.OdShr> OdShr_GetRecordByOdJryID(NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            List<NBaseData.DAC.OdShr> ret;
            ret = NBaseData.DAC.OdShr.GetRecordByOdJryID(loginUser, odJryID);
            return ret;
        }

        public List<NBaseData.DAC.OdShr> OdShr_GetRecordsByOdThiFilter(NBaseData.DAC.MsUser loginUser, int status, NBaseData.DS.OdThiFilter filter)
        {
            List<NBaseData.DAC.OdShr> ret;
            ret = NBaseData.DAC.OdShr.GetRecordsByFilter(loginUser, status, filter);
            return ret;
        }

        public List<NBaseData.DAC.OdShr> OdShr_GetRecords落成済み未払い(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdJryFilter filter)
        {
            List<NBaseData.DAC.OdShr> ret;
            ret = NBaseData.DAC.OdShr.GetRecords落成済み未払い(loginUser, filter);
            return ret;
        }

        public List<NBaseData.DAC.OdShr> OdShr_GetRecords未払い(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdJryFilter filter)
        {
            List<NBaseData.DAC.OdShr> ret;
            ret = NBaseData.DAC.OdShr.GetRecords未払い(loginUser, filter);
            return ret;
        }

        public List<NBaseData.DAC.OdShr> OdShr_GetRecordByGassanItem(NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            List<NBaseData.DAC.OdShr> ret;
            ret = NBaseData.DAC.OdShr.GetRecordByGassanItem(loginUser, odJryID);
            return ret;
        }

        public bool OdShr_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShr info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool OdShr_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShr info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
