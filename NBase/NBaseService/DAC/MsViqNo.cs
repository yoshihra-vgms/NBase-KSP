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
        List<MsViqNo> MsViqNo_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsViqNo> MsViqNo_SearchRecords(MsUser loginUser, int codeId, string name);


        [OperationContract]
        bool MsViqNo_InsertOrUpdate(MsUser loginUser, MsViqNo m);
    }

    public partial class Service
    {
        public List<MsViqNo> MsViqNo_GetRecords(MsUser loginUser)
        {
            return MsViqNo.GetRecords(loginUser);
        }


        public List<MsViqNo> MsViqNo_SearchRecords(MsUser loginUser, int codeId, string name)
        {
            return MsViqNo.SearchRecords(loginUser, codeId, name);
        }


        public bool MsViqNo_InsertOrUpdate(MsUser loginUser, MsViqNo m)
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
