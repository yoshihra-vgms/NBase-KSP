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
        List<MsKensa> MsKensa_GetRecords(MsUser loginUser);

        [OperationContract]
        List<MsKensa> MsKensa_GetRecordsBy証書リンクデータ(MsUser loginUser, string ks_shousho_id);

        [OperationContract]
        List<MsKensa> MsKensa_GetRecordsBy荷役リンクデータ(MsUser loginUser, string ks_niyaku_id);

        [OperationContract]
        List<MsKensa> MsKensa_GetRecordsBy検査名(MsUser loginUser, string kensa_name);

        [OperationContract]
        MsKensa MsKensa_GetRecord(MsUser loginUser, string ms_kensa_id);

        [OperationContract]
        bool MsKensa_InsertRecord(MsUser loginUser, MsKensa data);
        [OperationContract]
        bool MsKensa_UpdateRecord(MsUser loginUser, MsKensa data);
    }

    public partial class Service
    {
        public List<MsKensa> MsKensa_GetRecords(MsUser loginUser)
        {
            return MsKensa.GetRecords(loginUser);
        }

        public List<MsKensa> MsKensa_GetRecordsBy証書リンクデータ(MsUser loginUser, string ks_shousho_id)
        {
            return MsKensa.GetRecordsBy証書リンクデータ(loginUser, ks_shousho_id);
        }

        public List<MsKensa> MsKensa_GetRecordsBy荷役リンクデータ(MsUser loginUser, string ks_niyaku_id)
        {
            return MsKensa.GetRecordsBy荷役リンクデータ(loginUser, ks_niyaku_id);
        }

        public List<MsKensa> MsKensa_GetRecordsBy検査名(MsUser loginUser, string kensa_name)
        {
            return MsKensa.GetRecordsBy検査名(loginUser, kensa_name);
        }


        public MsKensa MsKensa_GetRecord(MsUser loginUser, string ms_kensa_id)
        {
            return MsKensa.GetRecord(loginUser, ms_kensa_id);
        }


        //挿入
        public bool MsKensa_InsertRecord(MsUser loginUser, MsKensa data)
        {

            return data.InsertRecord(loginUser);
        }
        public bool MsKensa_UpdateRecord(MsUser loginUser, MsKensa data)
        {

            return data.UpdateRecord(loginUser);
        }

    }
}
