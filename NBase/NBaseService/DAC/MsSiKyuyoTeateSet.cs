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
        List<MsSiKyuyoTeateSet> MsSiKyuyoTeateSet_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsSiKyuyoTeateSet> MsSiKyuyoTeateSet_SearchRecords(MsUser loginUser, int msSiKyuyoTeateId, int msSiShokumeiId);


        [OperationContract]
        bool MsSiKyuyoTeateSet_InsertOrUpdate(MsUser loginUser, MsSiKyuyoTeateSet m);
    }

    public partial class Service
    {
        public List<MsSiKyuyoTeateSet> MsSiKyuyoTeateSet_GetRecords(MsUser loginUser)
        {
            return MsSiKyuyoTeateSet.GetRecords(loginUser);
        }


        public List<MsSiKyuyoTeateSet> MsSiKyuyoTeateSet_SearchRecords(MsUser loginUser, int msSiKyuyoTeateId, int msSiShokumeiId)
        {
            List<MsSiKyuyoTeateSet> all = MsSiKyuyoTeateSet.GetRecords(loginUser);
            List<MsSiKyuyoTeateSet> ret = all;

            if (msSiKyuyoTeateId > 0)
            {
                ret = all.Where(obj => obj.MsSiKyuyoTeateID == msSiKyuyoTeateId).ToList();
            }
            if (msSiShokumeiId > 0)
            {
                ret = all.Where(obj => obj.MsSiShokumeiID == msSiShokumeiId).ToList();
            }

            return ret;
        }


        public bool MsSiKyuyoTeateSet_InsertOrUpdate(MsUser loginUser, MsSiKyuyoTeateSet m)
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
