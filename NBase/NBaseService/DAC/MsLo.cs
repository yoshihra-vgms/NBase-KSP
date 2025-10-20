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
        List<NBaseData.DAC.MsLo> MsLo_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.MsLo MsLo_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string msLoId);

        [OperationContract]
        bool MsLo_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsLo msLo);

        [OperationContract]
        bool MsLo_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsLo msLo);

        [OperationContract]
        bool MsLo_DeleteRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsLo msLo);

        [OperationContract]
        List<NBaseData.DAC.MsLo> MsLo_SearchRecords(NBaseData.DAC.MsUser loginUser, string msLoId, string loName);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsLo> MsLo_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsLo> ret;
            ret = NBaseData.DAC.MsLo.GetRecords(loginUser);
            return ret;
        }

        public NBaseData.DAC.MsLo MsLo_GetRecordsByMsLoID(NBaseData.DAC.MsUser loginUser, string msLoId)
        {
            NBaseData.DAC.MsLo ret;
            ret = NBaseData.DAC.MsLo.GetRecord(loginUser, msLoId);
            return ret;
        }

        public bool MsLo_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsLo msLo)
        {
            msLo.InsertRecord(loginUser);
            return true;
        }

        public bool MsLo_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsLo msLo)
        {
            msLo.UpdateRecord(loginUser);
            return true;
        }

        public bool MsLo_DeleteRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsLo msLo)
        {
            msLo.DeleteRecord(loginUser);
            return true;
        }

        public List<NBaseData.DAC.MsLo> MsLo_SearchRecords(NBaseData.DAC.MsUser loginUser, string msLoId, string loName)
        {
            List<NBaseData.DAC.MsLo> ret;
            ret = NBaseData.DAC.MsLo.SearchRecords(loginUser, msLoId, loName);
            return ret;
        }
    }
}
