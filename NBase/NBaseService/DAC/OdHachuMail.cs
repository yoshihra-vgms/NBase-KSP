using System;
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
        OdHachuMail OdHachuMail_GetRecord(NBaseData.DAC.MsUser loginUser, string odMkId, string tantousha, string mailAdress, string subject);

        [OperationContract]
        List<OdHachuMail> OdHachuMail_GetRecords(NBaseData.DAC.MsUser loginUser, string odMkId);

        [OperationContract]
        bool OdHachuMail_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdHachuMail info);

        [OperationContract]
        bool OdHachuMail_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdHachuMail info);
    }


    public partial class Service
    {
        public OdHachuMail OdHachuMail_GetRecord(NBaseData.DAC.MsUser loginUser, string odMkId, string tantousha, string mailAdress, string subject)
        {
            OdHachuMail ret = OdHachuMail.GetRecord(loginUser, odMkId, tantousha, mailAdress, subject);

            return ret;
        }

        public List<OdHachuMail> OdHachuMail_GetRecords(NBaseData.DAC.MsUser loginUser, string odMkId)
        {
            List<OdHachuMail> ret = OdHachuMail.GetRecords(loginUser, odMkId);

            return ret;
        }

        public bool OdHachuMail_Insert(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdHachuMail info)
        {
            return info.InsertRecord(loginUser);
        }

        public bool OdHachuMail_Update(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdHachuMail info)
        {
            return info.UpdateRecord(loginUser);
        }
    }
}
