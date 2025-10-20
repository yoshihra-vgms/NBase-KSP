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
		List<BgJiseki> BgJiseki_GetRecords_年単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd);

		[OperationContract]
		List<BgJiseki> BgJiseki_GetRecords_月単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd);

		[OperationContract]
		List<BgJiseki> BgJiseki_GetRecords_年単位_全社(MsUser loginUser, string yearStart, string yearEnd);

		[OperationContract]
		List<BgJiseki> BgJiseki_GetRecords_年単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd);

		[OperationContract]
		List<BgJiseki> BgJiseki_GetRecordsByVesselTypePriodBumonHimokus(
			MsUser loginUser, string vesseltype, string start, string end, int bumonid);

		[OperationContract]
		List<BgJiseki> BgJiseki_GetRecordsByVesselTypePriodHimokus(
			MsUser loginUser, string vesseltype, string start, string end);

        [OperationContract]
        List<BgJiseki> BgJiseki_GetRecords_月単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd);

        [OperationContract]
        List<BgJiseki> BgJiseki_GetRecords_月単位_全社(MsUser loginUser, string yearStart, string yearEnd);

        [OperationContract]
        List<BgJiseki> BgJiseki_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id);
	}

	public partial class Service
	{
		public List<BgJiseki> BgJiseki_GetRecords_年単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd)
		{
			return BgJiseki.GetRecords_年単位_船(loginUser, msVesselId, jisekiDateStart, jisekiDateEnd);
		}


		public List<BgJiseki> BgJiseki_GetRecords_月単位_船(MsUser loginUser, int msVesselId, string jisekiDateStart, string jisekiDateEnd)
		{
			return BgJiseki.GetRecords_月単位_船(loginUser, msVesselId, jisekiDateStart, jisekiDateEnd);
		}

		public List<BgJiseki> BgJiseki_GetRecords_年単位_全社(MsUser loginUser, string yearStart, string yearEnd)
		{
			return BgJiseki.GetRecords_年単位_全社(loginUser, yearStart, yearEnd);
		}

		public List<BgJiseki> BgJiseki_GetRecords_年単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd)
		{
			return BgJiseki.GetRecords_年単位_グループ(
				loginUser, msVesselTypeId, yearStart, yearEnd);
		}

		public List<BgJiseki> BgJiseki_GetRecordsByVesselTypePriodBumonHimokus(
			MsUser loginUser, string vesseltype, string start, string end, int bumonid)
		{
			return BgJiseki.GetRecordsByVesselTypePriodBumonHimokus
				(loginUser, vesseltype, start, end, bumonid);
		}

		//実績を船種別ごとの合計を費目別に取得(全部)
		public List<BgJiseki> BgJiseki_GetRecordsByVesselTypePriodHimokus(
			MsUser loginUser, string vesseltype, string start, string end)
		{
			return BgJiseki.GetRecordsByVesselTypePriodHimokus
				(loginUser, vesseltype, start, end);
		}

        public List<BgJiseki> BgJiseki_GetRecords_月単位_グループ(MsUser loginUser, string msVesselTypeId, string yearStart, string yearEnd)
        {            
            return BgJiseki.GetRecords_月単位_グループ(loginUser, msVesselTypeId, yearStart, yearEnd);
        }

        public List<BgJiseki> BgJiseki_GetRecords_月単位_全社(MsUser loginUser, string yearStart, string yearEnd)
        {
            return BgJiseki.GetRecords_月単位_全社(loginUser, yearStart, yearEnd);
        }


        public List<BgJiseki> BgJiseki_GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            return BgJiseki.GetRecordsByMsVesselID(loginUser, ms_vessel_id);
        }
	}
}
