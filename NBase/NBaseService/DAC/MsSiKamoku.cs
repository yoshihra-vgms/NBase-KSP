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
        List<MsSiKamoku> MsSiKamoku_GetRecords(MsUser loginUser);

        [OperationContract]
        List<MsSiKamoku> MsSiKamoku_SearchRecords(MsUser loginUser, string name);


        [OperationContract]
        bool MsSiKamoku_InsertOrUpdate(MsUser loginUser, MsSiKamoku m);
    }

    public partial class Service
    {
        public List<MsSiKamoku> MsSiKamoku_GetRecords(MsUser loginUser)
        {
            List<MsSiKamoku> ret;
            ret = MsSiKamoku.GetRecords(loginUser);
            return ret;
        }

        public List<MsSiKamoku> MsSiKamoku_SearchRecords(MsUser loginUser, string name)
        {
            return MsSiKamoku.SearchRecords(loginUser, name);
        }

        public bool MsSiKamoku_InsertOrUpdate(MsUser loginUser, MsSiKamoku m)
        {
            m.RenewUserID = loginUser.MsUserID;
            m.RenewDate = DateTime.Now;

            if (m.IsNew())
            {
                return m.InsertRecord(loginUser);
            }
            else
            {
                return m.UpdateRecord(loginUser);
            }
        }
    }
}
