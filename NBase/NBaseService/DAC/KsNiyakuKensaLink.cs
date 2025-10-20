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
        bool KsNiyakuKensaLink_InsertRecord(MsUser loginUser, KsNiyakuKensaLink data);

        [OperationContract]
        bool KsNiyakuKensaLink_UpdateRecord(MsUser loginUser, KsNiyakuKensaLink data);

        [OperationContract]
        bool KsNiyakuKensaLink_DeleteRecordBy検査_荷役(MsUser loginUser, string ks_niyaku_id, string ms_kensa_id);

        [OperationContract]
        List<KsNiyakuKensaLink> KsNiyakuKensaLink_GetRecordsByMsKensaID(MsUser loginUser, string ms_kensa_id);
    }

    public partial class Service
    {
        public bool KsNiyakuKensaLink_InsertRecord(MsUser loginUser, KsNiyakuKensaLink data)
        {
            return data.InsertRecord(loginUser);
        }

        public bool KsNiyakuKensaLink_UpdateRecord(MsUser loginUser, KsNiyakuKensaLink data)
        {
            return data.UpdateRecord(loginUser);
        }

        public bool KsNiyakuKensaLink_DeleteRecordBy検査_荷役(MsUser loginUser, string ks_niyaku_id, string ms_kensa_id)
        {
            return KsNiyakuKensaLink.DeleteRecordBy検査_荷役(loginUser, ks_niyaku_id, ms_kensa_id);
        }

        public List<KsNiyakuKensaLink> KsNiyakuKensaLink_GetRecordsByMsKensaID(MsUser loginUser, string ms_kensa_id)
        {
            return KsNiyakuKensaLink.GetRecordsByMsKensaID(loginUser, ms_kensa_id);
        }
    }
}
