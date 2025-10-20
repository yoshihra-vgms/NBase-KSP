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
        List<MsShinsa> MsShinsa_GetRecords(MsUser loginUser);

        [OperationContract]
        List<MsShinsa> MsShinsa_GetRecordsBy証書リンク(MsUser loginUser, string ks_shousho_id);

        [OperationContract]
        List<MsShinsa> MsShinsa_GetRecordsByName(MsUser loginUser, string Name);

        [OperationContract]
        bool MsShinsa_InsertRecord(MsUser loginUser, MsShinsa msShinsa);

        [OperationContract]
        bool MsShinsa_UpdateRecord(MsUser loginUser, MsShinsa msShinsa);

        [OperationContract]
        List<KsShinsa> KsShinsa_GetRecordsByMsShinsaID(MsUser loginUser, string ms_shinsa_id);
    }

    public partial class Service
    {
        public List<MsShinsa> MsShinsa_GetRecords(MsUser loginUser)
        {
            return MsShinsa.GetRecords(loginUser);
        }

        public List<MsShinsa> MsShinsa_GetRecordsBy証書リンク(MsUser loginUser, string ks_shousho_id)
        {
            return MsShinsa.GetRecordsBy証書リンク(loginUser, ks_shousho_id);
        }

        public List<MsShinsa> MsShinsa_GetRecordsByName(MsUser loginUser, string Name)
        {
            return MsShinsa.GetRecordsByName(loginUser, Name);
        }

        public bool MsShinsa_InsertRecord(MsUser loginUser, MsShinsa msShinsa)
        {
            return msShinsa.InsertRecord(loginUser);
        }

        public bool MsShinsa_UpdateRecord(MsUser loginUser, MsShinsa msShinsa)
        {
            return msShinsa.UpdateRecord(loginUser);
        }


        public List<KsShinsa> KsShinsa_GetRecordsByMsShinsaID(MsUser loginUser, string ms_shinsa_id)
        {
            return KsShinsa.GetRecordsByMsShinsaID(loginUser, ms_shinsa_id);
        }
    }
}
