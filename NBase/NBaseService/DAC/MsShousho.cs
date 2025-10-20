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
        List<MsShousho> MsShousho_GetRecords(MsUser loginUser);

        [OperationContract]
        List<MsShousho> MsShousho_GetRecordsByName(MsUser loginUser,string Name);

        [OperationContract]
        bool MsShousho_InsertRecord(MsUser loginUser, MsShousho msShousho);

        [OperationContract]
        bool MsShousho_UpdateRecord(MsUser loginUser, MsShousho msShousho);

    }

    public partial class Service
    {
        public List<MsShousho> MsShousho_GetRecords(MsUser loginUser)
        {
            return MsShousho.GetRecords(loginUser);
        }

        public List<MsShousho> MsShousho_GetRecordsByName(MsUser loginUser, string Name)
        {
            return MsShousho.GetRecordsByName(loginUser, Name);
        }

        public bool MsShousho_InsertRecord(MsUser loginUser, MsShousho msShousho)
        {
            return msShousho.InsertRecord(loginUser);
        }

        public bool MsShousho_UpdateRecord(MsUser loginUser, MsShousho msShousho)
        {
            return msShousho.UpdateRecord(loginUser);
        }
    }
}
