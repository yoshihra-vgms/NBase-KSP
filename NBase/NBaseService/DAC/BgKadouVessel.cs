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
		List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadId, int msVesselId);

        [OperationContract]
        List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselIDAndYearRange(MsUser loginUser, int yosanHeadId, int msVesselId, int yearStart, int yearEnd);

        [OperationContract]
        List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId);

        [OperationContract]
		List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadAndYearRange(
			MsUser loginUser, int yosanhead, int yearStart, int yearEnd);

        [OperationContract]
        bool BgKadouVessel_UpdateRecords(MsUser loginUser, List<BgKadouVessel> list);

        [OperationContract]
        List<BgKadouVessel> BgKadouVessel_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id);
    }

	public partial class Service
	{
		public List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadId, int msVesselId)
		{
			return BgKadouVessel.GetRecordsByYosanHeadIDAndMsVesselID(
				loginUser, yosanHeadId, msVesselId);
		}

        public List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselIDAndYearRange(MsUser loginUser, int yosanHeadId, int msVesselId, int yearStart, int yearEnd)
        {
            return BgKadouVessel.GetRecordsByYosanHeadIDAndMsVesselIDAndYearRange(
                loginUser, yosanHeadId, msVesselId, yearStart, yearEnd);
        }

        public List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId)
        {
            return BgKadouVessel.GetRecordsByYosanHeadID(
                loginUser, yosanHeadId);
        }

        //予算頭と年を指定して稼動を取得する
        public List<BgKadouVessel> BgKadouVessel_GetRecordsByYosanHeadAndYearRange(
			MsUser loginUser, int yosanhead, int yearStart, int yearEnd)
		{
			return BgKadouVessel.GetRecordsByYosanHeadAndYearRange
				(loginUser, yosanhead, yearStart, yearEnd);
		}

        public bool BgKadouVessel_UpdateRecords(MsUser loginUser, List<BgKadouVessel> list)
        {
            return BgKadouVessel.UpdateRecords(loginUser, list);
        }


        public List<BgKadouVessel> BgKadouVessel_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            return BgKadouVessel.GetRecordsByMsVesselID(loginUser, ms_vessel_id);
        }

    }
}
