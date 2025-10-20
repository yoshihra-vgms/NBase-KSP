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
        List<MsSiHiyouKamoku> MsSiHiyouKamoku_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsSiHiyouKamoku> MsSiHiyouKamoku_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsSiHiyouKamoku_InsertOrUpdate(MsUser loginUser, MsSiHiyouKamoku m);
    }

    public partial class Service
    {
        public List<MsSiHiyouKamoku> MsSiHiyouKamoku_GetRecords(MsUser loginUser)
        {
            return MsSiHiyouKamoku.GetRecords(loginUser);
        }


        public List<MsSiHiyouKamoku> MsSiHiyouKamoku_SearchRecords(MsUser loginUser, string name)
        {
            return MsSiHiyouKamoku.SearchRecords(loginUser, name);
        }


        public bool MsSiHiyouKamoku_InsertOrUpdate(MsUser loginUser, MsSiHiyouKamoku m)
        {
            m.RenewUserID = loginUser.MsUserID;
            m.RenewDate = DateTime.Now;

            if (m.IsNew())
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
