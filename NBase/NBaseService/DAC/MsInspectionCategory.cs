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
        List<MsInspectionCategory> MsInspectionCategory_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsInspectionCategory> MsInspectionCategory_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsInspectionCategory_InsertOrUpdate(MsUser loginUser, MsInspectionCategory m);
    }

    public partial class Service
    {
        public List<MsInspectionCategory> MsInspectionCategory_GetRecords(MsUser loginUser)
        {
            return MsInspectionCategory.GetRecords(loginUser);
        }


        public List<MsInspectionCategory> MsInspectionCategory_SearchRecords(MsUser loginUser, string name)
        {
            return MsInspectionCategory.SearchRecords(loginUser, name);
        }


        public bool MsInspectionCategory_InsertOrUpdate(MsUser loginUser, MsInspectionCategory m)
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
