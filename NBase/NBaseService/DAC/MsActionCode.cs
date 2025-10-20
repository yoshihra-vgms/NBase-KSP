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
        List<MsActionCode> MsActionCode_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsActionCode> MsActionCode_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsActionCode_InsertOrUpdate(MsUser loginUser, MsActionCode m);
    }

    public partial class Service
    {
        public List<MsActionCode> MsActionCode_GetRecords(MsUser loginUser)
        {
            return MsActionCode.GetRecords(loginUser);
        }


        public List<MsActionCode> MsActionCode_SearchRecords(MsUser loginUser, string name)
        {
            return MsActionCode.SearchRecords(loginUser, name);
        }


        public bool MsActionCode_InsertOrUpdate(MsUser loginUser, MsActionCode m)
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
