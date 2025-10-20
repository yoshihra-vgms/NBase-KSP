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
        List<MsBashoKyori> MsBashoKyori_GetRecordsByKyori1Kyori2(MsUser loginUser, string BashoNo1, string BashoNo2);

        [OperationContract]
        MsBashoKyori MsBashoKyori_GetRecord(MsUser loginUser, string BashoNo1, string BashoNo2);

        [OperationContract]
        bool MsBashoKyori_InsertRecord(MsUser loginUser, MsBashoKyori msBashoKyori);

        [OperationContract]
        bool MsBashoKyori_UpdateRecord(MsUser loginUser, MsBashoKyori msBashoKyori);

        [OperationContract]
        List<MsBashoKyori> MsBashoKyori_GetRecordsByMsBashoID(MsUser loginUser, string ms_basho_id);

    }
    public partial class Service
    {
        public List<MsBashoKyori> MsBashoKyori_GetRecordsByKyori1Kyori2(MsUser loginUser, string BashoNo1, string BashoNo2)
        {
            return MsBashoKyori.GetRecordsByKyori1Kyori2(loginUser, BashoNo1, BashoNo2);
        }

        public MsBashoKyori MsBashoKyori_GetRecord(MsUser loginUser, string BashoNo1, string BashoNo2)
        {
            return MsBashoKyori.GetRecord(loginUser, BashoNo1, BashoNo2);
        }

        public bool MsBashoKyori_InsertRecord(MsUser loginUser, MsBashoKyori msBashoKyori)
        {
            return msBashoKyori.InsertRecord(loginUser);
        }

        public bool MsBashoKyori_UpdateRecord(MsUser loginUser, MsBashoKyori msBashoKyori)
        {
            return msBashoKyori.UpdateRecord(loginUser);
        }


        public List<MsBashoKyori> MsBashoKyori_GetRecordsByMsBashoID(MsUser loginUser, string ms_basho_id)
        {
            return MsBashoKyori.GetRecordsByMsBashoID(loginUser, ms_basho_id);
        }
    }
}
