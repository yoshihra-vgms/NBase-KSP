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
        List<MsViqCode> MsViqCode_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsViqCode> MsViqCode_SearchRecords(MsUser loginUser, int nameId, string name);


        [OperationContract]
        bool MsViqCode_InsertOrUpdate(MsUser loginUser, MsViqCode m);
    }

    public partial class Service
    {
        public List<MsViqCode> MsViqCode_GetRecords(MsUser loginUser)
        {
            return MsViqCode.GetRecords(loginUser);
        }


        public List<MsViqCode> MsViqCode_SearchRecords(MsUser loginUser, int nameId, string name)
        {
            return MsViqCode.SearchRecords(loginUser, nameId, name);
        }


        public bool MsViqCode_InsertOrUpdate(MsUser loginUser, MsViqCode m)
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
