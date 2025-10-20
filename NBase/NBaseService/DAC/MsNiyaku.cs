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
        List<MsNiyaku> MsNiyaku_GetRecords(MsUser loginUser);
        [OperationContract]
        List<MsNiyaku> MsNiyaku_GetRecordsByName(MsUser loginUser,string Name);
        [OperationContract]
        bool MsNiyaku_InsertRecord(MsUser loginUser, MsNiyaku niyaku);
        [OperationContract]
        bool MsNiyaku_UpdateRecord(MsUser loginUser, MsNiyaku niyaku);
    }

    public partial class Service
    {
        public List<MsNiyaku> MsNiyaku_GetRecords(MsUser loginUser)
        {
            return MsNiyaku.GetRecords(loginUser);
        }
        public List<MsNiyaku> MsNiyaku_GetRecordsByName(MsUser loginUser, string Name)
        {
            return MsNiyaku.GetRecordsByName(loginUser,Name);
        }
        public bool MsNiyaku_InsertRecord(MsUser loginUser, MsNiyaku niyaku)
        {
            return niyaku.InsertRecord(loginUser);
        }
        public bool MsNiyaku_UpdateRecord(MsUser loginUser, MsNiyaku niyaku)
        {
            return niyaku.UpdateRecord(loginUser);
        }
    }
}
