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
        List<MsSiAllowance> MsSiAllowance_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsSiAllowance> MsSiAllowance_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsSiAllowance_InsertOrUpdate(MsUser loginUser, MsSiAllowance m);
    }

    public partial class Service
    {
        public List<MsSiAllowance> MsSiAllowance_GetRecords(MsUser loginUser)
        {
            return MsSiAllowance.GetRecords(loginUser);
        }


        public List<MsSiAllowance> MsSiAllowance_SearchRecords(MsUser loginUser, string name)
        {
            List<MsSiAllowance> all = MsSiAllowance.GetRecords(loginUser);
            List<MsSiAllowance> ret = all;

            if (name != null && name.Length > 0)
            {
                ret = all.Where(obj => obj.Name.Contains(name)).ToList();
            }

            return ret;
        }


        public bool MsSiAllowance_InsertOrUpdate(MsUser loginUser, MsSiAllowance m)
        {
            m.RenewUserID = loginUser.MsUserID;
            m.RenewDate = DateTime.Now;

            if(m.IsNew())
            {
                return m.InsertRecord(loginUser);
            }
            else
            {
                return m.UpdateRecord(loginUser);
            }
        }
    }
}
