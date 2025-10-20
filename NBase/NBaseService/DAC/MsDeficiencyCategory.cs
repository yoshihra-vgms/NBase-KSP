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
        List<MsDeficiencyCategory> MsDeficiencyCategory_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsDeficiencyCategory> MsDeficiencyCategory_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsDeficiencyCategory_InsertOrUpdate(MsUser loginUser, MsDeficiencyCategory m);
    }

    public partial class Service
    {
        public List<MsDeficiencyCategory> MsDeficiencyCategory_GetRecords(MsUser loginUser)
        {
            return MsDeficiencyCategory.GetRecords(loginUser);
        }


        public List<MsDeficiencyCategory> MsDeficiencyCategory_SearchRecords(MsUser loginUser, string name)
        {
            return MsDeficiencyCategory.SearchRecords(loginUser, name);
        }


        public bool MsDeficiencyCategory_InsertOrUpdate(MsUser loginUser, MsDeficiencyCategory m)
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
