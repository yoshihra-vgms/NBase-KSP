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
using System.Collections.Generic;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.DAC.MsBumon> MsBumon_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.MsBumon MsBumon_GetRecordsByBumonID(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        bool MsBumon_Insert(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        bool MsBumon_Update(NBaseData.DAC.MsUser loginUser);

    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsBumon> MsBumon_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsBumon> all;
            all = NBaseData.DAC.MsBumon.GetRecords(loginUser);

            var ret = all.Where(o => o.BumonName.Contains("予備") == false).OrderBy(o => o.MsBumonID).ToList();
            return ret;
        }

        public NBaseData.DAC.MsBumon MsBumon_GetRecordsByBumonID(NBaseData.DAC.MsUser loginUser)
        {
            NBaseData.DAC.MsBumon ret;
            ret = NBaseData.DAC.MsBumon.GetRecordsByBumonID(loginUser, "k");
            return ret;
        }

        public bool MsBumon_Insert(NBaseData.DAC.MsUser loginUser)
        {
            return true;
        }

        public bool MsBumon_Update(NBaseData.DAC.MsUser loginUser)
        {
            return true;
        }

    }

}
