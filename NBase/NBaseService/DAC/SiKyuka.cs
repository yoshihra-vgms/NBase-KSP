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
        List<SiKyuka> SiKyuka_GetRecordsByNendo(MsUser loginUser, string nendo);
        
        [OperationContract]
        bool SiKyuka_InsertOrUpdate(MsUser loginUser, List<SiKyuka> kyukaList);
    }

    public partial class Service
    {
        public List<SiKyuka> SiKyuka_GetRecordsByNendo(MsUser loginUser, string nendo)
        {
            return SiKyuka.GetRecordsByNendo(loginUser, nendo);
        }

        public bool SiKyuka_InsertOrUpdate(MsUser loginUser, List<SiKyuka> kyukaList)
        {
            bool retValue = true;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    foreach (SiKyuka s in kyukaList)
                    {
                        bool ret = true;
                        s.RenewUserID = loginUser.MsUserID;
                        s.RenewDate = DateTime.Now;

                        if (s.IsNew())
                        {
                            s.SiKyukaID = System.Guid.NewGuid().ToString();
                            ret = s.InsertRecord(loginUser);
                        }
                        else
                        {
                            ret = s.UpdateRecord(loginUser);
                        }
                        if (!ret)
                        {
                            retValue = ret;
                            break;
                        }
                    }
                    dbConnect.Commit();
                }
                catch (Exception e)
                {
                    retValue = false;
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                }
            }

            return retValue;
        }
    }
}
