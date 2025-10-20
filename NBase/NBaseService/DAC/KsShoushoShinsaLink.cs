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
        bool KsShoushoShinsaLink_DeleteRecordBy審査マスタID証書ID(MsUser loginUser, string ms_shinsa_id, string ks_shousho_id);

        [OperationContract]
        bool KsShoushoShinsaLink_InsertRecord(MsUser loginUser, KsShoushoShinsaLink data);
    }

    public partial class Service
    {
        public bool KsShoushoShinsaLink_DeleteRecordBy審査マスタID証書ID(MsUser loginUser, string ms_shinsa_id, string ks_shousho_id)
        {
            return KsShoushoShinsaLink.DeleteRecordBy審査マスタID証書ID(loginUser, ms_shinsa_id, ks_shousho_id);
        }

        public bool KsShoushoShinsaLink_InsertRecord(MsUser loginUser, KsShoushoShinsaLink data)
        {
            return data.InsertRecord(loginUser);
        }
    }
}
