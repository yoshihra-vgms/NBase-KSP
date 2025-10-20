using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        NBaseData.DAC.MsUserBumon MsUserBumon_GetRecordsByUserID(NBaseData.DAC.MsUser loginUser, string msUserID);

        [OperationContract]
        bool MsUserBumon_InsertRecord(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        bool MsUserBumon_UpdateRecord(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        bool MsUserBumon_DeleteRecord(NBaseData.DAC.MsUser loginUser, string msUserID);

		[OperationContract]
		List<NBaseData.DAC.MsUserBumon> MsUserBumon_GetRecordsByUserIDList(NBaseData.DAC.MsUser loginUser, string MsUserId);
    }

    public partial class Service
    {
        public NBaseData.DAC.MsUserBumon MsUserBumon_GetRecordsByUserID(NBaseData.DAC.MsUser loginUser, string MsUserId)
        {
            NBaseData.DAC.MsUserBumon ret;
            ret = NBaseData.DAC.MsUserBumon.GetRecordByUserID(loginUser, MsUserId);
            return ret;
        }

        public bool MsUserBumon_InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return true;
        }

        public bool MsUserBumon_UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return true;
        }

        public bool MsUserBumon_DeleteRecord(NBaseData.DAC.MsUser loginUser, string msUserID)
        {
            return true;
        }


		public List<NBaseData.DAC.MsUserBumon> MsUserBumon_GetRecordsByUserIDList(NBaseData.DAC.MsUser loginUser, string MsUserId)
		{
			return NBaseData.DAC.MsUserBumon.GetRecordsByUserID(loginUser, MsUserId);
		}
    }
}
