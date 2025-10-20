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
        List<KsNiyaku> KsNiyaku_GetRecordsBy船ID(MsUser loginUser, int ms_vessel_id);

        [OperationContract]
        List<KsNiyaku> KsNiyaku_GetRecordsBy船ID_荷役ID(MsUser loginUser, int ms_vessel_id, string ms_niyaku_id);

        [OperationContract]
        List<KsNiyaku> KsNiyaku_GetRecordsBy船ID_予定データ(MsUser loginUser, int ms_vessel_id);

        [OperationContract]
        List<KsNiyaku> KsNiyaku_GetRecordsBy船ID_予定データ_荷役ID(MsUser loginUser, int ms_vessel_id, string ms_niyaku_id);

        [OperationContract]
        List<KsNiyaku> KsNiyaku_GetRecordsFor船_指定検査_予定(MsUser loginUser, string ms_kensa_id, int ms_vessel_id);

        [OperationContract]
        KsNiyaku KsNiyaku_GetRecord(MsUser loginUser, string ks_niyaku_id);

        [OperationContract]
        bool KsNiyaku_InsertRecord(MsUser loginUser, KsNiyaku data);

        [OperationContract]
        bool KsNiyaku_UpdateRecord(MsUser loginUser, KsNiyaku data);

        [OperationContract]
        List<KsNiyaku> KsNiyaku_GetRecordsByMsNiyakuID(MsUser loginUser, string ms_niyaku_id);
    }

    public partial class Service
    {

        public List<KsNiyaku> KsNiyaku_GetRecordsBy船ID(MsUser loginUser, int ms_vessel_id)
        {
            return KsNiyaku.GetRecordsBy船ID(loginUser, ms_vessel_id);
        }

        public List<KsNiyaku> KsNiyaku_GetRecordsBy船ID_荷役ID(MsUser loginUser, int ms_vessel_id, string ms_niyaku_id)
        {
            return KsNiyaku.GetRecordsBy船ID_荷役ID(loginUser, ms_vessel_id, ms_niyaku_id);
        }

        
        public List<KsNiyaku> KsNiyaku_GetRecordsBy船ID_予定データ(MsUser loginUser, int ms_vessel_id)
        {
            return KsNiyaku.GetRecordsBy船ID_予定データ(loginUser, ms_vessel_id);
        }

        public List<KsNiyaku> KsNiyaku_GetRecordsBy船ID_予定データ_荷役ID(MsUser loginUser, int ms_vessel_id, string ms_niyaku_id)
        {
            return KsNiyaku.GetRecordsBy船ID_予定データ_荷役ID(loginUser, ms_vessel_id, ms_niyaku_id);
        }

        public List<KsNiyaku> KsNiyaku_GetRecordsFor船_指定検査_予定(MsUser loginUser, string ms_kensa_id, int ms_vessel_id)
        {
            return KsNiyaku.GetRecordsFor船_指定検査_予定(loginUser, ms_kensa_id, ms_vessel_id);
        }
        
        public KsNiyaku KsNiyaku_GetRecord(MsUser loginUser, string ks_niyaku_id)
        {
            return KsNiyaku.GetRecord(loginUser, ks_niyaku_id);
        }


        
        public bool KsNiyaku_InsertRecord(MsUser loginUser, KsNiyaku data)
        {
            return data.InsertRecord(loginUser);
        }

        public bool KsNiyaku_UpdateRecord(MsUser loginUser, KsNiyaku data)
        {
            return data.UpdateRecord(loginUser);
        }


        public List<KsNiyaku> KsNiyaku_GetRecordsByMsNiyakuID(MsUser loginUser, string ms_niyaku_id)
        {
            return KsNiyaku.GetRecordsByMsNiyakuID(loginUser, ms_niyaku_id);
        }
    }
}
