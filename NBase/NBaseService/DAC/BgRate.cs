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

namespace NBaseService
{
	public partial interface IService
	{
		[OperationContract]
		List<BgRate> BgRate_GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId);

		[OperationContract]
		bool BgRate_UpdateRecords(MsUser loginUser, List<BgRate> rates);
	}

	public partial class Service
	{
		public List<BgRate> BgRate_GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId)
		{
			return BgRate.GetRecordsByYosanHeadID(loginUser, yosanHeadId);
		}


		public bool BgRate_UpdateRecords(MsUser loginUser, List<BgRate> rates)
		{
			return BgRate.UpdateRecords(loginUser, rates);
		}
	}
}
