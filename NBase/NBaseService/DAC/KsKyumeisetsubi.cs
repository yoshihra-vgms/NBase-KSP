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
        List<KsKyumeisetsubi> KsKyumeisetsubi_GetRecordsBy船ID(MsUser loginUser, int ms_vessel_id);

        [OperationContract]
        List<KsKyumeisetsubi> KsKyumeisetsubi_GetRecordsBy船ID_救命設備ID(MsUser loginUser, int ms_vessel_id, string ms_kyumei_id);
        
        [OperationContract]
        List<KsKyumeisetsubi> KsKyumeisetsubi_GetRecordBy船ID予定データ(MsUser loginUser, int ms_vessel_id);

        [OperationContract]
        List<KsKyumeisetsubi> KsKyumeisetsubi_GetRecordBy船ID予定データ_救命設備ID(MsUser loginUser, int ms_vessel_id, string ms_kyumei_id);

        [OperationContract]
        KsKyumeisetsubi KsKyumeisetsubi_GetRecord(MsUser loginUser, string ks_kyumeisetsubi_id);
        
        [OperationContract]
        bool KsKyumeisetsubi_InsertRecord(MsUser loginUser, KsKyumeisetsubi data);
        
        [OperationContract]
        bool KsKyumeisetsubi_UpdateRecord(MsUser loginUser, KsKyumeisetsubi data);

        [OperationContract]
        List<KsKyumeisetsubi> KsKyumeisetsubi_GetRecordsByMsKyumeisetsubiID(MsUser loginUser, string ms_kyu_id);
    }

    public partial class Service
    {
        public List<KsKyumeisetsubi> KsKyumeisetsubi_GetRecordsBy船ID(MsUser loginUser, int ms_vessel_id)
        {
            return KsKyumeisetsubi.GetRecordsBy船ID(loginUser, ms_vessel_id);
        }

        public List<KsKyumeisetsubi> KsKyumeisetsubi_GetRecordsBy船ID_救命設備ID(MsUser loginUser, int ms_vessel_id, string ms_kyumei_id)
        {
            return KsKyumeisetsubi.GetRecordsBy船ID_救命設備ID(loginUser, ms_vessel_id, ms_kyumei_id);
        }

        public List<KsKyumeisetsubi> KsKyumeisetsubi_GetRecordBy船ID予定データ(MsUser loginUser, int ms_vessel_id)
        {
            return KsKyumeisetsubi.GetRecordBy船ID予定データ(loginUser, ms_vessel_id);
        }

        public List<KsKyumeisetsubi> KsKyumeisetsubi_GetRecordBy船ID予定データ_救命設備ID(MsUser loginUser, int ms_vessel_id, string ms_kyumei_id)
        {
            return KsKyumeisetsubi.GetRecordBy船ID予定データ_救命設備ID(loginUser, ms_vessel_id, ms_kyumei_id);
        }
        
        public KsKyumeisetsubi KsKyumeisetsubi_GetRecord(MsUser loginUser, string ks_kyumeisetsubi_id)
        {
            return KsKyumeisetsubi.GetRecord(loginUser, ks_kyumeisetsubi_id);
        }

        public bool KsKyumeisetsubi_InsertRecord(MsUser loginUser, KsKyumeisetsubi data)
        {
            return data.InsertRecord(loginUser);
        }

        public bool KsKyumeisetsubi_UpdateRecord(MsUser loginUser, KsKyumeisetsubi data)
        {
            return data.UpdateRecord(loginUser);
        }

        public List<KsKyumeisetsubi> KsKyumeisetsubi_GetRecordsByMsKyumeisetsubiID(MsUser loginUser, string ms_kyu_id)
        {
            return KsKyumeisetsubi.GetRecordsByMsKyumeisetsubiID(loginUser, ms_kyu_id);
        }
    }
}
