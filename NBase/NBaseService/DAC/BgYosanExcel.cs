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
        BgYosanExcel BgYosanExcel_GetRecordByYosanHeadIDAndMsVesselIdAndShubetsu(MsUser loginUser, int yosanHeadId, int msVesselId, int shubetsu);

        [OperationContract]
        bool BgYosanExcel_InsertOrUpdate(MsUser loginUser, BgYosanExcel yosanExcel);
    }

	public partial class Service
	{
        public BgYosanExcel BgYosanExcel_GetRecordByYosanHeadIDAndMsVesselIdAndShubetsu(MsUser loginUser, int yosanHeadId, int msVesselId, int shubetsu)
		{
            return BgYosanExcel.GetRecordByYosanHeadIDAndMsVesselIdAndShubetsu(loginUser, yosanHeadId, msVesselId, shubetsu);
		}


        public bool BgYosanExcel_InsertOrUpdate(MsUser loginUser, BgYosanExcel yosanExcel)
        {
            return BgYosanExcel.InsertOrUpdate(loginUser, yosanExcel);
        }
    }
}
