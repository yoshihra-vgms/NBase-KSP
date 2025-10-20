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
        List<MsViqCodeName> MsViqCodeName_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsViqCodeName> MsViqCodeName_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsViqCodeName_InsertOrUpdate(MsUser loginUser, MsViqCodeName m);
    }

    public partial class Service
    {
        public List<MsViqCodeName> MsViqCodeName_GetRecords(MsUser loginUser)
        {
            return MsViqCodeName.GetRecords(loginUser);
        }


        public List<MsViqCodeName> MsViqCodeName_SearchRecords(MsUser loginUser, string name)
        {
            return MsViqCodeName.SearchRecords(loginUser, name);
        }


        public bool MsViqCodeName_InsertOrUpdate(MsUser loginUser, MsViqCodeName m)
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
