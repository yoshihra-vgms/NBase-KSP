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
        List<DjDouseiHoukoku> DjDouseiHoukoku_GetRecordsByHoukokuDate(MsUser loginUser, DateTime houkokuDate);
        
        [OperationContract]
        DjDouseiHoukoku DjDouseiHoukoku_InsertOrUpdate(MsUser loginUser, DjDouseiHoukoku houkoku);
    }

    public partial class Service
    {
        public List<DjDouseiHoukoku> DjDouseiHoukoku_GetRecordsByHoukokuDate(MsUser loginUser, DateTime houkokuDate)
        {
            return DjDouseiHoukoku.GetRecordsByHoukokuDate(loginUser, houkokuDate);
        }

        public DjDouseiHoukoku DjDouseiHoukoku_InsertOrUpdate(MsUser loginUser, DjDouseiHoukoku houkoku)
        {
            bool ret = true;
            houkoku.RenewUserID = loginUser.MsUserID;
            houkoku.RenewDate = DateTime.Now;

            if (houkoku.IsNew())
            {
                houkoku.DjDouseiHoukokuID = System.Guid.NewGuid().ToString();
                ret = houkoku.InsertRecord(loginUser);
            }
            else
            {
                ret = houkoku.UpdateRecord(loginUser);
            }
            if (ret)
            {
                if (houkoku.DeleteFlag == 1)
                {
                    return houkoku;
                }
                else
                {
                    DjDouseiHoukoku retHoukoku = DjDouseiHoukoku.GetRecord(loginUser, houkoku.DjDouseiHoukokuID);
                    return retHoukoku;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
