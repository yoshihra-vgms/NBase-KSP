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
        List<NBaseData.DAC.MsKanidouseiInfoShubetu> MsKanidouseiInfoShubetu_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<NBaseData.DAC.MsKanidouseiInfoShubetu> MsKanidouseiInfoShubetu_GetRecordsByDouseiEnabled(NBaseData.DAC.MsUser loginUser);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsKanidouseiInfoShubetu> MsKanidouseiInfoShubetu_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsKanidouseiInfoShubetu> ret;
            ret = NBaseData.DAC.MsKanidouseiInfoShubetu.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.MsKanidouseiInfoShubetu> MsKanidouseiInfoShubetu_GetRecordsByDouseiEnabled(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsKanidouseiInfoShubetu> ret;
            ret = NBaseData.DAC.MsKanidouseiInfoShubetu.GetRecordsByDouseiEnabled(loginUser);
            return ret;
        }
    }
}
