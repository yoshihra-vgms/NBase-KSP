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
using System.ServiceModel;
using System.Collections.Generic;
using NBaseData.DAC;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<MsBashoKubun> MsBashoKubun_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<MsBashoKubun> MsBashoKubun_GetRecordsByName(NBaseData.DAC.MsUser loginUser, string bashoKubunName);

        [OperationContract]
        bool MsBashoKubun_InsertRecord(NBaseData.DAC.MsUser loginUser, MsBashoKubun msBashoKubun);

        [OperationContract]
        bool MsBashoKubun_UpdateRecord(NBaseData.DAC.MsUser loginUser, MsBashoKubun msBashoKubun);
    }

    public partial class Service
    {
        public List<MsBashoKubun> MsBashoKubun_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return MsBashoKubun.GetRecords(loginUser);
        }

        public List<MsBashoKubun> MsBashoKubun_GetRecordsByName(NBaseData.DAC.MsUser loginUser, string bashoKubunName)
        {
            return MsBashoKubun.GetRecordsByName(loginUser, bashoKubunName);
        }

        public bool MsBashoKubun_InsertRecord(NBaseData.DAC.MsUser loginUser, MsBashoKubun msBashoKubun)
        {
            return msBashoKubun.InsertRecord(loginUser);
        }
        public bool MsBashoKubun_UpdateRecord(NBaseData.DAC.MsUser loginUser, MsBashoKubun msBashoKubun)
        {
            return msBashoKubun.UpdateRecord(loginUser);
        }
    }
}
