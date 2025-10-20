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
		BgYosanBikou BgYosanBikou_GetRecordByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadId, int msVesselId);

        [OperationContract]
        List<BgYosanBikou> BgYosanBikou_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id);
    }

	public partial class Service
	{
		public BgYosanBikou BgYosanBikou_GetRecordByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadId, int msVesselId)
		{
			return BgYosanBikou.GetRecordByYosanHeadIDAndMsVesselID(
				loginUser, yosanHeadId, msVesselId);
		}

        public List<BgYosanBikou> BgYosanBikou_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            return BgYosanBikou.GetRecordsByMsVesselID(loginUser, ms_vessel_id);
        }
    }
}
