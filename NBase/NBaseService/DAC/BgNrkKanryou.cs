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
		List<BgNrkKanryou> BgNrkKanryou_GetRecordsByYosanHeadID(MsUser msUser, int p);

		[OperationContract]
		bool BgNrkKanryou_UpdateRecord(MsUser msUser, BgNrkKanryou k);

        [OperationContract]
        List<BgNrkKanryou> BgNrkKanryou_GetRecordsByMsUserID(MsUser loginUser, string ms_user_id);
	}

	public partial class Service
	{
		public List<BgNrkKanryou> BgNrkKanryou_GetRecordsByYosanHeadID(MsUser msUser, int p)
		{
			return BgNrkKanryou.GetRecordsByYosanHeadID(msUser, p);
		}


		public bool BgNrkKanryou_UpdateRecord(MsUser msUser, BgNrkKanryou k)
		{
			return k.UpdateRecord(msUser);
		}

        public List<BgNrkKanryou> BgNrkKanryou_GetRecordsByMsUserID(MsUser loginUser, string ms_user_id)
        {
            return BgNrkKanryou.GetRecordsByMsUserID(loginUser, ms_user_id);
        }
	}
}