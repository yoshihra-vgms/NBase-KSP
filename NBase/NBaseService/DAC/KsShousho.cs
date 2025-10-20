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
        List<KsShousho> KsShousho_GetRecordsBy船ID(MsUser loginUser, int msvessel_id);

        [OperationContract]
        List<KsShousho> KsShousho_GetRecordsBy船ID_予定データ(MsUser loginUser, int msvessel_id);

        [OperationContract]
        List<KsShousho> KsShousho_GetRecordsFor船_指定検査_予定(MsUser loginUser, string ms_kensa_id, int ms_vessel_id);

        [OperationContract]
        KsShousho KsShousho_GetRecord(MsUser loginUser, string ks_shousho_id);

        [OperationContract]
        bool KsShousho_InsertRecord(MsUser loginUser, KsShousho data);

        [OperationContract]
        bool KsShousho_UpdateRecord(MsUser loginUser, KsShousho data);

        [OperationContract]
        List<KsShousho> KsShousho_GetRecordsFor船_指定検船_予定(MsUser loginUser, string ms_kensen_id, int ms_vessel_id);

        [OperationContract]
        List<KsShousho> KsShousho_GetRecordsFor船_指定審査_予定(MsUser loginUser, string ms_shinsa_id, int ms_vessel_id);

        [OperationContract]
        List<KsShousho> KsShousho_GetRecordsByMsShoushoID(MsUser loginUser, string ms_shousho_id);
    }

    public partial class Service
    {        

        public List<KsShousho> KsShousho_GetRecordsBy船ID(MsUser loginUser, int msvessel_id)
        {
            List<KsShousho> datalist = KsShousho.GetRecordsBy船ID(loginUser, msvessel_id);

            return datalist;
        }


        public List<KsShousho> KsShousho_GetRecordsBy船ID_予定データ(MsUser loginUser, int msvessel_id)
        {
            List<KsShousho> datalist = KsShousho.GetRecordsBy船ID_予定データ(loginUser, msvessel_id);

            return datalist;
        }

        public List<KsShousho> KsShousho_GetRecordsFor船_指定検査_予定(MsUser loginUser, string ms_kensa_id, int ms_vessel_id)
        {
            return KsShousho.GetRecordsFor船_指定検査_予定(loginUser, ms_kensa_id, ms_vessel_id);
        }


        public KsShousho KsShousho_GetRecord(MsUser loginUser, string ks_shousho_id)
        {
            KsShousho sho = KsShousho.GetRecord(loginUser, ks_shousho_id);

            return sho;
        }


        public bool KsShousho_InsertRecord(MsUser loginUser, KsShousho data)
        {
            return data.InsertRecord(loginUser);
        }

        public bool KsShousho_UpdateRecord(MsUser loginUser, KsShousho data)
        {
            return data.UpdateRecord(loginUser);
        }

        public List<KsShousho> KsShousho_GetRecordsFor船_指定検船_予定(MsUser loginUser, string ms_kensen_id, int ms_vessel_id)
        {
            return KsShousho.GetRecordsFor船_指定検船_予定(loginUser, ms_kensen_id, ms_vessel_id);
        }

        public List<KsShousho> KsShousho_GetRecordsFor船_指定審査_予定(MsUser loginUser, string ms_shinsa_id, int ms_vessel_id)
        {
            return KsShousho.GetRecordsFor船_指定審査_予定(loginUser, ms_shinsa_id, ms_vessel_id);
        }


        public List<KsShousho> KsShousho_GetRecordsByMsShoushoID(MsUser loginUser, string ms_shousho_id)
        {
            return KsShousho.GetRecordsByMsShoushoID(loginUser, ms_shousho_id);
        }
    }
}
