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
        List<MsSiMeisai> MsSiMeisai_GetRecords(MsUser loginUser);

        [OperationContract]
        List<MsSiMeisai> MsSiMeisai_GetRecords削除を含む(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        MsSiMeisai MsSiMeisai_GetRecord(MsUser loginUser, int meisaiId);

        [OperationContract]
		List<MsSiMeisai> MsSiMeisai_SearchRecords(MsUser loginUser, string name, MsSiHiyouKamoku himo, MsSiDaikoumoku kou);

        [OperationContract]
        List<MsSiMeisai> MsSiMeisai_GetRecordsByMsSiDaikoumokuID(MsUser loginUser, int sidaikoumoku_id);

        [OperationContract]
        bool MsSiMeisai_InsertOrUpdate(MsUser loginUser, MsSiMeisai m);

        [OperationContract]
        List<MsSiMeisai> MsSiMeisai_GetRecordsByMsSiKamokuID(MsUser loginUser, int ms_sikamoku_id);
    }

    public partial class Service
    {
        public List<MsSiMeisai> MsSiMeisai_GetRecords(MsUser loginUser)
        {
            return MsSiMeisai.GetRecords(loginUser);
        }

        public List<MsSiMeisai> MsSiMeisai_GetRecords削除を含む(MsUser loginUser)
        {
            return MsSiMeisai.GetRecords削除を含む(loginUser);
        }

        public MsSiMeisai MsSiMeisai_GetRecord(MsUser loginUser, int meisaiId)
        {
            return MsSiMeisai.GetRecord(loginUser,meisaiId);
        }

		public List<MsSiMeisai> MsSiMeisai_SearchRecords(MsUser loginUser, string name, MsSiHiyouKamoku himo, MsSiDaikoumoku kou)
        {
            return MsSiMeisai.SearchRecords(loginUser, name, himo, kou);
        }

        public List<MsSiMeisai> MsSiMeisai_GetRecordsByMsSiDaikoumokuID(MsUser loginUser, int sidaikoumoku_id)
        {
            return MsSiMeisai.GetRecordsByMsSiDaikoumokuID(loginUser, sidaikoumoku_id);
        }

        public bool MsSiMeisai_InsertOrUpdate(MsUser loginUser, MsSiMeisai m)
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


        public List<MsSiMeisai> MsSiMeisai_GetRecordsByMsSiKamokuID(MsUser loginUser, int ms_sikamoku_id)
        {
            return MsSiMeisai.GetRecordsByMsSiKamokuID(loginUser, ms_sikamoku_id);
        }
    }
}
