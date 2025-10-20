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
        List<BgYosanItem> BgYosanItem_GetRecords_年単位_船(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd);

		[OperationContract]
		List<BgYosanItem> BgYosanItem_GetRecords_月単位(MsUser msUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd);

		[OperationContract]
		List<BgYosanItem> BgYosanItem_GetRecords_年単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd);

		[OperationContract]
		List<BgYosanItem> BgYosanItem_GetRecords_年単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd);

		[OperationContract]
		BgYosanItem BgYosanItem_GetRecordByYearHimokuIDMsVesselID(MsUser loginUser, int yosanheadid, int year, int himokuid, int vesselid);

		[OperationContract]
		List<BgYosanItem> BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeBumonIDHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype, int bumonid);

		[OperationContract]
		List<BgYosanItem> BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype);

        [OperationContract]
        List<BgYosanItem> BgYosanItem_GetRecords_月単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd);

        [OperationContract]
        List<BgYosanItem> BgYosanItem_GetRecords_月単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd);

        [OperationContract]
        bool BgYosanItem_UpdateRecords(MsUser loginUser, List<BgYosanItem> yosanItems);
    }

    public partial class Service
    {
        public List<BgYosanItem> BgYosanItem_GetRecords_年単位_船(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
        {
            return BgYosanItem.GetRecords_年単位_船(loginUser, yosanHeadId, msVesselId, yearStart, yearEnd);
        }

		public List<BgYosanItem> BgYosanItem_GetRecords_月単位(MsUser msUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
		{
			return BgYosanItem.GetRecords_月単位(msUser, yosanHeadId, msVesselId, yearStart, yearEnd);
		}

        public List<BgYosanItem> BgYosanItem_GetRecords_月単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd)
        {
            return BgYosanItem.GetRecords_月単位_グループ(loginUser, yosanHeadId, msVesselTypeId,
                yearStart, yearEnd);
        }

        public List<BgYosanItem> BgYosanItem_GetRecords_月単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd)
        {
            return BgYosanItem.GetRecords_月単位_全社(loginUser, yosanHeadId, yearStart, yearEnd);
        }

		public List<BgYosanItem> BgYosanItem_GetRecords_年単位_全社(MsUser loginUser, int yosanHeadId, string yearStart, string yearEnd)
		{
			return BgYosanItem.GetRecords_年単位_全社(loginUser, yosanHeadId, yearStart, yearEnd);
		}

		public List<BgYosanItem> BgYosanItem_GetRecords_年単位_グループ(MsUser loginUser, int yosanHeadId, string msVesselTypeId, string yearStart, string yearEnd)
		{
			return BgYosanItem.GetRecords_年単位_グループ(loginUser, yosanHeadId, msVesselTypeId, yearStart, yearEnd);
		}

		public BgYosanItem BgYosanItem_GetRecordByYearHimokuIDMsVesselID(MsUser loginUser, int yosanheadid, int year, int himokuid, int vesselid)
		{
			return BgYosanItem.GetRecordByYearHimokuIDMsVesselID(
				loginUser, yosanheadid, year, himokuid, vesselid);
		}

		//船種別ごとの合計を費目別に取得
		public List<BgYosanItem> BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeBumonIDHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype, int bumonid)
		{
			return BgYosanItem.GetRecordsByYosanHeadPriodVesselTypeBumonIDHimokus(
				loginUser, yosanheadid, start, end, vesseltype, bumonid);

		}

		//計画を船種別ごとの合計を費目別に取得(全部)
		public List<BgYosanItem> BgYosanItem_GetRecordsByYosanHeadPriodVesselTypeHimokus(
			MsUser loginUser, int yosanheadid, string start, string end, string vesseltype)
		{
			return BgYosanItem.GetRecordsByYosanHeadPriodVesselTypeHimokus(
				loginUser, yosanheadid, start, end, vesseltype);
		}

        public bool BgYosanItem_UpdateRecords(MsUser loginUser, List<BgYosanItem> list)
        {
            return BgYosanItem.UpdateRecords(loginUser, list);
        }
    }
}
