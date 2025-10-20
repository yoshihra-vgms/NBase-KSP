using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<MsItemKind> MsItemKind_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsItemKind> MsItemKind_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsItemKind_InsertOrUpdate(MsUser loginUser, MsItemKind m);
    }

    public partial class Service
    {
        public List<MsItemKind> MsItemKind_GetRecords(MsUser loginUser)
        {
            return MsItemKind.GetRecords(loginUser);
        }


        public List<MsItemKind> MsItemKind_SearchRecords(MsUser loginUser, string name)
        {
            return MsItemKind.SearchRecords(loginUser, name);
        }


        public bool MsItemKind_InsertOrUpdate(MsUser loginUser, MsItemKind m)
        {
            m.UpdateMsUserID = loginUser.MsUserID;

            if (m.IsNew())
            {
                m.CreateMsUserID = loginUser.MsUserID;
                return m.InsertRecord(loginUser);
            }
            else
            {
                return m.UpdateRecord(loginUser);
            }
        }
    }
}
