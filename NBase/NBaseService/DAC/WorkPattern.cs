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
using System.ServiceModel;
using System.Collections.Generic;
using NBaseData.DAC;
using ORMapping;
using NBaseUtil;

namespace NBaseService
{
	public partial interface IService
	{
		[OperationContract]
		List<WorkPattern> WorkPattern_GetRecords(MsUser loginUser, int eventKind, int msVesselID = 0);

		[OperationContract]
		bool WorkPattern_InsertOrUpdate(MsUser loginUser, List<WorkPattern> workPatternList);
	}

	public partial class Service
	{
		public List<WorkPattern> WorkPattern_GetRecords(MsUser loginUser, int eventKind, int msVesselID = 0)
		{
			return WorkPattern.GetRecords(loginUser, eventKind, msVesselID);
		}

		public bool WorkPattern_InsertOrUpdate(MsUser loginUser, List<WorkPattern> workPatternList)
		{
			bool ret = true;

			var gid = workPatternList.Select(o => o.GID).Where(o => o != null).Distinct().FirstOrDefault();

			if (StringUtils.Empty(gid) == false)
			{
				WorkPattern.DeleteRecords(loginUser, gid);
			}
			else
            {
				gid = DateTime.Now.ToString("yyyyMMddHHmmss");
            }


			var first = workPatternList.FirstOrDefault();
			var already = WorkPattern.GetRecords(loginUser, first.EventKind, first.MsVesselID);


			foreach(WorkPattern wp in workPatternList)
            {
				if (wp.DeleteFlag == 1)
					continue;

				var delWp = already.Where(o => o.MsSiShokuemiID == wp.MsSiShokuemiID && o.WorkDate == wp.WorkDate && o.WorkDateDiff == wp.WorkDateDiff && o.WorkPatternID != wp.WorkPatternID).FirstOrDefault();
				if (delWp != null)
                {
					delWp.DeleteFlag = 1;
					delWp.RenewDate = DateTime.Now;
					delWp.RenewUserID = loginUser.MsUserID;
					ret = delWp.UpdateRecord(loginUser);
				}

				wp.GID = gid;
				wp.RenewDate = DateTime.Now;
				wp.RenewUserID = loginUser.MsUserID;

				if (wp.WorkPatternID == 0)
				{
					ret = wp.InsertRecord(loginUser);
				}
				else
				{
					ret = wp.UpdateRecord(loginUser);
				}
            }

			return ret;
		}
	}
}
