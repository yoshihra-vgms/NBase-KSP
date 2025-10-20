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
        List<MsSiMenjou> MsSiMenjou_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsSiMenjou> MsSiMenjou_SearchRecords(MsUser loginUser, string name, string nameAbbr);


        [OperationContract]
        bool MsSiMenjou_InsertOrUpdate(MsUser loginUser, MsSiMenjou m);
    }

    public partial class Service
    {
        public List<MsSiMenjou> MsSiMenjou_GetRecords(MsUser loginUser)
        {
            return MsSiMenjou.GetRecords(loginUser);
        }


        public List<MsSiMenjou> MsSiMenjou_SearchRecords(MsUser loginUser, string name, string nameAbbr)
        {
            return MsSiMenjou.SearchRecords(loginUser, name, nameAbbr);
        }


        public bool MsSiMenjou_InsertOrUpdate(MsUser loginUser, MsSiMenjou m)
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
