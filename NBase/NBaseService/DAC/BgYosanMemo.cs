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
		List<BgYosanMemo> BgYosanMemo_GetRecordsByYosanHeadID(MsUser msUser, int p);

		[OperationContract]
		bool BgYosanMemo_UpdateRecord(MsUser msUser, BgYosanMemo editedYosanMemo);
	}

	public partial class Service
	{
		public List<BgYosanMemo> BgYosanMemo_GetRecordsByYosanHeadID(MsUser msUser, int p)
		{
			return BgYosanMemo.GetRecordsByYosanHeadID(msUser, p);
		}

		public bool BgYosanMemo_UpdateRecord(MsUser msUser, BgYosanMemo editedYosanMemo)
		{
			return editedYosanMemo.UpdateRecord(msUser);
		}
	}
}
