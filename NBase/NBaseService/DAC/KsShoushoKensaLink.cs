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
        bool KsShoushoKensaLink_DeleteRecordBy検査マスタID証書ID(MsUser loginUser, string ms_kensa_id, string ks_shousho_id);

        [OperationContract]
        bool KsShoushoKensaLink_InsertRecord(MsUser loginUser, KsShoushoKensaLink data);

        [OperationContract]
        List<KsShoushoKensaLink> KsShoushoKensaLink_GetRecordsByMsKensaID(MsUser loginUser, string ms_kensa_id);
    }

    public partial class Service
    {
        public bool KsShoushoKensaLink_InsertRecord(MsUser loginUser, KsShoushoKensaLink data)
        {
            return data.InsertRecord(loginUser);
        }

        public bool KsShoushoKensaLink_DeleteRecordBy検査マスタID証書ID(MsUser loginUser, string ms_kensa_id, string ks_shousho_id)
        {
            return KsShoushoKensaLink.DeleteRecordBy検査マスタID証書ID(loginUser, ms_kensa_id, ks_shousho_id);
        }


        public List<KsShoushoKensaLink> KsShoushoKensaLink_GetRecordsByMsKensaID(MsUser loginUser, string ms_kensa_id)
        {
            return KsShoushoKensaLink.GetRecordsByMsKensaID(loginUser, ms_kensa_id);
        }
    }
}
