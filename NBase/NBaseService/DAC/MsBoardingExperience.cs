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
        List<MsBoardingExperience> MsBoardingExperience_GetRecords(MsUser loginUser);

        [OperationContract]
        List<MsBoardingExperience> MsBoardingExperience_SearchRecords(MsUser loginUser, int kubun, int vesselId, int shokumeiId);

        [OperationContract]
        bool MsBoardingExperience_InsertOrUpdate(MsUser loginUser, MsBoardingExperience s);
    }

    public partial class Service
    {
        public List<MsBoardingExperience> MsBoardingExperience_GetRecords(MsUser loginUser)
        {
            return MsBoardingExperience.GetRecords(null, loginUser);
        }

        public List<MsBoardingExperience> MsBoardingExperience_SearchRecords(MsUser loginUser, int kubun, int vesselId, int shokumeiId)
        {
            return MsBoardingExperience.SearchRecords(null, loginUser, kubun, vesselId, shokumeiId);
        }


        public bool MsBoardingExperience_InsertOrUpdate(MsUser loginUser, MsBoardingExperience b)
        {
            b.RenewUserID = loginUser.MsUserID;
            b.RenewDate = DateTime.Now;

            if (b.IsNew())
            {
                b.MsBoardingExperienceID = System.Guid.NewGuid().ToString();
                return b.InsertRecord(loginUser);
            }
            else
            {
                return b.UpdateRecord(loginUser);
            }
        }

    }
}
