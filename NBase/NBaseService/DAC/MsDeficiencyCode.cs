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
        List<MsDeficiencyCode> MsDeficiencyCode_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsDeficiencyCode> MsDeficiencyCode_SearchRecords(MsUser loginUser, int categoryId, string name, string defectiveItem);


        [OperationContract]
        bool MsDeficiencyCode_InsertOrUpdate(MsUser loginUser, MsDeficiencyCode m);
    }

    public partial class Service
    {
        public List<MsDeficiencyCode> MsDeficiencyCode_GetRecords(MsUser loginUser)
        {
            return MsDeficiencyCode.GetRecords(loginUser);
        }


        public List<MsDeficiencyCode> MsDeficiencyCode_SearchRecords(MsUser loginUser, int categoryId, string name, string defectiveItem)
        {
            return MsDeficiencyCode.SearchRecords(loginUser, categoryId, name, defectiveItem);
        }


        public bool MsDeficiencyCode_InsertOrUpdate(MsUser loginUser, MsDeficiencyCode m)
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
