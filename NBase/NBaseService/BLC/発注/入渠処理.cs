using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.DAC.OdThiItem> BLC_入渠_直近ドックオーダー品目(
            NBaseData.DAC.MsUser loginUser,
            int msVesselID,
            string msThiIraiShousaiID);
    }
    public partial class Service
    {
        public List<NBaseData.DAC.OdThiItem> BLC_入渠_直近ドックオーダー品目(
            NBaseData.DAC.MsUser loginUser,
            int msVesselID,
            string msThiIraiShousaiID
            )
        {
            return NBaseData.BLC.入渠.BLC_直近ドックオーダー品目(loginUser, msVesselID, msThiIraiShousaiID);
        }
    }
 }
