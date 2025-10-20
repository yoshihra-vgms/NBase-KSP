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
        bool KsShoushoKensenLink_DeleteRecordBy検船マスタID証書ID(MsUser loginUser, string ms_kensen_id, string ks_shousho_id);

        [OperationContract]
        bool KsShoushoKensenLink_InsertRecord(MsUser loginUser, KsShoushoKensenLink data);

        [OperationContract]
        List<KsShoushoKensenLink> KsShoushoKensenLink_GetRecordsByMsKensenID(MsUser loginUser, string ms_kensen_ID);
        
    }

    public partial class Service
    {
        public bool KsShoushoKensenLink_DeleteRecordBy検船マスタID証書ID(MsUser loginUser, string ms_kensen_id, string ks_shousho_id)
        {
            return KsShoushoKensenLink.DeleteRecordBy検船マスタID証書ID(loginUser, ms_kensen_id, ks_shousho_id);
        }

        public bool KsShoushoKensenLink_InsertRecord(MsUser loginUser,KsShoushoKensenLink data)
        {
            return data.InsertRecord(loginUser);
        }



        public List<KsShoushoKensenLink> KsShoushoKensenLink_GetRecordsByMsKensenID(MsUser loginUser, string ms_kensen_ID)
        {
            return KsShoushoKensenLink.GetRecordsByMsKensenID(loginUser, ms_kensen_ID);
        }
    }
}
