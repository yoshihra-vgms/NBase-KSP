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

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.DAC.MsHimoku> MsHimoku_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.MsHimoku MsHimoku_GetRecord(NBaseData.DAC.MsUser loginUser, int msHimokuId);

		[OperationContract]
		List<MsHimoku> MsHimoku_GetRecordsWithMsKamoku(MsUser loginUser);

		[OperationContract]
		List<MsHimoku> MsHimoku_GetRecordsByMsBumonID(MsUser loginUser, int bumonid);
	}

    public partial class Service
    {
        public List<NBaseData.DAC.MsHimoku> MsHimoku_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsHimoku> ret;
            ret = NBaseData.DAC.MsHimoku.GetRecords(loginUser);
            return ret;
        }

        public NBaseData.DAC.MsHimoku MsHimoku_GetRecord(NBaseData.DAC.MsUser loginUser, int msHimokuId)
        {
            NBaseData.DAC.MsHimoku ret;
            ret = NBaseData.DAC.MsHimoku.GetRecord(loginUser, msHimokuId);
            return ret;
        }

		public List<MsHimoku> MsHimoku_GetRecordsWithMsKamoku(MsUser loginUser)
		{
			return MsHimoku.GetRecordsWithMsKamoku(loginUser);
		}


		public List<MsHimoku> MsHimoku_GetRecordsByMsBumonID(MsUser loginUser, int bumonid)
		{
			return MsHimoku.GetRecordsByMsBumonID(loginUser, bumonid);
		}
    }
}
