using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;
using NBaseData.DS;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<SiCardPlan> SiCardPlan_GetRecords(MsUser loginUser);


        [OperationContract]
        bool  SiCardPlan_DeleteRecord(MsUser loginUser, SiCardPlan plan);


    }

    public partial class Service
    {
        public List<SiCardPlan> SiCardPlan_GetRecords(MsUser loginUser)
        {
            return SiCardPlan.GetRecords(loginUser);
        }

        public bool SiCardPlan_DeleteRecord(MsUser loginUser, SiCardPlan plan)
        {
            bool ret = true;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                plan.RenewUserID = loginUser.MsUserID;
                plan.RenewDate = DateTime.Now;
                plan.DeleteFlag = 1;

                try
                {
                    //更新
                    ret = plan.UpdateRecord(dbConnect, loginUser);

                    if (ret == true)
                    {
                        dbConnect.Commit();
                    }
                    else
                    {
                        dbConnect.RollBack();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                }

            }
            return ret;
        }
    }
}
