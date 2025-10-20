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
        List<KsKensen> KsKensen_GetRecordBy船ID(MsUser loginUser, int ms_vessel_id);

        [OperationContract]
        List<KsKensen> KsKensen_GetRecordBy船ID予定データ(MsUser loginUser, int ms_vessel_id);

        [OperationContract]
        KsKensen KsKensen_GetRecord(MsUser loginUser, string ks_kensen_id);

        [OperationContract]
        bool KsKensen_InsertRecord(MsUser loginUser, KsKensen data);

        [OperationContract]
        bool KsKensen_UpdateRecord(MsUser loginUser, KsKensen data);

        [OperationContract]
        List<KsKensen> KsKensen_GetRecordsByMsKensenID(MsUser loginUser, string ms_kensen_id);
    }

    public partial class Service
    {
        public List<KsKensen> KsKensen_GetRecordBy船ID(MsUser loginUser, int ms_vessel_id)
        {
            return KsKensen.GetRecordBy船ID(loginUser, ms_vessel_id);
        }

        public List<KsKensen> KsKensen_GetRecordBy船ID予定データ(MsUser loginUser, int ms_vessel_id)
        {
            return KsKensen.GetRecordBy船ID予定データ(loginUser, ms_vessel_id);
        }

        public KsKensen KsKensen_GetRecord(MsUser loginUser, string ks_kensen_id)
        {
            return KsKensen.GetRecord(loginUser, ks_kensen_id);
        }

        public bool KsKensen_InsertRecord(MsUser loginUser, KsKensen data)
        {
            return data.InsertRecord(loginUser);
        }

        public bool KsKensen_UpdateRecord(MsUser loginUser, KsKensen data)
        {
            return data.UpdateRecord(loginUser);
        }


        public List<KsKensen> KsKensen_GetRecordsByMsKensenID(MsUser loginUser, string ms_kensen_id)
        {
            return KsKensen.GetRecordsByMsKensenID(loginUser, ms_kensen_id);
        }
    }
}
