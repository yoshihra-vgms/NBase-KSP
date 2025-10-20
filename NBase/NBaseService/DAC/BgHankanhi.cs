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
		BgHankanhi BgHankanhi_GetRecordByYosanHeadIDYear(MsUser loginUser, int headid, int year);
        
        [OperationContract]
        bool BgHankanhi_InsertRecord(MsUser loginUser, BgHankanhi hankanhi);

        [OperationContract]
        bool BgHankanhi_UpdateRecord(MsUser loginUser, BgHankanhi hankanhi);
    }

	public partial class Service
	{
		public BgHankanhi BgHankanhi_GetRecordByYosanHeadIDYear(MsUser loginUser, int headid, int year)
		{
			return BgHankanhi.GetRecordByYosanHeadIDYear(
				loginUser, headid, year);
		}
        
        public bool BgHankanhi_InsertRecord(MsUser loginUser, BgHankanhi hankanhi)
        {
            return hankanhi.InsertRecord(loginUser);
        }
        
        public bool BgHankanhi_UpdateRecord(MsUser loginUser, BgHankanhi hankanhi)
        {
            return hankanhi.UpdateRecord(loginUser);
        }
    }
}
