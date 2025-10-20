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

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.DAC.MsKamoku> MsKamoku_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.MsKamoku MsKamoku_GetRecordByHachuKamoku(NBaseData.DAC.MsUser loginUser, string thiIraiSbtId, string thiIraiShousaiId, string nyukyoKamokuId);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsKamoku> MsKamoku_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsKamoku> ret;
            ret = NBaseData.DAC.MsKamoku.GetRecords(loginUser);
            return ret;
        }

        public NBaseData.DAC.MsKamoku MsKamoku_GetRecordByHachuKamoku(NBaseData.DAC.MsUser loginUser, string thiIraiSbtId, string thiIraiShousaiId, string nyukyoKamokuId)
        {
            NBaseData.DAC.MsKamoku ret;
            ret = NBaseData.DAC.MsKamoku.GetRecordByHachuKamoku(loginUser, thiIraiSbtId, thiIraiShousaiId, nyukyoKamokuId);
            return ret;
        }
    }
}
