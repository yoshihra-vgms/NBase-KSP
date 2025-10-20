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
        List<KsShinsa> KsShinsa_GetRecordsBy船ID(MsUser loginUser, int ms_vessel_id);

        [OperationContract]
        List<KsShinsa> KsShinsa_GetRecordsBy船ID_予定データ(MsUser loginUser, int ms_vessel_id);

        [OperationContract]
        KsShinsa KsShinsa_GetRecord(MsUser loginUser, string ks_shinsa_id);

        [OperationContract]
        bool KsShinsa_InsertRecord(MsUser loginUser, KsShinsa data);

        [OperationContract]
        bool KsShinsa_UpdateRecord(MsUser loginUser, KsShinsa data);

        [OperationContract]
        List<KsShoushoShinsaLink> KsShoushoShinsaLink_GetRecordsByMsShinsaID(MsUser loginUser, string ms_shisa_id);
    }

    public partial class Service
    {

        public List<KsShinsa> KsShinsa_GetRecordsBy船ID(MsUser loginUser, int ms_vessel_id)
        {
            return KsShinsa.GetRecordsBy船ID(loginUser, ms_vessel_id);
        }

        public List<KsShinsa> KsShinsa_GetRecordsBy船ID_予定データ(MsUser loginUser, int ms_vessel_id)
        {
            return KsShinsa.GetRecordsBy船ID_予定データ(loginUser, ms_vessel_id);
        }

        public KsShinsa KsShinsa_GetRecord(MsUser loginUser, string ks_shinsa_id)
        {
            return KsShinsa.GetRecord(loginUser, ks_shinsa_id);
        }
        public bool KsShinsa_InsertRecord(MsUser loginUser, KsShinsa data)
        {
            return data.InsertRecord(loginUser);
        }
        public bool KsShinsa_UpdateRecord(MsUser loginUser, KsShinsa data)
        {
            return data.UpdateRecord(loginUser);
        }


        public List<KsShoushoShinsaLink> KsShoushoShinsaLink_GetRecordsByMsShinsaID(MsUser loginUser, string ms_shisa_id)
        {
            return KsShoushoShinsaLink.GetRecordsByMsShinsaID(loginUser, ms_shisa_id);
        }

    }
}
