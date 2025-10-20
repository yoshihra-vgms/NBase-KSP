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
        List<NBaseData.DAC.OdJry> OdJry_GetRecords(NBaseData.DAC.MsUser loginUser);
        
        [OperationContract]
        List<NBaseData.DAC.OdJry> OdJry_GetRecordsByOdMkId(NBaseData.DAC.MsUser loginUser, string odMkId);

        [OperationContract]
        List<NBaseData.DAC.OdJry> OdJry_GetRecordsByOdThiFilter(NBaseData.DAC.MsUser loginUser, int status, NBaseData.DS.OdThiFilter filter);

        [OperationContract]
        List<NBaseData.DAC.OdJry> OdJry_GetRecordsByOdJryFilter(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdJryFilter filter);

        [OperationContract]
        NBaseData.DAC.OdJry OdJry_GetRecord(NBaseData.DAC.MsUser loginUser, string odJryID);

        [OperationContract]
        bool OdJry_Insert();

        [OperationContract]
        bool OdJry_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJry info);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.OdJry> OdJry_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.OdJry> ret;
            ret = NBaseData.DAC.OdJry.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.OdJry> OdJry_GetRecordsByOdMkId(NBaseData.DAC.MsUser loginUser, string odMkId)
        {
            List<NBaseData.DAC.OdJry> ret;
            ret = NBaseData.DAC.OdJry.GetRecordsByOdMkId(loginUser, odMkId);
            return ret;
        }

        public List<NBaseData.DAC.OdJry> OdJry_GetRecordsByOdThiFilter(NBaseData.DAC.MsUser loginUser, int status, NBaseData.DS.OdThiFilter filter)
        {
            List<NBaseData.DAC.OdJry> ret;
            ret = NBaseData.DAC.OdJry.GetRecordsByFilter(loginUser, status, filter);
            return ret;
        }

        public List<NBaseData.DAC.OdJry> OdJry_GetRecordsByOdJryFilter(NBaseData.DAC.MsUser loginUser, NBaseData.DS.OdJryFilter filter)
        {
            List<NBaseData.DAC.OdJry> ret;
            ret = NBaseData.DAC.OdJry.GetRecordsByFilter(loginUser, filter);
            return ret;
        }

        public NBaseData.DAC.OdJry OdJry_GetRecord(NBaseData.DAC.MsUser loginUser, string odJryID)
        {
            NBaseData.DAC.OdJry ret;
            ret = NBaseData.DAC.OdJry.GetRecord(loginUser, odJryID);
            return ret;
        }

        public bool OdJry_Insert()
        {
            return true;
        }

        public bool OdJry_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdJry info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
