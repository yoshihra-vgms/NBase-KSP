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
        List<MsKensen> MsKensen_GetRecords(MsUser loginUser);

        [OperationContract]
        List<MsKensen> MsKensen_GetRecordsBy証書リンク(MsUser loginUser, string ks_shousho_id);

        [OperationContract]
        List<MsKensen> MsKensen_GetRecordsByName(MsUser loginUser, string Name);

        [OperationContract]
        bool MsKensen_InsertRecord(MsUser loginUser, MsKensen msKensen);

        [OperationContract]
        bool MsKensen_UpdateRecord(MsUser loginUser, MsKensen msKensen);
    }

    public partial class Service
    {
        public List<MsKensen> MsKensen_GetRecords(MsUser loginUser)
        {
            return MsKensen.GetRecords(loginUser);
        }

        public List<MsKensen> MsKensen_GetRecordsBy証書リンク(MsUser loginUser, string ks_shousho_id)
        {
            return MsKensen.GetRecordsBy証書リンク(loginUser, ks_shousho_id);
        }

        public List<MsKensen> MsKensen_GetRecordsByName(MsUser loginUser, string Name)
        {
            return MsKensen.GetRecordsByName(loginUser,Name);
        }

        public bool MsKensen_InsertRecord(MsUser loginUser, MsKensen msKensen)
        {
            return msKensen.InsertRecord(loginUser);
        }

        public bool MsKensen_UpdateRecord(MsUser loginUser, MsKensen msKensen)
        {
            return msKensen.UpdateRecord(loginUser);
        }
    }
}
