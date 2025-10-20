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
        List<MsSiMenjouKind> MsSiMenjouKind_GetRecords(MsUser loginUser);


        [OperationContract]
        List<MsSiMenjouKind> MsSiMenjouKind_SearchRecords(MsUser loginUser, int msSiMenjouId, string name, string nameAbbr);


        [OperationContract]
        bool MsSiMenjouKind_InsertOrUpdate(MsUser loginUser, MsSiMenjouKind m);


        [OperationContract]
        List<MsSiMenjouKind> MsSiMenjouKind_GetRecordsByMsSiMenjouID(MsUser loginUser, int ms_si_menjou_id);
    }

    public partial class Service
    {
        public List<MsSiMenjouKind> MsSiMenjouKind_GetRecords(MsUser loginUser)
        {
            return MsSiMenjouKind.GetRecords(loginUser);
        }


        public List<MsSiMenjouKind> MsSiMenjouKind_SearchRecords(MsUser loginUser, int msSiMenjouId, string name, string nameAbbr)
        {
            return MsSiMenjouKind.SearchRecords(loginUser, msSiMenjouId, name, nameAbbr);
        }


        public bool MsSiMenjouKind_InsertOrUpdate(MsUser loginUser, MsSiMenjouKind m)
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

        public List<MsSiMenjouKind> MsSiMenjouKind_GetRecordsByMsSiMenjouID(MsUser loginUser, int ms_si_menjou_id)
        {
            return MsSiMenjouKind.GetRecordsByMsSiMenjouID(loginUser, ms_si_menjou_id);
        }
    }
}
