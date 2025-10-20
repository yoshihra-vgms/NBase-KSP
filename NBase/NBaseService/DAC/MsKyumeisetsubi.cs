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
        List<MsKyumeisetsubi> MsKyumeisetsubi_GetRecords(MsUser loginUser);
        [OperationContract]
        List<MsKyumeisetsubi> MsKyumeisetsubi_GetRecordsByName(MsUser loginUser,string Name);
        [OperationContract]
        bool MsKyumeisetsubi_InsertRecord(MsUser loginUser, MsKyumeisetsubi msKyumeisetsubi);
        [OperationContract]
        bool MsKyumeisetsubi_UpdateRecord(MsUser loginUser, MsKyumeisetsubi msKyumeisetsubi);
    }

    public partial class Service
    {
        public List<MsKyumeisetsubi> MsKyumeisetsubi_GetRecords(MsUser loginUser)
        {
            return MsKyumeisetsubi.GetRecords(loginUser);
        }
        public List<MsKyumeisetsubi> MsKyumeisetsubi_GetRecordsByName(MsUser loginUser, string Name)
        {
            return MsKyumeisetsubi.GetRecordsByName(loginUser, Name);
        }
        public bool MsKyumeisetsubi_InsertRecord(MsUser loginUser, MsKyumeisetsubi msKyumeisetsubi)
        {
            return msKyumeisetsubi.InsertRecord(loginUser);
        }
        public bool MsKyumeisetsubi_UpdateRecord(MsUser loginUser, MsKyumeisetsubi msKyumeisetsubi)
        {
            return msKyumeisetsubi.UpdateRecord(loginUser);
        }
    }
}
