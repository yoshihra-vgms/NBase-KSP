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
        List<MsSiDaikoumoku> MsSiDaikoumoku_GetRecords(MsUser loginUser);


        [OperationContract]
		List<MsSiDaikoumoku> MsSiDaikoumoku_SearchRecords(MsUser loginUser, string name, MsSiHiyouKamoku ms_sihi);


        [OperationContract]
        bool MsSiDaikoumoku_InsertOrUpdate(MsUser loginUser, MsSiDaikoumoku m);


        [OperationContract]
        List<MsSiDaikoumoku> MsSiDaikoumoku_GetRecordsByMsSiHiyouKamokuID(MsUser loginUser, int ms_sihi_id);
    }

    public partial class Service
    {
        public List<MsSiDaikoumoku> MsSiDaikoumoku_GetRecords(MsUser loginUser)
        {
            return MsSiDaikoumoku.GetRecords(loginUser);
        }


		public List<MsSiDaikoumoku> MsSiDaikoumoku_SearchRecords(MsUser loginUser, string name, MsSiHiyouKamoku ms_sihi)
        {
			return MsSiDaikoumoku.SearchRecords(loginUser, name, ms_sihi);
        }


        public bool MsSiDaikoumoku_InsertOrUpdate(MsUser loginUser, MsSiDaikoumoku m)
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


        public List<MsSiDaikoumoku> MsSiDaikoumoku_GetRecordsByMsSiHiyouKamokuID(MsUser loginUser, int ms_sihi_id)
        {
            return MsSiDaikoumoku.GetRecordsByMsSiHiyouKamokuID(loginUser, ms_sihi_id);
        }
    }
}
