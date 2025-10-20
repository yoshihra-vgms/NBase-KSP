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
        List<MsSiKoushu> MsSiKoushu_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsSiKoushu> MsSiKoushu_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsSiKoushu_InsertOrUpdate(MsUser loginUser, MsSiKoushu k);
    }

    public partial class Service
    {
        public List<MsSiKoushu> MsSiKoushu_GetRecords(MsUser loginUser)
        {
            return MsSiKoushu.GetRecords(loginUser);
        }


        public List<MsSiKoushu> MsSiKoushu_SearchRecords(MsUser loginUser, string name)
        {
            return MsSiKoushu.SearchRecords(loginUser, name);
        }


        public bool MsSiKoushu_InsertOrUpdate(MsUser loginUser, MsSiKoushu k)
        {
            k.RenewUserID = loginUser.MsUserID;
            k.RenewDate = DateTime.Now;

            if(k.IsNew())
            {
                return k.InsertRecord(loginUser);
            }
            else
            {
                return k.UpdateRecord(loginUser);
            }
        }
    }
}
