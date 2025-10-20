using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        SiAllowance SiAllowance_GetRecord(MsUser loginUser, string siAllowanceId);

        [OperationContract]
        List<SiAllowance> SiAllowance_SearchRecords(MsUser loginUser, string from, string to, int vesselId, string allowanceName);

        [OperationContract]
        bool SiAllowance_InsertOrUpdate(MsUser loginUser, string id, SiAllowance allowance, List<SiAllowanceDetail> details);
    }

    public partial class Service
    {
        public SiAllowance SiAllowance_GetRecord(MsUser loginUser, string siAllowanceId)
        {
            return SiAllowance.GetRecord(loginUser, siAllowanceId);
        }

        public List<SiAllowance> SiAllowance_SearchRecords(MsUser loginUser, string from, string to, int vesselId, string allowanceName)
        {
            List<SiAllowance> ret = SiAllowance.GetRecords(loginUser, from, to, vesselId, allowanceName);
            return ret;
        }

        public bool SiAllowance_InsertOrUpdate(MsUser loginUser, string id, SiAllowance allowance, List<SiAllowanceDetail> details)
        {
            bool ret = true;

            allowance.RenewDate = DateTime.Now;
            allowance.RenewUserID = loginUser.MsUserID;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();
                try
                {
                    if (allowance.IsNew())
                    {
                        allowance.SiAllowanceID = id;
                        ret = allowance.InsertRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        ret = allowance.UpdateRecord(dbConnect, loginUser);
                    }
                    if (ret)
                    {
                        foreach(SiAllowanceDetail detail in details)
                        {
                            detail.SiAllowanceID = allowance.SiAllowanceID;
                            detail.RenewDate = allowance.RenewDate;
                            detail.RenewUserID = allowance.RenewUserID;

                            if (detail.IsNew())
                            {
                                detail.SiAllowanceDetailID = System.Guid.NewGuid().ToString();
                                ret = detail.InsertRecord(dbConnect, loginUser);
                            }
                            else
                            {
                                ret = detail.UpdateRecord(dbConnect, loginUser);
                            }
                        }

                    }
                    if (ret)
                    {
                        dbConnect.Commit();
                    }
                    else
                    {
                        dbConnect.RollBack();
                    }
                }
                catch(Exception ex)
                {
                    dbConnect.RollBack();
                }
            }

            return ret;
        }
    }
}
