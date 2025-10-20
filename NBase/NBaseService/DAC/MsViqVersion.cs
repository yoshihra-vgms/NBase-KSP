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
        List<MsViqVersion> MsViqVersion_GetRecords(MsUser loginUser);


        [OperationContract]
        bool MsViqVersion_InsertOrUpdate(MsUser loginUser, MsViqVersion m);
    }

    public partial class Service
    {
        public List<MsViqVersion> MsViqVersion_GetRecords(MsUser loginUser)
        {
            return MsViqVersion.GetRecords(loginUser);
        }


        public bool MsViqVersion_InsertOrUpdate(MsUser loginUser, MsViqVersion m)
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
